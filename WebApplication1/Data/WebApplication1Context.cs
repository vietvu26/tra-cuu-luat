using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Views.Home;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseName = "C:\\Users\\Viet\\Documents\\Zalo Received Files\\net03-1.db";

            optionsBuilder.UseSqlite($"Data Source={databaseName}");
        }

        public DbSet<WebApplication1.Views.Home.Article> Article { get; set; } = default!;

        public DbSet<WebApplication1.Views.Home.Section>? Section { get; set; }
    }
}
