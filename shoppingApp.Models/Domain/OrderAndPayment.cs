namespace ShoppingApp.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class OrderAndPayment
    {
        [Key]
        public int OrderId { get; set; }
        
        [Required(ErrorMessage = "UserId is required")]
        public string TokenUserId { get; set; }

        [Required(ErrorMessage = "ProductId is required")]
        public string ProductId { get; set; }

        [Required(ErrorMessage = "UserDetail Id is required")]
        public int UserDetailsId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "PaymentType is required")]
        public string PaymentType { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [JsonIgnore]
        public UserDetails UserDetail { get; set; }

    }
}
