using Microsoft.AspNetCore.Identity;

namespace Services.Authentication;
public class ApplicationRoles : IdentityRole
{
    public const string admin = "Admin";
    public const string user = "User";
}