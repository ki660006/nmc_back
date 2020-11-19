
Public Class NumericTextBox
    Inherits System.Windows.Forms.TextBox

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        Me.Text = "999.99"
    End Sub

    'UserControl1�� Dispose�� �������Ͽ� ���� ��� ����� �����մϴ�.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form �����̳ʿ� �ʿ��մϴ�.
    Private components As System.ComponentModel.IContainer

    '����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
    'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.  
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private Sub NumericTextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Dim KeyAscii As Integer

        KeyAscii = Asc(e.KeyChar)
        Select Case KeyAscii
            Case 48 To 57 '���� 0-9 
            Case 8, 13   '�齺���̽� ĳ���� ����

            Case 45  '���̳ʽ� ��ȣ
                ' �� ���ڴ� ���� ���̳ʽ� ��ȣ���� ���� �� �ִ�.
                ' ���� �̹� �ϳ��� ������ �ִٸ� �ϳ��� ������.
                If InStr(Me.Text, "-") <> 0 Then KeyAscii = 0

                ' ���� ������ 0�� �ƴ� ���(�ʵ��� ������ �ƴ� ���)����
                ' ���̳ʽ� ��ȣ�� ������.(���̳ʽ� ��ȣ�� �� ó���� �ƴϸ� �ȵǱ� �����̴�.)
                If Me.SelectionStart <> 0 Then KeyAscii = 0

            Case 46                 '�Ҽ��� ��ȣ(��ħǥ)
                '�Ҽ����� ������ �ִٸ�, ������.
                If InStr(Me.Text, ".") <> 0 Then KeyAscii = 0

            Case Else
                ' �ٸ�Ű�� ���ؼ��� ó���� ���� �ʴ´�
                KeyAscii = 0
        End Select
        If KeyAscii = 0 Then
            e.Handled = True
        Else
            e.Handled = False
        End If

    End Sub

End Class
