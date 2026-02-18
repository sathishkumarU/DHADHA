using FirstControllerProject.Services;
using FirstControllerProject.Models;
using FirstControllerProject.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ICommonServicescs<StudentDetails>, StudentServices>();
//builder.Services.AddScoped<ICommonServicescs<Chariot>, ChariotsServices>();
//builder.Services.AddScoped<StudentServices>();
//builder.Services.AddScoped<DHADHAMemberBO>();
builder.Services.AddScoped<IUserMaster,UserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()                     // Log level
    .WriteTo.Console()                               // Logs in console
    .WriteTo.File(
        path: "Logs/log-.txt",                       // Folder "Logs" will be created automatically
        rollingInterval: RollingInterval.Day,       // Daily log files
        retainedFileCountLimit: 7,                  // Keep last 7 log files
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog();

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
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
