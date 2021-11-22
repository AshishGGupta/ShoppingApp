namespace ShoppingApp.Models.Model
{
    using System.ComponentModel.DataAnnotations;

    public class OrderAndPaymentRequest
    {
        [Required]
        public string TokenUserId { get; set; }

        [Required]
        public int UserDetailsId { get; set; }

        [Required]
        public string PaymentType { get; set; }
    }
}
