using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        [HttpGet]
        public string GetDoctor()
        {
            return "Datos del doctor.";
        }

        [HttpGet]

        public string GetAllPatient()
        {
            return "Datos de todos los doctores.";
        }

        [HttpGet("GetByDoctor")]

        public string GetDoctorById(int pIdDoctor)
        {

            return "Datos  del doctor by doctor" + pIdDoctor;
        }
    }
}
