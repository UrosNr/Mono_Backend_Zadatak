using Microsoft.EntityFrameworkCore;
using Project.Service.Database;
using Project.Service.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(connStr));
builder.Services.AddScoped<IVehicleDbContext>(provider => provider.GetService<VehicleDbContext>());
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

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
