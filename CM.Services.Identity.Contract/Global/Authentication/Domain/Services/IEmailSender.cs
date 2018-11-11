using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.Global.Authentication.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
