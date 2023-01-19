using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SymphonyAPI.Symphony
{
    /// <summary>
    /// Custom DateTime JSON serializer/deserializer
    /// </summary>
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// DateTime format
        /// </summary>
        private string Format = "yyyy-mm-dd hh:MM:ss";

        /*
        public CustomDateTimeConverter(string _format)
        {
            Format = _format;
        }
        */
        /// <summary>
        /// Writes value to JSON
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Value to be written</param>
        /// <param name="serializer">JSON serializer</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString(Format));
        }

        /// <summary>
        /// Reads value from JSON
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="objectType">Target type</param>
        /// <param name="existingValue">Existing value</param>
        /// <param name="serializer">JSON serialized</param>
        /// <returns>Deserialized DateTime</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var s = reader.Value.ToString();
            DateTime result;
            if (DateTime.TryParseExact(s, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            return DateTime.Now;
        }
    }


    public class SymphonyOrderResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result result { get; set; }


        public class Result
        {
            public int AppOrderID { get; set; }
            public string OrderUniqueIdentifier { get; set; }
            public string ClientID { get; set; }
        }
    }

    public class PlaceOrder
    {
        public string exchangeSegment { get; set; }
        public int exchangeInstrumentID { get; set; }
        public string productType { get; set; }
        public string orderType { get; set; }
        public string orderSide { get; set; }
        public string timeInForce { get; set; }
        public int disclosedQuantity { get; set; }
        public int orderQuantity { get; set; }
        public float limitPrice { get; set; }
        public int stopPrice { get; set; }
        public string orderUniqueIdentifier { get; set; }
    }


    public class ModifyOrder
    {
        public long appOrderID { get; set; }
        public string modifiedProductType { get; set; }
        public string modifiedOrderType { get; set; }
        public int modifiedOrderQuantity { get; set; }
        public int modifiedDisclosedQuantity { get; set; }
        public float modifiedLimitPrice { get; set; }
        public int modifiedStopPrice { get; set; }
        public string modifiedTimeInForce { get; set; }
        public string orderUniqueIdentifier { get; set; }
    }

    public class ModifyOrderResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result result { get; set; }


        public class Result
        {
            public int AppOrderID { get; set; }
            public string OrderUniqueIdentifier { get; set; }
            public string ClientID { get; set; }
        }
    }
    public class CancelOrderResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result[] result { get; set; }


        public class Result
        {
            public int AppOrderID { get; set; }
            public string ClientID { get; set; }
        }
    }

    public class OrderBookResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result[] result { get; set; }

        public class Result
        {
            public string LoginID { get; set; }
            public string ClientID { get; set; }
            public int AppOrderID { get; set; }
            public string OrderReferenceID { get; set; }
            public string GeneratedBy { get; set; }
            public string ExchangeOrderID { get; set; }
            public string OrderCategoryType { get; set; }
            public string ExchangeSegment { get; set; }
            public int ExchangeInstrumentID { get; set; }
            public string OrderSide { get; set; }
            public string OrderType { get; set; }
            public string ProductType { get; set; }
            public string TimeInForce { get; set; }
            public float OrderPrice { get; set; }
            public int OrderQuantity { get; set; }
            public int OrderStopPrice { get; set; }
            public string OrderStatus { get; set; }
            public float OrderAverageTradedPrice { get; set; }
            public int LeavesQuantity { get; set; }
            public int CumulativeQuantity { get; set; }
            public int OrderDisclosedQuantity { get; set; }
            public string OrderGeneratedDateTime { get; set; }
            public string ExchangeTransactTime { get; set; }
            public string LastUpdateDateTime { get; set; }
            public string OrderExpiryDate { get; set; }
            public string CancelRejectReason { get; set; }
            public string OrderUniqueIdentifier { get; set; }
            public string OrderLegStatus { get; set; }
            public int BoLegDetails { get; set; }
            public bool IsSpread { get; set; }
            public string BoEntryOrderId { get; set; }
            public int MessageCode { get; set; }
            public int MessageVersion { get; set; }
            public int TokenID { get; set; }
            public int ApplicationType { get; set; }
            public int SequenceNumber { get; set; }
        }
    }

    public class TradeBookResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result[] result { get; set; }

        public class Result
        {
            public string LoginID { get; set; }
            public string ClientID { get; set; }
            public int AppOrderID { get; set; }
            public string OrderReferenceID { get; set; }
            public string GeneratedBy { get; set; }
            public string ExchangeOrderID { get; set; }
            public string OrderCategoryType { get; set; }
            public string ExchangeSegment { get; set; }
            public string ExchangeInstrumentID { get; set; }
            public string OrderSide { get; set; }
            public string OrderType { get; set; }
            public string ProductType { get; set; }
            public string TimeInForce { get; set; }
            public float OrderPrice { get; set; }
            public int OrderQuantity { get; set; }
            public int OrderStopPrice { get; set; }
            public string OrderStatus { get; set; }
            public float OrderAverageTradedPrice { get; set; }
            public int LeavesQuantity { get; set; }
            public int CumulativeQuantity { get; set; }
            public int OrderDisclosedQuantity { get; set; }
            public string OrderGeneratedDateTime { get; set; }
            public string ExchangeTransactTime { get; set; }
            public string LastUpdateDateTime { get; set; }
            public string OrderUniqueIdentifier { get; set; }
            public string OrderLegStatus { get; set; }
            public float LastTradedPrice { get; set; }
            public int LastTradedQuantity { get; set; }
            public string LastExecutionTransactTime { get; set; }
            public string ExecutionID { get; set; }
            public int ExecutionReportIndex { get; set; }
            public bool IsSpread { get; set; }
            public int MessageCode { get; set; }
            public int MessageVersion { get; set; }
            public int TokenID { get; set; }
            public int ApplicationType { get; set; }
            public int SequenceNumber { get; set; }
        }
    }

    public class HoldingsResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public string ClientId { get; set; }
            public Rmsholdinglist[] RMSHoldingList { get; set; }
            public Collateralholdinglist[] CollateralHoldingList { get; set; }
        }

        public class Rmsholdinglist
        {
            public Holdings Holdings { get; set; }
        }

        public class Holdings
        {
            public INF457M01133 INF457M01133 { get; set; }
        }

        public class INF457M01133
        {
            public string ISIN { get; set; }
            public int RMSHoldingId { get; set; }
            public string ClientId { get; set; }
            public int ExchangeNSEInstrumentId { get; set; }
            public int ExchangeBSEInstrumentId { get; set; }
            public int ExchangeMSEIInstrumentId { get; set; }
            public int HoldingType { get; set; }
            public int HoldingQuantity { get; set; }
            public int CollateralValuationType { get; set; }
            public int Haircut { get; set; }
            public int CollateralQuantity { get; set; }
            public string CreatedBy { get; set; }
            public string LastUpdatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime LastUpdatedOn { get; set; }
            public int UsedQuantity { get; set; }
            public bool IsCollateralHolding { get; set; }
            public int BuyAvgPrice { get; set; }
            public bool IsBuyAvgPriceProvided { get; set; }
            public int AuthorizeQuantity { get; set; }
            public bool IsNeedToDelete { get; set; }
        }

        public class Collateralholdinglist
        {
            public Holdings1 Holdings { get; set; }
        }

        public class Holdings1
        {
            public INF457M011331 INF457M01133 { get; set; }
        }

        public class INF457M011331
        {
            public string ISIN { get; set; }
            public int RMSHoldingId { get; set; }
            public string ClientId { get; set; }
            public int ExchangeNSEInstrumentId { get; set; }
            public int ExchangeBSEInstrumentId { get; set; }
            public int ExchangeMSEIInstrumentId { get; set; }
            public int HoldingType { get; set; }
            public int HoldingQuantity { get; set; }
            public int CollateralValuationType { get; set; }
            public int Haircut { get; set; }
            public int CollateralQuantity { get; set; }
            public string CreatedBy { get; set; }
            public string LastUpdatedBy { get; set; }
            public DateTime CreatedOn { get; set; }
            public DateTime LastUpdatedOn { get; set; }
            public int UsedQuantity { get; set; }
            public bool IsCollateralHolding { get; set; }
            public int BuyAvgPrice { get; set; }
            public bool IsBuyAvgPriceProvided { get; set; }
            public int AuthorizeQuantity { get; set; }
            public bool IsNeedToDelete { get; set; }
        }
    }

    public class PositionsResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result[] result { get; set; }

        public class Result
        {
            public string AccountID { get; set; }
            public string TradingSymbol { get; set; }
            public string ExchangeSegment { get; set; }
            public int ExchangeInstrumentID { get; set; }
            public string ProductType { get; set; }
            public int Marketlot { get; set; }
            public int Multiplier { get; set; }
            public float BuyAveragePrice { get; set; }
            public float SellAveragePrice { get; set; }
            public int OpenBuyQuantity { get; set; }
            public int OpenSellQuantity { get; set; }
            public int Quantity { get; set; }
            public int BuyAmount { get; set; }
            public int SellAmount { get; set; }
            public int NetAmount { get; set; }
            public int UnrealizedMTM { get; set; }
            public int RealizedMTM { get; set; }
            public int MTM { get; set; }
            public int BEP { get; set; }
            public int SumOfTradedQuantityAndPriceBuy { get; set; }
            public int SumOfTradedQuantityAndPriceSell { get; set; }
            public string statisticsLevel { get; set; }
            public string isInterOpPosition { get; set; }
            public Childpositions childPositions { get; set; }
            public int MessageCode { get; set; }
            public int MessageVersion { get; set; }
            public int TokenID { get; set; }
            public int ApplicationType { get; set; }
            public int SequenceNumber { get; set; }
        }

        public class Childpositions
        {
            public string AccountID { get; set; }
            public string TradingSymbol { get; set; }
            public string ExchangeSegment { get; set; }
            public string ExchangeInstrumentID { get; set; }
            public string ProductType { get; set; }
            public string Marketlot { get; set; }
            public string Multiplier { get; set; }
            public string BuyAveragePrice { get; set; }
            public string SellAveragePrice { get; set; }
            public string OpenBuyQuantity { get; set; }
            public string OpenSellQuantity { get; set; }
            public string Quantity { get; set; }
            public string BuyAmount { get; set; }
            public string SellAmount { get; set; }
            public string NetAmount { get; set; }
            public string UnrealizedMTM { get; set; }
            public string RealizedMTM { get; set; }
            public string MTM { get; set; }
            public string BEP { get; set; }
            public string SumOfTradedQuantityAndPriceBuy { get; set; }
            public string SumOfTradedQuantityAndPriceSell { get; set; }
            public string statisticsLevel { get; set; }
            public string isInterOpPosition { get; set; }
        }
    }


    public class PositionConvertRequest
    {
        public string exchangeSegment { get; set; }
        public int exchangeInstrumentID { get; set; }
        public string oldProductType { get; set; }
        public string newProductType { get; set; }
        public bool isDayWise { get; set; }
        public int targetQty { get; set; }
        public string statisticsLevel { get; set; }
        public bool isInterOpPosition { get; set; }
    }


    public class PositionConvertResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
    }


    //public class ExchangeStatusResponse
    //{
    //    public string type { get; set; }
    //    public string code { get; set; }
    //    public string description { get; set; }
    //    public Result[] result { get; set; }

    //    public class Result
    //    {
    //        public string exchangeSegment { get; set; }
    //        public string exchangeMarketType { get; set; }
    //        public string exchangeTradingSession { get; set; }
    //    }
    //}






    public class ExchangeStatusResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public ExchangeStatusResponseResult result { get; set; }
    }

    public class ExchangeStatusResponseResult
    {
        public Marketstatus[] marketStatus { get; set; }
    }

    public class Marketstatus
    {
        public string exchangeSegment { get; set; }
        public string exchangeMarketType { get; set; }
        public string exchangeTradingSession { get; set; }
    }





    //get from after execution

    public class ExchangeMessages
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public ExchangeMessagesResult result { get; set; }
    }
    public class ExchangeMessagesResult
    {
        public ExchangeMessageslist[] messageList { get; set; }
    }
    public class ExchangeMessageslist
    {
        public int ExchangeSegment { get; set; }
        public int ExchangeTimeStamp { get; set; }
        public string BroadcastMessage { get; set; }
        public long SequenceId { get; set; }
        public int ExchangeInstrumentID { get; set; }
        public int MessageCode { get; set; }
        public int MessageVersion { get; set; }
        public int TokenID { get; set; }
        public int ApplicationType { get; set; }
        public long SequenceNumber { get; set; }
    }


}
