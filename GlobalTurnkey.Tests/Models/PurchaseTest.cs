using System;
using System.Collections.Generic;
using GlobalTurnkey.Models;
using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class PurchaseTest
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

            // PURCHASE
            Dictionary<String, String> purchaseParams = new Dictionary<String, String>();
            purchaseParams.Add("amount", "20.0");
            purchaseParams.Add("channel", Channel.ECOM.getCode());
            purchaseParams.Add("country", CountryCode.GB.getCode());
            purchaseParams.Add("currency", CurrencyCode.EUR.getCode());
            purchaseParams.Add("paymentSolutionId", "500");
            purchaseParams.Add("customerId", tokenizeResult["customerId"]);
            purchaseParams.Add("specinCreditCardToken", tokenizeResult["cardToken"]);
            purchaseParams.Add("specinCreditCardCVV", "111");

            PurchaseCall call = new PurchaseCall(new ApplicationConfig(), purchaseParams);
            Dictionary<String, String> result = call.execute();

            Assert.IsNotNull(result);
        }
        /*
        [TestMethod]
        [Description("Expected: RequiredParamException")]
        public void noExTreqParExExpTestCallestCall()
        {
            try
            {

                Dictionary<String, String> inputParams = new Dictionary<String, String>();
                inputParams.Add("amount", "20.0");
                inputParams.Add("channel", Channel.ECOM.getCode());
                // inputParams.Add("country", CountryCode.GB.getCode());
                // inputParams.Add("currency", CurrencyCode.EUR.getCode());
                inputParams.Add("paymentSolutionId", "500");
                inputParams.Add("customerId", "8Gii57iYNVSd27xnFZzR");

                PurchaseCall call = new PurchaseCall(new ApplicationConfig(), inputParams);
                call.execute();

            }
            catch (RequireParamException e)
            {

                Assert.Equals(new List<String>("currency", "country"), e.Message);
                throw e;

            }
        }
        */

        }
}
