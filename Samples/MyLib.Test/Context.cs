
using Baubit.Configuration;
using Baubit.xUnit;

namespace MyLib.Test
{
    [EmbeddedJsonSources("MyLib.Test;context.json")]
    public class Context : IContext
    {
        public MyComponent MyComponent { get; set; }
        public Context(MyComponent myComponent)
        {
            MyComponent = myComponent;
        }
    }
}
