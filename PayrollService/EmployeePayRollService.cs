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
            sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
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
        /// <summary>
        /// Add Department
        /// </summary>
        public void AddDepartment()
        {
            Console.Write("Enter Name: ");
            string find = Console.ReadLine();
            SqlCommand sqlCommand = new SqlCommand($"select * from employee where Name = '{find}'", con);
            try
            {
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    Console.WriteLine($"Employee Logged In");
                    id = (int)reader["Id"];
                }
                con.Close();
                if (id != 0)
                {
                    Console.Write("Add Department: ");
                    string department = Console.ReadLine();
                    SqlCommand sqlDepartment = new SqlCommand();
                    sqlDepartment.Connection = con;
                    sqlDepartment.CommandType = CommandType.StoredProcedure;
                    sqlDepartment.CommandText = "sp_department";
                    sqlDepartment.Parameters.Add("@DepartmentName", SqlDbType.VarChar).Value = department;
                    sqlDepartment.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = id;
                    try
                    {
                        con.Open();
                        int count = sqlDepartment.ExecuteNonQuery();
                        if (count == -1)
                        {
                            Console.WriteLine($"Department Added Succesfully...");
                            PayRoll();
                        }
                        else
                            Console.WriteLine($"Failed to Add Department...");
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
                else
                    Console.WriteLine("Record Not Found!");
            }
            catch (Exception e)
            {
                Console.Write($"Error: {e}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Accessing PayRoll Data & UC8
        /// </summary>
        public void PayRoll()
        {
            Console.Write("Enter Name: ");
            string find = Console.ReadLine();
            SqlCommand sqlCommand = new SqlCommand($"select * from employee where Name = '{find}'", con);
            try
            {
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    Console.WriteLine($"Employee Logged In");
                    id = (int)reader["Id"];
                }
                con.Close();
                if (id != 0)
                {
                    Console.Write("BasePay: ");
                    double basePay = double.Parse(Console.ReadLine());
                    ///UC8
                    double diduct = basePay / 5;//20%
                    double taxablePay = basePay - diduct;//Salary-Deduction
                    double tax = taxablePay / 10;
                    double netPay = basePay - tax;

                    SqlCommand sqlPayRoll = new SqlCommand();
                    sqlPayRoll.Connection = con;
                    sqlPayRoll.CommandType = CommandType.StoredProcedure;
                    sqlPayRoll.CommandText = "sp_PayRoll";
                    sqlPayRoll.Parameters.Add("@BasicPay", SqlDbType.VarChar).Value = basePay;
                    sqlPayRoll.Parameters.Add("@Deduction", SqlDbType.VarChar).Value = diduct;
                    sqlPayRoll.Parameters.Add("@TaxablePay", SqlDbType.VarChar).Value = taxablePay;
                    sqlPayRoll.Parameters.Add("@IncomeTax", SqlDbType.VarChar).Value = tax;
                    sqlPayRoll.Parameters.Add("@NetPay", SqlDbType.VarChar).Value = netPay;
                    sqlPayRoll.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = id;
                    try
                    {
                        con.Open();
                        int count = sqlPayRoll.ExecuteNonQuery();
                        if (count == -1)
                            Console.WriteLine($"PayRoll Added Succesfully...");
                        else
                            Console.WriteLine($"Failed to Add PayRoll...");
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
                else
                    Console.WriteLine("Record Not Found!");
            }
            catch (Exception e)
            {
                Console.Write($"Error: {e}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// UC3 UC4 Update Salary
        /// </summary>
        public void Updatesalary()
        {
            Console.Write("Enter Name: ");
            string find = Console.ReadLine();
            SqlCommand sqlCommand = new SqlCommand($"select * from employee where Name = '{find}'", con);
            try
            {
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                int id = 0;
                while (reader.Read())
                {
                    Console.WriteLine($"Employee Logged In");
                    id = (int)reader["Id"];
                }
                con.Close();
                if (id != 0)
                {
                    Console.Write("BasePay: ");
                    double basePay = double.Parse(Console.ReadLine());

                    SqlCommand sqlPayRoll = new SqlCommand($"update payRoll set BasicPay = '{basePay}' where EmployeeId = '{id}'");
                    sqlPayRoll.Connection = con;
                    try
                    {
                        con.Open();
                        int count = sqlPayRoll.ExecuteNonQuery();
                        if (count != 0)
                            Console.WriteLine($"Salary Updated Succesfully...");
                        else
                            Console.WriteLine($"Failed to Update Salary...");
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
                else
                    Console.WriteLine("Record Not Found!");
            }
            catch (Exception e)
            {
                Console.Write($"Error: {e}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        ///UC5 Join PayRoll Table
        /// </summary>
        public void PayRollData()
        {
            SqlCommand sqlPayRoll = new SqlCommand($"select * from employee as E right join PayRoll as P on E.Id = P.EmployeeId");
            sqlPayRoll.Connection = con;
            try
            {
                con.Open();
                SqlDataReader reader = sqlPayRoll.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} {reader["Address"]} {reader["Gender"]} {reader["StartDate"]} {reader["BasicPay"]} {reader["Deduction"]} {reader["TaxablePay"]} {reader["IncomeTax"]} {reader["NetPay"]}");
                }
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
                Console.WriteLine("1.Create Employee 2.Add Department 3.Add RollPay, 4.Update Salary 5.PayRoll Data");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        InsertEmployee();
                        Repeat();
                        break;
                    case 2:
                        AddDepartment();
                        Repeat();
                        break;
                    case 3:
                        PayRoll();
                        Repeat();
                        break;
                    case 4:
                        Updatesalary();
                        Repeat();
                        break;
                    case 5:
                        PayRollData();
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