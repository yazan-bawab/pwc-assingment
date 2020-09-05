using Backend.API.Models;
namespace Backend.API.Services
{
    public interface IUsersRepository
    {
        bool create(User user);
        User getInformation(string email,string password);
        bool addToLogInUsers(LogInUsers user);
        bool isUserLogIn(string userId);
        bool removeLogInUser(string tokenId,string ipAddress);        
        bool isUserLogInByToken(string token,string ipAddress);
        User getLogInUserInformation(string token,string ipAddress);
        string getUserName(string id);
    }

}