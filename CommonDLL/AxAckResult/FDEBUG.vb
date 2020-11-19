Public Class FDEBUG
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents spd As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FDEBUG))
        Me.spd = New AxFPSpreadADO.AxfpSpread
        CType(Me.spd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spd
        '
        Me.spd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spd.Location = New System.Drawing.Point(0, 0)
        Me.spd.Name = "spd"
        Me.spd.OcxState = CType(resources.GetObject("spd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spd.Size = New System.Drawing.Size(704, 445)
        Me.spd.TabIndex = 0
        '
        'FDEBUG
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(704, 445)
        Me.Controls.Add(Me.spd)
        Me.Name = "FDEBUG"
        Me.Text = "FDEBUG"
        CType(Me.spd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
