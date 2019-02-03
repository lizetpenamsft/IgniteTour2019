using System;

namespace Ignite.API.Common.Documents
{
    public class DocumentHistory
    {
        public string ItemId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DocumentUri { get; set; }
    }
}