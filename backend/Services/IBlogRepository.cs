using Backend.API.Models;
using System.Collections.Generic;
using System;
namespace Backend.API.Services
{
    public interface IBlogRepository
    {
        bool create(Blog blog);
        Blog fetchSingleBlog(string id);
        IEnumerable<Blog> fetchBlogs();
        Blog updateBlog(string id,Blog updatedBlog);
    }

}