using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService(IConfiguration config) : ITokenService // Injecting Iconfig
    {
        public string CreateToken(AppUser user)
        {
            var tokenKey=config["TokenKey"] ?? throw new Exception("Cannot get key");
            if(tokenKey.Length<64) throw new Exception("Your token needs to be >=64 characters");
            var key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenKey));


            var claim= new List<Claim>
            {
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.NameIdentifier,user.Id)
                
            };
            var creds= new SigningCredentials(key,SecurityAlgorithms.HmacSha512);

            var tokenDescriptor= new SecurityTokenDescriptor
            {
                Subject=new ClaimsIdentity(claim),
                Expires=DateTime.UtcNow.AddDays(7),
                SigningCredentials=creds

            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);


            return tokenHandler.WriteToken(token);
        }
    }
}
