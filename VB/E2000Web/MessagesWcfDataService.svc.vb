Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Data.Services
Imports System.Data.Services.Common
Imports System.Linq
Imports System.ServiceModel.Web
Imports System.Web

Namespace E2000Web
	Public Class MessagesWcfDataService
		Inherits DataService(Of TestDatabaseEntities1)
		Public Shared Sub InitializeService(ByVal config As DataServiceConfiguration)
			config.SetEntitySetAccessRule("*", EntitySetRights.AllRead)
			config.SetServiceOperationAccessRule("*", ServiceOperationRights.All)
			config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2
		End Sub
	End Class
End Namespace
