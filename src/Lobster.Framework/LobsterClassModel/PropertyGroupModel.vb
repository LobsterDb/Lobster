'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class PropertyGroupModel

    Private mName As String
    Private mPropertyModels As FieldModels

    Sub New()
        'mPropertyModels = New FieldModels
    End Sub

    ReadOnly Property PropertyModels() As FieldModels
        Get
            PropertyModels = mPropertyModels
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
