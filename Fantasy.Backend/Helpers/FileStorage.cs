using Azure.Storage.Blobs;

namespace Fantasy.Backend.Helpers;

public class FileStorage : IFileStorage
{
    private readonly string _connectionString;

    public FileStorage(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AzureStorage");
    }

    public async Task RemoveFileAsync(string path, string containerName)
    {
        var client = new BlobContainerClient(_connectionString, containerName);
        await client.CreateIfNotExistsAsync();
        var fileName = Path.GetFileName(path);
        var blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> SaveFileAsync(byte[] content, string extention, string containerName)
    {
        var client = new BlobContainerClient(_connectionString, containerName);
        await client.CreateIfNotExistsAsync();

        //// Esta Parte es diferente a la del video(19) probablemente sea por la version
        client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
        var fileName = $"{Guid.NewGuid()}{extention}";
        var blob = client.GetBlobClient(fileName);

        using (var ms = new MemoryStream(content))
        {
            await blob.UploadAsync(ms);
        }

        return blob.Uri.ToString();
    }
}