using Customer_Information.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer_Information.WebAPI.Data
{
    public class CustomerDBContext : DbContext
    {
        public CustomerDBContext(DbContextOptions<CustomerDBContext> options)
           : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}
