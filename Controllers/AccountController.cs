using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mini_Project_Manager.DTOs;
using Mini_Project_Manager.Models;

namespace Mini_Project_Manager.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController(UserManager<User> _userManager) : ControllerBase
    {
        private readonly UserManager<User> userManager = _userManager;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var userExists = await userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
                return BadRequest(new { Message = "Email already in use." });

            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Name = registerDto.Name
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "User registered successfully" });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserDTO>> GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await userManager.FindByIdAsync(userId!);

            if (user == null)
                return NotFound(new { Message = "User not found." });

            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email!
            };
        }
    }
}