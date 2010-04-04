'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class FormModelBuilder

    Public Function Build(ByVal compositeModel As ClassModelReader.ClassModel) As String

        Dim json As String

        json = "{FormTitle:'" & compositeModel.Name & "',ControlModels:["

        Dim needsComma As Boolean = False

        For Each fieldmodel As FieldModel In compositeModel.GetFieldModels().Collection
            If needsComma Then
                json &= ","
            Else
                needsComma = True
            End If
            json &= "{FieldName:'" & fieldmodel.Name & "', ControlLabel:'" & _
                fieldmodel.Name & "', ControlTypeName:'TextBox'}"
        Next

        json &= "]}"

        Return json

    End Function

End Class
