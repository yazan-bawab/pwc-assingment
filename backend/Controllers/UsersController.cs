using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Backend.API.Services;
using Backend.API.Models;
using Backend.API.Requests;
using Backend.API.UseCase;
using Backend.API.UseCase.Common;
using Backend.API.Responses;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

namespace Backend.API.Controllers{

    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _UsersRepository;
        

        public UsersController(IUsersRepository repository)
        {
          _UsersRepository = repository;  

        }

        [HttpPost]
        public IActionResult RegesitarUser([FromBody] RegistarUserRequest userRequest)
        {
            RegistarUserUseCase registrationUseCase = new RegistarUserUseCase(_UsersRepository);
            UseCaseBaseOutput<bool> useCaseBaseOutput = registrationUseCase.Process(userRequest);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return StatusCode(useCaseBaseOutput.httpStatus, "Creating a new registration failed please try again");
            return StatusCode(useCaseBaseOutput.httpStatus);
        }

        [HttpPost("login")]
        public IActionResult LogIn(LogInRequest loginRequest)
        {
            LogInUseCase logInUseCase = new LogInUseCase(_UsersRepository);
            UseCaseBaseOutput<string> useCaseBaseOutput = logInUseCase.Process(loginRequest);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return StatusCode(useCaseBaseOutput.httpStatus, "Cannot logIn Please check your creditials");
            return StatusCode(useCaseBaseOutput.httpStatus,useCaseBaseOutput.value);
        }

        [EnableCors("_myAllowSpecificOrigins")]
        [HttpPost("logout")]
        public IActionResult LogOut([FromBody] LogOutRequest logOutRequest)
        {
            LogOutUseCase logOutUseCase = new LogOutUseCase(_UsersRepository);
            UseCaseBaseOutput<bool> useCaseBaseOutput = logOutUseCase.Process(logOutRequest);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return StatusCode(useCaseBaseOutput.httpStatus, "logOut Fails");
            return StatusCode(useCaseBaseOutput.httpStatus);
        }
    
        [HttpPost("info")]
        public IActionResult getLogInUserTypeAndId(LogInUserTypeAndIdRequest userTypeAndIdRequest){
            GetUserInformationFromTokenUseCase useCase = new GetUserInformationFromTokenUseCase(_UsersRepository);
            UseCaseBaseOutput<UserInformationResponse> useCaseBaseOutput = useCase.Process(userTypeAndIdRequest);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return StatusCode(useCaseBaseOutput.httpStatus, "logOut Fails");
            return StatusCode(useCaseBaseOutput.httpStatus,useCaseBaseOutput.value);
        }
    }

}