namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Interfaces;
    using ShoppingApp.Model;
    using ShoppingApp.Validation;
    using System.Threading.Tasks;

    [ServiceFilter(typeof(UserValidationAttribute))]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserServices userServices, ILogger<UserController> logger)
        {
            _userServices = userServices;
            _logger = logger;
        }

        [HttpPost("User/SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserModel userData)
        {
            _logger.LogInformation("Entering SignUp method");
            var response = await _userServices.UserSignUp(userData);
            return Ok(response);
        }

        [HttpPost("User/Login")]
        public async Task<IActionResult> Login([FromBody] UserModel userData)
        {
            _logger.LogInformation("Entering Login method");
            var response = await _userServices.UserLogin(userData);
            return Ok(response);
        }
    }
}
