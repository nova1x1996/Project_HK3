using System;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
	public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> User { get; set; }
    }
	
}

