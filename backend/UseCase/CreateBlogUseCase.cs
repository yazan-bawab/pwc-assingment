using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class CreateBlogUseCase
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly IUsersRepository _UserRepository;

        public CreateBlogUseCase(IBlogRepository repository,IUsersRepository usersRepository)
        {
            this._BlogsRepository = repository;
            this._UserRepository = usersRepository;
        }

        public UseCaseBaseOutput<bool> Process(BlogRequest request)
        {
            if(!_UserRepository.isUserLogInByToken(request.token,request.ipAddress))
                return new UseCaseBaseOutput<bool>(StatusCodes.Status401Unauthorized);

            User user = _UserRepository.getLogInUserInformation(request.token,request.ipAddress);
            Blog newBlog = new Blog();
            newBlog.title = request.title;
            newBlog.ownerUserId = user.id;
            newBlog.updatedDate = DateTime.Now;
            newBlog.content = request.content;
            newBlog.base64Image = request.base64Image;
            if(_BlogsRepository.create(newBlog))
                return new UseCaseBaseOutput<bool>(StatusCodes.Status200OK);
            return new UseCaseBaseOutput<bool>(StatusCodes.Status500InternalServerError);
        }
    }
}
