namespace Backend.API.Services{

    public class DatabaseSettings : IDatabaseSettings
    {
        public string usersCollectionName {get;set;}
        public string logInUsersCollectionName {get;set;}
        public string blogCollectionName {get;set;}
        public string ConnectionString {get;set;}
        public string DatabaseName {get;set;}
    }

    public interface IDatabaseSettings
   {
       string usersCollectionName{get;set;}
       string logInUsersCollectionName {get;set;}
       string blogCollectionName {get;set;}
       string ConnectionString{get;set;}
       string DatabaseName{get;set;}
   }


}