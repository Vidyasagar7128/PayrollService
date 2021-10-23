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
        public void Menus()
        {
            Console.WriteLine("1.Show Records 2.Add Data 0.Exit");
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
                    case 0:
                        Console.WriteLine("Exit");
                        again = false;
                        break;
                }
            }
        }
    }
}
