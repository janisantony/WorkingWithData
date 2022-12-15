using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StudentData
{
    public class StudentRepository
    {
        private SqlConnection _sqlConnection;
        public StudentRepository()
        {
            var connectionString = "data source =(localdb)\\mssqllocaldb;database=Employee;";
            _sqlConnection = new SqlConnection(connectionString);
        }

        public IEnumerable<Student> InsertStudent(Student stud)
        {
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO StudentDetails values(@Name,@Age,@Marks)", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Name", stud.Name);
                sqlCommand.Parameters.AddWithValue("Age", stud.Age);
                sqlCommand.Parameters.AddWithValue("Marks", stud.Marks);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employees = new List<Student>();
                while (sqlDataReader.Read())
                {
                    employees.Add(new Student
                    {
                        Name = (string)sqlDataReader["Name"],
                        Id = (int)sqlDataReader["Id"],
                        Age = (int)sqlDataReader["Age"],
                        Marks =(int)sqlDataReader["Marks"]
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

        public bool UpdateStudent(Student stud)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("update Employee \r\nset Name = @Name, Age = @Age,Marks=@Marks\r\nwhere Id = @Id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("Id", stud.Id);
                sqlCommand.Parameters.AddWithValue("Name", stud.Name);
                sqlCommand.Parameters.AddWithValue("Age", stud.Age);
                sqlCommand.Parameters.AddWithValue("Marks", stud.Marks);

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

        public bool DeleteStudent(int studId)
        {
            try
            {
                _sqlConnection.Open();

                var sqlCommand = new SqlCommand("delete from StudentDetails where Id = @id", _sqlConnection);
                sqlCommand.Parameters.AddWithValue("id", studId);

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

        public IEnumerable<Student> GetEmployees()
        {
            try
            {
                _sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("select * from StudentDetails", _sqlConnection);
                var sqlDataReader = sqlCommand.ExecuteReader();
                var employees = new List<Student>();
                while (sqlDataReader.Read())
                {
                    employees.Add(new Student
                    {
                        Name = (string)sqlDataReader["Name"],
                        Id = (int)sqlDataReader["Id"],
                        Age = (int)sqlDataReader["Age"],
                        Marks = (int)sqlDataReader["Marks"]
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

    }

    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Age { get; set; }
        public int Marks { get; set; }
    }

}
