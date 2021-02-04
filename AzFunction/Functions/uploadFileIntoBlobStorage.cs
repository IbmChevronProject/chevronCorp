using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class uploadFileIntoBlobStorage
    {
        [FunctionName("uploadFileIntoBlobStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            StorageCredentials credentials = new StorageCredentials("uc1feedbackstorage", "6iWM1QIrXYlgnMuqvJQYa58UWo84Xo7sMe01XHivMJ0WACKdr/y303GAe1E+DEqmMly8FYDnIoD7dKCi3/543Q==");
            CloudStorageAccount acc = new CloudStorageAccount(credentials, useHttps: true);
            CloudBlobClient client = acc.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("feedbacks");
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            CloudBlockBlob cblob = container.GetBlockBlobReference("testblob");
            var bytes = Convert.FromBase64String(@"iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==");
            using (var stream = new MemoryStream(bytes))
            {
                cblob.UploadFromStream(stream);
            }
            string responseMessage = "This HTTP triggered function executed successfully!";

            return new OkObjectResult(responseMessage);
        }
    }
}
