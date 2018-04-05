using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using log4net;

namespace MarketDss.Infrastructure.Service
{
    public class ServiceMain
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(ServiceMain));

        public static void Main(string[] args)
        {
            var bootstrapper = new ServiceBootstrapper();
            var configuration = bootstrapper.Configuration;
            var apiHost = bootstrapper.ApiHost;

            if (configuration.IsDebuggerAttached)
            {
                apiHost.Run();
            }
            else
            {
                apiHost.RunAsService();
            }
        }
    }
}
