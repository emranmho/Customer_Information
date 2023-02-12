using Customer_Information.View.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customer_Information.View.Models.ViewModel
{
    public class CustomerDtoVM
    {
        public CustomerDtoVM()
        {
            Customer = new CustomerDto();
            Country = new CountryDto();
            Address = new CustomerAddressDto();
        }
        public CustomerDto Customer { get; set; }
        [ValidateNever]
        public CountryDto Country { get; set; }
        [ValidateNever]
        public CustomerAddressDto Address { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CountryList { get; set; }
        [ValidateNever]
        public IEnumerable<CustomerDto> Customerlist { get; set; }
    }
}
