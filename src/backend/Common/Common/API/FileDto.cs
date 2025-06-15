namespace Common.API;

/// <summary>
/// Файл
/// </summary>
public class FileDto
{
    /// <summary>
    /// Название
    /// </summary>
    public string FileName { get; set; }
    
    /// <summary>
    /// Содержимое
    /// </summary>
    public byte[] Content { get; set; }
}