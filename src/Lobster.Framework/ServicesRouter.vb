'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ServicesRouter

    Sub New()
    End Sub

    Public Sub teste()
        Dim str As String = "{'ClassName':'StateGraph','MethodName':'GetState','Arguments':{'CompositeModelId':'6CFDAAD3-9B79-491C-9AF1-5B686C1A627E','Key':1}}"
        Debug.Print(RouteCall(str))
    End Sub

    Public Function RouteCall(ByVal requestString As String) As String

        Dim request As New JsonProxy(requestString)
        Dim proxy As IServicesProxy

        Select Case request.GetString("ClassName")
            Case Is = "StateGraph"
                proxy = New StateGraphBroker(request)
            Case Else
                Throw New Exception()
        End Select

        Dim returnObject As JsonString = proxy.RouteCall()
        Dim returnString As String = returnObject.GetJson()

        Return returnString

    End Function

End Class