using AutoMapper;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Models.Dto;
using Customer_Information.WebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customer_Information.WebAPI.Controllers
{
    [Route("api/CustomerAddress")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ICustomerAddressRepository _addressRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public AddressController(ICustomerAddressRepository addressRepo,IMapper mapper)
        {
            _addressRepo=addressRepo;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetAddressAll()
        {
            try
            {
                IEnumerable<CustomerAddress> addressList = await _addressRepo.GetAll();
                _response.Result = _mapper.Map<List<CustomerAddressDto>>(addressList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }


        [HttpGet("GetCustomerAddress/{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetCustomerAddress(int customerId)
        {
            try
            {
                if (customerId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var customerAddress = await _addressRepo.Get(u => u.CustomerId == customerId);
                if (customerAddress == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerAddressDto>(customerAddress);
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

        [HttpGet("{id:int}",Name = "GetAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetAddress(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var address = await _addressRepo.Get(x => x.Id == id);
                if (address == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerAddressDto>(address);
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateAddress([FromBody] CustomerAddressDto obj)
        {
            try
            {
                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                CustomerAddress address = _mapper.Map<CustomerAddress>(obj);

                await _addressRepo.Create(address);
                _response.Result = _mapper.Map<CustomerAddressDto>(address);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAddress", new { id = address.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSussess = false;
                _response.ErrorMessage
                    = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteAddress(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var cat = await _addressRepo.Get(x => x.Id == id);
                if (cat == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                await _addressRepo.Remove(cat);
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
        public async Task<ActionResult<ApiResponse>> UpdateCountry(int id, [FromBody] CustomerAddressDto obj)
        {
            try
            {
                if (obj == null || id != obj.Id)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                CustomerAddress adddress = _mapper.Map<CustomerAddress>(obj);

                await _addressRepo.Update(adddress);
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
