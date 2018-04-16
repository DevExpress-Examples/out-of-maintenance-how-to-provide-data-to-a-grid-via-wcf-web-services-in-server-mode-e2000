Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Reflection

Namespace InterLinq.Objects
	''' <summary>
	''' LINQ to Objects specific implementation of the
	''' <see cref="IQueryHandler"/>.
	''' </summary>
	''' <seealso cref="IQueryHandler"/>
	Public Class ObjectQueryHandler
		Implements IQueryHandler

		#Region "Fields"

		Private ReadOnly objectSource As IObjectSource

		#End Region

		#Region "Constructors"

		''' <summary>
		''' Initializes this class.
		''' </summary>
		''' <param name="objectSource"><see cref="IObjectSource"/> used get all objects of a type.</param>
		Public Sub New(ByVal objectSource As IObjectSource)
			If objectSource Is Nothing Then
				Throw New ArgumentNullException("objectSource")
			End If
			Me.objectSource = objectSource
		End Sub

		#End Region

		#Region "IQueryHandler Members"

		Public Function GetTable1(Of T As Class)() As System.Linq.IQueryable(Of T) Implements IQueryHandler.GetTable
            Return (CType(Me, IQueryHandler)).Get(Of T)()
        End Function

        Public Function GetTable(ByVal type As System.Type) As System.Linq.IQueryable Implements IQueryHandler.GetTable
            Return (CType(Me, IQueryHandler)).Get(type)
        End Function

        Public Function [Get](ByVal type As System.Type) As System.Linq.IQueryable Implements IQueryHandler.Get
            Dim getTableMethod As MethodInfo = Me.GetType().GetMethod("Get1", BindingFlags.Instance Or BindingFlags.Public, Nothing, New Type() {}, Nothing)
            Dim genericGetTableMethod As MethodInfo = getTableMethod.MakeGenericMethod(type)
            Return CType(genericGetTableMethod.Invoke(Me, New Object() {}), IQueryable)
        End Function

        Public Function Get1(Of T As Class)() As System.Linq.IQueryable(Of T) Implements IQueryHandler.Get
            Return objectSource.GetObjects(Of T)().AsQueryable()
        End Function

        Public Function StartSession() As Boolean Implements IQueryHandler.StartSession
            Return True
        End Function

        Public Function CloseSession() As Boolean Implements IQueryHandler.CloseSession
            Return True
        End Function

#End Region

	End Class
End Namespace
