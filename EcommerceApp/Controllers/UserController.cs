using EcommerceApp.Database;
using EcommerceApp.DTO;
using EcommerceApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EcommerceApp.Controllers
{
    [ApiController]
    [Authorize(Roles = "Customer,Admin,Seller,Guest")]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {  
        private  readonly ApplicationDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher =new PasswordHasher<User>();


        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            
            if(user == null) return Unauthorized();

            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash,dto.CurrentPassword);

            if(verificationResult == PasswordVerificationResult.Failed) return BadRequest(new{
                
                message= "Current password is incorrect."
            });

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.NewPassword);
            _dbContext.SaveChanges();

            return Ok(new   
            {
                Message = "Password changed successfully.",
                userId = user.Id
            });
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);
            if(user == null) return Unauthorized();
            return Ok(
                new {
                   firstName = user.FirstName,
                   lastName =user.LastName,
                   email = user.Email,
                   phoneNumber = user.PhoneNumber

                });

        }


    }
}
