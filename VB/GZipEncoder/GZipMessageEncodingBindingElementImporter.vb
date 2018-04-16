'-----------------------------------------------------------------------------
' Copyright (c) Microsoft Corporation.  All rights reserved.
'-----------------------------------------------------------------------------


Imports Microsoft.VisualBasic
	Imports System
	Imports System.Xml
	Imports System.ServiceModel.Description
	Imports System.Xml.Schema
	Imports System.Collections.ObjectModel
	Imports System.Collections.Generic
	Imports System.Text
Namespace Microsoft.ServiceModel.Samples

	Public Class GZipMessageEncodingBindingElementImporter
		Implements IPolicyImportExtension
		Public Sub New()
		End Sub

		Private Sub ImportPolicy(ByVal importer As MetadataImporter, ByVal context As PolicyConversionContext) Implements IPolicyImportExtension.ImportPolicy
			If importer Is Nothing Then
				Throw New ArgumentNullException("importer")
			End If

			If context Is Nothing Then
				Throw New ArgumentNullException("context")
			End If

			Dim assertions As ICollection(Of XmlElement) = context.GetBindingAssertions()
			For Each assertion As XmlElement In assertions
				If (assertion.NamespaceURI = GZipMessageEncodingPolicyConstants.GZipEncodingNamespace) AndAlso (assertion.LocalName = GZipMessageEncodingPolicyConstants.GZipEncodingName) Then
					assertions.Remove(assertion)
					context.BindingElements.Add(New GZipMessageEncodingBindingElement())
					Exit For
				End If
			Next assertion
		End Sub
	End Class
End Namespace

