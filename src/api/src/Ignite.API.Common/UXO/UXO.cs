using System;

namespace Ignite.API.Common.UXO
{
    public class UXO
    {
        public string Id { get; set; }
        public DateTime Reported { get; set; }
        public string Location { get; set; }
        public string ReportingUnit { get; set; }
        public string ContactFrequency { get; set; }
        public string ContactCallSign { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public OrdinanceType Ordinance { get; set; }
        public string OrdinanceText { get; set; } 
        public string NBCContamination { get; set; }
        public string ResourcesThreatened { get; set; }
        public string MissionImpact { get; set; }
        public string ProtectiveMeasures { get; set; }
        public Priority Priority { get; set; }
        public string PriorityText { get; set; }
    }
}