namespace ShoppingApp.Services.MediatorServices
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.MediatorClass;
    using System.Threading;
    using System.Threading.Tasks;

    public class LogoutHandler : IRequestHandler<LogoutUser, bool>
    {
        private readonly IUserLoginLogoutDbServices _dbServices;
        private readonly ILogger<LogoutHandler> _logger;

        public LogoutHandler(IUserLoginLogoutDbServices dbServices, ILogger<LogoutHandler> logger)
        {
            _dbServices = dbServices;
            _logger = logger;
        }
        public async Task<bool> Handle(LogoutUser request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UserToken) && !string.IsNullOrEmpty(request.SessionId))
            {
                return await _dbServices.LogoutUser(request);
            }
            _logger.LogInformation("UserToken or SessionId is invalid");
            return false;
        }
    }
}
