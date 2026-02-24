using Common.Domain;
using Media.Domain.Enums;

namespace Media.Domain.Models;

public class MediaFile : Entity<Guid>
{
    public string FileName { get; private set; } = null!;
    public string ObjectKey { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long Size { get; private set; }
    public MediaStatus Status { get; private set; }
    public DateTime CreateAtUtc { get; private set; }
    public DateTime? DeletedAtUtc { get; private set; }

    protected MediaFile() { }

    private MediaFile(string fileName, string objectKey, string contentType, long size)
    {
        Id = Guid.NewGuid();
        FileName = fileName;
        ObjectKey = objectKey;
        ContentType = contentType;
        Size = size;
        Status = MediaStatus.Active;
        CreateAtUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Factory method
    /// </summary>
    public static MediaFile Create(string fileName, string objectKey, string contentType, long size)
    {
        Validate(fileName, contentType, size);
        
        return new MediaFile(fileName, objectKey, contentType, size);
    }
    
    public void MarkAsDeleted()
    {
        if (Status == MediaStatus.Deleted)
            return;

        Status = MediaStatus.Deleted;
        DeletedAtUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (Status != MediaStatus.Deleted)
            return;

        Status = MediaStatus.Active;
        DeletedAtUtc = null;
    }
    
    private static void Validate(string fileName, string contentType, long size)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name is required");

        if (string.IsNullOrWhiteSpace(contentType))
            throw new ArgumentException("Content type is required");

        if (size <= 0)
            throw new ArgumentException("File size must be greater than zero");

        const long maxSize = 50 * 1024 * 1024; //50MB

        if (size > maxSize)
            throw new InvalidOperationException("File exceeds maximum allowed size");
    }
}