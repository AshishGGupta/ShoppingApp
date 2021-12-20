namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.DataAccess.IDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class DbOperations<T> : IDbOperations<T> where T : class
    {
        private readonly ShoppingDbContext _dbContext;
        private DbSet<T> _dbSet;

        public DbOperations(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddItem(T item)
        {
            await _dbSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItem(T item)
        {
            _dbSet.Remove(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetItemById(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetItemList()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<bool> ItemExists(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public async Task UpdateItem(T item)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
