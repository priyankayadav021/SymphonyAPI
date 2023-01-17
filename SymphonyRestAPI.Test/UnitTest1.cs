using SymphonyAPI;
using Microsoft.Extensions.Configuration;
namespace SymphonyRestAPI.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [OneTimeSetUp]
        public void Authorize_test()
        {
            SymphonyBrokerageRestAPIClient sym = new SymphonyBrokerageRestAPIClient();  
            // TODO: use configuration API to get secrets
            loginResponse login = sym.Authorize("", "", "");
            Console.WriteLine(login.result.token);
            Assert.Pass();
        }
    }
}