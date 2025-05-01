
using Baubit.Configuration;
using Baubit.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyLib
{
    public class MyModule : AModule<MyConfiguration>
    {
        public MyModule(ConfigurationSource configurationSource) : base(configurationSource)
        {
        }

        public MyModule(IConfiguration configuration) : base(configuration)
        {
        }

        public MyModule(MyConfiguration configuration, List<AModule> nestedModules, List<IConstraint> constraints) : base(configuration, nestedModules, constraints)
        {
        }

        public override void Load(IServiceCollection services)
        {
            services.AddSingleton<MyComponent>(new MyComponent(Configuration.MyStringProperty));
            base.Load(services);
        }
    }
}
