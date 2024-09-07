namespace Baubit.xUnit
{
    public interface ITestBrokerFactory
    {
        public TBroker LoadBroker<TBroker>() where TBroker : class, ITestBroker;
    }
}
