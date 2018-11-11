using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Linq;

namespace CM.Services.Identity.API.Infrastracture
{
    public class AppConfig
    {
        private readonly AppSettings _appSettings;

        public AppConfig(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public IEnumerable<ApiResource> GetApis()
        {
            return _appSettings.Local.Identity.Apis.Select(client => new ApiResource(client.Key, client.Value));
        }

        public IEnumerable<Client> GetClients()
        {
            var js = _appSettings.Local.Identity.Clients["js"];
            var wallet = _appSettings.Local.Identity.Clients["wallet"];
            var walletviews = _appSettings.Local.Identity.Clients["walletviews"];
            var exchange = _appSettings.Local.Identity.Clients["exchange"];

            return new List<Client>
            {
                new Client
                {
                    ClientId = js.Id,
                    ClientName = js.Name,
                    RedirectUris = js.RedirectUris.Select(uri => $"{_appSettings.Global.Web.HttpsUrl}{uri}").ToList(),
                    PostLogoutRedirectUris = new List<string> {$"{_appSettings.Global.Web.HttpsUrl}/" },
                    AllowedCorsOrigins = new List<string> {$"{_appSettings.Global.Web.HttpsUrl}" },
                    AllowedScopes = new List<string>() {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }.Concat(js.Scopes).ToList(),
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = true,
                },
                new Client
                {
                    ClientId = wallet.Id,
                    ClientName = wallet.Name,
                    ClientSecrets = { new Secret(wallet.Secret.Sha256()) },
                    RedirectUris = wallet.RedirectUris.Select(uri => $"{_appSettings.Global.Wallet.HttpsUrl}{uri}").ToList(),
                    PostLogoutRedirectUris = { $"{_appSettings.Global.Wallet.HttpsUrl}/swagger/" },
                    AllowedScopes = wallet.Scopes,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = walletviews.Id,
                    ClientName = walletviews.Name,
                    ClientSecrets = { new Secret(walletviews.Secret.Sha256()) },
                    RedirectUris = walletviews.RedirectUris.Select(uri => $"{_appSettings.Global.WalletViews.HttpsUrl}{uri}").ToList(),
                    PostLogoutRedirectUris = { $"{_appSettings.Global.WalletViews.HttpsUrl}/swagger/" },
                    AllowedScopes = walletviews.Scopes,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                },
                new Client
                {
                    ClientId = exchange.Id,
                    ClientName = exchange.Name,
                    ClientSecrets = { new Secret(exchange.Secret.Sha256()) },
                    RedirectUris = exchange.RedirectUris.Select(uri => $"{_appSettings.Global.Exchange.HttpsUrl}{uri}").ToList(),
                    PostLogoutRedirectUris = { $"{_appSettings.Global.Exchange.HttpsUrl}/swagger/" },
                    AllowedScopes = exchange.Scopes,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                }
            };
        }
    }
}