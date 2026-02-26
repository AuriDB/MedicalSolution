namespace DTO
{
    public class Appointment : BaseClass
    {
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Title { get; set; }
        public string Speciality { get; set; }
    }
}