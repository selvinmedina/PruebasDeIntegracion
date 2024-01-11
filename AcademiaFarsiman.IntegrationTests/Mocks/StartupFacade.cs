using AcademiaFarsiman.Api;
using AcademiaFarsiman.Api.Infrastructure.AcademiaFarsiman;
using Microsoft.Extensions.Configuration;

namespace AcademiaFarsiman.IntegrationTests.Mocks
{
    public class StartupFacade : Startup
    {
        public StartupFacade(IConfiguration configuration) : base(configuration)
        {
        }

        public override AcademiaFarsimanContext ResolverAcademiaFarsimanContext(IServiceProvider arg)
        {
            return DbContextMocker.GetAppDbContext();
        }
    }
}