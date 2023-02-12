using Customer_Information.WebAPI.Data;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Repository.IRepository;

namespace Customer_Information.WebAPI.Repository
{
    public class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly CustomerDBContext _context;
        public CustomerAddressRepository(CustomerDBContext context): base(context)
        {
            _context= context;
        }

        public async Task<CustomerAddress> Update(CustomerAddress entity)
        {
            _context.CustomerAddresses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
