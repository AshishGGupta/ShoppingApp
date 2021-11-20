namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Interfaces;
    using ShoppingApp.Model.Domain;
    using ShoppingApp.Validation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    [ApiController]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUserDetailServices _userDetailServices;
        private readonly ILogger<UserDetailsController> _logger;

        public UserDetailsController(IUserDetailServices userDetailServices, ILogger<UserDetailsController> logger)
        {
            _userDetailServices = userDetailServices;
            _logger = logger;
        }

        [HttpGet("UserDetails/GetUserDetails/{userId}")]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            _logger.LogInformation("Entering GetUserDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }

            List<UserDetails> userDetials = await _userDetailServices.GetUserDetails(userId);
            _logger.LogInformation("UserDetalis found: count: " + userDetials?.Count);
            if(userDetials?.Count > 0)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Ok(userDetials);
            }
            return NotFound("UserDetails not found");
        }

        [HttpPost("UserDetails/AddUserDetails")]
        public async Task<IActionResult> AddUserDetails([FromBody] UserDetails userDetails)
        {
            _logger.LogInformation("Entering AddUserDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userDetails.TokenUserId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }

            await _userDetailServices.AddUserDetail(userDetails);
            _logger.LogInformation("User deatils added successfully");
            return Ok("User details added successfully");
        }

        // PUT api/<UserDetailsController>/5
        [HttpPut("UserDetails/UpdateUserDetails")]
        public async Task<IActionResult> UpdateUserDetails([FromBody] UserDetails userDetails)
        {
            _logger.LogInformation("Entering UpdateUserDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userDetails.TokenUserId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            
            bool isSuccess = await _userDetailServices.EditUserDetail(userDetails);
            if (isSuccess)
            {
                return Ok("User details updated successfully");
            }
            return NotFound("User Details not found");
        }

        [HttpDelete("UserDetails/DeleteUserDetails/{userDetailsId}/{userId}")]
        public async Task<IActionResult> DeleteUserDetails(int userDetailsId, string userId)
        {
            _logger.LogInformation("Entering DeleteUserDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }

            bool isSuccess = await _userDetailServices.DeleteUserDetail(userDetailsId, userId);
            if (isSuccess)
            {
                return Ok("User details deleted successfully");
            }
            return NotFound("User Details not found");
        }
    }
}
