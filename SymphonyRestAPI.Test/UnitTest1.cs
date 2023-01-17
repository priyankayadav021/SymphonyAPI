using SymphonyAPI;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace SymphonyRestAPI.Test
{
    [TestFixture]
    public class Tests
    {

        [OneTimeSetUp]
        public void Authorize_test()
        {
            SymphonyBrokerageRestAPIClient sym = new SymphonyBrokerageRestAPIClient();
            // TODO: use configuration API to get secrets
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            loginResponse login = sym.Authorize(config["appSecret"], config["appKey"], config["loginSource"]);
            Console.WriteLine(login.result.token);
            Assert.Pass();
        }
    }
}