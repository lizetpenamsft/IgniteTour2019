using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ignite.API.Common.Coordintes;
using Ignite.API.Common.Settings;
using Ignite.API.Common.UXO;
using Ignite.Data.UXO.DataAccess;
using Ignite.Data.UXO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Ignite.Data.UXO.Queries
{
    public class UXOItemQuery : IUXOItemQuery
    {
        private readonly IUXODbContext _context;
        private readonly StorageSettings _storageSettings;

        public UXOItemQuery(IUXODbContext context, IOptions<StorageSettings> storageSettings)
        {
            _context = context;
            _storageSettings = storageSettings.Value;
        }

        public async Task<List<UXOMapItem>> GetMapItems()
        {
            var items = await _context.UXOs.Select(u => new UXOMapItem()
            {
                Id = u.Id,
                Latitude = u.Latitude,
                Longitude = u.Longitude,
                Symbol = u.Symbol2525
            }).ToListAsync();

            return items;
        }

        public async Task<Ignite.API.Common.UXO.UXO> GetItem(string id)
        {
            var item = await _context.UXOs.Include(uxo => uxo.Contact).FirstAsync(u => u.Id == id);
            if (item != null)
            {
                return new Ignite.API.Common.UXO.UXO()
                {
                    ContactCallSign = item.Contact.CallSign,
                    ContactFrequency = item.Contact.Frequency,
                    ContactName = item.Contact.Name,
                    ContactPhone = item.Contact.Phone,
                    Id = item.Id,
                    Location = CoordinateExtensions.LonLatToMGRS(item.Longitude, item.Latitude),
                    MissionImpact = item.MissionImpact,
                    NBCContamination = item.NBCContamination,
                    Ordinance = item.OrdinanceType,
                    OrdinanceText = item.OrdinanceType == OrdinanceType.Other ? item.OtherOrdinance : item.OrdinanceType.ToString(),
                    Priority = item.Priority,
                    PriorityText = item.Priority.ToString(),
                    ProtectiveMeasures = item.ProtectiveMeasures,
                    Reported = item.ReportedDate,
                    ReportingUnit = item.ReportingUnit,
                    ResourcesThreatened = item.ResourcesThreatened
                };
            }
            return null;
        }
    }
}