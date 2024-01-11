using AcademiaFarsiman.Api.Features.Personas.Services;
using AcademiaFarsiman.Api.Infrastructure;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using Farsiman.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace AcademiaFarsiman.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Exclusivamente del proyecto

            services.AddScoped(ResolverAcademiaFarsimanContext);


            services.AddScoped<UnitOfWorkBuillder>();
            services.AddScoped<AcademiaFarsimanUnitOfWork>();
            services.AddTransient<PersonaDomain>();
            services.AddTransient<PersonasService>();
            services.AddAutoMapper(typeof(Program));
        }

        public virtual AcademiaFarsimanContext ResolverAcademiaFarsimanContext(IServiceProvider arg)
        {
            var databaseBuilder = new DbContextOptionsBuilder<AcademiaFarsimanContext>();

            string? connectionString = Configuration.GetConnectionStringFromENV("AcademiaFarsiman");
            databaseBuilder.UseSqlServer(connectionString);

            return new AcademiaFarsimanContext(databaseBuilder.Options);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

