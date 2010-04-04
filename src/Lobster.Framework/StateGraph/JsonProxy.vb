'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Option Strict Off

Public Class JsonProxy

    Public js As MSScriptControl.ScriptControl

    Private _cursorPath As String = ""
    Private _pointer As String = ""

    Private mScope As String = ""

    Sub New(ByVal json As String)
        js = New MSScriptControl.ScriptControl
        js.Language = "jscript"

        'don't need to call eval because executestatement eval's the json
        js.ExecuteStatement("var root = (" & json & ");")
    End Sub

    Private Sub New(ByVal scriptControl As MSScriptControl.ScriptControl, ByVal scope As String)
        js = scriptControl
        mScope = scope
    End Sub

    Public Sub Execute(ByVal statement As String)
        js.ExecuteStatement(statement)
    End Sub

    Public Function Eval(ByVal expression As String) As Object
        Return (js.Eval(expression))
    End Function



    Public Sub moveCursor(ByVal path As String)
        Err.Raise(9999, , "moveCursor deprecated")
    End Sub

    Public Sub resetCursor()
        Err.Raise(999, , "resetCursor deprecated")
    End Sub

    Public Function getChildByIndex(ByVal collectionName As String, _
        ByVal index As Integer) As JsonProxy
        Dim childPath As String = mScope & "." & collectionName & "[" & index & "]"
        Dim dataObject As New JsonProxy(Me.js, childPath)
        Return dataObject
    End Function

    Public Function getChildByKey(ByVal collectionName As String, _
        ByVal keyName As String, ByVal keyValue As String) As JsonProxy
        'method is here for clarity in calling code
        Return getChildByProperty(collectionName, keyName, keyValue)
    End Function

    Public Function getChildByProperty(ByVal collectionName As String, _
        ByVal propertyName As String, ByVal value As String) As JsonProxy

        For x As Integer = 0 To Me.CollectionUpperBound(collectionName)
            Dim child As JsonProxy = Me.getChildByIndex(collectionName, x)
            If child.GetString(propertyName) = value Then
                Return child
            End If
        Next

        Err.Raise(999, , "getChildByProperty: " & collectionName & _
                "." & propertyName & " = " & value & " not found.")

    End Function

    Public Function GetBoolean(ByVal propertyName As String) As Boolean
        Return Convert.ToBoolean(Item(propertyName))
    End Function

    Public Function GetString(ByVal name As String) As String
        Return Convert.ToString(Item(name))
    End Function

    Public Function GetInteger(ByVal name As String) As Integer
        Return CType(Item(name), Integer)
    End Function

    Public Function GetDataObject(ByVal name As String) As JsonProxy
        Dim obj As New JsonProxy(Me.js, mScope & "." & name)
        Return obj
    End Function

    Public Function PropertyExists(ByVal name As String) As Boolean
        Return js.Eval(Me.getObject() & "['" & name & "'] != undefined")
    End Function

    Public Function CollectionUpperBound(ByVal collectionName As String) As Integer
        Dim col As Object = Me.Item(collectionName)
        Return (col.length - 1)
    End Function

    Public Function Item(ByVal name As String) As Object

        'Dim value As Object = js.Eval(Me.getObject() & name)

        Dim str As String = Me.getObject()
        str = str & "['" & Replace(name, "'", "\'") & "']"

        Dim value As Object = js.Eval(str)

        'Dim value As Object = js.Eval(Me.getObject() & name)
        'note: if value = 0, isnothing(0) returns false which is desired, but value=nothing returns true; not sure why equality evaluates differently

        If IsNothing(value) Then
            Err.Raise(9, "root.item", "property " & name & " doesn't exist.")
        Else
            Return value
        End If

    End Function
    Private Function getObject() As String
        Return "root" & mScope
    End Function
    Public Sub SetProperty(ByVal sName As String, ByVal vValue As String)

        'if passing in a string, pass it in single quotes
        'UPGRADE_WARNING: Couldn't resolve default property of object vValue. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
        js.ExecuteStatement((Me.getObject() & "['" & sName & "']=" & vValue))

    End Sub

    Public Sub SetInteger(ByVal fieldName As String, ByVal value As Integer)

        js.ExecuteStatement((Me.getObject() & "['" & fieldName & "']=" & value))

    End Sub

    Public Sub SetString(ByVal fieldName As String, ByVal value As String)
        js.ExecuteStatement(Me.getObject() & "['" & fieldName & "'] = '" & value & "'")
    End Sub

    Public Sub SetWithoutQuotes(ByVal propertyName As String, _
        ByVal value As String)
        js.ExecuteStatement((Me.getObject() & "['" & propertyName & "']=" & value))
    End Sub

    Public Sub SetBoolean(ByVal propertyName As String, ByVal value As Boolean)
        If value = True Then
            SetWithoutQuotes(propertyName, "true")
        Else
            SetWithoutQuotes(propertyName, "false")
        End If
    End Sub

    Public Function CopyObject()

        'Dim str As String
        '
        'str = "function copyObject(source){" & vbCrLf
        'str &= "var copy = {};" & vbCrLf
        'str &= "for (prop in source){" & vbCrLf
        'str &= "if (source[prop] && typeof source[prop] == 'object') {" & vbCrLf
        'str &= "//copy[prop] = copyObject(source[prop]);
        '} else {
        '  copy[prop] = source[prop];
        ' }
        '}
        '
        'return copy;
        '}

    End Function

End Class