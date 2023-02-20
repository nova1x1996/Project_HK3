using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Models.DTO;
using Project.Services;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;

using AspNetCoreHero.ToastNotification;


using Project.Models.Domain;
using Project.Repositories.Abstract;
using Project.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
builder.Services.AddDbContext<DatabaseContext>(options => 

options.UseSqlServer(builder.Configuration.GetConnectionString("WebDatabasePhong")));




builder.Services.AddSession();
// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

//add services to container
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
var app = builder.Build();


//Tạo dữ liệu tự động trong Database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context  = serviceScope.ServiceProvider.GetService<DatabaseContext>();
    context.Database.Migrate();
    TaoDuLieu.SeedData(context);
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();
//Tự thêm
app.UseAuthentication();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=MainAdmin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
