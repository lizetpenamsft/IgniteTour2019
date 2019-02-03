using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Ignite.Common.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Logging;

namespace Ignite.Common.Authentication
{
    public static class KeyVaultConfigurationExtensions
    {
        public static X509Certificate2 GetCertificate(this IKeyVaultRepository secrets, string keyVaultKey, ILogger logger)
        {
            SecretBundle rawCertificate;

            try
            {
                rawCertificate = secrets.GetSecretBundleAsync(keyVaultKey).Result;
            }
            catch (Exception e)
            {
                logger.LogError(0, e, "Failed to get the certificate with key '{0}'", keyVaultKey);
                return null;
            }

            Byte[] certificate;

            if (!rawCertificate.Value.ConvertToByteArray(out certificate))
            {
                logger.LogError(0, "Could not decode certificate.");
                return null;
            }

            if (certificate == null || certificate.Length == 0)
            {
                logger.LogError(0, "Empty certificate.");
                return null;
            }

            try
            {
                return new X509Certificate2(certificate);
            }
            catch (CryptographicException e)
            {
                logger.LogError(0, e, "Failed to initialize certificate.");
            }
            return null;
        }
    }
}