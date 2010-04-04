'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Imports SQLDMO

Public Class Tables

    Public Shared Sub Build(ByVal classModel As ClassModelReader.ClassModel, _
        ByVal session As Session)

        Dim table As New SQLDMO.Table2
        table.Name = classModel.TableName

        'sort name asc

        Dim col As Collection = classModel.GetFieldModels().Collection

        For Each fieldModel As FieldModel In col
            AddField(fieldModel, table)
        Next

        Dim contextWrapper As New SessionContextWrapper(session)

        Dim db As SQLDMO._Database = contextWrapper.GetDatabase(1)

        Dim key As New SQLDMO.Key
        key.Type = SQLDMO.SQLDMO_KEY_TYPE.SQLDMOKey_Primary
        key.Clustered = True
        key.KeyColumns.Add(classModel.KeyName)

        table.Keys.Add(key)

        db.Tables.Add(table)

    End Sub

    Private Shared Sub AddField(ByVal propertyModel As FieldModel, _
        ByVal table As Table2)

        Dim column As Column2 = New SQLDMO.Column2

        With column

            .Name = propertyModel.Name
            .Datatype = PropertyModelWrapper.SqlDataType(propertyModel)
            .Length = 500
            .AllowNulls = False

            table.Columns.Add(column)

            If .Datatype = "decimal" Then
                If propertyModel.DataTypeId = lafDataType.lafPercent Then
                    .NumericScale = 4
                Else
                    .NumericScale = 2
                End If
            End If

        End With

    End Sub

End Class
