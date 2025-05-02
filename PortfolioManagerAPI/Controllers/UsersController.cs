using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs.UserDto;
using PortfolioManagerAPI.Service.IService;
using System.Net;

namespace PortfolioManagerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        // [Authorize]
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (email == null) { return BadRequest(); }
            var userDTO = await _userService.GetUserByEmailAsync(email) ?? new();
            return Ok(userDTO);
        }

        // [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdatePublicProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(); }
            if (!await _userService.ExistsByEmailAsync(userUpdateDto.Email)) { return BadRequest(new()); }
            var userDto = await _userService.UpdateUserAsync(userUpdateDto) ?? new();
            return Ok(userDto);
        }

        // [Authorize]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteByEmail(string email)
        {
            if (email == null) { return BadRequest(); }

            if (!await _userService.ExistsByEmailAsync(email)) { return BadRequest(new()); }
            var success = await _userService.DeleteUserByEmailAsync(email);
            return Ok(success);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(); }
            if (await _userService.ExistsByEmailAsync(userRegisterDto.Email)) { return BadRequest(new()); }
            var userDto = await _userService.RegisterUserAsync(userRegisterDto);
            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(); }
            var responseLogin = await _userService.LoginUserAsync(userLoginDto);
            if (string.IsNullOrEmpty(responseLogin.Token)) { return BadRequest(responseLogin); }
            return Ok(responseLogin);
        }
    }
}
