using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace FitnessApp.Utilities
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public static IContainer Container
        {
            get;
            private set;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="registrations">The <see cref="ContainerBuilder"/> object instance.</param>
        public static void RegisterServices(ContainerBuilder registrations = null)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AppModule>();

            Container = builder.Build();
            registrations?.Update(Container);

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }
    }
}
