using Customer_Information.View.Models;
using Customer_Information.View.Models.Dto;
using Customer_Information.View.Services.IServices;
using static Utility.SD;

namespace Customer_Information.View.Services
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string informationUrl;
        public CountryService(IHttpClientFactory httpClientFactory,IConfiguration configuration): base(httpClientFactory)
        {
            _clientFactory= httpClientFactory;
            informationUrl = configuration.GetValue<string>("ServiceUrls:InformationAPI");
        }
        public Task<T> Create<T>(CountryDto entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = entity,
                Url= informationUrl+ "/api/Country"
            });
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/Country/" + id
            });
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/Country"
            });
        }

        public Task<T> Remove<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = informationUrl + "/api/Country/" + id
            });
        }

        public Task<T> Update<T>(CountryDto entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = entity,
                Url = informationUrl + "/api/Country/" + entity.Id
            });
        }
    }
}
