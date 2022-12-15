using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace WorkingWithData
{
    public class EmployeeRepository
    {
        private SqlConnection _sqlConnection;
        public EmployeeRepository()
        {
            var connectionString= "data source =(localdb)\\mssqllocaldb;database=Employee;";
            _sqlConnection = new SqlConnection(connectionString);
        }

        public IEnumerable<Employee> InsertEmployees(Employee emp)
        {
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO EMPLOYEE values(@Name,@Age)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Name",emp.Name);
                sqlCommand.Parameters.AddWithValue("Age",emp.Age);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employees = new List<Employee>();
                while (sqlDataReader.Read())
                {
                    employees.Add(new Employee
                    {
                        Name = (string)sqlDataReader["Name"],
                        Id = (int)sqlDataReader["Id"],
                        Age = (int)sqlDataReader["Age"]
                    });
                }
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("update Employee \r\nset Name = @Name, Age = @Age\r\nwhere Id = @Id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", emp.Id);
                sqlCommand.Parameters.AddWithValue("Name", emp.Name);
                sqlCommand.Parameters.AddWithValue("Age", emp.Age);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("delete from Employee where Id = @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", empId);

                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from EMPLOYEE", _sqlConnection);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employees = new List<Employee>();
                while (sqlDataReader.Read())
                {
                    employees.Add(new Employee
                    {
                        Name = (string)sqlDataReader["Name"],
                        Id = (int)sqlDataReader["Id"],
                        Age = (int)sqlDataReader["Age"]
                    });
                }
                return employees;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

    }

    public class Employee
    {
        public string Name { get; set; }
        public int  Id { get; set; }
        public int Age { get; set; }
    }
}
