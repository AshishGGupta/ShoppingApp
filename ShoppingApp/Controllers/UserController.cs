namespace ShoppingApp.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Models.MediatorClass;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Services.Validation;
    using System.Threading.Tasks;

    [ServiceFilter(typeof(UserValidationAttribute))]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// User sign up
        /// </summary>
        /// <param name="userData">User credentials</param>
        /// <returns>api response success status</returns>
        /// <response code="200">User Signed up successfully</response>
        /// <response code="400">Some input value is not valid</response>
        [HttpPost("User/SignUp")]
        public async Task<IActionResult> SignUp([FromBody] Signup userData)
        {
            _logger.LogInformation("Entering SignUp method");
            ApiResponse response = await _mediator.Send(userData);
            return Ok(response);
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userData">User Credentials</param>
        /// <returns>api response success status</returns>
        /// <response code="200">User Logged in successfully</response>
        /// <response code="400">Some input value is not valid</response>
        [HttpPost("User/Login")]
        public async Task<IActionResult> Login([FromBody] LoginUser userData)
        {
            _logger.LogInformation("Entering Login method");
            var response = await _mediator.Send(userData);
            return Ok(response);
        }
    }
}
