using System;
using System.Collections.Generic;

namespace WorkingWithData
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeRepo = new EmployeeRepository();
            employeeRepo.InsertEmployees(new Employee
            {
                Name = "Mariam",
                Age = 30
            });

            employeeRepo.UpdateEmployee(new Employee
            {
                Id = 4,
                Name = "Teny",
                Age = 30
            });

            employeeRepo.DeleteEmployee(5);
            var employeesList= employeeRepo.GetEmployees();
            foreach(var employee in employeesList)
            {
                Console.WriteLine($"{employee.Name}---{employee.Id}-----{employee.Age}");
            }

           
        }
    }
}
