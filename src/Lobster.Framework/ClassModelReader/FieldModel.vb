'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class FieldModel

    Private _parentSet As FieldModels
    Private _stateGraph As JsonProxy

    Sub New(ByVal parentSet As FieldModels, ByVal stateGraph As JsonProxy)
        _parentSet = parentSet
        _stateGraph = stateGraph
    End Sub

    ReadOnly Property FieldModelId() As Guid
        Get

        End Get
    End Property

    ReadOnly Property ClassModelId() As Guid
        Get

        End Get
    End Property

    Property Name() As String
        Get
            Name = _stateGraph.GetString("Name")
        End Get
        Set(ByVal value As String)
            _stateGraph.SetString("Name", value)
        End Set
    End Property

    Property DataTypeId() As lafDataType
        Get
            DataTypeId = CType(_stateGraph.GetInteger("DataTypeId"), lafDataType)
        End Get
        Set(ByVal value As lafDataType)
            _stateGraph.SetInteger("DataTypeId", CType(value, Integer))
        End Set
    End Property

    Property DataTypeName() As String
        Get
            DataTypeName = "String"
        End Get
        Set(ByVal value As String)
            '
        End Set
    End Property

    Public Sub ToJson()

        Debug.Print("Name: " & Name)
        Debug.Print("DataTypeId: " & DataTypeId)

    End Sub

    Public Sub Commit()

        _parentSet.AddObject(_stateGraph)

    End Sub

End Class
