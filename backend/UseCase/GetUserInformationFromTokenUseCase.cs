using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.Responses;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class GetUserInformationFromTokenUseCase
    {
        private readonly IUsersRepository _UserRepository;

        public GetUserInformationFromTokenUseCase(IUsersRepository usersRepository)
        {
            this._UserRepository = usersRepository;
        }

        public UseCaseBaseOutput<UserInformationResponse> Process(LogInUserTypeAndIdRequest request)
        {
            User user = _UserRepository.getLogInUserInformation(request.token,request.ipAddress);
            if(user == null)
                return new UseCaseBaseOutput<UserInformationResponse>(new UserInformationResponse(),StatusCodes.Status500InternalServerError);

            return new UseCaseBaseOutput<UserInformationResponse>(new UserInformationResponse{
                id = user.id,
                type = user.type.ToString()
            },StatusCodes.Status200OK);
        }
    }
}
