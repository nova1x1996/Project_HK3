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
            if (context.Movies.Count() < 3)
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


                //Tạo Movies_Category
                context.Movie_Cates.AddRange(new Movie_cate {
                    name = "Romatic",
                },new Movie_cate {
                    name = "Action"
                },new Movie_cate {
                    name = "Comedy"
                }
                );
                context.SaveChanges();

                //Tạo Movies
                context.Movies.AddRange(new Movie
                {
                   name= "The Dark Knight",
                   img= "/img/movie/inception.jpg",
                   content = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                   price = 10,
                    movie_cate_id = 2 ,


                }, new Movie
                {
                    name = "The Lord of the Rings: The Return of the King",
                    img = "/img/movie/LOTR.jfif",
                    content = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    price = 15,
                    movie_cate_id = 2,
                }, new Movie
                {
                    name = "Inception",
                    img = "/img/movie/TheDK.jfif",
                    content = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                    price = 20,
                    movie_cate_id = 2,
                }, new Movie
                {
                    name = "You People",
                    img = "/img/movie/YourPOM.jpg",
                    content = "Follows a new couple and their families, who find themselves examining modern love and family dynamics amidst clashing cultures, societal expectations and generational differences.",
                    price = 10,
                    movie_cate_id = 1,
                }, new Movie
                {
                    name = "Your Place or Mine",
                    img = "/img/movie/TheDK.jfif",
                    content = "Two long-distance best friends change each other's lives when she decides to pursue a lifelong dream and he volunteers to keep an eye on her teenage son.",
                    price = 15,
                    movie_cate_id = 1,
                }, new Movie
                {
                    name = "Empire of Light",
                    img = "/img/movie/large_empire-of-light-movie-poster-2022.jpeg",
                    content = "A drama about the power of human connection during turbulent times, set in an English coastal town in the early 1980s.",
                    price = 12,
                    movie_cate_id = 1,
                }, new Movie
                {
                    name = "Step Brothers",
                    img = "/img/movie/Step_Brothers.jfif",
                    content = "Two aimless middle-aged losers still living at home are forced against their will to become roommates when their parents marry.",
                    price = 8,
                    movie_cate_id = 3,
                }, new Movie
                {
                    name = "White Chicks",
                    img = "/img/movie/White_Chicks.jpg",
                    content = "Two disgraced FBI agents go way undercover in an effort to protect hotel heiresses the Wilson sisters from a kidnapping plot.",
                    price = 13,
                    movie_cate_id = 3,
                }, new Movie
                {
                    name = "The Hangover",
                    img = "/img/movie/The_Hangover.jfif",
                    content = "Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.",
                    price = 15,
                    movie_cate_id = 3,
                }

               );

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
