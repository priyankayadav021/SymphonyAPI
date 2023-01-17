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
    }
}
