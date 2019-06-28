using GlobalTurnkey.Models;
using GlobalTurnkey.Models.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GlobalTurnkey.Controllers
{
    public class TokenizeController : ApiController
    {

        public async Task<object> Post()

        {
            HttpContent requestContent = Request.Content;
            string res = requestContent.ReadAsStringAsync().Result;
            Dictionary<String, String> requestData = Tools.requestToDictionary(res);

            TokenizeCall tokenize = new TokenizeCall(new ApplicationConfig(), requestData);
            Dictionary<string, string> response = tokenize.execute();

            //return requestData;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }
}
