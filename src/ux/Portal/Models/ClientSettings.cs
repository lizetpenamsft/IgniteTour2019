namespace Portal.Models
{
    public class ClientSettings
    {
        public string AadClientId { get; set; }
        public string AadTenant { get; set; }
        public string ApiId { get; set; }
        public bool SkipAuth { get; set; }
        public string AadInstance { get; set; }
        public string ApiUrl { get; set; }
        public string IconBaseUrl { get; set; }
    }
}