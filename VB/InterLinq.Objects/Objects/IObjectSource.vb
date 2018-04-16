Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ServiceModel
Imports InterLinq.Expressions
Imports System.Runtime.Serialization

Namespace InterLinq.Objects
	<DataContract, Serializable> _
	Public Class SeverInfoObject
		Private privateObj As Object
		<DataMember> _
		Public Property Obj() As Object
			Get
				Return privateObj
			End Get
			Set(ByVal value As Object)
				privateObj = value
			End Set
		End Property
		Private privateMilliseconds As Integer
		<DataMember> _
		Public Property Milliseconds() As Integer
			Get
				Return privateMilliseconds
			End Get
			Set(ByVal value As Integer)
				privateMilliseconds = value
			End Set
		End Property
		Private privateIsServerMode As Boolean
		<DataMember> _
		Public Property IsServerMode() As Boolean
			Get
				Return privateIsServerMode
			End Get
			Set(ByVal value As Boolean)
				privateIsServerMode = value
			End Set
		End Property
	End Class
	''' <summary>
	''' This interface contains methods to get all <see langword="object">objects</see>
	''' of a certain <see cref="Type"/>.
	''' </summary>
	Public Interface IObjectSource
		''' <summary>
		''' Returns an <see cref="IEnumerable{T}"/> containing all objects
		''' of the <see cref="Type"/> <typeparamref name="T"/> stored in
		''' this implementation of <see cref="IObjectSource"/>.
		''' </summary>
		''' <typeparam name="T"><see cref="Type"/> of the <see langword="object">objects</see>.</typeparam>
		''' <returns>
		''' Returns an <see cref="IEnumerable{T}"/> containing all objects
		''' of the <see cref="Type"/> <typeparamref name="T"/> stored in
		''' this implementation of <see cref="IObjectSource"/>.
		''' </returns>
		''' <remarks>
		''' The implementation of this method may throws an <see cref="Exception"/> if
		''' the requested <see cref="Type"/> could not be found.
		''' </remarks>
		Function GetObjects(Of T)() As IEnumerable(Of T)
	End Interface
End Namespace
