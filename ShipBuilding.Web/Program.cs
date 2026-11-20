using ShipBuilding.Services;

namespace ShipBuilding.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            RegisterServices(builder);

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

        private static void RegisterServices(WebApplicationBuilder builder)
        {
            // Файл даних лежить поза wwwroot, щоб його не віддавав UseStaticFiles
            string dataFolder = Path.Combine(builder.Environment.ContentRootPath,
                builder.Configuration["Storage:DataFolder"] ?? "App_Data");

            // У режимі розробки порожнє сховище заповнюється тестовими даними
            builder.Services.AddShipBuildingServices(dataFolder,
                seedTestData: builder.Environment.IsDevelopment());

            // Перетворення між даними сервісів і view-моделями: профілі в ViewModels/<Сутність>/…Profile.cs.
            // Ключ ліцензії (cfg.LicenseKey) поки не задано — AutoMapper лише пише попередження в лог
            builder.Services.AddAutoMapper(cfg => { }, typeof(Program));
        }
    }
}
