'-----------------------------------------------------------------------------
' Copyright (c) Microsoft Corporation.  All rights reserved.
'-----------------------------------------------------------------------------


Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Configuration
Imports System.ServiceModel.Description
Imports System.Xml


Namespace Microsoft.ServiceModel.Samples

	' This is constants for GZip message encoding policy.
	Friend NotInheritable Class GZipMessageEncodingPolicyConstants
		Public Const GZipEncodingName As String = "GZipEncoding"
		Public Const GZipEncodingNamespace As String = "http://schemas.microsoft.com/ws/06/2004/mspolicy/netgzip1"
		Public Const GZipEncodingPrefix As String = "gzip"
	End Class

	'This is the binding element that, when plugged into a custom binding, will enable the GZip encoder
	Public NotInheritable Class GZipMessageEncodingBindingElement
		Inherits MessageEncodingBindingElement
		Implements IPolicyExportExtension
		'public XmlDictionaryReaderQuotas ReaderQuotas {
		'    get {
		'        BinaryMessageEncodingBindingElement el1 = this.innerBindingElement as BinaryMessageEncodingBindingElement;
		'        if(el1 != null) return el1.ReaderQuotas;
		'        TextMessageEncodingBindingElement el2 = this.innerBindingElement as TextMessageEncodingBindingElement;
		'        if(el2 != null) return el2.ReaderQuotas;
		'        return null;
		'    }
		'} 

		'We will use an inner binding element to store information required for the inner encoder
		Private innerBindingElement As MessageEncodingBindingElement

		'By default, use the default text encoder as the inner encoder
		Public Sub New()
			Me.New(New TextMessageEncodingBindingElement())
		End Sub

		Public Sub New(ByVal messageEncoderBindingElement As MessageEncodingBindingElement)
			Me.innerBindingElement = messageEncoderBindingElement
		End Sub

		Public Property InnerMessageEncodingBindingElement() As MessageEncodingBindingElement
			Get
				Return innerBindingElement
			End Get
			Set(ByVal value As MessageEncodingBindingElement)
				innerBindingElement = value
			End Set
		End Property

		'Main entry point into the encoder binding element. Called by WCF to get the factory that will create the
		'message encoder
		Public Overrides Function CreateMessageEncoderFactory() As MessageEncoderFactory
			Return New GZipMessageEncoderFactory(innerBindingElement.CreateMessageEncoderFactory())
		End Function

		Public Overrides Property MessageVersion() As MessageVersion
			Get
				Return innerBindingElement.MessageVersion
			End Get
			Set(ByVal value As MessageVersion)
				innerBindingElement.MessageVersion = value
			End Set
		End Property

		Public Overrides Function Clone() As BindingElement
			Return New GZipMessageEncodingBindingElement(Me.innerBindingElement)
		End Function

        Public Overrides Function GetProperty(Of T As Class)(ByVal context As BindingContext) As T
            If GetType(T) Is GetType(XmlDictionaryReaderQuotas) Then
                Return innerBindingElement.GetProperty(Of T)(context)
            Else
                Return MyBase.GetProperty(Of T)(context)
            End If
        End Function

		Public Overrides Function BuildChannelFactory(Of TChannel)(ByVal context As BindingContext) As IChannelFactory(Of TChannel)
			If context Is Nothing Then
				Throw New ArgumentNullException("context")
			End If

			context.BindingParameters.Add(Me)
			Return context.BuildInnerChannelFactory(Of TChannel)()
		End Function

        Public Overrides Function BuildChannelListener(Of TChannel As {Class, IChannel
                                                           })(ByVal context As BindingContext) As IChannelListener(Of TChannel)
            If context Is Nothing Then
                Throw New ArgumentNullException("context")
            End If

            context.BindingParameters.Add(Me)
            Return context.BuildInnerChannelListener(Of TChannel)()
        End Function

        Public Overrides Function CanBuildChannelListener(Of TChannel As {Class, IChannel})(ByVal context As BindingContext) As Boolean
            If context Is Nothing Then
                Throw New ArgumentNullException("context")
            End If

            context.BindingParameters.Add(Me)
            Return context.CanBuildInnerChannelListener(Of TChannel)()
        End Function

		Private Sub ExportPolicy(ByVal exporter As MetadataExporter, ByVal policyContext As PolicyConversionContext) Implements IPolicyExportExtension.ExportPolicy
			If policyContext Is Nothing Then
				Throw New ArgumentNullException("policyContext")
			End If
			Dim document As New XmlDocument()
			policyContext.GetBindingAssertions().Add(document.CreateElement(GZipMessageEncodingPolicyConstants.GZipEncodingPrefix, GZipMessageEncodingPolicyConstants.GZipEncodingName, GZipMessageEncodingPolicyConstants.GZipEncodingNamespace))
		End Sub
	End Class

	'This class is necessary to be able to plug in the GZip encoder binding element through
	'a configuration file
	Public Class GZipMessageEncodingElement
		Inherits BindingElementExtensionElement
		Public Sub New()
		End Sub

		'Called by the WCF to discover the type of binding element this config section enables
		Public Overrides ReadOnly Property BindingElementType() As Type
			Get
				Return GetType(GZipMessageEncodingBindingElement)
			End Get
		End Property

		'The only property we need to configure for our binding element is the type of
		'inner encoder to use. Here, we support text and binary.
		<ConfigurationProperty("innerMessageEncoding", DefaultValue := "textMessageEncoding")> _
		Public Property InnerMessageEncoding() As String
			Get
				Return CStr(MyBase.Item("innerMessageEncoding"))
			End Get
			Set(ByVal value As String)
				MyBase.Item("innerMessageEncoding") = value
			End Set
		End Property
		'[ConfigurationProperty("readerQuotas")]
		'public XmlDictionaryReaderQuotasElement ReaderQuotas {
		'    get { return (XmlDictionaryReaderQuotasElement)base["readerQuotas"]; }
		'    //set { base["readerQuotas"] = value; }
		'} 

		'Called by the WCF to apply the configuration settings (the property above) to the binding element
		Public Overrides Sub ApplyConfiguration(ByVal bindingElement As BindingElement)
			Dim binding As GZipMessageEncodingBindingElement = CType(bindingElement, GZipMessageEncodingBindingElement)
			Dim propertyInfo As PropertyInformationCollection = Me.ElementInformation.Properties
			If propertyInfo("innerMessageEncoding").ValueOrigin <> PropertyValueOrigin.Default Then
				Select Case Me.InnerMessageEncoding
					Case "textMessageEncoding"
						Dim textElement As New TextMessageEncodingBindingElement()
						textElement.ReaderQuotas.MaxArrayLength = Integer.MaxValue
						textElement.ReaderQuotas.MaxBytesPerRead = Integer.MaxValue
						textElement.ReaderQuotas.MaxDepth = Integer.MaxValue
						textElement.ReaderQuotas.MaxNameTableCharCount = Integer.MaxValue
						textElement.ReaderQuotas.MaxStringContentLength = Integer.MaxValue
						binding.InnerMessageEncodingBindingElement = textElement
					Case "binaryMessageEncoding"
						Dim binaryElement As New BinaryMessageEncodingBindingElement()
						binaryElement.ReaderQuotas.MaxArrayLength = Integer.MaxValue
						binaryElement.ReaderQuotas.MaxBytesPerRead = Integer.MaxValue
						binaryElement.ReaderQuotas.MaxDepth = Integer.MaxValue
						binaryElement.ReaderQuotas.MaxNameTableCharCount = Integer.MaxValue
						binaryElement.ReaderQuotas.MaxStringContentLength = Integer.MaxValue
						binding.InnerMessageEncodingBindingElement = binaryElement
				End Select
			End If
		End Sub

		'Called by the WCF to create the binding element
		Protected Overrides Function CreateBindingElement() As BindingElement
			Dim bindingElement As New GZipMessageEncodingBindingElement()
			Me.ApplyConfiguration(bindingElement)
			Return bindingElement
		End Function
	End Class
End Namespace
