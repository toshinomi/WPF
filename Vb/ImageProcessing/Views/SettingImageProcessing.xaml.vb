Public Class SettingImageProcessing
    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        LoadParam()

    End Sub

    Public Sub LoadParam()
        Dim items As List(Of ComImageProcessingType) = New List(Of ComImageProcessingType)
        items.Add(New ComImageProcessingType(My.Settings.ImgTypeEdgeId, My.Settings.ImgTypeEdgeName))
        items.Add(New ComImageProcessingType(My.Settings.ImgTypeGrayScaleId, My.Settings.ImgTypeGrayScaleName))
        items.Add(New ComImageProcessingType(My.Settings.ImgTypeBinarizationId, My.Settings.ImgTypeBinarizationName))
        items.Add(New ComImageProcessingType(My.Settings.ImgTypeGrayScale2DiffId, My.Settings.ImgTypeGrayScale2DiffName))
        items.Add(New ComImageProcessingType(My.Settings.ImgTypeColorReversalId, My.Settings.ImgTypeColorReversalName))

        cmbBoxImageProcessingType.ItemsSource = items
        cmbBoxImageProcessingType.SelectedIndex = items.Find(Function(x) x.Name = My.Settings.ImgTypeSelectName)?.Id - 1

        Return
    End Sub

    Public Sub SaveParam()
        Dim imgProcType As ComImageProcessingType = cmbBoxImageProcessingType.SelectedItem
        My.Settings.ImgTypeSelectName = imgProcType.Name
        My.Settings.Save()

        Return
    End Sub

    Private Sub OnClickOk(sender As Object, e As RoutedEventArgs) Handles btnOk.Click
        SaveParam()
        DialogResult = True
        Close()
    End Sub

    Private Sub OnClickCancel(sender As Object, e As RoutedEventArgs) Handles btnCancel.Click
        Close()
    End Sub
End Class
