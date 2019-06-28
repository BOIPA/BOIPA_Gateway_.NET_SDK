using System;
using System.Collections.Generic;
using GlobalTurnkey.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GlobalTurnkey.Models.config;



namespace GlobalTurnkey.Tests.Models
{
    [TestClass]
    public class TokenizeTest
    {
        [TestMethod]
        public void noExTestCall()
        {
            Dictionary<String, String> inputParams = new Dictionary<string, string>();
            inputParams.Add("number", "5424180279791732");
            inputParams.Add("nameOnCard", "mastercard");
            inputParams.Add("expiryYear", "2021");
            inputParams.Add("expiryMonth", "04");

            TokenizeCall call = new TokenizeCall(new ApplicationConfig(), inputParams);
            Dictionary<String, String> result = call.execute();


            Assert.IsNotNull(result);
        }

        [TestMethod]
        [Description("Expected: ActionCallException")]
        public void actCallExExpTestCall()
        {
                Dictionary<String, String> inputParams = new Dictionary<string, string>();
                inputParams.Add("number", "5424180279791732");
                inputParams.Add("nameOnCard", "mastercard");
                inputParams.Add("expiryYear", "2010");
                inputParams.Add("expiryMonth", "04");

                TokenizeCall call = new TokenizeCall(new ApplicationConfig(), inputParams);
                Dictionary<String, String> result = call.execute();
            
        }
    }
}
