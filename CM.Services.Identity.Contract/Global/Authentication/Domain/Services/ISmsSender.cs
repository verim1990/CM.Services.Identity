using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.Global.Authentication.Domain.Services
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
