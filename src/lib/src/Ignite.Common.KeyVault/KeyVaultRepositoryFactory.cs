using System;
using Microsoft.Extensions.Configuration;

namespace Ignite.Common.KeyVault
{
    public static class KeyVaultRepositoryFactory
    {
        public static CloudEnvironment Environment { get; set; }

        static KeyVaultRepositoryFactory()
        {
            Environment = CloudEnvironment.AzureCommercial;
        }

        public static string VaultUrl
        {
            get
            {
                string url = "";
                switch (Environment)
                {
                    case CloudEnvironment.AzureCommercial:
                        url = "https://{0}.vault.azure.net";
                        break;
                    case CloudEnvironment.AzureGovernment:
                        url = "https://{0}.vault.usgovcloudapi.net";
                        break;
                }
                return url;
            }
        }

        public static IKeyVaultRepository GetRepository(IConfiguration configuration, string vaultName = null)
        {
            var vault = vaultName ?? configuration.GetConfiguration("KeyVault", "Name");
            var appId = configuration.GetConfiguration("KeyVault", "ADApplicationId");
            var thumbprint = configuration.GetConfiguration("KeyVault", "Thumbprint");

            return new KeyVaultRepository(string.Format(VaultUrl, vault), appId, thumbprint);
        }
    }
}