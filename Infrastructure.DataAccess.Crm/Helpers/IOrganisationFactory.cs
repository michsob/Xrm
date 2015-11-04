using Microsoft.Xrm.Sdk;

namespace Xrm.Infrastructure.DataAccess.Helpers
{
    public interface IOrganisationFactory
    {
        IOrganizationService GetOrganisationService(string orgUrl, string username, string password);
    }
}