//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using RestSharp;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
//using RestSharp.Serializers.Newtonsoft.Json;
using SymphonyAPI.Symphony;
using System.Text.Json.Nodes;
using SymphonyAPI;
using System.Collections.Generic;

namespace SymphonyAPI
{

    /// <summary>
    /// class for symphony api
    /// </summary>
    public partial class SymphonyBrokerageRestAPIClient
    {
        //private readonly RateGate _restRateLimiter = new RateGate(10, TimeSpan.FromSeconds(1));
        private readonly string _tokenHeader = "authorization";
        private string _token = "";

        public SymphonyBrokerageRestAPIClient()
        {
            RestClient = new RestClient("https://xts.rmoneyindia.co.in:3000/");
        }

        /// <summary>
        /// Gets the RestClient.
        /// </summary>
        /// <value>An instance of the RestClient</value>
        public RestClient RestClient { get; }

        /// <summary>
        /// Symphony API Token
        /// </summary>
        /// <returns>A Symphony API Token</returns>
        public string SymphonyToken => _token;

        private void SignRequest(RestSharp.RestRequest request)
        {
            request.AddHeader(_tokenHeader, _token);
            request.AddHeader("Accept", "application/json");
        }

        /// <summary>
        /// If an IP address exceeds a certain number of requests per minute the 429 status code and
        /// JSON response {"error": "ERR_RATE_LIMIT"} will be returned
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public RestResponse ExecuteRestRequest(RestSharp.RestRequest request)
        {
            //const int maxAttempts = 10;
            //var attempts = 0;

            RestResponse response;
            SignRequest(request);
            //do
            //{
            //    if (!_restRateLimiter.WaitToProceed(TimeSpan.Zero))
            //    {
            //        Log.Trace("Brokerage.OnMessage(): " + new BrokerageMessageEvent(BrokerageMessageType.Warning, "RateLimit",
            //            "The API request has been rate limited. To avoid this message, please reduce the frequency of API calls."));

            //        _restRateLimiter.WaitToProceed();
            //    }

            //    response = RestClient.Execute(request);
            //    // 429 status code: Too Many Requests
            //} while (++attempts < maxAttempts && (int)response.StatusCode == 429);
            response = RestClient.Execute(request);
            return response;
        }

        /// <summary>
        /// Authenticate yourself to Symphony API.
        /// </summary>
        /// <param name="secretKey">Your Symphony Secret Key</param>
        /// <param name="appKey">Your Symphony appKey</param>
        /// <param name="source">your Symphony source</param


        public loginResponse Authorize(string secretKey, string appKey, string source)
        {
            //Log.Trace("SymphonyBrokerageAPI.Authorize(): Getting new Token");
            var request = new RestRequest("/interactive/user/session", Method.Post);
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
            var _loginResponse = JsonConvert.DeserializeObject<loginResponse>(response.Content);
            _token = _loginResponse.result.token;
            return _loginResponse;
        }

        ///// <summary>
        ///// Cancels the order, Invokes cancelOrder call from Symphony api
        ///// </summary>
        ///// <returns>OrderResponse</returns>
        //public CancelOrderResponse CancelOrder(string orderID)
        //{
        //    var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/orders?appOrderID={0}", orderID), Method.Delete);
        //    var response = ExecuteRestRequest(request);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception(
        //            $"SymphonyBrokerage.CancelOrder: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
        //        );
        //    }
        //    var _CancelOrderResponse = JsonConvert.DeserializeObject<CancelOrderResponse>(response.Content);
        //    return _CancelOrderResponse;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TradeBookResponse getTradebook()
        {
            var request = new RestRequest("/interactive/orders/trades", RestSharp.Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.getTradebook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _TradeBookResponse = JsonConvert.DeserializeObject<TradeBookResponse>(response.Content);
            return _TradeBookResponse;
        }

        ///// <summary>
        ///// Performs application-defined tasks associated with freeing, releasing, or resetting
        ///// unmanaged resources.
        ///// </summary>
        //public void Dispose()
        //{
        //    //_restRateLimiter.Dispose();
        //}

        /// <summary>
        /// Gets HoldingsResponses which contains list of Holding Details, Invokes getHoldings call
        /// </summary>
        /// <returns>HoldingsResponse</returns>
        public HoldingsResponse GetHoldings(string clientID)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/portfolio/holdings?clientID={0}", clientID), Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.GetHoldings: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            var _holdingResponse = JsonConvert.DeserializeObject<HoldingsResponse>(response.Content);
            return _holdingResponse;
        }

