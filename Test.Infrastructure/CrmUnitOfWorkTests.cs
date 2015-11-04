using CrmEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using Xrm.Infrastructure.DataAccess.Crm;

namespace Test.Domain
{
    [TestClass]
    public class CrmUnitOfWorkTests
    {
        [TestMethod]
        public void CommitAddedObject()
        {
            int callCount = 0;
            int exptectedCallCount = 1;

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            organisationService.ExecuteOrganizationRequest = (request) =>
            {
                callCount++;
                var results = new OrganizationResponse();

                return results;
            };

            var crmUnitOfWork = new CrmUnitOfWork(organisationService);
            var contactRepository = new 
                Xrm.Infrastructure.DataAccess.Crm.Fakes.StubContactRepository(organisationService, crmUnitOfWork);
            
            contactRepository.PersistAddedEntityBase = (contact) =>
            {
                var sdkContact = new Contact();
                sdkContact.ContactId = contact.Id;
                sdkContact.FirstName = "test";
                var createRequest = new CreateRequest()
                {
                    Target = sdkContact
                };
                crmUnitOfWork.Requests.Add(createRequest);
            };

            contactRepository.Add(new Xrm.Domain.Contact.Contact());
            crmUnitOfWork.Commit();

            Assert.AreEqual(exptectedCallCount, callCount);
        }

        [TestMethod]
        public void CommitModifiedObject()
        {
            int callCount = 0;
            int exptectedCallCount = 1;

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            organisationService.ExecuteOrganizationRequest = (request) =>
            {
                callCount++;
                var results = new OrganizationResponse();

                return results;
            };

            var crmUnitOfWork = new CrmUnitOfWork(organisationService);
            var contactRepository = new 
                Xrm.Infrastructure.DataAccess.Crm.Fakes.StubContactRepository(organisationService, crmUnitOfWork);

            contactRepository.PersistUpdatedEntityBase = (contact) =>
            {
                var sdkContact = new Contact();
                sdkContact.ContactId = contact.Id;
                sdkContact.FirstName = "test";
                var updateRequest = new UpdateRequest()
                {
                    Target = sdkContact
                };
                crmUnitOfWork.Requests.Add(updateRequest);
            };

            contactRepository.Update(new Xrm.Domain.Contact.Contact());
            crmUnitOfWork.Commit();

            Assert.AreEqual(exptectedCallCount, callCount);
        }

        [TestMethod]
        public void CommitRemovedObject()
        {
            int callCount = 0;
            int exptectedCallCount = 1;

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            organisationService.ExecuteOrganizationRequest = (request) =>
            {
                callCount++;
                var results = new OrganizationResponse();

                return results;
            };

            var crmUnitOfWork = new CrmUnitOfWork(organisationService);
            var contactRepository = new 
                Xrm.Infrastructure.DataAccess.Crm.Fakes.StubContactRepository(organisationService, crmUnitOfWork);

            contactRepository.PersistRemovedEntityBase = (contact) =>
            {
                var deleteRequest = new DeleteRequest()
                {
                    Target = new EntityReference(Contact.EntityLogicalName, Guid.NewGuid())
                };
                crmUnitOfWork.Requests.Add(deleteRequest);
            };

            contactRepository.Remove(new Xrm.Domain.Contact.Contact());
            crmUnitOfWork.Commit();

            Assert.AreEqual(exptectedCallCount, callCount);
        }
    }
}
