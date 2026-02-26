using Microsoft.Data.SqlClient;

namespace DataAccess.Dao
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> Parameters { get; set; }

        public SqlOperation()
        {
            Parameters = new List<SqlParameter>();
        }

        //metodos para agregar los distintos tipos de parametros

        public void AddVarcharParam(string parameterName, string parameterValue)
        {
            Parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
        public void AddIntParam(string parameterName, int parameterValue)
        {
            Parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
        public void AddDatetimeParam(string parameterName, DateTime parameterValue)
        {
            Parameters.Add(new SqlParameter("@" + parameterName, parameterValue));
        }
    }
}