'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Namespace ClassModelReader.Bootstrap

    Public Class ClassModel

        Private _classModelId As String
        Private _name As String
        Private _setNameOverload As String
        Private _tableName As String

        Private _fieldModels As Collection

        Sub New(ByVal key As String, ByVal frameworkConnection As FrameworkConnection)

            _classModelId = key

            Dim sql As String = "SELECT * FROM ClassModels Where ClassModelId = '" & _
                key & "';"

            Dim reader As LobsterReader = frameworkConnection.GetReader(sql)

            reader.Read()

            _name = reader.GetString("Name")
            _setNameOverload = reader.GetString("SetNameOverload")

            reader.Close()

            _fieldModels = LoadFieldModels(frameworkConnection)

        End Sub

        Private Function LoadFieldModels(ByVal connection As LobsterConnection) As Collection

            Dim collection As New Collection

            Dim sql As String = "SELECT * FROM ClassModels_FieldModels Where ClassModelId = '" & _
                _classModelId & "';"

            Dim reader As LobsterReader = connection.GetReader(sql)

            Do While reader.Read()
                Dim name As String = reader.GetString("Name")
                Dim dataTypeId As lafDataType = CType(reader.GetInteger("DataTypeId"), lafDataType)
                collection.Add(New BootstrapFieldModel(name, dataTypeId))
            Loop

            reader.Close()

            Return collection

        End Function

        ReadOnly Property ClassModelId() As String
            Get
                Return _classModelId
            End Get
        End Property

        ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        ReadOnly Property SetName() As String
            Get
                If _setNameOverload = "" Then
                    Return _name & "s"
                Else
                    Return _setNameOverload
                End If
            End Get
        End Property

        ReadOnly Property TableName() As String
            Get
                Return SetName
            End Get
        End Property

        ReadOnly Property KeyName() As String
            Get
                Return Name & "Id"
            End Get
        End Property

        Public Function GetFieldModels() As Collection
            Return _fieldModels
        End Function

    End Class

End Namespace
