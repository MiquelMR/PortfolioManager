namespace PortfolioManagerAPI.Models.DTOs.UserDto
{
    public class UserLoginResponseDto
    {
        public UserLoginDto UserLoginDto { get; set; }
        public string Token { get; set; }
    }
}
