using Baubit.Configuration;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Baubit.xUnit
{
    public abstract class AFixture<TBroker> : IFixture<TBroker>, IDisposable where TBroker : class, ITestBroker
    {
        public TBroker Broker
        {
            get;
            protected set;
        }

        protected AFixture()
        {
            var configSourceAttribute = typeof(TBroker).GetCustomAttribute<ConfigurationSourceAttribute>();
            if (configSourceAttribute == null)
            {
                throw new Exception($"{nameof(ConfigurationSourceAttribute)} not found on {typeof(TBroker).Name}{Environment.NewLine}The generic type parameter TBroker requires a {nameof(ConfigurationSourceAttribute)} to initialize test fixtures.");
            }
            var fixtureConfiguration = new MetaConfiguration() { JsonUriStrings = [configSourceAttribute.Source] }.Load();
            var testBrokerFactoryTypeName = fixtureConfiguration["testBrokerFactoryType"];
            var testBrokerFactoryMetaConfiguration = fixtureConfiguration.GetSection("testBrokerFactoryMetaConfiguration")
                                                                         .Get<MetaConfiguration>();
            var testBrokerFactoryType = Type.GetType(testBrokerFactoryTypeName);
            var testBrokerFactory = (ITestBrokerFactory)Activator.CreateInstance(testBrokerFactoryType, testBrokerFactoryMetaConfiguration);

            Broker = testBrokerFactory.LoadBroker<TBroker>();
        }

        public virtual void Dispose()
        {

        }
    }
}
