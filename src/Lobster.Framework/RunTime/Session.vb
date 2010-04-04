'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class Session

    Private mCompanyId As Integer
    Private mBranchId As Integer
    Private mDevelopmentOrProduction As DevelopmentOrProductionEnum
    Private _databaseConnection As LobsterConnection

    Public Enum DevelopmentOrProductionEnum As Integer
        Development = 1
        Production = 2
    End Enum

    Sub New(ByVal accountID As Integer)
        _databaseConnection = New LobsterConnection(accountID)
    End Sub

    Sub New(ByVal companyId As Integer, ByVal branchId As Integer, _
            ByVal developmentOrProduction As DevelopmentOrProductionEnum)
        mCompanyId = companyId
        mBranchId = branchId
        mDevelopmentOrProduction = developmentOrProduction
    End Sub

    ReadOnly Property CompanyId() As Integer
        Get
            CompanyId = mCompanyId
        End Get
    End Property

    ReadOnly Property BranchId() As Integer
        Get
            BranchId = mBranchId
        End Get
    End Property

    ReadOnly Property DevelopmentOrProduction() As DevelopmentOrProductionEnum
        Get
            DevelopmentOrProduction = mDevelopmentOrProduction
        End Get
    End Property

    ReadOnly Property DatabaseConnection() As LobsterConnection
        Get
            Return _databaseConnection
        End Get
    End Property



End Class
