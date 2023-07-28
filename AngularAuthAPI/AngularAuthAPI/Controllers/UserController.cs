using AngularAuthAPI.Context;
using AngularAuthAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authContext;
        public UserController(AppDbContext appDbContext)
        {
            _authContext = appDbContext;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            var user = await _authContext.Users.FirstOrDefaultAsync
                (x => x.UserName == userObj.UserName && x.Password == userObj.Password);

            if (user == null)
            {
                return NotFound(new { Message = "User Not Found!" });
            }
            return Ok(new
            {
                Message = "Login Success!"
            });
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }
            await _authContext.Users.AddAsync(userObj);
            await _authContext.SaveChangesAsync();
            return Ok(new
            {
                Message = "User Registered!"
            });
        }
    }
}




//using AngularAuthAPI.Context;
//using AngularAuthAPI.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AngularAuthAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly AppDbContext _authContext;
//        public UserController(AppDbContext appDbContext)
//        {
//            _authContext = appDbContext;
//        }

//        [HttpPost("authenticate")]
//        public async Task<IActionResult> Authenticate([FromBody] UserCredentials userCredentials)
//        {
//            if (userCredentials == null)
//            {
//                return BadRequest();
//            }

//            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.UserName == userCredentials.UserName && x.Password == userCredentials.Password);

//            if (user == null)
//            {
//                return NotFound(new { Message = "User Not Found!" });
//            }

//            return Ok(new
//            {
//                Message = "Login Success!"
//            });
//        }

//        [HttpPost("Register")]
//        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
//        {
//            if (userObj == null)
//            {
//                return BadRequest();
//            }

//            await _authContext.Users.AddAsync(userObj);
//            await _authContext.SaveChangesAsync();
//            return Ok(new
//            {
//                Message = "User Registered!"
//            });
//        }
//    }

//    public class UserCredentials
//    {
//        [Required]
//        public string UserName { get; set; }

//        [Required]
//        public string Password { get; set; }
//    }
//}
