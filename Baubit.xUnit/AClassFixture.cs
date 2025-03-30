using Xunit;
using Xunit.Abstractions;

namespace Baubit.xUnit
{
    [TestCaseOrderer(TestCaseByOrderOrderer.Name, TestCaseByOrderOrderer.Assembly)]
    public abstract class AClassFixture<TFixture, TBroker> : IClassFixture<TFixture> where TFixture : class, IFixture<TBroker>
                                                                                     where TBroker : class, ITestBroker
    {
        private TFixture Fixture { get; init; }
        protected TBroker Broker { get => Fixture.Broker; }
        protected ITestOutputHelper TestOutputHelper { get; init; }
        protected IMessageSink DiagnosticMessageSink { get; init; }
        protected AClassFixture(TFixture fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null)
        {
            Fixture = fixture;
            TestOutputHelper = testOutputHelper;
            DiagnosticMessageSink = diagnosticMessageSink;
        }
    }
}
