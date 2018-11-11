using CM.Shared.Kernel.Application.Settings;
using CM.Shared.Kernel.Others.Sql;
using System.Collections.Generic;

namespace CM.Services.Identity.API
{
    public class AppSettings
    {
        public GlobalSettings Global { get; set; } = new GlobalSettings();

        public LocalSettings Local { get; set; } = new LocalSettings();

        public object GetPublicSettings()
        {
            return new
            {
                ServiceName = Global.Identity.Name
            };
        }
    }

    public class GlobalSettings
    {
        public SqlSettings Sql { get; set; } = new SqlSettings();

        public ServiceSettings Web { get; set; } = new ServiceSettings();

        public ServiceSettings Identity { get; set; } = new ServiceSettings();

        public ServiceSettings Exchange { get; set; } = new ServiceSettings();

        public ServiceSettings Wallet { get; set; } = new ServiceSettings();

        public ServiceSettings WalletViews { get; set; } = new ServiceSettings();
    }

    public class LocalSettings
    {
        public Identity Identity { get; set; }

        public SqlContextSettings Sql { get; set; }
    }

    public class Identity
    {
        public Dictionary<string, string> Apis { get; set; }

        public Dictionary<string, ApiClient> Clients { get; set; }
    }

    public class ApiClient
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Secret { get; set; }

        public string[] RedirectUris { get; set; }

        public string[] Scopes { get; set; }
    }
}