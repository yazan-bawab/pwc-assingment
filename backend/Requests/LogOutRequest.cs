using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class LogOutRequest{
        [JsonProperty("tokenId")]
        public string tokenId { get;set; }

        [JsonProperty("ipAddress")]
        public string IPAddress { get;set; }

        [JsonConstructor]
        public LogOutRequest(){}

    }

}