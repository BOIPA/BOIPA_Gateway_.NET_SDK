using System;
using System.Collections.Generic;
using GlobalTurnkey.Models;
using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class CaptureTest
    {
        [TestMethod]
        public void noExTestCall()
        {
            // TOKENIZE
            Dictionary<String, String> tokenizeParams = new Dictionary<string, string>();
            tokenizeParams.Add("number", "5424180279791732");
            tokenizeParams.Add("nameOnCard", "mastercard");
            tokenizeParams.Add("expiryYear", "2021");
            tokenizeParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(new ApplicationConfig(), tokenizeParams);
            Dictionary<String, String> tokenizeResult = tokenizeCall.execute();

            Dictionary<String, String> authParams = new Dictionary<String, String>();
            authParams.Add("amount", "20.0");
            authParams.Add("channel", Channel.ECOM.getCode());
            authParams.Add("country", CountryCode.GB.getCode());
            authParams.Add("currency", CurrencyCode.EUR.getCode());
            authParams.Add("paymentSolutionId", "500");
            authParams.Add("customerId", tokenizeResult["customerId"]);
            authParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            authParams.Add("specinCreditCardCVV", "111");

            AuthCall authCall = new AuthCall(new ApplicationConfig(), authParams);
            Dictionary<String, String> authResult = authCall.execute();

            // CAPTURE
            Dictionary<String, String> inputParams = new Dictionary<String, String>();
            inputParams.Add("originalMerchantTxId", authResult["merchantTxId"]);
            inputParams.Add("amount", "20.0");

            CaptureCall call = new CaptureCall(new ApplicationConfig(), inputParams);
            Dictionary<String, String> result = call.execute();

            Assert.IsNotNull(result);
        }
    }
}
