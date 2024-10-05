using Baubit.Configuration;

namespace Baubit.xUnit.Test.Fixture
{
    [EmbeddedJsonSources("Baubit.xUnit.Test;Fixture.testBroker.json")]
    public class TestBroker : ITestBroker
    {
    }
}
