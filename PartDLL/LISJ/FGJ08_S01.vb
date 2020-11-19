Imports COMMON.CommFN

Public Class FGJ08_S01
    Inherits System.Windows.Forms.Form

    Public msWLTitle As String = ""

    Friend WithEvents txtWLTitle As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Public msAction As String

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.txtWLTitle = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'txtWLTitle
        '
        Me.txtWLTitle.Location = New System.Drawing.Point(12, 22)
        Me.txtWLTitle.MaxLength = 40
        Me.txtWLTitle.Name = "txtWLTitle"
        Me.txtWLTitle.Size = New System.Drawing.Size(294, 21)
        Me.txtWLTitle.TabIndex = 201
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(216, 64)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(90, 26)
        Me.btnCancel.TabIndex = 203
        Me.btnCancel.Text = "취소(ESC)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOk.Location = New System.Drawing.Point(124, 64)
        Me.btnOk.Margin = New System.Windows.Forms.Padding(1)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(90, 26)
        Me.btnOk.TabIndex = 202
        Me.btnOk.Text = "입  력"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'FGS14_S01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(318, 100)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtWLTitle)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGS14_S01"
        Me.Text = "W/L 제목 입력"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub FGS14_S01_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        msAction = "NO"
    End Sub

    Private Sub btnOk_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click

        msWLTitle = Me.txtWLTitle.Text
        msAction = "YES"
        Me.Close()

    End Sub

    Private Sub btnCancel_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        msWLTitle = ""
        msAction = "NO"
        Me.Close()

    End Sub


    Public WriteOnly Property WLTITLE() As String
        Set(ByVal Value As String)
            Me.txtWLTitle.Text = Value
        End Set
    End Property

    Public Property ACTION() As String
        Get
            Return msAction
        End Get
        Set(ByVal Value As String)

        End Set
    End Property
End Class
