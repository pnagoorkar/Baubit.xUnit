using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit
{
    public class TestBrokerConfiguration : AModuleConfiguration
    {

    }

    public class TestBrokerFactory : AModule<TestBrokerConfiguration>, ITestBrokerFactory
    {
        public TestBrokerFactory(ConfigurationSource configurationConfiguration) : base(configurationConfiguration)
        {
        }

        public TestBrokerFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public TestBrokerFactory(TestBrokerConfiguration moduleConfiguration, List<AModule> nestedModules) : base(moduleConfiguration, nestedModules)
        {
        }

        public override void Load(IServiceCollection services)
        {
            base.Load(services);
        }

        public TBroker LoadBroker<TBroker>() where TBroker : class, ITestBroker
        {
            var services = new ServiceCollection();
            services.AddSingleton<TBroker>();
            Load(services);
            return services.BuildServiceProvider().GetRequiredService<TBroker>();
        }
    }
}
