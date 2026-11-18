using ShipBuilding.Repositories;
using ShipBuilding.Repositories.Interfaces;
using ShipBuilding.Web.Services;

namespace ShipBuilding.Web
{
    public class Program
    {
        public static void Main(string[] args)
       {
            var builder = WebApplication.CreateBuilder(args);

            RegisterUoW(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            //app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }

        private static void RegisterUoW(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<DataInitializer>();
            builder.Services.AddSingleton<IUnitOfWork>(sp => {
                var initializer = sp.GetRequiredService<DataInitializer>();
                return initializer.CreateUnitOfWork();
            });
            builder.Services.AddTransient<IStatisticsRepository, StatisticsRepository>();
        }
    }
}
