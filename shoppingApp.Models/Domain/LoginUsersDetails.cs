namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class LoginUsersDetails
    {
        /// <summary>
        /// Login Id
        /// </summary>
        [Key]
        public int LoginId { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        [Required(ErrorMessage = "TokenUserId cannot be null or empty")]
        [ForeignKey("User")]
        public string TokenUserId { get; set; }

        /// <summary>
        /// Session Id
        /// </summary>
        [Required(ErrorMessage = "SessionId cannot be null or empty")]
        public string SessionId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
