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

namespace Notifications.Backend.Infrastructure
{
    using System.Collections.Generic;
    using Notifications.Backend.ViewModel;

    public static class NotifcationSoundEvents
    {
        public static List<BlobFileInfo> GetSoundEvents()
        {
            var result = new List<BlobFileInfo>();
            result.Add(new BlobFileInfo() { FileName = "Default Notification", FileUri = "ms-winsoundevent:Notification.Default" });
            result.Add(new BlobFileInfo() { FileName = "Mail Notification", FileUri = "ms-winsoundevent:Notification.Mail" });
            result.Add(new BlobFileInfo() { FileName = "SMS Notification", FileUri = "ms-winsoundevent:Notification.SMS" });
            result.Add(new BlobFileInfo() { FileName = "IM Notification", FileUri = "ms-winsoundevent:Notification.IM" });
            result.Add(new BlobFileInfo() { FileName = "Reminder Notification", FileUri = "ms-winsoundevent:Notification.Reminder" });
            return result;
        }
    }
}