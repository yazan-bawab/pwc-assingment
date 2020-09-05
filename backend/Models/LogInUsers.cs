using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Backend.API.Models{
    
     [BsonIgnoreExtraElements]
    public class LogInUsers {
        public string user_id;
        public string tokenId;
        public string ipAddress;
        public DateTime loginDateTime;

        public LogInUsers(){
            this.user_id = string.Empty;
            this.tokenId = string.Empty;
            this.ipAddress = string.Empty;
            this.loginDateTime = DateTime.Now;
        }
    }
}