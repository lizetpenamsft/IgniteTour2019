using Ignite.Common.Authentication;

namespace Ignite.API.Models
{
    public class SqlSettings
    {
        public string ConnectionString { get; set; }
        public bool UseKeyVault { get; set; }
    }
}