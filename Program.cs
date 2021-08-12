using System;
using System.Linq;

namespace EFBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            /*  Using a DbContext
                The lifetime of a DbContext instance should span a single unit
                of work. 

                A typical unit of work with EF core includes:
                - Creation of a DbContext instance
                - Tracking of entities
                - Changes are made to tracked entities
                - A call to 'SaveChanges' or 'SaveChangesAsync'
                - Disposal of the DbContext instance

                ! DbContext instances are not thread-safe
            */
            using (var db = new BlogContext())
            {
                Console.WriteLine($"Database path: {db.DbPath}");

                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();
                
                Console.WriteLine(blog.ToString());

                // Update 
                System.Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";
                blog.Posts.Add(
                    new Post { Title="Hello World", Content="The first EF post!" }
                );
                db.SaveChanges();

                // Querying again
                blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();
                
                Console.WriteLine("Updated blog: " + blog.ToString());

                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}
