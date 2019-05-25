Imports Microsoft.Win32

Namespace ImageProcessing
    Public Class ComSaveFileDialog : Inherits ComFileDialog
        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overloads Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Function ShowDialog() As Boolean
            Dim bRst As Boolean = False
            Dim saveDialog As SaveFileDialog = New SaveFileDialog()
            saveDialog.FileName = MyBase.FileName
            saveDialog.InitialDirectory = MyBase.InitialDirectory
            saveDialog.Filter = MyBase.Filter
            saveDialog.FilterIndex = MyBase.FilterIndex
            saveDialog.Title = MyBase.Title
            saveDialog.CheckFileExists = MyBase.CheckFileExists
            saveDialog.CheckPathExists = MyBase.CheckPathExists
            If (saveDialog.ShowDialog() = True) Then
                MyBase.m_strFilePass = saveDialog.FileName
                bRst = True
            End If

            Return bRst
        End Function
    End Class
End Namespace
