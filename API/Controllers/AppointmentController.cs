using AppLogic;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [EnableCors("DemoPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentManager _appointmentManager;

        public AppointmentController(IAppointmentManager appManager)
        {
            _appointmentManager = appManager;
        }

        [HttpPost("CrearCita")]
        public ApiResponse CreateAppointment(Appointment dto)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _appointmentManager.CreateAppointment(dto);
                response.Result = "ok";
            }
            catch (Exception ex)
            {
                response.Result = "error";
                response.Message = ex.Message;
            }
            return response;
        }


        [HttpPost("GetAppointmentByPatientId")]
        public ApiResponse GetAppointmentByPatientId(int patientId)
        {
            var response = new ApiResponse();
            try
            {
                response.Data = _appointmentManager.GetAppointmentByPatientId(patientId);
                response.Result = "ok";
            }
            catch (Exception ex)
            {
                response.Result = "error";
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
