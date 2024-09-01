using Microsoft.EntityFrameworkCore;
using SoftballStats.Data;
using SoftballStats.Repositories;
using SoftballStats.Interfaces;
using Microsoft.AspNetCore.Identity;
using SoftballStats.Models;
using SoftballStats.Services;
using Microsoft.AspNetCore.SignalR;
using SoftballStats.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add the db context connection string
builder.Services.AddDbContext<StatContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// add scope for the interfaces and repositories
builder.Services.AddScoped<IPlayer, PlayerRepository>();
builder.Services.AddScoped<ITeam, TeamRepository>();
builder.Services.AddScoped<IStats, StatRepository>();

// add services for the interface and service to work with Cloudinary
builder.Services.AddScoped<IPhotoService, PhotoService>();

// add services for the connection string with Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<StatContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"))
);

// identity framework
builder.Services.AddIdentity<User, IdentityRole>
    (// add password options here
        
    )
    .AddEntityFrameworkStores<StatContext>()
    .AddDefaultTokenProviders();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
