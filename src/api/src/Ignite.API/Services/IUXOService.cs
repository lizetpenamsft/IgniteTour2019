using System.Collections.Generic;
using System.Threading.Tasks;
using Ignite.API.Common.UXO;

namespace Ignite.API.Services
{
    public interface IUXOService
    {
        Task<List<UXOMapItem>> GetUXOsForDisplay();
        Task<UXO> FetchUXO(string id);
    }
}