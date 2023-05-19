using Microsoft.AspNetCore.Identity;

namespace Services.Authentication;
public class ApplicationUser : IdentityUser
{
    public int Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}