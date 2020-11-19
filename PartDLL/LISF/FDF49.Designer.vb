<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF49
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF49))
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.lblModNM = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.cmuLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuHelp_cmt = New System.Windows.Forms.ToolStripMenuItem
        Me.spdItem = New AxFPSpreadADO.AxfpSpread
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnCdHelp_Test = New System.Windows.Forms.Button
        Me.cboPartCd = New System.Windows.Forms.ComboBox
        Me.lblPartCd = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.cmuLink.SuspendLayout()
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCd.SuspendLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tclSpc
        '
        Me.tclSpc.Controls.Add(Me.tbcTpg)
        Me.tclSpc.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tclSpc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tclSpc.ItemSize = New System.Drawing.Size(84, 17)
        Me.tclSpc.Location = New System.Drawing.Point(0, 0)
        Me.tclSpc.Name = "tclSpc"
        Me.tclSpc.SelectedIndex = 0
        Me.tclSpc.Size = New System.Drawing.Size(792, 600)
        Me.tclSpc.TabIndex = 1
        Me.tclSpc.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.lblModNM)
        Me.tbcTpg.Controls.Add(Me.txtModDT)
        Me.tbcTpg.Controls.Add(Me.lblModDT)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(784, 575)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "종합검증 소견 정보"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(318, 545)
        Me.txtModID.Margin = New System.Windows.Forms.Padding(0)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(70, 21)
        Me.txtModID.TabIndex = 6
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        Me.txtModID.Visible = False
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(705, 545)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(70, 21)
        Me.txtRegNm.TabIndex = 200
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'lblModNM
        '
        Me.lblModNM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNM.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNM.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNM.ForeColor = System.Drawing.Color.Black
        Me.lblModNM.Location = New System.Drawing.Point(225, 545)
        Me.lblModNM.Margin = New System.Windows.Forms.Padding(0)
        Me.lblModNM.Name = "lblModNM"
        Me.lblModNM.Size = New System.Drawing.Size(92, 22)
        Me.lblModNM.TabIndex = 5
        Me.lblModNM.Text = " 변경삭제자"
        Me.lblModNM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblModNM.Visible = False
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(98, 545)
        Me.txtModDT.Margin = New System.Windows.Forms.Padding(0)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(120, 21)
        Me.txtModDT.TabIndex = 4
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        Me.txtModDT.Visible = False
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(4, 544)
        Me.lblModDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(93, 22)
        Me.lblModDT.TabIndex = 3
        Me.lblModDT.Text = " 변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblModDT.Visible = False
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(487, 545)
        Me.txtRegDT.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(120, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(613, 545)
        Me.lblUserNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(91, 22)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = " 최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(395, 545)
        Me.lblRegDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(91, 22)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = " 최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(705, 545)
        Me.txtRegID.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(70, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        Me.txtRegID.Visible = False
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.ContextMenuStrip = Me.cmuLink
        Me.grpCdInfo1.Controls.Add(Me.spdItem)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(9, 52)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(767, 488)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = " 검사항목별 소견 정보"
        '
        'cmuLink
        '
        Me.cmuLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuHelp_cmt})
        Me.cmuLink.Name = "cmuRstList"
        Me.cmuLink.Size = New System.Drawing.Size(151, 26)
        Me.cmuLink.Text = "상황에 맞는 메뉴"
        '
        'mnuHelp_cmt
        '
        Me.mnuHelp_cmt.Name = "mnuHelp_cmt"
        Me.mnuHelp_cmt.Size = New System.Drawing.Size(150, 22)
        Me.mnuHelp_cmt.Text = "소견코드 보기"
        '
        'spdItem
        '
        Me.spdItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdItem.ContextMenuStrip = Me.cmuLink
        Me.spdItem.DataSource = Nothing
        Me.spdItem.Location = New System.Drawing.Point(15, 35)
        Me.spdItem.Name = "spdItem"
        Me.spdItem.OcxState = CType(resources.GetObject("spdItem.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdItem.Size = New System.Drawing.Size(736, 435)
        Me.spdItem.TabIndex = 4
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnCdHelp_Test)
        Me.grpCd.Controls.Add(Me.cboPartCd)
        Me.grpCd.Controls.Add(Me.lblPartCd)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(7, 4)
        Me.grpCd.Margin = New System.Windows.Forms.Padding(0)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(769, 44)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        '
        'btnCdHelp_Test
        '
        Me.btnCdHelp_Test.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCdHelp_Test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_Test.Image = CType(resources.GetObject("btnCdHelp_Test.Image"), System.Drawing.Image)
        Me.btnCdHelp_Test.Location = New System.Drawing.Point(495, 15)
        Me.btnCdHelp_Test.Name = "btnCdHelp_Test"
        Me.btnCdHelp_Test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_Test.TabIndex = 3
        Me.btnCdHelp_Test.TabStop = False
        Me.btnCdHelp_Test.UseVisualStyleBackColor = False
        '
        'cboPartCd
        '
        Me.cboPartCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartCd.FormattingEnabled = True
        Me.cboPartCd.Location = New System.Drawing.Point(92, 14)
        Me.cboPartCd.Margin = New System.Windows.Forms.Padding(0)
        Me.cboPartCd.Name = "cboPartCd"
        Me.cboPartCd.Size = New System.Drawing.Size(227, 20)
        Me.cboPartCd.TabIndex = 1
        '
        'lblPartCd
        '
        Me.lblPartCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartCd.ForeColor = System.Drawing.Color.White
        Me.lblPartCd.Location = New System.Drawing.Point(15, 14)
        Me.lblPartCd.Name = "lblPartCd"
        Me.lblPartCd.Size = New System.Drawing.Size(76, 21)
        Me.lblPartCd.TabIndex = 96
        Me.lblPartCd.Text = " 부서"
        Me.lblPartCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(432, 15)
        Me.txtTestCd.MaxLength = 10
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(62, 21)
        Me.txtTestCd.TabIndex = 2
        Me.txtTestCd.Tag = ""
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(362, 15)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(69, 21)
        Me.lblTestCd.TabIndex = 7
        Me.lblTestCd.Text = " 검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(672, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 0
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF49
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 600)
        Me.Controls.Add(Me.tclSpc)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FDF49"
        Me.Text = "[49] 종합검증 검사항목별 설정 등록"
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.cmuLink.ResumeLayout(False)
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNM As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents spdItem As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cboPartCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblPartCd As System.Windows.Forms.Label
    Friend WithEvents btnCdHelp_Test As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents cmuLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuHelp_cmt As System.Windows.Forms.ToolStripMenuItem
End Class
