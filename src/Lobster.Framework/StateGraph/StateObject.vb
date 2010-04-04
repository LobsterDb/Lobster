'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class StateObject

    Private _scriptObject As MSScriptControl.ScriptControl
    Private _scope As String = ""

    Sub New()
        Me.New("{}")
    End Sub

    Sub New(ByVal json As String)
        _scriptObject = New MSScriptControl.ScriptControl
        _scriptObject.Language = "jscript"
        _scriptObject.ExecuteStatement("var root = (" & json & ");")
    End Sub

    Sub New(ByVal scriptObject As MSScriptControl.ScriptControl, _
            ByVal scope As String)
        _scriptObject = scriptObject
        _scope = scope
    End Sub

    Private Function getObject() As String
        Return "root" & _scope
    End Function

    Public Function Item(ByVal name As String) As Object

        'Dim value As Object = js.Eval(Me.getObject() & name)

        Dim str As String = Me.getObject()
        str = str & "['" & Replace(name, "'", "\'") & "']"

        Dim value As Object = _scriptObject.Eval(str)

        'Dim value As Object = js.Eval(Me.getObject() & name)
        'note: if value = 0, isnothing(0) returns false which is desired, but value=nothing returns true; not sure why equality evaluates differently

        If IsNothing(value) Then
            Err.Raise(9, "root.item", "property " & name & " doesn't exist.")
        Else
            Return value
        End If

    End Function

    Public Function GetBoolean(ByVal propertyName As String) As Boolean
        Return Convert.ToBoolean(Item(propertyName))
    End Function

    Public Sub SetBoolean(ByVal propertyName As String, ByVal value As Boolean)
        If value = True Then
            SetWithoutQuotes(propertyName, "true")
        Else
            SetWithoutQuotes(propertyName, "false")
        End If
    End Sub

    Public Function GetString(ByVal name As String) As String
        Return Convert.ToString(Item(name))
    End Function

    Public Sub SetString(ByVal fieldName As String, ByVal value As String)
        _scriptObject.ExecuteStatement(Me.getObject() & "['" & fieldName & "'] = '" & value & "'")
    End Sub

    Public Function GetInteger(ByVal name As String) As Integer
        'think convert.toInt32 would do the same thing
        Return CType(Item(name), Integer)
    End Function

    Public Sub SetInteger(ByVal fieldName As String, ByVal value As Integer)
        _scriptObject.ExecuteStatement((Me.getObject() & "['" & fieldName & "']=" & value))
    End Sub

    Public Sub SetWithoutQuotes(ByVal propertyName As String, _
                                ByVal value As String)
        _scriptObject.ExecuteStatement((Me.getObject() & "['" & propertyName & "']=" & value))
    End Sub

    Public Function AddArray(ByVal name As String) As StateArray
        _scriptObject.ExecuteStatement((Me.getObject() & "['" & name & "'] = [];"))
        Dim stateArray As New StateArray(_scope & "." & name, _scriptObject)
        Return stateArray
    End Function

    Public Function GetArray(ByVal name As String) As StateArray
        Return New StateArray(_scope & "." & name, _scriptObject)
    End Function

End Class
