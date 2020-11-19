<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGHISTORY
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGHISTORY))
        Me.spdHistory = New AxFPSpreadADO.AxfpSpread
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.axPatInfo = New AxAckResult.AxRstPatInfo
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdHistory
        '
        Me.spdHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdHistory.Location = New System.Drawing.Point(6, 118)
        Me.spdHistory.Name = "spdHistory"
        Me.spdHistory.OcxState = CType(resources.GetObject("spdHistory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdHistory.Size = New System.Drawing.Size(915, 331)
        Me.spdHistory.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(740, 456)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(85, 31)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "선택"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(831, 456)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 31)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'axPatInfo
        '
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.BcNo = ""
        Me.axPatInfo.Location = New System.Drawing.Point(3, 0)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.RegNo = ""
        Me.axPatInfo.Size = New System.Drawing.Size(919, 114)
        Me.axPatInfo.SlipCd = ""
        Me.axPatInfo.TabIndex = 5
        '
        'FGHISTORY
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(928, 491)
        Me.Controls.Add(Me.axPatInfo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.spdHistory)
        Me.Name = "FGHISTORY"
        Me.Text = "검사결과 History"
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdHistory As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents axPatInfo As AxAckResult.AxRstPatInfo
End Class
