Imports Microsoft.VisualBasic
Imports System.Windows
Imports DevExpress.Data.Linq.Helpers

Namespace DXGridSample

	Partial Public Class App
		Inherits Application
		Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
			LinqServerModeCache.ForceSkip = True
		End Sub
	End Class
End Namespace
