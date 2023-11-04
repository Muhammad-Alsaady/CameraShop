using CameraShop.DataAccess;
using CameraShop.DataAccess.Repository;
using CameraShop.DataAccess.Repository.IRepository;
using CameraShop.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")??
        throw new InvalidOperationException("No DB connection is stablished");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy => policy.RequireRole(SD.Role_Admin));
    });


builder.Services.AddAuthentication().AddFacebook(fbOptions =>
        {
            fbOptions.AppId = builder.Configuration.GetSection("FacebookSettings").GetValue<string>("AppId");
            fbOptions.AppSecret = builder.Configuration.GetSection("FacebookSettings").GetValue<string>("AppSecret");
        });

builder.Services.AddAuthentication().AddGoogle(GOptions =>
{
    GOptions.ClientId = builder.Configuration.GetSection("GoogleSettings").GetValue<string>("ClientId");
    GOptions.ClientSecret = builder.Configuration.GetSection("GoogleSettings").GetValue<string>("ClientSecret");
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
