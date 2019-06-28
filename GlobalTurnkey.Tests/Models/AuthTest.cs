using System;
using System.Collections.Generic;
using GlobalTurnkey.Models;
using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class AuthTest
    {
        [TestMethod]
        public void noExTestCall()
        {
            // TOKENIZE
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            TokenizeCall tokenizeCall = new TokenizeCall(new ApplicationConfig(), inputParams);
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

            AuthCall call = new AuthCall(new ApplicationConfig(), authParams);
            Dictionary<String, String> result = call.execute();

            
            Assert.IsNotNull(result);

        }
    }
}
