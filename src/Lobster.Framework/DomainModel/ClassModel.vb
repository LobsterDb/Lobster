'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Namespace DomainModel

    Public Class ClassModel
        Inherits Composite

        Sub New(ByVal session As Session)
            MyBase.New(CompositeModelFactory.GetClassModelReader( _
                       DomainModel.ClassModel.ClassModelId, _
                       ObjectSource.Framework, _
                       session), session)
        End Sub

        Shared ReadOnly Property ClassModelId() As String
            Get
                Return "26761d27-392c-4466-ab5d-7849c7c91474"
            End Get
        End Property

        Property Name() As String
            Get
                Return GetString("Name")
            End Get
            Set(ByVal value As String)
                SetString("Name", value)
            End Set
        End Property

        Property SetNameOverload() As String
            Get
                Return GetString("SetNameOverload")
            End Get
            Set(ByVal value As String)
                SetString("SetNameOverload", value)
            End Set
        End Property

        Public Overloads Function Persist() As String
            SetBoolean("IsComposite", True)
            SetString("ParentClassModelId", "00000000-0000-0000-0000-000000000000")
            SetString("CompositeClassModelId", "00000000-0000-0000-0000-000000000000")
            Return MyBase.Persist()
        End Function

    End Class

End Namespace