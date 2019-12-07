''' <summary>
''' 共通関数のロジック
''' </summary>
Public Class ComFunc
    ''' <summary>
    ''' Double型のデータからbyte型のデータへの変換
    ''' </summary>
    ''' <param name="_dValue">Double型のデータ</param>
    ''' <returns>byte型のデータ</returns>
    Public Shared Function DoubleToByte(ByVal _dValue As Double) As Byte
        Dim nCnvValue As Byte = 0

        If (_dValue > 255.0) Then
            nCnvValue = 255
        ElseIf (_dValue < 0) Then
            nCnvValue = 0
        Else
            nCnvValue = CByte(_dValue)
        End If

        Return nCnvValue
    End Function

    ''' <summary>
    ''' long型のデータからbyte型のデータへの変換
    ''' </summary>
    ''' <param name="_lValue">long型のデータ</param>
    ''' <returns>byte型のデータ</returns>
    Public Shared Function LongToByte(ByVal _lValue As Long) As Byte
        Dim nCnvValue As Byte = 0

        If (_lValue > 255) Then
            nCnvValue = 255
        ElseIf (_lValue < 0) Then
            nCnvValue = 0
        Else
            nCnvValue = CByte(_lValue)
        End If

        Return nCnvValue
    End Function
End Class