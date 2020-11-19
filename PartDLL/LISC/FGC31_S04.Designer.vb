<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGC31_S04
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGC31_S04))
        Me.spdHistory = New AxFPSpreadADO.AxfpSpread
        Me.btnExit = New System.Windows.Forms.Button
        Me.spdPatInfo = New AxFPSpreadADO.AxfpSpread
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spdHistory
        '
        Me.spdHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdHistory.DataSource = Nothing
        Me.spdHistory.Location = New System.Drawing.Point(5, 61)
        Me.spdHistory.Name = "spdHistory"
        Me.spdHistory.OcxState = CType(resources.GetObject("spdHistory.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdHistory.Size = New System.Drawing.Size(760, 379)
        Me.spdHistory.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(668, 8)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(97, 47)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'spdPatInfo
        '
        Me.spdPatInfo.DataSource = Nothing
        Me.spdPatInfo.Location = New System.Drawing.Point(5, 8)
        Me.spdPatInfo.Name = "spdPatInfo"
        Me.spdPatInfo.OcxState = CType(resources.GetObject("spdPatInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPatInfo.Size = New System.Drawing.Size(584, 47)
        Me.spdPatInfo.TabIndex = 5
        '
        'FGC31_S04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(777, 452)
        Me.Controls.Add(Me.spdPatInfo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.spdHistory)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGC31_S04"
        Me.Text = "REJECT 결과 내역"
        CType(Me.spdHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdHistory As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents spdPatInfo As AxFPSpreadADO.AxfpSpread
End Class
