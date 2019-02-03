using System.Threading.Tasks;
using Ignite.API.Common.UXO;
using Ignite.Data.UXO.Documents;

namespace Ignite.API.Services
{
    public class UXODocumentService : IUXODocumentService
    {
        private readonly IUXODocumentGenerator _documentGenerator;
        public UXODocumentService(IUXODocumentGenerator documentGenerator)
        {
            _documentGenerator = documentGenerator;
        }

        public async Task<byte[]> CreateDocument(UXO uxo)
        {
            return await _documentGenerator.GenerateTemplateAsync(uxo);
        }
    }
}