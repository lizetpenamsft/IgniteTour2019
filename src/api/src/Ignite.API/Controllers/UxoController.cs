using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Ignite.API.Common.UXO;
using Ignite.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ignite.API.Controllers
{
    [Authorize]
    [Route("recons/uxo/[controller]")]
    [ApiController]
    public class UxoController : ControllerBase
    {
        private readonly IUXOService _uxoService;
        private readonly IUXODocumentService _documentService;

        public UxoController(IUXOService uxoService, IUXODocumentService uxoDocumentService)
        {
            _uxoService = uxoService;
            _documentService = uxoDocumentService;
        }

        [HttpGet]
        public async Task<List<UXOMapItem>> GetUXOs()
        {
            return await _uxoService.GetUXOsForDisplay();
        }

        [HttpGet("{uxoid}/details")]
        public async Task<UXO> GetUXOData(string uxoid)
        {
            return await _uxoService.FetchUXO(uxoid);
        }

        [HttpPost("{uxoid}/documents/create")]
        public async Task<IActionResult> GenerateDocument(string uxoid)
        {
            var contentType = "APPLICATION/octet-stream";
            var uxo = await _uxoService.FetchUXO(uxoid);
            if (uxo == null)
            {
                return NotFound(uxoid);
            }

            var fileBytes = await _documentService.CreateDocument(uxo);
            if (fileBytes == null || fileBytes.Length == 0)
            {
                return NoContent();
            }

            var fileName = $"UXO-{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mmZ")}.docx";

            var contentDisposition = new ContentDisposition()
            {
                FileName = fileName,
                Inline = false
            };

            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(fileBytes, contentType);
        }
    }
}