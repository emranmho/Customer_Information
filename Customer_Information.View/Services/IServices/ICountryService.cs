using Customer_Information.View.Models.Dto;
using System.Linq.Expressions;

namespace Customer_Information.View.Services.IServices
{
    public interface ICountryService
    {
        Task<T> GetAll<T>();
        Task<T> Get<T>(int id);
        Task<T> Create<T>(CountryDto entity);
        Task<T> Remove<T>(int id);
        Task<T> Update<T>(CountryDto entity);
    }
}
