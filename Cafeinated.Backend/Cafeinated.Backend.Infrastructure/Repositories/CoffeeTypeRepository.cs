using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class CoffeeTypeRepository : GenericRepository<CoffeeType>
{
    public CoffeeTypeRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}