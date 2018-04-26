using Autofac;
using FitnessApp.ViewModels;
using System;

namespace FitnessApp
{
    public class AppModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register all view
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.Name.EndsWith("Page", StringComparison.OrdinalIgnoreCase))
                .AsSelf()
                .InstancePerDependency();

            // register non signle instance model
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.Name.EndsWith("ViewModel", StringComparison.OrdinalIgnoreCase) &&
                    !t.Name.Equals(nameof(BaseViewModel), StringComparison.OrdinalIgnoreCase))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency();
        }
    }
}
