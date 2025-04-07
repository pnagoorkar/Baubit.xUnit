using Baubit.xUnit;
using Xunit.Abstractions;

namespace MyLib.Test
{
    public class Test : AClassFixture<TestBroker>
    {
        public Test(Fixture<TestBroker> fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
        {
        }

        [Fact]
        public void TestMethod()
        {
            Assert.NotNull(Broker);
            Assert.NotNull(Broker.MyComponent);
            Assert.NotNull(Broker.MyComponent.SomeString);

        }
    }
}
