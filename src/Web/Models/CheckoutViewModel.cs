using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CheckoutViewModel
    {
        public BasketViewModel? Basket { get; set; } = null!;

        [Required, MaxLength(180)]
        [Display(Name = "Street")]
        public string? Street { get; set; }

        [Required, MaxLength(100)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [MaxLength(60)]
        [Display(Name = "State")]
        public string? State { get; set; }

        [Required, MaxLength(90)]
        [Display(Name = "Country")]
        public string? Country { get; set; }

        [Required, MaxLength(18)]
        [Display(Name = "Zip")]
        public string? ZipCode { get; set; }

        [Required]
        [Display(Name = "Name on card")]
        public string? CCHolder { get; set; }

        [Required, CreditCard]
        [Display(Name = "Credit card number")]
        public string? CCNumber { get; set; }

        [Required, RegularExpression(@"^[0-9]{2}\/[0-9]{2}$", ErrorMessage = "Invalid {0}.")]
        [Display(Name = "Expiration")]
        public string? CCExpiration { get; set; }

        [Required, RegularExpression("^[0-9]{3}$", ErrorMessage = "Invalid {0}.")]
        [Display(Name = "CVV")]
        public string? CCCvv { get; set; }
    }
}
