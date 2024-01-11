using System.Net.Http.Headers;
using System.Reflection;
using AcademiaFarsiman.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace AcademiaFarsiman.IntegrationTests.Helpers
{
    public class TestFixture<TStartup>
    {

        public static string GetProjectPath(string projectRelativePath, Assembly startupAssembly)
        {
            string? projectName = startupAssembly.GetName().Name;

            string? applicationBasePath = AppContext.BaseDirectory;

            DirectoryInfo? directoryInfo = new DirectoryInfo(applicationBasePath);

            if (projectName == null) return "";

            do
            {
                directoryInfo = directoryInfo.Parent;
                if (directoryInfo == null) return "";

                DirectoryInfo? projectDirectoryInfo = new DirectoryInfo(Path.Combine(directoryInfo.FullName, projectRelativePath));

                if (projectDirectoryInfo.Exists && new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                {
                    return Path.Combine(projectDirectoryInfo.FullName, projectName);
                }
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
        }

        private readonly TestServer Server;

        public TestFixture()
            : this(Path.Combine("../"))
        {
        }

        HttpClient HttpClient { get; }
        public RestClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            HttpClient.Dispose();
            Server.Dispose();
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            Assembly? startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            ApplicationPartManager? manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

        protected TestFixture(string relativeTargetProjectParentDir)
        {
            Assembly? startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;
            string? contentRoot = GetProjectPath(relativeTargetProjectParentDir, startupAssembly);

            IConfigurationBuilder? configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(contentRoot)
                .AddJsonFile("appsettings.Tests.json");

            IWebHostBuilder? webHostBuilder = new WebHostBuilder()
                .UseContentRoot(contentRoot)
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Production")
                .UseStartup(typeof(StartupFacade));

            Server = new TestServer(webHostBuilder);

            HttpClient = Server.CreateClient();
            HttpClient.BaseAddress = new Uri("http://localhost:5001");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = new RestClient(HttpClient);
        }

    }
}

