using System.Linq.Expressions;
using Cafeinated.Backend.Core.Entities.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;

namespace Cafeinated.Backend.Infrastructure.Repositories.Abstractions;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<ActionResponse<IEnumerable<T>>> GetAll();
    Task<ActionResponse<IEnumerable<T>>> FindBy(Expression<Func<T, bool>> predicate);
    Task<ActionResponse<T>> GetEntityBy(Expression<Func<T, bool>> predicate);
    Task<ActionResponse<T>> Add(T entity);
    Task<ActionResponse<T>> Delete(string id);
    Task<ActionResponse<T>> Edit(T entity);
}