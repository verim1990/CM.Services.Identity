using System.ComponentModel.DataAnnotations;

namespace CM.Services.Identity.Contract.Global.Login.Presentation.ViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}
