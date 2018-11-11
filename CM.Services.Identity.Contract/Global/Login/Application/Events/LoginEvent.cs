using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Login.Application.Events
{
    public class LoginEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}