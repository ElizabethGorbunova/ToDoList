using Microsoft.AspNetCore.Identity;
using ToDoWebApp.Entities;
using ToDoWebApp.ModelsDto;
using ToDoWebApp.Exceptions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace ToDoWebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly TaskDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(TaskDbContext _dbContext, IPasswordHasher<User> _passwordHasher, AuthenticationSettings _authenticationSettings)
        {
            dbContext = _dbContext;
            passwordHasher = _passwordHasher;
            authenticationSettings = _authenticationSettings;

        }

        public void RegisterUser(UserDto user)
        {
            var newUser = new User()
            {
                Name = user.Name,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                RoleId = user.RoleId,

            };

            var hashedPassword = passwordHasher.HashPassword(newUser, user.Password);
            newUser.PasswordHash = hashedPassword;

            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginDto loginDto)
        {
            var user = dbContext.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            };

           var passwordResult= passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (passwordResult == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,  user.UserId.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Name} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("DateOfBirth", user.DateOfBirth.ToString()), 

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer, 
                authenticationSettings.JwtIssuer, 
                claims, 
                expires: expires,
                signingCredentials:cred);


            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
