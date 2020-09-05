using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class LogOutUseCase
    {
        private readonly IUsersRepository _UserRepository;

        public LogOutUseCase(IUsersRepository repository)
        {
            this._UserRepository = repository;
        }

        public UseCaseBaseOutput<bool> Process(LogOutRequest request)
        {
            if(!_UserRepository.removeLogInUser(request.tokenId,request.IPAddress))
                return new UseCaseBaseOutput<bool>(StatusCodes.Status500InternalServerError);
            return new UseCaseBaseOutput<bool>(StatusCodes.Status200OK);
        }
    }
}
