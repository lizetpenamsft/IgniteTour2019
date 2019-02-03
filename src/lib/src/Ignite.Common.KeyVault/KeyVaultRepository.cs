using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Ignite.Common.KeyVault
{
    public class KeyVaultRepository : IKeyVaultRepository
    {
        private IKeyVaultClient KeyVaultClient { get; }
        public string Name { get; }

        public KeyVaultRepository(string vault, string adAppId, string thumbprint)
        : this(new KeyVaultClient((authority, resource, scope) => GetTokenByThumbprint(adAppId, thumbprint, authority, resource, CancellationToken.None)), vault)
        {

        }

        public KeyVaultRepository(IKeyVaultClient client, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            KeyVaultClient = client ?? throw new ArgumentNullException(nameof(client));
        }

        private static async Task<string> GetTokenByThumbprint(string adAppId, string thumbprint, string authority, string resource, CancellationToken cancellationToken)
        {
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
            var certificate = CertificateExtensions.FindByThumbprint(thumbprint);
            var clientAssertion = new ClientAssertionCertificate(adAppId, certificate);

            var result = await context.AcquireTokenAsync(resource, clientAssertion).ConfigureAwait(false);

            if (result?.AccessToken == null)
            {
                throw new InvalidOperationException($"Unable to acquire token for resource {resource}. Authority: {authority}. ApplicationId: {adAppId}. Thumbprint: {thumbprint}");
            }

            return result.AccessToken;
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