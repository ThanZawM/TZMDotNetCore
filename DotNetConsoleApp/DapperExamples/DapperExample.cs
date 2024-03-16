using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DotNetConsoleApp.Models;
using System.Reflection.Metadata;

namespace DotNetConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "THANZAWMYINT\\SQLEXPRESS", // server name
            InitialCatalog = "TestDB", // db name
            UserID = "sa",
            Password = "sasa@123",
        };
        public void Read()
        {
            string query = @"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                      FROM [dbo].[Tbl_Blogs]";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();

            foreach(BlogModel item in lst)
            {
                Console.WriteLine("ID..." + item.BlogId);
                Console.WriteLine("Title..." + item.BlogTitle);
                Console.WriteLine("Author..." + item.BlogAuthor);
                Console.WriteLine("Content..." + item.BlogContent);
            }
        }

        public void Edit(int id)
        {
            string query = $@"SELECT [BlogId]
                          ,[BlogTitle]
                          ,[BlogAuthor]
                          ,[BlogContent]
                      FROM [dbo].[Tbl_Blogs] Where BlogId = @BlogId";

            BlogModel blog = new BlogModel()
            {
                BlogId = id,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            
            // var item = db.Query<BlogModel>(query, new { BlogId = id }).FirstOrDefault();
            var item = db.Query<BlogModel>(query, blog).FirstOrDefault();
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
            string query = @"INSERT INTO [dbo].[Tbl_Blogs]
                   ([BlogTitle]
                   ,[BlogAuthor]
                   ,[BlogContent])
             VALUES
                   (@BlogTitle
                   ,@BlogAuthor
                   ,@BlogContent)";

            BlogModel blog = new BlogModel()
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Saving Successful." : "Saving Failed.";

            Console.WriteLine(message);

        }

        public void Update(int id, string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blogs]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
             WHERE [BlogId] = @BlogId";

            BlogModel blog = new BlogModel()
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            Console.WriteLine(message);

        }

        public void Delete(int id)
        {
            string query = $@"Delete FROM [dbo].[Tbl_Blogs] Where [BlogId] = @BlogId";

            BlogModel blog = new BlogModel()
            {
                BlogId = id,
            };

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            Console.WriteLine(message);

        }
    }
}
