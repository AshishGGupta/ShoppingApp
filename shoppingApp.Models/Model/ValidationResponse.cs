namespace ShoppingApp.Models.Model
{
    public class ValidationResponse
    {
        public int StatusCode { get; set;}
        public string Message { get; set;}
        public ValidationResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
