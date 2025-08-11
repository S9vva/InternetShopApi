using InternetShopApi.Data.Data;
using InternetShopApi.Modul;
using InternetShopApi.Service.Service.Extensions;
using InternetShopApi.Seed;
using System.Threading.Tasks;
using InternetShopApi.Middleware;


namespace InternetShopApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            });

            // Add services to the container.
            builder.Services.AddDataServices(builder.Configuration);
            builder.Services.AddService();
            builder.Services.AddIdentityAndJwt(builder.Configuration);
            builder.Services.AddSwaggerWithJwt();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedData.SeedRolesAsync(services);
                await SeedData.SeedAdminToUserAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<ErrorMidleware>();

            app.UseHttpLogging();

            app.UseHttpsRedirection();


            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();



            app.Run();
        }

    }
}
