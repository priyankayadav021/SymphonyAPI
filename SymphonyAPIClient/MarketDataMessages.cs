using Newtonsoft.Json.Linq;

namespace SymphonyAPI
{


    //public class marketLoginResponse
    //{
    //    public marketLoginResponseStatus[] status { get; set; }
    //}

    //public class marketLoginResponseStatus
    //{
    //    public string type { get; set; }
    //    public string code { get; set; }
    //    public string description { get; set; }
    //    public marketLoginResponseInfo info { get; set; }
    //}

    //public class marketLoginResponseInfo
    //{
    //    public string token { get; set; }
    //    public string userID { get; set; }
    //    public string appVersion { get; set; }
    //}




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




}
