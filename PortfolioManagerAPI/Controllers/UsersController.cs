using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioManagerAPI.Models;
using PortfolioManagerAPI.Models.DTOs;
using PortfolioManagerAPI.Repository.IRepository;

namespace PortfolioManagerAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var userList = _userRepository.GetUsers();
            var userDtoList = new List<UserDto>();
            foreach (var user in userList)
            {
                userDtoList.Add(_mapper.Map<UserDto>(user));
            }
            return Ok(userDtoList);
        }

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

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("User data is invalid");
            }

            var user = _mapper.Map<User>(userDto);

            if (_userRepository.ExistsById(user.UserId))
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(409, ModelState);
            }

            _userRepository.CreateUser(user);
            return CreatedAtRoute("GetUser", new { UserId = user.UserId }, user);
        }

        [HttpPatch("{UserId:int}", Name = "UpdatePatchUser")]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int UserId, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("User data is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("User data is invalid");
            }

            var user = _mapper.Map<User>(userDto);

            if (!_userRepository.ExistsById(UserId))
            {
                ModelState.AddModelError("", "User not found");
                return StatusCode(404, ModelState);
            }

            _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{UserId:int}", Name = "DeleteUser")]
        [ProducesResponseType(201, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int UserId)
        {
            if (!_userRepository.ExistsById(UserId))
            {
                ModelState.AddModelError("", "User not found");
                return StatusCode(404, ModelState);
            }

            var user = _userRepository.GetUserById(UserId);
            _userRepository.DeleteUser(user);
            return NoContent();
        }
    }
}
