namespace Common.Infrastructure.ObjectStorage;

public class MinioConfig
{
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Endpoint { get; set; }
    public string BucketName { get; set; }
    public bool UseSSL { get; set; }
}