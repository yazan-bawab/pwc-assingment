using System;
using Newtonsoft.Json;
namespace Backend.API.Responses{
    public class UserInformationResponse{

        [JsonProperty("id")]
        public string id { get;set;}

        [JsonProperty("type")]
        public string type { get;set;}

        [JsonConstructor]
        public UserInformationResponse(){}

    }

}