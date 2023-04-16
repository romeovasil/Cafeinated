using System.Linq.Expressions;
using Cafeinated.Backend.Core.Database;
using Cafeinated.Backend.Core.Entities.Abstractions;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly AppDBContext _dbContext;
    private IQueryable<T> _queryable;

    public GenericRepository(AppDBContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
        _queryable = _dbSet;
        _dbContext = dbContext;
    }
    
    public void ChainQueryable(Func<IQueryable<T>, IQueryable<T>> func)
    {
        _queryable = func(_queryable);
    }

    public async Task<ActionResponse<IEnumerable<T>>> GetAll()
    {
        var entities = await _queryable.ToListAsync();
        return new ActionResponse<IEnumerable<T>>(entities);
    }

    public async Task<ActionResponse<IEnumerable<T>>> FindBy(Expression<Func<T, bool>> predicate)
    {
        var filteredEntities = await _queryable.Where(predicate).ToListAsync();
        return new ActionResponse<IEnumerable<T>>(filteredEntities);
    }

    public async Task<ActionResponse<T>> GetEntityBy(Expression<Func<T, bool>> predicate)
    {
        var response = new ActionResponse<T>();
        
        var requestedEntity = await _queryable.FirstOrDefaultAsync(predicate);
        if (requestedEntity is null)
        {
            response.AddError("Requested entity doesn't exist!");
            return response;
        }

        response.Item = requestedEntity;
        return response;
    }

    public async Task<ActionResponse<T>> Add(T entity)
    {
        entity.Created = entity.Updated = DateTime.Now;
        entity.Id = Guid.NewGuid().ToString();

        var addedEntity = await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        
        return new ActionResponse<T>(addedEntity.Entity) ;
    }

    public async Task<ActionResponse<T>> Delete(string id)
    {
        var response = new ActionResponse<T>();
        
        var existingEntity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        if (existingEntity is null)
        {
            response.AddError("Entity doesn't exist!");
            return response;
        }

        var removedEntity = _dbSet.Remove(existingEntity);
        await _dbContext.SaveChangesAsync();

        response.Item = removedEntity.Entity;
        return response;
    }

    public async Task<ActionResponse<T>> Edit(T entity)
    {
        entity.Updated = DateTime.Now;
        var updatedEntity = _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();

        return new ActionResponse<T>(updatedEntity.Entity);
    }
}