using DotNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcApp.Controllers
{
    // https://localhost:7258/Blog/Index
    public class BlogController : Controller
    {
        private readonly AppDbContext _content;

        public BlogController()
        {
            _content = new AppDbContext();
        }

        // https://localhost:7258/Blog/Index
        [ActionName("Index")]
        public IActionResult BlogIndex()
        {
            var lst = _content.Blogs.ToList();
            return View("BlogIndex", lst);
        }

        // https://localhost:7258/Blog/Edit/1
        // https://localhost:7258/Blog/Edit?id=1
        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item == null)
            {
                return Redirect("/Blog");
            }

            return View("BlogEdit", item);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogModel blog)
        {
            _content.Blogs.Add(blog);
            _content.SaveChanges();

            return Redirect("/Blog");
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogModel blog)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _content.SaveChanges();

            return Redirect("/Blog");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item == null)
            {
                return Redirect("/Blog");
            }

            _content.Blogs.Remove(item);
            _content.SaveChanges();

            return Redirect("/Blog");
        }
    }
}
