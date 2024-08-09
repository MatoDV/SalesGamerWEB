using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesGamerWEB.Controllers;
using SalesGamerWEB.Models;

var builder = WebApplication.CreateBuilder(args);

// Inicializar la base de datos
DB_Controller.Initialize(builder.Configuration);

// Configurar servicios de correo electrónico
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>(); // Registramos EmailSender como IEmailSender

// Configurar DbContext e Identity
builder.Services.AddDbContext<SalesGamerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<SalesGamerDbContext>()
.AddDefaultTokenProviders();

// Configurar autenticación y cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Asegúrate de que esto esté antes de UseAuthorization
app.UseAuthorization();

// Configurar las rutas
app.UseEndpoints(endpoints =>
{
    // Rutas para las páginas de inicio de sesión, registro y carrito
    endpoints.MapControllerRoute(
        name: "login",
        pattern: "/Login",
        defaults: new { controller = "Login", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "carrito",
        pattern: "/Carrito",
        defaults: new { controller = "Carrito", action = "Index" }
    );

    endpoints.MapControllerRoute(
        name: "register",
        pattern: "/Register",
        defaults: new { controller = "Register", action = "Index" }
    );

    // Ruta por defecto
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    // Otras rutas específicas
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

    endpoints.MapRazorPages();
});

// Redireccionar al login si el usuario no está autenticado al intentar acceder al carrito
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/Carrito") && !context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Login");
        return;
    }

    await next.Invoke();
});

app.Run();
