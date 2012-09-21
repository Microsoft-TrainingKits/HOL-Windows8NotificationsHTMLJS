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

namespace Notifications.Backend.ViewModel
{
    using System.Collections.Generic;

    public class NotificationTemplateViewModel
    {
        public IEnumerable<BlobFileInfo> TileImages { get; set; }

        public IEnumerable<string> Priorities { get; set; }

        public IEnumerable<string> BadgeGlyphValueContent { get; set; }

        public IEnumerable<string> ToastAudioContent { get; set; }

        public string ChannelUrl { get; set; }

        public string ApplicationId { get; set; }

        public string RowKey { get; set; }

        public string NotificationType { get; set; }

        public string NotificationTemplateType { get; set; }
    }
}