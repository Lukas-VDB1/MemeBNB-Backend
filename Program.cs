using Microsoft.EntityFrameworkCore;
using Programming_Web_API.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Programming_Web_API.Interfaces;
using Programming_Web_API.Repository;
using Programming_Web_API.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Text.Json.Serialization;

namespace Programming_Web_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // CORS
            builder.Services.AddCors(s => s.AddPolicy("MyPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Dependency Injections
            builder.Services.AddScoped<ICampingSpotRepository, CampingSpotRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<JwtService>();

            //Error: Possible object cycle was detected which is not supported
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });


            // Add DB context
            builder.Services.AddDbContextPool<DataContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            // Add Authentication services
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })


            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = "557930415010-e9skpnol7sgho2avro3q1qlieetilj7t.apps.googleusercontent.com";
                options.ClientSecret = "GOCSPX-CdDFGWkp5XRikiv 9HOqLpczNQMbJ";
                options.CallbackPath = "/auth/callback";
            });


            var app = builder.Build();

            // Use CORS
            app.UseCors("MyPolicy");


            app.UseCookiePolicy();

            // Use Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
