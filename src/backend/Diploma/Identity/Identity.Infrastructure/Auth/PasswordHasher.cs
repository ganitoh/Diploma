using Identity.Infrastructure.Auth.Abstractions;

namespace Identity.Infrastructure.Auth;

/// <summary>
/// Класс для работы с паролями
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <summary>
    /// Генерация хеша пароля
    /// </summary>
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    /// <summary>
    /// Проверка на соответствие
    /// </summary>
    public bool Verify(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
}