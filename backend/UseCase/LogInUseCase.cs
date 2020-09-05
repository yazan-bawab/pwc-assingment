using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class LogInUseCase
    {
        private readonly IUsersRepository _UserRepository;

        public LogInUseCase(IUsersRepository repository)
        {
            this._UserRepository = repository;
        }

        public UseCaseBaseOutput<string> Process(LogInRequest request)
        {
            User user = _UserRepository.getInformation(request.email,
                                          Security.ComputeSha256Hash(request.password));
           
            if(user == null)
                return new UseCaseBaseOutput<string>("",StatusCodes.Status404NotFound);
            
            
            if(_UserRepository.isUserLogIn(user.id))
                return new UseCaseBaseOutput<string>(StatusCodes.Status200OK);

            string token = Security.ComputeSha256Hash(DateTime.Now.ToString() + request.IPAddress);
            
            LogInUsers logInUsers = new LogInUsers{
                user_id = user.id,
                tokenId = token,
                ipAddress = request.IPAddress,
                loginDateTime = DateTime.Now
            };
            _UserRepository.addToLogInUsers(logInUsers);
            return new UseCaseBaseOutput<string>(token,StatusCodes.Status200OK);
        }
    }
}
