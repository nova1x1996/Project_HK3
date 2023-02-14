using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Models;

namespace Project.Services
{
    public class TaoDuLieu
    {

       
        public static  void SeedData(DatabaseContext context)
        {
            if (context.Roles.Count() < 3)
            {

                //Tạo role
                context.Roles.AddRange(new IdentityRole {
                    Name = "dealer"
                },new IdentityRole {
                    Name = "customer" 
                },new IdentityRole {
                    Name = "admin"
                }
                    );
                //Tạo User
           


                //Tạo Package
                context.Packages.Add(new Package
                {

                    name = "Sliver",
                    duration = 3,
                    details = "LÀ gói ok",
                    status = "Đang hoạt động",
                    price = 2000
                });
                context.Packages.Add(new Package
                {

                    name = "Sliver",
                    duration = 3,
                    details = "LÀ gói ok",
                    status = "Đang hoạt động",
                    price = 3000
                });
                context.SaveChanges();
              
            }
        }
           
     

      
    }
}
