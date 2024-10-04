using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit
{
    public sealed class TestBrokerFactory : ITestBrokerFactory
    {
        RootModule rootModule;
        public TestBrokerFactory(IConfiguration rootModuleConfiguration)
        {
            rootModule = new RootModule(rootModuleConfiguration);
        }
        public TestBrokerFactory(ConfigurationSource configurationSource) : this(configurationSource.Load())
        {

        }
        public TBroker Resolve<TBroker>() where TBroker : class, ITestBroker
        {
            var services = new ServiceCollection();
            services.AddSingleton<TBroker>();
            rootModule.Load(services);
            return services.BuildServiceProvider().GetRequiredService<TBroker>();
        }
    }
}
