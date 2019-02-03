using System;
using System.Collections.Concurrent;
using System.Security.Cryptography.X509Certificates;

namespace Ignite.Common.KeyVault
{
    public static class CertificateExtensions
    {
        public static X509Certificate2 FindByThumbprint(string thumbprint) => StoreLocation.LocalMachine.FindCertificateByThumbprint(thumbprint);

        public static X509Certificate2 FindCertificateByThumbprint(this StoreLocation location, string thumbprint, StoreName name = StoreName.My)
        {
            return CertificateExtensions.FindCertificateInternal(location, name, X509FindType.FindByThumbprint, thumbprint, false);
        }

        public static bool ConvertToByteArray(this string encodedCert, out byte[] cert)
        {
            try
            {
                cert = Convert.FromBase64String(encodedCert);
                return true;
            }
            catch (Exception)
            {
                cert = null;
                return false;
            }
        }

        private static X509Certificate2 FindCertificateInternal(StoreLocation storeLocation, StoreName storeName, X509FindType findType, string findValue, bool validOnly)
        {
            using (var store = new X509Store2(storeName, storeLocation, OpenFlags.ReadOnly))
            {
                var certificates = store.Certificates.Find(findType, findValue, validOnly);
                return certificates.Count >= 1 ? certificates[0] : null;
            }
        }

        private class X509Store2 : IDisposable
        {
            private readonly X509Store store;

            public X509Store2(StoreName storeName, StoreLocation storeLocation, OpenFlags flags)
            {
                store = new X509Store(storeName, storeLocation);
                store.Open(flags);
            }

            public X509Certificate2Collection Certificates
            {
                get { return store.Certificates; }
            }

            public void Dispose()
            {
                store.Close();
            }
        }
    }
}