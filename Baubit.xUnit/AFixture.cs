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
            var configSourceAttribute = typeof(TBroker).GetCustomAttribute<JsonConfigurationSourceAttribute>();
            if (configSourceAttribute == null)
            {
                throw new Exception($"{nameof(JsonConfigurationSourceAttribute)} not found on {typeof(TBroker).Name}{Environment.NewLine}The generic type parameter TBroker requires a {nameof(JsonConfigurationSourceAttribute)} to initialize test fixtures.");
            }
            var fullyQualifiedResourceName = $"{typeof(TBroker).Namespace}.{configSourceAttribute.Source}.json";

            var readResourceResult = Baubit.Resource
                                           .Operations.ReadEmbeddedResourceAsync(new Resource.EmbeddedResourceReadContext(fullyQualifiedResourceName, typeof(TBroker).Assembly))
                                           .GetAwaiter()
                                           .GetResult();

            if (!readResourceResult.IsSuccess) { throw new Exception("Unable to read Broker configuration source !"); }

            var fixtureConfiguration = new MetaConfiguration() { RawJsonStrings = [readResourceResult.Value] }.Load();
            var testBrokerFactoryTypeName = fixtureConfiguration["testBrokerFactoryType"];
            var testBrokerFactoryMetaConfiguration = fixtureConfiguration.GetSection("testBrokerFactoryMetaConfiguration")
                                                                         .Get<MetaConfiguration>();
            var resolutionResult = Baubit.Store.Operations.ResolveTypeAsync(new Store.TypeResolutionContext(testBrokerFactoryTypeName)).GetAwaiter().GetResult();

            if(!resolutionResult.IsSuccess) { throw new Exception("Unable to resolve test broker factory type name !"); }

            var testBrokerFactory = (ITestBrokerFactory)Activator.CreateInstance(resolutionResult.Value, testBrokerFactoryMetaConfiguration)!;

            Broker = testBrokerFactory.LoadBroker<TBroker>();
        }

        public virtual void Dispose()
        {

        }
    }
}
