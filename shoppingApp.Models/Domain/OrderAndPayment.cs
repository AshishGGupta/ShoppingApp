namespace ShoppingApp.Models.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class OrderAndPayment
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [Key]
        public int OrderId { get; set; }

        [Required]
        public Guid OrderToken { get; set; }
        
        /// <summary>
        /// logged in user's userId
        /// </summary>
        [Required(ErrorMessage = "UserId is required")]
        public string TokenUserId { get; set; }

        /// <summary>
        /// product Id
        /// </summary>
        [Required(ErrorMessage = "ProductId is required")]
        public int ProductId { get; set; }

        /// <summary>
        /// product Id
        /// </summary>
        [Required(ErrorMessage = "ProductQuantity is required")]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// User details Id
        /// </summary>
        [Required(ErrorMessage = "UserDetail Id is required")]
        public int UserDetailsId { get; set; }

        /// <summary>
        /// total quantity of ordered products
        /// </summary>
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        /// <summary>
        /// Payment type selected e.g. COD, net banking, etc
        /// </summary>
        [Required(ErrorMessage = "PaymentType is required")]
        public string PaymentType { get; set; }

        /// <summary>
        /// Date of order placed
        /// </summary>
        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        [JsonIgnore]
        public UserDetails UserDetail { get; set; }

    }
}
