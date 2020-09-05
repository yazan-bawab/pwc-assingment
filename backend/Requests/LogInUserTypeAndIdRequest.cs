using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class LogInUserTypeAndIdRequest{
        [JsonProperty("token")]
        public string token { get;set; }

        [JsonProperty("ipAddress")]
        public string ipAddress { get;set; }

        [JsonConstructor]
        public LogInUserTypeAndIdRequest(){}

    }

}