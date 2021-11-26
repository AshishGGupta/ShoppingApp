namespace ShoppingApp.Models.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        public string TokenUserId { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Cart { get; set; }
        
        [JsonIgnore]
        public ICollection<UserDetails> UserDetails { get; set; }
    }
}
