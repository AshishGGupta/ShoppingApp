namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserDbServices : IUserDbServices
    {
        private readonly ShoppingDbContext _dbContext;
        public UserDbServices(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserDetails(UserDetails userDetails)
        {
            await _dbContext.UserDetails.AddAsync(userDetails);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDetails> UserItemExists(int userDetailsId, string userId)
        {
            return await _dbContext.UserDetails.FirstOrDefaultAsync(x => x.Id == userDetailsId && x.TokenUserId == userId);
        }

        public async Task DeleteUserDetail(UserDetails userDetails)
        {
            _dbContext.UserDetails.Remove(userDetails);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateUserDetails(UserDetails userDetails)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserDetails>> GetUserDetails(string userId)
        {
            return await _dbContext.UserDetails.Where(x => x.TokenUserId == userId).ToListAsync();
        }
    }
}
