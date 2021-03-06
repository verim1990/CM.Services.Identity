﻿using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Application.Commands;
using CM.Services.Identity.Contract.Global.Login.Application.Commands.Responses;
using CM.Services.Identity.Contract.Global.Login.Application.Events;
using CM.Services.Identity.Contract.Global.Login.Domain.Models;
using CM.Services.Identity.Contract.Global.Login.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace CM.Services.Identity.Concrete.Global.Login.Application.CommandHandlers
{
    public class LoginRecoveryCommandHandler : IRequestHandler<LoginRecoveryCommand, LoginResponse>
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;
        private readonly ILoginService _loginService;

        public LoginRecoveryCommandHandler(
            ILoginService loginService,
            IIdentityService identityService,
            IMediator mediator)
        {
            _loginService = loginService;
            _identityService = identityService;
            _mediator = mediator;
        }

        public async Task<LoginResponse> Handle(LoginRecoveryCommand command, CancellationToken token)
        {
            var user = await _identityService.GetTwoFactorAuthenticationUser();

            if (user == null)
            {
                return new LoginResponse()
                {
                    LoginResult = new LoginResult()
                    {
                        SignInResult = SignInResult.Failed
                    }
                };
            }

            var signInResult = await _loginService.LoginWithRecoveryCode(command.RecoveryCode);

            if (signInResult.Succeeded)
                await _mediator.Publish(new LoginEvent());

            return new LoginResponse()
            {
                LoginResult = new LoginResult()
                {
                    SignInResult = signInResult
                }
            };
        }
    }
}