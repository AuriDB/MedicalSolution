using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        [HttpGet("GetPatient")]
        public string GetPatient()
        {
            return "Datos del paciente.";
        }

        [HttpGet("GetAllPatients")]

        public List<Patient> GetAllPatient()
        {
            var patients = new List<Patient>();

            patients.Add(new Patient() { name = "Erick" } );
            patients.Add(new Patient() { name = "Bryan" } );
            patients.Add(new Patient() { name = "David" });

            return patients;

        }

        [HttpGet("GetByDoctor")]

        public string GetPatientByDoctor(int pIdDoctor)
        {

            return $"Datos de los pacientes del doctor: {pIdDoctor}.";
        }
    }
}
