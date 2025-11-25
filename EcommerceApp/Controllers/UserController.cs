using EcommerceApp.Database;
using EcommerceApp.DTO;
using EcommerceApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {  
        private  readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher =new PasswordHasher<User>();

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost("register")]
        public IActionResult Register( User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            user.PasswordHash=_passwordHasher.HashPassword(null, user.PasswordHash);
           
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok(new { message= "Registered successfully",
                            userName=});

        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1. Find user by email only
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            // 2. Verify password using PasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

            if (result == PasswordVerificationResult.Success ||
                result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                return Ok(new
                {
                    message = "Login successful",
                    userName = user.FirstName,
                    email = user.Email
                });
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }

    }
}
