using SymphonyAPI;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using SymphonyAPI.Symphony;

namespace SymphonyRestAPI.Test
{
    [TestFixture]
    public class Tests
    {
        SymphonyBrokerageRestAPIClient sym = new SymphonyBrokerageRestAPIClient();
        loginResponse login;

        [OneTimeSetUp]
        public void Authorize_test()
        { 
            // TODO: use configuration API to get secrets
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            login = sym.Authorize(config["appSecret"], config["appKey"], config["loginSource"]);
            Console.WriteLine(login.result.token);
            Assert.Pass();
        }

        [Test]
        public void endSession_test()
        {
            logOffResponse logoff = sym.endSession();
            Console.WriteLine(logoff);
            Assert.Pass();
        }


        [Test]
        public void getUserProfile_test()
        {
            userProfileResponse userProfile = sym.getUserProfile(login.result.userID);
            Console.WriteLine(userProfile.result.ClientId);
        }

        [Test]
        public void getuserBalance_test()
        {
            userBalanceResponse userBalance = sym.getuserBalance(login.result.userID);
            Console.WriteLine(userBalance.result.BalanceList[0].limitHeader);
        }

        [Test]
        public void exchangeStatusResponse_test()
        {
            ExchangeStatusResponse ExchgStatus = sym.exchangeStatusResponse(login.result.userID);
            Console.WriteLine(ExchgStatus.result.marketStatus[0].exchangeTradingSession);
        }

        public void getTradebook_test()
        {
            TradeBookResponse trades = sym.getTradebook();
            if (trades.result.Length > 0)
                Console.WriteLine(trades.result[0].LoginID);
        }


        public void GetOrderBook()
        {
            OrderBookResponse orderResponse = sym.GetOrderBook();
            if (orderResponse.result.Length > 0)
            {
                Console.WriteLine(orderResponse.result[0].LoginID);
            }
        }


        public void GetHoldings_test()
        {
            HoldingsResponse holdingResponse = sym.GetHoldings(login.result.userID);
            Console.WriteLine(holdingResponse.result.ClientId);
        }


        public void GetExchangeMessages_test()
        {
            ExchangeMessages message = sym.GetExchangeMessages("NSECM");
            if (message.result.messageList.Length > 0)
            {
                Console.WriteLine(message.result.messageList[6].BroadcastMessage);
            }
        }


    }
}