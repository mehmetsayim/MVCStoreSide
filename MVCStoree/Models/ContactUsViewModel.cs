using System.ComponentModel.DataAnnotations;

namespace MVCStoreeWeb.Models
{
    public class ContactUsViewModel
    {
        [Display(Name = "Ad/Soyad ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
           
        public string Name { get; set; }

        [Display(Name = "Eposta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir e-posta adresi yazınız!")]
        public string Email { get; set; }

        [Display(Name = "İrtibat No.")]
        [DataType(DataType.PhoneNumber)]
        
        public string? PhonoNumber { get; set; }
        [Display(Name = "Mesaj")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Message { get; set; }
    }
}
