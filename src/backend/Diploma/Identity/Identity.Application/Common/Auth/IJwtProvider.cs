using System.Globalization;
using Identity.Domain.Models;

namespace Identity.Application.Common.Auth;

/// <summary>
/// Создание jwt токена
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Генерация токена на основе данных пользователя
    /// </summary>
    string GenerateToken(User user);
}