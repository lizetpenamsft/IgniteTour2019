using System.Threading.Tasks;
using Ignite.API.Common.UXO;

namespace Ignite.API.Services
{
    public interface IUXODocumentService
    {
        Task<byte[]> CreateDocument(UXO uxo);
    }
}