using Microsoft.Extensions.Configuration;
using SymphonyAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SymphonyRestAPI.Test
{
    [TestFixture]
    public class MarketData
    {

        MarketAPIProtocol marketdata = new MarketAPIProtocol();
        marketLoginResponse login;

        [OneTimeSetUp]
        public void Authorize_test()
        {
            // TODO: use configuration API to get secrets
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            login = marketdata.Authorize(config["appSecret"], config["appKey"], config["loginSource"]);
            Console.WriteLine(login.result.token);
            Assert.Pass();
        }

        [Test]
        public void GetClientConfigResponse_test()
        {
            ClientConfigResponse client = marketdata.GetClientConfigResponse();
            Console.WriteLine(client.result.broadCastMode[0]);
        }

        [Test]

        public void getSeriesResponse_test()
        {
            GetSeriesResponse getSeries = marketdata.getSeriesResponse("NSEFO");
            Console.WriteLine(getSeries.Property1[0].result._1);
        }


        [Test]
        public void sessionEnd_test()
        {
            marketSessionEnd logout = marketdata.sessionEnd();
            Console.WriteLine(logout.description);

        }


        [Test]
        public void getEquitySymbol_test()
        {
            GetEquitySymbol symbol = marketdata.getEquitySymbol("1", "EQ", "Acc");
            Console.WriteLine(symbol.result[0].InstrumentID);
        }


        [Test]
        public void getExpiryDate_test()
        {
            GetExpiryDate expiry = marketdata.getExpiryDate("2", "FUTIDX", "NIFTY");
            Console.WriteLine(expiry.result[0]);
        }


        [Test]
        public void getFutureSymbol_test()
        {
            GetFutureSymbol symbol = marketdata.getFutureSymbol("2", "FUTIDX", "NIFTY", "23Feb2023");
            Console.WriteLine(symbol.result[0].PriceBand.CreditRating.ToString());
        }


        [Test]
        public void getOptionSymbol_test()
        {
            GetOptionSymbol optSymbol = marketdata.getOptionSymbol("2", "FUTIDX", "NIFTY", "23Feb2023", "CE", "18150");  //data not available
            Console.WriteLine(optSymbol.Property1[0].result.PriceBand.LowString);
        }


        [Test]
        public void getOptionType_test()
        {
            GetOptionType optionType = marketdata.getOptionType("2", "FUTIDX", "NIFTY", "23Feb2023");
            Console.WriteLine(optionType.result[0]);
        }

        [Test]
        public void getIndexList_test()
        {
            IndexList expiry = marketdata.getIndexList("1");
            Console.WriteLine(expiry.result.indexList[0]);
        }
    }
}
