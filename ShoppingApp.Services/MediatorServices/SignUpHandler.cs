namespace ShoppingApp.Services.MediatorServices
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ShoppingApp.Common;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Models.MediatorClass;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Services.Services;
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class SignUpHandler : IRequestHandler<Signup, ApiResponse>
    {
        private readonly SignUpDetails signupDetails;
        private readonly IProductDbServices _dbServices;
        private readonly ILogger<LoginUserHandler> _logger;

        public SignUpHandler(IOptions<SignUpDetails> iSignupDetails, IProductDbServices dbServices, ILogger<LoginUserHandler> logger)
        {
            signupDetails = iSignupDetails.Value;
            _dbServices = dbServices;
            _logger = logger;
        }
        public async Task<ApiResponse> Handle(Signup userData, CancellationToken cancellationToken)
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

                if (response.IsSuccessStatusCode)
                {
                    string details = await response.Content.ReadAsStringAsync();
                    AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(details);
                    User user = new User()
                    {
                        UserName = userData.UserName,
                        TokenUserId = Constants.auth0 + authResponse._Id
                    };
                    await _dbServices.RegisterUser(user);
                    apiResponse.IsSuccess = true;
                    apiResponse.Message = "User SignUp is successfull";
                    _logger.LogInformation("User SignUp is successfull. UserName: " + userData.UserName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while processing the request. Ex: " + ex.Message);
                apiResponse.Message = "An Error occured while signup. Error: ";
            }
            return apiResponse;
        }
    }
}
