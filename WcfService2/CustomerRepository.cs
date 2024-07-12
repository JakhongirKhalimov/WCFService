using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WcfService2.DbHelper;
using WcfService2.Services;

public class CustomerRepository
{
    private readonly DbHelper _dbHelper;

    public CustomerRepository()
    {
        _dbHelper = new DbHelper();
    }

    public void AddCustomer(Customer customer)
    {
        try
        {
            string query = "INSERT INTO Customer (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", customer.Name),
                new SqlParameter("@Email", customer.Email),
                new SqlParameter("@Phone", customer.Phone)
            };

            _dbHelper.ExecuteNonQuery(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding customer: " + ex.Message, ex);
        }
    }

    public void UpdateCustomer(Customer customer)
    {
        try
        {
            string query = "UPDATE Customer SET Name = @Name, Email = @Email, Phone = @Phone WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", customer.Id),
                new SqlParameter("@Name", customer.Name),
                new SqlParameter("@Email", customer.Email),
                new SqlParameter("@Phone", customer.Phone)
            };

            _dbHelper.ExecuteNonQuery(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating customer: " + ex.Message, ex);
        }
    }

    public void DeleteCustomer(int customerId)
    {
        try
        {
            string query = "DELETE FROM Customer WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", customerId)
            };

            _dbHelper.ExecuteNonQuery(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting customer: " + ex.Message, ex);
        }
    }

    public Customer GetCustomer(int customerId)
    {
        try
        {
            string query = "SELECT Id, Name, Email, Phone FROM Customer WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", customerId)
            };

            var dataTable = _dbHelper.ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count == 0) return null;

            var row = dataTable.Rows[0];
            return new Customer
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString()
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting customer: " + ex.Message, ex);
        }
    }

    public List<Customer> GetAllCustomers()
    {
        try
        {
            string query = "SELECT Id, Name, Email, Phone FROM Customer";
            var dataTable = _dbHelper.ExecuteQuery(query, null);

            var customers = new List<Customer>();
            foreach (DataRow row in dataTable.Rows)
            {
                customers.Add(new Customer
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString()
                });
            }

            return customers;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting all customers: " + ex.Message, ex);
        }
    }

  
}
