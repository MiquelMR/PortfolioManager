namespace PortfolioManagerWASM.Models
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
