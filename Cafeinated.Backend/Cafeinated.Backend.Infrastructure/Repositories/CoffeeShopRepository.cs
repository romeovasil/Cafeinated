using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class CoffeeShopRepository : GenericRepository<CoffeeShop>
{
    public CoffeeShopRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}