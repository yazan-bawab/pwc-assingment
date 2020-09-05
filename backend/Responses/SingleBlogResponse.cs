using System;
using Newtonsoft.Json;
namespace Backend.API.Responses{
    public class SingleBlogResponse{

        [JsonProperty("id")]
        public string id { get;set;}

        [JsonProperty("title")]
        public string title { get;set;}

        [JsonProperty("content")]
        public string content { get;set; }

        [JsonProperty("image")]
        public string base64Image { get;set; }

        [JsonProperty("updateDate")]
        public string updatedDate {get;set;}

        [JsonProperty("CreatorName")]
        public string creatorName {get;set;}

        [JsonProperty("ownerUserId")]
        public string ownerUserId {get;set;}

        [JsonConstructor]
        public SingleBlogResponse(){}

    }

}