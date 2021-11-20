namespace ShoppingApp.Validation
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Model;

    public class UserValidationAttribute : ActionFilterAttribute
    {
        private readonly ILogger<UserValidationAttribute> _logger;
        public UserValidationAttribute(ILogger<UserValidationAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context?.ActionArguments != null)
            {
                foreach (var parameter in context.ActionArguments)
                {
                    UserModel user = (UserModel)parameter.Value;
                    if (string.IsNullOrEmpty(user.UserName))
                    {
                        _logger.LogInformation("UserName cannot be null or empty");
                        context.Result = new BadRequestObjectResult(new ValidationResponse(StatusCodes.Status400BadRequest, "UserName cannot be null or empty"));
                        break;
                    }
                    else if (string.IsNullOrEmpty(user.Password))
                    {
                        _logger.LogInformation("Password cannot be null or empty");
                        context.Result = new BadRequestObjectResult(new ValidationResponse(StatusCodes.Status400BadRequest, parameter.Value + "Password cannot be null or empty"));
                        break;
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
