using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Models;
using Project.Models.Domain;
using Project.Models.DTO;
using Project.Repositories.Abstract;
using Project.Repositories.Implementation;

namespace Project.Services
{
    public class TaoDuLieu
    {
        private readonly IUserAuthenticationService _authService1;



      
        public static  void SeedData(DatabaseContext context)
        {
            if (context.Movies.Count() < 3)
            {
              


                //Tạo role
                context.Roles.AddRange(new IdentityRole {
                    Id = "1",
                    Name = "dealer",
                    NormalizedName = "DEALER"
                    
                },new IdentityRole {
                    Id = "2",
                    Name = "customer",
                    NormalizedName ="CUSTOMER"
                    
                },new IdentityRole {
                    Id = "3",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                    
                }
                    );
                //Tạo User


                //Tạo FAQ
                //Tạo Movies_Category
                context.Faq.AddRange(new Faq
                {
                    question = "What is satellite television and how does it work?",
                    answer = "Satellite television is a broadcast delivery system that uses communication satellites orbiting the earth to transmit television signals. A satellite dish installed at the user's location receives the satellite signals and sends them to a set-top box or integrated television with a built-in receiver. The user can then watch the satellite television channels using their television set.",
                    status = "show",
                }, new Faq
                {
                    question = "What are the advantages of satellite television over other broadcast methods?",
                    answer = "Satellite television offers a wider selection of channels than cable or over-the-air broadcasts, and the signal quality is often superior. It can also be accessed from remote or rural areas where other broadcast methods may be unavailable.",
                    status = "show",
                }, new Faq
                {
                    question = "How can I subscribe to your satellite television service?",
                    answer = "To subscribe to our satellite television service, please visit our website or call our customer service hotline. Our representatives will assist you in choosing a package that best suits your needs and schedule a convenient time for installation.",
                    status = "show",
                }, new Faq
                {
                    question = "What types of packages do you offer, and how much do they cost?",
                    answer = "We offer a variety of packages to fit different viewing needs and budgets, including basic, standard, and premium options. Please visit our website or contact our customer service hotline for detailed information on package offerings and pricing.",
                    status = "show",
                }, new Faq
                {
                    question = "Can I watch satellite television on multiple devices?",
                    answer = "Yes, you can connect multiple televisions or other devices to your satellite service, depending on your package and equipment. Please contact our customer service hotline for more information on multi-room viewing options.",
                    status = "show",
                }, new Faq
                {
                    question = "What channels and programs are included in your packages?",
                    answer = "We offer a wide range of channels and programs, including local and international news, sports, entertainment, movies, and more. Please visit our website or contact our customer service hotline for more information on specific channel lineups and programming.",
                    status = "show",
                }, new Faq
                {
                    question = "How can I troubleshoot problems with my satellite service?",
                    answer = "If you are experiencing issues with your satellite service, please first check your equipment connections and power supply. If the issue persists, please contact our customer service hotline for further troubleshooting assistance.",
                    status = "show",
                }
                );

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
                   img= "/img/movie/TheDK.jpg",
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
                    img = "/img/movie/inception.jpg",
                    content = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                    price = 20,
                    movie_cate_id = 2,
                }, new Movie
                {
                    name = "You People",
                    img = "/img/movie/You_People_Film_Poster.jpg",
                    content = "Follows a new couple and their families, who find themselves examining modern love and family dynamics amidst clashing cultures, societal expectations and generational differences.",
                    price = 10,
                    movie_cate_id = 1,
                }, new Movie
                {
                    name = "Your Place or Mine",
                    img = "/img/movie/Your_Place_Or_Mine.jpg",
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

                //Tạo Set Up Box
                context.SetUpBoxes.AddRange(new SetUpBox{
                name = "R-DTH Digital TV SD",
                details = "R-DTH Digital TV SD is a type of digital television service that provides standard definition (SD) channels to viewers. ",
                img = "/img/setupbox/SUBSD.jpg",
                price = 500,
                
               
                },new SetUpBox {
                    name = "R-DTH Digital TV HD",
                    details = "R - DTH Digital TV HD is a type of digital television service that provides high definition(HD) channels to viewers. ",
                    img = "/img/setupbox/SUBHD.jpg",
                    price = 700,

                },new SetUpBox { 
                    name= "R-DTH Digital TV 4K",
                    details = "R-DTH Digital TV 4K is a type of digital television service that provides ultra-high definition (UHD) 4K channels to viewers.",
                    img = "/img/setupbox/SUB4K.jpg",
                    price = 1000,
                });

                //Tạo Package
                context.Packages.Add(new Package
                {

                    name = "Bronze Pack",
                    duration = 1,
                    details = "80 channels (including R-DTH Cab) + VOD library",
                    status = true,
                    price = 100
                });
                context.Packages.Add(new Package
                {

                    name = "Sliver Pack",
                    duration = 1,
                    details = "100 channels (including R-DTH Cab) + VOD library",
                    status = true,
                    price = 130
                });
                context.Packages.Add(new Package
                {

                    name = "Gold Pack",
                    duration = 1,
                    details = "144 channels (including R-DTH Cab) + VOD library",
                    status = true,
                    price = 150
                });
                context.Packages.Add(new Package
                {

                    name = "Diamond Pack",
                    duration = 1,
                    details = "160 channels (including R-DTH Cab) + VOD library + Premium Galaxy",
                    status = true,
                    price = 200
                });
                context.SaveChanges();
              
            }
        }
           
     

      
    }
}
