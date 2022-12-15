using System;
using StudentData;

namespace StudentDataWithLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var studentRepo = new StudentRepository();
            studentRepo.InsertStudent(new Student
            {
                Name = "Mariam",
                Age = 30
            });

            studentRepo.UpdateStudent(new Student
            {
                Id = 4,
                Name = "Teny",
                Age = 30
            });

            studentRepo.DeleteStudent(5);
            var studentsList = studentRepo.GetEmployees();
            foreach (var student in studentsList)
            {
                Console.WriteLine($"{student.Name}---{student.Id}-----{student.Age}");
            }
        }
    }
}
