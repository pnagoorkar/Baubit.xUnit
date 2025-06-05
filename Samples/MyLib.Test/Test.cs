using Baubit.Testing;
using Baubit.xUnit;
using FluentResults;
using Xunit.Abstractions;

namespace MyLib.Test
{
    public class Test : AClassFixture<Context>
    {
        public Test(Fixture<Context> fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
        {
        }

        [Fact]
        public void TestMethod()
        {
            Assert.NotNull(Context);
            Assert.NotNull(Context.MyComponent);
            Assert.NotNull(Context.MyComponent.SomeString);

        }
        [Theory]
        [InlineData("MyLib.Test;Scenarios.emptyScenario.json")]
        public void AnotherTest(string embeddedJsonResource)
        {
            var result = ScenarioBuilder<MyScenario>.BuildFromEmbeddedJsonResources(embeddedJsonResource)
                                                    .Bind(scenario => scenario.Run(Context));
            Assert.True(result.IsSuccess);
        }
    }

    public class MyScenario : IScenario<Context>
    {
        public Result Run(Context context) => Result.Ok();

        public Result Run() => Result.Ok();

        public Task<Result> RunAsync(Context context) => Task.FromResult(Result.Ok());

        public Task<Result> RunAsync() => Task.FromResult(Result.Ok());
        public void Dispose()
        {

        }
    }
}
