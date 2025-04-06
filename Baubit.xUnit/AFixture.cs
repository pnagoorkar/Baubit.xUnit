using Baubit.Configuration;
using System.Reflection;
using Baubit.DI;
using Microsoft.Extensions.DependencyInjection;
using Baubit.Traceability.Errors;

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

            var services = new ServiceCollection();
            services.AddSingleton<TBroker>();
            var configurationAddResult = services.AddFrom(configurationSource);
            if (!configurationAddResult.IsSuccess)
            {
                throw new AggregateException(new CompositeError<IServiceCollection>(configurationAddResult).ToString());
            }
            Broker = configurationAddResult.Value.BuildServiceProvider().GetRequiredService<TBroker>();
        }

        public virtual void Dispose()
        {

        }
    }
}
