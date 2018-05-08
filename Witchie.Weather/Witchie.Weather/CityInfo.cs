using Newtonsoft.Json;
namespace Witchie.Weather
{
    /// <summary>
    /// 城市信息
    /// </summary>
    internal class CityInfo
    {
        /// <summary>
        /// 命令执行状态
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }
        /// <summary>
        /// 执行结果
        /// </summary>
        [JsonProperty("result")]
        public City[] Citys { get; set; }
    }
}
