using Customer_Information.WebAPI.Models;

namespace Customer_Information.WebAPI.Repository.IRepository
{
    public interface ICustomerAddressRepository : IRepository<CustomerAddress>
    {
        Task<CustomerAddress> Update(CustomerAddress entity);
    }
}
