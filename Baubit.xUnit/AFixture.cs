using Baubit.Configuration;
using System.Reflection;
using Baubit.DI;

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
            var configSourceAttribute = typeof(TBroker).GetCustomAttribute<EmbeddedJsonSourcesAttribute>();

            if (configSourceAttribute == null)
            {
                throw new Exception($"{nameof(EmbeddedJsonSourcesAttribute)} not found on {typeof(TBroker).Name}{Environment.NewLine}The generic type parameter TBroker requires a {nameof(EmbeddedJsonSourcesAttribute)} to initialize test fixtures.");
            }

            var configurationSource = new ConfigurationSource();
            configurationSource.EmbeddedJsonResources = configSourceAttribute.Values;

            Broker = configurationSource.Load()
                                        .As<ITestBrokerFactory>()
                                        .Resolve<TBroker>();
        }

        public virtual void Dispose()
        {

        }
    }
}
