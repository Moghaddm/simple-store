using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtService _jwtService;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        JwtService jwtService,
        SignInManager<ApplicationUser> signInManager
    ) => (_userManager, _jwtService, _signInManager) = (userManager, jwtService, signInManager);

    [HttpGet("Login", Name = nameof(Login))]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Username,
            model.Password,
            false,
            lockoutOnFailure: false
        );
        if (!result.Succeeded)
            return Unauthorized();
        var user = await _userManager.FindByEmailAsync(model.Email);
        var token = _jwtService.GenerateToken(model);
        return Ok(
            new
            {
                Token = token,
                User = user,
                Expire = "5 Minutes"
            }
        );
    }

    [HttpPost("Register", Name = nameof(Register))]
    public async Task<IActionResult> Register([FromBody] ApplicationUser user)
    {
        var newUser = new ApplicationUser
        {
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password
        };
        var result = await _userManager.CreateAsync(newUser, newUser.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        return Ok(new { Message = "Your Registeration Being Successfully !" });
    }
}
