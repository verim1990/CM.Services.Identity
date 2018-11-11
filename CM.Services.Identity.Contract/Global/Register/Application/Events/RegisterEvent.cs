using CM.Services.Identity.Contract.Global.Authentication.Domain.Models;
using MediatR;

namespace CM.Services.Identity.Contract.Global.Register.Application.Events
{
    public class RegisterEvent : INotification
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}