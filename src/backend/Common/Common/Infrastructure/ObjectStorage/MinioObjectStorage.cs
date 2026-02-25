using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Common.Infrastructure.ObjectStorage;

public class MinioObjectStorage : IObjectStorage
{
    private readonly MinioConfig _config;
    private readonly IMinioClient _client;

    public MinioObjectStorage(IOptions<MinioConfig> options)
    {
        _config = options.Value;
        
        _client = new MinioClient()
            .WithEndpoint(_config.Endpoint)
            .WithCredentials(_config.AccessKey, _config.SecretKey);

        if (_config.UseSSL)
        {
            _client.WithSSL();
        }

        _client.Build();
    }
    
    /// <inheritdoc />
    public async Task StoreAsync(string storageKey, Stream content, CancellationToken cancellationToken = default)
    {
        var args = new PutObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(storageKey)
            .WithObjectSize(content.Length)
            .WithStreamData(content);

        await _client.PutObjectAsync(args, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<string> GetPresignedUploadUrlAsync(string storageKey, Stream content, int expirySeconds = 600, CancellationToken cancellationToken = default)
    {
        var presignedArgs = new PresignedPutObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(storageKey)
            .WithExpiry(expirySeconds); // ссылка действительна 10 минут по умолчанию

        var url = await _client.PresignedPutObjectAsync(presignedArgs);

        return url;
    }

    /// <inheritdoc />
    public async Task<Stream> GetStreamAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        
        var args = new GetObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(storageKey)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });

        await _client.GetObjectAsync(args, cancellationToken);

        memoryStream.Position = 0;
        return memoryStream;
    }

    /// <inheritdoc />
    public async Task<string?> GetLinkAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(storageKey)
            .WithExpiry(30);

        return await _client.PresignedGetObjectAsync(args);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default)
    {
        var args = new RemoveObjectArgs()
            .WithBucket(_config.BucketName)
            .WithObject(storageKey);

        await _client.RemoveObjectAsync(args, cancellationToken);
    }
}