using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("DemoPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RHConnectorController : ControllerBase
    {
        private readonly IRHConnector _rhConnector;
        private readonly IRHRestSharpConnector _rhRestSharpConnector;
        private readonly IRHFlurConnector _rhFlurConnector;

        public RHConnectorController(
            IRHConnector rhConnector,
            IRHRestSharpConnector rhRestSharpConnector,
            IRHFlurConnector rhFlurConnector)
        {
            _rhConnector = rhConnector;
            _rhRestSharpConnector = rhRestSharpConnector;
            _rhFlurConnector = rhFlurConnector;
        }

        //  Metodos en clasesica (ya existentes)

        [HttpGet("GetAllEmployees")]
        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _rhConnector.RetrieveAllEmployees();
        }

        [HttpGet("GetAllSpecialties")]
        public async Task<List<string>> GetAllSpecialties()
        {
            return await _rhConnector.RetrieveAllSpecialties();
        }

        //  Metodos nuevos (Parte 1 de la practica)

        // Id de empleado, da los datos de su manager.

        [HttpGet("GetEmployeeManager/{employeeId}")]
        public async Task<ActionResult<Employee>> GetEmployeeManager(int employeeId)
        {
            var manager = await _rhConnector.GetEmployeeManager(employeeId);
            if (manager == null)
                return NotFound($"No se encontro manager para el empleado con Id {employeeId}.");
            return Ok(manager);
        }

        // Da los empleados con mas anios

        [HttpGet("GetOldestEmployee")]
        public async Task<List<Employee>> GetOldestEmployee()
        {
            return await _rhConnector.GetOldestEmployee();
        }

        // Da los empleados con menos anios

        [HttpGet("GetNewestEmployee")]
        public async Task<List<Employee>> GetNewestEmployee()
        {
            return await _rhConnector.GetNewestEmployee();
        }

        // Da empleado con el Id indicado

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<ActionResult<Employee?>> GetEmployeeById(int id)
        {
            var employee = await _rhConnector.GetEmployeeById(id);
            if (employee == null)
                return Ok((Employee?)null);
            return Ok(employee);
        }


        // Da empleados con WithMoreThan and LessThan [x] cantidfad de anios

        [HttpGet("GetEmployeesWithMoreThan/{years}")]
        public async Task<List<Employee>> GetEmployeesWithMoreThan(int years)
        {
            return await _rhConnector.GetEmployeesWithMoreThan(years);
        }

        [HttpGet("GetEmployeesWithLessThan/{years}")]
        public async Task<List<Employee>> GetEmployeesWithLessThan(int years)
        {
            return await _rhConnector.GetEmployeesWithLessThan(years);
        }

        //  Librerias adicionales que se mencionaron en la practica (Parte 2 de la practica)

        // Da empleados con RestSharp

        [HttpGet("GetAllEmployeesRestSharp")]
        public async Task<List<Employee>> GetAllEmployeesRestSharp()
        {
            return await _rhRestSharpConnector.GetAllEmployeesRestSharp();
        }

        ///Da empleados con Flurl

        [HttpGet("GetAllEmployeesFlur")]
        public async Task<List<Employee>> GetAllEmployeesFlur()
        {
            return await _rhFlurConnector.GetAllEmployeesFlur();
        }
    }
}
