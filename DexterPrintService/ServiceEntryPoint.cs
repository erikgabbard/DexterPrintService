using Topshelf;
using Topshelf.Ninject;

namespace DexterPrintService
{
    class ServiceEntryPoint
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.UseNinject(new IocModule());

                x.Service<PrintService>(svc =>
                {
                    svc.ConstructUsingNinject();
                    svc.WhenStarted((service, hostControl) => service.Start(hostControl));
                    svc.WhenStopped((service, hostControl) => service.Stop(hostControl));
                });

                x.RunAsNetworkService();
                x.SetDescription("Dexter Print Service using RabbitMQ.");
                x.SetDisplayName("Dexter Print Service");
                x.SetServiceName("DexterPrintService");
            });
        }
    }
}
