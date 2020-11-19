<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_BM
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_BM))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCmt1 = New System.Windows.Forms.TextBox
        Me.txtCmt2 = New System.Windows.Forms.TextBox
        Me.txtCmt3 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTCnt = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtM = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtWBC = New System.Windows.Forms.TextBox
        Me.txtHb = New System.Windows.Forms.TextBox
        Me.txtPLT = New System.Windows.Forms.TextBox
        Me.txtReti = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdRst1 = New AxFPSpreadADO.AxfpSpread
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spdRst2 = New AxFPSpreadADO.AxfpSpread
        Me.txtBMB = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdRst1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.spdRst2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(619, 772)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 36)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(534, 772)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 36)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "저장(F2)"
        '
        'Label1
        '
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 39)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 30)
        Me.Label1.TabIndex = 87
        Me.Label1.Text = "적혈구"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 71)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 30)
        Me.Label2.TabIndex = 88
        Me.Label2.Text = "백혈구"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(12, 103)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 30)
        Me.Label3.TabIndex = 89
        Me.Label3.Text = "혈소판"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCmt1
        '
        Me.txtCmt1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmt1.Location = New System.Drawing.Point(106, 39)
        Me.txtCmt1.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCmt1.Multiline = True
        Me.txtCmt1.Name = "txtCmt1"
        Me.txtCmt1.Size = New System.Drawing.Size(593, 30)
        Me.txtCmt1.TabIndex = 4
        '
        'txtCmt2
        '
        Me.txtCmt2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmt2.Location = New System.Drawing.Point(106, 71)
        Me.txtCmt2.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCmt2.Multiline = True
        Me.txtCmt2.Name = "txtCmt2"
        Me.txtCmt2.Size = New System.Drawing.Size(593, 30)
        Me.txtCmt2.TabIndex = 5
        '
        'txtCmt3
        '
        Me.txtCmt3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmt3.Location = New System.Drawing.Point(106, 103)
        Me.txtCmt3.Margin = New System.Windows.Forms.Padding(1)
        Me.txtCmt3.Multiline = True
        Me.txtCmt3.Name = "txtCmt3"
        Me.txtCmt3.Size = New System.Drawing.Size(593, 30)
        Me.txtCmt3.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 140)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 23)
        Me.Label4.TabIndex = 93
        Me.Label4.Text = " 골수천자도말 관찰 소견"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(274, 145)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 12)
        Me.Label5.TabIndex = 94
        Me.Label5.Text = "TotalCount"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTCnt
        '
        Me.txtTCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTCnt.Location = New System.Drawing.Point(353, 140)
        Me.txtTCnt.Name = "txtTCnt"
        Me.txtTCnt.Size = New System.Drawing.Size(83, 21)
        Me.txtTCnt.TabIndex = 7
        Me.txtTCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(440, 145)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 96
        Me.Label6.Text = "세포"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.Location = New System.Drawing.Point(519, 145)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 12)
        Me.Label7.TabIndex = 97
        Me.Label7.Text = "M : E ratio"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtM
        '
        Me.txtM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtM.Location = New System.Drawing.Point(594, 140)
        Me.txtM.Name = "txtM"
        Me.txtM.Size = New System.Drawing.Size(75, 21)
        Me.txtM.TabIndex = 8
        Me.txtM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(673, 145)
        Me.Label8.Margin = New System.Windows.Forms.Padding(1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 12)
        Me.Label8.TabIndex = 99
        Me.Label8.Text = ": 1"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 8)
        Me.Label9.Margin = New System.Windows.Forms.Padding(1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(163, 23)
        Me.Label9.TabIndex = 100
        Me.Label9.Text = " 말초혈액도말 관찰 소견"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(274, 13)
        Me.Label10.Margin = New System.Windows.Forms.Padding(1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(131, 12)
        Me.Label10.TabIndex = 101
        Me.Label10.Text = "WBC - Hb - PLT - Reti"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtWBC
        '
        Me.txtWBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWBC.Location = New System.Drawing.Point(497, 8)
        Me.txtWBC.Name = "txtWBC"
        Me.txtWBC.Size = New System.Drawing.Size(37, 21)
        Me.txtWBC.TabIndex = 0
        Me.txtWBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtHb
        '
        Me.txtHb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHb.Location = New System.Drawing.Point(553, 8)
        Me.txtHb.Name = "txtHb"
        Me.txtHb.Size = New System.Drawing.Size(37, 21)
        Me.txtHb.TabIndex = 1
        Me.txtHb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPLT
        '
        Me.txtPLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPLT.Location = New System.Drawing.Point(608, 8)
        Me.txtPLT.Name = "txtPLT"
        Me.txtPLT.Size = New System.Drawing.Size(37, 21)
        Me.txtPLT.TabIndex = 2
        Me.txtPLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtReti
        '
        Me.txtReti.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReti.Location = New System.Drawing.Point(662, 8)
        Me.txtReti.Name = "txtReti"
        Me.txtReti.Size = New System.Drawing.Size(37, 21)
        Me.txtReti.TabIndex = 3
        Me.txtReti.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.Location = New System.Drawing.Point(649, 13)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(11, 12)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "-"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.Location = New System.Drawing.Point(593, 13)
        Me.Label12.Margin = New System.Windows.Forms.Padding(1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(11, 12)
        Me.Label12.TabIndex = 107
        Me.Label12.Text = "-"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.Location = New System.Drawing.Point(538, 13)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(11, 12)
        Me.Label13.TabIndex = 108
        Me.Label13.Text = "-"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.spdRst1)
        Me.Panel1.Location = New System.Drawing.Point(12, 170)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(686, 155)
        Me.Panel1.TabIndex = 9
        '
        'spdRst1
        '
        Me.spdRst1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdRst1.DataSource = Nothing
        Me.spdRst1.Location = New System.Drawing.Point(0, 0)
        Me.spdRst1.Name = "spdRst1"
        Me.spdRst1.OcxState = CType(resources.GetObject("spdRst1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst1.Size = New System.Drawing.Size(684, 155)
        Me.spdRst1.TabIndex = 9
        '
        'txtComment
        '
        Me.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComment.Location = New System.Drawing.Point(57, 329)
        Me.txtComment.Margin = New System.Windows.Forms.Padding(1)
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.Size = New System.Drawing.Size(588, 107)
        Me.txtComment.TabIndex = 10
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.Location = New System.Drawing.Point(58, 446)
        Me.Label14.Margin = New System.Windows.Forms.Padding(1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 12)
        Me.Label14.TabIndex = 111
        Me.Label14.Text = "● BMB -"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.spdRst2)
        Me.Panel2.Location = New System.Drawing.Point(12, 470)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(686, 284)
        Me.Panel2.TabIndex = 12
        '
        'spdRst2
        '
        Me.spdRst2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdRst2.DataSource = Nothing
        Me.spdRst2.Location = New System.Drawing.Point(0, 0)
        Me.spdRst2.Name = "spdRst2"
        Me.spdRst2.OcxState = CType(resources.GetObject("spdRst2.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRst2.Size = New System.Drawing.Size(687, 283)
        Me.spdRst2.TabIndex = 12
        '
        'txtBMB
        '
        Me.txtBMB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBMB.Location = New System.Drawing.Point(115, 440)
        Me.txtBMB.Name = "txtBMB"
        Me.txtBMB.Size = New System.Drawing.Size(530, 21)
        Me.txtBMB.TabIndex = 11
        Me.txtBMB.Text = "Indequate specimen"
        '
        'FGPOPUPST_BM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(712, 816)
        Me.Controls.Add(Me.txtBMB)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtComment)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtReti)
        Me.Controls.Add(Me.txtPLT)
        Me.Controls.Add(Me.txtHb)
        Me.Controls.Add(Me.txtWBC)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtM)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTCnt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCmt3)
        Me.Controls.Add(Me.txtCmt2)
        Me.Controls.Add(Me.txtCmt1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGPOPUPST_BM"
        Me.Text = "특수검사 모듈 (BM)"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdRst1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdRst2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCmt1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCmt2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCmt3 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtTCnt As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtM As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtWBC As System.Windows.Forms.TextBox
    Friend WithEvents txtHb As System.Windows.Forms.TextBox
    Friend WithEvents txtPLT As System.Windows.Forms.TextBox
    Friend WithEvents txtReti As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdRst1 As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents spdRst2 As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtBMB As System.Windows.Forms.TextBox
End Class
