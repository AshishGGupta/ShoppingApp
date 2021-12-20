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

    public class ProductDbServices : DbOperations<Product>, IProductDbServices
    {
        private readonly ShoppingDbContext _dbContext;
        private readonly ProductSortAndFilter _sortAndFilter;

        public ProductDbServices(ShoppingDbContext dbContext, ProductSortAndFilter sortAndFilter) : base(dbContext)
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

        //Products

        public async Task<List<Product>> GetProductList(SortAndFilter sortFilter)
        {
            IQueryable<Product> query = _dbContext.Products;
            if (sortFilter != null)
            {
                query = _sortAndFilter.GetSortAndFilterQuery(query, sortFilter);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetProductByListOfId(List<string> productIdList)
        {
            return await _dbContext.Products.Where(x => productIdList.Contains(x.ProductId.ToString())).Distinct().ToListAsync();
        }
    }
}
