using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebTutorial.Dtos.Account;
using WebTutorial.Model;

namespace WebTutorial.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        public AccountController(UserManager<AppUser> _userManager)
        {
            userManager = _userManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var create = await userManager.CreateAsync(user, registerDto.Password);

                if (create.Succeeded)
                {
                    var role = await userManager.AddToRoleAsync(user, "User");
                    if (role.Succeeded)
                    {
                        return Ok("tạo thành công");
                    }
                    else
                        return StatusCode(500, role.Errors);
                }
                else
                    return StatusCode(500, create.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex+"lỗi rồi");
            }
        }
    }
}
