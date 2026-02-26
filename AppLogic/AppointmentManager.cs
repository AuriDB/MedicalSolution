using DataAccess.Crud;
using DTO;

namespace AppLogic
{
    public interface IAppointmentManager
    {
        string CreateAppointment(Appointment appointment);
        List<Appointment> GetAppointmentByPatientId(int patientId);
    }
    public class AppointmentManager : IAppointmentManager
    {
        public string CreateAppointment(Appointment appointment)
        {
            var appointmentCrud = new AppointmentCrud();
            appointmentCrud.Create(appointment);
            return "Cita registrada de manera correcta";
        }

        public List<Appointment> GetAppointmentByPatientId(int patientId)
        {
            var appointmentCrud = new AppointmentCrud();
            return appointmentCrud.RetrieveAllByPatientId<Appointment>(patientId);
        }
    }
}
