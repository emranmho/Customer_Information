using Customer_Information.WebAPI.Models;

namespace Customer_Information.WebAPI.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        
        Task<Customer> Update(Customer entity);
    }
}
