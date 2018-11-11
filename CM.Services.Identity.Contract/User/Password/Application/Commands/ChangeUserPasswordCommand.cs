using CM.Services.Identity.Contract.User.Password.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.User.Password.Application.Commands
{
    public class ChangeUserPasswordCommand : IRequest<ChangeUserPasswordResponse>
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public ApplicationUser User { get; set; }
    }
}