using System.Net;

namespace PortfolioManagerAPI.Models
{
    public class ResponseAPI
    {
        public ResponseAPI()
        {
            ErrorMessages = [];
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public bool Result { get; set; }
    }
}
