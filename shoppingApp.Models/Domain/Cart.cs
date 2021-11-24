namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class Cart
    {
        public int CartId { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        public string TokenUserId { get; set; }

        [Required(ErrorMessage = "productId is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public string Quantity{ get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
