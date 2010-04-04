'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class FieldModels

    Private _stateGraph As JsonProxy

    Sub New(ByVal stateGraph As JsonProxy)
        _stateGraph = stateGraph
    End Sub

    ReadOnly Property Collection() As Collection
        Get
            Dim col As New Collection
            For x As Integer = 0 To _stateGraph.CollectionUpperBound("FieldModels")
                Dim stateGraph As JsonProxy = _stateGraph.getChildByIndex("FieldModels", x)
                Dim fieldModel As New FieldModel(Me, stateGraph)
                col.Add(fieldModel)
            Next x
            Return col
        End Get
    End Property

    Public Function GetNew() As FieldModel

        Dim stateGraph As New JsonProxy("{}")

        Dim pm As New FieldModel(Me, stateGraph)
        '_collection.Add(pm)
        'GetNew = pm
    End Function

    Public Sub AddObject(ByVal stateGraph As JsonProxy)




    End Sub

End Class
