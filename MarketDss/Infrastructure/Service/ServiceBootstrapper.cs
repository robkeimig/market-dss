using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using log4net;
using log4net.Config;
using MarketDss.Business.DividendCapture;
using MarketDss.Business.Securities;
using MarketDss.Infrastructure.Configuration;
using MarketDss.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace MarketDss.Infrastructure.Service
{
    internal class ServiceBootstrapper
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(ServiceBootstrapper));

        public ServiceBootstrapper()
        {
            Bootstrap();
        }

        public ServiceConfiguration Configuration { get; private set; }

        public IWebHost ApiHost { get; private set; }

        private void Bootstrap()
        {
            XmlConfigurator.Configure();
            SchemaContext.Initialize();
            var connectionString = ConfigurationManager.ConnectionStrings["ServiceDbConnection"].ConnectionString;
            var settingRepository = new SettingRepository(connectionString);
            var settingService = new SettingService(settingRepository);
            var configurationService = new ConfigurationService(settingService, connectionString);
            Configuration = configurationService.GetConfiguration();
            var bindUrl = GetHttpSysBindUrl(Configuration.UrlBinding);
            AddUrlAcl(bindUrl);
            ApiHost = new WebHostBuilder()
                .ConfigureServices(sc => BuildContainer(sc))
                .UseHttpSys(options =>
                {
                    options.UrlPrefixes.Add(bindUrl);
                })
                .UseContentRoot(Configuration.WwwRootParentPath)
                .UseStartup<ApiStartup>()
                .Build();
        }

        private void BuildContainer(IServiceCollection sc)
        {
            //AspNetCore MVC
            sc.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Formatting = Formatting.Indented;
            });

            //Swagger
            sc.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MarketDSS", Version = "v1" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "MarketDss.xml");
                c.IncludeXmlComments(xmlPath);
            });

            sc.AddSingleton(Configuration);
            sc.AddSingleton<SecuritiesRepository>();
            sc.AddSingleton<SecuritiesService>();
            sc.AddSingleton<DividendCaptureDecisionService>();
            sc.AddSingleton<SettingRepository>();
            sc.AddSingleton<SettingService>();
        }

        private string GetHttpSysBindUrl(string source)
        {
            var uriReader = new Uri(source);
            var scheme = uriReader.Scheme;
            var port = uriReader.Port;
            var url = $"{scheme}://+:{port}/";
            return url;
        }

        private void AddUrlAcl(string httpSysBindUrl)
        {
            var arguments = $"http add urlacl url=\"{httpSysBindUrl}\" user=\"{WindowsIdentity.GetCurrent().Name}\"";
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    Verb = "runas",
                    Arguments = arguments,
                    FileName = "netsh"
                }
            };
            Log.Info($"Ensuring URL ACL - netsh {arguments}");
            process.Start();
            process.WaitForExit();
        }
    }
}