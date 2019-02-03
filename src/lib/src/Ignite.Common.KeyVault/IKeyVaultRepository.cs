using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;

namespace Ignite.Common.KeyVault
{
    public interface IKeyVaultRepository
    {
        string Name { get; }
        Task<string> GetAsync(string name);
        Task<SecretBundle> GetSecretBundleAsync(string name);
    }
}