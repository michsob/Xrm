using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Xrm.DataAccessLayer.Helpers;
using Xrm.Domain.DataAccessBase;
using Xrm.Domain.ModelBase;

namespace Xrm.Infrastructure.DataAccess.Crm
{
    public class CrmUnitOfWork : ICrmUnitOfWork
    {
        private readonly IOrganizationService _service;
        private readonly int _batchSize;

        public List<OrganizationRequest> Requests { get; }
        private Dictionary<EntityBase, IPersistRepository> Added { get; set; }
        private Dictionary<EntityBase, IPersistRepository> Modified { get; set; }
        private Dictionary<EntityBase, IPersistRepository> Removed { get; set; }

        public CrmUnitOfWork(IOrganizationService service, int batchSize = 100)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            _service = service;
            _batchSize = batchSize;

            Requests = new List<OrganizationRequest>();
            Added = new Dictionary<EntityBase, IPersistRepository>();
            Modified = new Dictionary<EntityBase, IPersistRepository>();
            Removed = new Dictionary<EntityBase, IPersistRepository>();
        }

        public void RegisterAdded(EntityBase entity, IPersistRepository repository)
        {
            if (!Added.ContainsKey(entity))
                Added.Add(entity, repository);
        }

        public void RegisterUpdated(EntityBase entity, IPersistRepository repository)
        {
            if (!Modified.ContainsKey(entity))
                Modified.Add(entity, repository);
        }

        public void RegisterRemoved(EntityBase entity, IPersistRepository repository)
        {
            if (!Removed.ContainsKey(entity))
                Removed.Add(entity, repository);
        }

        public void Commit()
        {
            var transactionRequest = new ExecuteTransactionRequest
            {
                Requests = new OrganizationRequestCollection()
            };

            foreach (var entity in Added.Keys)
                Added[entity].PersistAdded(entity);
            foreach (var entity in Modified.Keys)
                Modified[entity].PersistUpdated(entity);
            foreach (var entity in Removed.Keys)
                Removed[entity].PersistRemoved(entity);

            Added.Clear();
            Modified.Clear();
            Removed.Clear();

            try
            {
                for (int i = 0; i < Requests.Count; i += _batchSize)
                {
                    var requestBatch = Requests.Skip(i).Take(_batchSize);

                    transactionRequest.Requests.AddRange(requestBatch);
                    _service.Execute(transactionRequest);

                    transactionRequest = new ExecuteTransactionRequest
                    {
                        Requests = new OrganizationRequestCollection()
                    };
                }
            }
            catch (FaultException<OrganizationServiceFault> fault)
            {
                if (fault.Detail.ErrorDetails.Contains("MaxBatchSize"))
                {
                    int maxBatchSize = Convert.ToInt32(fault.Detail.ErrorDetails["MaxBatchSize"]);
                    if (maxBatchSize < transactionRequest.Requests.Count)
                    {
                        var errMsg =
                            string.Format(
                                "The input request collection contains {0} requests, which exceeds the maximum allowed {1}",
                                transactionRequest.Requests.Count, maxBatchSize);
                        throw new InvalidOperationException(errMsg, fault);
                    }
                }

                throw;
            }
        }

        public IEnumerable<Guid> CommitEnumerable()
        {
            var response = new List<Guid>();
            var transactionRequest = new ExecuteTransactionRequest
            {
                Requests = new OrganizationRequestCollection()
            };

            foreach (var entity in Added.Keys)
                Added[entity].PersistAdded(entity);
            foreach (var entity in Modified.Keys)
                Modified[entity].PersistUpdated(entity);
            foreach (var entity in Removed.Keys)
                Removed[entity].PersistRemoved(entity);

            Added.Clear();
            Modified.Clear();
            Removed.Clear();

            try
            {
                for (int i = 0; i < Requests.Count; i += _batchSize)
                {
                    var requestBatch = Requests.Skip(i).Take(_batchSize);

                    transactionRequest.Requests.AddRange(requestBatch);
                    var multipleResponse = (ExecuteTransactionResponse)_service.Execute(transactionRequest);
                    response.AddRange(CrmHelper.GetGuidsAsEnumerable(multipleResponse));

                    transactionRequest = new ExecuteTransactionRequest
                    {
                        Requests = new OrganizationRequestCollection()
                    };
                }
            }
            catch (FaultException<OrganizationServiceFault> fault)
            {
                if (fault.Detail.ErrorDetails.Contains("MaxBatchSize"))
                {
                    int maxBatchSize = Convert.ToInt32(fault.Detail.ErrorDetails["MaxBatchSize"]);
                    if (maxBatchSize < transactionRequest.Requests.Count)
                    {
                        var errMsg =
                            string.Format(
                                "The input request collection contains {0} requests, which exceeds the maximum allowed {1}",
                                transactionRequest.Requests.Count, maxBatchSize);
                        throw new InvalidOperationException(errMsg, fault);
                    }
                }

                throw;
            }

            return response;
        }
    }
}
