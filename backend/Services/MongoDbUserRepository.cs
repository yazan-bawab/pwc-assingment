using Backend.API.Models;
using Backend.API.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace Backend.API.Services
{

    public class MongoDbUserRepository : IUsersRepository
    {
        private readonly IMongoCollection<User> _UsersData;
        private readonly IMongoCollection<LogInUsers> _LogInUserData;

        public MongoDbUserRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _UsersData = database.GetCollection<User>(settings.usersCollectionName);
            _LogInUserData = database.GetCollection<LogInUsers>(settings.logInUsersCollectionName);
        }

        public bool create(User user)
        {
            try{
                _UsersData.InsertOne(user);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }
    
        public User getInformation(string email,string password)
        {
            try{
                User user =_UsersData.Find<User>(user =>
                 user.email == email && user.password == password )
                 .FirstOrDefault(); 
                return user;
            }catch(Exception ex){
                return null;
            }
        }
    
        public bool addToLogInUsers(LogInUsers user){
            try{
                _LogInUserData.InsertOne(user);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    
        public bool isUserLogIn(string userId)
        {
            LogInUsers user = _LogInUserData.Find<LogInUsers>(user => user.user_id == userId).FirstOrDefault();
            if(user != null)
                return true;
            else
                return false;
        }
    
        public bool removeLogInUser(string tokenId,string ipAddress){
            try{
                DeleteResult result = _LogInUserData.DeleteOne(user => user.tokenId == tokenId && user.ipAddress == ipAddress);
                if(result.DeletedCount > 0)
                    return true;
                return false;
            }catch(Exception ex){
                return false;
            }

        }
    
        public bool isUserLogInByToken(string token,string ipAddress)
        {
             LogInUsers user = _LogInUserData.Find<LogInUsers>(user => user.tokenId == token && user.ipAddress == ipAddress).FirstOrDefault();
            if(user != null)
                return true;
            else
                return false;
        }

        public User getLogInUserInformation(string token,string ipAddress)
        {
            LogInUsers logInUser = _LogInUserData.Find<LogInUsers>(user => user.tokenId == token && user.ipAddress == ipAddress).FirstOrDefault();
            try{
                User user =_UsersData.Find<User>(user =>
                 user.id == logInUser.user_id)
                 .FirstOrDefault(); 
                return user;
            }catch(Exception ex){
                return null;
            }
        }
    
        public string getUserName(string id){
            try{
                User user =_UsersData.Find<User>(user =>
                 user.id == id)
                 .FirstOrDefault(); 
                return user.name;
            }catch(Exception ex){
                return "";
            }
        }
    }

}