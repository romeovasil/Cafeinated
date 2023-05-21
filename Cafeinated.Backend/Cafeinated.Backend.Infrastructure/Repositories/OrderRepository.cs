using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}