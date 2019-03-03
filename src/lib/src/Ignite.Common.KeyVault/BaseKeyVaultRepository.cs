using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;

namespace Ignite.Common.KeyVault
{
    public class BaseKeyVaultRepository : IKeyVaultRepository
    {
        protected IKeyVaultClient KeyVaultClient { get; }
        public string Name { get; }

        protected BaseKeyVaultRepository(IKeyVaultClient client, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            KeyVaultClient = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetAsync(string name)
        {
            var secret = "";

            var value = await GetSecretBundleAsync(name);
            if (value != null)
            {
                secret = value.Value;
            }
            return secret;
        }

        public async Task<SecretBundle> GetSecretBundleAsync(string name)
        {
            SecretBundle value;
            try
            {
                value = await KeyVaultClient.GetSecretAsync(Name, name);
            }
            catch (KeyVaultErrorException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw ex;
            }
            return value;
        }
    }
}