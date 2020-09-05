using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class LogInRequest{
        [JsonProperty("email")]
        public string email { get;set; }

        [JsonProperty("password")]
        public string password { get;set; }

        [JsonProperty("ipAddress")]
        public string IPAddress { get;set; }

        [JsonConstructor]
        public LogInRequest(){}

    }

}