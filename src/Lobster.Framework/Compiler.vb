'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Imports System.Text

Public Class Compiler

    Public Shared Sub CompileToFile(ByVal classModel As ClassModel)

        Dim path As String
        path = "C:\Data\Lobster\Builder.DomainModel.Test\Generated\" & _
            classModel.Name & ".vb"

        Dim code As String = Compile(classModel)

        My.Computer.FileSystem.WriteAllText(path, code, False)

    End Sub

    Public Shared Function Compile(ByVal classModel As ClassModel) As String

        Dim sb As New System.Text.StringBuilder()

        sb.AppendLine("Public Class " & classModel.Name)

        sb.Append(vbCrLf)

        Dim propertyModels As Collection = classModel.GetAllPropertyModels()
        Dim pm As FieldModel

        For Each pm In propertyModels
            sb.AppendLine(vbTab & "Private m" & pm.Name & " As " & pm.DataTypeName)
        Next

        sb.Append(vbCrLf)

        For Each pm In propertyModels
            With sb
                .AppendLine(vbTab & "Public Property " & pm.Name & "() As " & _
                    pm.DataTypeName)
                .AppendLine(vbTab & vbTab & "Get")
                .AppendLine(vbTab & vbTab & vbTab & pm.Name & " = m" & pm.Name)
                .AppendLine(vbTab & vbTab & "End Get")
                .AppendLine(vbTab & vbTab & vbTab & "Set(ByVal value As " & _
                    pm.DataTypeName & ")")
                .AppendLine(vbTab & vbTab & vbTab & "m" & pm.Name & " = value")
                .AppendLine(vbTab & vbTab & "End Set")
                .AppendLine(vbTab & "End Property")
                .Append(vbCrLf)
            End With
        Next

        sb.AppendLine("End Class")

        Return sb.ToString

    End Function

    Private Shared Sub BuildUpdate(ByVal classModel As ClassModel, _
        ByVal propertyModels As Collection, ByRef sb As StringBuilder)

        Dim tableName As String = ClassModelWrapper.TableName(classModel)
        Dim sessionContextWrapper As New SessionContextWrapper(classModel.SessionContext)

        Dim dbName As String = sessionContextWrapper.DatabaseName()


        'sb.AppendLine(vbTab & vbTab & "Dim sql As String = " & piq(sql) & vbCrLf)

        sb.AppendLine(vbTab & vbTab & "Dim dbName as String = " & piq(dbName) & vbCrLf)

        sb.AppendLine(vbTab & vbTab & "Dim pkey as Integer = db.GetNextID(" & piq(classModel.IdName) & _
            ", " & piq(tableName) & ", dbName)" & vbCrLf)

        sb.AppendLine(vbTab & vbTab & "Dim cn as SqlConnection = db.GetConnection(dbName)" & vbCrLf)

        sb.AppendLine(vbTab & vbTab & "Dim command As New SqlCommand(sql, cn)")

        sb.AppendLine(vbTab & vbTab & "With command.Parameters")

        

        sb.AppendLine(vbTab & vbTab & "End With" & vbCrLf)

        sb.AppendLine(vbTab & vbTab & "command.ExecuteNonQuery()")
        sb.AppendLine(vbTab & vbTab & "command = Nothing" & vbCrLf)
        sb.AppendLine(vbTab & vbTab & "cn.Close()" & vbCrLf)

        sb.AppendLine(vbTab & "End Function" & vbCrLf)

    End Sub

    Private Shared Function piq(ByVal str As String) As String
        piq = """" & str & """"
    End Function

End Class
