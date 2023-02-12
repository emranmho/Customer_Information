using AutoMapper;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Models.Dto;

namespace Customer_Information.WebAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Country, CountryDto>().ReverseMap(); // country map

            CreateMap<Customer, CustomerDto>().ReverseMap(); // customer map
            CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap(); // address map

        }
    }
}
