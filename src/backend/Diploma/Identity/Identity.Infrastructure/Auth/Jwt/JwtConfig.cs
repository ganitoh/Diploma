namespace Identity.Infrastructure.Auth.Jwt;

/// <summary>
/// Конфигурация для создания jwt токена
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// Ключ для шифрования
    /// </summary>
    public string SecretKey { get; set; }
    
    /// <summary>
    /// Пользователь токена
    /// </summary>
    public string Audience { get; set; }
    
    /// <summary>
    /// Издатель
    /// </summary>
    public string Issuer { get; set; }
    
    /// <summary>
    /// Время действия токена (в часах)
    /// </summary>
    public int ExpiresHours { get; set; }
}