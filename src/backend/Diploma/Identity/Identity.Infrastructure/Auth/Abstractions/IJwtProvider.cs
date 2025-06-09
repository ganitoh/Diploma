using Identity.Domain.Models;

namespace Identity.Infrastructure.Auth.Abstractions;

/// <summary>
/// Абстракция для рабоыт с jwt
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Генерация access токена
    /// </summary>
    string GenerateToken(User user);
    
    /// <summary>
    /// Генерация refresh токена
    /// </summary>
    public string GenerateRefreshToken();
}