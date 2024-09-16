using Task_manager.DAL;
using Microsoft.EntityFrameworkCore;
using Task_manager.DAL.Interfaces;
using Task_manager.Entity;
using Task_manager.DAL.Repositories;
using Task_manager;
using Microsoft.AspNetCore.Razor.Runtime;
using Microsoft.Extensions.Configuration;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    DbContextOptionsBuilder dbContextOptionsBuilder = options.UseSqlServer(builder.Configuration.GetConnectionString("MSQL"));
});
builder.Services.AddScoped<IBaseRepository<TaskEntity>, TaskRepository>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.RegisterRepositories();



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
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
