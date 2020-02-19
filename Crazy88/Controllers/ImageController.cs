using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Crazy88Test.Controllers
{
    [Serializable]
    public class Image
    {
        public string image;
    }

    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        const string storageConnectionString = "DefaultEndpointsProtocol=https;"
                                         + "AccountName=crazy88storage"
                                         + ";AccountKey=dVofx0e7EYfyNDdj/HltVy6wNB1VP/n+q7EZwG5TLgJrObNhti0w9kfCYo9uMHmvb9HUMtK0T5YSVDU82Djtzg=="
                                         + ";EndpointSuffix=core.windows.net";

        [HttpPost("{id}")]
        public async Task<ActionResult<string>> PostImage(string id, Image image)
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient cloudBlobClient = account.CreateCloudBlobClient();
            CloudBlobContainer container = cloudBlobClient.GetContainerReference("images");

            container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };

            await container.SetPermissionsAsync(permissions);

            //name that will be given to file which is being uploaded
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(id + "_" + Guid.NewGuid() + ".jpg");

            byte[] data = Convert.FromBase64String(image.image);
            using (var mem = new MemoryStream(data))
            {
                await cloudBlockBlob.UploadFromStreamAsync(mem);
            }

            return cloudBlockBlob?.Uri.ToString();
        }
    }
}