using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CM.Services.Identity.Contract.Global.Authentication.Domain.Models
{
    public class ApplicationUser : IdentityUser<string>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}