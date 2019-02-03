using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Ignite.Common.KeyVault;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Ignite.Common.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly ILogger _logger;
        private AuthenticationOptions _options;
        private AuthenticationContext _authenticationContext;
        private ClientAssertionCertificate _clientAssertion;
        private X509Certificate2 _certificate;

        public Authenticator(ILogger<Authenticator> logger, AuthenticationOptions options, IKeyVaultRepository keyVaultRespository)
        {
            _logger = logger;
            _options = options;
            _certificate = keyVaultRespository.GetCertificate(options.KeyVaultKey, _logger);

            Initialize(options?.ClientId);
        }

        public async Task<string> Authenticate()
        {
            var token = await _authenticationContext.AcquireTokenAsync(_options.Resource, _clientAssertion).ConfigureAwait(false);
            return token.AccessToken;
        }

        private void Initialize(string clientId)
        {
            try
            {
                _authenticationContext = new AuthenticationContext(string.Format(_options.Authority, _options?.TenantId));
                _clientAssertion = new ClientAssertionCertificate(clientId, _certificate);
            }
            catch (Exception e)
            {
                _logger.LogError(0, e, "Error initializing client assertion from certificate.");
            }
        }
    }
}