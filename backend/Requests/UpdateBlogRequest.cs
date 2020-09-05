using Newtonsoft.Json;
namespace Backend.API.Requests{
    public class UpdateBlogRequest{

        [JsonProperty("blogId")]
        public string blogId {get;set;}
        
        [JsonProperty("blog")]
        public BlogContent blog {get;set;}

        [JsonProperty("token")]
        public string token {get;set;}

        [JsonProperty("ipAddress")]
        public string ipAddress {get;set;}

        [JsonConstructor]
        public UpdateBlogRequest(){}

    }

    public class BlogContent{
        [JsonProperty("title")]
        public string title { get;set;}

        [JsonProperty("content")]
        public string content { get;set; }

        [JsonProperty("image")]
        public string base64Image { get;set; }

        [JsonConstructor]
        public BlogContent(){}

    }

}