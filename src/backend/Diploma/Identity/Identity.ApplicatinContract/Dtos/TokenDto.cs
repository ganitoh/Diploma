namespace Identity.ApplicatinContract.Dtos;

public class TokenDto
{
    /// <summary>
    /// Токен доступа
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Токен обновления
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    public TokenDto() { }
    
    public TokenDto(string accessToken, string refreshToken)
    {
        this.AccessToken = accessToken;
        this.RefreshToken = refreshToken;
    }
}