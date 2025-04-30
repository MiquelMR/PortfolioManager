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
        protected ResponseAPI _responseApi = new();

        // [Authorize]
        [HttpGet("by-email/{email}", Name = "GetUserByEmail")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var userDTO = await _userService.GetByEmailAsync(email);
            if (userDTO == null) { return NotFound(); }
            return Ok(userDTO);
        }

        // [Authorize]
        [HttpPatch("update")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdateDto)
        {
            bool exists = await _userService.ExistsByEmailAsync(userUpdateDto.Email);
            if (!exists)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("User don't exists");
                return BadRequest(_responseApi);
            }

            var success = await _userService.UpdateAsync(userUpdateDto);
            if (!success)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Error during register");
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            return Ok(_responseApi);
        }

        // [Authorize]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { message = "Email cannot be null or empty." });
            }
            if (!await _userService.ExistsByEmailAsync(email))
            {
                _responseApi.StatusCode = HttpStatusCode.NotFound;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("User not found with the given email");
                return BadRequest(_responseApi);
            }

            var success = await _userService.DeleteByEmailAsync(email);
            if (!success)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Server error");
                return BadRequest(_responseApi);
            }

            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool alreadyExists = await _userService.ExistsByEmailAsync(userRegisterDto.Email);
            if (alreadyExists)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Email already exists");
                return BadRequest(_responseApi);
            }

            var success = await _userService.RegisterAsync(userRegisterDto);
            if (!success)
            {
                _responseApi.StatusCode = HttpStatusCode.InternalServerError;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Error during register");
                return StatusCode((int)HttpStatusCode.InternalServerError, _responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            return Ok(_responseApi);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var responseLogin = await _userService.LoginAsync(userLoginDto);
            if (responseLogin == null || string.IsNullOrEmpty(responseLogin.Token))
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("The email or password is incorrect");
                return BadRequest(_responseApi);
            }

            _responseApi.StatusCode = HttpStatusCode.OK;
            _responseApi.IsSuccess = true;
            _responseApi.Result = responseLogin;
            return Ok(_responseApi);
        }
    }
}
