'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class BootstrapFieldModel

    Private _name As String
    Private _dataTypeId As lafDataType

    Sub New(ByVal name As String, ByVal dataTypeId As lafDataType)
        _name = name
        _dataTypeId = dataTypeId
    End Sub

    ReadOnly Property Name() As String
        Get
            Return _name
        End Get
    End Property

    ReadOnly Property DataTypeId() As lafDataType
        Get
            Return _dataTypeId
        End Get
    End Property

End Class
