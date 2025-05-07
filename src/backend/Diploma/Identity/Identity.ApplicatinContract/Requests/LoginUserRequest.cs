using System.ComponentModel.DataAnnotations;

namespace Identity.ApplicatinContract.Requests;

/// <summary>
/// Запрос для входа в систему
/// </summary>
public class LoginUserRequest
{
    /// <summary>
    /// Почта
    /// </summary>
    [Required]
    public string Email { get; set; } = null!;

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public string Password { get; set; } = null!;
}