using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;

namespace Xrm.Infrastructure.DataAccess.Helpers
{
    public class OrganisationFactory : IOrganisationFactory
    {
        public IOrganizationService GetOrganisationService(string orgUrl, string username, string password)
        {
            CrmConnection connection = new CrmConnection(orgUrl, username, password);
            return connection.ServiceProxy;
        }

        public IOrganizationService GetOrganisationService(string orgUrl)
        {
            CrmConnection connection = new CrmConnection(orgUrl);
            return connection.ServiceProxy;
        }

        public OrganizationServiceContext GetOrganisationContext(string orgUrl, string username, string password)
        {
            CrmConnection connection = new CrmConnection(orgUrl, username, password);
            return connection.GetContext();
        }

        public OrganizationServiceContext GetOrganisationContext(string orgUrl)
        {
            CrmConnection connection = new CrmConnection(orgUrl);
            return connection.GetContext();
        }
    }
}
