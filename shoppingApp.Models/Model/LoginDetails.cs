namespace ShoppingApp.Models.Model
{
    public class LoginDetails
    {
        public string Audience { get; set; }

        public string LoginURL { get; set; }

        public string grant_type { get; set; }

        public string client_id { get; set; }

        public string client_secret { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }
}
