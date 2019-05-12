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


        cmbBoxImageProcessingType.ItemsSource = m_items
        cmbBoxImageProcessingType.SelectedIndex = My.Settings.ImgTypeSelectIndex
        cmbBoxImageProcessingType.DataContext = My.Settings.ImgTypeSelectName

        Return
    End Sub

    Public Sub SaveParam()
        My.Settings.ImgTypeSelectIndex = cmbBoxImageProcessingType.SelectedIndex
        My.Settings.ImgTypeSelectName = cmbBoxImageProcessingType.DataContext
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

    Private Sub OnSelectionChangedCmbBoxImageProcessingType(sender As Object, e As SelectionChangedEventArgs) Handles cmbBoxImageProcessingType.SelectionChanged
        cmbBoxImageProcessingType.DataContext = m_items.Find(Function(x) x.Id = cmbBoxImageProcessingType.SelectedIndex + 1)?.Name
    End Sub
End Class
