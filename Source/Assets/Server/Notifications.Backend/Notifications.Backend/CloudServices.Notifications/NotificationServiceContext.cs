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

    public class NotificationServiceContext
    {
        private static readonly NotificationServiceContext Instance = new NotificationServiceContext();

        private readonly NotificationServiceConfig config = new NotificationServiceConfig();

        public static NotificationServiceContext Current
        {
            get { return Instance; }
        }

        public NotificationServiceConfig Configuration
        {
            get { return this.config; }
        }

        public void Configure(Action<NotificationServiceConfig> configure)
        {
            if (configure == null)
                throw new ArgumentException(Constants.ErrorParameterConfigureParamCannotBeNull, "configure");

            configure(this.config);
        }
    }
}
