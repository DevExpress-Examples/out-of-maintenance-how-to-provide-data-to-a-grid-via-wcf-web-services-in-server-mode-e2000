'  Copyright (c) Microsoft Corporation.  All Rights Reserved.


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text
Imports System.IO
Imports System.IO.Compression
Imports System.Runtime.Serialization
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Configuration
Imports System.Xml
Imports System.ServiceModel.Security
Imports System.ServiceModel.Description

Namespace Microsoft.ServiceModel.Samples
	Public Class MessageSizeEventArgs
		Inherits EventArgs
		Public Sub New(ByVal messageSize As Integer)
            Me.MessageSize = messageSize
		End Sub
		Private privateMessageSize As Integer
		Public Property MessageSize() As Integer
			Get
				Return privateMessageSize
			End Get
			Private Set(ByVal value As Integer)
				privateMessageSize = value
			End Set
		End Property
	End Class
	Public Delegate Sub MeassageSizeEventHandler(ByVal sender As Object, ByVal e As MessageSizeEventArgs)
	'This class is used to create the custom encoder (GZipMessageEncoder)
	Public Class GZipMessageEncoderFactory
		Inherits MessageEncoderFactory
		Public Shared Event MessageSent As MeassageSizeEventHandler
		Public Shared Event MessageReceived As MeassageSizeEventHandler
		Friend Shared Sub RaiseMessageSent(ByVal size As Integer)
			RaiseEvent MessageSent(Nothing, New MessageSizeEventArgs(size))
		End Sub
		Friend Shared Sub RaiseMessageReceived(ByVal size As Integer)
			RaiseEvent MessageReceived(Nothing, New MessageSizeEventArgs(size))
		End Sub
		Private encoder_Renamed As MessageEncoder

		'The GZip encoder wraps an inner encoder
		'We require a factory to be passed in that will create this inner encoder
		Public Sub New(ByVal messageEncoderFactory As MessageEncoderFactory)
			If messageEncoderFactory Is Nothing Then
				Throw New ArgumentNullException("messageEncoderFactory", "A valid message encoder factory must be passed to the GZipEncoder")
			End If
			encoder_Renamed = New GZipMessageEncoder(messageEncoderFactory.Encoder)

		End Sub

		'The service framework uses this property to obtain an encoder from this encoder factory
		Public Overrides ReadOnly Property Encoder() As MessageEncoder
			Get
				Return encoder_Renamed
			End Get
		End Property

		Public Overrides ReadOnly Property MessageVersion() As MessageVersion
			Get
				Return encoder_Renamed.MessageVersion
			End Get
		End Property

		'This is the actual GZip encoder
		Private Class GZipMessageEncoder
			Inherits MessageEncoder
			Private Shared GZipContentType As String = "application/x-gzip"

			'This implementation wraps an inner encoder that actually converts a WCF Message
			'into textual XML, binary XML or some other format. This implementation then compresses the results.
			'The opposite happens when reading messages.
			'This member stores this inner encoder.
			Private innerEncoder As MessageEncoder

			'We require an inner encoder to be supplied (see comment above)
			Friend Sub New(ByVal messageEncoder As MessageEncoder)
				MyBase.New()
				If messageEncoder Is Nothing Then
					Throw New ArgumentNullException("messageEncoder", "A valid message encoder must be passed to the GZipEncoder")
				End If
				innerEncoder = messageEncoder
			End Sub

			'public override string CharSet
			'{
			'    get { return ""; }
			'}

			Public Overrides ReadOnly Property ContentType() As String
				Get
					Return GZipContentType
				End Get
			End Property

			Public Overrides ReadOnly Property MediaType() As String
				Get
					Return GZipContentType
				End Get
			End Property

			'SOAP version to use - we delegate to the inner encoder for this
			Public Overrides ReadOnly Property MessageVersion() As MessageVersion
				Get
					Return innerEncoder.MessageVersion
				End Get
			End Property

			'Helper method to compress an array of bytes
			Private Shared Function CompressBuffer(ByVal buffer As ArraySegment(Of Byte), ByVal bufferManager As BufferManager, ByVal messageOffset As Integer) As ArraySegment(Of Byte)
				Dim memoryStream As New MemoryStream()
				memoryStream.Write(buffer.Array, 0, messageOffset)

				Using gzStream As New GZipStream(memoryStream, CompressionMode.Compress, True)
					gzStream.Write(buffer.Array, messageOffset, buffer.Count)
				End Using


				Dim compressedBytes() As Byte = memoryStream.ToArray()
				Dim bufferedBytes() As Byte = bufferManager.TakeBuffer(compressedBytes.Length)

				Array.Copy(compressedBytes, 0, bufferedBytes, 0, compressedBytes.Length)

				bufferManager.ReturnBuffer(buffer.Array)
				Dim byteArray As New ArraySegment(Of Byte)(bufferedBytes, messageOffset, bufferedBytes.Length - messageOffset)

				Return byteArray
			End Function

			'Helper method to decompress an array of bytes
			Private Shared Function DecompressBuffer(ByVal buffer As ArraySegment(Of Byte), ByVal bufferManager As BufferManager) As ArraySegment(Of Byte)
				Dim memoryStream As New MemoryStream(buffer.Array, buffer.Offset, buffer.Count - buffer.Offset)
				Dim decompressedStream As New MemoryStream()
				Dim totalRead As Integer = 0
				Dim blockSize As Integer = 1024
				Dim tempBuffer() As Byte = bufferManager.TakeBuffer(blockSize)
				Using gzStream As New GZipStream(memoryStream, CompressionMode.Decompress)
					Do
						Dim bytesRead As Integer = gzStream.Read(tempBuffer, 0, blockSize)
						If bytesRead = 0 Then
							Exit Do
						End If
						decompressedStream.Write(tempBuffer, 0, bytesRead)
						totalRead += bytesRead
					Loop
				End Using
				bufferManager.ReturnBuffer(tempBuffer)

				Dim decompressedBytes() As Byte = decompressedStream.ToArray()
				Dim bufferManagerBuffer() As Byte = bufferManager.TakeBuffer(decompressedBytes.Length + buffer.Offset)
				Array.Copy(buffer.Array, 0, bufferManagerBuffer, 0, buffer.Offset)
				Array.Copy(decompressedBytes, 0, bufferManagerBuffer, buffer.Offset, decompressedBytes.Length)

				Dim byteArray As New ArraySegment(Of Byte)(bufferManagerBuffer, buffer.Offset, decompressedBytes.Length)
				bufferManager.ReturnBuffer(buffer.Array)

				Return byteArray
			End Function


			'One of the two main entry points into the encoder. Called by WCF to decode a buffered byte array into a Message.
			Public Overrides Overloads Function ReadMessage(ByVal buffer As ArraySegment(Of Byte), ByVal bufferManager As BufferManager, ByVal contentType As String) As Message
				'Decompress the buffer
				Dim decompressedBuffer As ArraySegment(Of Byte) = DecompressBuffer(buffer, bufferManager)
				'Use the inner encoder to decode the decompressed buffer
				Dim returnMessage As Message = innerEncoder.ReadMessage(decompressedBuffer, bufferManager)
				GZipMessageEncoderFactory.RaiseMessageSent(buffer.Count)
				returnMessage.Properties.Encoder = Me
				Return returnMessage
			End Function

			'One of the two main entry points into the encoder. Called by WCF to encode a Message into a buffered byte array.
			Public Overrides Overloads Function WriteMessage(ByVal message As Message, ByVal maxMessageSize As Integer, ByVal bufferManager As BufferManager, ByVal messageOffset As Integer) As ArraySegment(Of Byte)
				'Use the inner encoder to encode a Message into a buffered byte array
				Dim buffer As ArraySegment(Of Byte) = innerEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset)
				'Compress the resulting byte array
				Dim result As ArraySegment(Of Byte) = CompressBuffer(buffer, bufferManager, messageOffset)
				GZipMessageEncoderFactory.RaiseMessageReceived(result.Count)
				Return result
			End Function

			Public Overrides Overloads Function ReadMessage(ByVal stream As System.IO.Stream, ByVal maxSizeOfHeaders As Integer, ByVal contentType As String) As Message
				Dim gzStream As New GZipStream(stream, CompressionMode.Decompress, True)
				Return innerEncoder.ReadMessage(gzStream, maxSizeOfHeaders)
			End Function

			Public Overrides Overloads Sub WriteMessage(ByVal message As Message, ByVal stream As System.IO.Stream)
				Using gzStream As New GZipStream(stream, CompressionMode.Compress, True)
					innerEncoder.WriteMessage(message, gzStream)
				End Using

				' innerEncoder.WriteMessage(message, gzStream) depends on that it can flush data by flushing 
				' the stream passed in, but the implementation of GZipStream.Flush will not flush underlying
				' stream, so we need to flush here.
				stream.Flush()
			End Sub
		End Class
	End Class
End Namespace
