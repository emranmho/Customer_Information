using Customer_Information.View.Models;
using Customer_Information.View.Models.Dto;
using Customer_Information.View.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Customer_Information.View.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        public CountryController(ICountryService countryService)
        {
            _countryService= countryService;
        } 
        public async Task<IActionResult> IndexCountry()
        {
            List<CountryDto> list = new();
            var response = await _countryService.GetAll<ApiResponse>();
            if(response != null && response.IsSussess)
            {
                list = JsonConvert.DeserializeObject<List<CountryDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
