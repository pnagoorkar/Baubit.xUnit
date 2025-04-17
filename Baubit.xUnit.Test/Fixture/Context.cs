
using Baubit.Reflection;
using Baubit.Testing;

namespace Baubit.xUnit.Test.Fixture
{
    [Source(EmbeddedJsonResources = ["Baubit.xUnit.Test;Fixture.context.json"])]
    public class Context : IContext
    {
        public void Dispose()
        {

        }
    }
}
