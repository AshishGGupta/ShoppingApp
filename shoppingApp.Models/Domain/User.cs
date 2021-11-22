namespace ShoppingApp.Models.Domain
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class User
    {
        [Key]
        public string TokenUserId { get; set; }

        public string UserName { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Cart { get; set; }
        
        [JsonIgnore]
        public ICollection<UserDetails> UserDetails { get; set; }
    }
}
