using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;
namespace Witchie.Weather
{
    public class HttpRequestHelper
    {
        public string AppCode { get; set; } = "66d1ba6166dc4519870001e5d1a6caab";
        /// <summary>
        /// 获取城市信息
        /// </summary>
        /// <returns></returns>
        public CityModel GetCityList()
        {
            HttpRequestBase httpRequestBase = new HttpRequestBase();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("Authorization", "APPCODE " + AppCode);
            var res = httpRequestBase.Request("weather/city", keyValuePairs, HttpMethod.Get);
            var city=  JsonConvert.DeserializeObject<CityInfo>(res);
            CityModel model = new CityModel();
            model.CityName = "中国";
            var result = GetCity(city.Citys);
            model.SubCity.AddRange(result);   
            return model;
        }
        public string WeatherQuery(CityModel cityModel)
        {
            HttpRequestBase httpRequestBase = new HttpRequestBase();
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("Authorization", "APPCODE " + AppCode);
            var path = "weather/query"+"?"+"city="+cityModel.CityName+"&citycode="+cityModel.CityCode+ "&cityid="+cityModel.CityID;
            var res = httpRequestBase.Request(path, keyValuePairs, HttpMethod.Get);
            return res;
        }
        private  CityModel[] GetCity(City[] cities)
        {
            var dta = cities.Where(p => p.ParentId == 0);
            List<CityModel> models = new List<CityModel>();
            foreach (var item in dta)
            {
                var model = GetCityModel(cities, item, item.CityID);
                CityModel model1 = new CityModel();
                model1.CityCode = item.CityCode;
                model1.CityID = item.CityID;
                model1.CityName = item.CityName;
                model1.SubCity.AddRange(model);
                models.Add(model1);
            }
            return models.ToArray();
        }
        private CityModel[] GetCityModel(City[] cities, City cityModel, int cityid)
        {
            var citys = cities.Where(p => p.ParentId == cityid).ToArray();
            List<CityModel> models = new List<CityModel>();
            foreach (var item in citys)
            {
                var city = GetCityModel(cities, item, item.CityID);
                if (city != null)
                {
                    CityModel model = new CityModel();
                    model.CityCode = item.CityCode;
                    model.CityID = item.CityID;
                    model.CityName = item.CityName;
                    model.SubCity.AddRange(city);
                    models.Add(model);
                }
                else
                {
                    CityModel model = new CityModel();
                    model.CityCode = item.CityCode;
                    model.CityID = item.CityID;
                    model.CityName = item.CityName;
                    models.Add(model);
                }
            }
            return models.ToArray();
        }
    }
    internal class HttpRequestBase
    {
        public Uri Host { get; set; } =new Uri("http://jisutqybmf.market.alicloudapi.com");
        // private const String appcode = "66d1ba6166dc4519870001e5d1a6caab";
        public string Request(string requesturi ,Dictionary<string,string> header, HttpMethod httpMethod, string bodys = "")
        {
            requesturi = Host.ToString() + requesturi;
            HttpWebRequest httpRequest = null;
            if (requesturi.StartsWith("https"))
            {
                ServicePointManager.ServerCertificateValidationCallback = (s, c, ce, e) => true;
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(requesturi));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(requesturi);
            }
            httpRequest.Method = httpMethod.Method;
            foreach (var item in header)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }
    }
}
