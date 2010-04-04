'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class StateArray

    Private _scope As String
    Private _scriptObject As MSScriptControl.ScriptControl

    Sub New(ByVal scope As String, _
            ByVal scriptObject As MSScriptControl.ScriptControl)
        _scope = scope
        _scriptObject = scriptObject
    End Sub

    Private Function GetPath() As String
        Return "root" & _scope
    End Function

    Public Function AddObject() As StateObject
        Dim index As Integer = CType(_scriptObject.Eval( _
        Me.GetPath() & ".push({})"), Integer) - 1
        Dim stateobject As New StateObject(_scriptObject, _scope & "[" & _
                                           index & "]")
        Return stateobject
    End Function

    Public Function GetLength() As Integer
        Return CType(_scriptObject.Eval(Me.GetPath & ".length"), Integer)
    End Function

    Public Function GetObject(ByVal index As Integer) As StateObject
        Dim relativePath As String = _scope & "[" & index & "]"
        Return New StateObject(_scriptObject, relativePath)
    End Function

End Class
