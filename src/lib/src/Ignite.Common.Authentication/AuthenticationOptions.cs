namespace Ignite.Common.Authentication
{
    public class AuthenticationOptions
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string Resource { get; set; }
        public string KeyVaultKey { get; set; }
        public string Authority { get; set; }
    }
}