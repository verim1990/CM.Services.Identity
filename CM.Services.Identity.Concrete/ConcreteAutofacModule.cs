using Autofac;
using CM.Services.Identity.Concrete.Authentication.Domain.Services;
using CM.Services.Identity.Concrete.Global.Authentication.Domain.Services;
using CM.Services.Identity.Concrete.Global.Login.Domain.Services;
using CM.Services.Identity.Concrete.Global.Register.Domain.Services;
using CM.Services.Identity.Concrete.User.Email.Domain.Services;
using CM.Services.Identity.Concrete.User.ExternalLogin.Domain.Services;
using CM.Services.Identity.Concrete.User.Phone.Domain.Services;
using CM.Services.Identity.Concrete.User.TwoFactorAuthentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Authentication.Domain.Services;
using CM.Services.Identity.Contract.Global.Login.Domain.Services;
using CM.Services.Identity.Contract.Global.Register.Domain.Services;
using CM.Services.Identity.Contract.User.Email.Domain.Services;
using CM.Services.Identity.Contract.User.ExternalLogin.Domain.Services;
using CM.Services.Identity.Contract.User.Password.Domain.Services;
using CM.Services.Identity.Contract.User.Phone.Domain.Services;
using CM.Services.Identity.Contract.User.TwoFactorAuthentication.Domain.Services;
using Module = Autofac.Module;

namespace CM.Services.Identity.Concrete
{
    public class ConcreteAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<IdentityService>()
                .As<IIdentityService>().InstancePerLifetimeScope();
            builder.RegisterType<LoginService>()
                .As<ILoginService>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterService>()
                .As<IRegisterService>().InstancePerLifetimeScope();
            builder.RegisterType<UserEmailService>()
                .As<IUserEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<UserPasswordService>()
                .As<IUserPasswordService>().InstancePerLifetimeScope();
            builder.RegisterType<PhoneService>()
                .As<IPhoneService>().InstancePerLifetimeScope();
            builder.RegisterType<TwoFactorAuthenticationService>()
                .As<ITwoFactorAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<ExternalLoginService>()
                .As<IExternalLoginService>().InstancePerLifetimeScope();
            builder.RegisterType<UserExternalLoginService>()
                .As<IUserExternalLoginService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthMessageSender>()
                .As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthMessageSender>()
                .As<ISmsService>().InstancePerLifetimeScope();
        }
    }
}