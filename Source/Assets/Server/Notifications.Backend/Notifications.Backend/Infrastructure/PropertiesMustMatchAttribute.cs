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
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "'{0}' and '{1}' do not match.";

        private readonly object typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(DefaultErrorMessage)
        {
            this.OriginalProperty = originalProperty;
            this.ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }

        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return this.typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, this.ErrorMessageString, this.OriginalProperty, this.ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            var properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(this.OriginalProperty, true).GetValue(value);
            object confirmValue = properties.Find(this.ConfirmProperty, true).GetValue(value);

            return object.Equals(originalValue, confirmValue);
        }
    }
}
