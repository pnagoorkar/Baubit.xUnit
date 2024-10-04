using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit
{
    public abstract class ATestBrokerFactory<TConfiguration> : AModule<TConfiguration>, ITestBrokerFactory where TConfiguration : ATestBrokerFactoryConfiguration
    {
        protected ATestBrokerFactory(ConfigurationSource configurationSource) : base(configurationSource)
        {
        }

        protected ATestBrokerFactory(IConfiguration configuration) : base(configuration)
        {
        }

        protected ATestBrokerFactory(TConfiguration moduleConfiguration, List<AModule> nestedModules) : base(moduleConfiguration, nestedModules)
        {
        }


        public TBroker LoadBroker<TBroker>() where TBroker : class, ITestBroker
        {
            var services = new ServiceCollection();
            Load(services);
            RegisterBroker<TBroker>(services);
            return services.BuildServiceProvider().GetRequiredService<TBroker>();
        }

        protected virtual void RegisterBroker<TBroker>(IServiceCollection services) where TBroker : class, ITestBroker
        {
            services.AddSingleton<TBroker>();
        }
    }
}
