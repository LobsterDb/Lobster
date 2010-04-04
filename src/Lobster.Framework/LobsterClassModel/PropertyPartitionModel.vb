'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class PropertyPartitionModel

    Private mName As String
    Private mPropertyGroupModels As PropertyGroupModels

    Sub New()
        mPropertyGroupModels = New PropertyGroupModels
    End Sub

    ReadOnly Property PropertyGroupModels() As PropertyGroupModels
        Get
            PropertyGroupModels = mPropertyGroupModels
        End Get
    End Property

    Property Name() As String
        Get
            Name = mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

End Class
