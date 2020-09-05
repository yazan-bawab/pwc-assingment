using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Backend.API.Models{
    
     [BsonIgnoreExtraElements]
    public class User {
        public string id ;
        public string name;
        public string email;
        public string password;
        public UserTypes type;

        public User(string name,string email,string password,UserTypes type){
            this.name = name;
            this.email = email;
            this.password = password;
            this.type = type;
        }

        public User(){
            id = ObjectId.GenerateNewId().ToString();
            this.name = string.Empty;
            this.email = string.Empty;
            this.password = string.Empty;
            this.type = UserTypes.WRITER;
        }
    }
}