using AutoMapper;
using Customer_Information.WebAPI.Models.Dto;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Customer_Information.WebAPI.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public CustomerController(ICustomerRepository customerRepo, ICountryRepository countryRepository, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _countryRepo= countryRepository;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetCustomerAllInfo()
        {
            try
            {
                IEnumerable<Customer> customerList = await _customerRepo.GetAll(includeProperties: "Country");
                _response.Result = _mapper.Map<List<CustomerDto>>(customerList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetCustomerInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetCustomerInfo(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var customer = await _customerRepo.Get(x => x.Id == id, includeProperties: "Country");
                if (customer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerDto>(customer);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }
        public class Demo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateCustomerInfo([FromBody] CustomerDto obj)
        {
            try
            {
                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                //custom validation
                if (await _customerRepo.Get(x => x.CustomerName.ToLower() == obj.CustomerName.ToLower() && x.FatherName.ToLower() == obj.FatherName.ToLower()) != null)
                {
                    ModelState.AddModelError("Error", "Customer Information is already Exsisted.. !");
                    return BadRequest(ModelState);
                }
                var country = await _countryRepo.Get(c => c.Id == obj.CountryId);
                if (country == null)
                {
                    country = new Country { CountryName = obj.CountryName };
                    await _countryRepo.Create(country);
                }

                Customer customer = _mapper.Map<Customer>(obj);
                customer.CountryId = country.Id;
                await _customerRepo.Create(customer);
                _response.Result = _mapper.Map<CustomerDto>(customer);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCustomerInfo", new { id = customer.Id }, _response);


            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }

        private async Task<byte[]> GetByteArrayAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteCustomerInfo(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var cat = await _customerRepo.Get(x => x.Id == id);
                if (cat == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                await _customerRepo.Remove(cat);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSussess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> UpdateCustomerInfo(int id,[FromBody]CustomerDto obj)
        {
            try
            {
                if (obj == null || id != obj.Id)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                var existingCustomer = await _customerRepo.Get(x => x.CustomerName.ToLower() == obj.CustomerName.ToLower() && x.FatherName.ToLower() == obj.FatherName.ToLower());
                if (existingCustomer != null && existingCustomer.Id != obj.Id)
                {
                    ModelState.AddModelError("Error", "Customer Information is already Exsisted.. !");
                    return BadRequest(ModelState);
                }
                //var country = await _countryRepo.Get(c => c.CountryName.ToLower() == obj.CountryName.ToLower());
                //if (country == null)
                //{
                //    country = new Country { CountryName = obj.CountryName };
                //    await _countryRepo.Create(country);
                //}
                Customer customer = _mapper.Map<Customer>(obj);
                //customer.CountryId = country.Id;
                //if (obj.CustomerPhotoFile != null)
                //{
                //    customer.CustomerPhoto = GetByteArrayAsync(obj.CustomerPhotoFile).Result;
                //}

                await _customerRepo.Update(customer);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSussess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
