
using Xunit.Abstractions;

namespace Baubit.xUnit
{
    [AttributeUsage(validOn: AttributeTargets.Method, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public string Order { get; private set; }
        public OrderAttribute(string order)
        {
            Order = order;
        }
    }

    public static class OrderExtensions
    {
        public static string? GetOrder(this IMethodInfo methodInfo)
        {
            return methodInfo?.GetCustomAttributes(typeof(OrderAttribute).AssemblyQualifiedName)?
                              .FirstOrDefault()?
                              .GetConstructorArguments()?
                              .FirstOrDefault()?
                              .ToString();
        }
    }
}
