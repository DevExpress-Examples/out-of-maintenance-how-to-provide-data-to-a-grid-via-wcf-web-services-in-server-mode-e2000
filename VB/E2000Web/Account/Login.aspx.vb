Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace E2000Web.Account
	Partial Public Class Login
		Inherits System.Web.UI.Page
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" & HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
		End Sub
	End Class
End Namespace
