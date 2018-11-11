using CM.Services.Identity.Contract.User.Email.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.Email.Application.Commands
{
    public class SetUserEmailCommand : IRequest<SetUserEmailResponse>
    {
        public string NewEmail { get; set; }

        public ApplicationUser User { get; set; }
    }
}