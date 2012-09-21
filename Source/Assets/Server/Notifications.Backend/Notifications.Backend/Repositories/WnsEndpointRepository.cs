// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

namespace Notifications.Backend.Repositories
{
    using System.Collections.Generic;
    using Notifications.Backend.CloudServices.Notifications;

    public class WnsEndpointRepository : IWnsEndpointRepository
    {
        private readonly IEndpointRepository endpointRepository;

        public WnsEndpointRepository()
        {
            this.endpointRepository = NotificationServiceContext.Current.Configuration.StorageProvider;
        }

        public IEnumerable<Endpoint> GetAllEndpoints()
        {
            return this.endpointRepository.All();
        }

        public Endpoint GetEndpoint(string applicationId, string tileId, string clientId)
        {
            return this.endpointRepository.Find(e => e.ApplicationId == applicationId && e.TileId == tileId && e.ClientId == clientId);
        }
    }
}