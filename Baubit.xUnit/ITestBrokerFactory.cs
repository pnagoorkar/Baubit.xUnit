namespace Baubit.xUnit
{
    public interface ITestBrokerFactory
    {
        TBroker Resolve<TBroker>() where TBroker : class, ITestBroker;
    }
}
