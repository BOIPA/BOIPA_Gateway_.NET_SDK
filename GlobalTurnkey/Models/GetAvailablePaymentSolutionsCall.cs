using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using GlobalTurnkey.Models.code;
using GlobalTurnkey.Models.config;

namespace GlobalTurnkey.Models
{
    public class GetAvailablePaymentSolutionsCall : ApiCall
    {
        public GetAvailablePaymentSolutionsCall(ApplicationConfig config, Dictionary<String, String> inputParams) : base(config, inputParams)
        {
            
        }

        protected override void preValidateParams(Dictionary<String, String> inputParams) {
            List<String> requiredParams = new List<String>();
            requiredParams.Add("country");
            requiredParams.Add("currency");
            foreach (KeyValuePair<String, String> entry in inputParams) {
                if (entry.Value != null && entry.Value.Trim().Length > 0) {
                    requiredParams.Remove(entry.Key);
                }
            }

            if (requiredParams.Count > 0) {
                throw new RequireParamException(requiredParams.ToString());
            }
        }

        protected override Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams) {
            Dictionary<String, String> tokenParams = inputParams;
            tokenParams.Add("merchantId", Properties.Settings.Default.merchantId);
            tokenParams.Add("password", Properties.Settings.Default.password);
            tokenParams.Add("action", ActionType.GET_AVAILABLE_PAYSOLS.getCode());
            tokenParams.Add("timestamp", (Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds)).ToString());
            tokenParams.Add("allowOriginUrl", Properties.Settings.Default.allowOriginUrl);

            return tokenParams;
        }

        protected override Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token) {
            Dictionary<String, String> actionParams = new Dictionary<String, String>();
            actionParams.Add("merchantId", Properties.Settings.Default.merchantId);
            actionParams.Add("token", token);

            return actionParams;
        }

    }
}