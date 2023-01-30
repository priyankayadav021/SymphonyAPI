using Newtonsoft.Json.Linq;
using SymphonyAPI;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SocketIOClient;
using Newtonsoft.Json;

namespace SymphonyAPIClient
{
    public class Main
    {
        //public SocketIO socket =  new SocketIO("https://xts.rmoneyindia.co.in:3000/marketdata/socket.io/");
        public bool isConnected = false;
        MarketAPIProtocol market;
        

        public Main() {
            market = new MarketAPIProtocol();
        }


        public void SocketConnection()
        {
           
            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            var login = market.Authorize(config["mappSecret"], config["mappKey"], config["mloginSource"]);
            string url = "https://xts.rmoneyindia.co.in:3000/?token" + login.result.token + "&userID=" + login.result.userID + "&publishFormat=JSON&broadcastMode=Full";
            var socket = new SocketIO(url, new SocketIOOptions
            {
                Path = "/marketdata/socket.io",
                //Query = new List<KeyValuePair<string, string>>
                //    {
                //        new KeyValuePair<string, string>("token",  login.result.token),
                //        new KeyValuePair<string, string>("userID", login.result.userID),
                //        new KeyValuePair<string, string>("publishFormat" , "JSON"),
                //        new KeyValuePair<string, string>("broadcastMode" , "Full")
                //    },
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
            });
            socket.On("connect", (data) =>
            {
                Console.WriteLine("connected");
                Console.WriteLine(data.ToString());
                //isConnected = true;
            });
            socket.On("1501-json-full", (data) =>
            {
                Console.WriteLine(data);
            });
            Console.WriteLine(socket);
            socket.ConnectAsync();
            Console.WriteLine(socket.Connected);
            getsubscribe();

        }

        public void SocketDisconnect()
        {
            //if(isConnected) socket.Close();
        }
        

        public void getsubscribe()
        {
            string[] exchangeinstID = { "151289", "58459", "104002", "58489", "53427" };
            for (int k = 0; k < 5; k++)
            {
                MarketStreamResponse response = market.getMarketStream(exchangeinstID[k],"2");
                //for (int i = 0; i < response.result.listQuotes.Length; i++)
                //    Console.WriteLine(response.result.listQuotes[0]);
            }
        }


        public void updateParams(string data)
        {
            //update params
            Console.WriteLine(data);
        }
    }
}
