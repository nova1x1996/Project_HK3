using System;
using System.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Models.Domain;
namespace Project.Models
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        protected readonly IConfiguration Configuration;
        public DatabaseContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            Configuration = configuration;
        }



        public DbSet<Recharge> Recharges { get; set; }
        public DbSet<Customer_order> Customer_orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Package> Packages { set; get; }
        public DbSet<Faq> Faq { set; get; }

        public DbSet<SetUpBox> SetUpBoxes { set; get; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Movie_cate> Movie_Cates { get; set; }

        public DbSet<CustomerCare> CustomerCare { set; get; }
        public DbSet<Location> Location { set; get; }
        public DbSet<ContactUs> ContactUss { set; get; }
        public DbSet<ChangePackage> ChangePackages { set; get; }
        public DbSet<Dealers> Dealers { get; set; }
        public DbSet<DealersOrder> Dealer_Orders { get; set; }

        public DbSet<Feedback> Feed_Backs { get; set; }
    }

}

