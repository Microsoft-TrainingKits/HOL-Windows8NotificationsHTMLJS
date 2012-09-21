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
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;

    public class EndpointController : ApiController
    {
        protected readonly IEndpointRepository Repository;
        private readonly Func<HttpRequestMessage, string> mapUsernameDelegate;
        private static TimeSpan defaultEndpointExpirationTime = TimeSpan.FromDays(30);

        public EndpointController()
            : this(NotificationServiceContext.Current.Configuration.StorageProvider)
        {
        }

        public EndpointController(IEndpointRepository repository)
            : this(repository, NotificationServiceContext.Current.Configuration.MapUsername)
        {
        }

        public EndpointController(IEndpointRepository repository, Func<HttpRequestMessage, string> mapUsernameDelegate)
        {
            if (repository == null)
                throw new ArgumentNullException("repository");

            this.Repository = repository;
            this.mapUsernameDelegate = mapUsernameDelegate;
        }

        [AuthenticateEndpoint, AuthorizeManagementEndpoint]
        public IEnumerable<Endpoint> Get()
        {
            try
            {
                return this.Repository.All();
            }
            catch (Exception exception)
            {
                throw WebException(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [AuthenticateEndpoint, AuthorizeManagementEndpoint]
        public Endpoint Get(string applicationId, string tileId, string clientId)
        {
            var endpoint = default(Endpoint);
            
            try
            {
                endpoint = this.Repository.Find(applicationId, tileId, clientId);
            }
            catch (Exception exception)
            {
                throw WebException(exception.Message, HttpStatusCode.InternalServerError);
            }

            if (endpoint == null)
                throw WebException("Not Found.", HttpStatusCode.NotFound);

            return Endpoint.To<Endpoint>(endpoint);
        }

        [AuthenticateEndpoint, AuthorizeRegistrationEndpoint]
        public HttpResponseMessage Put(Endpoint endpoint)
        {
            try
            {
                if (endpoint == null)
                    throw WebException(Constants.ErrorParameterEndpointCannotBeNull, HttpStatusCode.BadRequest);

                // Set the username under which the app will store the channel
                endpoint.UserId = this.mapUsernameDelegate(this.Request);

                if (endpoint.ExpirationTime == null || endpoint.ExpirationTime == DateTime.MinValue)
                    endpoint.ExpirationTime = DateTime.UtcNow.Add(defaultEndpointExpirationTime);

                this.Repository.InsertOrUpdate(endpoint);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (HttpResponseException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw WebException(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        [AuthenticateEndpoint, AuthorizeRegistrationEndpoint]
        public HttpResponseMessage Delete(string applicationId, string tileId, string clientId)
        {
            if (string.IsNullOrWhiteSpace(applicationId))
                throw WebException(Constants.ErrorParameterApplicationIdCannotBeNull, HttpStatusCode.BadRequest);

            if (string.IsNullOrWhiteSpace(clientId))
                throw WebException(Constants.ErrorParameterClientIdCannotBeNull, HttpStatusCode.BadRequest);

            try
            {
                this.Repository.Delete(applicationId, tileId, clientId);
                return new HttpResponseMessage(HttpStatusCode.Accepted);
            }
            catch (Exception exception)
            {
                throw WebException(exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        public virtual HttpContext RequestContext()
        {
            return HttpContext.Current;
        }

        private static HttpResponseException WebException(string message, HttpStatusCode code)
        {
            return new HttpResponseException(new HttpResponseMessage(code) { Content = new StringContent(message) });
        }
    }
}
