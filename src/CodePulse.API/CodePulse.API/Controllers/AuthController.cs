using CodePulse.API.Models.Dtos.Requests.Auth;
using CodePulse.API.Models.Dtos.Responses;
using CodePulse.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenRepository _tokenRepository;

    public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
    {
        var identityUser = await _userManager.FindByEmailAsync(loginRequest.Email.Trim());

        if(identityUser is not null)
        {
            var passwordValid = await _userManager.CheckPasswordAsync(identityUser, loginRequest.Password);

            if(passwordValid)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);

                var token = _tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                var response = new LoginResponseDto()
                {
                    Email = loginRequest.Email,
                    Roles = roles.ToList(),
                    Token = token

                };

                return Ok(response);
            }
        }

        ModelState.AddModelError("", "Email or Password Incorrect");

        return ValidationProblem(ModelState);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]AuthRequestDto authRequest)
    {
        var user = new IdentityUser
        {
            UserName = authRequest.Email?.Trim(),
            Email = authRequest.Email?.Trim(),
        };

        var identityResult = await _userManager.CreateAsync(user, authRequest.Password);

        if(identityResult.Succeeded)
        {
            
            await _userManager.AddToRoleAsync(user, "Reader");

            if (identityResult.Succeeded)
                return Ok();
            else
            {
                if (identityResult.Errors.Any())
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

        }
        else
        {
            if(identityResult.Errors.Any())
            {
                foreach(var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }

        return ValidationProblem(ModelState);
    }
}
