namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class Cart
    {
        public int CartId { get; set; }

        [Required]
        public string TokenUserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Quantity{ get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
    }
}
