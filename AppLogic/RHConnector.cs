using DTO;
using Newtonsoft.Json;

namespace AppLogic
{
    public interface IRHConnector
    {
        Task<List<Employee>> RetrieveAllEmployees();
        Task<List<string>> RetrieveAllSpecialties();

        // Practica 1 Metodos 
        Task<Employee?> GetEmployeeManager(int employeeId);
        Task<List<Employee>> GetOldestEmployee();
        Task<List<Employee>> GetNewestEmployee();
        Task<Employee?> GetEmployeeById(int id);
        Task<List<Employee>> GetEmployeesWithMoreThan(int years);
        Task<List<Employee>> GetEmployeesWithLessThan(int years);
    }

    // (Personal: Esto es de la clase *ojo*)
    public class RHConnector : IRHConnector
    {
        private static HttpClient _httpClient;
        private const string _baseUrl = "https://rh-central.azurewebsites.net/";

        public RHConnector()
        {
            if (_httpClient is null)
            {
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_baseUrl),
                    Timeout = TimeSpan.FromSeconds(15)
                };
            }
        }

        public async Task<List<Employee>> RetrieveAllEmployees()
        {
            string serviceUrl = "/api/RH/GetAllEmployees";
            string result = await InvokeGetAsync(serviceUrl);
            var dtoEmployees = JsonConvert.DeserializeObject<List<Employee>>(result);

            return dtoEmployees ?? new List<Employee>();
        }

        public async Task<List<string>> RetrieveAllSpecialties()
        {
            string serviceUrl = "/api/RH/GetSpecialties";
            string result = await InvokeGetAsync(serviceUrl);
            var specialtiesStrings = JsonConvert.DeserializeObject<List<string>>(result);

            return specialtiesStrings ?? new List<string>();
        }

        // Metros de la Practica 1 

        // Da los datos de su manager

        public async Task<Employee?> GetEmployeeManager(int employeeId)
        {
            var employees = await RetrieveAllEmployees();

            var employee = employees.FirstOrDefault(e => e.Id == employeeId);
            if (employee == null || employee.ManagerId == null)
                return null;

            return employees.FirstOrDefault(e => e.Id == employee.ManagerId);
        }

        // Da los empleados por anios
        public async Task<List<Employee>> GetOldestEmployee()
        {
            var employees = await RetrieveAllEmployees();

            var valid = employees
                .Where(e => !string.IsNullOrEmpty(e.HiringDate))
                .Select(e => new { Employee = e, Fecha = DateTime.Parse(e.HiringDate) })
                .ToList();

            if (!valid.Any()) return new List<Employee>();

            var minDate = valid.Min(e => e.Fecha);
            return valid.Where(e => e.Fecha == minDate).Select(e => e.Employee).ToList();
        }

        public async Task<List<Employee>> GetNewestEmployee()
        {
            var employees = await RetrieveAllEmployees();

            var valid = employees
                .Where(e => !string.IsNullOrEmpty(e.HiringDate))
                .Select(e => new { Employee = e, Fecha = DateTime.Parse(e.HiringDate) })
                .ToList();

            if (!valid.Any()) return new List<Employee>();

            var maxDate = valid.Max(e => e.Fecha);
            return valid.Where(e => e.Fecha == maxDate).Select(e => e.Employee).ToList();
        }

        // Da la informacion de un empleado por su id
        public async Task<Employee?> GetEmployeeById(int id)
        {
            var employees = await RetrieveAllEmployees();
            return employees.FirstOrDefault(e => e.Id == id);
        }

        // Da los empleados con MAS años en la compañía que la cantidad indicada.
        public async Task<List<Employee>> GetEmployeesWithMoreThan(int years)
        {
            var employees = await RetrieveAllEmployees();
            var fechaCorte = DateTime.Now.AddYears(-years);

            return employees
                .Where(e => !string.IsNullOrEmpty(e.HiringDate) &&
                            DateTime.Parse(e.HiringDate) <= fechaCorte)
                .ToList();
        }

        public async Task<List<Employee>> GetEmployeesWithLessThan(int years)
        {
            var employees = await RetrieveAllEmployees();
            var fechaCorte = DateTime.Now.AddYears(-years);

            return employees
                .Where(e => !string.IsNullOrEmpty(e.HiringDate) &&
                            DateTime.Parse(e.HiringDate) >= fechaCorte)
                .ToList();
        }

        //   HTTP  
        #region Metodos Helpers

        private async Task<string> InvokeGetAsync(string uri)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.GetAsync(uri);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }

                return responseString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<string> InvokePutAsync(string uri, StringContent content)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.PutAsync(uri, content);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }

                return responseString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<string> InvokePostAsync(string uri, StringContent content)
        {
            try
            {
                string responseString = string.Empty;
                var results = await _httpClient.PostAsync(uri, content);
                if (results.IsSuccessStatusCode)
                {
                    responseString = await results.Content.ReadAsStringAsync();
                }

                return responseString;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Metodos Helpers
    }
}
