using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Newtonsoft.Json.Linq;

namespace Ignite.Common.KeyVault
{
    public class KeyVaultMSIRepository : BaseKeyVaultRepository
    {
        public KeyVaultMSIRepository(string vault, string msiClientId)
        : base(new KeyVaultClient((authority, resource, scope) => GetTokenByMSI(resource, msiClientId)), vault)
        {
        }

        // https://stackoverflow.com/a/54241207
        public static async Task<string> GetTokenByMSI(string resource, string clientId = null)
        {
            var endpoint = Environment.GetEnvironmentVariable("MSI_ENDPOINT", EnvironmentVariableTarget.Process);
            var secret = Environment.GetEnvironmentVariable("MSI_SECRET", EnvironmentVariableTarget.Process);

            if (string.IsNullOrEmpty(endpoint))
            {
                throw new InvalidOperationException("MSI_ENDPOINT environment variable not set");
            }
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("MSI_SECRET environment variable not set");
            }

            Uri uri;
            if (clientId == null)
            {
                uri = new Uri($"{endpoint}?resource={resource}&api-version=2017-09-01");
            }
            else
            {
                uri = new Uri($"{endpoint}?resource={resource}&api-version=2017-09-01&clientid={clientId}");
            }

            // get token from MSI
            var tokenRequest = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };
            tokenRequest.Headers.Add("secret", secret);
            var httpClient = new HttpClient();

            var response = await httpClient.SendAsync(tokenRequest);


            var body = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(body);

            string token = result["access_token"].ToString();
            return token;

        }
    }
}