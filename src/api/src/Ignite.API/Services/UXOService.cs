using System.Collections.Generic;
using System.Threading.Tasks;
using Ignite.API.Common.Settings;
using Ignite.API.Common.UXO;
using Ignite.Common.KeyVault;
using Ignite.Data.UXO.DataAccess;
using Ignite.Data.UXO.Queries;
using Microsoft.Extensions.Options;

namespace Ignite.API.Services
{
    public class UXOService : IUXOService
    {
        private readonly IUXOItemQuery _uxoQuery;

        public UXOService(IUXOItemQuery uxoQuery)
        {
            _uxoQuery = uxoQuery;
        }

        public async Task<UXO> FetchUXO(string id)
        {
            var item = await _uxoQuery.GetItem(id);
            return item;
        }

        public async Task<List<UXOMapItem>> GetUXOsForDisplay()
        {
            var uxoItems = await _uxoQuery.GetMapItems();
            return uxoItems;
        }
    }
}