using EcommerceApp.DTO;
using EcommerceApp.Enum;
using EcommerceApp.Model;
using EcommerceApp.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthController(ApplicationDbContext dbContext, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("register")]
        public IActionResult Register( RegisterDto dto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (_dbContext.Users.Any(u => u.Email == dto.Email && u.IsActive == true)) return Conflict(new { message = "Email already registred " });
            if(_dbContext.Users.Any(u => u.Email == dto.Email && u.IsActive == false))
            {
                var existingUser = _dbContext.Users.Single(u => u.Email == dto.Email && u.IsActive == false);
                existingUser.IsActive = true;
                existingUser.FirstName = dto.FirstName;
                existingUser.LastName = dto.LastName;
                existingUser.Role = dto.Role;
                existingUser.PhoneNumber = dto.PhoneNumber;
                existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, dto.Password);
                _dbContext.SaveChanges();
                return Ok(new
                {
                    message = "User registered successfully",
                    UserName = existingUser.FirstName,
                    UserId = existingUser.Id,
                });
            }
            var user = new User();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.PhoneNumber = dto.PhoneNumber;
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(new { message = "User registered successfully",
                           UserName= user.FirstName,
                           UserId= user.Id,
                           });

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = _dbContext.Users.SingleOrDefault(u => u.Email == dto.Email && u.IsActive==true);
            if (user == null) return Unauthorized(new { message = "Invalid email or password" });



            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if(result == PasswordVerificationResult.Failed) return Unauthorized(new { message = "Invalid email or password" });

            var token = _tokenService.CreateToken(user);

        
            return Ok(new
            {
                message = "Login successful",
                accessToken = token,
                expiration = DateTime.UtcNow.AddHours(1).ToString("o")  
            });
        }

    }
}
