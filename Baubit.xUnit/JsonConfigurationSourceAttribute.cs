namespace Baubit.xUnit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class JsonConfigurationSourceAttribute : Attribute
    {
        public string Source { get; private set; }
        public JsonConfigurationSourceAttribute(string source)
        {
            Source = source;
        }
    }
}
