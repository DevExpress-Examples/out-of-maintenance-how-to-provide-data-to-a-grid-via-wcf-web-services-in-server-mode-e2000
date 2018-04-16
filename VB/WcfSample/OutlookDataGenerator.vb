Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.Serialization
Imports System.ServiceModel
Imports System.Text
Imports InterLinq.Objects
Imports DataObjects
Imports InterLinq.Communication.Wcf
Imports InterLinq
Imports InterLinq.Sql
Imports System.Data.Linq
Imports System.Web.Configuration

Namespace WcfSample
	Public Class ExampleObjectSource
		Implements IObjectSource
		Public Sub New()
			Objects = New List(Of WpfServerSideGridTest)()
		End Sub
		Private privateObjects As List(Of WpfServerSideGridTest)
		Public Property Objects() As List(Of WpfServerSideGridTest)
			Get
				Return privateObjects
			End Get
			Set(ByVal value As List(Of WpfServerSideGridTest))
				privateObjects = value
			End Set
		End Property
		Public Function GetObjects(Of T)() As IEnumerable(Of T) Implements IObjectSource.GetObjects
			If GetType(T) Is GetType(WpfServerSideGridTest) Then
				Return CType(Objects, IEnumerable(Of T))
			End If
			Throw New Exception(String.Format("Type '{0}' is not stored in this ExampleObjectSource.", GetType(T)))
		End Function

	End Class
	Public Class User
		Private privateId As Integer
		Public Property Id() As Integer
			Get
				Return privateId
			End Get
			Set(ByVal value As Integer)
				privateId = value
			End Set
		End Property
		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Public Overrides Function ToString() As String
			Return Name
		End Function
	End Class
	Public Enum Priority
		Low
		BelowNormal
		Normal
		AboveNormal
		High
	End Enum
	Public NotInheritable Class OutlookDataGenerator
		Private Shared rnd As New Random()
		Public Shared ReadOnly Users() As User = { New User() With {.Id = 0, .Name = "Peter Dolan"}, New User() With {.Id = 1, .Name = "Ryan Fischer"}, New User() With {.Id = 2, .Name = "Richard Fisher"}, New User() With {.Id = 3, .Name = "Tom Hamlett"}, New User() With {.Id = 4, .Name = "Mark Hamilton"}, New User() With {.Id = 5, .Name = "Steve Lee"}, New User() With {.Id = 6, .Name = "Jimmy Lewis"}, New User() With {.Id = 7, .Name = "Jeffrey W McClain"}, New User() With {.Id = 8, .Name = "Andrew Miller"}, New User() With {.Id = 9, .Name = "Dave Murrel"}, New User() With {.Id = 10, .Name = "Bert Parkins"}, New User() With {.Id = 11, .Name = "Mike Roller"}, New User() With {.Id = 12, .Name = "Ray Shipman"}, New User() With {.Id = 13, .Name = "Paul Bailey"}, New User() With {.Id = 14, .Name = "Brad Barnes"}, New User() With {.Id = 15, .Name = "Carl Lucas"}, New User() With {.Id = 16, .Name = "Jerry Campbell"} }
		Public Shared Subjects() As String = { "Integrating Developer Express MasterView control into an Accounting System.", "Web Edition: Data Entry Page. There is an issue with date validation.", "Payables Due Calculator is ready for testing.", "Web Edition: Search Page is ready for testing.", "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.", "Receivables Calculator. Where can I find the complete specs?", "Ledger: Inconsistency. Please fix it.", "Receivables Printing module is ready for testing.", "Screen Redraw. Somebody has to look at it.", "Email System. What library are we going to use?", "Cannot add new vendor. This module doesn't work!", "History. Will we track sales history in our system?", "Main Menu: Add a File menu. File menu item is missing.", "Currency Mask. The current currency mask in completely unusable.", "Drag & Drop operations are not available in the scheduler module.", "Data Import. What database types will we support?", "Reports. The list of incomplete reports.", "Data Archiving. We still don't have this features in our application.", "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.", "Check Register. We are using different paths for different modules.", "Data Export. Our customers asked us for export to Microsoft Excel"}

		Private Sub New()
		End Sub
		Public Shared Function GetSubject() As String
			Return Subjects(rnd.Next(Subjects.Length - 1))
		End Function

		Public Shared Function GetFrom() As String
			Return Users(GetFromId()).Name
		End Function

		Public Shared Function GetSentDate() As DateTime
			Dim ret As DateTime = DateTime.Today
			Dim r As Integer = rnd.Next(12)
			If r > 1 Then
				ret = ret.AddDays(-rnd.Next(50))
			End If
			Return ret
		End Function
		Public Shared Function GetSize(ByVal largeData As Boolean) As Integer
			Return 1000 + (If(largeData, 20 * rnd.Next(10000), 30 * rnd.Next(100)))
		End Function
		Public Shared Function GetHasAttachment() As Boolean
			Return rnd.Next(10) > 5
		End Function
		Public Shared Function GetPriority() As Priority
			Return CType(rnd.Next(5), Priority)
		End Function
		Public Shared Function GetHoursActive() As Integer
			Return CInt(Fix(Math.Round(rnd.NextDouble() * 1000, 1)))
		End Function
		Public Shared Function GetFromId() As Integer
			Return rnd.Next(Users.Length)
		End Function
		Public Shared Function CreateNewObject() As WpfServerSideGridTest
			Dim obj As New WpfServerSideGridTest()
			obj.Subject = OutlookDataGenerator.GetSubject()
			obj.From = OutlookDataGenerator.GetFrom()
			obj.Sent = OutlookDataGenerator.GetSentDate()
			obj.HasAttachment = OutlookDataGenerator.GetHasAttachment()
			obj.Size = OutlookDataGenerator.GetSize(obj.HasAttachment.Value)
			Return obj
		End Function
	End Class
End Namespace
