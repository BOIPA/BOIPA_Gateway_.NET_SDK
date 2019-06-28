using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using GlobalTurnkey.Models;
using GlobalTurnkey.Models.config;

namespace GlobalTurnkey.Controllers
{
    public class GetAvailablePaymentSolutionsController : ApiController
    {

        public async Task<object> Post()

        {
            HttpContent requestContent = Request.Content;
            string res = requestContent.ReadAsStringAsync().Result;
            Dictionary<String, String> requestData = Tools.requestToDictionary(res);

            GetAvailablePaymentSolutionsCall gaps = new GetAvailablePaymentSolutionsCall(new ApplicationConfig(), requestData);
            Dictionary<string, string> response = gaps.execute();

            //return requestData;
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


    }
}
