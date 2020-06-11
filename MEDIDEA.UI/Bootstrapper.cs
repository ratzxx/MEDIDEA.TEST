using Autofac;
using MEDIDEA.Domain;
using MEDIDEA.Infrastructure;
using MEDIDEA.Infrastructure.Repositories;
using MEDIDEA.UI.ViewModels;

namespace MEDIDEA.UI
{
    public class Bootstrapper
    {
        public static IContainer Container { get; set; }

        public static void BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UoW>().As<IUoW>();
            builder.RegisterType<MedideaContext>();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<PhoneRepository>().As<IPhoneRepository>();
            builder.RegisterType<VisitRepository>().As<IVisitRepository>();

            builder.RegisterType<CustomerListViewModel>();
            builder.RegisterType<CustomerViewModel>().As<ICustomerViewModel>();
            builder.RegisterType<VisitViewModel>().As<IVisitViewModel>();
            builder.RegisterType<CustomerSelectViewModel>();
            builder.RegisterType<PhoneViewModel>();
            

            Container = builder.Build();
        }
    }
}
