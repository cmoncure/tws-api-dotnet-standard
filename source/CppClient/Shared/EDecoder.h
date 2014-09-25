#pragma once
//const int MIN_SERVER_VER_REAL_TIME_BARS       = 34;
//const int MIN_SERVER_VER_SCALE_ORDERS         = 35;
//const int MIN_SERVER_VER_SNAPSHOT_MKT_DATA    = 35;
//const int MIN_SERVER_VER_SSHORT_COMBO_LEGS    = 35;
//const int MIN_SERVER_VER_WHAT_IF_ORDERS       = 36;
//const int MIN_SERVER_VER_CONTRACT_CONID       = 37;
const int MIN_SERVER_VER_PTA_ORDERS             = 39;
const int MIN_SERVER_VER_FUNDAMENTAL_DATA       = 40;
const int MIN_SERVER_VER_UNDER_COMP             = 40;
const int MIN_SERVER_VER_CONTRACT_DATA_CHAIN    = 40;
const int MIN_SERVER_VER_SCALE_ORDERS2          = 40;
const int MIN_SERVER_VER_ALGO_ORDERS            = 41;
const int MIN_SERVER_VER_EXECUTION_DATA_CHAIN   = 42;
const int MIN_SERVER_VER_NOT_HELD               = 44;
const int MIN_SERVER_VER_SEC_ID_TYPE            = 45;
const int MIN_SERVER_VER_PLACE_ORDER_CONID      = 46;
const int MIN_SERVER_VER_REQ_MKT_DATA_CONID     = 47;
const int MIN_SERVER_VER_REQ_CALC_IMPLIED_VOLAT = 49;
const int MIN_SERVER_VER_REQ_CALC_OPTION_PRICE  = 50;
const int MIN_SERVER_VER_CANCEL_CALC_IMPLIED_VOLAT = 50;
const int MIN_SERVER_VER_CANCEL_CALC_OPTION_PRICE  = 50;
const int MIN_SERVER_VER_SSHORTX_OLD            = 51;
const int MIN_SERVER_VER_SSHORTX                = 52;
const int MIN_SERVER_VER_REQ_GLOBAL_CANCEL      = 53;
const int MIN_SERVER_VER_HEDGE_ORDERS			= 54;
const int MIN_SERVER_VER_REQ_MARKET_DATA_TYPE	= 55;
const int MIN_SERVER_VER_OPT_OUT_SMART_ROUTING  = 56;
const int MIN_SERVER_VER_SMART_COMBO_ROUTING_PARAMS = 57;
const int MIN_SERVER_VER_DELTA_NEUTRAL_CONID    = 58;
const int MIN_SERVER_VER_SCALE_ORDERS3          = 60;
const int MIN_SERVER_VER_ORDER_COMBO_LEGS_PRICE = 61;
const int MIN_SERVER_VER_TRAILING_PERCENT       = 62;
const int MIN_SERVER_VER_DELTA_NEUTRAL_OPEN_CLOSE = 66;
const int MIN_SERVER_VER_POSITIONS              = 67;
const int MIN_SERVER_VER_ACCOUNT_SUMMARY        = 67;
const int MIN_SERVER_VER_TRADING_CLASS          = 68;
const int MIN_SERVER_VER_SCALE_TABLE            = 69;
const int MIN_SERVER_VER_LINKING                = 70;
const int MIN_SERVER_VER_ALGO_ID                = 71;
const int MIN_SERVER_VER_OPTIONAL_CAPABILITIES  = 72;
const int MIN_SERVER_VER_ORDER_SOLICITED        = 73;
const int MIN_SERVER_VER_LINKING_AUTH           = 74;
const int MIN_SERVER_VER_PRIMARYEXCH            = 75;

// incoming msg id's
const int TICK_PRICE                = 1;
const int TICK_SIZE                 = 2;
const int ORDER_STATUS              = 3;
const int ERR_MSG                   = 4;
const int OPEN_ORDER                = 5;
const int ACCT_VALUE                = 6;
const int PORTFOLIO_VALUE           = 7;
const int ACCT_UPDATE_TIME          = 8;
const int NEXT_VALID_ID             = 9;
const int CONTRACT_DATA             = 10;
const int EXECUTION_DATA            = 11;
const int MARKET_DEPTH              = 12;
const int MARKET_DEPTH_L2           = 13;
const int NEWS_BULLETINS            = 14;
const int MANAGED_ACCTS             = 15;
const int RECEIVE_FA                = 16;
const int HISTORICAL_DATA           = 17;
const int BOND_CONTRACT_DATA        = 18;
const int SCANNER_PARAMETERS        = 19;
const int SCANNER_DATA              = 20;
const int TICK_OPTION_COMPUTATION   = 21;
const int TICK_GENERIC              = 45;
const int TICK_STRING               = 46;
const int TICK_EFP                  = 47;
const int CURRENT_TIME              = 49;
const int REAL_TIME_BARS            = 50;
const int FUNDAMENTAL_DATA          = 51;
const int CONTRACT_DATA_END         = 52;
const int OPEN_ORDER_END            = 53;
const int ACCT_DOWNLOAD_END         = 54;
const int EXECUTION_DATA_END        = 55;
const int DELTA_NEUTRAL_VALIDATION  = 56;
const int TICK_SNAPSHOT_END         = 57;
const int MARKET_DATA_TYPE          = 58;
const int COMMISSION_REPORT         = 59;
const int POSITION_DATA             = 61;
const int POSITION_END              = 62;
const int ACCOUNT_SUMMARY           = 63;
const int ACCOUNT_SUMMARY_END       = 64;
const int VERIFY_MESSAGE_API        = 65;
const int VERIFY_COMPLETED          = 66;
const int DISPLAY_GROUP_LIST        = 67;
const int DISPLAY_GROUP_UPDATED     = 68;
const int VERIFY_AND_AUTH_MESSAGE_API = 69;
const int VERIFY_AND_AUTH_COMPLETED   = 70;

