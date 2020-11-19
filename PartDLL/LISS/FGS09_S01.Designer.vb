<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS09_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS09_S01))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnClear = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.cboSlipCd = New System.Windows.Forms.ComboBox
        Me.lblCencelDate = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCalc = New System.Windows.Forms.TextBox
        Me.spdTest = New AxFPSpreadADO.AxfpSpread
        Me.spdCalBuf = New AxFPSpreadADO.AxfpSpread
        Me.lblGuide2 = New System.Windows.Forms.Label
        Me.chkSpc = New System.Windows.Forms.CheckBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.spdCalBuf, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 451)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(635, 30)
        Me.Panel1.TabIndex = 5
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(448, 1)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(1)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(90, 26)
        Me.btnClear.TabIndex = 7
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(537, 1)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(90, 26)
        Me.btnExit.TabIndex = 8
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(359, 1)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(90, 26)
        Me.btnSave.TabIndex = 6
        Me.btnSave.Text = "적용(F2)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'cboSlipCd
        '
        Me.cboSlipCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlipCd.FormattingEnabled = True
        Me.cboSlipCd.Location = New System.Drawing.Point(88, 5)
        Me.cboSlipCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSlipCd.Name = "cboSlipCd"
        Me.cboSlipCd.Size = New System.Drawing.Size(272, 20)
        Me.cboSlipCd.TabIndex = 1
        '
        'lblCencelDate
        '
        Me.lblCencelDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCencelDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCencelDate.ForeColor = System.Drawing.Color.White
        Me.lblCencelDate.Location = New System.Drawing.Point(10, 5)
        Me.lblCencelDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCencelDate.Name = "lblCencelDate"
        Me.lblCencelDate.Size = New System.Drawing.Size(77, 21)
        Me.lblCencelDate.TabIndex = 0
        Me.lblCencelDate.Text = "검사분야"
        Me.lblCencelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(10, 389)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 21)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "계산식"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCalc
        '
        Me.txtCalc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCalc.Location = New System.Drawing.Point(88, 389)
        Me.txtCalc.Name = "txtCalc"
        Me.txtCalc.Size = New System.Drawing.Size(534, 21)
        Me.txtCalc.TabIndex = 4
        '
        'spdTest
        '
        Me.spdTest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdTest.DataSource = Nothing
        Me.spdTest.Location = New System.Drawing.Point(10, 30)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(612, 357)
        Me.spdTest.TabIndex = 2
        '
        'spdCalBuf
        '
        Me.spdCalBuf.DataSource = Nothing
        Me.spdCalBuf.Location = New System.Drawing.Point(547, 389)
        Me.spdCalBuf.Name = "spdCalBuf"
        Me.spdCalBuf.OcxState = CType(resources.GetObject("spdCalBuf.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCalBuf.Size = New System.Drawing.Size(75, 22)
        Me.spdCalBuf.TabIndex = 6
        '
        'lblGuide2
        '
        Me.lblGuide2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuide2.Location = New System.Drawing.Point(10, 417)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(613, 23)
        Me.lblGuide2.TabIndex = 7
        Me.lblGuide2.Text = "입력가능 문자: [A] ~ [Z], and, or, (, )" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "예) ( [A] and [B] or [C] ) "
        '
        'chkSpc
        '
        Me.chkSpc.AutoSize = True
        Me.chkSpc.Location = New System.Drawing.Point(368, 7)
        Me.chkSpc.Name = "chkSpc"
        Me.chkSpc.Size = New System.Drawing.Size(72, 16)
        Me.chkSpc.TabIndex = 8
        Me.chkSpc.Text = "검체포함"
        Me.chkSpc.UseVisualStyleBackColor = True
        '
        'FGS09_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(635, 481)
        Me.Controls.Add(Me.chkSpc)
        Me.Controls.Add(Me.lblGuide2)
        Me.Controls.Add(Me.spdCalBuf)
        Me.Controls.Add(Me.txtCalc)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCencelDate)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.cboSlipCd)
        Me.Controls.Add(Me.spdTest)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS09_S01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "조회 조건식"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.spdCalBuf, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboSlipCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblCencelDate As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCalc As System.Windows.Forms.TextBox
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdCalBuf As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents chkSpc As System.Windows.Forms.CheckBox
End Class
