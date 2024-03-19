using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieAPI.DTO.user;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManger;
        public UserController(UserManager<AppUser> user)
        {
            this._userManger = user;
        }

        [HttpPost("register")]

        public async Task<IActionResult> RegisterUser([FromBody] UserDto userDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = userDto.Username,
                    Email = userDto.Email
                };

                var createdUser = await _userManger.CreateAsync(appUser, userDto.Password);

                if(createdUser.Succeeded)
                {
                    var roleRes = await _userManger.AddToRolesAsync(appUser, new[] { "User" });
                    if(roleRes.Succeeded)
                        return Ok("User Created");
                    else
                        return BadRequest(roleRes.Errors);
                }
                else
                {
                    return BadRequest(createdUser.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}