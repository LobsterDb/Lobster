'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Namespace ClassModelReader

    Public Class ClassModel

        Private _classModelId As String
        Private _objectSource As ObjectSource
        Private _databaseConnection As LobsterConnection
        Private _stateGraph As JsonProxy
        Private _childClassModels As Collection

        Sub New(ByVal classModelID As String, _
                ByVal classModelSource As ObjectSource, _
                ByVal session As Session)

            _objectSource = classModelSource

            If _objectSource = Framework.ObjectSource.Framework Then
                _databaseConnection = New FrameworkConnection
            Else
                _databaseConnection = session.DatabaseConnection
            End If

            _stateGraph = New Bootstrap.StateGraphLoader().GetStateGraph( _
                classModelID, classModelSource, session)

            Dim reader As LobsterReader = _databaseConnection.GetReader( _
                "SELECT * FROM ClassModels where ParentClassModelId = '" & _
                Me.ClassModelId & "'")

            _childClassModels = New Collection

            Do While reader.Read()
                _childClassModels.Add(New ClassModelReader.ClassModel( _
                                  reader.GetGuid("ClassModelId").ToString, _
                                  ObjectSource, _
                                  session))
            Loop

            reader.Close()

        End Sub

        Public Function GetChildClassModels() As Collection
            Return _childClassModels
        End Function

        Public Function GetListOfChildClassModels() As String
            Dim json As String = "["
            Dim needsComma As Boolean
            For Each componentModel As ClassModelReader.ClassModel _
                In _childClassModels
                If needsComma Then
                    json &= ","
                Else
                    needsComma = True
                End If
                json &= "'" & componentModel.ClassModelId & "'"
            Next
            Return json & "]"
        End Function

        Public Function GetFieldModels() As FieldModels

            Dim fieldModels As New FieldModels(_stateGraph)
            GetFieldModels = fieldModels

        End Function

        ReadOnly Property ClassModelId() As String
            Get
                ClassModelId = _stateGraph.GetString("ClassModelId")
            End Get
        End Property

        ReadOnly Property ObjectSource() As ObjectSource
            Get
                Return _objectSource
            End Get
        End Property

        ReadOnly Property KeyName() As String
            Get
                KeyName = Name & "Id"
            End Get
        End Property

        ReadOnly Property KeyType() As lafDataType
            Get
                If Me.ClassModelId = DomainModel.ClassModel.ClassModelId Or Me.ClassModelId = "c26f63b3-df37-49d6-a238-aa04009dabb1" Then
                    Return lafDataType.lafGuid
                Else
                    Return lafDataType.lafInteger
                End If
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

        Property SetNameOverload() As String
            Get
                SetNameOverload = _stateGraph.GetString("SetNameOverload")
            End Get
            Set(ByVal value As String)
                _stateGraph.SetString("SetNameOverload", value)
            End Set
        End Property

        ReadOnly Property SetName() As String
            Get
                If SetNameOverload = "" Then
                    SetName = Name & "s"
                Else
                    SetName = SetNameOverload
                End If
            End Get
        End Property

        ReadOnly Property TableName() As String
            Get
                If SetName = "FieldModels" Then
                    TableName = "ClassModels_FieldModels"
                Else
                    TableName = SetName
                End If
            End Get
        End Property

    End Class

End Namespace
