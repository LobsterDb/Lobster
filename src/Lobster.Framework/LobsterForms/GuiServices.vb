'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class LobsterFormGuiServices

    Public Shared Function GetFormItemsJS(ByVal classModel As ClassModel) As String

        Dim js As New System.Text.StringBuilder
        Dim tabs As String = vbTab & vbTab
        Dim needsComma As Boolean = False

        With js
            .AppendLine(tabs & "var itemModels = [];")
            For Each fieldModel As FieldModel In classModel.GetAllPropertyModels()
                .AppendLine(tabs & "itemModels.push({")
                .AppendLine(tabs & vbTab & "fieldLabel: '" & fieldModel.Name & "',")
                .AppendLine(tabs & vbTab & "name: '" & fieldModel.Name & "'")
                .AppendLine(tabs & "});")
            Next
        End With

        Return js.ToString()

    End Function


End Class
