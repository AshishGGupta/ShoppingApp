namespace ShoppingApp.Services.Validation
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Model;

    public class UserLoginValidator : ActionFilterAttribute
    {
        private readonly ILogger<UserLoginValidator> _logger;
        private readonly IUserLoginLogoutDbServices _dbServices;

        public UserLoginValidator(ILogger<UserLoginValidator> logger, IUserLoginLogoutDbServices dbServices)
        {
            _logger = logger;
            _dbServices = dbServices;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context?.ActionArguments != null)
            {
                var sessionId = context.HttpContext.Request.Headers["SessionId"];
                if(!string.IsNullOrEmpty(sessionId))
                {
                    if(!_dbServices.SessionExists(sessionId).Result)
                    {
                        _logger.LogInformation("User should login first");
                        context.Result = new UnauthorizedObjectResult(new ValidationResponse(StatusCodes.Status401Unauthorized, "User is not logged in."));
                    }
                }
                else
                {
                    _logger.LogInformation("SessionId is null/empty");
                    context.Result = new UnauthorizedObjectResult(new ValidationResponse(StatusCodes.Status401Unauthorized, "User is not logged in."));
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
