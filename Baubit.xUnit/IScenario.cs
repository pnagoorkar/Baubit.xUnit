using FluentResults;

namespace Baubit.xUnit
{
    public interface IScenario<TContext> where TContext : IContext
    {
        public Result Run(TContext context);
    }
}
