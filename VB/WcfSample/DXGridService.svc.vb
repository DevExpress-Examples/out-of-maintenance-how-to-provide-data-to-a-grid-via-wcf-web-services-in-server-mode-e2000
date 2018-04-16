Imports Microsoft.VisualBasic
Imports System
Imports System.Data.Linq
Imports System.ServiceModel
Imports System.Web.Configuration
Imports DataObjects
Imports InterLinq
Imports InterLinq.Communication.Wcf
Imports InterLinq.Objects
Imports InterLinq.Sql

Namespace WcfSample
	<ServiceBehavior(ConcurrencyMode := ConcurrencyMode.Single, InstanceContextMode := InstanceContextMode.Single, IncludeExceptionDetailInFaults := True)> _
	Public Class TestServerQueryWcfHandler
		Implements IQueryRemoteHandler

		Private Shared Function CreateHandler() As IQueryHandler
			Dim connectionString As String = WebConfigurationManager.ConnectionStrings("DXGridServerModeDB").ConnectionString

			If (Not String.IsNullOrEmpty(connectionString)) Then
				Dim dataContext As DataContext = New DataGridTestClassesDataContext(connectionString)
				Return New SqlQueryHandler(dataContext)
			Else
				Dim exampleObjectSource As New ExampleObjectSource()

				For i As Integer = 0 To 29999
					Dim obj As WpfServerSideGridTest = OutlookDataGenerator.CreateNewObject()
					obj.OID = i + 1
					exampleObjectSource.Objects.Add(obj)
				Next i

				Return New ObjectQueryHandler(exampleObjectSource)
			End If
		End Function

		Private handler As IQueryRemoteHandler
		Private isServerMode As Boolean

		Public Sub New()
			Dim innerHandler As IQueryHandler = CreateHandler()
			isServerMode = TypeOf innerHandler Is SqlQueryHandler
			handler = New ServerQueryWcfHandler(innerHandler)
		End Sub

		#Region "IQueryRemoteHandler Members"

		Public Function Retrieve(ByVal expression As InterLinq.Expressions.SerializableExpression) As Object Implements IQueryRemoteHandler.Retrieve
			Dim tickCount As Long = Environment.TickCount
			Dim obj As Object = handler.Retrieve(expression)
			Return New SeverInfoObject() With {.IsServerMode = isServerMode, .Milliseconds = CInt(Fix(Environment.TickCount - tickCount)), .Obj = obj}
		End Function

		#End Region
	End Class
End Namespace
