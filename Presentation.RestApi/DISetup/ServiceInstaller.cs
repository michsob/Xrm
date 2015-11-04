using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Xrm.Sdk;
using System.Configuration;
using Xrm.Domain.Contact;
using Xrm.Domain.DataAccessBase;
using Xrm.Infrastructure.Base;
using Xrm.Infrastructure.DataAccess.Crm;
using Xrm.Infrastructure.DataAccess.Helpers;

namespace Xrm.Presentation.RestApi.DISetup
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IInterceptor>().ImplementedBy<ErrorHandler>());

            container.Register(
                Component.For<IOrganisationFactory>().ImplementedBy<OrganisationFactory>());
            container.Register(Component.For<IOrganizationService>().UsingFactory(
                (IOrganisationFactory f) => f.GetOrganisationService(
                    ConfigurationManager.AppSettings["OrganisationServiceUrl"],
                    ConfigurationManager.AppSettings["Username"],
                    ConfigurationManager.AppSettings["Password"]))
                    .LifeStyle.Singleton);

            container.Register(
                Component.For<IUnitOfWork>().ImplementedBy<CrmUnitOfWork>());

            container.Register(
                Component.For<IContactRepository>().ImplementedBy<ContactRepository>());

            container.Register(
                Component.For<IContactService>().ImplementedBy<ContactService>().Interceptors<ErrorHandler>());
        }
    }
}