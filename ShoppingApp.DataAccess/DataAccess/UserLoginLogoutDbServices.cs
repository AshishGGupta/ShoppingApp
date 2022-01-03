namespace ShoppingApp.DataAccess.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Models.MediatorClass;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserLoginLogoutDbServices: IUserLoginLogoutDbServices
    {
        private readonly ShoppingDbContext _dbContext;

        public UserLoginLogoutDbServices(ShoppingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task RegisterUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> UserExists(string userName)
        {
            return await _dbContext.Users.Where(x => x.UserName == userName).Select(x => x.TokenUserId).FirstOrDefaultAsync();
        }

        public async Task<bool> LoginUser(LoginUsersDetails loginUsersDetails)
        {
            //Optional if statement if want new sessionID everytime
            if (!await _dbContext.LoginUsersDetails.AnyAsync(x => x.TokenUserId == loginUsersDetails.TokenUserId))
            {
                await _dbContext.LoginUsersDetails.AddAsync(loginUsersDetails);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> LogoutUser(LogoutUser logoutUser)
        {
            //Optional if statement if want new sessionID everytime
            var loginUserDetail = await _dbContext.LoginUsersDetails.FirstOrDefaultAsync(x => x.TokenUserId == logoutUser.UserToken && x.SessionId == logoutUser.SessionId);
            if (loginUserDetail != null)
            {
                _dbContext.LoginUsersDetails.Remove(loginUserDetail);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SessionExists(string sessionId)
        {
            return await _dbContext.LoginUsersDetails.AnyAsync(x => x.SessionId == sessionId);
        }
    }
}
