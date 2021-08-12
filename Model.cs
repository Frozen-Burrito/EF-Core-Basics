using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EFBasics
{
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public string DbPath { get; private set; }

        public BlogContext()
        {
            // Set a connection string, later used in OnConfiguring.
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}blog.db";
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            :base(options)
        {
            // The options parameter allows explicit constructuring of 
            // a BlogContext using DI.
        }

        // Perform context configuration with this override.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}"); // Configure instance db provider
    }

    // Both 'Blog' and 'Post' represent entities.
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();

        public override string ToString()
        {
            return $"Url: {Url}, with {Posts.Count} posts.";
        }
    }

    public class Post 
    {
        public int PostId { get; set; }
        public string Title { get; set; }   
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}