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

namespace Notifications.Backend.Models
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using Microsoft.WindowsAzure.StorageClient;

    public class UserPrivilege : TableServiceEntity
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors",
            Justification =
                "The PartitionKey and RowKey properties are set to uniquely identify the UserPrivilege entity.")]
        public UserPrivilege()
        {
            PartitionKey = "a";
            RowKey = string.Format(
                                CultureInfo.InvariantCulture,
                                "{0:10}_{1}",
                                DateTime.MaxValue.Ticks - DateTime.Now.Ticks,
                                Guid.NewGuid());
        }

        public string UserId { get; set; }

        public string Privilege { get; set; }
    }
}