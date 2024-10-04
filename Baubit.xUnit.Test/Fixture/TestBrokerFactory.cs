using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit.Test.Fixture
{
    public class TestBrokerConfiguration : ATestBrokerFactoryConfiguration
    {

    }

    public sealed class TestBrokerFactory : ATestBrokerFactory<TestBrokerConfiguration>
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
    }
}