        /// <summary>
        /// Gets orderbook from SymphonyApi, Invokes orderBook call from Symphony api
        /// </summary>
        /// <returns>OrderBookResponse</returns>
        public OrderBookResponse GetOrderBook()
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/orders"), Method.Get);
            var response = ExecuteRestRequest(request);
            if ((response.StatusCode != HttpStatusCode.OK) && (response.StatusDescription.Contains("No Orders found")))
            {

                throw new Exception(
                     $"SymphonyBrokerage.GetOrderBook: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                 );
            }


            var _orderBookResponse = JsonConvert.DeserializeObject<OrderBookResponse>(response.Content);
            return _orderBookResponse;
        }

        ///// <summary>
        ///// Gets Order Details, Invokes getOrderStatus call from Symphony api
        ///// </summary>
        ///// <returns>OrderResponse</returns>
        //public SymphonyOrderResponse GetOrderDetails()
        //{
        //    var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/orders"), Method.Get);
        //    var response = ExecuteRestRequest(request);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception(
        //            $"SymphonyBrokerage.GetOrderDetails: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
        //        );
        //    }

        //    var _SymphonyOrderResponse = JsonConvert.DeserializeObject<SymphonyOrderResponse>(response.Content);
        //    return _SymphonyOrderResponse;
        //}

