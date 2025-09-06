using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

namespace xTest1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddSingleton<ICountriesService, CountryService>();
            builder.Services.AddSingleton<IPersonsService, PersonService>();

            builder.Services.AddDbContext<PersonDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
                options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllers();
            

            app.Run();
        }
    }
}
