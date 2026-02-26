using DataAccess.Dao;
using DataAccess.Mappers;
using DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Crud
{
    public class PatientCrud : CrudFactory
    {
        PatientMapper _mapper; //Nos apoyamos para llamar al mapper, para construir los objetos y las operaciones sql

        public PatientCrud()
        {
            _mapper = new PatientMapper();
            _sqlDao = SqlDao.GetInstance();
        }
        public override void Create(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var operation = _mapper.GetRetrieveAllStatement(); // paso 1 pedir la operacion al mapper (del crud al mapper)
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

        public override List<T> RetrieveById<T>(int pId)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}
