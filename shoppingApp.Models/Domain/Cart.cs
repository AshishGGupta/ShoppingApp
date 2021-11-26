namespace ShoppingApp.Models.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class Cart
    {
        /// <summary>
        /// Cart Id primary key
        /// </summary>
        public int CartId { get; set; }

        /// <summary>
        /// Logged in user's User Id
        /// </summary>
        [Required(ErrorMessage = "UserId is required.")]
        public string TokenUserId { get; set; }

        /// <summary>
        /// product ID
        /// </summary>
        [Required(ErrorMessage = "productId is required")]
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity of the product to add in cart
        /// </summary>
        [Required(ErrorMessage = "Quantity is required")]
        public string Quantity{ get; set; }

        /// <summary>
        /// Products model
        /// </summary>
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
