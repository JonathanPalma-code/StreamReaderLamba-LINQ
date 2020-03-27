using System;
using System.IO;
using System.Collections.Generic;
using StreamReaderLambaAndLINQ.Entities;
using System.Linq;

namespace StreamReaderLambaAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            List<Employee> employees = new List<Employee>();

            using (StreamReader streamReader = File.OpenText(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string[] fields = streamReader.ReadLine().Split(',');
                    string name = fields[0];
                    string email = fields[1];
                    double salary = double.Parse(fields[2]);

                    employees.Add(new Employee(name, email, salary));
                }
            }

            Console.Write("Enter the salary: ");
            double nSalary = double.Parse(Console.ReadLine());

            Console.WriteLine($"Email of people whose salary is more than £{nSalary.ToString("F2")}:");
            var result = employees.Where(e => e.Salary > nSalary).OrderBy(e => e.Email).Select(e => e.Email);
            foreach (string employee in result)
            {
                Console.WriteLine(employee);
            }
            var salaryWm = employees.Where(e => e.Name[0] == 'M').Sum(e => e.Salary);
            Console.WriteLine("Sum of people's salary whose name starts with 'M': £" + salaryWm.ToString("F2") + ".");
        }
    }
}
