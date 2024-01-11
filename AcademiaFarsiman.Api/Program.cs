using AcademiaFarsiman.Api.Features.Personas.Services;
using AcademiaFarsiman.Api.Infrastructure;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using Farsiman.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFarsiman.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


