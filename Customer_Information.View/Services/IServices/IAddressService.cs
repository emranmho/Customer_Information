using Customer_Information.View.Models.Dto;
using System.Linq.Expressions;

namespace Customer_Information.View.Services.IServices
{
    public interface IAddressService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> GetCustomerAddress<T>(int customerId);
        Task<T> Create<T>(CustomerAddressDto entity);
        Task<T> Remove<T>(int id);
        Task<T> Update<T>(CustomerAddressDto entity);
    }
}
