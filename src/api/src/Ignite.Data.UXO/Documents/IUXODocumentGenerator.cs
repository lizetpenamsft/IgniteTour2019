using System.Threading.Tasks;

namespace Ignite.Data.UXO.Documents
{
    public interface IUXODocumentGenerator
    {
        Task<byte[]> GenerateTemplateAsync(Ignite.API.Common.UXO.UXO uxo);
    }
}