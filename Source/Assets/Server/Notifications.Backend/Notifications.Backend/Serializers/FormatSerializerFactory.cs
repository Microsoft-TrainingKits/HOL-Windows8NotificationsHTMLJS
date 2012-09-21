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

namespace Notifications.Backend.Serializers
{
    using System.Collections.Specialized;
    using System.Web;
    using Notifications.Backend.Infrastructure;

    public class FormatSerializerFactory : IFormatSerializerFactory
    {
        private IFormatSerializer jsonSerializer;
        private IFormatSerializer jsonpSerializer;
        private IFormatSerializer xmlSerializer;

        private IFormatSerializer JsonSerializer
        {
            get { return this.jsonSerializer ?? (this.jsonSerializer = new JsonSerializer()); }
        }

        private IFormatSerializer JsonpSerializer
        {
            get { return this.jsonpSerializer ?? (this.jsonpSerializer = new JsonSerializer()); }
        }

        private IFormatSerializer XmlSerializer
        {
            get { return this.xmlSerializer ?? (this.xmlSerializer = new XmlSerializer()); }
        }

        public IFormatSerializer GetSerializer(NameValueCollection headers, NameValueCollection queryString)
        {
            // Check content type too.
            var mimeType = headers["Accept"] ?? HttpConstants.MimeApplicationAtomXml;
            string callbackName = string.Empty;

            if (HttpContext.Current != null)
            {
                callbackName = queryString["jsonCallback"] ?? queryString["callback"];
            }

            if (mimeType.Contains(HttpConstants.MimeApplicationJson))
            {
                return string.IsNullOrWhiteSpace(callbackName) ? this.JsonSerializer : this.JsonpSerializer;
            }

            if (mimeType.Contains("text/xml") || mimeType.Contains("application/xml"))
            {
                return this.XmlSerializer;
            }

            if (!string.IsNullOrWhiteSpace(callbackName))
            {
                return this.JsonpSerializer;
            }

            return this.XmlSerializer;
        }
    }
}