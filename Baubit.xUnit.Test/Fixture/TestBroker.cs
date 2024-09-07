namespace Baubit.xUnit.Test.Fixture
{
    [ConfigurationSource(TestBroker.ConfigSource)]
    public class TestBroker : ITestBroker
    {
        public const string ConfigSource = $"~ExecutingAssemblyLocation~\\Fixture\\settings.json";
    }
}
