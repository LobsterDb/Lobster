'LobsterDb
'Copyright(c) 2010, LobsterDb
'Licensed under the AGPL Version 3 license
'http://www.lobsterdb.com/license

Public Class PropertyModelWrapper

    Public Shared Function SqlDataType(ByVal propertyModel As FieldModel) As String

        Select Case propertyModel.DataTypeId
            Case lafDataType.lafDecimal, lafDataType.lafPercent, lafDataType.lafCurrency
                Return "decimal"
            Case lafDataType.lafBoolean
                Return "bit"
                'Case 2 'Byte
                '   Return "tinyint"
            Case lafDataType.lafGuid
                Return "uniqueidentifier"
            Case lafDataType.lafInteger
                Return "int"
            Case lafDataType.lafCurrency
                Return "money"
            Case lafDataType.lafForeignKey
                Return "int"
            Case lafDataType.lafEnum
                Return "int"
            Case lafDataType.lafDate
                Return "datetime"
            Case lafDataType.lafString, lafDataType.lafFileRef
                Return "varchar"
            Case lafDataType.lafFile 'TEMP TEST, MIKE
                Return "varchar"
                'Case 11 'Url
                '   Return "varchar"
                'Case 12 'Memo
                '   Return "text"
            Case Else
                Err.Raise(1, , "datatypesqlserver case else")
        End Select

    End Function

End Class

