<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDHELP_TEST
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDHELP_TEST))
        Me.txtTCode = New System.Windows.Forms.TextBox()
        Me.lblTest = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboSpc = New System.Windows.Forms.ComboBox()
        Me.txtUsDt = New System.Windows.Forms.TextBox()
        Me.txtSlipNmd = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTnmd = New System.Windows.Forms.TextBox()
        Me.txtTubeNmd = New System.Windows.Forms.TextBox()
        Me.txtExLabYn = New System.Windows.Forms.TextBox()
        Me.txtRrptst = New System.Windows.Forms.TextBox()
        Me.txtTelNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkExeDay7 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay6 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay5 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay4 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay3 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay2 = New System.Windows.Forms.CheckBox()
        Me.chkExeDay1 = New System.Windows.Forms.CheckBox()
        Me.spdTestInfo = New AxFPSpreadADO.AxfpSpread()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtRef = New System.Windows.Forms.TextBox()
        Me.txtInfo1 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtInfo2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtCWarning = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.txtTestCd = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.picTube = New System.Windows.Forms.PictureBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtInfo3 = New System.Windows.Forms.TextBox()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.chkErGbn1 = New System.Windows.Forms.CheckBox()
        Me.chkErGbn2 = New System.Windows.Forms.CheckBox()
        Me.btnCdHelp_Tnm = New System.Windows.Forms.Button()
        Me.txtOrdSlip = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtTAT = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtInfo4 = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        CType(Me.spdTestInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        CType(Me.picTube, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTCode
        '
        Me.txtTCode.Location = New System.Drawing.Point(112, 24)
        Me.txtTCode.MaxLength = 0
        Me.txtTCode.Name = "txtTCode"
        Me.txtTCode.Size = New System.Drawing.Size(120, 21)
        Me.txtTCode.TabIndex = 31
        Me.txtTCode.Tag = ""
        Me.txtTCode.Text = "012345678"
        '
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.White
        Me.lblTest.Location = New System.Drawing.Point(7, 24)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(104, 21)
        Me.lblTest.TabIndex = 32
        Me.lblTest.Text = " 검사코드"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(4, 368)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(419, 10)
        Me.Label1.TabIndex = 193
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(4, 15)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1108, 9)
        Me.Label2.TabIndex = 194
        Me.Label2.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblInfo.Location = New System.Drawing.Point(5, 5)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(111, 12)
        Me.lblInfo.TabIndex = 195
        Me.lblInfo.Text = ">> 검사 기본정보"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(278, 24)
        Me.btnCdHelp_test.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 196
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(7, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 21)
        Me.Label3.TabIndex = 197
        Me.Label3.Text = " 검체명"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(310, 68)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(68, 21)
        Me.Label4.TabIndex = 198
        Me.Label4.Text = " 적용일자"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(7, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 21)
        Me.Label5.TabIndex = 199
        Me.Label5.Text = " 검사분류"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(7, 112)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 21)
        Me.Label6.TabIndex = 200
        Me.Label6.Text = " 실시요일"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSpc
        '
        Me.cboSpc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpc.FormattingEnabled = True
        Me.cboSpc.Location = New System.Drawing.Point(112, 46)
        Me.cboSpc.Name = "cboSpc"
        Me.cboSpc.Size = New System.Drawing.Size(192, 20)
        Me.cboSpc.TabIndex = 202
        Me.cboSpc.Tag = "SPC_01"
        '
        'txtUsDt
        '
        Me.txtUsDt.Location = New System.Drawing.Point(379, 68)
        Me.txtUsDt.MaxLength = 0
        Me.txtUsDt.Name = "txtUsDt"
        Me.txtUsDt.Size = New System.Drawing.Size(106, 21)
        Me.txtUsDt.TabIndex = 203
        Me.txtUsDt.Tag = "USDT"
        Me.txtUsDt.Text = "0000-00-00 00:00"
        '
        'txtSlipNmd
        '
        Me.txtSlipNmd.Location = New System.Drawing.Point(112, 90)
        Me.txtSlipNmd.MaxLength = 0
        Me.txtSlipNmd.Name = "txtSlipNmd"
        Me.txtSlipNmd.Size = New System.Drawing.Size(192, 21)
        Me.txtSlipNmd.TabIndex = 204
        Me.txtSlipNmd.Tag = "SLIPNMD"
        Me.txtSlipNmd.Text = "012345678"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(310, 90)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 21)
        Me.Label10.TabIndex = 209
        Me.Label10.Text = " 소요일"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(487, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(69, 21)
        Me.Label11.TabIndex = 208
        Me.Label11.Text = " 위탁기관"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(310, 46)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(104, 21)
        Me.Label12.TabIndex = 207
        Me.Label12.Text = " 용기명"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(310, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 21)
        Me.Label13.TabIndex = 206
        Me.Label13.Text = " 검사명"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTnmd
        '
        Me.txtTnmd.Location = New System.Drawing.Point(415, 24)
        Me.txtTnmd.MaxLength = 0
        Me.txtTnmd.Name = "txtTnmd"
        Me.txtTnmd.Size = New System.Drawing.Size(179, 21)
        Me.txtTnmd.TabIndex = 210
        Me.txtTnmd.Tag = "TNMD"
        Me.txtTnmd.Text = "012345678"
        '
        'txtTubeNmd
        '
        Me.txtTubeNmd.Location = New System.Drawing.Point(415, 46)
        Me.txtTubeNmd.MaxLength = 0
        Me.txtTubeNmd.Name = "txtTubeNmd"
        Me.txtTubeNmd.Size = New System.Drawing.Size(206, 21)
        Me.txtTubeNmd.TabIndex = 211
        Me.txtTubeNmd.Tag = "TUBENMD"
        Me.txtTubeNmd.Text = "012345678"
        '
        'txtExLabYn
        '
        Me.txtExLabYn.Location = New System.Drawing.Point(557, 68)
        Me.txtExLabYn.MaxLength = 0
        Me.txtExLabYn.Name = "txtExLabYn"
        Me.txtExLabYn.Size = New System.Drawing.Size(64, 21)
        Me.txtExLabYn.TabIndex = 212
        Me.txtExLabYn.Tag = "EXLABYN"
        Me.txtExLabYn.Text = "012345678"
        '
        'txtRrptst
        '
        Me.txtRrptst.Location = New System.Drawing.Point(379, 90)
        Me.txtRrptst.MaxLength = 0
        Me.txtRrptst.Name = "txtRrptst"
        Me.txtRrptst.Size = New System.Drawing.Size(106, 21)
        Me.txtRrptst.TabIndex = 213
        Me.txtRrptst.Tag = "RRPTST"
        Me.txtRrptst.Text = "012345678"
        '
        'txtTelNo
        '
        Me.txtTelNo.Location = New System.Drawing.Point(557, 90)
        Me.txtTelNo.MaxLength = 0
        Me.txtTelNo.Name = "txtTelNo"
        Me.txtTelNo.Size = New System.Drawing.Size(64, 21)
        Me.txtTelNo.TabIndex = 236
        Me.txtTelNo.Tag = "TELNO"
        Me.txtTelNo.Text = "1254"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(487, 90)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 21)
        Me.Label7.TabIndex = 235
        Me.Label7.Text = " 내선번호"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.Location = New System.Drawing.Point(5, 160)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(111, 12)
        Me.Label8.TabIndex = 237
        Me.Label8.Text = ">> 세부검사 목록"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Gray
        Me.Label9.Location = New System.Drawing.Point(4, 171)
        Me.Label9.Margin = New System.Windows.Forms.Padding(0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(622, 9)
        Me.Label9.TabIndex = 238
        Me.Label9.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chkExeDay7)
        Me.Panel1.Controls.Add(Me.chkExeDay6)
        Me.Panel1.Controls.Add(Me.chkExeDay5)
        Me.Panel1.Controls.Add(Me.chkExeDay4)
        Me.Panel1.Controls.Add(Me.chkExeDay3)
        Me.Panel1.Controls.Add(Me.chkExeDay2)
        Me.Panel1.Controls.Add(Me.chkExeDay1)
        Me.Panel1.Location = New System.Drawing.Point(112, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(284, 21)
        Me.Panel1.TabIndex = 239
        '
        'chkExeDay7
        '
        Me.chkExeDay7.Location = New System.Drawing.Point(241, 1)
        Me.chkExeDay7.Name = "chkExeDay7"
        Me.chkExeDay7.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay7.TabIndex = 241
        Me.chkExeDay7.Tag = "EXEDAY7"
        Me.chkExeDay7.Text = "일"
        '
        'chkExeDay6
        '
        Me.chkExeDay6.Location = New System.Drawing.Point(200, 1)
        Me.chkExeDay6.Name = "chkExeDay6"
        Me.chkExeDay6.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay6.TabIndex = 240
        Me.chkExeDay6.Tag = "EXEDAY6"
        Me.chkExeDay6.Text = "토"
        '
        'chkExeDay5
        '
        Me.chkExeDay5.Location = New System.Drawing.Point(163, 1)
        Me.chkExeDay5.Name = "chkExeDay5"
        Me.chkExeDay5.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay5.TabIndex = 239
        Me.chkExeDay5.Tag = "EXEDAY5"
        Me.chkExeDay5.Text = "금"
        '
        'chkExeDay4
        '
        Me.chkExeDay4.Location = New System.Drawing.Point(122, 1)
        Me.chkExeDay4.Name = "chkExeDay4"
        Me.chkExeDay4.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay4.TabIndex = 238
        Me.chkExeDay4.Tag = "EXEDAY4"
        Me.chkExeDay4.Text = "목"
        '
        'chkExeDay3
        '
        Me.chkExeDay3.Location = New System.Drawing.Point(85, 1)
        Me.chkExeDay3.Name = "chkExeDay3"
        Me.chkExeDay3.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay3.TabIndex = 237
        Me.chkExeDay3.Tag = "EXEDAY3"
        Me.chkExeDay3.Text = "수"
        '
        'chkExeDay2
        '
        Me.chkExeDay2.Location = New System.Drawing.Point(44, 1)
        Me.chkExeDay2.Name = "chkExeDay2"
        Me.chkExeDay2.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay2.TabIndex = 236
        Me.chkExeDay2.Tag = "EXEDAY2"
        Me.chkExeDay2.Text = "화"
        '
        'chkExeDay1
        '
        Me.chkExeDay1.Location = New System.Drawing.Point(7, 1)
        Me.chkExeDay1.Name = "chkExeDay1"
        Me.chkExeDay1.Size = New System.Drawing.Size(31, 18)
        Me.chkExeDay1.TabIndex = 235
        Me.chkExeDay1.Tag = "EXEDAY1"
        Me.chkExeDay1.Text = "월"
        '
        'spdTestInfo
        '
        Me.spdTestInfo.DataSource = Nothing
        Me.spdTestInfo.Location = New System.Drawing.Point(6, 185)
        Me.spdTestInfo.Name = "spdTestInfo"
        Me.spdTestInfo.OcxState = CType(resources.GetObject("spdTestInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTestInfo.Size = New System.Drawing.Size(614, 160)
        Me.spdTestInfo.TabIndex = 240
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.Location = New System.Drawing.Point(5, 357)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(111, 12)
        Me.Label14.TabIndex = 241
        Me.Label14.Text = ">> 검사 상세정보"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.White
        Me.Label15.Location = New System.Drawing.Point(5, 377)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(104, 91)
        Me.Label15.TabIndex = 242
        Me.Label15.Text = " 참고치"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRef
        '
        Me.txtRef.Location = New System.Drawing.Point(110, 378)
        Me.txtRef.MaxLength = 0
        Me.txtRef.Multiline = True
        Me.txtRef.Name = "txtRef"
        Me.txtRef.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRef.Size = New System.Drawing.Size(302, 90)
        Me.txtRef.TabIndex = 243
        Me.txtRef.Tag = "REFINFO"
        Me.txtRef.Text = "012345678"
        '
        'txtInfo1
        '
        Me.txtInfo1.Location = New System.Drawing.Point(110, 470)
        Me.txtInfo1.MaxLength = 0
        Me.txtInfo1.Multiline = True
        Me.txtInfo1.Name = "txtInfo1"
        Me.txtInfo1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo1.Size = New System.Drawing.Size(302, 48)
        Me.txtInfo1.TabIndex = 245
        Me.txtInfo1.Tag = "TESTINFO1"
        Me.txtInfo1.Text = "012345678"
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(5, 469)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(104, 49)
        Me.Label16.TabIndex = 244
        Me.Label16.Text = " 검사법/        참조검사"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInfo2
        '
        Me.txtInfo2.Location = New System.Drawing.Point(110, 520)
        Me.txtInfo2.MaxLength = 0
        Me.txtInfo2.Multiline = True
        Me.txtInfo2.Name = "txtInfo2"
        Me.txtInfo2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo2.Size = New System.Drawing.Size(302, 48)
        Me.txtInfo2.TabIndex = 247
        Me.txtInfo2.Tag = "TESTINFO2"
        Me.txtInfo2.Text = "012345678"
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label17.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.White
        Me.Label17.Location = New System.Drawing.Point(5, 519)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(104, 49)
        Me.Label17.TabIndex = 246
        Me.Label17.Text = " 주의내용"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCWarning
        '
        Me.txtCWarning.Location = New System.Drawing.Point(110, 570)
        Me.txtCWarning.MaxLength = 0
        Me.txtCWarning.Multiline = True
        Me.txtCWarning.Name = "txtCWarning"
        Me.txtCWarning.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCWarning.Size = New System.Drawing.Size(302, 48)
        Me.txtCWarning.TabIndex = 249
        Me.txtCWarning.Tag = "CWARNING"
        Me.txtCWarning.Text = "012345678"
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(5, 569)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(104, 49)
        Me.Label18.TabIndex = 248
        Me.Label18.Text = " 검체 채취시    주의사항"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.txtTestCd)
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 623)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1110, 32)
        Me.pnlBottom.TabIndex = 250
        '
        'txtTestCd
        '
        Me.txtTestCd.Location = New System.Drawing.Point(495, 4)
        Me.txtTestCd.MaxLength = 0
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(120, 21)
        Me.txtTestCd.TabIndex = 32
        Me.txtTestCd.Tag = ""
        Me.txtTestCd.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(924, 2)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 25)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "출력"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(1011, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(88, 25)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.ForeColor = System.Drawing.Color.Gray
        Me.Label19.Location = New System.Drawing.Point(415, 368)
        Me.Label19.Margin = New System.Windows.Forms.Padding(0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(210, 10)
        Me.Label19.TabIndex = 251
        Me.Label19.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.Location = New System.Drawing.Point(417, 357)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(91, 12)
        Me.Label20.TabIndex = 252
        Me.Label20.Text = ">> 용기이미지"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'picTube
        '
        Me.picTube.BackColor = System.Drawing.Color.White
        Me.picTube.Location = New System.Drawing.Point(418, 377)
        Me.picTube.Name = "picTube"
        Me.picTube.Size = New System.Drawing.Size(203, 241)
        Me.picTube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTube.TabIndex = 253
        Me.picTube.TabStop = False
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.White
        Me.Label21.Location = New System.Drawing.Point(629, 364)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(472, 21)
        Me.Label21.TabIndex = 254
        Me.Label21.Text = "임    상    적    의    의"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInfo3
        '
        Me.txtInfo3.Location = New System.Drawing.Point(629, 387)
        Me.txtInfo3.MaxLength = 0
        Me.txtInfo3.Multiline = True
        Me.txtInfo3.Name = "txtInfo3"
        Me.txtInfo3.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo3.Size = New System.Drawing.Size(472, 231)
        Me.txtInfo3.TabIndex = 255
        Me.txtInfo3.Tag = "TESTINFO3"
        Me.txtInfo3.Text = "012345678"
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(233, 24)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(44, 21)
        Me.btnToggle.TabIndex = 256
        Me.btnToggle.Text = "<->"
        '
        'chkErGbn1
        '
        Me.chkErGbn1.BackColor = System.Drawing.Color.Transparent
        Me.chkErGbn1.Location = New System.Drawing.Point(499, 116)
        Me.chkErGbn1.Name = "chkErGbn1"
        Me.chkErGbn1.Size = New System.Drawing.Size(73, 17)
        Me.chkErGbn1.TabIndex = 257
        Me.chkErGbn1.Tag = "ERGBN1"
        Me.chkErGbn1.Text = "응급검사"
        Me.chkErGbn1.UseVisualStyleBackColor = False
        '
        'chkErGbn2
        '
        Me.chkErGbn2.BackColor = System.Drawing.Color.Transparent
        Me.chkErGbn2.Location = New System.Drawing.Point(412, 116)
        Me.chkErGbn2.Name = "chkErGbn2"
        Me.chkErGbn2.Size = New System.Drawing.Size(73, 15)
        Me.chkErGbn2.TabIndex = 258
        Me.chkErGbn2.Tag = "ERGBN1"
        Me.chkErGbn2.Text = "당일검사"
        Me.chkErGbn2.UseVisualStyleBackColor = False
        '
        'btnCdHelp_Tnm
        '
        Me.btnCdHelp_Tnm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_Tnm.Image = CType(resources.GetObject("btnCdHelp_Tnm.Image"), System.Drawing.Image)
        Me.btnCdHelp_Tnm.Location = New System.Drawing.Point(595, 24)
        Me.btnCdHelp_Tnm.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_Tnm.Name = "btnCdHelp_Tnm"
        Me.btnCdHelp_Tnm.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_Tnm.TabIndex = 259
        Me.btnCdHelp_Tnm.UseVisualStyleBackColor = True
        '
        'txtOrdSlip
        '
        Me.txtOrdSlip.Location = New System.Drawing.Point(112, 68)
        Me.txtOrdSlip.MaxLength = 0
        Me.txtOrdSlip.Name = "txtOrdSlip"
        Me.txtOrdSlip.Size = New System.Drawing.Size(192, 21)
        Me.txtOrdSlip.TabIndex = 261
        Me.txtOrdSlip.Tag = "SLIPNMD"
        Me.txtOrdSlip.Text = "012345678"
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(7, 68)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(104, 21)
        Me.Label22.TabIndex = 260
        Me.Label22.Text = " 처방슬립"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(7, 134)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(104, 21)
        Me.Label23.TabIndex = 262
        Me.Label23.Text = " TAT 시간"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTAT
        '
        Me.txtTAT.Location = New System.Drawing.Point(112, 135)
        Me.txtTAT.MaxLength = 0
        Me.txtTAT.Name = "txtTAT"
        Me.txtTAT.Size = New System.Drawing.Size(509, 21)
        Me.txtTAT.TabIndex = 263
        Me.txtTAT.Tag = "RRPTST"
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label24.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.White
        Me.Label24.Location = New System.Drawing.Point(627, 24)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(472, 21)
        Me.Label24.TabIndex = 264
        Me.Label24.Text = "검   사   의   뢰   정   보"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtInfo4
        '
        Me.txtInfo4.Location = New System.Drawing.Point(627, 47)
        Me.txtInfo4.MaxLength = 0
        Me.txtInfo4.Multiline = True
        Me.txtInfo4.Name = "txtInfo4"
        Me.txtInfo4.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInfo4.Size = New System.Drawing.Size(472, 313)
        Me.txtInfo4.TabIndex = 265
        Me.txtInfo4.Tag = "TESTINFO3"
        '
        'FGCDHELP_TEST
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1110, 655)
        Me.Controls.Add(Me.txtInfo4)
        Me.Controls.Add(Me.Label24)
        Me.Controls.Add(Me.txtTAT)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.txtOrdSlip)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.btnCdHelp_Tnm)
        Me.Controls.Add(Me.chkErGbn2)
        Me.Controls.Add(Me.chkErGbn1)
        Me.Controls.Add(Me.btnToggle)
        Me.Controls.Add(Me.txtInfo3)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.picTube)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.txtCWarning)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtInfo2)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtInfo1)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.txtRef)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtTelNo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtRrptst)
        Me.Controls.Add(Me.txtExLabYn)
        Me.Controls.Add(Me.txtTubeNmd)
        Me.Controls.Add(Me.txtTnmd)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtSlipNmd)
        Me.Controls.Add(Me.txtUsDt)
        Me.Controls.Add(Me.cboSpc)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCdHelp_test)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTCode)
        Me.Controls.Add(Me.lblTest)
        Me.Controls.Add(Me.spdTestInfo)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGCDHELP_TEST"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "검사 정보관리"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdTestInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        CType(Me.picTube, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected Friend WithEvents txtTCode As System.Windows.Forms.TextBox
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboSpc As System.Windows.Forms.ComboBox
    Protected Friend WithEvents txtUsDt As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtSlipNmd As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Protected Friend WithEvents txtTnmd As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtTubeNmd As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtExLabYn As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtRrptst As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtTelNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkExeDay7 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay6 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay5 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay4 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay3 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay2 As System.Windows.Forms.CheckBox
    Friend WithEvents chkExeDay1 As System.Windows.Forms.CheckBox
    Friend WithEvents spdTestInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Protected Friend WithEvents txtRef As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtInfo1 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Protected Friend WithEvents txtInfo2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Protected Friend WithEvents txtCWarning As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents picTube As System.Windows.Forms.PictureBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Protected Friend WithEvents txtInfo3 As System.Windows.Forms.TextBox
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Protected Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents chkErGbn1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkErGbn2 As System.Windows.Forms.CheckBox
    Friend WithEvents btnCdHelp_Tnm As System.Windows.Forms.Button
    Protected Friend WithEvents txtOrdSlip As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Protected Friend WithEvents txtTAT As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Protected Friend WithEvents txtInfo4 As System.Windows.Forms.TextBox
End Class
