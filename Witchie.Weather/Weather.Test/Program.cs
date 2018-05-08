using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Witchie.Weather;

namespace Weather.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpRequestHelper helper = new HttpRequestHelper();
            var res = helper.GetCityList();
            Console.WriteLine(res);
            var city=    helper.WeatherQuery(res.SubCity[1]);
            Console.Read();
        }
       
    }
}
