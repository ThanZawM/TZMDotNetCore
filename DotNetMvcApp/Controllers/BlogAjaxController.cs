using DotNetMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMvcApp.Controllers
{
    // https://localhost:7258/Blog/Index
    public class BlogAjaxController : Controller
    {
        private readonly AppDbContext _content;

        public BlogAjaxController()
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
            int result = _content.SaveChanges();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            BlogMessageResponseModel model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Json(model);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogModel blog)
        {
            BlogMessageResponseModel model = new BlogMessageResponseModel();

            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                model = new BlogMessageResponseModel()
                {
                    IsSuccess = false,
                    Message = "No data found.",
                };
                return Json(model);
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            int result = _content.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Json(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(BlogModel blog)
        {
            BlogMessageResponseModel model = new BlogMessageResponseModel();

            var item = _content.Blogs.FirstOrDefault(x => x.BlogId == blog.BlogId);
            if (item == null)
            {
                model = new BlogMessageResponseModel()
                {
                    IsSuccess = false,
                    Message = "No data found.",
                };
                return Json(model);
            }

            _content.Blogs.Remove(item);
            int result = _content.SaveChanges();
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            model = new BlogMessageResponseModel()
            {
                IsSuccess = result > 0,
                Message = message,
            };
            return Json(model);
        }
    }
}
