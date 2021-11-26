namespace ShoppingApp.Models.Model
{
    using System.ComponentModel.DataAnnotations;

    public class OrderAndPaymentRequest
    {
        /// <summary>
        /// logged in user's userId
        /// </summary>
        [Required]
        public string TokenUserId { get; set; }

        /// <summary>
        /// User detials ID
        /// </summary>
        [Required]
        public int UserDetailsId { get; set; }

        /// <summary>
        /// Payment type selected
        /// </summary>
        [Required]
        public string PaymentType { get; set; }
    }
}
