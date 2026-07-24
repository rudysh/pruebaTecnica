using Microsoft.EntityFrameworkCore;
using pruebaTecnica.Data;
using pruebaTecnica.Repositories;

namespace pruebaTecnica
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString =
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection"
                    );

                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IArticuloRepository,ArticuloRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Articulos}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
