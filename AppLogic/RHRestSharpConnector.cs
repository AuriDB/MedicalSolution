using DTO;
using Newtonsoft.Json;
using RestSharp;

namespace AppLogic
{
    public interface IRHRestSharpConnector
    {
        Task<List<Employee>> GetAllEmployeesRestSharp();
    }

    public class RHRestSharpConnector : IRHRestSharpConnector
    {
        private const string _baseUrl = "https://rh-central.azurewebsites.net";

        public async Task<List<Employee>> GetAllEmployeesRestSharp()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("/api/RH/GetAllEmployees", Method.Get);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful && response.Content != null)
            {
                return JsonConvert.DeserializeObject<List<Employee>>(response.Content) ?? new List<Employee>();
            }

            return new List<Employee>();
        }
    }
}
