using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Ignite.API.Common.Documents;
using Ignite.API.Common.Settings;
using Ignite.Common.KeyVault;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;

namespace Ignite.Data.UXO.Documents
{
    public class UXODocumentGenerator : IUXODocumentGenerator
    {
        private static string TemplateFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Templates\9-LINE-Template.docx");

        private readonly IKeyVaultRepository _keyVaultRepository;
        private readonly StorageSettings _storageSettings;

        public UXODocumentGenerator(IKeyVaultRepository keyVaultRepository, IOptions<StorageSettings> storageOptions)
        {
            _keyVaultRepository = keyVaultRepository;
            _storageSettings = storageOptions.Value;
        }

        public async Task<byte[]> GenerateTemplateAsync(Ignite.API.Common.UXO.UXO uxo)
        {
            var tempDirectory = Path.GetTempPath();
            var documentDirectory = Path.Combine(tempDirectory, "IgniteDemo\\UXO");
            var outputFile = Path.Combine(documentDirectory, $"{Guid.NewGuid()}.docx");
            byte[] uploadedDocument = null;

            try
            {
                if (!Directory.Exists(documentDirectory))
                {
                    Directory.CreateDirectory(documentDirectory);
                }
                File.Copy(TemplateFile, outputFile);

                if (File.Exists(outputFile))
                {
                    var fieldReplacements = GetMappings(uxo);
                    using (var wordDoc = WordprocessingDocument.Open(outputFile, true))
                    {
                        var fields = wordDoc.MainDocumentPart.RootElement.Descendants<FieldCode>().ToList();
                        foreach (var replacement in fieldReplacements.Keys)
                        {
                            ReplaceMergeFieldWithText(fields, replacement, fieldReplacements[replacement]);
                        }
                        wordDoc.Save();
                    }
                }
                uploadedDocument = await UploadToBlobAsync(outputFile);
            }
            finally
            {
                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }
            }
            return uploadedDocument;
        }

        // https://stackoverflow.com/a/39116458
        private void ReplaceMergeFieldWithText(IEnumerable<FieldCode> fields, string mergeFieldName, string replacementText)
        {
            var field = fields
                .Where(f => f.InnerText.Contains(mergeFieldName))
                .FirstOrDefault();

            if (field != null)
            {
                var fieldCode = field.Parent as Run; 

                var previousRun = fieldCode.PreviousSibling<Run>();
                var nextRun = fieldCode.NextSibling<Run>();
                var textRun = nextRun.NextSibling<Run>();
                var endRun = textRun.NextSibling<Run>();

                var text = textRun.GetFirstChild<Text>();
                text.Text = replacementText;

                fieldCode.Remove();
                previousRun.Remove();
                nextRun.Remove();
                endRun.Remove();
            }
        }

        private async Task<byte[]> UploadToBlobAsync(string filePath)
        {
            byte[] file;
            try
            {
                var connectionString = await _keyVaultRepository.GetAsync(_storageSettings.ConnectionString);
                var storageAccount = CloudStorageAccount.Parse(connectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();
                var blobContainer = blobClient.GetContainerReference(_storageSettings.DocumentBlob.ToLower());
                await blobContainer.CreateIfNotExistsAsync();
                var blobItem = blobContainer.GetBlockBlobReference(Path.GetFileName(filePath));
                await blobItem.UploadFromFileAsync(filePath);
                await blobItem.FetchAttributesAsync();
                var fileByteLength = blobItem.Properties.Length;
                file = new byte[fileByteLength];
                for (int i = 0; i < fileByteLength; i++)
                {
                    file[i] = 0x20;
                }
                await blobItem.DownloadToByteArrayAsync(file, 0);
            }
            catch (Exception)
            {
                file = null;
            }
            return file;
        }

        private Dictionary<string, string> GetMappings(Ignite.API.Common.UXO.UXO uxo)
        {
            var replacementValues = new Dictionary<string, string>();
            replacementValues[Constants.DateTimeGroup] = uxo.Reported.ToUniversalTime().ToString("yyyy-MM-ddTHH:mmZ");
            replacementValues[Constants.ReportingUnit] = $"Reporting Unit: {uxo.ReportingUnit} - Location: {uxo.Location}";
            replacementValues[Constants.ContFreq] = uxo.ContactFrequency;
            replacementValues[Constants.ContCallSign] = uxo.ContactCallSign;
            replacementValues[Constants.ContPhone] = uxo.ContactPhone;
            replacementValues[Constants.ContName] = uxo.ContactName;
            replacementValues[Constants.OrdType] = uxo.OrdinanceText;
            replacementValues[Constants.NBCContamination] = uxo.NBCContamination;
            replacementValues[Constants.ResourcesThreatened] = uxo.ResourcesThreatened;
            replacementValues[Constants.MissionImpact] = uxo.MissionImpact;
            replacementValues[Constants.ProtectiveMeasures] = uxo.ProtectiveMeasures;
            replacementValues[Constants.Priority] = uxo.PriorityText;
            return replacementValues;
        }
    }
}