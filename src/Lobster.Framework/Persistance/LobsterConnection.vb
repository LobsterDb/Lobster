'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class LobsterConnection

    Private mConnection As SqlConnection
    Private mAccountId As Integer
    Private mDomainId As Integer
    Private mDevOrProd As Session.DevelopmentOrProductionEnum
    Public _databaseName As String

    Sub New(ByVal accountId As Integer, ByVal domainId As Integer, _
            ByVal devOrProd As Session.DevelopmentOrProductionEnum)

        mAccountId = accountId
        mDomainId = domainId
        mDevOrProd = devOrProd

        _databaseName = "LobsterDb_1"
        mConnection = GetConnection()

    End Sub

    Sub New(ByVal databaseName As String)
        _databaseName = databaseName
        mConnection = GetConnection()
    End Sub

    Sub New(ByVal accountId As Integer)
        _databaseName = "LobsterDb_1"
        mConnection = GetConnection()
    End Sub

    Private ReadOnly Property DatabaseName() As String
        Get
            Return _databaseName
        End Get
    End Property

    Private Function GetConnection() As SqlConnection

        Dim cn As New SqlConnection("Data Source=Lobster-Dev" & _
            ";Initial Catalog=" & Me.DatabaseName & ";Integrated Security=True")
        cn.Open()

        Return cn

    End Function

    ReadOnly Property Connection() As SqlConnection
        Get
            Connection = mConnection
        End Get
    End Property

    Public Function GetNextID(ByVal pkeyName As String, ByVal tableName As String) As Integer

        Dim sql As String = "SELECT Max(" & pkeyName & ") As MaxID FROM " & tableName & ";"

        Dim reader As New LobsterReader(sql, Me)

        reader.Read()

        Dim returnValue As Integer = 0

        If IsDBNull(reader.GetItem("MaxID")) Then
            returnValue = 1
        Else
            returnValue = CType(reader.GetItem("MaxID"), Integer) + 1
        End If

        reader.Close()
        reader = Nothing

        GetNextID = returnValue

    End Function

    Public Function GetReader(ByVal sql As String) As LobsterReader

        Dim reader As New LobsterReader(sql, Me)
        Return reader

    End Function


End Class
