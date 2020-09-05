using Backend.API.Models;
using Backend.API.Services;
using MongoDB.Driver;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
using Microsoft.Extensions.Logging;

namespace Backend.API.Services
{

    public class MongoDbBlogRepository : IBlogRepository
    {
        private readonly IMongoCollection<Blog> _BlogData;

        public MongoDbBlogRepository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _BlogData = database.GetCollection<Blog>(settings.blogCollectionName);
        }

        public bool create(Blog blog)
        {
            try{
                _BlogData.InsertOne(blog);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }
    
        public Blog fetchSingleBlog(string id)
        {
            var BlogId = new ObjectId();
            if(!ObjectId.TryParse(id,out BlogId))   
                return null;
            try{
                Blog blog =_BlogData.Find<Blog>(blog => blog.id == id).FirstOrDefault(); 
                return blog;
            }catch(Exception ex){
                return null;
            }
        }
    
        public IEnumerable<Blog> fetchBlogs(){
            try{
                List<Blog> blogs =  _BlogData.Find(blog => true).ToList();
                return blogs;
            }catch(Exception ex){
                return null;
            }
        }
    
        public Blog updateBlog(string id,Blog updatedBlog){
            var blogId = new ObjectId();
            if(!ObjectId.TryParse(id,out blogId))   
                return new Blog();
            try{
                _BlogData.ReplaceOne(blog => blog.id == id,updatedBlog);
                return updatedBlog;
            }catch(Exception ex){
                return null;
            }
        }
    }

}