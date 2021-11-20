namespace ShoppingApp.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.IDataAccess;
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartDbServices : ICartDbService
    {
        private readonly ShoppingDbContext _dbContext;
        public CartDbServices(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddToCart(Cart cart)
        {
            await _dbContext.Cart.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Edit(Cart cart)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartDetails(string userId)
        {
            return await _dbContext.Cart.Include(x => x.Product).Where(x => x.TokenUserId == userId).ToListAsync();
        }

        public async Task Delete(Cart cart)
        {
            _dbContext.Cart.Remove(cart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Cart> CartItemExists(int cartId)
        {
            return await _dbContext.Cart.FirstOrDefaultAsync(x => x.CartId == cartId);
        }

        public async Task<Cart> CartItemExistsByProductId(int productId)
        {
            return await _dbContext.Cart.FirstOrDefaultAsync(x => x.ProductId == productId);
        }
    }
}
