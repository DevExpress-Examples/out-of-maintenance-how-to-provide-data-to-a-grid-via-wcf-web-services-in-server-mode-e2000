//-----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.Xml;


namespace Microsoft.ServiceModel.Samples
{

    // This is constants for GZip message encoding policy.
    static class GZipMessageEncodingPolicyConstants
    {
        public const string GZipEncodingName = "GZipEncoding";
        public const string GZipEncodingNamespace = "http://schemas.microsoft.com/ws/06/2004/mspolicy/netgzip1";
        public const string GZipEncodingPrefix = "gzip";
    }

    //This is the binding element that, when plugged into a custom binding, will enable the GZip encoder
    public sealed class GZipMessageEncodingBindingElement 
                        : MessageEncodingBindingElement //BindingElement
                        , IPolicyExportExtension
    {
        //public XmlDictionaryReaderQuotas ReaderQuotas {
        //    get {
        //        BinaryMessageEncodingBindingElement el1 = this.innerBindingElement as BinaryMessageEncodingBindingElement;
        //        if(el1 != null) return el1.ReaderQuotas;
        //        TextMessageEncodingBindingElement el2 = this.innerBindingElement as TextMessageEncodingBindingElement;
        //        if(el2 != null) return el2.ReaderQuotas;
        //        return null;
        //    }
        //} 

        //We will use an inner binding element to store information required for the inner encoder
        MessageEncodingBindingElement innerBindingElement;

        //By default, use the default text encoder as the inner encoder
        public GZipMessageEncodingBindingElement()
            : this(new TextMessageEncodingBindingElement()) { }

        public GZipMessageEncodingBindingElement(MessageEncodingBindingElement messageEncoderBindingElement)
        {
            this.innerBindingElement = messageEncoderBindingElement;
        }

        public MessageEncodingBindingElement InnerMessageEncodingBindingElement
        {
            get { return innerBindingElement; }
            set { innerBindingElement = value; }
        }

        //Main entry point into the encoder binding element. Called by WCF to get the factory that will create the
        //message encoder
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new GZipMessageEncoderFactory(innerBindingElement.CreateMessageEncoderFactory());
        }
       
        public override MessageVersion MessageVersion
        {
            get { return innerBindingElement.MessageVersion; }
            set { innerBindingElement.MessageVersion = value; }
        }

        public override BindingElement Clone()
        {
            return new GZipMessageEncodingBindingElement(this.innerBindingElement);
        }

        public override T GetProperty<T>(BindingContext context)
        {
            if (typeof(T) == typeof(XmlDictionaryReaderQuotas))
            {
                return innerBindingElement.GetProperty<T>(context);
            }
            else 
            {
                return base.GetProperty<T>(context);
            }
        }

        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }

        void IPolicyExportExtension.ExportPolicy(MetadataExporter exporter, PolicyConversionContext policyContext)
        {
            if (policyContext == null)
            {
                throw new ArgumentNullException("policyContext");
            }
            XmlDocument document = new XmlDocument();
            policyContext.GetBindingAssertions().Add(document.CreateElement(
                GZipMessageEncodingPolicyConstants.GZipEncodingPrefix,
                GZipMessageEncodingPolicyConstants.GZipEncodingName,
                GZipMessageEncodingPolicyConstants.GZipEncodingNamespace));
        }
    }

    //This class is necessary to be able to plug in the GZip encoder binding element through
    //a configuration file
    public class GZipMessageEncodingElement : BindingElementExtensionElement
    {
        public GZipMessageEncodingElement()
        {
        }

        //Called by the WCF to discover the type of binding element this config section enables
        public override Type BindingElementType
        {
            get { return typeof(GZipMessageEncodingBindingElement); }
        }

        //The only property we need to configure for our binding element is the type of
        //inner encoder to use. Here, we support text and binary.
        [ConfigurationProperty("innerMessageEncoding", DefaultValue = "textMessageEncoding")]
        public string InnerMessageEncoding
        {
            get { return (string)base["innerMessageEncoding"]; }
            set { base["innerMessageEncoding"] = value; }
        }
        //[ConfigurationProperty("readerQuotas")]
        //public XmlDictionaryReaderQuotasElement ReaderQuotas {
        //    get { return (XmlDictionaryReaderQuotasElement)base["readerQuotas"]; }
        //    //set { base["readerQuotas"] = value; }
        //} 

        //Called by the WCF to apply the configuration settings (the property above) to the binding element
        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            GZipMessageEncodingBindingElement binding = (GZipMessageEncodingBindingElement)bindingElement;
            PropertyInformationCollection propertyInfo = this.ElementInformation.Properties;
            if (propertyInfo["innerMessageEncoding"].ValueOrigin != PropertyValueOrigin.Default)
            {
                switch (this.InnerMessageEncoding)
                {
                    case "textMessageEncoding":
                        TextMessageEncodingBindingElement textElement = new TextMessageEncodingBindingElement();
                        textElement.ReaderQuotas.MaxArrayLength = int.MaxValue;
                        textElement.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                        textElement.ReaderQuotas.MaxDepth = int.MaxValue;
                        textElement.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                        textElement.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                        binding.InnerMessageEncodingBindingElement = textElement;
                        break;
                    case "binaryMessageEncoding":
                        BinaryMessageEncodingBindingElement binaryElement = new BinaryMessageEncodingBindingElement();
                        binaryElement.ReaderQuotas.MaxArrayLength = int.MaxValue;
                        binaryElement.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
                        binaryElement.ReaderQuotas.MaxDepth = int.MaxValue;
                        binaryElement.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
                        binaryElement.ReaderQuotas.MaxStringContentLength = int.MaxValue;
                        binding.InnerMessageEncodingBindingElement = binaryElement;
                        break;
                }
            }
        }

        //Called by the WCF to create the binding element
        protected override BindingElement CreateBindingElement()
        {
            GZipMessageEncodingBindingElement bindingElement = new GZipMessageEncodingBindingElement();
            this.ApplyConfiguration(bindingElement);
            return bindingElement;
        }
    }
}
