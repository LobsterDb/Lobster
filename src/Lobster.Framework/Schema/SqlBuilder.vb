'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class SqlBuilder

    Public Shared Function GetInsertSql( _
    ByVal classModel As ClassModelReader.ClassModel) As String

        Dim pm As FieldModel

        Dim sqlFields As String = ""
        Dim sqlParameters As String = ""

        Dim needsComma As Boolean = False

        For Each pm In classModel.GetFieldModels().Collection
            If needsComma = True Then
                sqlFields += ", "
                sqlParameters += ", "
            Else
                needsComma = True
            End If
            sqlFields += "[" & pm.Name & "]"
            sqlParameters += "@" & pm.Name

        Next

        Dim tableName As String = classModel.TableName

        Dim sql As String = "INSERT INTO " & tableName & _
            " (" & sqlFields & ") VALUES (" & sqlParameters & ");"

        Return sql

    End Function

End Class
