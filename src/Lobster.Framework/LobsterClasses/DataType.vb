'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class DataType

    Private _dataTypeId As Integer
    Private _name As String

    ReadOnly Property Name() As String
        Get
            Name = _name
        End Get
    End Property

    ReadOnly Property DataTypeId() As Integer
        Get
            DataTypeId = _dataTypeId
        End Get
    End Property

End Class