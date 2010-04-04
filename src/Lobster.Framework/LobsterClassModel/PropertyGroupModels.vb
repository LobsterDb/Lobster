'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class PropertyGroupModels

    Private mCollection As Collection

    Sub New()
        mCollection = New Collection()
    End Sub

    ReadOnly Property Collection() As Collection
        Get
            Collection = mCollection
        End Get
    End Property

    Public Function GetNew() As PropertyGroupModel
        Dim item As New PropertyGroupModel
        mCollection.Add(item)
        GetNew = item
    End Function

End Class
