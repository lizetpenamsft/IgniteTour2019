using System.Collections.Generic;
using System.Threading.Tasks;
using Ignite.API.Common.UXO;

namespace Ignite.Data.UXO.Queries
{
    public interface IUXOItemQuery
    {
        Task<List<UXOMapItem>> GetMapItems();
        Task<Ignite.API.Common.UXO.UXO> GetItem(string id);
    }
}