using Baubit.DI;
using Baubit.Testing;
using Baubit.Traceability;
using Microsoft.Extensions.DependencyInjection;

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
            Context = ComponentBuilder<TContext>.CreateFromSourceAttribute()
                                                .Bind(compBuilder => compBuilder.WithRegistrationHandler(services => services.AddSingleton<TContext>()))
                                                .Bind(compBuilder => compBuilder.Build())
                                                .ThrowIfFailed()
                                                .Value;
        }

        public virtual void Dispose()
        {

        }
    }
}
