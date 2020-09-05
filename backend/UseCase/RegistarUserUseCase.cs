using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class RegistarUserUseCase
    {
        private readonly IUsersRepository _UserRepository;

        public RegistarUserUseCase(IUsersRepository repository)
        {
            this._UserRepository = repository;
        }

        public UseCaseBaseOutput<bool> Process(RegistarUserRequest request)
        {
            User newUser = new User();
            newUser.name = request.name;
            newUser.email = request.email;
            newUser.type = (UserTypes)Enum.Parse(typeof(UserTypes), request.type);
            newUser.password = Security.ComputeSha256Hash(request.password);
            if(_UserRepository.create(newUser))
                return new UseCaseBaseOutput<bool>(StatusCodes.Status200OK);
            return new UseCaseBaseOutput<bool>(StatusCodes.Status500InternalServerError);
        }
    }
}
