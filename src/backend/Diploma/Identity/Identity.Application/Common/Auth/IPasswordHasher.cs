namespace Identity.Application.Common.Auth;

/// <summary>
/// Класс для работы с паролями
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