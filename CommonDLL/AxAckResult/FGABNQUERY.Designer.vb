<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGABNQUERY
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGABNQUERY))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdAbn = New AxFPSpreadADO.AxfpSpread()
        Me.Panel1.SuspendLayout()
        CType(Me.spdAbn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.spdAbn)
        Me.Panel1.Location = New System.Drawing.Point(-1, -2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1294, 631)
        Me.Panel1.TabIndex = 0
        '
        'spdAbn
        '
        Me.spdAbn.DataSource = Nothing
        Me.spdAbn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdAbn.Location = New System.Drawing.Point(0, 0)
        Me.spdAbn.Name = "spdAbn"
        Me.spdAbn.OcxState = CType(resources.GetObject("spdAbn.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAbn.Size = New System.Drawing.Size(1294, 631)
        Me.spdAbn.TabIndex = 0
        '
        'FGABNQUERY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1294, 629)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FGABNQUERY"
        Me.Text = "655555555555555555555555555555555555555555555555555555555555555555555555555555555" & _
            "555555555"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdAbn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdAbn As AxFPSpreadADO.AxfpSpread
End Class
