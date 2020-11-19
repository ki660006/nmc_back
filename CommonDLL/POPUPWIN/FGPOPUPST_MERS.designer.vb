<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_MERS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_MERS))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnHelp_cmt = New System.Windows.Forms.Button
        Me.btnHelp_con = New System.Windows.Forms.Button
        Me.txtCmt = New System.Windows.Forms.TextBox
        Me.lblCmt = New System.Windows.Forms.Label
        Me.lblCon = New System.Windows.Forms.Label
        Me.txtCon = New System.Windows.Forms.TextBox
        Me.lblSpc = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.lblMethod = New System.Windows.Forms.Label
        Me.txtSpcnm = New System.Windows.Forms.TextBox
        Me.txtSpcDate = New System.Windows.Forms.TextBox
        Me.txtTestnm = New System.Windows.Forms.TextBox
        Me.btnSpc = New System.Windows.Forms.Button
        Me.lblRst = New System.Windows.Forms.Label
        Me.txtRst = New System.Windows.Forms.TextBox
        Me.btnRst = New System.Windows.Forms.Button
        Me.lblTest = New System.Windows.Forms.Label
        Me.txtTestinfo = New System.Windows.Forms.TextBox
        Me.btnHelp_test = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(566, 608)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(93, 36)
        Me.btnClose.TabIndex = 83
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(467, 608)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 36)
        Me.btnSave.TabIndex = 82
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnHelp_cmt
        '
        Me.btnHelp_cmt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_cmt.Image = CType(resources.GetObject("btnHelp_cmt.Image"), System.Drawing.Image)
        Me.btnHelp_cmt.Location = New System.Drawing.Point(638, 212)
        Me.btnHelp_cmt.Name = "btnHelp_cmt"
        Me.btnHelp_cmt.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_cmt.TabIndex = 110
        Me.btnHelp_cmt.UseVisualStyleBackColor = True
        '
        'btnHelp_con
        '
        Me.btnHelp_con.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_con.Image = CType(resources.GetObject("btnHelp_con.Image"), System.Drawing.Image)
        Me.btnHelp_con.Location = New System.Drawing.Point(2, 610)
        Me.btnHelp_con.Name = "btnHelp_con"
        Me.btnHelp_con.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_con.TabIndex = 111
        Me.btnHelp_con.UseVisualStyleBackColor = True
        Me.btnHelp_con.Visible = False
        '
        'txtCmt
        '
        Me.txtCmt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmt.Location = New System.Drawing.Point(46, 212)
        Me.txtCmt.MaxLength = 40000
        Me.txtCmt.Multiline = True
        Me.txtCmt.Name = "txtCmt"
        Me.txtCmt.Size = New System.Drawing.Size(591, 131)
        Me.txtCmt.TabIndex = 114
        Me.txtCmt.Tag = "LG11002"
        '
        'lblCmt
        '
        Me.lblCmt.AutoSize = True
        Me.lblCmt.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmt.ForeColor = System.Drawing.Color.Blue
        Me.lblCmt.Location = New System.Drawing.Point(20, 197)
        Me.lblCmt.Name = "lblCmt"
        Me.lblCmt.Size = New System.Drawing.Size(95, 13)
        Me.lblCmt.TabIndex = 113
        Me.lblCmt.Text = "5. Comment:"
        '
        'lblCon
        '
        Me.lblCon.AutoSize = True
        Me.lblCon.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCon.ForeColor = System.Drawing.Color.Blue
        Me.lblCon.Location = New System.Drawing.Point(12, 634)
        Me.lblCon.Name = "lblCon"
        Me.lblCon.Size = New System.Drawing.Size(119, 13)
        Me.lblCon.TabIndex = 115
        Me.lblCon.Text = "7. Conclusion:"
        Me.lblCon.Visible = False
        '
        'txtCon
        '
        Me.txtCon.Location = New System.Drawing.Point(-157, 617)
        Me.txtCon.MaxLength = 40000
        Me.txtCon.Multiline = True
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(522, 80)
        Me.txtCon.TabIndex = 116
        Me.txtCon.Tag = "LH99903"
        Me.txtCon.Visible = False
        '
        'lblSpc
        '
        Me.lblSpc.AutoSize = True
        Me.lblSpc.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpc.ForeColor = System.Drawing.Color.Blue
        Me.lblSpc.Location = New System.Drawing.Point(20, 32)
        Me.lblSpc.Name = "lblSpc"
        Me.lblSpc.Size = New System.Drawing.Size(77, 13)
        Me.lblSpc.TabIndex = 117
        Me.lblSpc.Text = "1. 검체: "
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Blue
        Me.lblDate.Location = New System.Drawing.Point(20, 65)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(205, 13)
        Me.lblDate.TabIndex = 118
        Me.lblDate.Text = "2. 검체채취일/검체의뢰일: "
        '
        'lblMethod
        '
        Me.lblMethod.AutoSize = True
        Me.lblMethod.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMethod.ForeColor = System.Drawing.Color.Blue
        Me.lblMethod.Location = New System.Drawing.Point(20, 99)
        Me.lblMethod.Name = "lblMethod"
        Me.lblMethod.Size = New System.Drawing.Size(198, 13)
        Me.lblMethod.TabIndex = 119
        Me.lblMethod.Text = "3. 검사목표 및 검사방법: "
        '
        'txtSpcnm
        '
        Me.txtSpcnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcnm.Location = New System.Drawing.Point(92, 26)
        Me.txtSpcnm.Name = "txtSpcnm"
        Me.txtSpcnm.Size = New System.Drawing.Size(148, 21)
        Me.txtSpcnm.TabIndex = 120
        Me.txtSpcnm.Tag = "L177814"
        '
        'txtSpcDate
        '
        Me.txtSpcDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSpcDate.Location = New System.Drawing.Point(219, 62)
        Me.txtSpcDate.Name = "txtSpcDate"
        Me.txtSpcDate.Size = New System.Drawing.Size(416, 21)
        Me.txtSpcDate.TabIndex = 121
        Me.txtSpcDate.Tag = "L177814"
        Me.txtSpcDate.Text = "상기동일 / 상기동일"
        '
        'txtTestnm
        '
        Me.txtTestnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTestnm.Location = New System.Drawing.Point(219, 96)
        Me.txtTestnm.Name = "txtTestnm"
        Me.txtTestnm.Size = New System.Drawing.Size(416, 21)
        Me.txtTestnm.TabIndex = 122
        Me.txtTestnm.Tag = "L177814"
        Me.txtTestnm.Text = "MERS-CoV, real-time RT PCR"
        '
        'btnSpc
        '
        Me.btnSpc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSpc.Image = CType(resources.GetObject("btnSpc.Image"), System.Drawing.Image)
        Me.btnSpc.Location = New System.Drawing.Point(242, 26)
        Me.btnSpc.Name = "btnSpc"
        Me.btnSpc.Size = New System.Drawing.Size(25, 21)
        Me.btnSpc.TabIndex = 123
        Me.btnSpc.UseVisualStyleBackColor = True
        '
        'lblRst
        '
        Me.lblRst.AutoSize = True
        Me.lblRst.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRst.ForeColor = System.Drawing.Color.Blue
        Me.lblRst.Location = New System.Drawing.Point(20, 130)
        Me.lblRst.Name = "lblRst"
        Me.lblRst.Size = New System.Drawing.Size(271, 26)
        Me.lblRst.TabIndex = 124
        Me.lblRst.Text = "4. 검사결과: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   * MERS-CoV, real-time RT-PCR: "
        '
        'txtRst
        '
        Me.txtRst.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRst.Location = New System.Drawing.Point(282, 136)
        Me.txtRst.MaxLength = 40000
        Me.txtRst.Multiline = True
        Me.txtRst.Name = "txtRst"
        Me.txtRst.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtRst.Size = New System.Drawing.Size(353, 43)
        Me.txtRst.TabIndex = 125
        Me.txtRst.Tag = "LG11001"
        '
        'btnRst
        '
        Me.btnRst.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRst.Image = CType(resources.GetObject("btnRst.Image"), System.Drawing.Image)
        Me.btnRst.Location = New System.Drawing.Point(636, 136)
        Me.btnRst.Name = "btnRst"
        Me.btnRst.Size = New System.Drawing.Size(25, 21)
        Me.btnRst.TabIndex = 126
        Me.btnRst.UseVisualStyleBackColor = True
        '
        'lblTest
        '
        Me.lblTest.AutoSize = True
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.Blue
        Me.lblTest.Location = New System.Drawing.Point(20, 373)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(107, 13)
        Me.lblTest.TabIndex = 127
        Me.lblTest.Text = "6. 검사개요: "
        '
        'txtTestinfo
        '
        Me.txtTestinfo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTestinfo.Location = New System.Drawing.Point(121, 373)
        Me.txtTestinfo.MaxLength = 40000
        Me.txtTestinfo.Multiline = True
        Me.txtTestinfo.Name = "txtTestinfo"
        Me.txtTestinfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtTestinfo.Size = New System.Drawing.Size(514, 154)
        Me.txtTestinfo.TabIndex = 128
        Me.txtTestinfo.Tag = "LH99903"
        '
        'btnHelp_test
        '
        Me.btnHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_test.Image = CType(resources.GetObject("btnHelp_test.Image"), System.Drawing.Image)
        Me.btnHelp_test.Location = New System.Drawing.Point(636, 373)
        Me.btnHelp_test.Name = "btnHelp_test"
        Me.btnHelp_test.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_test.TabIndex = 129
        Me.btnHelp_test.UseVisualStyleBackColor = True
        Me.btnHelp_test.Visible = False
        '
        'FGPOPUPST_MERS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(672, 656)
        Me.Controls.Add(Me.btnHelp_test)
        Me.Controls.Add(Me.txtTestinfo)
        Me.Controls.Add(Me.lblTest)
        Me.Controls.Add(Me.btnRst)
        Me.Controls.Add(Me.txtRst)
        Me.Controls.Add(Me.lblRst)
        Me.Controls.Add(Me.btnSpc)
        Me.Controls.Add(Me.txtTestnm)
        Me.Controls.Add(Me.txtSpcDate)
        Me.Controls.Add(Me.txtSpcnm)
        Me.Controls.Add(Me.lblMethod)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblSpc)
        Me.Controls.Add(Me.txtCon)
        Me.Controls.Add(Me.lblCon)
        Me.Controls.Add(Me.txtCmt)
        Me.Controls.Add(Me.lblCmt)
        Me.Controls.Add(Me.btnHelp_con)
        Me.Controls.Add(Me.btnHelp_cmt)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGPOPUPST_MERS"
        Me.Text = "특수검사 모듈 (MERS)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHelp_cmt As System.Windows.Forms.Button
    Friend WithEvents btnHelp_con As System.Windows.Forms.Button
    Friend WithEvents txtCmt As System.Windows.Forms.TextBox
    Friend WithEvents lblCmt As System.Windows.Forms.Label
    Friend WithEvents lblCon As System.Windows.Forms.Label
    Friend WithEvents txtCon As System.Windows.Forms.TextBox
    Friend WithEvents lblSpc As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblMethod As System.Windows.Forms.Label
    Friend WithEvents txtSpcnm As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcDate As System.Windows.Forms.TextBox
    Friend WithEvents txtTestnm As System.Windows.Forms.TextBox
    Friend WithEvents btnSpc As System.Windows.Forms.Button
    Friend WithEvents lblRst As System.Windows.Forms.Label
    Friend WithEvents txtRst As System.Windows.Forms.TextBox
    Friend WithEvents btnRst As System.Windows.Forms.Button
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents txtTestinfo As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_test As System.Windows.Forms.Button
End Class
