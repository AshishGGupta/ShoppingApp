namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartDbServices : DbOperations<Cart>, ICartDbService
    {
        private readonly ShoppingDbContext _dbContext;
        public CartDbServices(ShoppingDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cart>> GetCartDetails(string userId)
        {
            return await _dbContext.Cart.Include(x => x.Product).Where(x => x.TokenUserId == userId).ToListAsync();
        }

        public async Task<Cart> CartItemExistsByProductId(int productId, string userId)
        {
            return await _dbContext.Cart.FirstOrDefaultAsync(x => x.ProductId == productId && x.TokenUserId == userId);
        }

        public async Task BulkCartDelete(List<Cart> cart)
        {
            _dbContext.Cart.RemoveRange(cart);
            await _dbContext.SaveChangesAsync();
        }
    }
}
