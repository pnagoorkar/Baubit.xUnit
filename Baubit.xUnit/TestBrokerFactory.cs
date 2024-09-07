using Baubit.Configuration;

namespace Baubit.xUnit
{
    public class TestBrokerFactory : ITestBrokerFactory
    {
        public TestBrokerFactory(MetaConfiguration metaConfiguration)
        {

        }
        public TBroker LoadBroker<TBroker>() where TBroker : class, ITestBroker
        {
            return Activator.CreateInstance<TBroker>();
        }
    }
}
