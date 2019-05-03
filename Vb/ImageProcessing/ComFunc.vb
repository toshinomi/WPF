Namespace ImageProcessing
    Public Class ComFunc
        Public Shared Function DoubleToByte(ByVal _dValue As Double) As Byte
            Dim byteCnvValue As Byte = 0

            If (_dValue > 255.0) Then
                byteCnvValue = 255
            ElseIf (_dValue < 0) Then
                byteCnvValue = 0
            Else
                byteCnvValue = CByte(_dValue)
            End If

            Return byteCnvValue
        End Function
    End Class
End Namespace
