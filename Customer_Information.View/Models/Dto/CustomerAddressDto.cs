using System.ComponentModel.DataAnnotations.Schema;

namespace Customer_Information.View.Models.Dto
{
    public class CustomerAddressDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
    }
}
