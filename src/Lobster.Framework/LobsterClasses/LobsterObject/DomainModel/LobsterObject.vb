'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class LobsterObject

    Protected _classModel As ClassModelReader.ClassModel
    Protected _stateObject As StateObject

    Sub New(ByVal classModel As ClassModelReader.ClassModel, _
            ByVal stateObject As StateObject)
        _classModel = classModel
        _stateObject = stateObject
    End Sub

    Sub New(ByVal classModel As ClassModelReader.ClassModel)
        _classModel = classModel
        _stateObject = New StateObject("{}")
    End Sub

    Sub New()

    End Sub

    Public Function GetBoolean(ByVal fieldName As String) As Boolean
        GetBoolean = _stateObject.GetBoolean(fieldName)
    End Function

    Public Sub SetBoolean(ByVal fieldName As String, ByVal value As Boolean)
        _stateObject.SetBoolean(fieldName, value)
    End Sub

    Public Function GetString(ByVal fieldName As String) As String
        GetString = _stateObject.GetString(fieldName)
    End Function

    Public Sub SetString(ByVal fieldName As String, ByVal value As String)
        _stateObject.SetString(fieldName, value)
    End Sub

    Public Function GetInteger(ByVal fieldName As String) As Integer
        GetInteger = _stateObject.GetInteger(fieldName)
    End Function

    Public Sub SetInteger(ByVal fieldName As String, ByVal value As Integer)
        _stateObject.SetInteger(fieldName, value)
    End Sub

    Public Sub Commit()

    End Sub

    Protected Sub PopulateInsertParameters(ByRef command As SqlCommand)

        For Each pm As FieldModel In _classModel.GetFieldModels().Collection
            Select Case pm.DataTypeId
                Case lafDataType.lafBoolean
                    command.Parameters.AddWithValue(pm.Name, Me.GetBoolean(pm.Name))
                Case lafDataType.lafGuid
                    command.Parameters.AddWithValue(pm.Name, Me.GetString(pm.Name))
                Case lafDataType.lafString
                    command.Parameters.AddWithValue(pm.Name, Me.GetString(pm.Name))
                Case lafDataType.lafInteger
                    command.Parameters.AddWithValue(pm.Name, Me.GetInteger(pm.Name))
                Case Else
                    Err.Raise(11, , "problem")
            End Select
        Next

    End Sub




End Class
