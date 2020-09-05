using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Backend.API.Services;
using Backend.API.Models;
using Backend.API.Requests;
using Backend.API.Responses;
using Backend.API.UseCase;
using Backend.API.UseCase.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Backend.API.Controllers{

    
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogRepository _BlogsRepository;
        private readonly IUsersRepository _UsersRepository;
        

        public BlogsController(IBlogRepository repository,IUsersRepository usersRepository)
        {
          _BlogsRepository = repository;  
          _UsersRepository = usersRepository;
        }

        [HttpPost]
        public IActionResult AddBlog(BlogRequest request)
        {
            CreateBlogUseCase createBlogUseCase = new CreateBlogUseCase(_BlogsRepository,_UsersRepository);
            UseCaseBaseOutput<bool> useCaseBaseOutput = createBlogUseCase.Process(request);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return StatusCode(useCaseBaseOutput.httpStatus, "Creating a new Blog failed please try again");
            return StatusCode(useCaseBaseOutput.httpStatus);
        }

        
        [HttpGet("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public ActionResult<SingleBlogResponse> getSingleBlog(string id)
        {
            GetSingleBlogUseCase getSingleBlogUseCase = new GetSingleBlogUseCase(_BlogsRepository,_UsersRepository);
            UseCaseBaseOutput<SingleBlogResponse> useCaseBaseOutput = getSingleBlogUseCase.Process(id);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return  StatusCode(useCaseBaseOutput.httpStatus,"failed fetching blog");
            return StatusCode(useCaseBaseOutput.httpStatus,useCaseBaseOutput.value);
        }

        
        [HttpGet]
        [EnableCors("_myAllowSpecificOrigins")]
        public ActionResult<SingleBlogResponse> getBlogs()
        {
            GetBlogsUseCase getBlogsUseCase = new GetBlogsUseCase(_BlogsRepository,_UsersRepository);
            UseCaseBaseOutput<IEnumerable<SingleBlogResponse>> useCaseBaseOutput = getBlogsUseCase.Process();
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return  StatusCode(useCaseBaseOutput.httpStatus,"failed fetching blog");
            return StatusCode(useCaseBaseOutput.httpStatus,useCaseBaseOutput.value);
        }

        [HttpPut]
        public IActionResult update(UpdateBlogRequest request)
        {
            UpdateBlogUseCase updateBlogUseCase = new UpdateBlogUseCase(_BlogsRepository,_UsersRepository);
            UseCaseBaseOutput<SingleBlogResponse> useCaseBaseOutput = updateBlogUseCase.Process(request);
            if(useCaseBaseOutput.httpStatus != StatusCodes.Status200OK)
                return  StatusCode(useCaseBaseOutput.httpStatus,"failed to update blog please try again later");
            return StatusCode(useCaseBaseOutput.httpStatus,useCaseBaseOutput.value);
        }
    }

}