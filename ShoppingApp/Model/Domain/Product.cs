namespace ShoppingApp.Model.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    public class Product
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name cannot be null or empty")]
        [StringLength(50, ErrorMessage = "Length must be under 50")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Length must be under 200")]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Cart { get; set; }
    }
}
