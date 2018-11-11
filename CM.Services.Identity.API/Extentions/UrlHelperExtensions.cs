using Microsoft.AspNetCore.Mvc;

namespace CM.Services.Identity.API.Extentions
{
    public static class UrlHelperExtensions
    {
        public static string HomeLink(this IUrlHelper urlHelper)
        {
            return urlHelper.Action("index", "home", new { area = "" });
        }

        public static string LoginLink(this IUrlHelper urlHelper, string returnUrl = null)
        {
            return urlHelper.Action("login", "login", values: new { area = "public", returnUrl });
        }

        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string token, string scheme)
        {
            return urlHelper.Action("confirm", "email", values: new { area = "public", userId, token }, protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string token, string scheme)
        {
            return urlHelper.Action("reset", "password", values: new { area = "public", userId, token }, protocol: scheme);
        }
    }
}
