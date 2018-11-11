using System.ComponentModel.DataAnnotations;

namespace CM.Services.Identity.Contract.User.Password.Presentation.ViewModels
{
    public class ForgotUserPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
