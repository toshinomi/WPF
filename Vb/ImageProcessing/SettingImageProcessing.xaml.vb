Public Class SettingImageProcessing
    Private m_items As List(Of ImageProcessingType)

    Public Sub New()

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        LoadParam()

    End Sub

    Public Sub LoadParam()
        m_items = New List(Of ImageProcessingType)
        m_items.Add(New ImageProcessingType(My.Settings.ImgTypeEdgeId, My.Settings.ImgTypeEdgeName))
        m_items.Add(New ImageProcessingType(My.Settings.ImgTypeGrayScaleId, My.Settings.ImgTypeGrayScaleName))
        m_items.Add(New ImageProcessingType(My.Settings.ImgTypeBinarizationId, My.Settings.ImgTypeBinarizationName))
        m_items.Add(New ImageProcessingType(My.Settings.ImgTypeGrayScale2DiffId, My.Settings.ImgTypeGrayScale2DiffName))

        cmbBoxImageProcessingType.ItemsSource = m_items
        cmbBoxImageProcessingType.SelectedIndex = m_items.Find(Function(x) x.Name = My.Settings.ImgTypeSelectName)?.Id - 1

        Return
    End Sub

    Public Sub SaveParam()
        Dim imgProcType As ImageProcessingType = cmbBoxImageProcessingType.SelectedItem
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
