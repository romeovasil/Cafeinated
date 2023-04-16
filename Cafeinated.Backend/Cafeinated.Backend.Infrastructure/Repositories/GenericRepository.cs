using System.Linq.Expressions;
using Cafeinated.Backend.Core.Entities.Abstractions;
using Cafeinated.Backend.Infrastructure.Repositories.Abstractions;
using Cafeinated.Backend.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;

namespace Cafeinated.Backend.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly DbContext _dbContext;

    public GenericRepository(DbSet<T> dbSet, DbContext dbContext)
    {
        _dbSet = dbSet;
        _dbContext = dbContext;
    }

    public async Task<ActionResponse<IEnumerable<T>>> GetAll()
    {
        var entities = await _dbSet.ToListAsync();
        return new ActionResponse<IEnumerable<T>>(entities);
    }

    public async Task<ActionResponse<IEnumerable<T>>> FindBy(Expression<Func<T, bool>> predicate)
    {
        var filteredEntities = await _dbSet.Where(predicate).ToListAsync();
        return new ActionResponse<IEnumerable<T>>(filteredEntities);
    }

    public async Task<ActionResponse<T>> GetEntityBy(Expression<Func<T, bool>> predicate)
    {
        var response = new ActionResponse<T>();
        
        var requestedEntity = await _dbSet.FirstOrDefaultAsync(predicate);
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

        var addedEntity = await _dbContext.AddAsync(entity);
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

        var removedEntity = _dbContext.Remove(existingEntity);
        await _dbContext.SaveChangesAsync();

        response.Item = removedEntity.Entity;
        return response;
    }

    public async Task<ActionResponse<T>> Edit(T entity)
    {
        entity.Updated = DateTime.Now;
        var updatedEntity = _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();

        return new ActionResponse<T>(updatedEntity.Entity);
    }
}