using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;
using System.Net;
using XAct;

namespace PortfolioManagerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected ResponseAPI _responseApi;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            this._responseApi = new();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            bool alreadyExists = _userRepository.UserAlreadyExistsByEmail(userRegisterDto.Email);
            if (alreadyExists)
            {
                _responseApi.StatusCode = HttpStatusCode.BadRequest;
                _responseApi.IsSuccess = false;
                _responseApi.ErrorMessages.Add("Email already exists");
                return BadRequest(_responseApi);
            }

            var user = await _userRepository.Register(userRegisterDto);
            if (user == null)
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
            var responseLogin = await _userRepository.Login(userLoginDto);
            if (responseLogin == null || string.IsNullOrEmpty(responseLogin.Token)) {
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

        [Authorize]
        [HttpGet("{UserId:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int UserId)
        {
            var user = _userRepository.GetUserById(UserId);
            if (user == null) { return NotFound(); }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }        
    }
}
