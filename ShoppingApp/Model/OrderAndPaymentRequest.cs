using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Model
{
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
