using Customer_Information.View.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Customer_Information.View.Models.ViewModel
{
    public class CountryDtoVM
    {
        public CountryDtoVM()
        {
            Country = new CountryDto();
        }
        public CountryDto Country { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CountryList { get; set; }
    }
}
