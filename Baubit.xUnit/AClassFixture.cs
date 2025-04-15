using Baubit.Configuration;
using FluentResults;
using Microsoft.Extensions.Configuration;
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
        protected Result ExecuteScenario<TScenario>(string embeddedJsonResource) where TScenario : IScenario<TContext>
        {
            return ExecuteScenario<TScenario>(new ConfigurationSource { EmbeddedJsonResources = [embeddedJsonResource] });
        }
        protected Result ExecuteScenario<TScenario>(ConfigurationSource configurationSource) where TScenario : IScenario<TContext>
        {
            return configurationSource.Build().Bind(config => config.GetChildren().Any() ? Result.Try(() => config.Get<TScenario>()).Bind(scenario => scenario.Run(Context)) : Result.Ok());
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
