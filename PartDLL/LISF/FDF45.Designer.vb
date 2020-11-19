<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF45
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF45))
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tbcTpg = New System.Windows.Forms.TabPage()
        Me.txtModNm = New System.Windows.Forms.TextBox()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.btnCdHelp_test = New System.Windows.Forms.Button()
        Me.txtSpcNmD = New System.Windows.Forms.TextBox()
        Me.txtSpcCd = New System.Windows.Forms.TextBox()
        Me.lblSpcCd = New System.Windows.Forms.Label()
        Me.txtTNmD = New System.Windows.Forms.TextBox()
        Me.txtTestCd = New System.Windows.Forms.TextBox()
        Me.lblTestCd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.txtModID = New System.Windows.Forms.TextBox()
        Me.lblModNm = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.txtModDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.lblModDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.btnWbcHelp = New System.Windows.Forms.Button()
        Me.txtSlipCd = New System.Windows.Forms.TextBox()
        Me.txtWbcTnm = New System.Windows.Forms.TextBox()
        Me.txtWbcTcd = New System.Windows.Forms.TextBox()
        Me.lblWbcTcd = New System.Windows.Forms.Label()
        Me.spdItem = New AxFPSpreadADO.AxfpSpread()
        Me.cboFormGbn = New System.Windows.Forms.ComboBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblFormGbn = New System.Windows.Forms.Label()
        Me.errpd = New System.Windows.Forms.ErrorProvider()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tclSpc.Size = New System.Drawing.Size(795, 614)
        Me.tclSpc.TabIndex = 1
        Me.tclSpc.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.grpCd)
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.lblModNm)
        Me.tbcTpg.Controls.Add(Me.txtRegDT)
        Me.tbcTpg.Controls.Add(Me.txtModDT)
        Me.tbcTpg.Controls.Add(Me.lblUserNm)
        Me.tbcTpg.Controls.Add(Me.lblRegDT)
        Me.tbcTpg.Controls.Add(Me.lblModDT)
        Me.tbcTpg.Controls.Add(Me.txtRegID)
        Me.tbcTpg.Controls.Add(Me.grpCdInfo1)
        Me.tbcTpg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.tbcTpg.Location = New System.Drawing.Point(4, 21)
        Me.tbcTpg.Name = "tbcTpg"
        Me.tbcTpg.Size = New System.Drawing.Size(787, 589)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "KEYPAD 검사정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(316, 552)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(100, 21)
        Me.txtModNm.TabIndex = 203
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(704, 552)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 202
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnCdHelp_test)
        Me.grpCd.Controls.Add(Me.txtSpcNmD)
        Me.grpCd.Controls.Add(Me.txtSpcCd)
        Me.grpCd.Controls.Add(Me.lblSpcCd)
        Me.grpCd.Controls.Add(Me.txtTNmD)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 13)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(771, 53)
        Me.grpCd.TabIndex = 3
        Me.grpCd.TabStop = False
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(276, 18)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 3
        Me.btnCdHelp_test.TabStop = False
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'txtSpcNmD
        '
        Me.txtSpcNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSpcNmD.Location = New System.Drawing.Point(486, 18)
        Me.txtSpcNmD.Name = "txtSpcNmD"
        Me.txtSpcNmD.ReadOnly = True
        Me.txtSpcNmD.Size = New System.Drawing.Size(188, 21)
        Me.txtSpcNmD.TabIndex = 5
        Me.txtSpcNmD.TabStop = False
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(239, 18)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(36, 21)
        Me.txtSpcCd.TabIndex = 2
        Me.txtSpcCd.Tag = "TCLSCD"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(170, 18)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(68, 21)
        Me.lblSpcCd.TabIndex = 2
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(303, 18)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(182, 21)
        Me.txtTNmD.TabIndex = 4
        Me.txtTNmD.TabStop = False
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(83, 18)
        Me.txtTestCd.MaxLength = 5
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(72, 21)
        Me.txtTestCd.TabIndex = 1
        Me.txtTestCd.Tag = "TCLSCD"
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(14, 18)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(68, 21)
        Me.lblTestCd.TabIndex = 0
        Me.lblTestCd.Text = "검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(690, 15)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 0
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(316, 552)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(100, 21)
        Me.txtModID.TabIndex = 0
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = ""
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(219, 552)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(96, 21)
        Me.lblModNm.TabIndex = 0
        Me.lblModNm.Tag = ""
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(512, 552)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
        Me.txtRegDT.TabIndex = 0
        Me.txtRegDT.TabStop = False
        Me.txtRegDT.Tag = "REGDT"
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(108, 552)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
        Me.txtModDT.TabIndex = 0
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = ""
        '
        'lblUserNm
        '
        Me.lblUserNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.Black
        Me.lblUserNm.Location = New System.Drawing.Point(619, 552)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 21)
        Me.lblUserNm.TabIndex = 0
        Me.lblUserNm.Text = "최종등록자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRegDT
        '
        Me.lblRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRegDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegDT.ForeColor = System.Drawing.Color.Black
        Me.lblRegDT.Location = New System.Drawing.Point(427, 552)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(11, 552)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(96, 21)
        Me.lblModDT.TabIndex = 0
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(704, 552)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(68, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.btnWbcHelp)
        Me.grpCdInfo1.Controls.Add(Me.txtSlipCd)
        Me.grpCdInfo1.Controls.Add(Me.txtWbcTnm)
        Me.grpCdInfo1.Controls.Add(Me.txtWbcTcd)
        Me.grpCdInfo1.Controls.Add(Me.lblWbcTcd)
        Me.grpCdInfo1.Controls.Add(Me.spdItem)
        Me.grpCdInfo1.Controls.Add(Me.cboFormGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblTitle)
        Me.grpCdInfo1.Controls.Add(Me.lblFormGbn)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 72)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(771, 468)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "KEYPAD 세부정보"
        '
        'btnWbcHelp
        '
        Me.btnWbcHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWbcHelp.Image = CType(resources.GetObject("btnWbcHelp.Image"), System.Drawing.Image)
        Me.btnWbcHelp.Location = New System.Drawing.Point(200, 51)
        Me.btnWbcHelp.Name = "btnWbcHelp"
        Me.btnWbcHelp.Size = New System.Drawing.Size(26, 21)
        Me.btnWbcHelp.TabIndex = 8
        Me.btnWbcHelp.TabStop = False
        Me.btnWbcHelp.UseVisualStyleBackColor = True
        '
        'txtSlipCd
        '
        Me.txtSlipCd.BackColor = System.Drawing.Color.Thistle
        Me.txtSlipCd.Location = New System.Drawing.Point(129, 426)
        Me.txtSlipCd.Name = "txtSlipCd"
        Me.txtSlipCd.ReadOnly = True
        Me.txtSlipCd.Size = New System.Drawing.Size(202, 21)
        Me.txtSlipCd.TabIndex = 155
        Me.txtSlipCd.Visible = False
        '
        'txtWbcTnm
        '
        Me.txtWbcTnm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtWbcTnm.Location = New System.Drawing.Point(117, 73)
        Me.txtWbcTnm.Name = "txtWbcTnm"
        Me.txtWbcTnm.ReadOnly = True
        Me.txtWbcTnm.Size = New System.Drawing.Size(202, 21)
        Me.txtWbcTnm.TabIndex = 9
        Me.txtWbcTnm.TabStop = False
        '
        'txtWbcTcd
        '
        Me.txtWbcTcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWbcTcd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWbcTcd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtWbcTcd.Location = New System.Drawing.Point(117, 51)
        Me.txtWbcTcd.MaxLength = 7
        Me.txtWbcTcd.Name = "txtWbcTcd"
        Me.txtWbcTcd.Size = New System.Drawing.Size(82, 21)
        Me.txtWbcTcd.TabIndex = 7
        Me.txtWbcTcd.Tag = "TCLSCD"
        '
        'lblWbcTcd
        '
        Me.lblWbcTcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblWbcTcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWbcTcd.ForeColor = System.Drawing.Color.White
        Me.lblWbcTcd.Location = New System.Drawing.Point(14, 50)
        Me.lblWbcTcd.Name = "lblWbcTcd"
        Me.lblWbcTcd.Size = New System.Drawing.Size(102, 21)
        Me.lblWbcTcd.TabIndex = 151
        Me.lblWbcTcd.Text = "WBC 검사코드"
        Me.lblWbcTcd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdItem
        '
        Me.spdItem.DataSource = Nothing
        Me.spdItem.Location = New System.Drawing.Point(352, 49)
        Me.spdItem.Name = "spdItem"
        Me.spdItem.OcxState = CType(resources.GetObject("spdItem.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdItem.Size = New System.Drawing.Size(412, 398)
        Me.spdItem.TabIndex = 10
        '
        'cboFormGbn
        '
        Me.cboFormGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormGbn.FormattingEnabled = True
        Me.cboFormGbn.Items.AddRange(New Object() {"숫자", "알파벳"})
        Me.cboFormGbn.Location = New System.Drawing.Point(117, 28)
        Me.cboFormGbn.Name = "cboFormGbn"
        Me.cboFormGbn.Size = New System.Drawing.Size(109, 20)
        Me.cboFormGbn.TabIndex = 6
        Me.cboFormGbn.Tag = "FORMGBN"
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTitle.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(350, 26)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(118, 21)
        Me.lblTitle.TabIndex = 146
        Me.lblTitle.Text = "상세 검사항목"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFormGbn
        '
        Me.lblFormGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblFormGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFormGbn.ForeColor = System.Drawing.Color.White
        Me.lblFormGbn.Location = New System.Drawing.Point(14, 28)
        Me.lblFormGbn.Name = "lblFormGbn"
        Me.lblFormGbn.Size = New System.Drawing.Size(102, 21)
        Me.lblFormGbn.TabIndex = 145
        Me.lblFormGbn.Text = "KEYPAD 폼 종류"
        Me.lblFormGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF45
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 614)
        Me.Controls.Add(Me.tclSpc)
        Me.Name = "FDF45"
        Me.Text = "[45] KEYPAD 설정"
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtSpcNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents cboFormGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblFormGbn As System.Windows.Forms.Label
    Friend WithEvents spdItem As AxFPSpreadADO.AxfpSpread
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents lblWbcTcd As System.Windows.Forms.Label
    Friend WithEvents txtWbcTnm As System.Windows.Forms.TextBox
    Friend WithEvents txtWbcTcd As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipCd As System.Windows.Forms.TextBox
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents btnWbcHelp As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
End Class
