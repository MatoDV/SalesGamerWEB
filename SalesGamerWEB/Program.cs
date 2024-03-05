using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesGamerWEB.Controllers;
using SalesGamerWEB.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SalesGamerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure the database connection
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
DB_Controller.Initialize(builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "/Login",
        defaults: new { controller = "Login", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "register",
        pattern: "/Register",
        defaults: new { controller = "Register", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "productos",
        pattern: "/Productos",
        defaults: new { controller = "Producto", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "About",
        pattern: "/About",
        defaults: new { controller = "About", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "Contacto",
        pattern: "/Contacto",
        defaults: new { controller = "Contacto", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "Compra",
        pattern: "/Compra",
        defaults: new { controller = "Compra", action = "Index" }
    );
});
