using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using File = Cafeinated.Backend.Core.Entities.File;

namespace Cafeinated.Backend.Infrastructure.Repositories.Abstractions;

public interface IUploadManager
{
    Task<ActionResponse<File>> Upload(IFormFile file);
}