using System;
using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class DatabaseContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DatabaseContext()
        {
        }

       
        public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Package> Packages { set; get; }
        public DbSet<Faq> Faq { set; get; }

        public DbSet<SetUpBox> SetUpBoxes { set; get; }
    }

}

