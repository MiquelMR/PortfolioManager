namespace PortfolioManagerWASM.Models
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
    }
}
