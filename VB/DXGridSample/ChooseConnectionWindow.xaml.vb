Imports Microsoft.VisualBasic
Imports System.Windows

Namespace DXGridSample

	Partial Public Class ChooseConnectionWindow
		Inherits Window
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub localButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Tag = True
			Close()
		End Sub

		Private Sub internetButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Tag = False
			Close()
		End Sub
	End Class
End Namespace
