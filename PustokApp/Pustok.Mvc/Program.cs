using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Pustok.Mvc;
using Pustok.Mvc.Data;
using Pustok.Mvc.Dto;
using Pustok.Mvc.Models;
using Pustok.Mvc.Services;

var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup()
    .LoadConfigurationFromFile("nlog.config")
    .GetCurrentClassLogger();

try
{
    // Default logging-i sil
   builder.Logging.ClearProviders();

    // NLog ?lav? et
    builder.Host.UseNLog();



    // Add services to the container.
    builder.Services.AddControllersWithViews();
    //.AddNewtonsoftJson(options =>
    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    //);
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });
    builder.Services.AddScoped<LayoutService>();
    builder.Services.AddScoped<IEmailService, EmailService>();
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(1);
    });
    builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        //options.SignIn.RequireConfirmedEmail = true;
        options.User.RequireUniqueEmail = true;
        //options.SignIn.RequireConfirmedEmail = true;
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.AllowedForNewUsers = true;
    }).AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseSession();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
              );

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<LogMiddleware>();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Unhandled exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}