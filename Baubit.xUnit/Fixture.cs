namespace Baubit.xUnit
{
    public sealed class Fixture<TContext> : AFixture<TContext> where TContext : class, IContext
    {
    }
}
