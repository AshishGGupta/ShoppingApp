namespace ShoppingApp.Models.Model
{
    using ShoppingApp.Models.Domain;

    public class ProductQuantity
    {
        public int CartId { get; set; }

        public Product Product { get; set; }
        
        public int Quantity { get; set; }
        
        public bool ProductExpired { get; set; }
    }
}
