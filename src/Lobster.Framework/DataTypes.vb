'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Enum lafDataType As Integer
    lafString = 10
    lafInteger = 4
    lafDecimal = 2
    lafCurrency = 5
    lafPercent = 3
    lafBoolean = 1
    lafDate = 8
    lafEnum = 7
    lafForeignKey = 6
    lafFile = 11
    lafFileRef = 11
    lafGuid = 12
End Enum

Public Enum ObjectSource As Integer
    Framework = 1
    Application = 2
End Enum