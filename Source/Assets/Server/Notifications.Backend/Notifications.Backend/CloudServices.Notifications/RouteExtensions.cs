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

namespace Notifications.Backend.CloudServices.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Routing;

    public static class RouteExtensions
    {
        public static void MapRegistrationServiceRoute(this RouteCollection routes, string prefix)
        {
            var handlers = NotificationServiceContext.Current.Configuration.DelegatingHandlers.ToArray();

            routes.MapRegistrationServiceRoute(prefix, handlers);
        }

        public static void MapRegistrationServiceRoute(this RouteCollection routes, string prefix, params DelegatingHandler[] handlers)
        {
            var currentConfiguration = GlobalConfiguration.Configuration;

            // Handlers
            currentConfiguration.AddDelegatingHandlers(handlers);
            
            // Routes
            routes.MapHttpRoute(
                name: prefix,
                routeTemplate: prefix + "/{applicationId}/{clientId}/{tileId}",
                defaults: new { Controller = "Endpoint", applicationId = RouteParameter.Optional, tileId = RouteParameter.Optional, clientId = RouteParameter.Optional });
        }
    }
}
