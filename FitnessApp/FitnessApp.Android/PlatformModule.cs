using Autofac;
using FitnessApp.Droid.Notification;
using FitnessApp.Droid.Utilities;

namespace FitnessApp.Droid
{
    public class PlatformModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // register your platform specific view model, services & etc here
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => t.Name.Equals(nameof(DeviceUtility)))
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            // register notification manager
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                   .Where(t => t.Name.Equals(nameof(NotificationManager)))
                   .AsSelf()
                   .AsImplementedInterfaces()
                   .SingleInstance();
        }
    }
}