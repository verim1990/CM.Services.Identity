using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.Password.Application.Commands
{
    public class ForgetUserPasswordCommand : IRequest<ForgetUserPasswordResponse>
    {
        public ApplicationUser User { get; set; }
    }
}