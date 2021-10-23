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
        public void Menu()
        {
            Console.WriteLine("1.Show Records 0.Exit");
            bool again = true;
            while (again)
            {
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        ShowData();
                        Menu();
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
