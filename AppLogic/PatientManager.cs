using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLogic
{

    public interface IPatientManager
    {
        List<Patient> GetAllPatient();
        string GetPatientByDoctor(int pIdDoctor);
        string GetPatient();
    }


    public class PatientManager : IPatientManager
    {
        public List<Patient> GetAllPatient()
        {
            var patients = new List<Patient>();

            /*
            patients.Add(new Patient() { name = "Erick" });
            patients.Add(new Patient() { name = "Bryan" });
            patients.Add(new Patient() { name = "David" });
            */

            return patients;

        }

        public string GetPatientByDoctor(int pIdDoctor)
        {
            return $"Datos de los pacientes del doctor: {pIdDoctor}.";
        }

        public string GetPatient()
        {
            return "Datos del paciente.";
        }
    }
}
