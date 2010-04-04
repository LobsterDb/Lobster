'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class LobsterReader

    Private mReader As SqlDataReader

    Sub New(ByVal sql As String, ByVal sessionContext As Session)

        Dim lobsterConnection As LobsterConnection = _
            New LobsterConnection(1, 0, sessionContext.DevelopmentOrProductionEnum.Development)

        Dim command As New SqlCommand()

        command.Connection = lobsterConnection.Connection
        command.CommandText = sql

        mReader = command.ExecuteReader(CommandBehavior.CloseConnection)

    End Sub

    Sub New(ByVal sql As String, ByVal lobsterConnection As LobsterConnection)

        Dim command As New SqlCommand()

        command.Connection = lobsterConnection.Connection
        command.CommandText = sql

        mReader = command.ExecuteReader()

    End Sub

    Public Function Read() As Boolean
        Dim boo As Boolean = mReader.Read()
        Return boo
    End Function

    Public Sub Close()
        mReader.Close()
    End Sub

    Public Function GetReader() As SqlDataReader

        Return mReader

    End Function

    Public Function GetBoolean(ByVal fieldName As String) As Boolean

        Dim ordinal As Integer = mReader.GetOrdinal(fieldName)
        GetBoolean = mReader.GetBoolean(ordinal)

    End Function

    Public Function GetGuid(ByVal fieldName As String) As Guid

        Dim ordinal As Integer = mReader.GetOrdinal(fieldName)
        GetGuid = mReader.GetGuid(ordinal)

    End Function

    Public Function GetString(ByVal fieldName As String) As String

        Dim ordinal As Integer = mReader.GetOrdinal(fieldName)
        GetString = mReader.GetString(ordinal)

    End Function

    Public Function GetInteger(ByVal fieldName As String) As Integer

        Dim ordinal As Integer = mReader.GetOrdinal(fieldName)
        GetInteger = mReader.GetInt32(ordinal)

    End Function

    Public Function GetItem(ByVal columnName As String) As Object

        GetItem = mReader.Item(columnName)

    End Function


End Class
