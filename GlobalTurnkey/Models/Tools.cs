using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


namespace GlobalTurnkey.Models
{
    public static class Tools
    {

        public static Dictionary<String, String> requestToDictionary(String requestString) {
            Dictionary<String, String> data = new Dictionary<String, String>();
            try
            {
                object[] kvp = requestString.Split('&');
                foreach (String s in kvp)
                {
                    object[] o = s.Split('=');
                    data.Add(o[0].ToString(), o[1].ToString());
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return data;
        }

        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static Dictionary<String, String> JsonToDictionary(String Json) {
            Dictionary<String, String> dic = new Dictionary<string, string>();
            String[] kvps = Json.Split(',');
            for (int i = 0; i < kvps.Length; i++) {
                String[] kvp = kvps[i].Split(':');
                String k = kvp[0].Trim().Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                String v = kvp[1].Trim().Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace("[", "").Replace("]", "");
                dic.Add(k, v);
            }
            return dic;
        }


    }
}