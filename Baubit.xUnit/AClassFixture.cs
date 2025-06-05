using Baubit.Testing;
using Xunit;
using Xunit.Abstractions;

namespace Baubit.xUnit
{
    [TestCaseOrderer(TestCaseByOrderOrderer.Name, TestCaseByOrderOrderer.Assembly)]
    public abstract class AClassFixture<TFixture, TContext> : IClassFixture<TFixture> where TFixture : class, IFixture<TContext>
                                                                                      where TContext : class, IContext
    {
        private TFixture Fixture { get; init; }
        protected TContext Context { get => Fixture.Context; }
        protected ITestOutputHelper TestOutputHelper { get; init; }
        protected IMessageSink DiagnosticMessageSink { get; init; }
        protected AClassFixture(TFixture fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null)
        {
            Fixture = fixture;
            TestOutputHelper = testOutputHelper;
            DiagnosticMessageSink = diagnosticMessageSink;
        }

    }
    public abstract class AClassFixture<TContext> : AClassFixture<Fixture<TContext>, TContext> where TContext : class, IContext
    {
        protected AClassFixture(Fixture<TContext> fixture, 
                                ITestOutputHelper testOutputHelper, 
                                IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
        {
        }
    }

}
