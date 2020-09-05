using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class RegistarUserRequest{

        [JsonProperty("Name")]
        public string name { get;set;}

        [JsonProperty("email")]
        public string email { get;set; }

        [JsonProperty("password")]
        public string password { get;set; }

        [JsonProperty("type")]
        public string type { get;set; }

        [JsonConstructor]
        public RegistarUserRequest(){}

    }

}