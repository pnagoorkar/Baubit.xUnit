using Xunit.Abstractions;

namespace Baubit.xUnit.Test.Fixture
{
    public class Test : AClassFixture<Fixture<TestBroker>, TestBroker>
    {
        public Test(Fixture<TestBroker> fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
        {
        }

        [Fact]
        public void BrokerIsNotNull()
        {
            Assert.NotNull(Broker);
        }
    }
}

