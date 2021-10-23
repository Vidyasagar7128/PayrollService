using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using PayrollService;

namespace PayRollTest
{
    [TestClass]
    public class UnitTest1
    {
        private RestClient client = new RestClient("http://localhost:3000/");
        [TestMethod]
        public void TestMethod1()
        {
            var req = new RestRequest("/languages/");
            req.AddHeader("Accept", "application/json");
            IRestResponse<List<User>> restRequest = client.Get<List<User>>(req);
            Assert.AreEqual(restRequest.Data,"");
        }
    }
}
