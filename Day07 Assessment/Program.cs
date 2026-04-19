using Day07_Assessment.Infrastructure.Repositories_Implementations;
using Day07_Assessment.Domain.Repository_Interfaces;
using Day07_Assessment.Infrastructure.Data.DbContexts;
using Day07_Assessment.Presentation.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Day07_Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                            .AddRazorOptions(options =>
                            {
                                options.ViewLocationFormats.Add("/Presentation/Views/{1}/{0}.cshtml");
                                options.ViewLocationFormats.Add("/Presentation/Views/Shared/{0}.cshtml");
                            });
            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Task/Error");
            }
            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Task}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
