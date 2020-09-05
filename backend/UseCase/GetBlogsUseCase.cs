using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Backend.API.Services;
using Backend.API.Requests;
using Backend.API.Responses;
using Backend.API.UseCase.Common;
using Backend.API.Models;
using Backend.Util;

namespace Backend.API.UseCase
{
    public sealed class GetBlogsUseCase
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly IUsersRepository _UserRepository;

        public GetBlogsUseCase(IBlogRepository repository,IUsersRepository usersRepository)
        {
            this._BlogsRepository = repository;
            this._UserRepository = usersRepository;
        }

        public UseCaseBaseOutput<IEnumerable<SingleBlogResponse>> Process()
        {
            IEnumerable<Blog> fetchingResult = _BlogsRepository.fetchBlogs();
            if(fetchingResult == null)
                return new UseCaseBaseOutput<IEnumerable<SingleBlogResponse>>(
                    StatusCodes.Status500InternalServerError);
            List<SingleBlogResponse> response = new List<SingleBlogResponse>();
            foreach(Blog blog in fetchingResult){
                response.Add(new SingleBlogResponse{
                    id = blog.id,
                    title = blog.title,
                    content = blog.content,
                    base64Image = blog.base64Image,
                    updatedDate = blog.updatedDate.ToString("MMMM dd, yyyy"),
                    creatorName = _UserRepository.getUserName(blog.ownerUserId),
                    ownerUserId = blog.ownerUserId
                });
            }
            return new UseCaseBaseOutput<IEnumerable<SingleBlogResponse>>(
                    response,StatusCodes.Status200OK);

        }
    }
}
