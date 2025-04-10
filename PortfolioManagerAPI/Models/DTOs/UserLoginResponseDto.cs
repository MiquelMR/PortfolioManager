namespace PortfolioManagerAPI.Models.DTOs
{
    public class UserLoginResponseDto
    {
        public UserLoginDto UserLoginDto { get; set; }
        public string Token { get; set; }
    }
}
