namespace Baubit.xUnit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ConfigurationSourceAttribute : Attribute
    {
        public string Source { get; private set; }
        public ConfigurationSourceAttribute(string source)
        {
            Source = source;
        }
    }
}
