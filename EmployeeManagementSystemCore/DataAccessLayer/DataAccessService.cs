using EmployeeManagementSystemCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystemCore.DataAccessLayer
{
    public class DataAccessService : IDataAccessService
    {
        private readonly string constr;

        public DataAccessService()
        {
            constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        }

        public DataTable ExecuteDataSet<T>(string storedProcedure, Dictionary<string, object> parameters)
        {
            SqlDataReader myReader;
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand myCommand = new SqlCommand(storedProcedure);
                myCommand.Connection = connection;
                myCommand.CommandTimeout = 30;
                /*Instead of the below command we have passed the stored procedure while creating SqlCommand object
                 * myCommand.CommandText = query;*/
                myCommand.CommandType = CommandType.StoredProcedure;



                foreach (var param in parameters)
                {
                    var parameter = new SqlParameter
                    {
                        ParameterName = param.Key,
                        Value = param.Value,
                        Direction = ParameterDirection.Input
                    };
                    myCommand.Parameters.Add(parameter);
                }
                connection.Open();
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
            }
            return table;
        }

        public int ExecuteNonQuery(string storedProcedure, Dictionary<string, object> parameters)
        {
            try
            {
                int count = 0;
                /*SqlDataReader myReader;*/
                DataTable table = new DataTable();
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    SqlCommand myCommand = new SqlCommand(storedProcedure);
                    myCommand.Connection = connection;
                    myCommand.CommandTimeout = 30;
                    /*Instead of the below command we have passed the stored procedure while creating SqlCommand object*/
                    /*  myCommand.CommandText = storedProcedure;*/
                    myCommand.CommandType = CommandType.StoredProcedure;
                    foreach (var param in parameters)
                    {
                        var parameter = new SqlParameter
                        {
                            ParameterName = param.Key,
                            Value = param.Value,
                            Direction = ParameterDirection.Input
                        };
                        myCommand.Parameters.Add(parameter);
                    }
                    connection.Open();
                    count = myCommand.ExecuteNonQuery();
                }
                return count;
            }
            catch (ArgumentException ae)
            {
                return 0;
            }
        }
        public object ExecuteScalar(string storedProcedure, Dictionary<string, object> parameters)
        {
            object result;
            using (SqlConnection connection = new SqlConnection(constr))
            {
                SqlCommand myCommand = new SqlCommand(storedProcedure);
                myCommand.Connection = connection;
                myCommand.CommandTimeout = 30;
                /*Instead of the below command we have passed the stored procedure while creating SqlCommand object
                 * myCommand.CommandText = query;*/
                myCommand.CommandType = CommandType.StoredProcedure;
                foreach (var param in parameters)
                {
                    var parameter = new SqlParameter
                    {
                        ParameterName = param.Key,
                        Value = param.Value,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input
                    };
                    myCommand.Parameters.Add(parameter);
                }
                connection.Open();
                result = myCommand.ExecuteScalar();
            }
            return result;


        }

    }
}