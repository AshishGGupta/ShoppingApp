namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class UserDetails
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string TokenUserId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
