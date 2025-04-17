using Baubit.Configuration;
using Baubit.Testing;
using FluentResults;
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

        protected Result ExecuteScenario<TScenario>(string embeddedJsonResource) where TScenario : class, IScenario<TContext>
        {
            return ExecuteScenario<TScenario>(new ConfigurationSource<TScenario> { EmbeddedJsonResources = [embeddedJsonResource] });
        }
        protected Result ExecuteScenario<TScenario>(ConfigurationSource<TScenario> configurationSource) where TScenario : class, IScenario<TContext>
        {
            return configurationSource.Load<TScenario>().Bind(scenario => scenario.Run(Context));
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
