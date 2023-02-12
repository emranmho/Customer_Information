using Customer_Information.View.Models;
using Customer_Information.View.Models.Dto;
using Customer_Information.View.Services.IServices;
using static Utility.SD;

namespace Customer_Information.View.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string informationUrl;
        public CustomerService(IHttpClientFactory httpClientFactory,IConfiguration configuration): base(httpClientFactory)
        {
            _clientFactory= httpClientFactory;
            informationUrl = configuration.GetValue<string>("ServiceUrls:InformationAPI");
        }
        public Task<T> Create<T>(CustomerDto entity)
        {

            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.POST,
                Data = entity,
                Url = informationUrl + "/api/Customer"
            });
        }

        public Task<T> Get<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/Customer/" + id
            });
        }

        public Task<T> GetAll<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.GET,
                Url = informationUrl + "/api/Customer"
            });
        }

        public Task<T> Remove<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.DELETE,
                Url = informationUrl + "/api/Customer/"+id
            });
        }

        public Task<T> Update<T>(CustomerDto entity)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.PUT,
                Data = entity,
                Url = informationUrl + "/api/Customer/"+entity.Id
            });
        }
    }
}
