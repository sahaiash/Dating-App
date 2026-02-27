using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO_s;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController(AppDbContext context,ITokenService tokenService):BaseApiController
    {
        [HttpPost("register")]

        public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
        {

            if(await EmailExsits(registerDTO.Email)) return BadRequest("Email Already Exsist");
           using var hmac=new HMACSHA512();
           var user = new AppUser
           {
               DisplayName= registerDTO.DisplayName,
               Email=registerDTO.Email,
               PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
               PasswordSalt=hmac.Key
           };

           context.Users.Add(user);
           await context.SaveChangesAsync();

           return user;
        }

        [HttpPost("login")]

        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user=await context.Users.SingleOrDefaultAsync(x=> x.Email==loginDTO.Email);
            if(user == null) return Unauthorized("Invalid Credentials");
            using var hmac=new HMACSHA512(user.PasswordSalt);

            var PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
            if(!PasswordHash.SequenceEqual(user.PasswordHash)) return Unauthorized("Invalid Credentials");
            return user.ToDto(tokenService);
        }

        private async Task<bool> EmailExsits (string email)
        {
            return await context.Users.AnyAsync(x=>x.Email.ToLower()==email.ToLower());
        }

    }
}
