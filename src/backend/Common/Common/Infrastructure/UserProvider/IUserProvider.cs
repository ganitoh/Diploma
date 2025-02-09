namespace Common.Infrastructure.UserProvider;

public interface IUserProvider
{
    Guid GetUserId();

    string? GetUserName();
}