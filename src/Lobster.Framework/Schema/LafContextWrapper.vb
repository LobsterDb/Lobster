'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Option Explicit On
Option Strict On

Public Class SessionContextWrapper

    Private mSessionContext As Session

    Sub New(ByVal sessionContext As Session)
        mSessionContext = sessionContext
    End Sub

    Public Function DatabaseName() As String
        Dim name As String
        name = "LobsterDb_1"
        ' & mSessionContext.CompanyId & _
        '           "_" & mSessionContext.BranchId & "_Data"
        DatabaseName = name
    End Function

    Public Function GetDatabase(ByVal accountID As Integer) As SQLDMO._Database

        Dim databaseName As String = "LobsterDb_" & accountID

        Dim sql As New SQLDMO.SQLServer

        sql.LoginSecure = True
        sql.Connect(Parameters.DataSource)

        GetDatabase = sql.Databases.Item(databaseName)

    End Function

End Class
