using Autofac;
using Autofac.Integration.Mvc;
using MrE.Models;
using MrE.Models.Entities;
using MrE.Repository;
using MrE.Repository.Abstractions;
using MrE.Services;
using MrE.Services.Abstractions;
using MrE.Services.Validations;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MrEOnline
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            builder.RegisterType<DataStoreContext>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));

            builder.RegisterType<ValidationException>().InstancePerRequest();
            builder.RegisterType<ValidationExceptionService>().As<IValidationExceptionService>();
            builder.RegisterType<StatusValidationService>().As<IValidationService<Status>>().InstancePerRequest();
            builder.RegisterType<StatusService>().As<IService<Status>>().InstancePerRequest();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}
