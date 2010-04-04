'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class StateGraphBroker
    Implements IServicesProxy

    Private _request As JsonProxy

    Sub New(ByVal request As JsonProxy)

        _request = request

    End Sub

    Public Function RouteCall() As JsonString Implements IServicesProxy.RouteCall

        Dim methodName As String = _request.GetString("MethodName")
        Dim arguments As JsonProxy = _request.GetDataObject("Arguments")
        Dim session As New Session(1)

        'Dim classTypeId As ObjectSource = CType(arguments.GetInteger("ObjectSource"),  _
        'ObjectSource)

        Dim compositeModelId As String = arguments.GetString("CompositeModelId")
        Dim compositeModel As New ClassModelReader.ClassModel(compositeModelId, _
            ObjectSource.Application, session)

        'Dim lobsterObject As New LobsterObjectClientServices(ClassModel)

        Dim returnObject As New JsonString()
        Dim returnValue As String

        Dim stateGraph As New StateGraph()

        Select Case methodName
            Case Is = "GetState"
                returnValue = stateGraph.GetState(compositeModel, _
                                                  arguments.GetString("Key"), _
                                                  session)
            Case Is = "Persist"

            Case Else
                Throw New Exception()
        End Select

        returnObject.AddJson("ReturnValue", returnValue)

        Return returnObject

    End Function

    Private Shared Function GetState(ByVal lobsterObject As LobsterObjectClientServices, _
        ByVal classModel As ClassModel, ByVal arguments As JsonProxy) As String

        Dim key As Integer = arguments.GetInteger("Key")
        'Dim state As String = lobsterObject.GetState(key)

        'Return state
        Return ""
    End Function

End Class
