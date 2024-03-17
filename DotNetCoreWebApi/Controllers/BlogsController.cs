using DotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public BlogsController()
        {
            _dbContext = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogModel> lst = _dbContext.Blogs.OrderByDescending(x => x.BlogId).ToList();
            return Ok(lst);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            BlogModel? item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            BlogModel? item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null)
            {
                return NotFound("No data found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            BlogModel? item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("No data found.");
            }
            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
