using AutoMapper;
using Customer_Information.WebAPI.Models;
using Customer_Information.WebAPI.Models.Dto;
using Customer_Information.WebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customer_Information.WebAPI.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepo;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public CountryController(ICountryRepository countryRepo,IMapper mapper)
        {
            _countryRepo= countryRepo;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetCountriesAll()
        {
            try
            {
                IEnumerable<Country> countryList = await _countryRepo.GetAll();
                _response.Result = _mapper.Map<List<CountryDto>>(countryList);
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


        [HttpGet("{id:int}",Name = "GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetCountry(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var country = await _countryRepo.Get(x => x.Id == id);
                if (country == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CountryDto>(country);
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
        public async Task<ActionResult<ApiResponse>> CreateCountry([FromBody] CountryDto obj)
        {
            try
            {
                //custom validation  
                if (await _countryRepo.Get(x => x.CountryName.ToLower() == obj.CountryName.ToLower()) != null)
                {
                    ModelState.AddModelError("Error", "Name is already Exsisted.. !");
                    return BadRequest(ModelState);
                }
                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                Country country = _mapper.Map<Country>(obj);

                await _countryRepo.Create(country);
                _response.Result = _mapper.Map<CountryDto>(country);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCountry", new { id = country.Id }, _response);
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
        public async Task<ActionResult<ApiResponse>> DeleteCountry(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSussess = false;
                    return BadRequest(_response);
                }
                var cat = await _countryRepo.Get(x => x.Id == id);
                if (cat == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                await _countryRepo.Remove(cat);
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
        public async Task<ActionResult<ApiResponse>> UpdateCountry(int id, [FromBody] CountryDto obj)
        {
            try
            {
                if (await _countryRepo.Get(x => x.CountryName.ToLower() == obj.CountryName.ToLower()) != null)
                {
                    ModelState.AddModelError("Error", "Name is already Exsisted.. !");
                    return BadRequest(ModelState);
                }
                if (obj == null || id != obj.Id)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSussess = false;
                    return NotFound(_response);
                }
                Country country = _mapper.Map<Country>(obj);

                await _countryRepo.Update(country);
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
