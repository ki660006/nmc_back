<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF11_S01
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF11_S01))
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.lblSlip = New System.Windows.Forms.Label
        Me.spdTest = New AxFPSpreadADO.AxfpSpread
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.Location = New System.Drawing.Point(97, 10)
        Me.cboSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.cboSlip.MaxDropDownItems = 10
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(343, 20)
        Me.cboSlip.TabIndex = 8
        Me.cboSlip.TabStop = False
        Me.cboSlip.Tag = "SPCNMD_01"
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.White
        Me.lblSlip.Location = New System.Drawing.Point(12, 9)
        Me.lblSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(84, 21)
        Me.lblSlip.TabIndex = 7
        Me.lblSlip.Text = " 검사분류"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdTest
        '
        Me.spdTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdTest.Location = New System.Drawing.Point(12, 33)
        Me.spdTest.Name = "spdTest"
        Me.spdTest.OcxState = CType(resources.GetObject("spdTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTest.Size = New System.Drawing.Size(737, 576)
        Me.spdTest.TabIndex = 9
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDown.Location = New System.Drawing.Point(721, 9)
        Me.btnDown.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(28, 21)
        Me.btnDown.TabIndex = 35
        Me.btnDown.Text = "▼"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUp.Location = New System.Drawing.Point(693, 9)
        Me.btnUp.Margin = New System.Windows.Forms.Padding(1)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(28, 21)
        Me.btnUp.TabIndex = 34
        Me.btnUp.Text = "▲"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 617)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(763, 30)
        Me.Panel1.TabIndex = 36
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(592, 1)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 25)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(674, 1)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(80, 25)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "닫기(ESC)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'FDF11_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(763, 647)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.spdTest)
        Me.Controls.Add(Me.cboSlip)
        Me.Controls.Add(Me.lblSlip)
        Me.KeyPreview = True
        Me.Name = "FDF11_S01"
        Me.Text = "표시순서 정하기"
        CType(Me.spdTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents spdTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
