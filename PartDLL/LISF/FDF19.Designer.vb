<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF19
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF19))
        Me.tclSpc = New System.Windows.Forms.TabControl
        Me.tbcTpg = New System.Windows.Forms.TabPage
        Me.txtModNm = New System.Windows.Forms.TextBox
        Me.txtRegNm = New System.Windows.Forms.TextBox
        Me.txtModID = New System.Windows.Forms.TextBox
        Me.lblModNm = New System.Windows.Forms.Label
        Me.txtModDT = New System.Windows.Forms.TextBox
        Me.lblModDT = New System.Windows.Forms.Label
        Me.txtRegDT = New System.Windows.Forms.TextBox
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblRegDT = New System.Windows.Forms.Label
        Me.txtRegID = New System.Windows.Forms.TextBox
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox
        Me.txtIncNm = New System.Windows.Forms.TextBox
        Me.lblIncNm = New System.Windows.Forms.Label
        Me.grpCd = New System.Windows.Forms.GroupBox
        Me.btnClear_spc = New System.Windows.Forms.Button
        Me.chkSpcGbn = New System.Windows.Forms.CheckBox
        Me.btnCdHelp_spc = New System.Windows.Forms.Button
        Me.txtSpcNmd = New System.Windows.Forms.TextBox
        Me.txtSpcCd = New System.Windows.Forms.TextBox
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.btnCdHelp_test = New System.Windows.Forms.Button
        Me.txtTNmD = New System.Windows.Forms.TextBox
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.lblTestCd = New System.Windows.Forms.Label
        Me.txtIncCd = New System.Windows.Forms.TextBox
        Me.lblIncCd = New System.Windows.Forms.Label
        Me.btnUE = New System.Windows.Forms.Button
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
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
        Me.tclSpc.Size = New System.Drawing.Size(797, 602)
        Me.tclSpc.TabIndex = 3
        Me.tclSpc.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtModNm)
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModID)
        Me.tbcTpg.Controls.Add(Me.lblModNm)
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
        Me.tbcTpg.Size = New System.Drawing.Size(789, 577)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "균 결과 정보"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(319, 546)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(70, 21)
        Me.txtModNm.TabIndex = 188
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(708, 546)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(70, 21)
        Me.txtRegNm.TabIndex = 12
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(319, 546)
        Me.txtModID.Margin = New System.Windows.Forms.Padding(0)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(70, 21)
        Me.txtModID.TabIndex = 6
        Me.txtModID.TabStop = False
        Me.txtModID.Tag = "MODID"
        '
        'lblModNm
        '
        Me.lblModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModNm.ForeColor = System.Drawing.Color.Black
        Me.lblModNm.Location = New System.Drawing.Point(226, 546)
        Me.lblModNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(92, 21)
        Me.lblModNm.TabIndex = 5
        Me.lblModNm.Text = " 변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(101, 546)
        Me.txtModDT.Margin = New System.Windows.Forms.Padding(0)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(120, 21)
        Me.txtModDT.TabIndex = 4
        Me.txtModDT.TabStop = False
        Me.txtModDT.Tag = "MODDT"
        '
        'lblModDT
        '
        Me.lblModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblModDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblModDT.ForeColor = System.Drawing.Color.Black
        Me.lblModDT.Location = New System.Drawing.Point(7, 546)
        Me.lblModDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(93, 21)
        Me.lblModDT.TabIndex = 3
        Me.lblModDT.Text = " 변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(491, 546)
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
        Me.lblUserNm.Location = New System.Drawing.Point(616, 546)
        Me.lblUserNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(91, 21)
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
        Me.lblRegDT.Location = New System.Drawing.Point(399, 546)
        Me.lblRegDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(91, 21)
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
        Me.txtRegID.Location = New System.Drawing.Point(708, 546)
        Me.txtRegID.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegID.Name = "txtRegID"
        Me.txtRegID.ReadOnly = True
        Me.txtRegID.Size = New System.Drawing.Size(70, 21)
        Me.txtRegID.TabIndex = 0
        Me.txtRegID.TabStop = False
        Me.txtRegID.Tag = "REGID"
        '
        'grpCdInfo1
        '
        Me.grpCdInfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCdInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCdInfo1.Controls.Add(Me.txtIncNm)
        Me.grpCdInfo1.Controls.Add(Me.lblIncNm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(9, 100)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(767, 440)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "균 결과 정보"
        '
        'txtIncNm
        '
        Me.txtIncNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncNm.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtIncNm.Location = New System.Drawing.Point(100, 24)
        Me.txtIncNm.MaxLength = 30
        Me.txtIncNm.Name = "txtIncNm"
        Me.txtIncNm.Size = New System.Drawing.Size(652, 21)
        Me.txtIncNm.TabIndex = 11
        Me.txtIncNm.Tag = "INCRSTNM"
        '
        'lblIncNm
        '
        Me.lblIncNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIncNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIncNm.ForeColor = System.Drawing.Color.White
        Me.lblIncNm.Location = New System.Drawing.Point(7, 24)
        Me.lblIncNm.Name = "lblIncNm"
        Me.lblIncNm.Size = New System.Drawing.Size(92, 21)
        Me.lblIncNm.TabIndex = 12
        Me.lblIncNm.Text = " 균 결과 내용"
        Me.lblIncNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnClear_spc)
        Me.grpCd.Controls.Add(Me.chkSpcGbn)
        Me.grpCd.Controls.Add(Me.btnCdHelp_spc)
        Me.grpCd.Controls.Add(Me.txtSpcNmd)
        Me.grpCd.Controls.Add(Me.txtSpcCd)
        Me.grpCd.Controls.Add(Me.lblSpcCd)
        Me.grpCd.Controls.Add(Me.btnCdHelp_test)
        Me.grpCd.Controls.Add(Me.txtTNmD)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.txtIncCd)
        Me.grpCd.Controls.Add(Me.lblIncCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(7, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(769, 94)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        '
        'btnClear_spc
        '
        Me.btnClear_spc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_spc.Location = New System.Drawing.Point(625, 40)
        Me.btnClear_spc.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_spc.Name = "btnClear_spc"
        Me.btnClear_spc.Size = New System.Drawing.Size(49, 21)
        Me.btnClear_spc.TabIndex = 9
        Me.btnClear_spc.TabStop = False
        Me.btnClear_spc.Text = "Clear"
        Me.btnClear_spc.UseVisualStyleBackColor = True
        Me.btnClear_spc.Visible = False
        '
        'chkSpcGbn
        '
        Me.chkSpcGbn.AutoSize = True
        Me.chkSpcGbn.Location = New System.Drawing.Point(489, 20)
        Me.chkSpcGbn.Name = "chkSpcGbn"
        Me.chkSpcGbn.Size = New System.Drawing.Size(108, 16)
        Me.chkSpcGbn.TabIndex = 4
        Me.chkSpcGbn.TabStop = False
        Me.chkSpcGbn.Text = "다중 검체 작업"
        Me.chkSpcGbn.UseVisualStyleBackColor = True
        '
        'btnCdHelp_spc
        '
        Me.btnCdHelp_spc.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_spc.Image = CType(resources.GetObject("btnCdHelp_spc.Image"), System.Drawing.Image)
        Me.btnCdHelp_spc.Location = New System.Drawing.Point(172, 40)
        Me.btnCdHelp_spc.Name = "btnCdHelp_spc"
        Me.btnCdHelp_spc.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_spc.TabIndex = 7
        Me.btnCdHelp_spc.TabStop = False
        Me.btnCdHelp_spc.UseVisualStyleBackColor = True
        '
        'txtSpcNmd
        '
        Me.txtSpcNmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSpcNmd.Location = New System.Drawing.Point(199, 40)
        Me.txtSpcNmd.Name = "txtSpcNmd"
        Me.txtSpcNmd.ReadOnly = True
        Me.txtSpcNmd.Size = New System.Drawing.Size(425, 21)
        Me.txtSpcNmd.TabIndex = 8
        Me.txtSpcNmd.TabStop = False
        Me.txtSpcNmd.Tag = "SPCNMD"
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(115, 40)
        Me.txtSpcCd.MaxLength = 7
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(56, 21)
        Me.txtSpcCd.TabIndex = 6
        Me.txtSpcCd.Tag = "SPCCD"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(6, 40)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(108, 21)
        Me.lblSpcCd.TabIndex = 5
        Me.lblSpcCd.Text = " 검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCdHelp_test
        '
        Me.btnCdHelp_test.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_test.Image = CType(resources.GetObject("btnCdHelp_test.Image"), System.Drawing.Image)
        Me.btnCdHelp_test.Location = New System.Drawing.Point(172, 17)
        Me.btnCdHelp_test.Name = "btnCdHelp_test"
        Me.btnCdHelp_test.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_test.TabIndex = 2
        Me.btnCdHelp_test.TabStop = False
        Me.btnCdHelp_test.UseVisualStyleBackColor = True
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(199, 17)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(284, 21)
        Me.txtTNmD.TabIndex = 3
        Me.txtTNmD.TabStop = False
        Me.txtTNmD.Tag = "TNMD"
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(115, 17)
        Me.txtTestCd.MaxLength = 7
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(56, 21)
        Me.txtTestCd.TabIndex = 1
        Me.txtTestCd.Tag = "TESTCD"
        '
        'lblTestCd
        '
        Me.lblTestCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestCd.ForeColor = System.Drawing.Color.White
        Me.lblTestCd.Location = New System.Drawing.Point(6, 17)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(108, 21)
        Me.lblTestCd.TabIndex = 0
        Me.lblTestCd.Text = " 검사코드"
        Me.lblTestCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIncCd
        '
        Me.txtIncCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIncCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtIncCd.Location = New System.Drawing.Point(115, 63)
        Me.txtIncCd.MaxLength = 3
        Me.txtIncCd.Name = "txtIncCd"
        Me.txtIncCd.Size = New System.Drawing.Size(56, 21)
        Me.txtIncCd.TabIndex = 10
        Me.txtIncCd.Tag = "INCRSTCD"
        '
        'lblIncCd
        '
        Me.lblIncCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIncCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIncCd.ForeColor = System.Drawing.Color.White
        Me.lblIncCd.Location = New System.Drawing.Point(6, 63)
        Me.lblIncCd.Name = "lblIncCd"
        Me.lblIncCd.Size = New System.Drawing.Size(108, 21)
        Me.lblIncCd.TabIndex = 10
        Me.lblIncCd.Text = " 균 결과 코드"
        Me.lblIncCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(684, 13)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 5
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF19
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 602)
        Me.Controls.Add(Me.tclSpc)
        Me.Name = "FDF19"
        Me.Text = "[19] 균 결과"
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tclSpc As System.Windows.Forms.TabControl
    Friend WithEvents tbcTpg As System.Windows.Forms.TabPage
    Friend WithEvents txtModID As System.Windows.Forms.TextBox
    Friend WithEvents lblModNm As System.Windows.Forms.Label
    Friend WithEvents txtModDT As System.Windows.Forms.TextBox
    Friend WithEvents lblModDT As System.Windows.Forms.Label
    Friend WithEvents txtRegDT As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblRegDT As System.Windows.Forms.Label
    Friend WithEvents txtRegID As System.Windows.Forms.TextBox
    Friend WithEvents grpCdInfo1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtIncNm As System.Windows.Forms.TextBox
    Friend WithEvents lblIncNm As System.Windows.Forms.Label
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtIncCd As System.Windows.Forms.TextBox
    Friend WithEvents lblIncCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents btnCdHelp_test As System.Windows.Forms.Button
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents btnCdHelp_spc As System.Windows.Forms.Button
    Friend WithEvents txtSpcNmd As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents chkSpcGbn As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear_spc As System.Windows.Forms.Button
End Class
