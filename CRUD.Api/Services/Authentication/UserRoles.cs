using Microsoft.AspNetCore.Identity;

namespace Services.Authentication;
public class UserRoles : IdentityRole
{
    public const string admin = "Admin";
    public const string user = "user";
}