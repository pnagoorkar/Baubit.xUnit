using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit
{
    public sealed class TestBrokerFactory : ITestBrokerFactory
    {
        public IConfiguration RootModuleConfiguration { get; init; }

        public TestBrokerFactory(IConfiguration rootModuleConfiguration)
        {
            RootModuleConfiguration = rootModuleConfiguration;
        }
        public TestBrokerFactory(ConfigurationSource configurationSource) : this(configurationSource.Load())
        {

        }
        public TBroker Resolve<TBroker>() where TBroker : class, ITestBroker
        {
            var services = new ServiceCollection();
            services.AddSingleton<TBroker>();

            var rootModule = new RootModule(RootModuleConfiguration);
            rootModule.Load(services);

            return services.BuildServiceProvider().GetRequiredService<TBroker>();
        }
    }
}
