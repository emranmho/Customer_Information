using Customer_Information.View.Models;

namespace Customer_Information.View.Services.IServices
{
    public interface IBaseService
    {
        ApiResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
