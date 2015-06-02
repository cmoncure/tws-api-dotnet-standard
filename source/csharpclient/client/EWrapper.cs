﻿/* Copyright (C) 2013 Interactive Brokers LLC. All rights reserved.  This code is subject to the terms
 * and conditions of the IB API Non-Commercial License or the IB API Commercial License, as applicable. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBApi
{
    /**
     * @interface EWrapper
     * @brief This interface's methods are used by the TWS/Gateway to communicate with the API client.
     * Every API client application needs to implement this interface in order to handle all the events generated by the TWS/Gateway. Almost every EClientSocket method call will result in at least one event delivered here.
     * @sa EClientSocket class
     */
    public interface EWrapper
    {
        /** @brief Handles errors generated within the API itself.
         * If an exception is thrown within the API code it will be notified here. Posible cases include errors while reading the information from the socket or even misshandlings at EWrapper's implementing class.
         * @param e the thrown exception.
         */
        void error(Exception e);
        
        /**
         * @param str The error message received.
         * 
         */
        void error(string str);
        
        /**
         * @brief Errors sent by the TWS are received here.
         * @param id the request identifier which generated the error.
         * @param errorCode the code identifying the error.
         * @param errorMsg error's description.
         *  
         */
        void error(int id, int errorCode, string errorMsg);

        /**
         * @brief Server's current time
         * This method will receive IB server's system time resulting after the invokation of reqCurrentTime
         * @sa reqCurrentTime()
         */
        void currentTime(long time);

        /**
         * @brief Market data tick price callback.
         * Handles all price related ticks.
         * @param tickerId the request's unique identifier.
         * @param field the type of the price being received (i.e. ask price).
         * @param price the actual price.
         * @param canAutoExecute Specifies whether the price tick is available for automatic execution (1) or not (0).
         * @sa TickType, tickSize, tickString, tickEFP, tickGeneric, tickOptionComputation, tickSnapshotEnd, marketDataType, EClientSocket::reqMktData
         */
        void tickPrice(int tickerId, int field, double price, int canAutoExecute);

        /**
         * @brief Market data tick size callback.
         * Handles all size-related ticks.
         * @param tickerId the request's unique identifier.
         * @param field the type of size being received (i.e. bid size)
         * @param size the actual size.
         * @see reqMarketData()
         * @sa TickType, tickPrice, tickString, tickEFP, tickGeneric, tickOptionComputation, tickSnapshotEnd, marketDataType, EClientSocket::reqMktData
         */
        void tickSize(int tickerId, int field, int size);

        /**
         * @brief Market data callback.
         * @param tickerId the request's unique identifier.
         * @param field the type of the tick being received
         * @param value
         * @sa TickType, tickSize, tickPrice, tickEFP, tickGeneric, tickOptionComputation, tickSnapshotEnd, marketDataType, EClientSocket::reqMktData
         */
        void tickString(int tickerId, int field, string value);

        /**
         * @brief Market data callback.
         * @param tickerId the request's unique identifier.
         * @param field the type of tick being received.
         * @param value
         */
        void tickGeneric(int tickerId, int field, double value);

        /**
         * @brief Exchange for Physicals.
         * @param tickerId The request's identifier.
         * @param tickType The type of tick being received.
         * @param basisPoints Annualized basis points, which is representative of the financing rate that can be directly compared to broker rates.
         * @param formattedBasisPoints Annualized basis points as a formatted string that depicts them in percentage form.
         * @param impliedFuture The implied Futures price.
         * @param holdDays The number of hold days until the lastTradeDate of the EFP.
         * @param futureLastTradeDate The expiration date of the single stock future.
         * @param dividendImpact The dividend impact upon the annualized basis points interest rate.
         * @param dividendsToLastTradeDate The dividends expected until the expiration of the single stock future.
         */
        void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays,  string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate);

        /**
         * @brief -
         * Upon accepting a Delta-Neutral DN RFQ(request for quote), the server sends a deltaNeutralValidation() message with the 
         * UnderComp structure. If the delta and price fields are empty in the original request, the confirmation will contain the current
         * values from the server. These values are locked when RFQ is processed and remain locked unitl the RFQ is cancelled.
         * @param reqId the request's identifier.
         * @param underComp Underlying Component
         */
        void deltaNeutralValidation(int reqId, UnderComp underComp);

        /**
         * @brief Receive's option specific market data.
         * This method is called when the market in an option or its underlier moves. TWS’s option model volatilities, prices, and deltas, along with the present value of dividends expected on that options underlier are received.
         * @sa TickType, tickSize, tickPrice, tickEFP, tickGeneric, tickString, tickSnapshotEnd, marketDataType, EClientSocket::reqMktData
         */
        void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice);

        /**
         * @brief When requesting market data snapshots, this market will indicate the snapshot reception is finished.
         * 
         */
        void tickSnapshotEnd(int tickerId);

        /**
         * @brief Receives next valid order id.
         * @param orderId the next order id
         * @sa EClientSocket::reqIds
         */
        void nextValidId(int orderId);

        /**
         * @brief Receives a comma-separated string with the managed account ids
         * @sa EClientSocket::reqManagedAccts
         */
        void managedAccounts(string accountsList);

        /**
         * @brief Notifes when the API-TWS connectivity has been closed.
         * @sa EClientSocket::eDisconnect
         */
        void connectionClosed();

        /**
         * @brief Receives the account information.
         * This method will receive the account information just as it appears in the TWS' Account Summary Window.
         * @param reqId the request's unique identifier.
         * @param account the account id
         * @param tag the account's attribute being received.
         * @param value the account's attribute's value.
         * @param currency the currency on which the value is expressed.
         * @sa accountSummaryEnd, EClientSocket::reqAccountSummary
         */
        void accountSummary(int reqId, string account, string tag, string value, string currency);

        /**
         * @brief notifies when all the accounts' information has ben received.
         * @param reqId the request's identifier.
         * @sa accountSummary, EClientSocket::reqAccountSummary
         */
        void accountSummaryEnd(int reqId);

        /*
         * @brief Delivers the Bond contract data after this has been requested via reqContractDetails
         * @param reqId the request's identifier
         * @param contract the bond contract's information.
         * @sa reqContractDetails
         */
        void bondContractDetails(int reqId, ContractDetails contract);

        /**
         * @brief Receives the subscribed account's information.
         * Only one account can be subscribed at a time.
         * @param key the value being updated.
         * @param value up-to-date value
         * @param currency the currency on which the value is expressed.
         * @param accountName the account
         * @sa updatePortfolio, updateAccountTime, accountDownloadEnd, EClientSocket::reqAccountUpdates
         */
        void updateAccountValue(string key, string value, string currency, string accountName);

        /**
         * @brief Receives the subscribed account's portfolio.
         * This function will receive only the portfolio of the subscribed account. If the portfolios of all managed accounts are needed, refer to EClientSocket::reqPosition
         * @param contract the Contract for which a position is held.
         * @param position the number of positions held.
         * @param marketPrice instrument's unitary price
         * @param marketValue total market value of the instrument.
         * @sa updateAccountTime, accountDownloadEnd, updateAccountValue, EClientSocket::reqAccountUpdates
         */
        void updatePortfolio(Contract contract, double position, double marketPrice, double marketValue,
            double averageCost, double unrealisedPNL, double realisedPNL, string accountName);

        /**
         * @brief Receives the last time on which the account was updated.
         * @param timestamp the last update system time.
         * @sa updatePortfolio, accountDownloadEnd, updateAccountValue, EClientSocket::reqAccountUpdates
         */
        void updateAccountTime(string timestamp);

        /**
         * @brief Notifies when all the account's information has finished.
         * @param account the account's id
         * @sa updateAccountTime, updatePortfolio, updateAccountValue, EClientSocket::reqAccountUpdates
         */
        void accountDownloadEnd(string account);

        /**
         * @brief Gives the up-to-date information of an order every time it changes.
         * @param orderId the order's client id.
         * @param status the current status of the order:
         *      PendingSubmit - indicates that you have transmitted the order, but have not yet received confirmation that it has been accepted by the order destination. NOTE: This order status is not sent by TWS and should be explicitly set by the API developer when an order is submitted.
         *      PendingCancel - indicates that you have sent a request to cancel the order but have not yet received cancel confirmation from the order destination. At this point, your order is not confirmed canceled. You may still receive an execution while your cancellation request is pending. NOTE: This order status is not sent by TWS and should be explicitly set by the API developer when an order is canceled.
         *      PreSubmitted - indicates that a simulated order type has been accepted by the IB system and that this order has yet to be elected. The order is held in the IB system until the election criteria are met. At that time the order is transmitted to the order destination as specified .
         *      Submitted - indicates that your order has been accepted at the order destination and is working.
         *      ApiCanceled - after an order has been submitted and before it has been acknowledged, an API client client can request its cancelation, producing this state.
         *      Cancelled - indicates that the balance of your order has been confirmed canceled by the IB system. This could occur unexpectedly when IB or the destination has rejected your order.
         *      Filled - indicates that the order has been completely filled.
         *      Inactive - indicates that the order has been accepted by the system (simulated orders) or an exchange (native orders) but that currently the order is inactive due to system, exchange or other issues.
         * @param filled number of filled positions.
         * @param remaining the remnant positions.
         * @param avgFillPrice average filling price.
         * @param permId the order's permId used by the TWs to identify orders.
         * @param parentId parent's id. Used for bracker and auto trailing stop orders.
         * @param lastFillPrice price at which the last positions were filled.
         * @param clientId API client which submitted the order.
         * @param whyHeld this field is used to identify an order held when TWS is trying to locate shares for a short sell. The value used to indicate this is 'locate'.
         * @sa openOrder, openOrderEnd, EClientSocket::placeOrder, EClientSocket::reqAllOpenOrders, EClientSocket::reqAutoOpenOrders
         */
        void orderStatus(int orderId, string status, int filled, int remaining, double avgFillPrice, 
            int permId, int parentId, double lastFillPrice, int clientId, string whyHeld);

        /**
         * @brief Feeds in currently open orders.
         * @param orderId the order's unique id
         * @param contract the order's Contract.
         * @param order the currently active Order.
         * @param orderState the order's OrderState
         * @sa orderStatus, openOrderEnd, EClientSocket::placeOrder, EClientSocket::reqAllOpenOrders, EClientSocket::reqAutoOpenOrders
         */
        void openOrder(int orderId, Contract contract, Order order, OrderState orderState);

        /**
         * @brief Notifies the end of the open orders' reception.
         * @sa orderStatus, openOrder, EClientSocket::placeOrder, EClientSocket::reqAllOpenOrders, EClientSocket::reqAutoOpenOrders
         */
        void openOrderEnd();

        /**
         * @brief receives the full contract's definitons
         * This method will return all contracts matching the requested via EClientSocket::reqContractDetails. For example, one can obtain the whole option chain with it.
         * @param reqId the unique request identifier
         * @param contractDetails the instrument's complete definition.        
         * @sa contractDetailsEnd, EClientSocket::reqContractDetails
         */
        void contractDetails(int reqId, ContractDetails contractDetails);

        /**
         * @brief After all contracts matching the request were returned, this method will mark the end of their reception. 
         * @param reqId the request's identifier
         * @sa contractDetails, EClientSocket::reqContractDetails
         */
        void contractDetailsEnd(int reqId);

        /**
         * @brief Provides the executions which happened in the last 24 hours.
         * @param reqId the request's identifier
         * @param contract the Contract of the Order
         * @param execution the Execution details.
         * @sa execDetailsEnd, commissionReport, EClientSocket::reqExecutions, Execution
         */
        void execDetails(int reqId, Contract contract, Execution execution);

        /**
         * @brief indicates the end of the Execution reception.
         * @param reqId the request's identifier
         * @sa execDetails, commissionReport, EClientSocket::reqExecutions
         */
        void execDetailsEnd(int reqId);

        /**
         * @brief provides the CommissionReport of an Execution
         * @sa execDetails, execDetailsEnd, EClientSocket::reqExecutions, CommissionReport
         */
        void commissionReport(CommissionReport commissionReport);

        /**
         * @brief returns Reuters' Fundamental data
         * @param reqId the request's identifier
         * @param data Reuthers xml-formatted fundamental data
         * @sa EClientSocket::reqFundamentalData
         */
        void fundamentalData(int reqId, string data);

        /**
         * @brief returns the requested historical data bars
         * @param reqId the request's identifier
         * @param date the bar's date and time (either as a yyyymmss hh:mm:ss formatted string or as system time according to the request)
         * @param open the bar's open point
         * @param high the bar's high point
         * @param low the bar's low point
         * @param close the bar's closing point
         * @param volume the bar's traded volume if available
         * @param count the number of trades during the bar's timespan (only available for TRADES).
         * @param WAP the bar's Weighted Average Price
         * @param hasGaps indicates if the data has gaps or not.
         * @sa EClientSocket::reqHistoricalData
         */
        void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps);

        /**
         * @brief Marks the ending of the historical bars reception.
         * 
         */
       void historicalDataEnd(int reqId, string start, string end);

       /**
        * @brief Returns the current market data type (frozen or real time streamed)
        * @param reqId the request's identifier
        * @param marketDataType 1 for real time, 2 for frozen
        * @sa EClientSocket::reqMarketDataType
        */
        void marketDataType(int reqId, int marketDataType);

        /**
         * @brief Returns the order book
         * @param tickerId the request's identifier
         * @param position the order book's row being updated
         * @param operation how to refresh the row:
         *      0 = insert (insert this new order into the row identified by 'position')·
         *      1 = update (update the existing order in the row identified by 'position')·
         *      2 = delete (delete the existing order at the row identified by 'position').
         * @param side 0 for ask, 1 for bid
         * @param price the order's price
         * @param size the order's size
         * @sa updateMktDepthL2, EClientSocket::reqMarketDepth
         */
        void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size);

        /**
         * @brief Returns the order book
         * @param tickerId the request's identifier
         * @param position the order book's row being updated
         * @param marketMaker the exchange holding the order
         * @param operation how to refresh the row:
         *      0 - insert (insert this new order into the row identified by 'position')·
         *      1 - update (update the existing order in the row identified by 'position')·
         *      2 - delete (delete the existing order at the row identified by 'position').
         * @param side 0 for ask, 1 for bid
         * @param price the order's price
         * @param size the order's size
         * @sa updateMktDepth, EClientSocket::reqMarketDepth
         */
        void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size);

        /**
         * @brief provides IB's bulletins
         * @param msgId the bulletin's identifier
         * @param msgType one of:
         *      1 - Regular news bulletin
         *      2 - Exchange no longer available for trading
         *      3 - Exchange is available for trading
         * @param message the message
         * @param origExchange the exchange where the message comes from.
         */
        void updateNewsBulletin(int msgId, int msgType, String message, String origExchange);

        /**
         * @brief provides the portfolio's open positions.
         * @param account the account holding the position.
         * @param contract the position's Contract
         * @param pos the number of positions held.
         * @Param avgCost the average cost of the position.
         * @sa positionEnd, EClientSocket::reqPositions
         */
        void position(string account, Contract contract, double pos, double avgCost);

        /**
         * @brief Indicates all the positions have been transmitted.
         * @sa position, reqPositions
         */
        void positionEnd();

        /**
         * @brief updates the real time 5 seconds bars
         * @param reqId the request's identifier
         * @param date the bar's date and time (either as a yyyymmss hh:mm:ss formatted string or as system time according to the request)
         * @param open the bar's open point
         * @param high the bar's high point
         * @param low the bar's low point
         * @param close the bar's closing point
         * @param volume the bar's traded volume if available
         * @param WAP the bar's Weighted Average Price
         * @param count the number of trades during the bar's timespan (only available for TRADES).
         * @sa EClientSocket::reqRealTimeBars
         */
        void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count);

        /**
         * @brief provides the xml-formatted parameters available to create a market scanner.
         * @param xml the xml-formatted string with the available parameters.
         * @sa scannerData, EClientSocket::reqScannerParameters
         */
        void scannerParameters(string xml);

        /**
         * @brief provides the data resulting from the market scanner request.
         * @param reqid the request's identifier.
         * @param rank the ranking within the response of this bar.
         * @param contractDetails the data's ContractDetails
         * @param distance according to query.
         * @param benchmark according to query.
         * @param projection according to query.
         * @param legStr describes the combo legs when the scanner is returning EFP
         * @sa scannerParameters, scannerDataEnd, EClientSocket::reqScannerSubscription
         */
        void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr);

        /**
         * @brief Indicates the scanner data reception has terminated.
         * @param reqId the request's identifier
         * @sa scannerParameters, scannerData, EClientSocket>>reqScannerSubscription
         */
        void scannerDataEnd(int reqId);

        /**
         * @brief receives the Financial Advisor's configuration available in the TWS
         * @param faDataType one of:
         *    1. Groups: offer traders a way to create a group of accounts and apply a single allocation method to all accounts in the group.
         *    2. Profiles: let you allocate shares on an account-by-account basis using a predefined calculation value.
         *    3. Account Aliases: let you easily identify the accounts by meaningful names rather than account numbers.
         * @param faXmlData the xml-formatted configuration
         * @sa EClientSocket::requestFA, EClientSocket::replaceFA
         */
        void receiveFA(int faDataType, string faXmlData);


        void verifyMessageAPI(string apiData);
        void verifyCompleted(bool isSuccessful, string errorText);
        void verifyAndAuthMessageAPI(string apiData, string xyzChallenge);
        void verifyAndAuthCompleted(bool isSuccessful, string errorText);
        void displayGroupList(int reqId, string groups);
        void displayGroupUpdated(int reqId, string contractInfo);
        void connectAck();
    }
}
