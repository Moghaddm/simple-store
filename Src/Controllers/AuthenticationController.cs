using DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Services.Authentication;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtService _jwtService;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(
        UserManager<ApplicationUser> userManager,
        JwtService jwtService,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AuthenticationController> logger
    ) => (_userManager, _jwtService, _signInManager,_logger) = (userManager, jwtService, signInManager,logger);

    [HttpGet("Login", Name = nameof(Login))]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.UserName,
            model.Password,
            false,
            lockoutOnFailure: false
        );
        if (!result.Succeeded)
            return Unauthorized();
        var appUser = new ApplicationUser
        {
            UserName = model.UserName,
            PasswordHash = model.Password
        };
        var user = await _userManager.FindByNameAsync(appUser.UserName);
        var roles=await _userManager.GetRolesAsync(user);
        var token = _jwtService.GenerateToken(user!,roles);
        _logger.LogInformation("Login Occurred!");
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
    public async Task<IActionResult> Register([FromBody] UserRegisterationModel user)
    {
        if (!ModelState.IsValid)
            return BadRequest("Send Correct Stracture for USERNAME and PASSWORD");
        var appUser = new ApplicationUser { UserName = user.UserName, Email = user.Email };
        var result = await _userManager.CreateAsync(appUser, user.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);
        _logger.LogInformation("Registeration is OK!");
        return RedirectToAction(
            "Login",
            "Authentication"
        // new { Message = "Your Registeration Being Successfully !" }
        );
    }

    [HttpGet("[action]", Name = "Logout")]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        _logger.LogInformation("User Logouted!");
        return Ok();
    }
}
