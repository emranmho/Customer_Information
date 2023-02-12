using Customer_Information.View.Models.Dto;
using System.Linq.Expressions;

namespace Customer_Information.View.Services.IServices
{
    public interface ICustomerService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(CustomerDto entity);
        Task<T> Remove<T>(int id);
        Task<T> Update<T>(CustomerDto entity);
    }
}
