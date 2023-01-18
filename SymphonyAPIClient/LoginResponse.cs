namespace SymphonyAPI
{

    public class loginRequest
    {
        public string secretKey { get; set; }
        public string appKey { get; set; }
        public string source { get; set; }
    }


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



    public class logOffResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public logOffResponseResult result { get; set; }
    }

    public class logOffResponseResult
    {
    }



    public class userBalanceResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public userBalanceResult result { get; set; }
    }

    public class userBalanceResult
    {
        public Balancelist[] BalanceList { get; set; }
    }

    public class Balancelist
    {
        public string limitHeader { get; set; }
        public Limitobject limitObject { get; set; }
    }

    public class Limitobject
    {
        public Rmssublimits RMSSubLimits { get; set; }
        public Marginavailable marginAvailable { get; set; }
        public Marginutilized marginUtilized { get; set; }
        public Limitsassigned limitsAssigned { get; set; }
        public string AccountID { get; set; }
    }

    public class Rmssublimits
    {
        public string cashAvailable { get; set; }
        public int collateral { get; set; }
        public string marginUtilized { get; set; }
        public string netMarginAvailable { get; set; }
        public string MTM { get; set; }
        public string UnrealizedMTM { get; set; }
        public string RealizedMTM { get; set; }
    }

    public class Marginavailable
    {
        public string CashMarginAvailable { get; set; }
        public string AdhocMargin { get; set; }
        public string NotinalCash { get; set; }
        public string PayInAmount { get; set; }
        public string PayOutAmount { get; set; }
        public string CNCSellBenifit { get; set; }
        public string DirectCollateral { get; set; }
        public string HoldingCollateral { get; set; }
        public string ClientBranchAdhoc { get; set; }
        public string SellOptionsPremium { get; set; }
        public string NetOptionPremium { get; set; }
        public string BuyOptionsPremium { get; set; }
        public string TotalBranchAdhoc { get; set; }
        public string AdhocFOMargin { get; set; }
        public string AdhocCurrencyMargin { get; set; }
        public string AdhocCommodityMargin { get; set; }
    }

    public class Marginutilized
    {
        public string GrossExposureMarginPresent { get; set; }
        public string BuyExposureMarginPresent { get; set; }
        public string SellExposureMarginPresent { get; set; }
        public string VarELMarginPresent { get; set; }
        public string ScripBasketMarginPresent { get; set; }
        public string GrossExposureLimitPresent { get; set; }
        public string BuyExposureLimitPresent { get; set; }
        public string SellExposureLimitPresent { get; set; }
        public string CNCLimitUsed { get; set; }
        public string CNCAmountUsed { get; set; }
        public string MarginUsed { get; set; }
        public string LimitUsed { get; set; }
        public string TotalSpanMargin { get; set; }
        public string ExposureMarginPresent { get; set; }
    }

    public class Limitsassigned
    {
        public string CNCLimit { get; set; }
        public string TurnoverLimitPresent { get; set; }
        public string MTMLossLimitPresent { get; set; }
        public string BuyExposureLimit { get; set; }
        public string SellExposureLimit { get; set; }
        public string GrossExposureLimit { get; set; }
        public string GrossExposureDerivativesLimit { get; set; }
        public string BuyExposureFuturesLimit { get; set; }
        public string BuyExposureOptionsLimit { get; set; }
        public string SellExposureOptionsLimit { get; set; }
        public string SellExposureFuturesLimit { get; set; }
    }





    public class userProfileResponse
    {
        public string type { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public userProfileResult result { get; set; }
    }

    public class userProfileResult
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string PAN { get; set; }
        public bool IncludeInAutoSquareoff { get; set; }
        public bool IncludeInAutoSquareoffBlocked { get; set; }
        public bool IsProClient { get; set; }
        public bool IsInvestorClient { get; set; }
        public string ResidentialAddress { get; set; }
        public string OfficeAddress { get; set; }
        public Userbankinfolist[] ClientBankInfoList { get; set; }
        public Userexchangedetailslist ClientExchangeDetailsList { get; set; }
    }

    public class Userexchangedetailslist
    {
        public UserNSECM NSECM { get; set; }
        public UserNSEFO NSEFO { get; set; }
        public UserNSECD NSECD { get; set; }
        public UserMCXFO MCXFO { get; set; }
    }

    public class UserNSECM
    {
        public string ClientId { get; set; }
        public int ExchangeSegNumber { get; set; }
        public bool Enabled { get; set; }
        public string ParticipantCode { get; set; }
    }

    public class UserNSEFO
    {
        public string ClientId { get; set; }
        public int ExchangeSegNumber { get; set; }
        public bool Enabled { get; set; }
        public string ParticipantCode { get; set; }
    }

    public class UserNSECD
    {
        public string ClientId { get; set; }
        public int ExchangeSegNumber { get; set; }
        public bool Enabled { get; set; }
        public string ParticipantCode { get; set; }
    }

    public class UserMCXFO
    {
        public string ClientId { get; set; }
        public int ExchangeSegNumber { get; set; }
        public bool Enabled { get; set; }
        public string ParticipantCode { get; set; }
    }

    public class Userbankinfolist
    {
        public string ClientId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankCity { get; set; }
        public string CustomerId { get; set; }
        public string BankCityPincode { get; set; }
        public string BankIFSCCode { get; set; }
    }


}
