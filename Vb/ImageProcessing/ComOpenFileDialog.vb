Imports Microsoft.Win32

Namespace ImageProcessing
    Public Class ComOpenFileDialog : Inherits ComFileDialog
        Public Sub New()
            MyBase.New()
        End Sub

        Protected Overloads Sub Finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Function ShowDialog() As Boolean
            Dim bRst As Boolean = False
            Dim openDialog As OpenFileDialog = New OpenFileDialog()
            openDialog.FileName = MyBase.FileName
            openDialog.InitialDirectory = MyBase.InitialDirectory
            openDialog.Filter = MyBase.Filter
            openDialog.FilterIndex = MyBase.FilterIndex
            openDialog.Title = MyBase.Title
            openDialog.CheckFileExists = MyBase.CheckFileExists
            openDialog.CheckPathExists = MyBase.CheckPathExists
            If (openDialog.ShowDialog() = True) Then
                MyBase.m_strFilePass = openDialog.FileName
                bRst = True
            End If

            Return bRst
        End Function
    End Class
End Namespace