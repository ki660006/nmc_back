<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF43
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF43))
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tbcTpg = New System.Windows.Forms.TabPage()
        Me.txtModNm = New System.Windows.Forms.TextBox()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtModID = New System.Windows.Forms.TextBox()
        Me.lblModNm = New System.Windows.Forms.Label()
        Me.txtModDT = New System.Windows.Forms.TextBox()
        Me.lblModDT = New System.Windows.Forms.Label()
        Me.txtRegDT = New System.Windows.Forms.TextBox()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblRegDT = New System.Windows.Forms.Label()
        Me.txtRegID = New System.Windows.Forms.TextBox()
        Me.grpCdInfo1 = New System.Windows.Forms.GroupBox()
        Me.lblGuide2 = New System.Windows.Forms.Label()
        Me.pnlRstGbn = New System.Windows.Forms.Panel()
        Me.rdoFldGbnR = New System.Windows.Forms.RadioButton()
        Me.rdoFldGbnC = New System.Windows.Forms.RadioButton()
        Me.lblRstGbn = New System.Windows.Forms.Label()
        Me.lblCvt = New System.Windows.Forms.Label()
        Me.lblGuide1 = New System.Windows.Forms.Label()
        Me.txtCvtForm = New System.Windows.Forms.TextBox()
        Me.spdCvtTest = New AxFPSpreadADO.AxfpSpread()
        Me.lblCvtTest = New System.Windows.Forms.Label()
        Me.pnlCvtType = New System.Windows.Forms.Panel()
        Me.rdoCvtTypeM = New System.Windows.Forms.RadioButton()
        Me.rdoCvtTypeA = New System.Windows.Forms.RadioButton()
        Me.lblCvtType = New System.Windows.Forms.Label()
        Me.pnlCalR = New System.Windows.Forms.Panel()
        Me.rdoCvtBcNo = New System.Windows.Forms.RadioButton()
        Me.rdoCvtRegNo = New System.Windows.Forms.RadioButton()
        Me.lblCalR = New System.Windows.Forms.Label()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.btnCdHelp_rst = New System.Windows.Forms.Button()
        Me.btnCdHelp = New System.Windows.Forms.Button()
        Me.txtRstCont = New System.Windows.Forms.TextBox()
        Me.txtRstCd = New System.Windows.Forms.TextBox()
        Me.lblRstCd = New System.Windows.Forms.Label()
        Me.txtSpcNmD = New System.Windows.Forms.TextBox()
        Me.txtSpcCd = New System.Windows.Forms.TextBox()
        Me.lblSpcCd = New System.Windows.Forms.Label()
        Me.txtTNmD = New System.Windows.Forms.TextBox()
        Me.txtTestCd = New System.Windows.Forms.TextBox()
        Me.lblTestCd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        Me.pnlRstGbn.SuspendLayout()
        CType(Me.spdCvtTest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlCvtType.SuspendLayout()
        Me.pnlCalR.SuspendLayout()
        Me.grpCd.SuspendLayout()
        Me.SuspendLayout()
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
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
        Me.tbcTpg.Size = New System.Drawing.Size(787, 589)
        Me.tbcTpg.TabIndex = 0
        Me.tbcTpg.Text = "결과값 자동변환"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(301, 563)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 8
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(710, 563)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 7
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(301, 563)
        Me.txtModID.Name = "txtModID"
        Me.txtModID.ReadOnly = True
        Me.txtModID.Size = New System.Drawing.Size(68, 21)
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
        Me.lblModNm.Location = New System.Drawing.Point(216, 563)
        Me.lblModNm.Name = "lblModNm"
        Me.lblModNm.Size = New System.Drawing.Size(84, 21)
        Me.lblModNm.TabIndex = 5
        Me.lblModNm.Text = "변경삭제자"
        Me.lblModNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtModDT
        '
        Me.txtModDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModDT.BackColor = System.Drawing.Color.LightGray
        Me.txtModDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModDT.Location = New System.Drawing.Point(96, 563)
        Me.txtModDT.Name = "txtModDT"
        Me.txtModDT.ReadOnly = True
        Me.txtModDT.Size = New System.Drawing.Size(100, 21)
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
        Me.lblModDT.Location = New System.Drawing.Point(11, 563)
        Me.lblModDT.Name = "lblModDT"
        Me.lblModDT.Size = New System.Drawing.Size(84, 21)
        Me.lblModDT.TabIndex = 3
        Me.lblModDT.Text = "변경삭제일시"
        Me.lblModDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegDT
        '
        Me.txtRegDT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegDT.BackColor = System.Drawing.Color.LightGray
        Me.txtRegDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegDT.Location = New System.Drawing.Point(505, 563)
        Me.txtRegDT.Name = "txtRegDT"
        Me.txtRegDT.ReadOnly = True
        Me.txtRegDT.Size = New System.Drawing.Size(100, 21)
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
        Me.lblUserNm.Location = New System.Drawing.Point(625, 563)
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
        Me.lblRegDT.Location = New System.Drawing.Point(420, 563)
        Me.lblRegDT.Name = "lblRegDT"
        Me.lblRegDT.Size = New System.Drawing.Size(84, 21)
        Me.lblRegDT.TabIndex = 0
        Me.lblRegDT.Text = "최종등록일시"
        Me.lblRegDT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRegID
        '
        Me.txtRegID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegID.BackColor = System.Drawing.Color.LightGray
        Me.txtRegID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegID.Location = New System.Drawing.Point(710, 563)
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
        Me.grpCdInfo1.Controls.Add(Me.lblGuide2)
        Me.grpCdInfo1.Controls.Add(Me.pnlRstGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblRstGbn)
        Me.grpCdInfo1.Controls.Add(Me.lblCvt)
        Me.grpCdInfo1.Controls.Add(Me.lblGuide1)
        Me.grpCdInfo1.Controls.Add(Me.txtCvtForm)
        Me.grpCdInfo1.Controls.Add(Me.spdCvtTest)
        Me.grpCdInfo1.Controls.Add(Me.lblCvtTest)
        Me.grpCdInfo1.Controls.Add(Me.pnlCvtType)
        Me.grpCdInfo1.Controls.Add(Me.lblCvtType)
        Me.grpCdInfo1.Controls.Add(Me.pnlCalR)
        Me.grpCdInfo1.Controls.Add(Me.lblCalR)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(10, 141)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(772, 417)
        Me.grpCdInfo1.TabIndex = 1
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "계산식 정보"
        '
        'lblGuide2
        '
        Me.lblGuide2.AutoSize = True
        Me.lblGuide2.Location = New System.Drawing.Point(12, 387)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(173, 12)
        Me.lblGuide2.TabIndex = 146
        Me.lblGuide2.Text = "       의미: $$= and, ||= or"
        '
        'pnlRstGbn
        '
        Me.pnlRstGbn.Controls.Add(Me.rdoFldGbnR)
        Me.pnlRstGbn.Controls.Add(Me.rdoFldGbnC)
        Me.pnlRstGbn.Location = New System.Drawing.Point(78, 18)
        Me.pnlRstGbn.Name = "pnlRstGbn"
        Me.pnlRstGbn.Size = New System.Drawing.Size(440, 21)
        Me.pnlRstGbn.TabIndex = 0
        Me.pnlRstGbn.TabStop = True
        '
        'rdoFldGbnR
        '
        Me.rdoFldGbnR.Checked = True
        Me.rdoFldGbnR.Location = New System.Drawing.Point(20, 1)
        Me.rdoFldGbnR.Name = "rdoFldGbnR"
        Me.rdoFldGbnR.Size = New System.Drawing.Size(139, 19)
        Me.rdoFldGbnR.TabIndex = 11
        Me.rdoFldGbnR.TabStop = True
        Me.rdoFldGbnR.Text = "결과값 적용"
        '
        'rdoFldGbnC
        '
        Me.rdoFldGbnC.Location = New System.Drawing.Point(237, 1)
        Me.rdoFldGbnC.Name = "rdoFldGbnC"
        Me.rdoFldGbnC.Size = New System.Drawing.Size(103, 19)
        Me.rdoFldGbnC.TabIndex = 12
        Me.rdoFldGbnC.Text = "Comment 적용"
        '
        'lblRstGbn
        '
        Me.lblRstGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstGbn.ForeColor = System.Drawing.Color.White
        Me.lblRstGbn.Location = New System.Drawing.Point(12, 18)
        Me.lblRstGbn.Name = "lblRstGbn"
        Me.lblRstGbn.Size = New System.Drawing.Size(64, 21)
        Me.lblRstGbn.TabIndex = 0
        Me.lblRstGbn.Text = "결과구분"
        Me.lblRstGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCvt
        '
        Me.lblCvt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCvt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvt.ForeColor = System.Drawing.Color.White
        Me.lblCvt.Location = New System.Drawing.Point(10, 339)
        Me.lblCvt.Name = "lblCvt"
        Me.lblCvt.Size = New System.Drawing.Size(64, 21)
        Me.lblCvt.TabIndex = 2
        Me.lblCvt.Text = "계산식"
        Me.lblCvt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGuide1
        '
        Me.lblGuide1.AutoSize = True
        Me.lblGuide1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuide1.Location = New System.Drawing.Point(12, 368)
        Me.lblGuide1.Name = "lblGuide1"
        Me.lblGuide1.Size = New System.Drawing.Size(197, 12)
        Me.lblGuide1.TabIndex = 5
        Me.lblGuide1.Text = "※ 입력방법: [A] ~ [Z] ( ) || $$"
        '
        'txtCvtForm
        '
        Me.txtCvtForm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCvtForm.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCvtForm.Location = New System.Drawing.Point(75, 339)
        Me.txtCvtForm.MaxLength = 200
        Me.txtCvtForm.Name = "txtCvtForm"
        Me.txtCvtForm.Size = New System.Drawing.Size(688, 21)
        Me.txtCvtForm.TabIndex = 18
        '
        'spdCvtTest
        '
        Me.spdCvtTest.DataSource = Nothing
        Me.spdCvtTest.Location = New System.Drawing.Point(12, 121)
        Me.spdCvtTest.Name = "spdCvtTest"
        Me.spdCvtTest.OcxState = CType(resources.GetObject("spdCvtTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCvtTest.Size = New System.Drawing.Size(752, 215)
        Me.spdCvtTest.TabIndex = 17
        '
        'lblCvtTest
        '
        Me.lblCvtTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCvtTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvtTest.ForeColor = System.Drawing.Color.White
        Me.lblCvtTest.Location = New System.Drawing.Point(12, 99)
        Me.lblCvtTest.Name = "lblCvtTest"
        Me.lblCvtTest.Size = New System.Drawing.Size(64, 21)
        Me.lblCvtTest.TabIndex = 145
        Me.lblCvtTest.Text = "관련검사"
        Me.lblCvtTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCvtType
        '
        Me.pnlCvtType.Controls.Add(Me.rdoCvtTypeM)
        Me.pnlCvtType.Controls.Add(Me.rdoCvtTypeA)
        Me.pnlCvtType.Location = New System.Drawing.Point(78, 63)
        Me.pnlCvtType.Name = "pnlCvtType"
        Me.pnlCvtType.Size = New System.Drawing.Size(440, 21)
        Me.pnlCvtType.TabIndex = 144
        '
        'rdoCvtTypeM
        '
        Me.rdoCvtTypeM.Checked = True
        Me.rdoCvtTypeM.Location = New System.Drawing.Point(20, 2)
        Me.rdoCvtTypeM.Name = "rdoCvtTypeM"
        Me.rdoCvtTypeM.Size = New System.Drawing.Size(159, 18)
        Me.rdoCvtTypeM.TabIndex = 15
        Me.rdoCvtTypeM.TabStop = True
        Me.rdoCvtTypeM.Text = "수동 결과 입력 → 계산"
        Me.rdoCvtTypeM.UseCompatibleTextRendering = True
        '
        'rdoCvtTypeA
        '
        Me.rdoCvtTypeA.Location = New System.Drawing.Point(237, 2)
        Me.rdoCvtTypeA.Name = "rdoCvtTypeA"
        Me.rdoCvtTypeA.Size = New System.Drawing.Size(159, 18)
        Me.rdoCvtTypeA.TabIndex = 16
        Me.rdoCvtTypeA.Text = "자동 결과 전송 → 계산"
        Me.rdoCvtTypeA.UseCompatibleTextRendering = True
        '
        'lblCvtType
        '
        Me.lblCvtType.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCvtType.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvtType.ForeColor = System.Drawing.Color.White
        Me.lblCvtType.Location = New System.Drawing.Point(12, 62)
        Me.lblCvtType.Name = "lblCvtType"
        Me.lblCvtType.Size = New System.Drawing.Size(64, 21)
        Me.lblCvtType.TabIndex = 143
        Me.lblCvtType.Text = "계산방식"
        Me.lblCvtType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlCalR
        '
        Me.pnlCalR.Controls.Add(Me.rdoCvtBcNo)
        Me.pnlCalR.Controls.Add(Me.rdoCvtRegNo)
        Me.pnlCalR.Location = New System.Drawing.Point(78, 40)
        Me.pnlCalR.Name = "pnlCalR"
        Me.pnlCalR.Size = New System.Drawing.Size(440, 21)
        Me.pnlCalR.TabIndex = 1
        '
        'rdoCvtBcNo
        '
        Me.rdoCvtBcNo.Checked = True
        Me.rdoCvtBcNo.Location = New System.Drawing.Point(20, 1)
        Me.rdoCvtBcNo.Name = "rdoCvtBcNo"
        Me.rdoCvtBcNo.Size = New System.Drawing.Size(211, 19)
        Me.rdoCvtBcNo.TabIndex = 13
        Me.rdoCvtBcNo.TabStop = True
        Me.rdoCvtBcNo.Text = "본 검사"
        '
        'rdoCvtRegNo
        '
        Me.rdoCvtRegNo.Location = New System.Drawing.Point(237, 1)
        Me.rdoCvtRegNo.Name = "rdoCvtRegNo"
        Me.rdoCvtRegNo.Size = New System.Drawing.Size(191, 19)
        Me.rdoCvtRegNo.TabIndex = 14
        Me.rdoCvtRegNo.Text = "타 검사"
        '
        'lblCalR
        '
        Me.lblCalR.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCalR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCalR.ForeColor = System.Drawing.Color.White
        Me.lblCalR.Location = New System.Drawing.Point(12, 40)
        Me.lblCalR.Name = "lblCalR"
        Me.lblCalR.Size = New System.Drawing.Size(64, 21)
        Me.lblCalR.TabIndex = 1
        Me.lblCalR.Text = "계산범위"
        Me.lblCalR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnCdHelp_rst)
        Me.grpCd.Controls.Add(Me.btnCdHelp)
        Me.grpCd.Controls.Add(Me.txtRstCont)
        Me.grpCd.Controls.Add(Me.txtRstCd)
        Me.grpCd.Controls.Add(Me.lblRstCd)
        Me.grpCd.Controls.Add(Me.txtSpcNmD)
        Me.grpCd.Controls.Add(Me.txtSpcCd)
        Me.grpCd.Controls.Add(Me.lblSpcCd)
        Me.grpCd.Controls.Add(Me.txtTNmD)
        Me.grpCd.Controls.Add(Me.txtTestCd)
        Me.grpCd.Controls.Add(Me.lblTestCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(10, 12)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(771, 123)
        Me.grpCd.TabIndex = 0
        Me.grpCd.TabStop = False
        '
        'btnCdHelp_rst
        '
        Me.btnCdHelp_rst.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_rst.Image = CType(resources.GetObject("btnCdHelp_rst.Image"), System.Drawing.Image)
        Me.btnCdHelp_rst.Location = New System.Drawing.Point(152, 39)
        Me.btnCdHelp_rst.Name = "btnCdHelp_rst"
        Me.btnCdHelp_rst.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_rst.TabIndex = 9
        Me.btnCdHelp_rst.TabStop = False
        Me.btnCdHelp_rst.UseVisualStyleBackColor = True
        '
        'btnCdHelp
        '
        Me.btnCdHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp.Image = CType(resources.GetObject("btnCdHelp.Image"), System.Drawing.Image)
        Me.btnCdHelp.Location = New System.Drawing.Point(255, 17)
        Me.btnCdHelp.Name = "btnCdHelp"
        Me.btnCdHelp.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp.TabIndex = 4
        Me.btnCdHelp.TabStop = False
        Me.btnCdHelp.UseVisualStyleBackColor = True
        '
        'txtRstCont
        '
        Me.txtRstCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtRstCont.Location = New System.Drawing.Point(79, 62)
        Me.txtRstCont.Multiline = True
        Me.txtRstCont.Name = "txtRstCont"
        Me.txtRstCont.ReadOnly = True
        Me.txtRstCont.Size = New System.Drawing.Size(605, 43)
        Me.txtRstCont.TabIndex = 10
        '
        'txtRstCd
        '
        Me.txtRstCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRstCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRstCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtRstCd.Location = New System.Drawing.Point(79, 39)
        Me.txtRstCd.MaxLength = 7
        Me.txtRstCd.Name = "txtRstCd"
        Me.txtRstCd.Size = New System.Drawing.Size(72, 21)
        Me.txtRstCd.TabIndex = 8
        Me.txtRstCd.Tag = "RSTCDSEQ"
        '
        'lblRstCd
        '
        Me.lblRstCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstCd.ForeColor = System.Drawing.Color.White
        Me.lblRstCd.Location = New System.Drawing.Point(14, 39)
        Me.lblRstCd.Name = "lblRstCd"
        Me.lblRstCd.Size = New System.Drawing.Size(64, 21)
        Me.lblRstCd.TabIndex = 8
        Me.lblRstCd.Text = "결과코드"
        Me.lblRstCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpcNmD
        '
        Me.txtSpcNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSpcNmD.Location = New System.Drawing.Point(474, 17)
        Me.txtSpcNmD.Name = "txtSpcNmD"
        Me.txtSpcNmD.ReadOnly = True
        Me.txtSpcNmD.Size = New System.Drawing.Size(188, 21)
        Me.txtSpcNmD.TabIndex = 6
        '
        'txtSpcCd
        '
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtSpcCd.Location = New System.Drawing.Point(217, 17)
        Me.txtSpcCd.MaxLength = 5
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(37, 21)
        Me.txtSpcCd.TabIndex = 3
        Me.txtSpcCd.Tag = "TCLSCD"
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(152, 17)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(64, 21)
        Me.lblSpcCd.TabIndex = 2
        Me.lblSpcCd.Text = "검체코드"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTNmD
        '
        Me.txtTNmD.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTNmD.Location = New System.Drawing.Point(283, 17)
        Me.txtTNmD.Name = "txtTNmD"
        Me.txtTNmD.ReadOnly = True
        Me.txtTNmD.Size = New System.Drawing.Size(188, 21)
        Me.txtTNmD.TabIndex = 5
        '
        'txtTestCd
        '
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtTestCd.Location = New System.Drawing.Point(79, 17)
        Me.txtTestCd.MaxLength = 7
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
        Me.lblTestCd.Location = New System.Drawing.Point(14, 17)
        Me.lblTestCd.Name = "lblTestCd"
        Me.lblTestCd.Size = New System.Drawing.Size(64, 21)
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
        Me.btnUE.Location = New System.Drawing.Point(692, 12)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 7
        Me.btnUE.TabStop = False
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'FDF43
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 614)
        Me.Controls.Add(Me.tclSpc)
        Me.Name = "FDF43"
        Me.Text = "[43] 결과값 자동변환"
        CType(Me.errpd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        Me.pnlRstGbn.ResumeLayout(False)
        CType(Me.spdCvtTest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlCvtType.ResumeLayout(False)
        Me.pnlCalR.ResumeLayout(False)
        Me.grpCd.ResumeLayout(False)
        Me.grpCd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
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
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtRstCd As System.Windows.Forms.TextBox
    Friend WithEvents lblRstCd As System.Windows.Forms.Label
    Friend WithEvents txtSpcNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents txtTNmD As System.Windows.Forms.TextBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtRstCont As System.Windows.Forms.TextBox
    Friend WithEvents spdCvtTest As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblCvtTest As System.Windows.Forms.Label
    Friend WithEvents pnlCvtType As System.Windows.Forms.Panel
    Friend WithEvents rdoCvtTypeM As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCvtTypeA As System.Windows.Forms.RadioButton
    Friend WithEvents lblCvtType As System.Windows.Forms.Label
    Friend WithEvents pnlCalR As System.Windows.Forms.Panel
    Friend WithEvents rdoCvtBcNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCvtRegNo As System.Windows.Forms.RadioButton
    Friend WithEvents lblCalR As System.Windows.Forms.Label
    Friend WithEvents lblGuide1 As System.Windows.Forms.Label
    Friend WithEvents txtCvtForm As System.Windows.Forms.TextBox
    Friend WithEvents lblCvt As System.Windows.Forms.Label
    Friend WithEvents pnlRstGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoFldGbnR As System.Windows.Forms.RadioButton
    Friend WithEvents rdoFldGbnC As System.Windows.Forms.RadioButton
    Friend WithEvents lblRstGbn As System.Windows.Forms.Label
    Friend WithEvents btnCdHelp As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_rst As System.Windows.Forms.Button
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
End Class
