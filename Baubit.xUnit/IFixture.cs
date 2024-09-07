namespace Baubit.xUnit
{
    public interface IFixture<TBroker> where TBroker : class, ITestBroker
    {
        public TBroker Broker { get; }
    }
}
