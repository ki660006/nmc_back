
Public Class NumericTextBox
    Inherits System.Windows.Forms.TextBox

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        Me.Text = "999.99"
    End Sub

    'UserControl1은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private Sub NumericTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Integer

        KeyAscii = Asc(e.KeyChar)
        Select Case KeyAscii
            Case 48 To 57 '숫자 0-9 
            Case 8, 13   '백스페이스 캐리지 리턴

            Case 45  '마이너스 기호
                ' 이 숫자는 오직 마이너스 기호만을 가질 수 있다.
                ' 따라서 이미 하나를 가지고 있다면 하나는 버린다.
                If InStr(Me.Text, "-") <> 0 Then KeyAscii = 0

                ' 삽입 지점이 0이 아닌 경우(필드의 시작이 아닌 경우)에는
                ' 마이너스 기호를 버린다.(마이너스 기호는 맨 처음이 아니면 안되기 때문이다.)
                If Me.SelectionStart <> 0 Then KeyAscii = 0

            Case 46                 '소솟점 기호(마침표)
                '소수점을 가지고 있다면, 버린다.
                If InStr(Me.Text, ".") <> 0 Then KeyAscii = 0

            Case Else
                ' 다른키에 대해서는 처리를 하지 않는다
                KeyAscii = 0
        End Select
        If KeyAscii = 0 Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

End Class
