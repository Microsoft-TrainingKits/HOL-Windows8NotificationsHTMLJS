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

namespace Notifications.Backend.Infrastructure.Helpers
{
    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.StorageClient;
    using Notifications.Backend.Infrastructure;
    using Notifications.Backend.Models;

    public class CloudStorageInitializer
    {
        public static void InitializeCloudStorage(CloudStorageAccount account)
        {
            CloudTableClient cloudTableClient = account.CreateCloudTableClient();
            CloudQueueClient cloudQueueClient = account.CreateCloudQueueClient();

            CreateUserPrivilegeTable(cloudTableClient);
        }

        private static void CreateUserPrivilegeTable(CloudTableClient cloudTableClient)
        {
            cloudTableClient.CreateTableIfNotExist(PrivilegesTableServiceContext.UserPrivilegeTableName);

            // Execute conditionally for development storage only.
            if (cloudTableClient.BaseUri.IsLoopback)
            {
                TableServiceContext context = cloudTableClient.GetDataServiceContext();
                var entity = new UserPrivilege { UserId = "UserId", Privilege = "Privilege" };

                context.AddObject(PrivilegesTableServiceContext.UserPrivilegeTableName, entity);
                context.SaveChangesWithRetries();
                context.DeleteObject(entity);
                context.SaveChangesWithRetries();
            }
        }
    }
}