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
    }
}
