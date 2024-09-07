namespace Baubit.xUnit
{
    public sealed class Fixture<TBroker> : AFixture<TBroker> where TBroker : class, ITestBroker
    {
    }
}
