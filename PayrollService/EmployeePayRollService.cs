using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PayrollService
{
    class EmployeePayRollService
    {
        private string connectionPath = ConfigurationManager.ConnectionStrings["payConString"].ConnectionString;
        private SqlConnection con;
        public EmployeePayRollService()
        {
            con = new SqlConnection();
            con.ConnectionString = connectionPath;
        }
        /// <summary>
        /// Add Department
        /// </summary>
        public void InsertEmployee()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("Gender (M/F): ");
            string gender = Console.ReadLine();
            string date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_createEmp";
            sqlCommand.Parameters.Add("@Name",SqlDbType.VarChar).Value = name;
            sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;
            sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = gender;
            sqlCommand.Parameters.Add("@StartDate", SqlDbType.VarChar).Value = date;
            try
            {
                con.Open();
                int count = sqlCommand.ExecuteNonQuery();
                if (count == -1)
                    Console.WriteLine($"Employee Created Succesfully...");
                else
                    Console.WriteLine($"Failed to Create Employee...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        public void Repeat()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("1.Create Employee");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        InsertEmployee();
                        Repeat();
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        repeat = false;
                        break;
                }
            }
        }
        
    }
}
