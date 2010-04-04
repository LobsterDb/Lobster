'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ClassModelReader
    Implements IServicesProxy

    Private _request As JsonProxy

    Sub New(ByVal request As JsonProxy)
        _request = request
    End Sub

    Public Function RouteCall() As JsonString Implements IServicesProxy.RouteCall

        Dim methodName As String = _request.GetString("MethodName")
        Dim arguments As JsonProxy = _request.GetDataObject("Arguments")
        Dim session As New Session(1)

        Dim returnObject As New JsonString()
        Dim returnValue As String

        Select Case methodName
            Case Is = "GetState"
                returnValue = RouteGetState(arguments, session)
            Case Is = "GetListOfChildClassModels"
                returnValue = RouteGetListOfChildClassModels(arguments, session)
            Case Else
                Throw New Exception()
        End Select

        returnObject.AddJson("ReturnValue", returnValue)

        Return returnObject

    End Function

    Public Function RouteGetState(ByVal arguments As JsonProxy, ByVal session As Session) _
        As String

        Dim classModelId As String = arguments.GetString("ClassModelId")
        Dim classModelSourceString As String = arguments.GetString("ClassModelSource")
        Dim classModelSource As ObjectSource

        If classModelSourceString = "Framework" Then
            classModelSource = Framework.ObjectSource.Framework
        Else
            classModelSource = Framework.ObjectSource.Application
        End If

        Dim stateGraphLoader As New  _
            Lobster.Framework.ClassModelReader.Bootstrap.StateGraphLoader()

        Dim json As String = stateGraphLoader.GetStateGraphString(classModelId, _
                                                                  classModelSource, _
                                                                  session)
        Return json

    End Function

    Public Function RouteGetListOfChildClassModels( _
        ByVal arguments As JsonProxy, ByVal session As Session) As String

        Dim classModelId As String = arguments.GetString("ClassModelId")
        Dim classModelSourceString As String = arguments.GetString("ClassModelSource")
        Dim classModelSource As ObjectSource

        If classModelSourceString = "Framework" Then
            classModelSource = Framework.ObjectSource.Framework
        Else
            classModelSource = Framework.ObjectSource.Application
        End If

        Dim classModel As New Lobster.Framework.ClassModelReader.ClassModel( _
            classModelId, classModelSource, session)

        Return classModel.GetListOfChildClassModels

    End Function

End Class

