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

    public class Endpoint
    {
        public virtual string ApplicationId { get; set; }

        public virtual string TileId { get; set; }

        public virtual string ClientId { get; set; }

        public string ChannelUri { get; set; }

        public string UserId { get; set; }

        public DateTime ExpirationTime { get; set; }

        public string DeviceType { get; set; }

        public static T To<T>(Endpoint endpoint) where T : Endpoint
        {
            if (endpoint.GetType() == typeof(T))
                return endpoint as T;

            var destination = Activator.CreateInstance<T>();

            destination.ApplicationId = endpoint.ApplicationId;
            destination.TileId = endpoint.TileId;
            destination.ClientId = endpoint.ClientId;
            destination.ChannelUri = endpoint.ChannelUri;
            destination.UserId = endpoint.UserId;
            destination.ExpirationTime = endpoint.ExpirationTime;
            destination.DeviceType = endpoint.DeviceType;

            return destination;
        }
    }
}
