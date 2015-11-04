using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using Xrm.Domain.Contact;
using Xrm.Infrastructure.DataAccess.Crm;

namespace Xrm.Test.Infrastructure
{
    [TestClass]
    public class ContactRepositoryTests
    {
        [TestMethod]
        public void GetByEmailTest()
        {
            Contact actual;
            string email = "test@company.co.uk";

            var entity1 = new CrmEntities.Contact();
            entity1.Id = Guid.NewGuid();
            entity1.FirstName = "Kate";
            entity1.LastName = "O'conor";
            entity1.EMailAddress1 = "test2@company.co.uk";

            var entity2 = new CrmEntities.Contact();
            entity2.Id = Guid.NewGuid();
            entity2.FirstName = "John";
            entity2.LastName = "Smith";
            entity2.EMailAddress1 = "test@company.co.uk";

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();

            organisationService.ExecuteOrganizationRequest = r =>
            {
                List<Entity> entities = new List<Entity>
                {
                    entity2
                };

                var response = new RetrieveMultipleResponse
                {
                    Results = new ParameterCollection
                    {
                        { "EntityCollection", new EntityCollection(entities) }
                    }
                };

                return response;
            };

            var crmUnitOfWork = new Xrm.Infrastructure.DataAccess.Crm.Fakes.StubCrmUnitOfWork(organisationService, 10);
            var target = new ContactRepository(organisationService, crmUnitOfWork);

            //
            // Act
            //
            actual = target.GetByEmail(email);

            //
            // Assert
            //
            Assert.IsNotNull(actual);
            Assert.AreEqual(entity2.EMailAddress1, actual.EmailAddress1);
        }

        [TestMethod]
        public void PersistAddedTest()
        {
            var contact = new Contact();
            contact.Id = Guid.NewGuid();
            contact.Firstname = "Kate";
            contact.Lastname = "Smith";

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            var crmUnitOfWork = new Xrm.Infrastructure.DataAccess.Crm.Fakes.StubCrmUnitOfWork(organisationService, 10);
            var target = new ContactRepository(organisationService, crmUnitOfWork);

            //
            // Act
            //
            target.PersistAdded(contact);

            //
            // Assert
            //
            Assert.AreEqual(1, crmUnitOfWork.Requests.Count);
        }

        [TestMethod]
        public void PersistUpdatedTest()
        {
            var contact = new Contact();
            contact.Id = Guid.NewGuid();
            contact.Firstname = "Kate";
            contact.Lastname = "O'conor";

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            var crmUnitOfWork = new Xrm.Infrastructure.DataAccess.Crm.Fakes.StubCrmUnitOfWork(organisationService, 10);
            var target = new ContactRepository(organisationService, crmUnitOfWork);

            //
            // Act
            //
            target.PersistUpdated(contact);

            //
            // Assert
            //
            Assert.AreEqual(1, crmUnitOfWork.Requests.Count);
        }

        [TestMethod]
        public void PersistRemovedTest()
        {
            var contact = new Contact();
            contact.Id = Guid.NewGuid();
            contact.Firstname = "Kate";
            contact.Lastname = "O'conor";

            var organisationService = new Microsoft.Xrm.Sdk.Fakes.StubIOrganizationService();
            var crmUnitOfWork = new Xrm.Infrastructure.DataAccess.Crm.Fakes.StubCrmUnitOfWork(organisationService, 10);
            var target = new ContactRepository(organisationService, crmUnitOfWork);

            //
            // Act
            //
            target.PersistRemoved(contact);

            //
            // Assert
            //
            Assert.AreEqual(1, crmUnitOfWork.Requests.Count);
        }
    }
}
