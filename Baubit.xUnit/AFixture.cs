using Baubit.Testing;
using Baubit.Traceability;

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
            Context = Baubit.Reflection.ObjectLoader.Load<TContext>().ThrowIfFailed().Value;
        }

        public virtual void Dispose()
        {

        }
    }
}
