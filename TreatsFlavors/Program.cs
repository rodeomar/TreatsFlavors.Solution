using Microsoft.EntityFrameworkCore;
using TreatsFlavors.Models;




namespace TreatsFlavors
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                        builder.Configuration["ConnectionStrings:DefaultConnection"],
                        ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])
                    )
            );

            var app = builder.Build();

          
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
