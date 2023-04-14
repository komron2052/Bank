using Bank.Data.DbContexts;
using Bank.Data.IRepositories;
using Bank.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bank.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly BankDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;  
    public Repository(BankDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var entity = await dbSet.FirstOrDefaultAsync(predicate);
        if (entity == null)
            return false;
        dbSet.Remove(entity);
        return true;    
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        var entry = await dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public async Task<bool> SaveAsync() =>
        await dbContext.SaveChangesAsync() > 0;

    public IQueryable<TEntity> SelectAllAsync() =>
        dbSet;

    public Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate) =>
        dbSet.FirstOrDefaultAsync(predicate);
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = dbSet.Update(entity);
        return entry.Entity;
    }
}
