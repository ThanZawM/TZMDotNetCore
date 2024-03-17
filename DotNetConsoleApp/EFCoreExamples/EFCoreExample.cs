using DotNetConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        AppDbContext _dbContext = new AppDbContext();
        public void Read()
        {
            List<BlogModel> lst = _dbContext.Blogs.ToList();

            foreach (BlogModel item in lst)
            {
                Console.WriteLine("ID..." + item.BlogId);
                Console.WriteLine("Title..." + item.BlogTitle);
                Console.WriteLine("Author..." + item.BlogAuthor);
                Console.WriteLine("Content..." + item.BlogContent);
            }
        }

        public void Edit(int id)
        {
            // BlogModel item = _dbContext.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
            BlogModel item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id); // Linq
            if(item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine("ID..." + item.BlogId);
            Console.WriteLine("Title..." + item.BlogTitle);
            Console.WriteLine("Author..." + item.BlogAuthor);
            Console.WriteLine("Content..." + item.BlogContent);
        }

        public void Create(string title, string author, string content)
        {
            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };
            _dbContext.Blogs.Add(blog);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            Console.WriteLine(message);
        }

        public void Update(int id, string title, string author, string content)
        {
            BlogModel item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id); // Linq
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            Console.WriteLine(message);
        }

        public void Delete(int id)
        {
            BlogModel item = _dbContext.Blogs.FirstOrDefault(x => x.BlogId == id); // Linq
            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            _dbContext.Blogs.Remove(item);
            int result = _dbContext.SaveChanges();

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            Console.WriteLine(message);
        }
    }
}
