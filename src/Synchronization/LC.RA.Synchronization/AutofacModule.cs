using Autofac;
using Microsoft.Extensions.Configuration;

namespace LC.RA.Synchronization
{
    public sealed class AutofacModule : Module
    {
        private readonly IConfiguration configuration;

        public AutofacModule(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
        }
    }
}