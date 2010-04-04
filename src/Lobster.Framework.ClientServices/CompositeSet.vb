'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Option Strict Off

Public Class CompositeSet

    Public Function GetObjectPickerData( _
    ByVal compositeModel As Lobster.Framework.ClassModelReader.ClassModel, _
    ByVal session As Session) As String

        Dim sql As String = "SELECT * FROM " & compositeModel.TableName & _
            " ORDER BY " & compositeModel.KeyName & " DESC;"

        Dim reader As LobsterReader = session.DatabaseConnection.GetReader(sql)

        Dim json As String = ReaderToJson(reader)

        Return "[" & json & "]"

    End Function

    Public Shared Function ReaderToJson(ByVal reader As LobsterReader) As String

        Dim json As String = ""
        Dim isFirstObject As Boolean = True

        Do While reader.Read()

            If isFirstObject = False Then json += ","
            isFirstObject = False

            json += "{"
            json += GetData_GetJsonBody(reader)
            json += "}"

        Loop

        Return json

    End Function

    Public Shared Function GetData_GetJsonBody(ByVal lreader As LobsterReader) As String

        Dim json As String = ""
        Dim isFirstProperty As Boolean = True

        Dim reader As SqlDataReader = lreader.GetReader()

        For x As Integer = 0 To (reader.FieldCount - 1)

            If isFirstProperty = False Then json += ","
            isFirstProperty = False

            Dim name As String = reader.GetName(x)
            Dim valRaw As Object = reader.GetValue(x)
            Dim val As String = ""

            If TypeOf valRaw Is DateTime Then
                val = piq(reader.GetDateTime(x).ToString("yyyy-MM-dd"))
            ElseIf TypeOf valRaw Is Short Then
                val = valRaw
            ElseIf TypeOf valRaw Is Int32 Then
                val = valRaw
            ElseIf TypeOf valRaw Is Decimal Then
                val = valRaw
            ElseIf TypeOf valRaw Is String Then
                val = piq(encodeJson(reader.GetValue(x).ToString))
            ElseIf TypeOf valRaw Is Guid Then
                val = piq(valRaw.ToString)
            ElseIf TypeOf valRaw Is Boolean Then
                If valRaw = True Then
                    val = "true"
                Else
                    val = "false"
                End If
            Else
                Err.Raise(999, "GetData_GetJsonBody", "Unexpected Type ")
            End If

            json &= piq(name) & ":" & val

            'If TypeOf reader.GetValue(x) Is DateTime Then
            'json &= piq(reader.GetName(x)) & ":" & piq(reader.GetDateTime(x).ToString("yyyy-MM-dd"))
            'Else
            'json &= piq(reader.GetName(x)) & ":" & piq(encodeJson(reader.GetValue(x).ToString))
            'End If

        Next x

        Return json

    End Function

    Public Shared Function piq(ByVal str As String) As String
        piq = """" & str & """"
    End Function

    Public Shared Function encodeJson(ByVal json As String) As String

        'escape double quotes
        encodeJson = Replace(json, """", "\""")

    End Function

End Class
