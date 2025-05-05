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
        [HttpGet("by-email/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: user email is null", null));
            var exists = await _userService.ExistsByEmailAsync(userEmail);
            if (!exists)
                return NotFound(new ResponseAPI<object>(404, "Asset not found", null));

            var userDto = await _userService.GetUserByEmailAsync(userEmail);
            if (userDto == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error", null));

            return Ok(new ResponseAPI<UserDto>(200, "User created successfully", userDto));
        }

        // [Authorize]
        [HttpPatch]
        public async Task<IActionResult> UpdatePublicProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: userUpdateDto is null", null));


            var userExists = await _userService.ExistsByEmailAsync(userUpdateDto.Email);
            if (!userExists)
                return StatusCode(404, new ResponseAPI<object>(404, "User not found", null));

            var userDto = await _userService.UpdateUserAsync(userUpdateDto);
            if (userDto == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: User update failed", null));

            return Ok(new ResponseAPI<UserDto>(200, "User updated successfully", userDto));
        }

        // [Authorize]
        [HttpDelete("{userEmail}")]
        public async Task<IActionResult> DeleteByEmail(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: email is null", null));
            var userExists = await _userService.ExistsByEmailAsync(userEmail);
            if (!userExists)
                return StatusCode(404, new ResponseAPI<object>(404, "User not found", null));

            var success = await _userService.DeleteUserByEmailAsync(userEmail);
            if (!success)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: User deletion failed", null));

            return Ok(new ResponseAPI<UserDto>(200, "Success", null));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (userRegisterDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: userRegisterDto is null", null));

            var userExists = await _userService.ExistsByEmailAsync(userRegisterDto.Email);
            if (userExists)
                return StatusCode(404, new ResponseAPI<object>(404, "User already exists", null));

            var userDto = await _userService.RegisterUserAsync(userRegisterDto);
            if (userDto == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: User registration failed", null));

            return Ok(new ResponseAPI<UserDto>(200, "Success", userDto));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
                return BadRequest(new ResponseAPI<object>(400, "Invalid request: userLoginDto is null", null));

            var responseLogin = await _userService.LoginUserAsync(userLoginDto);
            if (responseLogin == null)
                return StatusCode(500, new ResponseAPI<object>(500, "Internal server error: User login failed", null));

            if (string.IsNullOrEmpty(responseLogin.Token))
                return StatusCode(401, new ResponseAPI<object>(401, "Invalid credentials : Login failed", null));

            return Ok(new ResponseAPI<UserLoginResponseDto>(200, "Authorized", responseLogin));
        }
    }
}
