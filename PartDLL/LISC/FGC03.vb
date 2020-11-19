'>>> 수탁 채혈
Public Class FGC03
    Inherits LISC.FGC01

    Public Sub New()

        ' 이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        ' InitializeComponent() 호출 뒤에 초기화 코드를 추가하십시오.

        Me.axCollList.CallForm = AxAckCollector.enumCollectCallForm.CollectCust

    End Sub

    Private Sub FGC03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.WindowState = Windows.Forms.FormWindowState.Maximized
    End Sub
End Class