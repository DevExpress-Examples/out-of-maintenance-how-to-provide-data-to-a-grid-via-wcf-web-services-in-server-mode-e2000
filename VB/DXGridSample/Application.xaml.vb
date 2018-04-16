Imports System
Imports System.Windows
Imports DevExpress.Data.Linq.Helpers

Namespace DXGridSample

	Partial Public Class App
		Inherits Application
        Private Sub Application_Startup(ByVal sender As Object, _
                                        ByVal e As StartupEventArgs)
            LinqServerModeKeyedCache.CanSkip = True
        End Sub
	End Class
End Namespace
