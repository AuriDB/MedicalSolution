using Azure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Dao
{
    public class SqlDao 
    {

        private string connectionString = "Server=tcp:app-server-alex.database.windows.net,1433;Initial Catalog=MedicalSolutionBD;Persist Security Info=False;User ID=azeledonov;Password=$Q4w73!K.a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"; // a mi SERVIDOR EN AZURE
        //private string connectionString = "Server=localhost\\MSSQLSERVER03;Database=MedicalSolutionBD;Trusted_Connection=True;TrustServerCertificate=True;"; // a mi LOCALHOST
        //para conectar con el usuario y la contraseña de la base de datos

        private static SqlDao? instance;

        public static SqlDao GetInstance()
        {
            if (instance is null)
            {
                instance = new SqlDao();
            }
            return instance;
        }

        /*
                C  ->  void
                R  ->  result
                U  ->  void
                D  ->  void
         */

        // este ejecuta  CREATE, UPDATE, DELETE
        public void ExecuteProcedure(SqlOperation pOperation)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = pOperation.ProcedureName;

                foreach (var param in pOperation.Parameters)
                {
                    cmd.Parameters.Add(param);
                }

                sqlConnection.Open();
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Dictionary<string, object>> ExecuteProcedureWithQuery(SqlOperation pOperation)
        {
            try
            {
                List<Dictionary<string, object>> lstResults = new List<Dictionary<string, object>>();
                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = pOperation.ProcedureName;

                foreach (var param in pOperation.Parameters)
                {
                    cmd.Parameters.Add(param);
                }

                sqlConnection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        //Recorre el reader en cada fila y obtiene los campos de forma que se almacenan ["nombre", valor]
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        lstResults.Add(row);
                    }
                }

                sqlConnection.Close();
                return lstResults;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}