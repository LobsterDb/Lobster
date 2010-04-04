'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class Component
    Inherits LobsterObject

    Sub New(ByVal classModel As ClassModelReader.ClassModel, _
            ByVal stateObject As StateObject)
        MyBase.New(classModel, stateObject)
    End Sub

    Public Overloads Sub Commit()

    End Sub

    Public Sub Persist(ByVal command As SqlCommand, _
                       ByVal connection As LobsterConnection, _
                       ByVal compositeKeyName As String, _
                       ByVal compositeKey As String)

        If _classModel.KeyType = lafDataType.lafGuid Then
            _stateObject.SetString(compositeKeyName, compositeKey)
        End If

        Dim key As String
        If _classModel.KeyType = lafDataType.lafGuid Then
            key = Guid.NewGuid().ToString()
            _stateObject.SetString(_classModel.KeyName, key)
        Else
            _stateObject.SetInteger(_classModel.KeyName, _
                                  connection.GetNextID(_classModel.KeyName, _
                                                       _classModel.TableName))
            key = Me.GetInteger(_classModel.KeyName).ToString()
        End If

        Me.PopulateInsertParameters(command)
        command.ExecuteNonQuery()
        command.Parameters.Clear()

    End Sub

End Class
