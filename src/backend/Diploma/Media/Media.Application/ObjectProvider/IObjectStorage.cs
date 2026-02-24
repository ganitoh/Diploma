namespace Media.Application.ObjectProvider;

/// <summary>
/// Работа с файловым хранилещем
/// </summary>
public interface IObjectStorage
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    Task StoreAsync(string storageKey, Stream content, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получить файл
    /// </summary>
    Task<Stream> GetStreamAsync(string storageKey, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Удалить файл
    /// </summary>
    Task DeleteAsync(string storageKey, CancellationToken cancellationToken = default);
}