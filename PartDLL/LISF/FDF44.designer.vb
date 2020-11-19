<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FDF44
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FDF44))
        Me.tclSpc = New System.Windows.Forms.TabControl()
        Me.tbcTpg = New System.Windows.Forms.TabPage()
        Me.txtRegNm = New System.Windows.Forms.TextBox()
        Me.txtModNm = New System.Windows.Forms.TextBox()
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
        Me.lblGuide1 = New System.Windows.Forms.Label()
        Me.spdCvtTest = New AxFPSpreadADO.AxfpSpread()
        Me.lblCvtTest = New System.Windows.Forms.Label()
        Me.lblCvtForm = New System.Windows.Forms.Label()
        Me.txtCvtForm = New System.Windows.Forms.TextBox()
        Me.grpCd = New System.Windows.Forms.GroupBox()
        Me.btnSelCmt = New System.Windows.Forms.Button()
        Me.txtCmtCont = New System.Windows.Forms.TextBox()
        Me.txtSlipNmd = New System.Windows.Forms.TextBox()
        Me.txtCmtCd = New System.Windows.Forms.TextBox()
        Me.lblCmtCd = New System.Windows.Forms.Label()
        Me.btnUE = New System.Windows.Forms.Button()
        Me.errpd = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.tclSpc.SuspendLayout()
        Me.tbcTpg.SuspendLayout()
        Me.grpCdInfo1.SuspendLayout()
        CType(Me.spdCvtTest, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.tclSpc.Size = New System.Drawing.Size(795, 614)
        Me.tclSpc.TabIndex = 1
        Me.tclSpc.TabStop = False
        '
        'tbcTpg
        '
        Me.tbcTpg.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbcTpg.Controls.Add(Me.txtRegNm)
        Me.tbcTpg.Controls.Add(Me.txtModNm)
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
        Me.tbcTpg.Text = "소견정보"
        '
        'txtRegNm
        '
        Me.txtRegNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRegNm.BackColor = System.Drawing.Color.LightGray
        Me.txtRegNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNm.Location = New System.Drawing.Point(702, 551)
        Me.txtRegNm.Name = "txtRegNm"
        Me.txtRegNm.ReadOnly = True
        Me.txtRegNm.Size = New System.Drawing.Size(68, 21)
        Me.txtRegNm.TabIndex = 142
        Me.txtRegNm.TabStop = False
        Me.txtRegNm.Tag = "REGNM"
        '
        'txtModNm
        '
        Me.txtModNm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModNm.BackColor = System.Drawing.Color.LightGray
        Me.txtModNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModNm.Location = New System.Drawing.Point(295, 553)
        Me.txtModNm.Name = "txtModNm"
        Me.txtModNm.ReadOnly = True
        Me.txtModNm.Size = New System.Drawing.Size(68, 21)
        Me.txtModNm.TabIndex = 141
        Me.txtModNm.TabStop = False
        Me.txtModNm.Tag = "MODNM"
        '
        'txtModID
        '
        Me.txtModID.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtModID.BackColor = System.Drawing.Color.LightGray
        Me.txtModID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtModID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtModID.Location = New System.Drawing.Point(295, 553)
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
        Me.lblModNm.Location = New System.Drawing.Point(210, 553)
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
        Me.txtModDT.Location = New System.Drawing.Point(95, 553)
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
        Me.lblModDT.Location = New System.Drawing.Point(10, 553)
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
        Me.txtRegDT.Location = New System.Drawing.Point(502, 551)
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
        Me.lblUserNm.Location = New System.Drawing.Point(617, 551)
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
        Me.lblRegDT.Location = New System.Drawing.Point(417, 551)
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
        Me.txtRegID.Location = New System.Drawing.Point(702, 551)
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
        Me.grpCdInfo1.Controls.Add(Me.lblGuide1)
        Me.grpCdInfo1.Controls.Add(Me.spdCvtTest)
        Me.grpCdInfo1.Controls.Add(Me.lblCvtTest)
        Me.grpCdInfo1.Controls.Add(Me.lblCvtForm)
        Me.grpCdInfo1.Controls.Add(Me.txtCvtForm)
        Me.grpCdInfo1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCdInfo1.Location = New System.Drawing.Point(8, 202)
        Me.grpCdInfo1.Name = "grpCdInfo1"
        Me.grpCdInfo1.Size = New System.Drawing.Size(764, 338)
        Me.grpCdInfo1.TabIndex = 2
        Me.grpCdInfo1.TabStop = False
        Me.grpCdInfo1.Text = "계산식 정보"
        '
        'lblGuide2
        '
        Me.lblGuide2.AutoSize = True
        Me.lblGuide2.Location = New System.Drawing.Point(6, 326)
        Me.lblGuide2.Name = "lblGuide2"
        Me.lblGuide2.Size = New System.Drawing.Size(173, 12)
        Me.lblGuide2.TabIndex = 149
        Me.lblGuide2.Text = "       의미: $$= and, ||= or"
        '
        'lblGuide1
        '
        Me.lblGuide1.AutoSize = True
        Me.lblGuide1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGuide1.Location = New System.Drawing.Point(6, 307)
        Me.lblGuide1.Name = "lblGuide1"
        Me.lblGuide1.Size = New System.Drawing.Size(197, 12)
        Me.lblGuide1.TabIndex = 148
        Me.lblGuide1.Text = "※ 입력방법: [A] ~ [Z] ( ) || $$"
        '
        'spdCvtTest
        '
        Me.spdCvtTest.DataSource = Nothing
        Me.spdCvtTest.Location = New System.Drawing.Point(6, 50)
        Me.spdCvtTest.Name = "spdCvtTest"
        Me.spdCvtTest.OcxState = CType(resources.GetObject("spdCvtTest.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdCvtTest.Size = New System.Drawing.Size(749, 218)
        Me.spdCvtTest.TabIndex = 147
        '
        'lblCvtTest
        '
        Me.lblCvtTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCvtTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvtTest.ForeColor = System.Drawing.Color.White
        Me.lblCvtTest.Location = New System.Drawing.Point(6, 26)
        Me.lblCvtTest.Name = "lblCvtTest"
        Me.lblCvtTest.Size = New System.Drawing.Size(64, 21)
        Me.lblCvtTest.TabIndex = 146
        Me.lblCvtTest.Text = "관련검사"
        Me.lblCvtTest.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCvtForm
        '
        Me.lblCvtForm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCvtForm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCvtForm.ForeColor = System.Drawing.Color.White
        Me.lblCvtForm.Location = New System.Drawing.Point(6, 280)
        Me.lblCvtForm.Name = "lblCvtForm"
        Me.lblCvtForm.Size = New System.Drawing.Size(45, 21)
        Me.lblCvtForm.TabIndex = 6
        Me.lblCvtForm.Text = "계산식"
        Me.lblCvtForm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtCvtForm
        '
        Me.txtCvtForm.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCvtForm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCvtForm.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCvtForm.Location = New System.Drawing.Point(53, 280)
        Me.txtCvtForm.MaxLength = 200
        Me.txtCvtForm.Name = "txtCvtForm"
        Me.txtCvtForm.Size = New System.Drawing.Size(701, 21)
        Me.txtCvtForm.TabIndex = 7
        '
        'grpCd
        '
        Me.grpCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpCd.Controls.Add(Me.btnSelCmt)
        Me.grpCd.Controls.Add(Me.txtCmtCont)
        Me.grpCd.Controls.Add(Me.txtSlipNmd)
        Me.grpCd.Controls.Add(Me.txtCmtCd)
        Me.grpCd.Controls.Add(Me.lblCmtCd)
        Me.grpCd.Controls.Add(Me.btnUE)
        Me.grpCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.grpCd.Location = New System.Drawing.Point(8, 4)
        Me.grpCd.Name = "grpCd"
        Me.grpCd.Size = New System.Drawing.Size(764, 192)
        Me.grpCd.TabIndex = 1
        Me.grpCd.TabStop = False
        '
        'btnSelCmt
        '
        Me.btnSelCmt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSelCmt.Image = CType(resources.GetObject("btnSelCmt.Image"), System.Drawing.Image)
        Me.btnSelCmt.Location = New System.Drawing.Point(158, 16)
        Me.btnSelCmt.Name = "btnSelCmt"
        Me.btnSelCmt.Size = New System.Drawing.Size(26, 21)
        Me.btnSelCmt.TabIndex = 181
        Me.btnSelCmt.UseVisualStyleBackColor = True
        '
        'txtCmtCont
        '
        Me.txtCmtCont.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCmtCont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCont.Location = New System.Drawing.Point(87, 47)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(668, 142)
        Me.txtCmtCont.TabIndex = 13
        Me.txtCmtCont.Tag = "CMTCONT"
        '
        'txtSlipNmd
        '
        Me.txtSlipNmd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSlipNmd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlipNmd.Location = New System.Drawing.Point(185, 16)
        Me.txtSlipNmd.Name = "txtSlipNmd"
        Me.txtSlipNmd.Size = New System.Drawing.Size(233, 21)
        Me.txtSlipNmd.TabIndex = 12
        Me.txtSlipNmd.Tag = "SECTNMD"
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtCmtCd.Location = New System.Drawing.Point(86, 16)
        Me.txtCmtCd.MaxLength = 5
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(72, 21)
        Me.txtCmtCd.TabIndex = 4
        Me.txtCmtCd.Tag = "CMTPCD"
        '
        'lblCmtCd
        '
        Me.lblCmtCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCmtCd.ForeColor = System.Drawing.Color.White
        Me.lblCmtCd.Location = New System.Drawing.Point(8, 16)
        Me.lblCmtCd.Name = "lblCmtCd"
        Me.lblCmtCd.Size = New System.Drawing.Size(77, 21)
        Me.lblCmtCd.TabIndex = 7
        Me.lblCmtCd.Text = "주 소견코드"
        Me.lblCmtCd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnUE
        '
        Me.btnUE.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.btnUE.Enabled = False
        Me.btnUE.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUE.ForeColor = System.Drawing.Color.White
        Me.btnUE.Location = New System.Drawing.Point(683, 14)
        Me.btnUE.Name = "btnUE"
        Me.btnUE.Size = New System.Drawing.Size(72, 27)
        Me.btnUE.TabIndex = 6
        Me.btnUE.Text = "사용종료"
        Me.btnUE.UseVisualStyleBackColor = False
        '
        'errpd
        '
        Me.errpd.ContainerControl = Me
        '
        'FDF44
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(795, 614)
        Me.Controls.Add(Me.tclSpc)
        Me.Name = "FDF44"
        Me.Text = "[44] 소견값 자동변환"
        Me.tclSpc.ResumeLayout(False)
        Me.tbcTpg.ResumeLayout(False)
        Me.tbcTpg.PerformLayout()
        Me.grpCdInfo1.ResumeLayout(False)
        Me.grpCdInfo1.PerformLayout()
        CType(Me.spdCvtTest, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents grpCd As System.Windows.Forms.GroupBox
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents lblCmtCd As System.Windows.Forms.Label
    Friend WithEvents btnUE As System.Windows.Forms.Button
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents txtSlipNmd As System.Windows.Forms.TextBox
    Friend WithEvents lblCvtForm As System.Windows.Forms.Label
    Friend WithEvents txtCvtForm As System.Windows.Forms.TextBox
    Friend WithEvents lblCvtTest As System.Windows.Forms.Label
    Friend WithEvents errpd As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnSelCmt As System.Windows.Forms.Button
    Friend WithEvents txtModNm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNm As System.Windows.Forms.TextBox
    Friend WithEvents lblGuide2 As System.Windows.Forms.Label
    Friend WithEvents lblGuide1 As System.Windows.Forms.Label
    Friend WithEvents spdCvtTest As AxFPSpreadADO.AxfpSpread
End Class
