using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PayrollService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WelCome to Payroll Service!");
            Menu();
        }
        static void Menu()
        {
            Console.Write("1.SQL 2.JSON : ");
            int num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    EmployeePayRollService service = new EmployeePayRollService();
                    service.Repeat();
                    Menu();
                    break;
                case 2:
                    JsonUser jsonUser = new JsonUser();
                    jsonUser.Menu();
                    Menu();
                    break;
            }
        }
    }
}
