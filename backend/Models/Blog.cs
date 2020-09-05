using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Backend.API.Models{
    
     [BsonIgnoreExtraElements]
    public class Blog {
        public string id ;
        public string title;
        public string ownerUserId;
        public DateTime updatedDate;
        public string content;
        public string base64Image;

        public Blog(){
            id = ObjectId.GenerateNewId().ToString();
            this.title = string.Empty;
            this.ownerUserId = string.Empty;
            this.content = string.Empty;
            this.base64Image = string.Empty;
            this.updatedDate = DateTime.Now;
        }
    }
}