Imports Microsoft.VisualBasic
Imports System.Linq
Imports InterLinq

Namespace DataObjects
	''' <summary>
	''' This is a context for easier access to the <see cref="IQueryHandler"/>.
	''' </summary>
	Public Class SimpleExampleContext
		Inherits InterLinqContext
		Public Sub New(ByVal queryHandler As IQueryHandler)
			MyBase.New(queryHandler)
		End Sub

		Public ReadOnly Property Objects() As IQueryable(Of WpfServerSideGridTest)
			Get
				Return QueryHander.Get(Of WpfServerSideGridTest)()
			End Get
		End Property

	End Class
End Namespace
