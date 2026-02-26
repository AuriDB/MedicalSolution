using DTO;
using AppLogic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientManager _patientManager;

        public PatientsController(IPatientManager ipmanager)
        {
            _patientManager = ipmanager;
        }


        [HttpGet("Get")]
        public string GetPatient()
        {
            return _patientManager.GetPatient();
        }

        [HttpGet("GetAll")]

        public List<Patient> GetAllPatient()
        {
            return _patientManager.GetAllPatient();

        }

        [HttpGet("GetByDoctor")]

        public string GetPatientByDoctor(int pIdDoctor)
        {

            return _patientManager.GetPatientByDoctor(pIdDoctor);
        }
    }
}
