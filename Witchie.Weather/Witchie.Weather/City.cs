using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Witchie.Weather
{
    /// <summary>
    /// 城市
    /// </summary>
    internal class City
    {
        /// <summary>
        /// 城市ID号码
        /// </summary>
        [JsonProperty("cityid")]
        public int CityID { get; set; }
        /// <summary>
        /// 父节点ID号码
        /// </summary>
        [JsonProperty("parentid")]
        public int ParentId { get; set; }
        /// <summary>
        /// 城市编码
        /// </summary>
        [JsonProperty("citycode")]
        public string CityCode { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        [JsonProperty("city")]
        public string CityName { get; set; }
    }
    /// <summary>
    /// 城市模型
    /// </summary>
    public class CityModel
    {
        /// <summary>
        /// 城市ID号码
        /// </summary>
        [JsonProperty("城市编码")]
        public int CityID { get; set; }
        /// <summary>
        /// 子城市列表
        /// </summary>
        [JsonProperty("下级列表")]
        public List<CityModel> SubCity { get; set; } = new List<CityModel>();
        /// <summary>
        /// 城市编码
        /// </summary>
        [JsonProperty("城市编码")]
        public string CityCode { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        [JsonProperty("城市名称")]
        public string CityName { get; set; }
        public override string ToString()
        {
            if (SubCity.Count==0)
            {
                var buffer = string.Join(",", SubCity);
                return CityName + ":" + SubCity.Count + Environment.NewLine;
            }
            else
            {
                var buffer = string.Join(",", SubCity);
                return CityName + ":" + SubCity.Count;
            }
        }
    }
}
