<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPSP_RST
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPSP_RST))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCmt = New System.Windows.Forms.TextBox
        Me.btnCdHelp = New System.Windows.Forms.Button
        Me.txtCmtCd = New System.Windows.Forms.TextBox
        Me.spdRsltList = New AxFPSpreadADO.AxfpSpread
        Me.lblWardroom = New System.Windows.Forms.Label
        Me.lblDeptnm = New System.Windows.Forms.Label
        Me.lblPatnm = New System.Windows.Forms.Label
        Me.lblno = New System.Windows.Forms.Label
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnToggle = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblSearch = New System.Windows.Forms.Label
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.btnClose = New AxAckButton.AxImgButton
        Me.btnCReg = New AxAckButton.AxImgButton
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.cmuRstList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDel_sp = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cboCmt = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.rdoB = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboBccls = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label39 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.rdoA = New System.Windows.Forms.RadioButton
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lblSickRoomT = New System.Windows.Forms.Label
        Me.lblWardT = New System.Windows.Forms.Label
        Me.cboWard = New System.Windows.Forms.ComboBox
        Me.cboSR = New System.Windows.Forms.ComboBox
        Me.lblDelInfo = New System.Windows.Forms.Label
        Me.btnExit = New AxAckButton.AxImgButton
        Me.spdSpcList = New AxFPSpreadADO.AxfpSpread
        Me.btnPrint = New AxAckButton.AxImgButton
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label32 = New System.Windows.Forms.Label
        Me.dtpEnd = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.dtpStart = New System.Windows.Forms.DateTimePicker
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.AxvaSpread1 = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdRsltList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.cmuRstList.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdSpcList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.AxvaSpread1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtCmt)
        Me.GroupBox1.Controls.Add(Me.btnCdHelp)
        Me.GroupBox1.Controls.Add(Me.txtCmtCd)
        Me.GroupBox1.Controls.Add(Me.spdRsltList)
        Me.GroupBox1.Controls.Add(Me.lblWardroom)
        Me.GroupBox1.Controls.Add(Me.lblDeptnm)
        Me.GroupBox1.Controls.Add(Me.lblPatnm)
        Me.GroupBox1.Controls.Add(Me.lblno)
        Me.GroupBox1.Controls.Add(Me.txtCmtCont)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnToggle)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblSearch)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(982, 558)
        Me.GroupBox1.TabIndex = 70
        Me.GroupBox1.TabStop = False
        '
        'txtCmt
        '
        Me.txtCmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCmt.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCmt.Location = New System.Drawing.Point(282, 155)
        Me.txtCmt.MaxLength = 0
        Me.txtCmt.Name = "txtCmt"
        Me.txtCmt.Size = New System.Drawing.Size(50, 21)
        Me.txtCmt.TabIndex = 163
        Me.txtCmt.Visible = False
        '
        'btnCdHelp
        '
        Me.btnCdHelp.Location = New System.Drawing.Point(246, 155)
        Me.btnCdHelp.Name = "btnCdHelp"
        Me.btnCdHelp.Size = New System.Drawing.Size(27, 21)
        Me.btnCdHelp.TabIndex = 162
        Me.btnCdHelp.Text = "..."
        Me.btnCdHelp.UseVisualStyleBackColor = True
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCmtCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCmtCd.Location = New System.Drawing.Point(194, 155)
        Me.txtCmtCd.MaxLength = 4
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(50, 21)
        Me.txtCmtCd.TabIndex = 161
        '
        'spdRsltList
        '
        Me.spdRsltList.DataSource = Nothing
        Me.spdRsltList.Location = New System.Drawing.Point(489, 14)
        Me.spdRsltList.Name = "spdRsltList"
        Me.spdRsltList.OcxState = CType(resources.GetObject("spdRsltList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdRsltList.Size = New System.Drawing.Size(470, 538)
        Me.spdRsltList.TabIndex = 160
        '
        'lblWardroom
        '
        Me.lblWardroom.BackColor = System.Drawing.Color.White
        Me.lblWardroom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWardroom.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWardroom.ForeColor = System.Drawing.Color.Black
        Me.lblWardroom.Location = New System.Drawing.Point(67, 103)
        Me.lblWardroom.Name = "lblWardroom"
        Me.lblWardroom.Size = New System.Drawing.Size(127, 22)
        Me.lblWardroom.TabIndex = 158
        Me.lblWardroom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptnm
        '
        Me.lblDeptnm.BackColor = System.Drawing.Color.White
        Me.lblDeptnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDeptnm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptnm.ForeColor = System.Drawing.Color.Black
        Me.lblDeptnm.Location = New System.Drawing.Point(67, 80)
        Me.lblDeptnm.Name = "lblDeptnm"
        Me.lblDeptnm.Size = New System.Drawing.Size(127, 22)
        Me.lblDeptnm.TabIndex = 157
        Me.lblDeptnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatnm
        '
        Me.lblPatnm.BackColor = System.Drawing.Color.White
        Me.lblPatnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPatnm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatnm.ForeColor = System.Drawing.Color.Black
        Me.lblPatnm.Location = New System.Drawing.Point(67, 57)
        Me.lblPatnm.Name = "lblPatnm"
        Me.lblPatnm.Size = New System.Drawing.Size(127, 22)
        Me.lblPatnm.TabIndex = 156
        Me.lblPatnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblno
        '
        Me.lblno.BackColor = System.Drawing.Color.White
        Me.lblno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblno.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblno.ForeColor = System.Drawing.Color.Black
        Me.lblno.Location = New System.Drawing.Point(67, 34)
        Me.lblno.Name = "lblno"
        Me.lblno.Size = New System.Drawing.Size(127, 22)
        Me.lblno.TabIndex = 155
        Me.lblno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCmtCont
        '
        Me.txtCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCont.Location = New System.Drawing.Point(3, 177)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(443, 375)
        Me.txtCmtCont.TabIndex = 81
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(4, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 22)
        Me.Label5.TabIndex = 80
        Me.Label5.Text = "진료과"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(3, 155)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(190, 21)
        Me.Label4.TabIndex = 78
        Me.Label4.Text = "comment"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(4, 103)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 22)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "병동"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(4, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 22)
        Me.Label2.TabIndex = 74
        Me.Label2.Text = "환자명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(195, 12)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(38, 21)
        Me.btnToggle.TabIndex = 18
        Me.btnToggle.Text = "<->"
        Me.btnToggle.Visible = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(4, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 22)
        Me.Label1.TabIndex = 72
        Me.Label1.Text = "등록번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(4, 12)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(62, 21)
        Me.lblSearch.TabIndex = 17
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSearch.Location = New System.Drawing.Point(67, 12)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(127, 21)
        Me.txtSearch.TabIndex = 16
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.ButtonStyle = AxAckButton.AxImgButton.enumButtonStyle.btn96x26
        Me.btnClose.ButtonText = "종  료(Esc)"
        Me.btnClose.Location = New System.Drawing.Point(890, 566)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(1)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(96, 26)
        Me.btnClose.TabIndex = 174
        '
        'btnCReg
        '
        Me.btnCReg.BackColor = System.Drawing.SystemColors.Control
        Me.btnCReg.ButtonStyle = AxAckButton.AxImgButton.enumButtonStyle.btn96x26
        Me.btnCReg.ButtonText = "특이결과등록"
        Me.btnCReg.Location = New System.Drawing.Point(792, 566)
        Me.btnCReg.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCReg.Name = "btnCReg"
        Me.btnCReg.Size = New System.Drawing.Size(96, 26)
        Me.btnCReg.TabIndex = 159
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1000, 622)
        Me.TabControl1.TabIndex = 71
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage2.ContextMenuStrip = Me.cmuRstList
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Controls.Add(Me.Panel2)
        Me.TabPage2.Controls.Add(Me.lblDelInfo)
        Me.TabPage2.Controls.Add(Me.btnExit)
        Me.TabPage2.Controls.Add(Me.spdSpcList)
        Me.TabPage2.Controls.Add(Me.btnPrint)
        Me.TabPage2.Controls.Add(Me.btnQuery)
        Me.TabPage2.Controls.Add(Me.Label32)
        Me.TabPage2.Controls.Add(Me.dtpEnd)
        Me.TabPage2.Controls.Add(Me.Label14)
        Me.TabPage2.Controls.Add(Me.dtpStart)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(992, 597)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "조회"
        '
        'cmuRstList
        '
        Me.cmuRstList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDel_sp})
        Me.cmuRstList.Name = "cmuRstList"
        Me.cmuRstList.Size = New System.Drawing.Size(153, 26)
        Me.cmuRstList.Text = "상황에 맞는 메뉴"
        '
        'mnuDel_sp
        '
        Me.mnuDel_sp.CheckOnClick = True
        Me.mnuDel_sp.Name = "mnuDel_sp"
        Me.mnuDel_sp.Size = New System.Drawing.Size(152, 22)
        Me.mnuDel_sp.Text = "특이결과 삭제"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.cboCmt)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.rdoB)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.cboBccls)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.Label39)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.rdoA)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Location = New System.Drawing.Point(2, 30)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(985, 26)
        Me.Panel1.TabIndex = 167
        '
        'cboCmt
        '
        Me.cboCmt.FormattingEnabled = True
        Me.cboCmt.Location = New System.Drawing.Point(370, 2)
        Me.cboCmt.Margin = New System.Windows.Forms.Padding(1)
        Me.cboCmt.Name = "cboCmt"
        Me.cboCmt.Size = New System.Drawing.Size(163, 20)
        Me.cboCmt.TabIndex = 168
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(309, 2)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(60, 20)
        Me.Label9.TabIndex = 167
        Me.Label9.Text = "소견내용"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rdoB
        '
        Me.rdoB.AutoSize = True
        Me.rdoB.Location = New System.Drawing.Point(115, 5)
        Me.rdoB.Name = "rdoB"
        Me.rdoB.Size = New System.Drawing.Size(47, 16)
        Me.rdoB.TabIndex = 159
        Me.rdoB.TabStop = True
        Me.rdoB.Text = "선택"
        Me.rdoB.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.White
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(904, 2)
        Me.Label11.Margin = New System.Windows.Forms.Padding(0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 21)
        Me.Label11.TabIndex = 166
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label11.Visible = False
        '
        'cboBccls
        '
        Me.cboBccls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls.Items.AddRange(New Object() {"자동화계", "특수계1", "툭스계2"})
        Me.cboBccls.Location = New System.Drawing.Point(165, 2)
        Me.cboBccls.Name = "cboBccls"
        Me.cboBccls.Size = New System.Drawing.Size(127, 20)
        Me.cboBccls.TabIndex = 127
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(818, 2)
        Me.Label12.Margin = New System.Windows.Forms.Padding(0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 21)
        Me.Label12.TabIndex = 165
        Me.Label12.Text = "검체수정율"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label12.Visible = False
        '
        'Label39
        '
        Me.Label39.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label39.ForeColor = System.Drawing.Color.Black
        Me.Label39.Location = New System.Drawing.Point(2, 2)
        Me.Label39.Margin = New System.Windows.Forms.Padding(0)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(62, 21)
        Me.Label39.TabIndex = 125
        Me.Label39.Text = "검체분류"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.White
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(746, 2)
        Me.Label8.Margin = New System.Windows.Forms.Padding(0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(71, 21)
        Me.Label8.TabIndex = 164
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label8.Visible = False
        '
        'rdoA
        '
        Me.rdoA.AutoSize = True
        Me.rdoA.Location = New System.Drawing.Point(68, 5)
        Me.rdoA.Name = "rdoA"
        Me.rdoA.Size = New System.Drawing.Size(47, 16)
        Me.rdoA.TabIndex = 158
        Me.rdoA.TabStop = True
        Me.rdoA.Text = "전체"
        Me.rdoA.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(683, 2)
        Me.Label10.Margin = New System.Windows.Forms.Padding(0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(62, 21)
        Me.Label10.TabIndex = 163
        Me.Label10.Text = "수정건수"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label10.Visible = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(548, 2)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(62, 21)
        Me.Label6.TabIndex = 161
        Me.Label6.Text = "총건수"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label6.Visible = False
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.White
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(611, 2)
        Me.Label7.Margin = New System.Windows.Forms.Padding(0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(71, 21)
        Me.Label7.TabIndex = 162
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label7.Visible = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lblSickRoomT)
        Me.Panel2.Controls.Add(Me.lblWardT)
        Me.Panel2.Controls.Add(Me.cboWard)
        Me.Panel2.Controls.Add(Me.cboSR)
        Me.Panel2.Location = New System.Drawing.Point(0, 32)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(411, 28)
        Me.Panel2.TabIndex = 172
        Me.Panel2.Visible = False
        '
        'lblSickRoomT
        '
        Me.lblSickRoomT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSickRoomT.ForeColor = System.Drawing.Color.Black
        Me.lblSickRoomT.Location = New System.Drawing.Point(239, 0)
        Me.lblSickRoomT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSickRoomT.Name = "lblSickRoomT"
        Me.lblSickRoomT.Size = New System.Drawing.Size(56, 20)
        Me.lblSickRoomT.TabIndex = 8
        Me.lblSickRoomT.Text = "병실"
        Me.lblSickRoomT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWardT
        '
        Me.lblWardT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblWardT.ForeColor = System.Drawing.Color.Black
        Me.lblWardT.Location = New System.Drawing.Point(3, 0)
        Me.lblWardT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWardT.Name = "lblWardT"
        Me.lblWardT.Size = New System.Drawing.Size(62, 20)
        Me.lblWardT.TabIndex = 7
        Me.lblWardT.Text = "병동"
        Me.lblWardT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.DropDownWidth = 162
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(66, 0)
        Me.cboWard.Margin = New System.Windows.Forms.Padding(0)
        Me.cboWard.MaxDropDownItems = 20
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(170, 20)
        Me.cboWard.TabIndex = 5
        '
        'cboSR
        '
        Me.cboSR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSR.DropDownWidth = 87
        Me.cboSR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSR.Location = New System.Drawing.Point(296, 0)
        Me.cboSR.Margin = New System.Windows.Forms.Padding(0)
        Me.cboSR.MaxDropDownItems = 20
        Me.cboSR.Name = "cboSR"
        Me.cboSR.Size = New System.Drawing.Size(75, 20)
        Me.cboSR.TabIndex = 6
        '
        'lblDelInfo
        '
        Me.lblDelInfo.BackColor = System.Drawing.Color.White
        Me.lblDelInfo.Location = New System.Drawing.Point(514, 8)
        Me.lblDelInfo.Name = "lblDelInfo"
        Me.lblDelInfo.Size = New System.Drawing.Size(236, 19)
        Me.lblDelInfo.TabIndex = 174
        Me.lblDelInfo.Text = "Label9"
        Me.lblDelInfo.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.BackColor = System.Drawing.SystemColors.Control
        Me.btnExit.ButtonStyle = AxAckButton.AxImgButton.enumButtonStyle.btn96x26
        Me.btnExit.ButtonText = "종  료(Esc)"
        Me.btnExit.Location = New System.Drawing.Point(888, 565)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(96, 26)
        Me.btnExit.TabIndex = 173
        '
        'spdSpcList
        '
        Me.spdSpcList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdSpcList.ContextMenuStrip = Me.cmuRstList
        Me.spdSpcList.DataSource = Nothing
        Me.spdSpcList.Location = New System.Drawing.Point(2, 65)
        Me.spdSpcList.Name = "spdSpcList"
        Me.spdSpcList.OcxState = CType(resources.GetObject("spdSpcList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdSpcList.Size = New System.Drawing.Size(982, 492)
        Me.spdSpcList.TabIndex = 0
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.SystemColors.Control
        Me.btnPrint.ButtonStyle = AxAckButton.AxImgButton.enumButtonStyle.btn96x26
        Me.btnPrint.ButtonText = "출  력"
        Me.btnPrint.Location = New System.Drawing.Point(786, 565)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(96, 26)
        Me.btnPrint.TabIndex = 160
        '
        'btnQuery
        '
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnQuery.Location = New System.Drawing.Point(271, 6)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(57, 21)
        Me.btnQuery.TabIndex = 128
        Me.btnQuery.Text = "조회"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(158, 9)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(11, 12)
        Me.Label32.TabIndex = 126
        Me.Label32.Text = "~"
        '
        'dtpEnd
        '
        Me.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEnd.Location = New System.Drawing.Point(175, 6)
        Me.dtpEnd.Name = "dtpEnd"
        Me.dtpEnd.Size = New System.Drawing.Size(86, 21)
        Me.dtpEnd.TabIndex = 124
        Me.dtpEnd.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(4, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(62, 21)
        Me.Label14.TabIndex = 123
        Me.Label14.Text = "등록일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpStart
        '
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStart.Location = New System.Drawing.Point(67, 6)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(86, 21)
        Me.dtpStart.TabIndex = 122
        Me.dtpStart.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.btnClose)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.btnCReg)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(992, 597)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "등록"
        '
        'AxvaSpread1
        '
        Me.AxvaSpread1.DataSource = Nothing
        Me.AxvaSpread1.Location = New System.Drawing.Point(4, 9)
        Me.AxvaSpread1.Name = "AxvaSpread1"
        Me.AxvaSpread1.OcxState = CType(resources.GetObject("AxvaSpread1.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AxvaSpread1.Size = New System.Drawing.Size(476, 597)
        Me.AxvaSpread1.TabIndex = 0
        '
        'FGPOPUPSP_RST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1009, 640)
        Me.Controls.Add(Me.TabControl1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGPOPUPSP_RST"
        Me.Text = "특이결과 등록 및 조회"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.spdRsltList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.cmuRstList.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdSpcList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        CType(Me.AxvaSpread1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents spdSpcList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents dtpEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboBccls As System.Windows.Forms.ComboBox
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents lblWardroom As System.Windows.Forms.Label
    Friend WithEvents lblDeptnm As System.Windows.Forms.Label
    Friend WithEvents lblPatnm As System.Windows.Forms.Label
    Friend WithEvents lblno As System.Windows.Forms.Label
    Friend WithEvents AxvaSpread1 As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnCReg As AxAckButton.AxImgButton
    Friend WithEvents spdRsltList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents rdoB As System.Windows.Forms.RadioButton
    Friend WithEvents rdoA As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As AxAckButton.AxImgButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblSickRoomT As System.Windows.Forms.Label
    Friend WithEvents lblWardT As System.Windows.Forms.Label
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents cboSR As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents btnCdHelp As System.Windows.Forms.Button
    Friend WithEvents txtCmt As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As AxAckButton.AxImgButton
    Friend WithEvents btnExit As AxAckButton.AxImgButton
    Friend WithEvents cmuRstList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDel_sp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblDelInfo As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboCmt As System.Windows.Forms.ComboBox
End Class
