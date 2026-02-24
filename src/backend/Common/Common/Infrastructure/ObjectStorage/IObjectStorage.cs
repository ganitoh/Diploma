namespace Common.Infrastructure.ObjectStorage;

/// <summary>
/// Работа с хранилищем файлом
/// </summary>
public interface IObjectStorage
{
    /// <summary>
    /// Добавить файл
    /// </summary>
    Task StoreAsync(string storageKey, Stream content, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить фйал
    /// </summary>
    Task<Stream> GetStreamAsync(string storageKey, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить ссылку на скачиваине
    /// </summary>
    Task<string?> GetLinkAsync(string storageKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить файл
    /// </summary>
    Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default);
}