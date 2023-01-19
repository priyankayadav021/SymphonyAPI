using RestSharp;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
//using RestSharp.Serializers.Newtonsoft.Json;
using SymphonyAPI.Symphony;
using System.Text.Json.Nodes;
using SymphonyAPI;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
//using System.Web.Helpers;

namespace SymphonyAPI
{
    public class MarketAPIProtocol
    {
        public RestClient RestClient { get; }
        private string _token = "";
        private readonly string _tokenHeader = "authorization";
        public MarketAPIProtocol()
        {
            RestClient = new RestClient("https://xts.rmoneyindia.co.in:3000/marketdata");
        }

        private void SignRequest(RestSharp.RestRequest request)
        {
            request.AddHeader(_tokenHeader, _token);
            request.AddHeader("Accept", "application/json");
        }

        public RestResponse ExecuteRestRequest(RestSharp.RestRequest request)
        {
            //const int maxAttempts = 10;
            //var attempts = 0;

            RestResponse response;
            SignRequest(request);
            response = RestClient.Execute(request);
            return response;
        }
        public marketLoginResponse Authorize(string secretKey, string appKey, string source)
        {
            //Log.Trace("SymphonyBrokerageAPI.Authorize(): Getting new Token"); 
            var request = new RestRequest("/auth/login", Method.Post);
            //request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(new { secretKey = secretKey, appKey = appKey, source = source });

            RestResponse response = RestClient.Execute(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.Authorize: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }
            var _loginResponse = JsonConvert.DeserializeObject<marketLoginResponse>(response.Content);
            _token = _loginResponse.result.token;
            return _loginResponse;
        }


        public marketSessionEnd marketsessionEnd()
        {
            var request = new RestRequest("/auth/logout", RestSharp.Method.Delete);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _logout = JsonConvert.DeserializeObject<marketSessionEnd>(response.Content);
            return _logout;
        }

        public ClientConfigResponse GetClientConfigResponse()
        {
            var request = new RestRequest("/config/clientConfig", RestSharp.Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _ClientConfigResponse = JsonConvert.DeserializeObject<ClientConfigResponse>(response.Content);
            return _ClientConfigResponse;
        }

        public QuoteResponse GetQuote()
        {
            var request = new RestRequest("/instruments/quotes", RestSharp.Method.Post);
            Instrument[] inst = { new Instrument { exchangeInstrumentID = 94191, exchangeSegment = 2 } };
            //inst[0] = x;
            var serialized = JsonConvert.SerializeObject(inst);
            var payload = new JsonObject
            {
                { "instruments", serialized},
                { "xtsMessageCode", 1502},
                { "publishFormat", "JSON" }
            };
            request.AddBody(payload);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getQuote = JsonConvert.DeserializeObject<QuoteResponse>(response.Content);
            return _getQuote;
        }




        public MarketStreamResponse getMarketStream()
        {
            var request = new RestRequest("/instruments/subscription", RestSharp.Method.Post);
            Instrument[] inst = { new Instrument { exchangeInstrumentID = 94191, exchangeSegment = 2 }, new Instrument { exchangeInstrumentID = 94191, exchangeSegment = 2 } };
            var serialized = JsonConvert.SerializeObject(inst);
            var payload = new JsonObject
            {
                { "instruments", serialized},
                { "xtsMessageCode", 1502}
            };
            request.AddBody(payload);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getMarketStream = JsonConvert.DeserializeObject<MarketStreamResponse>(response.Content);
            return _getMarketStream;
        }



        public MarketStreamUnsubsResponse getMarketStreamUnsubscribe()
        {
            var request = new RestRequest("/instruments/subscription", RestSharp.Method.Put);
            Instrument[] inst = { new Instrument { exchangeInstrumentID = 22, exchangeSegment = 2 }, };
            var serialized = JsonConvert.SerializeObject(inst);
            var payload = new JsonObject
            {
                { "instruments", serialized},
                { "xtsMessageCode", 1502}
            };
            request.AddBody(payload);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getMarketStreamUnsubs = JsonConvert.DeserializeObject<MarketStreamUnsubsResponse>(response.Content);
            return _getMarketStreamUnsubs;
        }

        public MasterResponse getMasterResponse()
        {
            var request = new RestRequest("/instruments/master", RestSharp.Method.Post);
            var exchangeSegmentList = new List<string>(){"NSECD","NSECM","NSEFO"};
            var payload = JsonConvert.SerializeObject(exchangeSegmentList);
            var temp = new JsonObject
            {
                {"exchangeSegmentList", payload}
            };
            request.AddBody(temp);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getMarketStream = JsonConvert.DeserializeObject<MasterResponse>(response.Content);
            return _getMarketStream;
        }

        public GetSeriesResponse getSeriesResponse(string exchgSegment)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/instruments/instrument/series?exchangeSegment={0}", exchgSegment), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _GetSeriesResponse = JsonConvert.DeserializeObject<GetSeriesResponse>(response.Content);
            return _GetSeriesResponse;
        }



        public GetEquitySymbol getEquitySymbol(string exchgSegment, string series, string symbol)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/instruments/instrument/symbol?exchangeSegment={0}&series={1}&symbol={2}", exchgSegment, series, symbol), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getEquitySymbol = JsonConvert.DeserializeObject<GetEquitySymbol>(response.Content);
            return _getEquitySymbol;
        }



        public GetExpiryDate getExpiryDate(string exchgSegment, string series, string symbol)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/instruments/instrument/expiryDate?exchangeSegment={0}&series={1}&symbol={2}", exchgSegment, series, symbol), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getExpiryDate = JsonConvert.DeserializeObject<GetExpiryDate>(response.Content);
            return _getExpiryDate;
        }



        public GetFutureSymbol getFutureSymbol(string exchgSegment, string series, string symbol, string expiry)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "instruments/instrument/futureSymbol?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3}", exchgSegment, series, symbol, expiry), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getFutureSymbol = JsonConvert.DeserializeObject<GetFutureSymbol>(response.Content);
            return _getFutureSymbol;
        }


        public GetOptionSymbol getOptionSymbol(string exchgSegment, string series, string symbol, string expiry, string optionType, string strikePrice)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/instruments/instrument/optionSymbol?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3}&optionType={4}&strikePrice={5}", exchgSegment, series, symbol, expiry, optionType, strikePrice), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getOptionSymbol = JsonConvert.DeserializeObject<GetOptionSymbol>(response.Content);
            return _getOptionSymbol;
        }


        public GetOptionType getOptionType(string exchgSegment, string series, string symbol, string expiry)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "instruments/instrument/optionType?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3}", exchgSegment, series, symbol, expiry), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _getOptionType = JsonConvert.DeserializeObject<GetOptionType>(response.Content);
            return _getOptionType;
        }


        public IndexList getIndexList(string exchgSegment)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/instruments/indexlist?exchangeSegment={0}", exchgSegment), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {

                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _IndexList = JsonConvert.DeserializeObject<IndexList>(response.Content);
            return _IndexList;
        }
    }
}
