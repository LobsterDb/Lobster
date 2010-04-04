'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ClassModels

    Public Shared Function GetClassModelId(ByVal name As String) As String

        Select Case name
            Case Is = "ClassModel"
                Return "26761d27-392c-4466-ab5d-7849c7c91474"
        End Select

    End Function

End Class
