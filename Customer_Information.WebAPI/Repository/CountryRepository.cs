using Customer_Information.WebAPI.Data;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Repository.IRepository;

namespace Customer_Information.WebAPI.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly CustomerDBContext _context;
        public CountryRepository(CustomerDBContext context): base(context)
        {
            _context= context;
        }

        public async Task<Country> Update(Country entity)
        {
            _context.Countries.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
