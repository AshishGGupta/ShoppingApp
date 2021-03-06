namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class OrderAndPaymentDbServices : IOrderAndPaymentDBServices
    {
        private readonly ShoppingDbContext _dbContext;

        public OrderAndPaymentDbServices(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAndPaymentDetails(List<OrderAndPayment> orderAndPaymentDetail)
        {
            await _dbContext.OrderAndPayments.AddRangeAsync(orderAndPaymentDetail);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrderAndPayment>> GetOrderAndPaymentDetails(string userId)
        {
            return await _dbContext.OrderAndPayments.Include(x => x.UserDetail).Include(x => x.Product).Where(x => x.TokenUserId == userId).OrderByDescending(x => x.OrderDate).ToListAsync();
        }
    }
}
