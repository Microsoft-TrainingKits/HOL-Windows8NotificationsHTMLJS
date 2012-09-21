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

namespace Notifications.Backend.CloudServices.Notifications.WindowsAzure
{
    using System;
    using System.Data.Services.Common;
    using System.Globalization;
    using System.Text;
    using Microsoft.WindowsAzure.Samples.Common.Storage;

    [DataServiceEntity]
    [DataServiceKey(new[] { "PartitionKey", "RowKey" })]
    public class EndpointTableRow : Endpoint, ITableServiceEntity
    {
        private string rowKey;

        public EndpointTableRow()
        {
            this.rowKey = string.Empty;
        }

        public string PartitionKey
        {
            get { return this.ApplicationId; }
            set { this.ApplicationId = value; }
        }

        public string RowKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.rowKey))
                    this.RowKey = CreateRowKey(this.TileId, this.ClientId);

                return this.rowKey;
            }

            set
            {
                this.rowKey = value;
            }
        }

        public virtual DateTime Timestamp { get; set; }

        public static string CreateRowKey(string tileId, string clientId)
        {
            var encodedTileId = Convert.ToBase64String(Encoding.UTF8.GetBytes(tileId ?? string.Empty)).Replace("/", "%");
            var encodedClientId = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId ?? string.Empty)).Replace("/", "%");

            return string.Format(CultureInfo.InvariantCulture, "{0}_{1}", encodedTileId, encodedClientId);
        }
    }
}
