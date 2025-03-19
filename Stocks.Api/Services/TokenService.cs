
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Stocks.Api.Services
{
    public class TokenService : ITokenService
    {
        readonly IConfiguration _config;
        readonly SymmetricSecurityKey _Key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier , user.Id),
                new(ClaimTypes.GivenName , user.UserName),
                new(ClaimTypes.Email , user.Email),
                // token generated id
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            //todo adding roles


            var cred = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                signingCredentials: cred,
                claims: claims,
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(double.Parse(_config["JWT:ExpiresInMinutes"]))
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
