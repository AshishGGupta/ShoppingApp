namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class UserDetails
    {
        /// <summary>
        /// User details Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Logged in user's userId
        /// </summary>
        [Required(ErrorMessage = "UserId is required")]
        public string TokenUserId { get; set; }

        /// <summary>
        /// Delivery Address 
        /// </summary>
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        /// <summary>
        /// Contact number
        /// </summary>
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