        /// <summary>
        /// Gets position details of the user (The details of equity, derivative, commodity,
        /// currency borrowed or owned by the user).
        /// </summary>
        /// <returns>PostionsResponse</returns>
        public PositionsResponse GetPositions(string positionType = "DayWise")
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/portfolio/positions?dayOrNet={0}", positionType), RestSharp.Method.Get);
            var response = ExecuteRestRequest(request);
            if (response.StatusCode != HttpStatusCode.OK && (response.StatusDescription.Contains("No Positions found")))
            {
                throw new Exception(
                    $"SymphonyBrokerage.GetPositions: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }
            var positionsReponse = JsonConvert.DeserializeObject<PositionsResponse>(response.Content);
            return positionsReponse;
        }

        ///// <summary>
        ///// Modifies the order, Invokes modifyOrder call from Symphony api
        ///// </summary>
        ///// <returns>OrderResponse</returns>
        //public ModifyOrderResponse ModifyOrder(PlaceOrder order)
        //{
        //    var payload = new JsonObject
        //    {
        //        { "modifiedTimeInForce", GetTimeInForce(order.timeInForce) },
        //        { "modifiedOrderQuantity", Math.Abs(order.orderQuantity).ToString(CultureInfo.InvariantCulture) },
        //        { "modifiedDisclosedQuantity", Math.Abs(order.disclosedQuantity).ToString(CultureInfo.InvariantCulture) },
        //        { "modifiedOrderType", ConvertOrderType(order.orderType) },
        //        { "modifiedLimitPrice", GetOrderPrice(order).ToString(CultureInfo.InvariantCulture) },
        //        { "modifiedStopPrice", GetOrderTriggerPrice(order).ToString(CultureInfo.InvariantCulture) }
        //    };

        //    var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/orders"), RestSharp.Method.PUT);
        //    request.AddJsonBody(payload.ToString());
        //    var response = ExecuteRestRequest(request);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception($"SymphonyBrokerage.ModifyOrder: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, " + $"Content: {response.Content}, ErrorMessage: {response.ErrorMessage}");
        //    }
        //    var _orderResponse = JsonConvert.DeserializeObject<ModifyOrderResponse>(response.Content);
        //    return _orderResponse;
        //}
        //private static string GetTimeInForce(TimeInForce orderTimeforce)
        //{
        //    if (orderTimeforce == TimeInForce.GoodTilCanceled || orderTimeforce == TimeInForce.Day)
        //    {
        //        return "DAY";
        //    }
        //    throw new NotSupportedException($"SymphonyBrokerage.GetOrderValidity: Unsupported orderTimeforce: {orderTimeforce}");
        //}

        //private static string ConvertOrderType(OrderType orderType)
        //{
        //    switch (orderType)
        //    {
        //        case OrderType.Limit:
        //            return "LIMIT";

        //        case OrderType.Market:
        //            return "MKT";

        //        case OrderType.StopMarket:
        //            return "SL-M";

        //        default:
        //            throw new NotSupportedException($"SymphonyBrokerage.ConvertOrderType: Unsupported order type: {orderType}");
        //    }
        //}
        ///// <summary>
        ///// Return a relevant price for order depending on order type Price must be positive
        ///// </summary>
        ///// <param name="order"></param>
        ///// <returns>A price for order</returns>
        //private static decimal GetOrderPrice(PlaceOrder order)
        //{
        //    switch (order.orderType)
        //    {
        //        case "LIMIT":
        //            return ((LimitOrder)order).LimitPrice;

        //        case "MKT":
        //            // Order price must be positive for market order too; refuses for price = 0
        //            return 0;

        //        case "SL-M":
        //            return ((StopMarketOrder)order).StopPrice;
        //    }

        //    throw new NotSupportedException($"SymphonyBrokerage.ConvertOrderType: Unsupported order type: {order.Type}");
        //}

        ///// <summary>
        ///// Return a relevant price for order depending on order type Price must be positive
        ///// </summary>
        ///// <param name="order"></param>
        ///// <returns>A trigger price for order</returns>
        //private static decimal GetOrderTriggerPrice(Order order)
        //{
        //    switch (order.Type)
        //    {
        //        case OrderType.Limit:
        //            return ((LimitOrder)order).LimitPrice;

        //        case OrderType.Market:
        //            // Order price must be positive for market order too; refuses for price = 0
        //            return 0;

        //        case OrderType.StopMarket:
        //            return ((StopMarketOrder)order).StopPrice;
        //    }

        //    throw new NotSupportedException($"SymphonyBrokerage.ConvertOrderType: Unsupported order type: {order.Type}");
        //}
        ///// <summary>
        ///// Places the order, Invokes PlaceOrder call from Symphony api
        ///// </summary>
        ///// <returns>List of Order Details</returns>
        //public SymphonyOrderResponse PlaceOrder(Order order, string symbol, string exchange, string productType)
        //{
        //    var payload = new JsonObject
        //    {
        //        { "exchangeSegment", exchange },
        //        { "timeInForce", GetTimeInForce(orderTimeforce: order.TimeInForce) },
        //        { "productType", productType },
        //        { "orderQuantity", Math.Abs(order.Quantity).ToString(CultureInfo.InvariantCulture) },
        //        { "disclosedQuantity", Math.Abs(order.Quantity).ToString(CultureInfo.InvariantCulture) },
        //        { "orderType", ConvertOrderType(order.Type) }
        //    };

        //    //if (order.Type == OrderType.Market || order.Type == OrderType.StopMarket)
        //    //{
        //    //    payload.Add("marketProtection", "2");
        //    //}

        //    //if (order.Type == OrderType.StopLimit || order.Type == OrderType.StopMarket || order.Type == OrderType.Limit)
        //    //{
        //    //    payload.Add("triggerPrice", GetOrderTriggerPrice(order).ToString(CultureInfo.InvariantCulture));
        //    //}
        //    if (GetOrderPrice(order).ToString(CultureInfo.InvariantCulture) != "0")
        //    {
        //        payload.Add("limitPrice", GetOrderPrice(order).ToString(CultureInfo.InvariantCulture));
        //    }
        //    var request = new RestRequest("/interactive/orders", RestSharp.Method.POST);
        //    request.AddJsonBody(payload.ToString());
        //    var response = ExecuteRestRequest(request);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        throw new Exception(
        //            $"SymphonyBrokerage.PlaceOrder: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
        //        );
        //    }
        //    var orderResponse = JsonConvert.DeserializeObject<SymphonyOrderResponse>(response.Content);
        //    return orderResponse;
        //}

        //private static string ConvertOrderDirection(OrderDirection orderDirection)
        //{
        //    if (orderDirection == OrderDirection.Buy || orderDirection == OrderDirection.Sell)
        //    {
        //        return orderDirection.ToString().ToUpperInvariant();
        //    }

        //    throw new NotSupportedException($"SymphonyBrokerage.ConvertOrderDirection: Unsupported order direction: {orderDirection}");
        //}

        public SymphonyLogoutResponse logout()
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/user/session"), RestSharp.Method.Delete);
            var response = ExecuteRestRequest(request);
            var logoutResponse = JsonConvert.DeserializeObject<SymphonyLogoutResponse>(response.Content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.GetQuote: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, " +
                    $"Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }
            return logoutResponse;
        }


        public ExchangeStatusResponse exchangeStatusResponse(string userID)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/status/exchange?userID={0}", userID), RestSharp.Method.Get);
            var response = ExecuteRestRequest(request);
            var ExchgStatus = JsonConvert.DeserializeObject<ExchangeStatusResponse>(response.Content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.GetQuote: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, " +
                    $"Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            return ExchgStatus;
        }


        public ExchangeMessages GetExchangeMessages(string exchange)
        {
            var request = new RestRequest(string.Format(CultureInfo.InvariantCulture, "/interactive/messages/exchange?exchangeSegment={0}", exchange), RestSharp.Method.Get);
            var response = ExecuteRestRequest(request);
            var ExchgStatus = JsonConvert.DeserializeObject<ExchangeMessages>(response.Content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(
                    $"SymphonyBrokerage.GetQuote: request failed: [{(int)response.StatusCode}] {response.StatusDescription}, " +
                    $"Content: {response.Content}, ErrorMessage: {response.ErrorMessage}"
                );
            }

            return ExchgStatus;
        }

    }
}
