using DataAccess.Dao;
using DataAccess.Mappers.Interfaces;
using DTO;

namespace DataAccess.Mappers
{
    public class PatientMapper : ICrudStatements
    {
        public BaseClass BuildObject(Dictionary<string, object> row)
        {
            var paciente = new Patient();
            paciente.Id = int.Parse(row[key: "Id"].ToString());
            paciente.SocialSecurityId = row[key: "SocialSecurityId"].ToString();
            paciente.Name = row[key: "Name"].ToString();
            paciente.LastName = row[key: "LastName"].ToString();
            return paciente;
        }

        public List<BaseClass> BuildObjects(List<Dictionary<string, object>> rows)
        {
            var results = new List<BaseClass>();
            foreach (var item in rows)
            {
                var row = BuildObject(item);
                results.Add(row);
            }

            return results;
        }

        public SqlOperation GetCreateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "SP_GET_PATIENTS";
            return operation;
        }

        public SqlOperation GetRetrieveByIdStatement(int pId)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseClass dto)
        {
            throw new NotImplementedException();
        }
    }
}
