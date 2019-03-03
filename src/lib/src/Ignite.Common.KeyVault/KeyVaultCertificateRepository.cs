using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Ignite.Common.KeyVault
{
    public class KeyVaultCertificateRepository : BaseKeyVaultRepository
    {
        public KeyVaultCertificateRepository(string vault, string adAppId, string thumbprint)
        : base(new KeyVaultClient((authority, resource, scope) => GetTokenByThumbprint(adAppId, thumbprint, authority, resource, CancellationToken.None)), vault)
        {
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
    }
}