namespace ShoppingApp.Models.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Product
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        [Required(ErrorMessage = "Name cannot be null or empty")]
        [StringLength(50, ErrorMessage = "Length must be under 50")]
        public string Name { get; set; }

        /// <summary>
        /// Product discription
        /// </summary>
        [StringLength(200, ErrorMessage = "Length must be under 200")]
        public string Description { get; set; }

        /// <summary>
        /// Products category
        /// </summary>
        [Required]
        public string Category { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// Date of product added
        /// </summary>
        [Required]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Expiry date of the product
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Cart { get; set; }
    }
}
