using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Services;
using NuGet.Protocol;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<DatabaseContext>(options => 

options.UseSqlServer(builder.Configuration.GetConnectionString("WebDatabaseKhoi")));
builder.Services.AddSession();
var app = builder.Build();

//Tạo dữ liệu tự động trong Database
using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
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
