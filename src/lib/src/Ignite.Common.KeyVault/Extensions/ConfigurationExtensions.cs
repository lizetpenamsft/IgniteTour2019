using System;
using Microsoft.Extensions.Configuration;

namespace Ignite.Common.KeyVault
{
    public static class ConfigurationExtensions
    {
        public static string GetConfiguration(this IConfiguration configuration, string section, string key)
        {
            return configuration[section + ":" + key];
        }
    }
}