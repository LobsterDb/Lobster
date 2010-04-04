'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Interface ILobsterClass

    Function GetStringFieldValue(ByVal fieldName As String) As String
    Function GetIntegerFieldValue(ByVal fieldName As String) As Integer

End Interface
