﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestEase;

namespace Prime.Plugins.Services.HitBtc
{
    [AllowAnyStatusCode]
    internal interface IHitBtcApi
    {
        /// <summary>
        /// Gets symbols with their characteristics supported by exchange.
        /// See https://hitbtc.com/api#symbols.
        /// </summary>
        /// <returns>List of currency symbols with their characteristics.</returns>
        [Get("/public/symbol")]
        Task<Response<HitBtcSchema.SymbolsResponse>> GetSymbolsAsync();

        /// <summary>
        /// Gets deposit address for specified currency. If does not exist, it will be created.
        /// See https://hitbtc.com/api#getaddress.
        /// </summary>
        /// <param name="currency">Currency code which deposit address is to be returned.</param>
        /// <returns>Deposit address of specified currency.</returns>
        [Get("/account/crypto/address/{currency}")]
        Task<Response<HitBtcSchema.DepositAddressResponse>> GetDepositAddressAsync([Path] string currency);

        /// <summary>
        /// Gets payment balances.
        /// See https://hitbtc.com/api#paymentbalance.
        /// </summary>
        /// <returns>Multi-currency balance of the main account.</returns>
        [Get("/account/balance")]
        Task<Response<HitBtcSchema.BalancesResponse>> GetBalancesAsync();

        /// <summary>
        /// Gets information about placed order.
        /// </summary>
        /// <param name="clientOrderId">The id of order.</param>
        /// <param name="wait"></param>
        /// <returns>Information about the order with specified id.</returns>
        [Get("/order/{clientOrderId}")]
        Task<Response<HitBtcSchema.ActiveOrderInfoResponse>> GetActiveOrderInfoAsync([Path] string clientOrderId, [Query] int? wait = null);

        /// <summary>
        /// Places crypto withdrawal request.
        /// </summary>
        /// <param name="body">Post parameters.</param>
        /// <returns></returns>
        [Post("/account/crypto/withdraw")]
        Task<Response<HitBtcSchema.WithdrawCryptoResponse>> WithdrawCryptoAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> body);

        /// <summary>
        /// Creates new order.
        /// For more details see https://api.hitbtc.com/#create-new-order.
        /// </summary>
        /// <param name="body">Post parameters.</param>
        /// <returns></returns>
        [Post("/order")]
        Task<Response<HitBtcSchema.ActiveOrderInfoResponse>> CreateNewOrderAsync([Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> body);

        /// <summary>
        /// Gets tickers for all currencies.
        /// See https://hitbtc.com/api#alltickers.
        /// </summary>
        /// <returns>Associative array of pair code and ticker data</returns>
        [Get("/public/ticker")]
        Task<Response<HitBtcSchema.TickersResponse>> GetAllTickersAsync();

        /// <summary>
        /// Get ticker for specified currency pair.
        /// See https://hitbtc.com/api#ticker.
        /// </summary>
        /// <param name="pairCode">Currency which ticker is to be returned.</param>
        /// <returns>Ticker data.</returns>
        [Get("/public/ticker/{pairCode}")]
        Task<Response<HitBtcSchema.TickerResponse>> GetTickerAsync([Path] string pairCode);
    }
}