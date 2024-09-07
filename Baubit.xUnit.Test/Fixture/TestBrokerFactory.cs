using Baubit.Configuration;

namespace Baubit.xUnit.Test.Fixture
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
