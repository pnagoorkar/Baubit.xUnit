using Baubit.Reflection;
using Baubit.Testing;

namespace MyLib.Test
{
    [Source(EmbeddedJsonResources = ["MyLib.Test;context.json"])]
    public class Context : IContext
    {
        public MyComponent MyComponent { get; set; }
        public Context(MyComponent myComponent)
        {
            MyComponent = myComponent;
        }
        public void Dispose()
        {

        }
    }
}
