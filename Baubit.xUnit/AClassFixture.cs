using Baubit.Configuration;
using Baubit.DI;
using Baubit.Testing;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;
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
            return ConfigurationSourceBuilder.CreateNew()
                                             .Bind(configSourceBuilder => configSourceBuilder.WithEmbeddedJsonResources(embeddedJsonResource))
                                             .Bind(configSourceBuilder => configSourceBuilder.Build())
                                             .Bind(ExecuteScenario<TScenario>);
        }
        protected Result ExecuteScenario<TScenario>(ConfigurationSource configurationSource) where TScenario : class, IScenario<TContext>
        {
            return ComponentBuilder<TScenario>.Create(configurationSource)
                                              .Bind(compBuilder => compBuilder.WithRegistrationHandler(services => services.AddSingleton<TScenario>()))
                                              .Bind(compBuilder => compBuilder.Build())
                                              .Bind(scenario => scenario.Run(Context));
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
