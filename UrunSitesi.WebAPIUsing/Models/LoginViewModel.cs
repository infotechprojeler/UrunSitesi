using System.ComponentModel.DataAnnotations;

namespace UrunSitesi.WebAPIUsing.Models
{
    public class LoginViewModel
    {
        [StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!"), EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(50), Required(ErrorMessage = "{0} Alanı Zorunludur!"), DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
