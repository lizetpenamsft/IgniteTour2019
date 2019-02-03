using System;
using Ignite.API.Common.UXO;

namespace Ignite.Data.UXO.Entities
{
    public class UXODTO
    {
        public string Id { get; set; }
        public DateTime ReportedDate { get; set; }
        public string ReportingUnit { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public OrdinanceType OrdinanceType { get; set; }
        public string OtherOrdinance { get; set; }
        public string NBCContamination { get; set; }
        public string ResourcesThreatened { get; set; }
        public string MissionImpact { get; set; }
        public string ProtectiveMeasures { get; set; }
        public Priority Priority { get; set; }
        public string Symbol2525 { get; set; }

        public virtual ContactDTO Contact { get; set; }
    }
}