
using CM.Services.Identity.Contract.User.Phone.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.Phone.Application.Commands
{
    public class SetUserPhoneCommand : IRequest<SetUserPhoneResponse>
    {
        public string NewPhone { get; set; }

        public ApplicationUser User { get; set; }
    }
}