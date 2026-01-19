using System.ComponentModel.DataAnnotations;

namespace lab9.Models
{
    public class OrderViewModel
    {
        public List<CartItem> CartItems { get; set; } = new();
        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres dostawy jest wymagany")]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PaymentMethod { get; set; } = "Karta";

        public List<string> PaymentOptions { get; set; } = new() { "Karta", "Przelew", "Blik", "Przy odbiorze" };
    }
}