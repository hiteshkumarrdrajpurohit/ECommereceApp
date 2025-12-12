using EcommerceApp.DTO;
using EcommerceApp.Model;
using EcommerceApp.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Numerics;
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
        public IActionResult ChangePassword( [FromBody] ChangePasswordDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId && u.IsActive ==true);
            
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

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId && u.IsActive == true);
            if(user == null) return Unauthorized("User Not found");
            return Ok(
                new {
                   firstName = user.FirstName,
                   lastName =user.LastName,
                   email = user.Email,
                   phoneNumber = user.PhoneNumber

                });

        }

        [HttpPost("address")]

        public IActionResult AddAddress([FromBody]AddressDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId && u.IsActive == true);

            var existingAddress = _dbContext.Addresses.FirstOrDefault(a => a.Name == dto.Name &&
                a.Phone == dto.Phone &&
                a.Street == dto.Street &&
                a.City == dto.City &&
                a.State == dto.State &&
                a.PostalCode == dto.PostalCode &&
                a.Country == dto.Country);

            if (user == null) return Unauthorized("User Not Found");

            if (existingAddress == null)
            {
                var address = new Address
                {
                    Name = dto.Name,
                    Phone = dto.Phone,
                    Street = dto.Street,
                    City = dto.City,
                    State = dto.State,
                    PostalCode = dto.PostalCode,
                    Country = dto.Country
                };

                address.Users.Add(user);
                user.Addresses.Add(address);

                _dbContext.SaveChanges();
                return Ok(address);
            }

            else
            {
                existingAddress.Users.Add(user);
                user.Addresses.Add(existingAddress);
                _dbContext.SaveChanges();
                return Ok(existingAddress);
            }
           

        }

        [HttpPut("address")]
        public IActionResult UpdateAddress([FromQuery] int id, AddressDTO dto ){

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = _dbContext.Users.FirstOrDefault(u => u.Id.ToString() == userId && u.IsActive == true);

            var exsitingAddress = _dbContext.Addresses.FirstOrDefault(a => a.Id == id);

            if (exsitingAddress ==null) return NotFound("Address Not Found");

            exsitingAddress.Users.Remove(user);
            user.Addresses.Remove(exsitingAddress);


            var address = new Address
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                PostalCode = dto.PostalCode,
                Country = dto.Country
            };

            address.Users.Add(user);
            user.Addresses.Add(address);

            _dbContext.SaveChanges();

            return Ok("Address Updated Successfully");
        }

        [HttpDelete("address")]
        public IActionResult DeleteAddress([FromQuery] int id)
        {
            var address = _dbContext.Addresses.FirstOrDefault(a => a.Id == id);

            if (address ==null) return NotFound("Address Not Found");
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _dbContext.Users.FirstOrDefault(u => u.Id ==id);

            if (user ==null) return NotFound("User Not Found");

            address.Users.Remove(user);
            user.Addresses.Remove(address);

            _dbContext.SaveChanges();

            return Ok("Addrees Deleted Successfully ");
        }

        [HttpGet("address/all")]

        public  async Task<IActionResult> GetAllAddress([FromQuery] PagingParameters paging)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId ==null) return Unauthorized();

            var query = _dbContext.Users
       .Where(u => u.Id == int.Parse(userId))
       .SelectMany(u => u.Addresses)    
       .AsNoTracking()
       .OrderBy(a => a.Id);    


            var pagedResult = await query
                            .ToPagedResponseAsync(
                             paging.Page,
                             paging.PageSize,
                             add => new ResponseAddressDTO
                             {
                                 Id = add.Id,
                                 Name = add.Name,
                                 Phone = add.Phone,
                                 Street = add.Street,
                                 City = add.City,
                                 State = add.State,
                                 PostalCode = add.PostalCode,
                                 Country = add.Country
                             });
            return Ok(pagedResult);





        }

    }
}
