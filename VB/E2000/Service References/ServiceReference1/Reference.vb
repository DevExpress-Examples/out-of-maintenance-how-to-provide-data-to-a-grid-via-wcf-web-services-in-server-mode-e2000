'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18033
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

' Original file name:
' Generation date: 2/11/2013 11:13:41 AM

Imports Microsoft.VisualBasic
Imports System
Namespace E2000.ServiceReference1

	''' <summary>
	''' There are no comments for TestDatabaseEntities1 in the schema.
	''' </summary>
	Partial Public Class TestDatabaseEntities1
		Inherits System.Data.Services.Client.DataServiceContext
		''' <summary>
		''' Initialize a new TestDatabaseEntities1 object.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Sub New(ByVal serviceRoot As Global.System.Uri)
			MyBase.New(serviceRoot)
			Me.ResolveName = New Global.System.Func(Of Global.System.Type, String)(AddressOf Me.ResolveNameFromType)
			Me.ResolveType = New Global.System.Func(Of String, Global.System.Type)(AddressOf Me.ResolveTypeFromName)
			Me.OnContextCreated()
		End Sub
		Partial Private Sub OnContextCreated()
		End Sub
		''' <summary>
		''' Since the namespace configured for this service reference
		''' in Visual Studio is different from the one indicated in the
		''' server schema, use type-mappers to map between the two.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Protected Function ResolveTypeFromName(ByVal typeName As String) As Global.System.Type
			If typeName.StartsWith("TestDatabaseModel", Global.System.StringComparison.Ordinal) Then
				Return Me.GetType().Assembly.GetType(String.Concat("E2000.ServiceReference1", typeName.Substring(17)), False)
			End If
			Return Nothing
		End Function
		''' <summary>
		''' Since the namespace configured for this service reference
		''' in Visual Studio is different from the one indicated in the
		''' server schema, use type-mappers to map between the two.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Protected Function ResolveNameFromType(ByVal clientType As Global.System.Type) As String
			If clientType.Namespace.Equals("E2000.ServiceReference1", Global.System.StringComparison.Ordinal) Then
				Return String.Concat("TestDatabaseModel.", clientType.Name)
			End If
			Return Nothing
		End Function
		''' <summary>
		''' There are no comments for Messages in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public ReadOnly Property Messages() As Global.System.Data.Services.Client.DataServiceQuery(Of Message)
			Get
				If (Me._Messages Is Nothing) Then
					Me._Messages = MyBase.CreateQuery(Of Message)("Messages")
				End If
				Return Me._Messages
			End Get
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _Messages As Global.System.Data.Services.Client.DataServiceQuery(Of Message)
		''' <summary>
		''' There are no comments for Messages in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Sub AddToMessages(ByVal message As Message)
			MyBase.AddObject("Messages", message)
		End Sub
	End Class
	''' <summary>
	''' There are no comments for TestDatabaseModel.Message in the schema.
	''' </summary>
	''' <KeyProperties>
	''' Id
	''' </KeyProperties>
	<Global.System.Data.Services.Common.EntitySetAttribute("Messages"), Global.System.Data.Services.Common.DataServiceKeyAttribute("Id")> _
	Partial Public Class Message
		Implements System.ComponentModel.INotifyPropertyChanged
		''' <summary>
		''' Create a new Message object.
		''' </summary>
		''' <param name="ID">Initial value of Id.</param>
		''' <param name="from">Initial value of From.</param>
		''' <param name="subject">Initial value of Subject.</param>
		''' <param name="sent">Initial value of Sent.</param>
		''' <param name="size">Initial value of Size.</param>
		''' <param name="hasAttachment">Initial value of HasAttachment.</param>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Shared Function CreateMessage(ByVal ID As Long, ByVal [from] As String, ByVal subject As String, ByVal sent As Global.System.DateTime, ByVal size As Long, ByVal hasAttachment As Boolean) As Message
			Dim message As New Message()
			message.Id = ID
			message.From = Message.From
			message.Subject = subject
			message.Sent = sent
			message.Size = size
			message.HasAttachment = hasAttachment
			Return message
		End Function
		''' <summary>
		''' There are no comments for Property Id in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property Id() As Long
			Get
				Return Me._Id
			End Get
			Set(ByVal value As Long)
				Me.OnIdChanging(value)
				Me._Id = value
				Me.OnIdChanged()
				Me.OnPropertyChanged("Id")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _Id As Long
		Partial Private Sub OnIdChanging(ByVal value As Long)
		End Sub
		Partial Private Sub OnIdChanged()
		End Sub
		''' <summary>
		''' There are no comments for Property From in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property [From]() As String
			Get
				Return Me._From
			End Get
			Set(ByVal value As String)
				Me.OnFromChanging(value)
				Me._From = value
				Me.OnFromChanged()
				Me.OnPropertyChanged("From")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _From As String
		Partial Private Sub OnFromChanging(ByVal value As String)
		End Sub
		Partial Private Sub OnFromChanged()
		End Sub
		''' <summary>
		''' There are no comments for Property Subject in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property Subject() As String
			Get
				Return Me._Subject
			End Get
			Set(ByVal value As String)
				Me.OnSubjectChanging(value)
				Me._Subject = value
				Me.OnSubjectChanged()
				Me.OnPropertyChanged("Subject")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _Subject As String
		Partial Private Sub OnSubjectChanging(ByVal value As String)
		End Sub
		Partial Private Sub OnSubjectChanged()
		End Sub
		''' <summary>
		''' There are no comments for Property Sent in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property Sent() As Global.System.DateTime
			Get
				Return Me._Sent
			End Get
			Set(ByVal value As System.DateTime)
				Me.OnSentChanging(value)
				Me._Sent = value
				Me.OnSentChanged()
				Me.OnPropertyChanged("Sent")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _Sent As Global.System.DateTime
		Partial Private Sub OnSentChanging(ByVal value As Global.System.DateTime)
		End Sub
		Partial Private Sub OnSentChanged()
		End Sub
		''' <summary>
		''' There are no comments for Property Size in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property Size() As Long
			Get
				Return Me._Size
			End Get
			Set(ByVal value As Long)
				Me.OnSizeChanging(value)
				Me._Size = value
				Me.OnSizeChanged()
				Me.OnPropertyChanged("Size")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _Size As Long
		Partial Private Sub OnSizeChanging(ByVal value As Long)
		End Sub
		Partial Private Sub OnSizeChanged()
		End Sub
		''' <summary>
		''' There are no comments for Property HasAttachment in the schema.
		''' </summary>
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Property HasAttachment() As Boolean
			Get
				Return Me._HasAttachment
			End Get
			Set(ByVal value As Boolean)
				Me.OnHasAttachmentChanging(value)
				Me._HasAttachment = value
				Me.OnHasAttachmentChanged()
				Me.OnPropertyChanged("HasAttachment")
			End Set
		End Property
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Private _HasAttachment As Boolean
		Partial Private Sub OnHasAttachmentChanging(ByVal value As Boolean)
		End Sub
		Partial Private Sub OnHasAttachmentChanged()
		End Sub
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Public Event PropertyChanged As Global.System.ComponentModel.PropertyChangedEventHandler Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
		<System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")> _
		Protected Overridable Sub OnPropertyChanged(ByVal [property] As String)
			If (Me.PropertyChangedEvent IsNot Nothing) Then
				RaiseEvent PropertyChanged(Me, New Global.System.ComponentModel.PropertyChangedEventArgs([property]))
			End If
		End Sub
	End Class
End Namespace
