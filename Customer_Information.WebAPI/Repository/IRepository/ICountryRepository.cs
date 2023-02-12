using Customer_Information.WebAPI.Models;

namespace Customer_Information.WebAPI.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country> Update(Country entity);
    }
}
