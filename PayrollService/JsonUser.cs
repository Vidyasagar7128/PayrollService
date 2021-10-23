using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayrollService
{
    class JsonUser
    {
        private readonly RestClient client = new RestClient("http://localhost:3000/");
        /// <summary>
        /// Accesing Data from Json Server
        /// </summary>
        public void ShowData()
        {
            RestRequest req = new RestRequest("/profile/");
            req.AddHeader("Accept", "application/json");
            IRestResponse<List<User>> restRequest = client.Get<List<User>>(req);
            foreach (var d in restRequest.Data)
            {
                Console.WriteLine($"{d.id} {d.Name} {d.Salary}");
            }
        }
        /// <summary>
        /// Add Data
        /// </summary>
        public void AddData()
        {
            RestRequest req = new RestRequest("/profile/");
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new User() { Name = "John Doe", Salary = 27000 });
            IRestResponse response = client.Post<User>(req);
            Console.WriteLine($"Code: {response.StatusCode}");
        }
        /// <summary>
        /// Update User Data using Id
        /// </summary>
        public void UpdateSalary()
        {
            Console.Write("Id: ");
            int uId = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Salary: ");
            double salary = double.Parse(Console.ReadLine());
            RestRequest req = new RestRequest($"/profile/{uId}");
            req.AddHeader("Content-Type", "application/json");
            req.AddJsonBody(new User() { Name = name, Salary = salary } );
            IRestResponse response = client.Put<User>(req);
            Console.WriteLine($"Code: {response.StatusCode}");
        }
        public void Menus()
        {
            Console.WriteLine("1.Show Records 2.Add Data 3.Update Data 0.Exit");
            bool again = true;
            while (again)
            {
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        ShowData();
                        Menus();
                        break;
                    case 2:
                        AddData();
                        Menus();
                        break;
                    case 3:
                        UpdateSalary();
                        Menus();
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        again = false;
                        break;
                }
            }
        }
    }
}
