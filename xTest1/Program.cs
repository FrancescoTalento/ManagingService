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
