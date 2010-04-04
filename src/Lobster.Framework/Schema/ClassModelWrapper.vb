'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ClassModelWrapper

    Public Shared Function TableName(ByVal classModel As ClassModel) As String
        TableName = classModel.Name & "s"
    End Function

End Class
