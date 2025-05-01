using Xunit.Abstractions;

namespace Baubit.xUnit.Test.Fixture
{
    public class Test : AClassFixture<Context>
    {
        public Test(Fixture<Context> fixture, ITestOutputHelper testOutputHelper, IMessageSink diagnosticMessageSink = null) : base(fixture, testOutputHelper, diagnosticMessageSink)
        {
        }

        [Fact]
        public void ContextIsNotNull()
        {
            Assert.NotNull(Context);
        }
    }
}

