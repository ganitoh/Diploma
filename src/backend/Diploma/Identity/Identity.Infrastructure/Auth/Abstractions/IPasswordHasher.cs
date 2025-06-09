namespace Identity.Infrastructure.Auth.Abstractions;

/// <summary>
/// Абстракция для работы с паролями
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Генерация хеша пароля
    /// </summary>
    string Generate(string password);
    
    /// <summary>
    /// Проверка на соответствие
    /// </summary>
    bool Verify(string password, string passwordHash);
}