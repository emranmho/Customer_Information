using static Utility.SD;

namespace Customer_Information.View.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
