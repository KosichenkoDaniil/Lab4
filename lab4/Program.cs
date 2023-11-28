using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;


namespace lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            string connection = builder.Configuration.GetConnectionString("MSSQL");

            // внедрение зависимости для доступа к БД с использованием EF
            builder.Services.AddDbContext<BankDeposits1Context>(options => options.UseSqlServer(connection));
            builder.Services.AddControllersWithViews(options => {
                options.CacheProfiles.Add("ModelCache",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = 2 * 17 + 240
                    });
            });

            builder.Services.AddSession();

            //Использование MVC - отключено
            //services.AddControllersWithViews();
            var app = builder.Build();
            // добавляем поддержку статических файлов
            app.UseStaticFiles();

            // добавляем поддержку сессий
            app.UseSession();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            }); ;


            app.Run();
        }
    }
}
