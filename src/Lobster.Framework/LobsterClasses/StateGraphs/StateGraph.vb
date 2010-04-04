'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class StateGraph

    Public Function GetState(ByVal compositeModel As ClassModelReader.ClassModel, _
                             ByVal key As String, _
                             ByVal session As Session) As String

        Dim keyCriteria As String

        If compositeModel.KeyType = lafDataType.lafGuid Then
            keyCriteria = "'" & key & "'"
        Else
            keyCriteria = key
        End If

        Dim reader As LobsterReader = session.DatabaseConnection.GetReader( _
            "SELECT * FROM " & compositeModel.TableName & _
            " WHERE " & compositeModel.KeyName & " = " & keyCriteria)

        reader.Read()

        Dim json As String = SerializeToJson(reader, compositeModel)

        reader.Close()

        For Each componentModel As ClassModelReader.ClassModel _
            In compositeModel.GetChildClassModels()

            reader = session.DatabaseConnection.GetReader( _
                "SELECT * FROM " & componentModel.TableName & " WHERE " & _
                compositeModel.KeyName & " = " & keyCriteria & ";")

            json &= ", " & componentModel.SetName & ":[" & _
                BuildComponentJson(componentModel, reader) & "]"

        Next

        Return json & "}"

    End Function

    Public Function BuildComponentJson(ByVal classModel As ClassModelReader.ClassModel, _
                                        ByVal reader As LobsterReader) As String

        Dim json As String = ""
        Dim needsComma As Boolean = False

        Do While reader.Read
            If needsComma Then
                json &= ", "
            Else
                needsComma = True
            End If
            json &= SerializeToJson(reader, classModel) & "}"
        Loop

        Return json

    End Function

    Public Function SerializeToJson(ByVal reader As LobsterReader, _
                                    ByVal classModel As ClassModelReader.ClassModel) _
                                    As String

        Dim jsonString As New JsonString()

        For Each field As FieldModel In classModel.GetFieldModels().Collection
            Select Case field.DataTypeId
                Case Is = lafDataType.lafBoolean
                    jsonString.AddBoolean(field.Name, reader.GetBoolean(field.Name))
                Case Is = lafDataType.lafGuid
                    jsonString.AddString(field.Name, reader.GetGuid(field.Name).ToString())
                Case Is = lafDataType.lafString
                    jsonString.AddString(field.Name, reader.GetString(field.Name))
                Case Is = lafDataType.lafInteger
                    jsonString.AddInteger(field.Name, reader.GetInteger(field.Name))
                Case Else
                    Err.Raise(11, , "serializetojson")
            End Select
        Next

        Return jsonString.GetJson1()

    End Function

    Public Sub ProcessComponentSet(ByVal stateGraph As JsonProxy, _
                                   ByVal compositeModelId As String, _
                                   ByVal classType As ObjectSource, _
                                   ByVal session As Session)

        Dim compositeModel As New ClassModelReader.ClassModel(compositeModelId, _
                                                              classType, _
                                                              session)

        For x As Integer = 0 To stateGraph.CollectionUpperBound(compositeModel.SetName)
            Dim stateObject As JsonProxy = stateGraph.getChildByIndex( _
                compositeModel.SetName, x)
            Debug.Print(stateObject.GetString("Name"))
        Next

    End Sub

End Class