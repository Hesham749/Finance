
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                new(JwtRegisteredClaimNames.GivenName , user.UserName),
                new(JwtRegisteredClaimNames.Email , user.Email),
                //new(ClaimTypes.SerialNumber,user.Id)
            };


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
