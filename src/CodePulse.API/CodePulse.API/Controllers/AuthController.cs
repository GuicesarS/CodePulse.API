using CodePulse.API.Models.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
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
