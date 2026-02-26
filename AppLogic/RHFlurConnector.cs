using DTO;
using Flurl.Http;
using Newtonsoft.Json;

namespace AppLogic
{
    public interface IRHFlurConnector
    {
        Task<List<Employee>> GetAllEmployeesFlur();
    }

    public class RHFlurConnector : IRHFlurConnector
    {
        private const string _baseUrl = "https://rh-central.azurewebsites.net";

        public async Task<List<Employee>> GetAllEmployeesFlur()
        {
            string serviceUrl = $"{_baseUrl}/api/RH/GetAllEmployees";

            string result = await serviceUrl.GetStringAsync();

            return JsonConvert.DeserializeObject<List<Employee>>(result) ?? new List<Employee>();
        }
    }
}
