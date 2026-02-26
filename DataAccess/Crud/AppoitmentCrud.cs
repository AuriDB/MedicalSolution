using DataAccess.Dao;
using DataAccess.Mappers;
using DTO;

namespace DataAccess.Crud
{
    public class AppointmentCrud : CrudFactory
    {
        AppointmentMapper _mapper;

        public AppointmentCrud()
        {
            _mapper = new AppointmentMapper();
            _sqlDao = SqlDao.GetInstance();
        }

        public override void Create(BaseClass dto) //override del metodo Create de CrudFactory, recibe un objeto del tipo BaseClass, que es el tipo base de todos los DTOs, y lo convierte a un objeto del tipo AppointmentDTO para luego crear la operacion SQL y ejecutarla
        {
            var operation = _mapper.GetCreateStatement(dto);
            _sqlDao.ExecuteProcedure(operation);
        }

        public override void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveById<T>(int pId)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveAllByPatientId<T>(int patientId)
        {

            var operation = _mapper.GetRetrieveAllByPatientIdStatement(patientId); // paso 1 pedir la operacion al mapper
            var results = _sqlDao.ExecuteProcedureWithQuery(operation); // paso 2 ejecutar la operacion y obtener los resultados

            //paso 3 convertir los resultados a una lista de objetos del tipo T
            var resultList = new List<T>();
            if (results.Count > 0)
            {
                var dtoList = _mapper.BuildObjects(results); // aca le devuelve la lista de BaseClass
                foreach (var item in dtoList)
                {
                    resultList.Add((T)Convert.ChangeType(item, typeof(T)));
                }
            }

            return resultList;

        }
    }
}