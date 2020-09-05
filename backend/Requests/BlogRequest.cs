using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class BlogRequest{

        [JsonProperty("title")]
        public string title { get;set;}

        [JsonProperty("content")]
        public string content { get;set; }

        [JsonProperty("image")]
        public string base64Image { get;set; }

        [JsonProperty("token")]
        public string token {get;set;}

        [JsonProperty("ipAddress")]
        public string ipAddress {get;set;}

        [JsonConstructor]
        public BlogRequest(){}

    }

}