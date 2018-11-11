using CM.Services.Identity.Contract.Global.Register.Domain.Models;
using System.Threading.Tasks;

namespace CM.Services.Identity.Contract.Global.Register.Domain.Services
{
    public interface IRegisterService
    {
        Task<RegisterResult> Register(string username, string email, string password);
    }
}