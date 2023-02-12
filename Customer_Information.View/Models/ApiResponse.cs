using System.Net;

namespace Customer_Information.View.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessage = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; } 
        public bool IsSussess { get; set; } = true;
        public List<string> ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}
