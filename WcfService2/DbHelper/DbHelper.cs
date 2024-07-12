using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WcfService2.DbHelper
{
    public class DbHelper
    {
        private readonly string _connectionString;

        public DbHelper()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while executing the command: {query}", ex);
            }
        }

        public DataTable ExecuteQuery(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {

                string errorMessage = $"SQL Error executing query: {sqlEx.Message}\nQuery: {query}\nError Code: {sqlEx.ErrorCode}\nNumber: {sqlEx.Number}";
                Console.WriteLine(errorMessage);
                throw new Exception(errorMessage, sqlEx);
            }
            catch (Exception ex)
            {
                string errorMessage = $"General Error executing query: {ex.Message}\nQuery: {query}";
                Console.WriteLine(errorMessage);
                throw new Exception(errorMessage, ex);
            }
        }

    }
}