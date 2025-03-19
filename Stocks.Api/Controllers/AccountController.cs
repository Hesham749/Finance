using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stocks.Api.DTOs.Account;

namespace Stocks.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var appUser = new AppUser
                {
                    Email = registerDTO.Email,
                    NormalizedEmail = registerDTO.Email.ToUpper(),
                    UserName = registerDTO.UserName,
                    NormalizedUserName = registerDTO.UserName.ToUpper(),
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);
                if (createdUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(appUser, "User");
                    if (addRole.Succeeded)
                        return Ok("User created");
                    else
                        return StatusCode(StatusCodes.Status500InternalServerError, addRole.Errors);
                }
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, (createdUser.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
