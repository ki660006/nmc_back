﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_NCOV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_NCOV))
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnHelp_cmt1 = New System.Windows.Forms.Button()
        Me.btnHelp_con = New System.Windows.Forms.Button()
        Me.txtCmt1 = New System.Windows.Forms.TextBox()
        Me.lblCmt = New System.Windows.Forms.Label()
        Me.lblCon = New System.Windows.Forms.Label()
        Me.txtCon = New System.Windows.Forms.TextBox()
        Me.lblSpc = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblMethod = New System.Windows.Forms.Label()
        Me.txtSpcnm = New System.Windows.Forms.TextBox()
        Me.txtSpcDate = New System.Windows.Forms.TextBox()
        Me.txtTestnm = New System.Windows.Forms.TextBox()
        Me.btnSpc = New System.Windows.Forms.Button()
        Me.lblRst = New System.Windows.Forms.Label()
        Me.txtRst = New System.Windows.Forms.TextBox()
        Me.btnRst = New System.Windows.Forms.Button()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.txtTestinfo = New System.Windows.Forms.TextBox()
        Me.btnHelp_test = New System.Windows.Forms.Button()
        Me.txtCmt2 = New System.Windows.Forms.TextBox()
        Me.txtCmt3 = New System.Windows.Forms.TextBox()
        Me.btnHelp_cmt2 = New System.Windows.Forms.Button()
        Me.btnHelp_cmt3 = New System.Windows.Forms.Button()
        Me.lblbfRst = New System.Windows.Forms.Label()
        Me.txtbfRst = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(566, 727)
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
        Me.btnSave.Location = New System.Drawing.Point(467, 727)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(93, 36)
        Me.btnSave.TabIndex = 82
        Me.btnSave.Text = "저장(F2)"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnHelp_cmt1
        '
        Me.btnHelp_cmt1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_cmt1.Image = CType(resources.GetObject("btnHelp_cmt1.Image"), System.Drawing.Image)
        Me.btnHelp_cmt1.Location = New System.Drawing.Point(635, 214)
        Me.btnHelp_cmt1.Name = "btnHelp_cmt1"
        Me.btnHelp_cmt1.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_cmt1.TabIndex = 110
        Me.btnHelp_cmt1.UseVisualStyleBackColor = True
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
        'txtCmt1
        '
        Me.txtCmt1.Location = New System.Drawing.Point(46, 214)
        Me.txtCmt1.MaxLength = 40000
        Me.txtCmt1.Multiline = True
        Me.txtCmt1.Name = "txtCmt1"
        Me.txtCmt1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCmt1.Size = New System.Drawing.Size(588, 61)
        Me.txtCmt1.TabIndex = 114
        Me.txtCmt1.Tag = "LG11102"
        '
        'lblCmt
        '
        Me.lblCmt.AutoSize = True
        Me.lblCmt.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmt.ForeColor = System.Drawing.Color.Blue
        Me.lblCmt.Location = New System.Drawing.Point(20, 198)
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
        Me.lblSpc.Location = New System.Drawing.Point(20, 20)
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
        Me.lblDate.Location = New System.Drawing.Point(20, 54)
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
        Me.lblMethod.Location = New System.Drawing.Point(20, 94)
        Me.lblMethod.Name = "lblMethod"
        Me.lblMethod.Size = New System.Drawing.Size(198, 13)
        Me.lblMethod.TabIndex = 119
        Me.lblMethod.Text = "3. 검사목표 및 검사방법: "
        '
        'txtSpcnm
        '
        Me.txtSpcnm.Location = New System.Drawing.Point(93, 17)
        Me.txtSpcnm.Name = "txtSpcnm"
        Me.txtSpcnm.Size = New System.Drawing.Size(302, 21)
        Me.txtSpcnm.TabIndex = 120
        Me.txtSpcnm.Tag = "L177814"
        '
        'txtSpcDate
        '
        Me.txtSpcDate.Location = New System.Drawing.Point(220, 51)
        Me.txtSpcDate.Name = "txtSpcDate"
        Me.txtSpcDate.Size = New System.Drawing.Size(414, 21)
        Me.txtSpcDate.TabIndex = 121
        Me.txtSpcDate.Tag = "L177814"
        Me.txtSpcDate.Text = "상기동일 / 상기동일"
        '
        'txtTestnm
        '
        Me.txtTestnm.Location = New System.Drawing.Point(219, 91)
        Me.txtTestnm.Name = "txtTestnm"
        Me.txtTestnm.Size = New System.Drawing.Size(415, 21)
        Me.txtTestnm.TabIndex = 122
        Me.txtTestnm.Tag = "L177814"
        Me.txtTestnm.Text = "Novel coronavirus (2019-nCoV) [real-time RT-PCR]"
        '
        'btnSpc
        '
        Me.btnSpc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSpc.Image = CType(resources.GetObject("btnSpc.Image"), System.Drawing.Image)
        Me.btnSpc.Location = New System.Drawing.Point(396, 17)
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
        Me.lblRst.Location = New System.Drawing.Point(20, 131)
        Me.lblRst.Name = "lblRst"
        Me.lblRst.Size = New System.Drawing.Size(287, 26)
        Me.lblRst.TabIndex = 124
        Me.lblRst.Text = "4. 검사결과: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   * 2019-nCoV [real-time RT-PCR]: "
        '
        'txtRst
        '
        Me.txtRst.Location = New System.Drawing.Point(303, 144)
        Me.txtRst.MaxLength = 40000
        Me.txtRst.Multiline = True
        Me.txtRst.Name = "txtRst"
        Me.txtRst.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtRst.Size = New System.Drawing.Size(331, 52)
        Me.txtRst.TabIndex = 125
        Me.txtRst.Tag = "LG11101"
        '
        'btnRst
        '
        Me.btnRst.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRst.Image = CType(resources.GetObject("btnRst.Image"), System.Drawing.Image)
        Me.btnRst.Location = New System.Drawing.Point(635, 144)
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
        Me.lblTest.Location = New System.Drawing.Point(20, 558)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(107, 13)
        Me.lblTest.TabIndex = 127
        Me.lblTest.Text = "7. 검사개요: "
        '
        'txtTestinfo
        '
        Me.txtTestinfo.Location = New System.Drawing.Point(119, 558)
        Me.txtTestinfo.MaxLength = 40000
        Me.txtTestinfo.Multiline = True
        Me.txtTestinfo.Name = "txtTestinfo"
        Me.txtTestinfo.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
        Me.txtTestinfo.Size = New System.Drawing.Size(515, 144)
        Me.txtTestinfo.TabIndex = 128
        Me.txtTestinfo.Tag = "LH99903"
        '
        'btnHelp_test
        '
        Me.btnHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_test.Image = CType(resources.GetObject("btnHelp_test.Image"), System.Drawing.Image)
        Me.btnHelp_test.Location = New System.Drawing.Point(635, 558)
        Me.btnHelp_test.Name = "btnHelp_test"
        Me.btnHelp_test.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_test.TabIndex = 129
        Me.btnHelp_test.UseVisualStyleBackColor = True
        Me.btnHelp_test.Visible = False
        '
        'txtCmt2
        '
        Me.txtCmt2.Location = New System.Drawing.Point(46, 280)
        Me.txtCmt2.MaxLength = 40000
        Me.txtCmt2.Multiline = True
        Me.txtCmt2.Name = "txtCmt2"
        Me.txtCmt2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCmt2.Size = New System.Drawing.Size(588, 61)
        Me.txtCmt2.TabIndex = 131
        Me.txtCmt2.Tag = "LG11102"
        '
        'txtCmt3
        '
        Me.txtCmt3.Location = New System.Drawing.Point(46, 345)
        Me.txtCmt3.MaxLength = 40000
        Me.txtCmt3.Multiline = True
        Me.txtCmt3.Name = "txtCmt3"
        Me.txtCmt3.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCmt3.Size = New System.Drawing.Size(588, 61)
        Me.txtCmt3.TabIndex = 132
        Me.txtCmt3.Tag = "LG11102"
        '
        'btnHelp_cmt2
        '
        Me.btnHelp_cmt2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_cmt2.Image = CType(resources.GetObject("btnHelp_cmt2.Image"), System.Drawing.Image)
        Me.btnHelp_cmt2.Location = New System.Drawing.Point(635, 282)
        Me.btnHelp_cmt2.Name = "btnHelp_cmt2"
        Me.btnHelp_cmt2.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_cmt2.TabIndex = 133
        Me.btnHelp_cmt2.UseVisualStyleBackColor = True
        '
        'btnHelp_cmt3
        '
        Me.btnHelp_cmt3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_cmt3.Image = CType(resources.GetObject("btnHelp_cmt3.Image"), System.Drawing.Image)
        Me.btnHelp_cmt3.Location = New System.Drawing.Point(635, 347)
        Me.btnHelp_cmt3.Name = "btnHelp_cmt3"
        Me.btnHelp_cmt3.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_cmt3.TabIndex = 134
        Me.btnHelp_cmt3.UseVisualStyleBackColor = True
        '
        'lblbfRst
        '
        Me.lblbfRst.AutoSize = True
        Me.lblbfRst.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblbfRst.ForeColor = System.Drawing.Color.Blue
        Me.lblbfRst.Location = New System.Drawing.Point(20, 416)
        Me.lblbfRst.Name = "lblbfRst"
        Me.lblbfRst.Size = New System.Drawing.Size(107, 13)
        Me.lblbfRst.TabIndex = 135
        Me.lblbfRst.Text = "6. 이전결과: "
        '
        'txtbfRst
        '
        Me.txtbfRst.Location = New System.Drawing.Point(46, 432)
        Me.txtbfRst.MaxLength = 40000
        Me.txtbfRst.Multiline = True
        Me.txtbfRst.Name = "txtbfRst"
        Me.txtbfRst.ReadOnly = True
        Me.txtbfRst.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtbfRst.Size = New System.Drawing.Size(613, 120)
        Me.txtbfRst.TabIndex = 136
        Me.txtbfRst.Tag = "LG11102"
        Me.txtbfRst.Text = "접수일자         검체번호           검체명                   결과" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "2020-02-11       20200211G1" & _
            "00010    Sputum"
        '
        'FGPOPUPST_NCOV
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(672, 775)
        Me.Controls.Add(Me.txtbfRst)
        Me.Controls.Add(Me.lblbfRst)
        Me.Controls.Add(Me.btnHelp_cmt3)
        Me.Controls.Add(Me.btnHelp_cmt2)
        Me.Controls.Add(Me.txtCmt3)
        Me.Controls.Add(Me.txtCmt2)
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
        Me.Controls.Add(Me.txtCmt1)
        Me.Controls.Add(Me.lblCmt)
        Me.Controls.Add(Me.btnHelp_con)
        Me.Controls.Add(Me.btnHelp_cmt1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGPOPUPST_NCOV"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "특수검사 모듈 (신종코로나바이러스)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnHelp_cmt1 As System.Windows.Forms.Button
    Friend WithEvents btnHelp_con As System.Windows.Forms.Button
    Friend WithEvents txtCmt1 As System.Windows.Forms.TextBox
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
    Friend WithEvents txtCmt2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCmt3 As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_cmt2 As System.Windows.Forms.Button
    Friend WithEvents btnHelp_cmt3 As System.Windows.Forms.Button
    Friend WithEvents lblbfRst As System.Windows.Forms.Label
    Friend WithEvents txtbfRst As System.Windows.Forms.TextBox
End Class
