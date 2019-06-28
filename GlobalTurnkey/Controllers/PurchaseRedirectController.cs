﻿using GlobalTurnkey.Models;
using GlobalTurnkey.Models.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GlobalTurnkey.Controllers
{
    public class PurchaseRedirectController : ApiController
    {
        protected readonly ApplicationConfig config;

        public PurchaseRedirectController()
        {
        }

        public async Task<object> Post()

        {
            try
            {
                HttpContent requestContent = Request.Content;
                string res = requestContent.ReadAsStringAsync().Result;
                Dictionary<String, String> inputParams = Tools.requestToDictionary(res);
                Dictionary<String, String> executeData = new PurchaseTokenCall(new ApplicationConfig(), inputParams).execute();

                inputParams.Add("merchantId", Properties.Settings.Default.merchantId);
                inputParams.Add("token", executeData["token"]);

                String url = "https://cashier-turnkeyuat.test.myriadpayments.com/ui/cashier?";
                foreach (KeyValuePair<String, String> kvp in inputParams) {
                    url += kvp.Key + "=" + kvp.Value + "&";
                }
                url = url.Remove(url.Length - 1);
                Dictionary<String, String> response = new Dictionary<string, string>();
                response.Add("url", url);
                
                return Request.CreateResponse(HttpStatusCode.OK, response);
                
            }
            catch (RequireParamException ex) {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Missing fields: " + ex.ToString());
            }
            catch (TokenAcquirationException ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Could not acquire token: " + ex.ToString());
            }
            catch (PostToApiException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Outgoing POST failed: " + ex.ToString());
            }
            catch (GeneralException ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "General SDK error: " + ex.ToString());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error: " + ex.ToString());
            }
            
            
        }
    }
}
