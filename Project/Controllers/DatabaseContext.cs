using System;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
	public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("WebDatabase"));
        }

        public DbSet<User> User { get; set; }
    }
	
}

