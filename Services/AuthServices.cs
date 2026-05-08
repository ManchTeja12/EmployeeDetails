using EmployeeManage.Data;
using EmployeeManage.Entities;
using EmployeeManage.Models;
using EmployeeManage.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;   
using BC = BCrypt.Net.BCrypt;
    
namespace EmployeeManage.Services
{
    public class AuthServices(UserDbContext context, IConfiguration configuration) :IAuthService
    {
        public async Task Register(RegisterDto user)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.UserEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                throw new ArgumentException("InvalidEmail");
            }
            var exitinguser = await context.users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            if (exitinguser != null)
            {
                throw new InvalidOperationException("AlreadyExists");
            }
            var hashpassword = BC.HashPassword(user.Password);
            var newuser = new User
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                DOB=user.DOB,
                Passwordhash = hashpassword
            };
            context.Add(newuser);
            await context.SaveChangesAsync();
        }
        public async Task<string?> Login(UserDto user)
        {
            var existinguser = await context.users.FirstOrDefaultAsync(u => u.UserEmail == user.UserEmail);
            if (existinguser==null)
            {
                throw new KeyNotFoundException("UserNotFound");
            }
            if (!BC.Verify(user.Password, existinguser.Passwordhash))
            {
                throw new UnauthorizedAccessException("InvalidPassword");
            }
            return CreateToken(existinguser);
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email",user.UserEmail),
                new Claim("UserId",user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
        DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
        ClaimValueTypes.Integer64)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AuthSettings:SecretKey")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new JwtSecurityToken(
               issuer: configuration["AuthSettings:Issuer"],
               audience: configuration["AuthSettings:Audience"],
               claims: claims,
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddDays(1),
               signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
