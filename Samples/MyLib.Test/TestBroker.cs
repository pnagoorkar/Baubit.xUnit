
using Baubit.Configuration;
using Baubit.xUnit;

namespace MyLib.Test
{
    [EmbeddedJsonSources("MyLib.Test;testBroker.json")]
    public class TestBroker : ITestBroker
    {
        public MyComponent MyComponent { get; set; }
        public TestBroker(MyComponent myComponent)
        {
            MyComponent = myComponent;
        }
    }
}
