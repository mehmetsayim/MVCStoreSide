using System.ComponentModel.DataAnnotations;

namespace MVCStoreeWeb.Models
{
    public class LoginViewModel
    {

        [Display(Name = "Eposta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="{0} alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir eposta adresi giriniz!")]
        public string UserName { get; set; }



        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]        
        public string Password{ get; set; }
        [Display(Name = "Oturum Açık Kalsın")]
        
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
