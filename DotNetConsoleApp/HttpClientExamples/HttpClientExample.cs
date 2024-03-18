using DotNetConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            // await Read();
            await ReadJsonPlaceholder();   
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7235/api/Blogs");
            if(response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);

                // JsonConvert.SerializeObject(); // C # object to JSON string
                // JsonConvert.DeserializeObject(); // JSON string to C# object

                List<BlogModel> lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;

                foreach (BlogModel item in lst)
                {
                    Console.WriteLine("ID..." + item.BlogId);
                    Console.WriteLine("Title..." + item.BlogTitle);
                    Console.WriteLine("Author..." + item.BlogAuthor);
                    Console.WriteLine("Content..." + item.BlogContent);
                }
            }
        }

        // https://jsonplaceholder.typicode.com/posts
        public async Task ReadJsonPlaceholder()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);

                // JsonConvert.SerializeObject(); // C # object to JSON string
                // JsonConvert.DeserializeObject(); // JSON string to C# object

                List<PostModel> lst = JsonConvert.DeserializeObject<List<PostModel>>(jsonStr)!;

                foreach (PostModel item in lst)
                {
                    Console.WriteLine("ID..." + item.userId);
                    Console.WriteLine("Title..." + item.id);
                    Console.WriteLine("Author..." + item.title);
                    Console.WriteLine("Content..." + item.body);
                }
            }
        }
    }
}
