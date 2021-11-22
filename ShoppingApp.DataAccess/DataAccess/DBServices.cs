namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.Common;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DBServices : IDBServices
    {
        private readonly ShoppingDbContext _dbContext;
        private readonly ProductSortAndFilter _sortAndFilter;
        public DBServices(ShoppingDbContext dbContext, ProductSortAndFilter sortAndFilter)
        {
            _dbContext = dbContext;
            _sortAndFilter = sortAndFilter;
        }

        public async Task RegisterUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _dbContext.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task AddProduct(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductList()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetProductList(SortAndFilter sortFilter)
        {
            IQueryable<Product> query = _dbContext.Products;
            if (sortFilter != null)
            {
                query = _sortAndFilter.GetSortAndFilterQuery(query, sortFilter);
            }

            return await query.ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task UpdateProduct(Product product)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ProductExists(int productId)
        {
            return await _dbContext.Products.AnyAsync(x => x.ProductId == productId);
        }

        public async Task<List<Product>> GetProductByListOfId(List<string> productIdList)
        {
            return await _dbContext.Products.Where(x => productIdList.Contains(x.ProductId.ToString())).Distinct().ToListAsync();
        }
    }
}
