using Baubit.Configuration;
using System.Reflection;
using Baubit.Store;
using Baubit.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Baubit.xUnit
{
    public abstract class AFixture<TBroker> : IFixture<TBroker>, IDisposable where TBroker : class, ITestBroker
    {
        public TBroker Broker
        {
            get;
            protected set;
        }

        private RootModule rootModule;
        protected AFixture()
        {
            var configSourceAttribute = typeof(TBroker).GetCustomAttribute<JsonConfigurationSourceAttribute>();
            if (configSourceAttribute == null)
            {
                throw new Exception($"{nameof(JsonConfigurationSourceAttribute)} not found on {typeof(TBroker).Name}{Environment.NewLine}The generic type parameter TBroker requires a {nameof(JsonConfigurationSourceAttribute)} to initialize test fixtures.");
            }
            var fullyQualifiedResourceName = $"{typeof(TBroker).Namespace}.{configSourceAttribute.Source}.json";

            var readResourceResult = typeof(TBroker).Assembly.ReadResource(fullyQualifiedResourceName).GetAwaiter().GetResult();

            if (!readResourceResult.IsSuccess) { throw new Exception("Unable to read Broker configuration source !"); }

            var fixtureConfiguration = new ConfigurationSource() { RawJsonStrings = [readResourceResult.Value] }.Load();

            rootModule = new RootModule(fixtureConfiguration);

            var services = new ServiceCollection();

            services.AddSingleton<TBroker>();

            rootModule.Load(services);

            var serviceProvider = services.BuildServiceProvider();

            Broker = serviceProvider.GetRequiredService<TBroker>();
        }

        public virtual void Dispose()
        {

        }
    }
}
