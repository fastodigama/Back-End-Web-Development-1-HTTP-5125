using System.Data.Common;
using Cumulative1.Models;
using Cumulative1.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Register DBConnection as a scoped service.
builder.Services.AddScoped<SchoolDbContext>();
builder.Services.AddScoped<TeacherAPIController>();
builder.Services.AddScoped<StudentAPIController>();
builder.Services.AddScoped<CoursesAPIController>();


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
