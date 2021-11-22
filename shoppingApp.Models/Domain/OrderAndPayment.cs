namespace ShoppingApp.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class OrderAndPayment
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required]
        public string TokenUserId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public int UserDetailsId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string PaymentType { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [JsonIgnore]
        public UserDetails UserDetail { get; set; }

    }
}
