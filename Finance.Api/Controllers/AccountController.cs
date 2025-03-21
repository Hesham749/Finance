using Microsoft.AspNetCore.Mvc;
using Finance.Api.DTOs.Account;

namespace Finance.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
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
                        return Ok(new NewUserDTO
                        {
                            UserName = registerDTO.UserName,
                            Email = registerDTO.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
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


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var appUser = await _userManager.FindByNameAsync(loginDTO.UserName);
            if (!await _userManager.CheckPasswordAsync(appUser, loginDTO.Password))
                return Unauthorized($"UserName or password incorrect!");

            return Ok(new NewUserDTO
            {
                UserName = loginDTO.UserName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            });
        }


    }
}
