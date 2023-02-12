using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Models;

namespace Project.Services
{
    public class TaoDuLieu
    {

       
        public static  void SeedData(DatabaseContext context)
        {
            if (context.User.Count() == 0)
            {
                //Tạo User
                context.User.AddRange(new User
                {
                    card_number = "0001",
                    name = "John Doe",
                    password = "password123",
                    email = "johndoe@example.com",
                    phone = "1234567890",
                    address = "123 Main Street",
                    services_sub_date = new DateTime(2020, 1, 1),
                    date_left = null,
                    payment_monthly = 50,
                    package_id = 1

                },
                new User
                {
                    card_number = "0002",
                    name = "Jane Doe",
                    password = "password456",
                    email = "janedoe@example.com",
                    phone = "0987654321",
                    address = "456 Second Street",
                    services_sub_date = new DateTime(2020, 2, 1),
                    date_left = null,
                    payment_monthly = 60,
                    package_id = 2
                });


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
