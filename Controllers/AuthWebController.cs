using CarZone_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarZone_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthWebController(SignInManager<User> sm, UserManager<User> um) : ControllerBase
    {
        private readonly SignInManager<User> signInManager = sm;
        private readonly UserManager<User> userManager = um;

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser(User user)
        {
            string message = "";
            IdentityResult result = new();
            try
            {
                User user_ = new User()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedDate = DateTime.Now,
                };
                result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest(result);
                }
                message = "Registered Successfully.";

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message, result });
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(Login login)
        {
            string message = "";

            try
            {
                User user_ = await userManager.FindByEmailAsync(login.Email);

                var result = await signInManager.PasswordSignInAsync(user_, login.Password, login.Rememer, false);

                if (!result.Succeeded)
                {
                    return Unauthorized("Check your login credentials and try again");
                }
                message = "Login Successfully.";

            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "Something went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message });
        }
        [HttpGet("logout"), Authorize]
        public async Task<ActionResult> LogoutUser()
        {
            string message = "";
            try
            {
                await signInManager.SignOutAsync();
            }
            catch (Exception ex) {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message });
        }
        [HttpGet("xhtlekd"), Authorize]
        public async Task<ActionResult> CheckUser()
        {
            string message = "Logged in";
            User currentuser = new();
            try
            {
                var user_ = HttpContext.User;
                var principals = new ClaimsPrincipal(user_);
                var result = signInManager.IsSignedIn(principals);
                if (result)
                {
                    currentuser = await signInManager.UserManager.GetUserAsync(principals);
                }
                else
                {
                    return Forbid("Access Denied");
                }
            }catch (Exception ex) {
                return BadRequest(new { message = "Something went wrong, please try again. " + ex.Message });
            }
            return Ok(new { message ,user = currentuser}); 
        }
    }
}
