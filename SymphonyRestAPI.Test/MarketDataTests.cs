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
    public class MarketDataTests
    {

        MarketAPIProtocol marketdata = new MarketAPIProtocol();
        marketLoginResponse login;

        [OneTimeSetUp]
        public void Authorize_test()
        {
            // TODO: use configuration API to get secrets
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            login = marketdata.Authorize(config["mappSecret"], config["mappKey"], config["mloginSource"]);
            Console.WriteLine(login.result.token);
            //Assert.Pass();
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


        [OneTimeTearDown]
        public void marketsessionEnd_test()
        {
            marketSessionEnd logout = marketdata.marketsessionEnd();
            Console.WriteLine(logout.description);

        }

        [Test]
        public void getQuote_test()
        {
            QuoteResponse getQuote = marketdata.GetQuote();  //not checked
            Console.WriteLine(getQuote.result.listQuotes);
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


        [Test]
        public void getMarketStream_test()
        {
            MarketStreamResponse marketStream = marketdata.getMarketStream();
            for(int i=0;i< marketStream.result.quotesList.Length;i++)
                Console.WriteLine(marketStream.result.quotesList[i].exchangeInstrumentID);
        }


        [Test]
        public void getMarketStreamUnsubs_test()
        {
            MarketStreamUnsubsResponse marketStream = marketdata.getMarketStreamUnsubscribe();
            Console.WriteLine(marketStream.result.unsubList[0].exchangeInstrumentID);
        }



        [Test]
        public void getMasterResponse_test()   //Like contract
        {
            MasterResponse master = marketdata.getMasterResponse();
            Console.WriteLine(master.result);
        }

    }
}
