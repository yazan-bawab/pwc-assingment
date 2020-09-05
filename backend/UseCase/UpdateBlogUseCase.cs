using System;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;
using Backend.API.Responses;

namespace Backend.API.UseCase
{
    public sealed class UpdateBlogUseCase
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly IUsersRepository _UserRepository;

        public UpdateBlogUseCase(IBlogRepository repository,IUsersRepository usersRepository)
        {
            this._BlogsRepository = repository;
            this._UserRepository = usersRepository;
        }

        public UseCaseBaseOutput<SingleBlogResponse> Process(UpdateBlogRequest request)
        {
            if(!_UserRepository.isUserLogInByToken(request.token,request.ipAddress))
                return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse(),StatusCodes.Status401Unauthorized);
            User user = _UserRepository.getLogInUserInformation(request.token,request.ipAddress);
            Blog blog = _BlogsRepository.fetchSingleBlog(request.blogId);
            if(blog == null)
                return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse(),StatusCodes.Status500InternalServerError);
            Blog updatedBlog = new Blog();
            if(user.id == blog.ownerUserId || user.type == UserTypes.ADMIN){    
                updatedBlog.base64Image = request.blog.base64Image;
                updatedBlog.content = request.blog.content;
                updatedBlog.title = request.blog.title;
                updatedBlog.id = blog.id;
                updatedBlog.ownerUserId = blog.ownerUserId;
                updatedBlog.updatedDate = DateTime.Now;
            }
            else
            {
                updatedBlog.base64Image = blog.base64Image;
                updatedBlog.content = blog.content + Environment.NewLine + request.blog.content;
                updatedBlog.title = blog.title;
                updatedBlog.id = blog.id;
                updatedBlog.ownerUserId = blog.ownerUserId;
                updatedBlog.updatedDate = DateTime.Now;
            }
            Blog afterUpdateBlog = _BlogsRepository.updateBlog(request.blogId,updatedBlog);

            if(afterUpdateBlog.id != updatedBlog.id || afterUpdateBlog == null)
                return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse(),StatusCodes.Status500InternalServerError);

            return new UseCaseBaseOutput<SingleBlogResponse>(new SingleBlogResponse{
                id = afterUpdateBlog.id,
                title = afterUpdateBlog.title,
                content = afterUpdateBlog.content,
                base64Image = afterUpdateBlog.base64Image,
                updatedDate = afterUpdateBlog.updatedDate.ToString("MMMM dd, yyyy"),
                creatorName = _UserRepository.getUserName(afterUpdateBlog.ownerUserId)
            },StatusCodes.Status200OK);
        }
    }
}
