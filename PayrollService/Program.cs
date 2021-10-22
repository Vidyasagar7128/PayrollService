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
            EmployeePayRollService service = new EmployeePayRollService();
            service.Repeat();
        }
    }
}
