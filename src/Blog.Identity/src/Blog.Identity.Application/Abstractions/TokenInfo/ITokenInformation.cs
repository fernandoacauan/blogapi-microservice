namespace Blog.Identity.Application.Abstractions.TokenInfo;

public interface ITokenInformation
{
    bool    IsAdmin();
    string  GetEmail();
    Guid    GetId();
}
