using Baubit.Configuration;
using System.Reflection;
using Baubit.DI;
using Microsoft.Extensions.DependencyInjection;
using Baubit.Traceability.Errors;

namespace Baubit.xUnit
{
    public abstract class AFixture<TContext> : IFixture<TContext>, IDisposable where TContext : class, IContext
    {
        public TContext Context
        {
            get;
            protected set;
        }

        protected AFixture()
        {
            var configSourceAttribute = typeof(TContext).GetCustomAttribute<EmbeddedJsonSourcesAttribute>();

            if (configSourceAttribute == null)
            {
                throw new Exception($"{nameof(EmbeddedJsonSourcesAttribute)} not found on {typeof(TContext).Name}{Environment.NewLine}The generic type parameter TContext requires a {nameof(EmbeddedJsonSourcesAttribute)} to initialize test fixtures.");
            }

            var configurationSource = new ConfigurationSource();
            configurationSource.EmbeddedJsonResources = configSourceAttribute.Values;

            var services = new ServiceCollection();
            services.AddSingleton<TContext>();
            var configurationAddResult = services.AddFrom(configurationSource);
            if (!configurationAddResult.IsSuccess)
            {
                throw new AggregateException(new CompositeError<IServiceCollection>(configurationAddResult).ToString());
            }
            Context = configurationAddResult.Value.BuildServiceProvider().GetRequiredService<TContext>();
        }

        public virtual void Dispose()
        {

        }
    }
}
