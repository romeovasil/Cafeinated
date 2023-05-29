using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cafeinated.Backend.Tests.Mocks;

public class TestAppDBContext<T> where T : BaseEntity
{
    public TestAppDBContext(IEnumerable<T> testData = null)
    {
        var options = new DbContextOptionsBuilder<AppDBContext>()
            .UseInMemoryDatabase(nameof(T) + "-Database-" + Guid.NewGuid())
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        Object = new AppDBContext(options);

        if (testData is null)
        {
            return;
        }
        
        Object.Set<T>().AddRange(testData);
        Object.SaveChanges();
    }
    
    public AppDBContext Object { get; }
}