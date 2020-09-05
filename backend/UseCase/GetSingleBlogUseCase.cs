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
    public sealed class GetSingleBlogUseCase
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly IUsersRepository _UserRepository;

        public GetSingleBlogUseCase(IBlogRepository repository,IUsersRepository usersRepository)
        {
            this._BlogsRepository = repository;
            this._UserRepository = usersRepository;
        }

        public UseCaseBaseOutput<SingleBlogResponse> Process(string blogId)
        {
            Blog blog = _BlogsRepository.fetchSingleBlog(blogId);
            if(blog == null)
                return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse(),StatusCodes.Status500InternalServerError);

            string userName = _UserRepository.getUserName(blog.ownerUserId);
            if(userName == string.Empty)
                return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse(),StatusCodes.Status500InternalServerError);
            
            SingleBlogResponse responses = new SingleBlogResponse{
                id = blog.id,
                title = blog.title,
                content = blog.content,
                base64Image = blog.base64Image,
                updatedDate = blog.updatedDate.ToString("MMMM dd, yyyy"),
                creatorName = userName,
                ownerUserId = blog.ownerUserId
            }; 
            return new UseCaseBaseOutput<SingleBlogResponse>(responses,StatusCodes.Status200OK);
        }
    }
}
