Public Class SettingImageProcessing
    Private m_items As List(Of ComImageProcessingType)

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        LoadParam()

    End Sub

    Public Sub LoadParam()
        m_items = New List(Of ComImageProcessingType)
        m_items.Add(New ComImageProcessingType(My.Settings.ImgTypeEdgeId, My.Settings.ImgTypeEdgeName))
        m_items.Add(New ComImageProcessingType(My.Settings.ImgTypeGrayScaleId, My.Settings.ImgTypeGrayScaleName))
        m_items.Add(New ComImageProcessingType(My.Settings.ImgTypeBinarizationId, My.Settings.ImgTypeBinarizationName))
        m_items.Add(New ComImageProcessingType(My.Settings.ImgTypeGrayScale2DiffId, My.Settings.ImgTypeGrayScale2DiffName))
        m_items.Add(New ComImageProcessingType(My.Settings.ImgTypeColorReversalId, My.Settings.ImgTypeColorReversalName))

        cmbBoxImageProcessingType.ItemsSource = m_items
        cmbBoxImageProcessingType.SelectedIndex = m_items.Find(Function(x) x.Name = My.Settings.ImgTypeSelectName)?.Id - 1

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
