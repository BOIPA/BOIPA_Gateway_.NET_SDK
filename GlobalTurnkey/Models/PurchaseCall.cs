using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GlobalTurnkey.Models
{
    public class PurchaseCall : ApiCall
    {

        public PurchaseCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {

        }

        protected override void preValidateParams(Dictionary<String, String> inputParams)
        {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("amount");
            requiredParams.Add("channel");
            requiredParams.Add("country");
            requiredParams.Add("currency");
            requiredParams.Add("paymentSolutionId");
            foreach (KeyValuePair<String, String> entry in inputParams)
            {
                if (entry.Value != null && entry.Value.Trim().Length > 0)
                {
                    requiredParams.Remove(entry.Key);
                }
            }

            if (requiredParams.Count > 0)
            {
                throw new RequireParamException(requiredParams.ToString());
            }
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams)
        {
            Dictionary<String, String> tokenParams = new Dictionary<string, string>();
            tokenParams.Add("merchantId", Properties.Settings.Default.merchantId);
            tokenParams.Add("password", Properties.Settings.Default.password);
            tokenParams.Add("action", ActionType.PURCHASE.getCode());
            tokenParams.Add("timestamp", (Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds)).ToString());
            tokenParams.Add("allowOriginUrl", Properties.Settings.Default.allowOriginUrl);
            tokenParams.Add("channel", inputParams["channel"]);
            tokenParams.Add("amount", inputParams["amount"]);
            tokenParams.Add("currency", inputParams["currency"]);
            tokenParams.Add("country", inputParams["country"]);
            tokenParams.Add("paymentSolutionId", inputParams["paymentSolutionId"]);
            tokenParams.Add("merchantNotificationUrl", Properties.Settings.Default.merchantNotificationUrl);
            tokenParams.Add("forceSecurePayment", "false");
            return tokenParams;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token)
        {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", Properties.Settings.Default.merchantId);
            actionParams.Add("token", token);
            actionParams.Add("customerId", inputParams["customerId"]);
            actionParams.Add("specinCreditCardToken", inputParams["specinCreditCardToken"]);
            actionParams.Add("specinCreditCardCVV", inputParams["specinCreditCardCVV"]);

            return actionParams;
        }



    }
}