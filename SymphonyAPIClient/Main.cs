using Newtonsoft.Json.Linq;
using SymphonyAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SocketIOClient;
using Quobject.SocketIoClientDotNet.Client;


namespace SymphonyAPIClient
{
    public class Main
    {
        public Quobject.SocketIoClientDotNet.Client.Socket socket = IO.Socket("https://xts.rmoneyindia.co.in:3000/marketdata/socket.io");
        public bool isConnected = false;
        MarketAPIProtocol market;
        

        public Main() {
            market = new MarketAPIProtocol();
        }


        public void SocketConnection()
        {

            var config = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

            var login = market.Authorize(config["mappSecret"], config["mappKey"], config["mloginSource"]);

            var payload = new JsonObject
                {
                {"token", login.result.token},
                {"userID", login.result.userID},
                {"publishFormat" , "JSON" },
                {"broadcastMode" , "Full" }
            };
            socket.Connect();
            socket.On("connect", (data) =>
            {
                Console.WriteLine(data.ToString());
                isConnected = true;
            });
             
            socket.Emit("connect", payload);
            getsubscribe();
            socket.On("1501-json-full", (data) =>
            {
                updateParams(data.ToString());
            });


        }
        
        public void SocketDisconnect()
        {
            if(isConnected) socket.Close();
        }
        

        public void getsubscribe()
        {
            string file = @"D:/Contract.txt";
            int count = 0;
            if (File.Exists(file))
            {
                string[] contract = File.ReadAllLines(file);
                foreach (string contractLine in contract)
                {
                    string[] words = contractLine.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length > 0)
                    { 
                        if (words[5] == "OPTSTK" && words[0] == "NSEFO")
                        {
                            MarketStreamResponse response = market.getMarketStream(words[1], words[2]);
                            for (int i = 0; i < response.result.listQuotes.Length; i++)
                                Console.WriteLine(response.result.listQuotes[0]);
                            count++;
                        }
                        if (count > 10) break;
                    }

                }

            }
        }


        public void updateParams(string data)
        {
            //update params
        }
    }
}
