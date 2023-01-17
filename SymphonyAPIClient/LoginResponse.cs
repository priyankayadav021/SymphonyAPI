namespace SymphonyAPI
{

    public class loginResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public Enums enums { get; set; }
        public string[] clientCodes { get; set; }
        public Exchangesegmentarray[] exchangeSegmentArray { get; set; }
        public string token { get; set; }
        public string userID { get; set; }
        public bool isInvestorClient { get; set; }
        public bool isOneTouchUser { get; set; }
    }

    public class Enums
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

    public class Exchangeinfo
    {
        public NSECM NSECM { get; set; }
        public NSEFO NSEFO { get; set; }
        public NSECD NSECD { get; set; }
        public MCXFO MCXFO { get; set; }
    }

    public class NSECM
    {
        public Producttype productType { get; set; }
        public string[] orderType { get; set; }
        public string[] timeInForce { get; set; }
    }

    public class Producttype
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _4 { get; set; }
        public int MIS { get; set; }
        public int CNC { get; set; }
        public int NRML { get; set; }
    }

    public class NSEFO
    {
        public Producttype1 productType { get; set; }
        public string[] orderType { get; set; }
        public string[] timeInForce { get; set; }
    }

    public class Producttype1
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public int MIS { get; set; }
        public int NRML { get; set; }
    }

    public class NSECD
    {
        public Producttype2 productType { get; set; }
        public string[] orderType { get; set; }
        public string[] timeInForce { get; set; }
    }

    public class Producttype2
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public int MIS { get; set; }
        public int NRML { get; set; }
    }

    public class MCXFO
    {
        public Producttype3 productType { get; set; }
        public string[] orderType { get; set; }
        public string[] timeInForce { get; set; }
    }

    public class Producttype3
    {
        public string _1 { get; set; }
        public string _2 { get; set; }
        public int MIS { get; set; }
        public int NRML { get; set; }
    }

    public class Exchangesegmentarray
    {
        public string key { get; set; }
        public string value { get; set; }
    }

}
