Public Class ClassModel

    Private mName As String
    Private mPropertyPartitionModels As PropertyPartitionModels

    Sub New()
        mPropertyPartitionModels = New PropertyPartitionModels
    End Sub

    Property Name() As String
        Get
            Name = mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property

    ReadOnly Property PropertyPartitionModels() As PropertyPartitionModels
        Get
            PropertyPartitionModels = mPropertyPartitionModels
        End Get
    End Property

    ReadOnly Property SessionContext() As Session
        Get
            Dim con As New Session(1, 0, SessionContext.DevelopmentOrProductionEnum.Development)
            SessionContext = con
        End Get
    End Property

    ReadOnly Property IdName() As String
        Get
            IdName = mName & "Id"
        End Get
    End Property

    Public Sub Update()

        Dim pm As FieldModel = Me.PropertyPartitionModels.GetNew().PropertyGroupModels.GetNew().PropertyModels.GetNew()

        'Dim pm As PropertyModel = Me.PropertyModels().GetNew()
        pm.Name = Me.IdName
        pm.DataTypeId = lafDataType.lafInteger

        'Tables.Build(Me)

    End Sub

    Public Function GetAllPropertyModels() As Collection

        Dim col As New Collection

        For Each partition As PropertyPartitionModel In _
            Me.PropertyPartitionModels.Collection()

            For Each group As PropertyGroupModel In _
                partition.PropertyGroupModels.Collection()

                For Each prp As FieldModel In group.PropertyModels.Collection()
                    col.Add(prp)
                Next

            Next

        Next

        GetAllPropertyModels = col

    End Function

End Class
