'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class Composite
    Inherits LobsterObject

    Private _session As Session

    Sub New(ByVal compositeModel As ClassModelReader.ClassModel, _
            ByVal stateObject As StateObject, _
            ByVal session As Session)
        _classModel = compositeModel
        _stateObject = stateObject
        _session = session
    End Sub

    Sub New(ByVal classModel As ClassModelReader.ClassModel)
        _classModel = classModel
    End Sub

    Sub New(ByVal compositeModel As ClassModelReader.ClassModel, _
            ByVal session As Session)
        MyBase.New(compositeModel, New StateObject("{'FieldModels':[]}"))
        _session = session
    End Sub

    Sub New(ByVal key As String, ByVal classModelId As String, _
            ByVal objectSource As ObjectSource, ByVal session As Session)

        _classModel = CompositeModelFactory.GetClassModelReader(classModelId, _
                                                               objectSource, _
                                                               session)
        _stateObject = New StateObject( _
            New StateGraph().GetState(_classModel, key, session))

    End Sub

    Public Function GetChildSet(ByVal classModelId As String) As ComponentSet

        Return GetChildSet( _
            CType(_classModel.GetChildClassModels(1), ClassModelReader.ClassModel))

    End Function

    Public Function GetChildSet( _
        ByVal childClassModel As ClassModelReader.ClassModel) As ComponentSet

        Return New ComponentSet(childClassModel, _
                                 _stateObject.GetArray(childClassModel.SetName), _
                               Me)

    End Function

    Public Function Persist() As String

        Dim connection As LobsterConnection = _session.DatabaseConnection
        Dim key As String

        If _classModel.KeyType = lafDataType.lafGuid Then
            key = Guid.NewGuid().ToString()
            _stateObject.SetString(_classModel.KeyName, key)
        Else
            _stateObject.SetInteger(_classModel.KeyName, _
                                  connection.GetNextID(_classModel.KeyName, _
                                                       _classModel.TableName))
            key = Me.GetInteger(_classModel.KeyName).ToString()
        End If

        Dim command As New SqlCommand(SqlBuilder.GetInsertSql(_classModel), _
                                      connection.Connection)

        Me.PopulateInsertParameters(command)

        command.ExecuteNonQuery()
        command = Nothing

        'cn.Close()

        For Each childClassModel As ClassModelReader.ClassModel _
            In _classModel.GetChildClassModels
            Me.GetChildSet(childClassModel).Persist(connection, _
                                                    _classModel.KeyName, _
                                                    key)
        Next

        If _classModel.ClassModelId = DomainModel.ClassModel.ClassModelId Then
            Dim newCompositeModel As New ClassModelReader.ClassModel( _
                key, ObjectSource.Application, _session)
            Tables.Build(newCompositeModel, _session)
        End If

        Return key

    End Function



End Class
