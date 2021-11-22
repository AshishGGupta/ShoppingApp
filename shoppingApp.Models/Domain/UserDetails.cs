namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class UserDetails
    {
        public int Id { get; set; }

        [Required]
        public string TokenUserId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
