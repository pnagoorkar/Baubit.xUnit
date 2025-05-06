using Baubit.Testing;

namespace Baubit.xUnit
{
    public interface IFixture<TContext> where TContext : class, IContext
    {
        public TContext Context { get; }
    }
}
