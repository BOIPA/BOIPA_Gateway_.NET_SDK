using GlobalTurnkey.Models.config;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using GlobalTurnkey.Models.code;
using System.Net;
using System.Collections.Specialized;
using System.Text;

namespace GlobalTurnkey.Models
{
    public abstract class ApiCall
    {
       
	    protected ApplicationConfig config;
        private static readonly HttpClient client = new HttpClient();
        private Dictionary<String, String> inputParams;

         public ApiCall(ApplicationConfig config, Dictionary<String, String> inputParams) {
             
             try
             {
                 this.config = config;
                 this.inputParams = inputParams;
                 preValidateParams(inputParams);


             }
             catch(RequireParamException ex) {

             }
         }

        public static async Task<String> postToApi(String url, Dictionary<String, String> paramMap) {
            String apiResponseString = "";
            try
            {
                
                var wb = new WebClient();
                NameValueCollection nameValueCollection = new NameValueCollection();
                foreach (var kvp in paramMap)
                {
                    nameValueCollection.Add(kvp.Key.ToString(), kvp.Value.ToString());
                }
                
                var response = wb.UploadValues(url, "POST", nameValueCollection);
                apiResponseString = Encoding.UTF8.GetString(response);
              
            }
            catch (PostToApiException ex) {
                new PostToApiException("HTTP POST error");
            }


            return apiResponseString;
        }
        
        

        protected abstract Dictionary<String, String> getTokenParams(Dictionary<String, String> inputParams);

        protected abstract Dictionary<String, String> getActionParams(Dictionary<String, String> inputParams, String token);

        protected abstract void preValidateParams(Dictionary<String, String> inputParams);
        
        public Dictionary<String, String> execute() {
            try {
                Task<String> tokenResponse = postToApi(Properties.Settings.Default.applicationSessionTokenRequestUrl, getTokenParams(inputParams));
                
                Dictionary<String, String> values = Tools.JsonToDictionary(tokenResponse.Result);
                if (values["result"] != "failure") {
                    String token = values["token"];
                    
                    Dictionary<String, String> actionParams = getActionParams(inputParams, token);
                    if (actionParams == null) {
                        
                        return values;
                    }
                    Task<String> actionResponse = postToApi(Properties.Settings.Default.applicationPaymentOperationActionUrl, actionParams);
                    values = Tools.JsonToDictionary(actionResponse.Result);
                    if (values["result"] == "failure") {
                        new ActionCallException("error during the action call: " + actionResponse.ToString());
                    }
                    
                    return values;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            throw new Exception();
            }
        



    }
}