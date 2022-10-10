using MVCStoreData;
using System.ComponentModel.DataAnnotations;

namespace MVCStoreeWeb.Models
{
    public class RegisterViewModel
    {

        [Display(Name = "E posta")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="{0} alanı boş bırakılamaz")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir eposta adresi giriniz!")]
        public string UserName { get; set; }





        [Display(Name = "Ad Soyad")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string Name { get; set; }

        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]        
        public string Password{ get; set; }

        [Display(Name = "Parola Tekrarı")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Compare("Password", ErrorMessage = "{0} alanı ile {1} alanı aynı olmalıdır!")]
        public string PasswordVerify { get; set; }

        [Display(Name = "Cinsiyet")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public Genders Gender { get; set; }

        [Display(Name = "Doğum T.")]
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public DateTime BirthDate { get; set; }


        [Display(Name = "Oturum Açık Kalsın")] 
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
