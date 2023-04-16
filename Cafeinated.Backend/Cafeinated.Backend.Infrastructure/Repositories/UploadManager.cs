using System.Text;
using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using File = Cafeinated.Backend.Core.Entities.File;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class UploadManager : IUploadManager
{
    private readonly AppDBContext _dbContext;
    private readonly DbSet<File> _dbSet;
    private readonly string _contentDir;

    public UploadManager(AppDBContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Files;
        _contentDir = configuration["Content_Directory"];
    }

    public async Task<ActionResponse<File>> Upload(IFormFile formFile)
    {
        const string subDir = "upload/files";
        var path = Path.Combine(_contentDir, subDir).Replace("\\", "/");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var file = new File
        {
            Name = SanitizeFileName(formFile.FileName) + "_" + GenerateRandomHexString(10),
            Extension = Path.GetExtension(formFile.FileName).Substring(1).ToLower(),
            OriginalName = formFile.FileName,
            Created = DateTime.Now,
            Updated = DateTime.Now,
            Id = Guid.NewGuid().ToString()
        };
        
        var filePath = Path.Combine(path, $"{file.Name}.{file.Extension}").Replace("\\", "/");
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(fileStream);
        }

        file.Path = filePath;
        var addedFile = await _dbSet.AddAsync(file);
        await _dbContext.SaveChangesAsync();

        return new ActionResponse<File>(addedFile.Entity);
    }
    
    private string SanitizeFileName(string fileName)
    {
        var newName = new StringBuilder();
        fileName = Path.GetFileNameWithoutExtension(fileName);
        foreach (var c in fileName)
        {
            newName.Append(Path.GetInvalidFileNameChars().Contains(c) ? '_' : c);
        }

        return newName.ToString();
    }
    
    private string GenerateRandomHexString(int length = 20)
    {
        var str = "";
        while (str.Length < length)
        {
            str += Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }

        return str.Substring(0, length);
    }
}