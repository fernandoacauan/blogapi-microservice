namespace Blog.Identity.Domain.Constants.Role;

public static class RoleConstant
{
    private const string Admin = "a1111111-1111-1111-1111-111111111111";
    private const string User = "b2222222-2222-2222-2222-222222222222";
    public static readonly string AdminName = "Admin";
    public static readonly string UserName = "User";
    public static Guid AdminId = Guid.Parse(Admin);
    public static Guid UserId = Guid.Parse(User);
}
