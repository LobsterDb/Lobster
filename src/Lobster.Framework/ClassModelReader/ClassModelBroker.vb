'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ClassModelBroker
    Implements IServicesProxy

    Private _request As JsonProxy

    Sub New(ByVal request As JsonProxy)
        _request = request
    End Sub

    Public Function RouteCall() As JsonString Implements IServicesProxy.RouteCall

        Dim methodName As String = _request.GetString("MethodName")
        Dim arguments As JsonProxy = _request.GetDataObject("Arguments")
        Dim session As New Session(1)

        Dim compositeModelId As String = arguments.GetString("CompositeModelId")
        Dim compositeModelSource As String = arguments.GetString("CompositeModelSource")

        Dim objectSource As ObjectSource

        If compositeModelSource = "Framework" Then
            objectSource = Framework.ObjectSource.Framework
        Else
            objectSource = Framework.ObjectSource.Application
        End If

        Dim compositeModel As New ClassModelReader.ClassModel( _
            compositeModelId, objectSource, session)

        Dim returnObject As New JsonString()
        Dim returnValue As String

        Select Case methodName
            Case Is = "BuildFormModel"
                returnValue = New FormModelBuilder().Build(compositeModel)
            Case Else
                Throw New Exception()
        End Select

        returnObject.AddJson("ReturnValue", returnValue)

        Return returnObject

    End Function

End Class
