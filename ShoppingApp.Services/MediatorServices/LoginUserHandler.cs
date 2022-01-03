namespace ShoppingApp.Services.MediatorServices
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Models.MediatorClass;
    using ShoppingApp.Models.Model;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class LoginUserHandler : IRequestHandler<LoginUser, ApiResponse>
    {
        private readonly LoginDetails loginDetails;
        private readonly IUserLoginLogoutDbServices _dbServices;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(IOptions<LoginDetails> iLoginDetails, IUserLoginLogoutDbServices dbServices, ILogger<LoginUserHandler> logger)
        {
            loginDetails = iLoginDetails.Value;
            _dbServices = dbServices;
            _logger = logger;
        }
        public async Task<ApiResponse> Handle(LoginUser userData, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse()
            {
                IsSuccess = false,
                Message = "Login unsuccessfull! Please try again."
            };
            try
            {
                string userToken = await _dbServices.UserExists(userData.UserName);
                if (string.IsNullOrEmpty(userToken))
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
                    
                    //Give a sessionId
                    //apiResponse.SessionId = Guid.NewGuid().ToString();
                    apiResponse.SessionId = "6abd369d-51f7-41a1-aa43-775d9cc1773e";
                    var loginDetails = new LoginUsersDetails()
                    {
                        TokenUserId = userToken,
                        SessionId = apiResponse.SessionId
                    };
                    await _dbServices.LoginUser(loginDetails);

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
    }
}
