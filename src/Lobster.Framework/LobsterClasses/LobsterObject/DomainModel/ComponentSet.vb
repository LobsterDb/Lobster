'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class ComponentSet

    Public _componentModel As ClassModelReader.ClassModel
    Public _parent As Composite
    Private _stateArray As StateArray

    Sub New(ByVal componentModel As ClassModelReader.ClassModel, _
            ByVal stateArray As StateArray, ByVal parent As Composite)
        _componentModel = componentModel
        _stateArray = stateArray
    End Sub

    Public Function GetNew() As Component
        'Dim component As New Component()
        Dim stateObject = _stateArray.AddObject()
        Dim component As New Component(_componentModel, stateObject)
        Return component
    End Function

    Public Function GetCollection() As Collection
        Dim collection As New Collection
        For i As Integer = 0 To _stateArray.GetLength - 1
            collection.Add(New Component(_componentModel, _
                                         _stateArray.GetObject(i)))
        Next
        Return collection
    End Function

    Friend Sub Persist(ByVal connection As LobsterConnection, _
                       ByVal compositeKeyName As String, _
                       ByVal compositeKey As String)
        Dim command As New SqlCommand(SqlBuilder.GetInsertSql(_componentModel), _
                                      connection.Connection)
        For Each component As Component In Me.GetCollection()
            component.Persist(command, connection, compositeKeyName, compositeKey)
        Next
    End Sub

End Class
