<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGMODIFY
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGMODIFY))
        Me.spdHistory = New AxFPSpreadADO.AxfpSpread
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdHistory
        '
        Me.spdHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdHistory.Location = New System.Drawing.Point(5, 7)
        Me.spdHistory.Name = "spdHistory"
        Me.spdHistory.OcxState = CType(resources.GetObject("spdHistory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdHistory.Size = New System.Drawing.Size(697, 221)
        Me.spdHistory.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(613, 237)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 31)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Location = New System.Drawing.Point(2, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(705, 227)
        Me.Panel1.TabIndex = 4
        '
        'FGMODIFY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(710, 280)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.spdHistory)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Name = "FGMODIFY"
        Me.Text = "최종보고 수정사유 내역"
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdHistory As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
