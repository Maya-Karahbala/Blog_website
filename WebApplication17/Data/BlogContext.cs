using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication17.Models;

namespace WebApplication17.Data
{
    public class BlogContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=127.0.0.1; port=3306;user=root;password=1234;database=blogdb")
                .UseLoggerFactory(LoggerFactory.Create(b => b
            .AddConsole().AddFilter(level => level > LogLevel.Information))).EnableSensitiveDataLogging().EnableDetailedErrors();
            //optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ContosoAppData");
        }
    }
}
