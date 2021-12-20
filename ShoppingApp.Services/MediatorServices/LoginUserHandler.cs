namespace ShoppingApp.Services.MediatorServices
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShoppingApp.DataAccess.IDataAccess;
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
        private readonly IProductDbServices _dbServices;
        private readonly ILogger<LoginUserHandler> _logger;

        public LoginUserHandler(IOptions<LoginDetails> iLoginDetails, IProductDbServices dbServices, ILogger<LoginUserHandler> logger)
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
                if (!await _dbServices.UserExists(userData.UserName))
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
    }
}
