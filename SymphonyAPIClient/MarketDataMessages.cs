using Newtonsoft.Json.Linq;

namespace SymphonyAPI
{



    public class marketLoginResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public marketLoginResponseResult result { get; set; }
    }

    public class marketLoginResponseResult
    {
        public marketINFO enums { get; set; }
        public string[] clientCodes { get; set; }
        public Exchangesegmentarray[] exchangeSegmentArray { get; set; }
        public string token { get; set; }
        public string userID { get; set; }
        public bool isInvestorClient { get; set; }
        public bool isOneTouchUser { get; set; }
    }

    public class marketINFO
    {
        public string[] socketEvent { get; set; }
        public string[] orderSide { get; set; }
        public string[] orderSource { get; set; }
        public string[] positionSqureOffMode { get; set; }
        public string[] positionSquareOffQuantityType { get; set; }
        public string[] dayOrNet { get; set; }
        public string[] instrumentType { get; set; }
        public string[] exchangeSegment { get; set; }
        public Exchangeinfo exchangeInfo { get; set; }
    }




    public class ClientConfigResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public ClientConfigResponseResult result { get; set; }
    }

    public class ClientConfigResponseResult
    {
        public Exchangesegments exchangeSegments { get; set; }
        public Xtsmessagecode xtsMessageCode { get; set; }
        public string[] publishFormat { get; set; }
        public string[] broadCastMode { get; set; }
        public Instrumenttype instrumentType { get; set; }
    }

    public class Exchangesegments
    {
        public int NSECM { get; set; }
        public int NSEFO { get; set; }
        public int NSECD { get; set; }
        public int NSECO { get; set; }
        public int SLBM { get; set; }
        public int NIFSC { get; set; }
        public int BSECM { get; set; }
        public int BSEFO { get; set; }
        public int BSECD { get; set; }
        public int BSECO { get; set; }
        public int NCDEX { get; set; }
        public int MSECM { get; set; }
        public int MSEFO { get; set; }
        public int MSECD { get; set; }
        public int MCXFO { get; set; }
    }

    public class Xtsmessagecode
    {
        public int touchlineEvent { get; set; }
        public int marketDepthEvent { get; set; }
        public int indexDataEvent { get; set; }
        public int candleDataEvent { get; set; }
        public int openInterestEvent { get; set; }
        public int instrumentPropertyChangeEvent { get; set; }
        public int ltpEvent { get; set; }
    }

    public class Instrumenttype
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _4 { get; set; }
        public string _8 { get; set; }
        public string _16 { get; set; }
        public string _32 { get; set; }
        public string _64 { get; set; }
        public string _128 { get; set; }
        public string _256 { get; set; }
        public string _512 { get; set; }
        public int Futures { get; set; }
        public int Options { get; set; }
        public int Spread { get; set; }
        public int Equity { get; set; }
        public int Spot { get; set; }
        public int PreferenceShares { get; set; }
        public int Debentures { get; set; }
        public int Warrants { get; set; }
        public int Miscellaneous { get; set; }
        public int MutualFund { get; set; }
    }




    public class GetSeriesResponse
    {
        public GetSeriesResponseClass[] Property1 { get; set; }
    }

    public class GetSeriesResponseClass
    {
        public GetSeriesResponseResult result { get; set; }
    }

    public class GetSeriesResponseResult
    {
        public string[] _1 { get; set; }
    }




    public class marketSessionEnd
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public marketSessionEndResult result { get; set; }
    }

    public class marketSessionEndResult
    {
    }


    public class QuoteRequest
    {
        public Instrument[] instruments { get; set; }
        public int xtsMessageCode { get; set; }
        public string publishFormat { get; set; }
    }

    public class Instrument
    {
        public int exchangeSegment { get; set; }
        public int exchangeInstrumentID { get; set; }
    }



    public class QuoteResponse
    {
        public QuoteResponseClass[] Property { get; set; }
    }

    public class QuoteResponseClass
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public QuoteResponseResult result { get; set; }
    }

    public class QuoteResponseResult
    {
        public int mdp { get; set; }
        public Quotelist quoteList { get; set; }
        public string listQuotes { get; set; }
    }

    public class Quotelist
    {
        public int exchangeSegment { get; set; }
        public int exchangeInstrumentID { get; set; }
    }




    public class GetEquitySymbol
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public GetEquitySymbolResult[] result { get; set; }
    }

    public class GetEquitySymbolResult
    {
        public int ExchangeSegment { get; set; }
        public int ExchangeInstrumentID { get; set; }
        public int InstrumentType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Series { get; set; }
        public string NameWithSeries { get; set; }
        public long InstrumentID { get; set; }
        public Priceband PriceBand { get; set; }
        public int FreezeQty { get; set; }
        public float TickSize { get; set; }
        public int LotSize { get; set; }
    }

    public class Priceband
    {
        public float High { get; set; }
        public float Low { get; set; }
        public string HighString { get; set; }
        public string LowString { get; set; }
        public string CreditRating { get; set; }
        public string HighExecBandString { get; set; }
        public string LowExecBandString { get; set; }
        public int HighExecBand { get; set; }
        public int LowExecBand { get; set; }
        public string TERRange { get; set; }
    }





    public class GetExpiryDate
    {
        public DateTime[] result { get; set; }
    }



    public class GetFutureSymbol
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public GetFutureSymbolResult[] result { get; set; }
    }

    public class GetFutureSymbolResult
    {
        public int ExchangeSegment { get; set; }
        public int ExchangeInstrumentID { get; set; }
        public int InstrumentType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Series { get; set; }
        public long InstrumentID { get; set; }
        public Priceband PriceBand { get; set; }
        public int FreezeQty { get; set; }
        public float TickSize { get; set; }
        public int LotSize { get; set; }
        public int UnderlyingInstrumentId { get; set; }
        public string UnderlyingIndexName { get; set; }
        public DateTime ContractExpiration { get; set; }
        public string ContractExpirationString { get; set; }
        public int RemainingExpiryDays { get; set; }
    }






    public class GetOptionSymbol
    {
        public GetOptionSymbolClass1[] Property1 { get; set; }
    }

    public class GetOptionSymbolClass1
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public GetOptionSymbolResult result { get; set; }
    }

    public class GetOptionSymbolResult
    {
        public int ExchangeSegment { get; set; }
        public int ExchangeInstrumentID { get; set; }
        public int InstrumentType { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Series { get; set; }
        public long InstrumentID { get; set; }
        public Priceband PriceBand { get; set; }
        public int FreezeQty { get; set; }
        public float TickSize { get; set; }
        public int LotSize { get; set; }
        public int UnderlyingInstrumentId { get; set; }
        public string UnderlyingIndexName { get; set; }
        public DateTime ContractExpiration { get; set; }
        public string ContractExpirationString { get; set; }
        public int RemainingExpiryDays { get; set; }
        public int StrikePrice { get; set; }
        public int OptionType { get; set; }
    }





    public class GetOptionType
    {
        public object[] result { get; set; }
    }





    public class IndexList
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public IndexListResult result { get; set; }
    }

    public class IndexListResult
    {
        public int exchangeSegment { get; set; }
        public string[] indexList { get; set; }
    }

}
