using CM.Services.Identity.API.Infrastracture;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CM.Services.Identity.API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IOptionsSnapshot<AppSettings> _appSettings;

        public HomeController(
            IIdentityServerInteractionService interactionService,
            IOptionsSnapshot<AppSettings> appSettings
            )
        {
            _interactionService = interactionService;
            _appSettings = appSettings;
        }

        public IActionResult Index(string returnUrl)
        {
            return View();
        }

        public IActionResult Settings()
        {
            return Json(_appSettings.Value.GetPublicSettings());
        }

        public async Task<IActionResult> Error(string errorId)
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ViewData["Message"] = (await _interactionService.GetErrorContextAsync(errorId)).ToString();

            return View();
        }
    }
}