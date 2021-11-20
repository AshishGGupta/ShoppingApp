namespace ShoppingApp.Validation
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;

    public class ExceptionAttribute : IExceptionFilter
    {
        private readonly ILogger<ExceptionAttribute> _logger;
        public ExceptionAttribute(ILogger<ExceptionAttribute> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context != null)
            {
                _logger.LogError("An Error occured while processing the request. Error:" + context.Exception.Message);
                context.Result = new ObjectResult("Oops!...An Error Occured while performing the operation.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}
