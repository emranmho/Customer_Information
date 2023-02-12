using Customer_Information.View.Models;
using Customer_Information.View.Models.Dto;
using Customer_Information.View.Services.IServices;
using static Utility.SD;

namespace Customer_Information.View.Services
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string informationUrl;
        public AddressService(IHttpClientFactory httpClientFactory,IConfiguration configuration): base(httpClientFactory)
        {
            _clientFactory= httpClientFactory;
            informationUrl = configuration.GetValue<string>("ServiceUrls:InformationAPI");
        }
        public Task<T> Create<T>(CustomerAddressDto entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = entity,
                Url= informationUrl+ "/api/CustomerAddress"
            });
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/CustomerAddress/" + id
            });
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/CustomerAddress"
            });
        }

        public Task<T> GetCustomerAddress<T>(int customerId)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/CustomerAddress/GetCustomerAddress/" + customerId
            });
        }

        public Task<T> Remove<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = informationUrl + "/api/CustomerAddress/" + id
            });
        }

        public Task<T> Update<T>(CustomerAddressDto entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = entity,
                Url = informationUrl + "/api/CustomerAddress/" + entity.Id
            });
        }
    }
}
