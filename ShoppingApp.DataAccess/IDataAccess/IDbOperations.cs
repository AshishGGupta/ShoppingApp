namespace ShoppingApp.DataAccess.IDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IDbOperations<T> where T : class
    {
        Task AddItem(T Item);
        Task<List<T>> GetItemList();
        Task<T> GetItemById(Expression<Func<T, bool>> expression);
        Task DeleteItem(T product);
        Task<bool> ItemExists(Expression<Func<T, bool>> expression);
        Task UpdateItem(T item);
    }
}
