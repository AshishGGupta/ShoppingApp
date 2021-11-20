namespace ShoppingApp.Services
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShoppingApp.Common;
    using ShoppingApp.IDataAccess;
    using ShoppingApp.Interfaces;
    using ShoppingApp.Model;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class UserServices : IUserServices
    {
        private readonly SignUpDetails signupDetails;
        private readonly LoginDetails loginDetails;
        private readonly IDBServices _dbServices;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IOptions<SignUpDetails> iSignupDetails, IOptions<LoginDetails> iLoginDetails, IDBServices dbServices, ILogger<UserServices> logger)
        {
            signupDetails = iSignupDetails.Value;
            loginDetails = iLoginDetails.Value;
            _dbServices = dbServices;
            _logger = logger;
        }

        public async Task<ApiResponse> UserSignUp(UserModel userData)
        {
            ApiResponse apiResponse = new ApiResponse()
            {
                IsSuccess = false,
                Message = "Signup unsuccessfull! Please try again."
            };
            try
            {
                if (await _dbServices.UserExists(userData.UserName))
                {
                    _logger.LogInformation("User already registered. userId: " + userData.UserName);
                    apiResponse.Message = "User already registered";
                    return apiResponse;
                }
                signupDetails.email = userData.UserName;
                signupDetails.password = userData.Password;
                var stringContent = new StringContent(JsonConvert.SerializeObject(signupDetails), UnicodeEncoding.UTF8, "application/json");
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(signupDetails.SignupURL);
                var response = await httpClient.PostAsync("", stringContent);

                if(response.IsSuccessStatusCode)
                {
                    string details = await response.Content.ReadAsStringAsync();
                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(details);
                    await _dbServices.RegisterUser(MapUser(userData.UserName, authResponse._Id));
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "User SignUp is successfull";
                    _logger.LogInformation("User SignUp is successfull. UserName: " + userData.UserName);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("An error occured while processing the request. Ex: " + ex.Message);
                apiResponse.Message = "An Error occured while signup. Error: ";
            }
            return apiResponse;
        }

        public async Task<ApiResponse> UserLogin(UserModel userData)
        {
            ApiResponse apiResponse = new ApiResponse()
            {
                IsSuccess = false,
                Message = "Login unsuccessfull! Please try again."
            };
            try
            {
                if (! await _dbServices.UserExists(userData.UserName))
                {
                    _logger.LogInformation("User does not exist. userId: " + userData.UserName);
                    apiResponse.Message = "User does not exist";
                    return apiResponse;
                }
                loginDetails.username = userData.UserName;
                loginDetails.password = userData.Password;
                var stringContent = new StringContent(JsonConvert.SerializeObject(loginDetails), UnicodeEncoding.UTF8, "application/json");
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(loginDetails.LoginURL);
                var response = await httpClient.PostAsync("", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    string details = await response.Content.ReadAsStringAsync();
                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(details);
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "User Login is successfull";
                    apiResponse.Token = authResponse.Access_token;
                    _logger.LogInformation("User Login is successfull. UserName: " + userData.UserName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while processing the request. Ex: " + ex.Message);
                apiResponse.Message = "An Error occured while Login.";
            }
            return apiResponse;
        }

        public User MapUser(string email, string token)
        {
            return new User()
            {
                UserName = email,
                TokenUserId = Constants.auth0 + token
            };
        }
    }
}
