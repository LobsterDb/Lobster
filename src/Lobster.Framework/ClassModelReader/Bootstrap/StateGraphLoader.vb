'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Namespace ClassModelReader.Bootstrap

    Public Class StateGraphLoader

        Public Function GetStateGraph(ByVal classModelId As String, _
                                      ByVal classModelSource As ObjectSource, _
                                      ByVal session As Session) As JsonProxy

            Return New JsonProxy(Me.GetStateGraphString(classModelId, _
                                                        classModelSource, _
                                                        session))
        End Function

        Public Function GetStateGraphString(ByVal classModelId As String, _
                                            ByVal classModelSource As ObjectSource, _
                                            ByVal session As Session) _
                                            As String

            Dim connection As LobsterConnection

            If classModelSource = Framework.ObjectSource.Framework Then
                connection = New FrameworkConnection
            Else
                connection = session.DatabaseConnection
            End If

            Dim keyName As String = ""

            Dim classModelModelId As String = "26761d27-392c-4466-ab5d-7849c7c91474"
            Dim fieldModelModelId As String = "c26f63b3-df37-49d6-a238-aa04009dabb1"

            Dim frameworkConnection As New FrameworkConnection()

            Dim classModel As New ClassModel(classModelModelId, frameworkConnection)

            Dim headerSql As String = "SELECT * FROM ClassModels WHERE " & _
                "ClassModelID = '" & classModelId & "';"

            Dim Reader As LobsterReader = connection.GetReader(headerSql)

            Reader.Read()

            Dim stateGraph As New JsonString()

            Me.PopulateJsonString(stateGraph, classModel, Reader)

            Reader.Close()

            Dim fieldModelClassModel As New ClassModel(fieldModelModelId, _
                                                       frameworkConnection)

            Reader = connection.GetReader( _
                "SELECT * FROM ClassModels_FieldModels " & _
                " WHERE ClassModelID = '" & classModelId & "';")

            Dim fieldsArray As String = "["
            Dim needsComma As Boolean = False

            While Reader.Read()
                If needsComma Then
                    fieldsArray += ","
                Else
                    needsComma = True
                End If
                Dim js As New JsonString()
                Me.PopulateJsonString(js, fieldModelClassModel, Reader)
                fieldsArray += js.GetJson()
            End While

            fieldsArray += "]"
            stateGraph.AddJson("FieldModels", fieldsArray)
            Reader.Close()

            Return stateGraph.GetJson()

        End Function

        'Private Function BuildArray(ByVal componentData As LobsterReader)
        '
        'End Function

        Private Sub PopulateJsonString(ByRef jsonString As JsonString, _
            ByVal classModel As ClassModel, ByVal dataReader As LobsterReader)

            'jsonString.AddString(classModel.KeyName, _
            '    dataReader.GetGuid(classModel.KeyName).ToString)

            For Each fieldModel As BootstrapFieldModel In classModel.GetFieldModels()
                Select Case fieldModel.DataTypeId
                    Case Is = lafDataType.lafString
                        jsonString.AddString(fieldModel.Name, dataReader.GetString(fieldModel.Name))
                    Case Is = lafDataType.lafInteger
                        jsonString.AddInteger(fieldModel.Name, dataReader.GetInteger(fieldModel.Name))
                    Case Is = lafDataType.lafGuid
                        jsonString.AddString(fieldModel.Name, dataReader.GetGuid(fieldModel.Name).ToString)
                    Case Is = lafDataType.lafBoolean
                        jsonString.AddBoolean(fieldModel.Name, dataReader.GetBoolean(fieldModel.Name))
                    Case Else
                        Throw New Exception("BuildJsonString.CaseElse")
                End Select
            Next

        End Sub

    End Class

End Namespace