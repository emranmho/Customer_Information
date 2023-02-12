using Customer_Information.WebAPI.Data;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Repository.IRepository;

namespace Customer_Information.WebAPI.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly CustomerDBContext _context;
        public CustomerRepository(CustomerDBContext context): base(context)
        {
            _context= context;
        }

        public async Task<Customer> Update(Customer entity)
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