// helper structures
namespace {

struct BarData {
	std::string date;
	double open;
	double high;
	double low;
	double close;
	int volume;
	double average;
	std::string hasGaps;
	int barCount;
};

struct ScanData {
	ContractDetails contract;
	int rank;
	std::string distance;
	std::string benchmark;
	std::string projection;
	std::string legsStr;
};

} // end of anonymous namespace

///////////////////////////////////////////////////////////
// utility funcs
static std::string errMsg(std::exception e) {
	// return the error associated with this exception
	return std::string(e.what());
}

class EWrapper;
class EClient;

class EDecoder
{
    EWrapper *m_pEWrapper;
    EClient *m_pEClient;
    int m_serverVersion;

    int processTickPriceMsg(const char* ptr, const char* endPtr);
    int processTickSizeMsg(const char* ptr, const char* endPtr);
    int processTickOptionComputationMsg(const char* ptr, const char* endPtr);
    int processTickGenericMsg(const char* ptr, const char* endPtr);
    int processTickStringMsg(const char* ptr, const char* endPtr);
    int processTickEfpMsg(const char* ptr, const char* endPtr);
    int processOrderStatusMsg(const char* ptr, const char* endPtr);
    int processErrMsgMsg(const char* ptr, const char* endPtr);
    int processOpenOrderMsg(const char* ptr, const char* endPtr);
    int processAcctValueMsg(const char* ptr, const char* endPtr);
    int processPortfolioValueMsg(const char* ptr, const char* endPtr);
    int processAcctUpdateTimeMsg(const char* ptr, const char* endPtr);
    int processNextValidIdMsg(const char* ptr, const char* endPtr);
    int processContractDataMsg(const char* ptr, const char* endPtr);
    int processBondContractDataMsg(const char* ptr, const char* endPtr);
    int processExecutionDataMsg(const char* ptr, const char* endPtr);
    int processMarketDepthMsg(const char* ptr, const char* endPtr);
    int processMarketDepthL2Msg(const char* ptr, const char* endPtr);
    int processNewsBulletinsMsg(const char* ptr, const char* endPtr);
    int processManagedAcctsMsg(const char* ptr, const char* endPtr);
    int processReceiveFaMsg(const char* ptr, const char* endPtr);
    int processHistoricalDataMsg(const char* ptr, const char* endPtr);
    int processScannerDataMsg(const char* ptr, const char* endPtr);
    int processScannerParametersMsg(const char* ptr, const char* endPtr);
    int processCurrentTimeMsg(const char* ptr, const char* endPtr);
    int processRealTimeBarsMsg(const char* ptr, const char* endPtr);
    int processFundamentalDataMsg(const char* ptr, const char* endPtr);
    int processContractDataEndMsg(const char* ptr, const char* endPtr);
    int processOpenOrderEndMsg(const char* ptr, const char* endPtr);
    int processAcctDownloadEndMsg(const char* ptr, const char* endPtr);
    int processExecutionDataEndMsg(const char* ptr, const char* endPtr);
    int processDeltaNeutralValidationMsg(const char* ptr, const char* endPtr);
    int processTickSnapshotEndMsg(const char* ptr, const char* endPtr);
    int processMarketDataTypeMsg(const char* ptr, const char* endPtr);
    int processCommissionReportMsg(const char* ptr, const char* endPtr);
    int processPositionDataMsg(const char* ptr, const char* endPtr);
    int processPositionEndMsg(const char* ptr, const char* endPtr);
    int processAccountSummaryMsg(const char* ptr, const char* endPtr);
    int processAccountSummaryEndMsg(const char* ptr, const char* endPtr);
    int processVerifyMessageApiMsg(const char* ptr, const char* endPtr);
    int processVerifyCompletedMsg(const char* ptr, const char* endPtr);
    int processDisplayGroupListMsg(const char* ptr, const char* endPtr);
    int processDisplayGroupUpdatedMsg(const char* ptr, const char* endPtr);
    int processVerifyAndAuthMessageApiMsg(const char* ptr, const char* endPtr);
    int processVerifyAndAuthCompletedMsg(const char* ptr, const char* endPtr);

public:
	static bool CheckOffset(const char* ptr, const char* endPtr);
	static const char* FindFieldEnd(const char* ptr, const char* endPtr);
	// decoders
	static bool DecodeField(bool&, const char*& ptr, const char* endPtr);
	static bool DecodeField(int&, const char*& ptr, const char* endPtr);
	static bool DecodeField(long&, const char*& ptr, const char* endPtr);
	static bool DecodeField(double&, const char*& ptr, const char* endPtr);
	static bool DecodeField(std::string&, const char*& ptr, const char* endPtr);

	static bool DecodeFieldMax(int&, const char*& ptr, const char* endPtr);
	static bool DecodeFieldMax(long&, const char*& ptr, const char* endPtr);
	static bool DecodeFieldMax(double&, const char*& ptr, const char* endPtr);

    EDecoder(EClient *parent, EWrapper *callback, int serverVersion);

    int parseAndProcessMsg(const char*& beginPtr, const char* endPtr);
};

#define DECODE_FIELD(x) if (!EDecoder::DecodeField(x, ptr, endPtr)) return 0;
#define DECODE_FIELD_MAX(x) if (!EDecoder::DecodeFieldMax(x, ptr, endPtr)) return 0;
