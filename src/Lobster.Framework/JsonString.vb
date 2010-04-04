'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Imports Microsoft.VisualBasic

Public Class JsonString

    Private mJson As String
    Private mNeedsComma As Boolean

    Sub New()
        mJson = "{"
    End Sub

    Public Sub AddCommaIfNeeded()
        If mNeedsComma Then
            mJson += ", "
        Else
            mNeedsComma = True
        End If
    End Sub

    Public Sub AddString(ByVal name As String, ByVal value As String)
        AddCommaIfNeeded()
        Dim escapedString As String = Replace(value, "'", "\'")
        escapedString = Replace(escapedString, vbCrLf, "\n")
        escapedString = Replace(escapedString, vbTab, "\t")
        mJson += "'" & name & "': '" & escapedString & "'"
    End Sub

    Public Sub AddInteger(ByVal propertyName As String, ByVal value As Integer)
        AddCommaIfNeeded()
        mJson += "'" & propertyName & "': " & value
    End Sub

    Public Sub AddJson(ByVal name As String, ByVal value As String)
        AddCommaIfNeeded()
        mJson += "'" & name & "': " & value
    End Sub

    Public Sub AddBoolean(ByVal name As String, ByVal value As Boolean)
        AddCommaIfNeeded()
        If value = True Then
            mJson &= "'" & name & "': true"
        Else
            mJson &= "'" & name & "': false"
        End If
    End Sub

    Public Function GetJson() As String
        Return mJson & "}"
    End Function

    Public Function GetJson1() As String
        Return mJson
    End Function

End Class
