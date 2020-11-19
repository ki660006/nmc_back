<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF47
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
        Me.components = New System.ComponentModel.Container
        Me.tbcBody = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.chkDelflg = New System.Windows.Forms.CheckBox
        Me.cboCmtGbn = New System.Windows.Forms.ComboBox
        Me.lblCmtGbn = New System.Windows.Forms.Label
        Me.txtCmtCd = New System.Windows.Forms.TextBox
        Me.lblCmtCd = New System.Windows.Forms.Label
        Me.btnDel = New System.Windows.Forms.Button
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.lblCmtCont = New System.Windows.Forms.Label
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tbcBody.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbcBody
        '
        Me.tbcBody.Controls.Add(Me.tbcTpg)
        Me.tbcBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbcBody.Location = New System.Drawing.Point(0, 0)
        Me.tbcBody.Name = "tbcBody"
        Me.tbcBody.SelectedIndex = 0
        Me.tbcBody.Size = New System.Drawing.Size(792, 605)
        Me.tbcBody.TabIndex = 1
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.lblModNm)
        Me.tbcTpg.Controls.Add(Me.txtModDT)
        Me.tbcTpg.Controls.Add(Me.lblModDT)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(784, 580)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "기타 코드"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(292, 539)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 199
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(292, 539)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
        Me.txtModID.TabIndex = 22
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(706, 539)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 200
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(207, 539)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 21
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(96, 539)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 20
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(10, 539)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 19
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(509, 539)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 16
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(621, 539)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 15
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(424, 539)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 18
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(706, 539)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 17
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.chkDelflg)
        Me.grpCd.Controls.Add(Me.cboCmtGbn)
        Me.grpCd.Controls.Add(Me.lblCmtGbn)
        Me.grpCd.Controls.Add(Me.txtCmtCd)
        Me.grpCd.Controls.Add(Me.lblCmtCd)
        Me.grpCd.Controls.Add(Me.btnDel)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 44)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'chkDelflg
        '
        Me.chkDelflg.AutoSize = True
        Me.chkDelflg.Location = New System.Drawing.Point(418, 18)
        Me.chkDelflg.Name = "chkDelflg"
        Me.chkDelflg.Size = New System.Drawing.Size(72, 16)
        Me.chkDelflg.TabIndex = 7
        Me.chkDelflg.Tag = "DELFLG"
        Me.chkDelflg.Text = "사용안함"
        Me.chkDelflg.UseVisualStyleBackColor = True
        '
        'cboCmtGbn
        '
        Me.cboCmtGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmtGbn.Location = New System.Drawing.Point(73, 14)
        Me.cboCmtGbn.Name = "cboCmtGbn"
        Me.cboCmtGbn.Size = New System.Drawing.Size(196, 20)
        Me.cboCmtGbn.TabIndex = 1
        Me.cboCmtGbn.Tag = "CMTGBN_01"
        '
        'lblCmtGbn
        '
        Me.lblCmtGbn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCmtGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtGbn.ForeColor = System.Drawing.Color.White
        Me.lblCmtGbn.Location = New System.Drawing.Point(8, 14)
        Me.lblCmtGbn.Name = "lblCmtGbn"
        Me.lblCmtGbn.Size = New System.Drawing.Size(64, 21)
        Me.lblCmtGbn.TabIndex = 4
        Me.lblCmtGbn.Text = " 구분"
        Me.lblCmtGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCd.Location = New System.Drawing.Point(357, 14)
        Me.txtCmtCd.MaxLength = 4
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(40, 21)
        Me.txtCmtCd.TabIndex = 1
        Me.txtCmtCd.Tag = "CMTCD"
        '
        'lblCmtCd
        '
        Me.lblCmtCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCmtCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCd.ForeColor = System.Drawing.Color.White
        Me.lblCmtCd.Location = New System.Drawing.Point(292, 14)
        Me.lblCmtCd.Name = "lblCmtCd"
        Me.lblCmtCd.Size = New System.Drawing.Size(64, 21)
        Me.lblCmtCd.TabIndex = 0
        Me.lblCmtCd.Text = " 코드"
        Me.lblCmtCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnDel
        '
        Me.btnDel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnDel.Enabled = False
        Me.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDel.ForeColor = System.Drawing.Color.White
        Me.btnDel.Location = New System.Drawing.Point(687, 13)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(72, 27)
        Me.btnDel.TabIndex = 0
        Me.btnDel.TabStop = False
        Me.btnDel.Text = "코드삭제"
        Me.btnDel.UseVisualStyleBackColor = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.lblCmtCont)
        Me.grpCdInfo1.Controls.Add(Me.txtCmtCont)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(768, 472)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "코드 정보"
        '
        'lblCmtCont
        '
        Me.lblCmtCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCont.ForeColor = System.Drawing.Color.White
        Me.lblCmtCont.Location = New System.Drawing.Point(8, 29)
        Me.lblCmtCont.Name = "lblCmtCont"
        Me.lblCmtCont.Size = New System.Drawing.Size(63, 21)
        Me.lblCmtCont.TabIndex = 0
        Me.lblCmtCont.Text = " 내용"
        Me.lblCmtCont.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCmtCont
        '
        Me.txtCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(72, 29)
        Me.txtCmtCont.MaxLength = 200
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(684, 21)
        Me.txtCmtCont.TabIndex = 3
        Me.txtCmtCont.Tag = "CMTCONT"
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF47
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 605)
        Me.Controls.Add(Me.tbcBody)
        Me.Name = "FDF47"
        Me.Text = "[47] 기타 사유"
        Me.tbcBody.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tbcBody As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents lblCmtCd As System.Windows.Forms.Label
    Friend WithEvents btnDel As System.Windows.Forms.Button
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCmtCont As System.Windows.Forms.Label
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents cboCmtGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblCmtGbn As System.Windows.Forms.Label
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents chkDelflg As System.Windows.Forms.CheckBox
End Class
