using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.DTO.user;
using MovieAPI.Interfaces;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly IJwtToken _token;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(UserManager<AppUser> user, IJwtToken token, SignInManager<AppUser> signInManager)
        {
            this._signInManager = signInManager;
            this._token = token;
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
                        return Ok("User Registered Successfully!");
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


        [HttpPost("login")]

        public async Task <IActionResult> LoginUser([FromBody] UserLoginDto userLoginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManger.Users.FirstOrDefaultAsync(x=> x.UserName == userLoginDto.Username.ToLower());

            if(user == null)
                return Unauthorized("Invalid Username!");

            var result  = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);
            
            if(!result.Succeeded)
                return Unauthorized("Username or Password is incorrect!");

            return Ok(new 
            { 
                Message = "Login Successful!",
                Token = _token.GenerateToken(user) 
            });
        } 
    }
}