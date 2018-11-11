using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Authentication.Domain.Services
{
    public class AuthMessageSender : IEmailService, ISmsService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}
