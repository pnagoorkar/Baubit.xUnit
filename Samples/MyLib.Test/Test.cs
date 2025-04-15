﻿using Baubit.xUnit;
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
            Assert.True(ExecuteScenario<MyScenario>(embeddedJsonResource).IsSuccess);
        }
    }

    public class MyScenario : IScenario<Context>
    {
        public Result Run(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
