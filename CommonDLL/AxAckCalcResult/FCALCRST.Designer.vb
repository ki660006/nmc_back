<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FCALCRST
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FCALCRST))
        Me.lblUrVol = New System.Windows.Forms.Label
        Me.txtUrVol = New System.Windows.Forms.TextBox
        Me.pnlRst = New System.Windows.Forms.Panel
        Me.spdRst = New AxFPSpreadADO.AxfpSpread
        Me.btnApply = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.lblBcNo = New System.Windows.Forms.Label
        Me.btnCalc = New System.Windows.Forms.Button
        Me.chkOptCalc = New System.Windows.Forms.CheckBox
        Me.txtCoPeriod = New System.Windows.Forms.TextBox
        Me.lblCoPeriod = New System.Windows.Forms.Label
        Me.pnlRst.SuspendLayout()
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblUrVol
        '
        Me.lblUrVol.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUrVol.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUrVol.ForeColor = System.Drawing.Color.Black
        Me.lblUrVol.Location = New System.Drawing.Point(4, 4)
        Me.lblUrVol.Margin = New System.Windows.Forms.Padding(1)
        Me.lblUrVol.Name = "lblUrVol"
        Me.lblUrVol.Size = New System.Drawing.Size(135, 21)
        Me.lblUrVol.TabIndex = 0
        Me.lblUrVol.Text = "[UV,UZ] Urine Volume"
        Me.lblUrVol.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUrVol
        '
        Me.txtUrVol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUrVol.Location = New System.Drawing.Point(141, 4)
        Me.txtUrVol.Margin = New System.Windows.Forms.Padding(1)
        Me.txtUrVol.Name = "txtUrVol"
        Me.txtUrVol.Size = New System.Drawing.Size(41, 21)
        Me.txtUrVol.TabIndex = 1
        '
        'pnlRst
        '
        Me.pnlRst.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlRst.Controls.Add(Me.spdRst)
        Me.pnlRst.Location = New System.Drawing.Point(4, 30)
        Me.pnlRst.Name = "pnlRst"
        Me.pnlRst.Size = New System.Drawing.Size(585, 385)
        Me.pnlRst.TabIndex = 2
        '
        'spdRst
        '
        Me.spdRst.DataSource = Nothing
        Me.spdRst.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdRst.Location = New System.Drawing.Point(0, 0)
        Me.spdRst.Name = "spdRst"
        Me.spdRst.OcxState = CType(resources.GetObject("spdRst.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst.Size = New System.Drawing.Size(585, 385)
        Me.spdRst.TabIndex = 0
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Location = New System.Drawing.Point(414, 423)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(86, 29)
        Me.btnApply.TabIndex = 4
        Me.btnApply.Text = "적용(&A)"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(503, 423)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 29)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "종료 Esc"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtBcNo
        '
        Me.txtBcNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBcNo.BackColor = System.Drawing.Color.Gainsboro
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.Location = New System.Drawing.Point(469, 4)
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.ReadOnly = True
        Me.txtBcNo.Size = New System.Drawing.Size(120, 21)
        Me.txtBcNo.TabIndex = 7
        Me.txtBcNo.Text = "20080301-C1-0001-0"
        '
        'lblBcNo
        '
        Me.lblBcNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBcNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblBcNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBcNo.ForeColor = System.Drawing.Color.White
        Me.lblBcNo.Location = New System.Drawing.Point(400, 4)
        Me.lblBcNo.Name = "lblBcNo"
        Me.lblBcNo.Size = New System.Drawing.Size(68, 21)
        Me.lblBcNo.TabIndex = 6
        Me.lblBcNo.Text = "검체번호"
        Me.lblBcNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnCalc
        '
        Me.btnCalc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCalc.Location = New System.Drawing.Point(325, 423)
        Me.btnCalc.Name = "btnCalc"
        Me.btnCalc.Size = New System.Drawing.Size(86, 29)
        Me.btnCalc.TabIndex = 3
        Me.btnCalc.Text = "계산(&C)"
        Me.btnCalc.UseVisualStyleBackColor = True
        '
        'chkOptCalc
        '
        Me.chkOptCalc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkOptCalc.AutoSize = True
        Me.chkOptCalc.Location = New System.Drawing.Point(171, 429)
        Me.chkOptCalc.Name = "chkOptCalc"
        Me.chkOptCalc.Size = New System.Drawing.Size(148, 16)
        Me.chkOptCalc.TabIndex = 8
        Me.chkOptCalc.Text = "결과 입력 시 자동 계산"
        Me.chkOptCalc.UseVisualStyleBackColor = True
        '
        'txtCoPeriod
        '
        Me.txtCoPeriod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCoPeriod.Location = New System.Drawing.Point(353, 4)
        Me.txtCoPeriod.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCoPeriod.Name = "txtCoPeriod"
        Me.txtCoPeriod.Size = New System.Drawing.Size(37, 21)
        Me.txtCoPeriod.TabIndex = 10
        '
        'lblCoPeriod
        '
        Me.lblCoPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCoPeriod.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCoPeriod.ForeColor = System.Drawing.Color.Black
        Me.lblCoPeriod.Location = New System.Drawing.Point(226, 4)
        Me.lblCoPeriod.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCoPeriod.Name = "lblCoPeriod"
        Me.lblCoPeriod.Size = New System.Drawing.Size(125, 21)
        Me.lblCoPeriod.TabIndex = 9
        Me.lblCoPeriod.Text = "[CP,CZ] Co.Period"
        Me.lblCoPeriod.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FCALCRST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(593, 461)
        Me.Controls.Add(Me.txtCoPeriod)
        Me.Controls.Add(Me.lblCoPeriod)
        Me.Controls.Add(Me.chkOptCalc)
        Me.Controls.Add(Me.btnCalc)
        Me.Controls.Add(Me.lblBcNo)
        Me.Controls.Add(Me.txtBcNo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.pnlRst)
        Me.Controls.Add(Me.txtUrVol)
        Me.Controls.Add(Me.lblUrVol)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FCALCRST"
        Me.Text = "계산식 결과 입력 및 확인"
        Me.pnlRst.ResumeLayout(False)
        CType(Me.spdRst, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUrVol As System.Windows.Forms.Label
    Friend WithEvents txtUrVol As System.Windows.Forms.TextBox
    Friend WithEvents pnlRst As System.Windows.Forms.Panel
    Friend WithEvents spdRst As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents lblBcNo As System.Windows.Forms.Label
    Friend WithEvents btnCalc As System.Windows.Forms.Button
    Friend WithEvents chkOptCalc As System.Windows.Forms.CheckBox
    Friend WithEvents txtCoPeriod As System.Windows.Forms.TextBox
    Friend WithEvents lblCoPeriod As System.Windows.Forms.Label
End Class
