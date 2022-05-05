using BootstrapBlazor.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Models;

public class BloggingContext : DbContext
{
    public BloggingContext(DbContextOptions options) : base(options) { }
    protected BloggingContext() { }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=db.db");
    }
}

public class Blog
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Url { get; set; }
    public int Rating { get; set; }
    public bool Required { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}


