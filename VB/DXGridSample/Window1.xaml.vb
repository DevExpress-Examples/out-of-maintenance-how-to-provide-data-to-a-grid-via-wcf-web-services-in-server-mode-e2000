Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.ObjectModel
Imports System.ServiceModel
Imports System.Windows
Imports DataObjects
Imports DevExpress.Data.Linq
Imports InterLinq
Imports InterLinq.Communication
Imports InterLinq.Objects
Imports Microsoft.ServiceModel.Samples

Namespace DXGridSample

	Partial Public Class Window1
		Inherits Window

		Public Sub New()
			InitializeComponent()

			Dim connectionWindow As New ChooseConnectionWindow()
			connectionWindow.ShowDialog()

			Dim useLocalConnection As Boolean = CBool(connectionWindow.Tag)
			Dim configName As String = "InterLinqServiceNetTcp" & (If(useLocalConnection, "_local", String.Empty))
			Dim wrapper As New QueryRemoteHandlerWrapper(New ChannelFactory(Of IQueryRemoteHandler)(configName).CreateChannel())
			Dim clientQueryHandler As New ClientQueryHandler(wrapper)
			Dim simpleExampleContext As New SimpleExampleContext(clientQueryHandler)

			Dim collectionSource As New LinqServerModeSource() With {.ElementType = GetType(WpfServerSideGridTest), .QueryableSource = simpleExampleContext.Objects, .KeyExpression = "OID"}

			grid.ItemsSource = collectionSource

			infoContainer.DataContext = wrapper
			scrollViewer.ScrollToEnd()
			logHeaderText.Text = "ServerProcessingTime(ms)" & Constants.vbTab & "TotalProcessingTime(ms)" & Constants.vbTab & "RequestSize(bytes)" & Constants.vbTab & "ResponseSize(bytes)"
		End Sub
	End Class
	Public Class QueryInfo
		Private privateRequestSize As Integer
		Public Property RequestSize() As Integer
			Get
				Return privateRequestSize
			End Get
			Set(ByVal value As Integer)
				privateRequestSize = value
			End Set
		End Property
		Private privateResponseSize As Integer
		Public Property ResponseSize() As Integer
			Get
				Return privateResponseSize
			End Get
			Set(ByVal value As Integer)
				privateResponseSize = value
			End Set
		End Property
	End Class

	Public Class QueryRemoteHandlerWrapper
		Inherits DependencyObject
		Implements IQueryRemoteHandler
		Public Property TotalBytesSent() As Integer
			Get
				Return CInt(Fix(GetValue(TotalBytesSentProperty)))
			End Get
			Set(ByVal value As Integer)
				SetValue(TotalBytesSentProperty, value)
			End Set
		End Property

		Public Shared ReadOnly TotalBytesSentProperty As DependencyProperty = DependencyProperty.Register("TotalBytesSent", GetType(Integer), GetType(QueryRemoteHandlerWrapper), New UIPropertyMetadata(0))

		Public Property TotalBytesReceived() As Integer
			Get
				Return CInt(Fix(GetValue(TotalBytesReceivedProperty)))
			End Get
			Set(ByVal value As Integer)
				SetValue(TotalBytesReceivedProperty, value)
			End Set
		End Property

		Public Shared ReadOnly TotalBytesReceivedProperty As DependencyProperty = DependencyProperty.Register("TotalBytesReceived", GetType(Integer), GetType(QueryRemoteHandlerWrapper), New UIPropertyMetadata(0))

		Public Property NotInServerMode() As Boolean
			Get
				Return CBool(GetValue(NotInServerModeProperty))
			End Get
			Set(ByVal value As Boolean)
				SetValue(NotInServerModeProperty, value)
			End Set
		End Property

		Public Shared ReadOnly NotInServerModeProperty As DependencyProperty = DependencyProperty.Register("NotInServerMode", GetType(Boolean), GetType(QueryRemoteHandlerWrapper), New UIPropertyMetadata(False))

		Private Const MaxLogSize As Integer = 20
		Private ReadOnly handler As IQueryRemoteHandler
		Private privateLog As ObservableCollection(Of String)
		Public Property Log() As ObservableCollection(Of String)
			Get
				Return privateLog
			End Get
			Private Set(ByVal value As ObservableCollection(Of String))
				privateLog = value
			End Set
		End Property
		Private currentInfo As QueryInfo

		Public Sub New(ByVal handler As IQueryRemoteHandler)
			AddHandler GZipMessageEncoderFactory.MessageReceived, AddressOf GZipMessageEncoderFactory_MessageReceived
			AddHandler GZipMessageEncoderFactory.MessageSent, AddressOf GZipMessageEncoderFactory_MessageSent

			Me.handler = handler

			Log = New ObservableCollection(Of String)()
			For i As Integer = 0 To MaxLogSize - 1
				Log.Add(String.Empty)
			Next i
		End Sub

		Private Sub GZipMessageEncoderFactory_MessageSent(ByVal sender As Object, ByVal e As MessageSizeEventArgs)
			currentInfo.RequestSize = e.MessageSize
		End Sub

		Private Sub GZipMessageEncoderFactory_MessageReceived(ByVal sender As Object, ByVal e As MessageSizeEventArgs)
			currentInfo.ResponseSize = e.MessageSize
		End Sub

		#Region "IQueryRemoteHandler Members"
		Public Function Retrieve(ByVal expression As InterLinq.Expressions.SerializableExpression) As Object Implements IQueryRemoteHandler.Retrieve
			currentInfo = New QueryInfo()
			Dim tickCount As Long = Environment.TickCount
			Dim pair As SeverInfoObject = CType(handler.Retrieve(expression), SeverInfoObject)
			RefreshLog(pair.Milliseconds, CInt(Fix(Environment.TickCount - tickCount)), pair.IsServerMode)

			Return pair.Obj
		End Function

		Private Sub RefreshLog(ByVal serverProcessingTime As Integer, ByVal totalProcessingTime As Integer, ByVal isServerMode As Boolean)
			Dim logEntry As String = String.Format("{0:### ##0}" & Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab & "{1:### ##0}" & Constants.vbTab + Constants.vbTab + Constants.vbTab + Constants.vbTab & "{2:### ##0}" & Constants.vbTab + Constants.vbTab + Constants.vbTab & "{3:### ##0}", serverProcessingTime, totalProcessingTime, currentInfo.RequestSize, currentInfo.ResponseSize)

			For i As Integer = 0 To MaxLogSize - 2
				Log(i) = Log(i + 1)
			Next i

			Log(MaxLogSize - 1) = logEntry
			TotalBytesSent += currentInfo.RequestSize
			TotalBytesReceived += currentInfo.ResponseSize
			NotInServerMode = Not isServerMode
		End Sub
		#End Region
	End Class
End Namespace
