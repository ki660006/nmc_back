<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPOPUPST_BM_NMC
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPOPUPST_BM_NMC))
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblRBC = New System.Windows.Forms.Label
        Me.lblWBC = New System.Windows.Forms.Label
        Me.lblPLT = New System.Windows.Forms.Label
        Me.txtRBC = New System.Windows.Forms.TextBox
        Me.txtWBC = New System.Windows.Forms.TextBox
        Me.txtPLT = New System.Windows.Forms.TextBox
        Me.lblDiif = New System.Windows.Forms.Label
        Me.lblTcount = New System.Windows.Forms.Label
        Me.txtTCount = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblM = New System.Windows.Forms.Label
        Me.txtM = New System.Windows.Forms.TextBox
        Me.lblE = New System.Windows.Forms.Label
        Me.lblPb = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCBC_Hb = New System.Windows.Forms.TextBox
        Me.txtCBC_WBC = New System.Windows.Forms.TextBox
        Me.txtCBC_PLT = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdDiff = New AxFPSpreadADO.AxfpSpread
        Me.txtOther = New System.Windows.Forms.TextBox
        Me.txtMega = New System.Windows.Forms.TextBox
        Me.btnHelp_r = New System.Windows.Forms.Button
        Me.btnHelp_w = New System.Windows.Forms.Button
        Me.btnHelp_p = New System.Windows.Forms.Button
        Me.txtCell = New System.Windows.Forms.TextBox
        Me.lblCell = New System.Windows.Forms.Label
        Me.lblOther = New System.Windows.Forms.Label
        Me.btnHelp_oth = New System.Windows.Forms.Button
        Me.lblMega = New System.Windows.Forms.Label
        Me.btnHelp_mega = New System.Windows.Forms.Button
        Me.lblBM = New System.Windows.Forms.Label
        Me.txtBm = New System.Windows.Forms.TextBox
        Me.btnHelp_bm = New System.Windows.Forms.Button
        Me.lblHemato = New System.Windows.Forms.Label
        Me.btnHelp_hemato = New System.Windows.Forms.Button
        Me.txtHemato = New System.Windows.Forms.TextBox
        Me.lblK = New System.Windows.Forms.Label
        Me.lblSlide = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSlideno2 = New System.Windows.Forms.TextBox
        Me.txtSlideno1 = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        CType(Me.spdDiff, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(619, 646)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(80, 36)
        Me.btnClose.TabIndex = 13
        Me.btnClose.Text = "닫기(Esc)"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Location = New System.Drawing.Point(534, 646)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 36)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "저장(F2)"
        '
        'lblRBC
        '
        Me.lblRBC.AutoSize = True
        Me.lblRBC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRBC.Location = New System.Drawing.Point(29, 82)
        Me.lblRBC.Margin = New System.Windows.Forms.Padding(1)
        Me.lblRBC.Name = "lblRBC"
        Me.lblRBC.Size = New System.Drawing.Size(35, 12)
        Me.lblRBC.TabIndex = 87
        Me.lblRBC.Text = "RBC :"
        Me.lblRBC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWBC
        '
        Me.lblWBC.AutoSize = True
        Me.lblWBC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWBC.Location = New System.Drawing.Point(29, 104)
        Me.lblWBC.Margin = New System.Windows.Forms.Padding(1)
        Me.lblWBC.Name = "lblWBC"
        Me.lblWBC.Size = New System.Drawing.Size(35, 12)
        Me.lblWBC.TabIndex = 88
        Me.lblWBC.Text = "WBC :"
        Me.lblWBC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPLT
        '
        Me.lblPLT.AutoSize = True
        Me.lblPLT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPLT.Location = New System.Drawing.Point(29, 125)
        Me.lblPLT.Margin = New System.Windows.Forms.Padding(1)
        Me.lblPLT.Name = "lblPLT"
        Me.lblPLT.Size = New System.Drawing.Size(35, 12)
        Me.lblPLT.TabIndex = 89
        Me.lblPLT.Text = "PLT :"
        Me.lblPLT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRBC
        '
        Me.txtRBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRBC.Location = New System.Drawing.Point(68, 74)
        Me.txtRBC.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRBC.Multiline = True
        Me.txtRBC.Name = "txtRBC"
        Me.txtRBC.Size = New System.Drawing.Size(603, 21)
        Me.txtRBC.TabIndex = 4
        Me.txtRBC.Tag = "06"
        '
        'txtWBC
        '
        Me.txtWBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWBC.Location = New System.Drawing.Point(68, 96)
        Me.txtWBC.Margin = New System.Windows.Forms.Padding(1)
        Me.txtWBC.Multiline = True
        Me.txtWBC.Name = "txtWBC"
        Me.txtWBC.Size = New System.Drawing.Size(603, 21)
        Me.txtWBC.TabIndex = 5
        Me.txtWBC.Tag = "07"
        '
        'txtPLT
        '
        Me.txtPLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPLT.Location = New System.Drawing.Point(68, 118)
        Me.txtPLT.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPLT.Multiline = True
        Me.txtPLT.Name = "txtPLT"
        Me.txtPLT.Size = New System.Drawing.Size(603, 21)
        Me.txtPLT.TabIndex = 6
        Me.txtPLT.Tag = "08"
        '
        'lblDiif
        '
        Me.lblDiif.AutoSize = True
        Me.lblDiif.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDiif.Location = New System.Drawing.Point(10, 158)
        Me.lblDiif.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDiif.Name = "lblDiif"
        Me.lblDiif.Size = New System.Drawing.Size(145, 12)
        Me.lblDiif.TabIndex = 93
        Me.lblDiif.Text = "Bone Marrow Findings"
        Me.lblDiif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTcount
        '
        Me.lblTcount.AutoSize = True
        Me.lblTcount.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTcount.Location = New System.Drawing.Point(324, 182)
        Me.lblTcount.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTcount.Name = "lblTcount"
        Me.lblTcount.Size = New System.Drawing.Size(83, 12)
        Me.lblTcount.TabIndex = 94
        Me.lblTcount.Text = "Total Count :"
        Me.lblTcount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTCount
        '
        Me.txtTCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTCount.Location = New System.Drawing.Point(412, 177)
        Me.txtTCount.Name = "txtTCount"
        Me.txtTCount.Size = New System.Drawing.Size(57, 21)
        Me.txtTCount.TabIndex = 7
        Me.txtTCount.Tag = "09"
        Me.txtTCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.Location = New System.Drawing.Point(473, 182)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(11, 12)
        Me.Label6.TabIndex = 96
        Me.Label6.Text = "%"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblM
        '
        Me.lblM.AutoSize = True
        Me.lblM.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblM.Location = New System.Drawing.Point(507, 182)
        Me.lblM.Margin = New System.Windows.Forms.Padding(1)
        Me.lblM.Name = "lblM"
        Me.lblM.Size = New System.Drawing.Size(83, 12)
        Me.lblM.TabIndex = 97
        Me.lblM.Text = "M : E ratio ="
        Me.lblM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtM
        '
        Me.txtM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtM.Location = New System.Drawing.Point(594, 177)
        Me.txtM.Name = "txtM"
        Me.txtM.Size = New System.Drawing.Size(75, 21)
        Me.txtM.TabIndex = 8
        Me.txtM.Tag = "28"
        Me.txtM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblE
        '
        Me.lblE.AutoSize = True
        Me.lblE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblE.Location = New System.Drawing.Point(673, 182)
        Me.lblE.Margin = New System.Windows.Forms.Padding(1)
        Me.lblE.Name = "lblE"
        Me.lblE.Size = New System.Drawing.Size(23, 12)
        Me.lblE.TabIndex = 99
        Me.lblE.Text = ": 1"
        Me.lblE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPb
        '
        Me.lblPb.AutoSize = True
        Me.lblPb.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPb.Location = New System.Drawing.Point(10, 52)
        Me.lblPb.Margin = New System.Windows.Forms.Padding(1)
        Me.lblPb.Name = "lblPb"
        Me.lblPb.Size = New System.Drawing.Size(180, 12)
        Me.lblPb.TabIndex = 100
        Me.lblPb.Text = "Peripheral Blood Findings"
        Me.lblPb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.Location = New System.Drawing.Point(390, 55)
        Me.Label10.Margin = New System.Windows.Forms.Padding(1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 101
        Me.Label10.Text = "Hb - WBC - PLT :"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCBC_Hb
        '
        Me.txtCBC_Hb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCBC_Hb.Location = New System.Drawing.Point(495, 50)
        Me.txtCBC_Hb.Name = "txtCBC_Hb"
        Me.txtCBC_Hb.Size = New System.Drawing.Size(37, 21)
        Me.txtCBC_Hb.TabIndex = 0
        Me.txtCBC_Hb.Tag = "04"
        Me.txtCBC_Hb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCBC_WBC
        '
        Me.txtCBC_WBC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCBC_WBC.Location = New System.Drawing.Point(551, 50)
        Me.txtCBC_WBC.Name = "txtCBC_WBC"
        Me.txtCBC_WBC.Size = New System.Drawing.Size(37, 21)
        Me.txtCBC_WBC.TabIndex = 1
        Me.txtCBC_WBC.Tag = "03"
        Me.txtCBC_WBC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtCBC_PLT
        '
        Me.txtCBC_PLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCBC_PLT.Location = New System.Drawing.Point(606, 50)
        Me.txtCBC_PLT.Name = "txtCBC_PLT"
        Me.txtCBC_PLT.Size = New System.Drawing.Size(37, 21)
        Me.txtCBC_PLT.TabIndex = 2
        Me.txtCBC_PLT.Tag = "05"
        Me.txtCBC_PLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.Location = New System.Drawing.Point(591, 55)
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
        Me.Label13.Location = New System.Drawing.Point(536, 55)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(11, 12)
        Me.Label13.TabIndex = 108
        Me.Label13.Text = "-"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.spdDiff)
        Me.Panel1.Location = New System.Drawing.Point(30, 203)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(667, 155)
        Me.Panel1.TabIndex = 9
        '
        'spdDiff
        '
        Me.spdDiff.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdDiff.DataSource = Nothing
        Me.spdDiff.Location = New System.Drawing.Point(1, 0)
        Me.spdDiff.Name = "spdDiff"
        Me.spdDiff.OcxState = CType(resources.GetObject("spdDiff.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDiff.Size = New System.Drawing.Size(665, 155)
        Me.spdDiff.TabIndex = 9
        '
        'txtOther
        '
        Me.txtOther.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOther.Location = New System.Drawing.Point(126, 359)
        Me.txtOther.Margin = New System.Windows.Forms.Padding(1)
        Me.txtOther.Multiline = True
        Me.txtOther.Name = "txtOther"
        Me.txtOther.Size = New System.Drawing.Size(543, 39)
        Me.txtOther.TabIndex = 10
        Me.txtOther.Tag = "27"
        '
        'txtMega
        '
        Me.txtMega.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtMega.Location = New System.Drawing.Point(147, 400)
        Me.txtMega.Name = "txtMega"
        Me.txtMega.Size = New System.Drawing.Size(522, 21)
        Me.txtMega.TabIndex = 11
        Me.txtMega.Tag = "32"
        '
        'btnHelp_r
        '
        Me.btnHelp_r.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_r.Image = CType(resources.GetObject("btnHelp_r.Image"), System.Drawing.Image)
        Me.btnHelp_r.Location = New System.Drawing.Point(673, 74)
        Me.btnHelp_r.Name = "btnHelp_r"
        Me.btnHelp_r.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_r.TabIndex = 112
        Me.btnHelp_r.UseVisualStyleBackColor = True
        '
        'btnHelp_w
        '
        Me.btnHelp_w.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_w.Image = CType(resources.GetObject("btnHelp_w.Image"), System.Drawing.Image)
        Me.btnHelp_w.Location = New System.Drawing.Point(673, 96)
        Me.btnHelp_w.Name = "btnHelp_w"
        Me.btnHelp_w.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_w.TabIndex = 113
        Me.btnHelp_w.UseVisualStyleBackColor = True
        '
        'btnHelp_p
        '
        Me.btnHelp_p.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_p.Image = CType(resources.GetObject("btnHelp_p.Image"), System.Drawing.Image)
        Me.btnHelp_p.Location = New System.Drawing.Point(673, 118)
        Me.btnHelp_p.Name = "btnHelp_p"
        Me.btnHelp_p.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_p.TabIndex = 114
        Me.btnHelp_p.UseVisualStyleBackColor = True
        '
        'txtCell
        '
        Me.txtCell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCell.Location = New System.Drawing.Point(116, 177)
        Me.txtCell.Name = "txtCell"
        Me.txtCell.Size = New System.Drawing.Size(154, 21)
        Me.txtCell.TabIndex = 115
        Me.txtCell.Tag = "01"
        Me.txtCell.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblCell
        '
        Me.lblCell.AutoSize = True
        Me.lblCell.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCell.Location = New System.Drawing.Point(29, 182)
        Me.lblCell.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCell.Name = "lblCell"
        Me.lblCell.Size = New System.Drawing.Size(83, 12)
        Me.lblCell.TabIndex = 116
        Me.lblCell.Text = "Cellularity :"
        Me.lblCell.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOther
        '
        Me.lblOther.AutoSize = True
        Me.lblOther.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOther.Location = New System.Drawing.Point(76, 364)
        Me.lblOther.Margin = New System.Windows.Forms.Padding(1)
        Me.lblOther.Name = "lblOther"
        Me.lblOther.Size = New System.Drawing.Size(41, 12)
        Me.lblOther.TabIndex = 117
        Me.lblOther.Text = "Others"
        Me.lblOther.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnHelp_oth
        '
        Me.btnHelp_oth.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_oth.Image = CType(resources.GetObject("btnHelp_oth.Image"), System.Drawing.Image)
        Me.btnHelp_oth.Location = New System.Drawing.Point(671, 359)
        Me.btnHelp_oth.Name = "btnHelp_oth"
        Me.btnHelp_oth.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_oth.TabIndex = 118
        Me.btnHelp_oth.UseVisualStyleBackColor = True
        '
        'lblMega
        '
        Me.lblMega.AutoSize = True
        Me.lblMega.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMega.Location = New System.Drawing.Point(29, 408)
        Me.lblMega.Margin = New System.Windows.Forms.Padding(1)
        Me.lblMega.Name = "lblMega"
        Me.lblMega.Size = New System.Drawing.Size(113, 12)
        Me.lblMega.TabIndex = 119
        Me.lblMega.Text = "Megakaryocytes are"
        Me.lblMega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnHelp_mega
        '
        Me.btnHelp_mega.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_mega.Image = CType(resources.GetObject("btnHelp_mega.Image"), System.Drawing.Image)
        Me.btnHelp_mega.Location = New System.Drawing.Point(671, 400)
        Me.btnHelp_mega.Name = "btnHelp_mega"
        Me.btnHelp_mega.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_mega.TabIndex = 120
        Me.btnHelp_mega.UseVisualStyleBackColor = True
        '
        'lblBM
        '
        Me.lblBM.AutoSize = True
        Me.lblBM.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBM.Location = New System.Drawing.Point(28, 436)
        Me.lblBM.Margin = New System.Windows.Forms.Padding(1)
        Me.lblBM.Name = "lblBM"
        Me.lblBM.Size = New System.Drawing.Size(98, 13)
        Me.lblBM.TabIndex = 121
        Me.lblBM.Text = "BM ASPIRATION"
        Me.lblBM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBm
        '
        Me.txtBm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBm.Location = New System.Drawing.Point(68, 454)
        Me.txtBm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBm.Multiline = True
        Me.txtBm.Name = "txtBm"
        Me.txtBm.Size = New System.Drawing.Size(600, 45)
        Me.txtBm.TabIndex = 122
        Me.txtBm.Tag = "25"
        '
        'btnHelp_bm
        '
        Me.btnHelp_bm.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_bm.Image = CType(resources.GetObject("btnHelp_bm.Image"), System.Drawing.Image)
        Me.btnHelp_bm.Location = New System.Drawing.Point(671, 454)
        Me.btnHelp_bm.Name = "btnHelp_bm"
        Me.btnHelp_bm.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_bm.TabIndex = 123
        Me.btnHelp_bm.UseVisualStyleBackColor = True
        '
        'lblHemato
        '
        Me.lblHemato.AutoSize = True
        Me.lblHemato.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblHemato.Location = New System.Drawing.Point(10, 520)
        Me.lblHemato.Margin = New System.Windows.Forms.Padding(1)
        Me.lblHemato.Name = "lblHemato"
        Me.lblHemato.Size = New System.Drawing.Size(152, 12)
        Me.lblHemato.TabIndex = 124
        Me.lblHemato.Text = "Hematologic Diagnosis"
        Me.lblHemato.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnHelp_hemato
        '
        Me.btnHelp_hemato.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnHelp_hemato.Image = CType(resources.GetObject("btnHelp_hemato.Image"), System.Drawing.Image)
        Me.btnHelp_hemato.Location = New System.Drawing.Point(671, 540)
        Me.btnHelp_hemato.Name = "btnHelp_hemato"
        Me.btnHelp_hemato.Size = New System.Drawing.Size(25, 21)
        Me.btnHelp_hemato.TabIndex = 126
        Me.btnHelp_hemato.UseVisualStyleBackColor = True
        '
        'txtHemato
        '
        Me.txtHemato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHemato.Location = New System.Drawing.Point(68, 540)
        Me.txtHemato.Margin = New System.Windows.Forms.Padding(1)
        Me.txtHemato.Multiline = True
        Me.txtHemato.Name = "txtHemato"
        Me.txtHemato.Size = New System.Drawing.Size(600, 59)
        Me.txtHemato.TabIndex = 125
        Me.txtHemato.Tag = "29"
        '
        'lblK
        '
        Me.lblK.AutoSize = True
        Me.lblK.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblK.Location = New System.Drawing.Point(647, 55)
        Me.lblK.Margin = New System.Windows.Forms.Padding(1)
        Me.lblK.Name = "lblK"
        Me.lblK.Size = New System.Drawing.Size(11, 12)
        Me.lblK.TabIndex = 127
        Me.lblK.Text = "K"
        Me.lblK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSlide
        '
        Me.lblSlide.AutoSize = True
        Me.lblSlide.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlide.Location = New System.Drawing.Point(10, 25)
        Me.lblSlide.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSlide.Name = "lblSlide"
        Me.lblSlide.Size = New System.Drawing.Size(68, 12)
        Me.lblSlide.TabIndex = 128
        Me.lblSlide.Text = "Slide No."
        Me.lblSlide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.Location = New System.Drawing.Point(102, 21)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 131
        Me.Label1.Text = "-"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSlideno2
        '
        Me.txtSlideno2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlideno2.Location = New System.Drawing.Point(118, 16)
        Me.txtSlideno2.Name = "txtSlideno2"
        Me.txtSlideno2.Size = New System.Drawing.Size(45, 21)
        Me.txtSlideno2.TabIndex = 130
        Me.txtSlideno2.Tag = ""
        Me.txtSlideno2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSlideno1
        '
        Me.txtSlideno1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSlideno1.Location = New System.Drawing.Point(78, 16)
        Me.txtSlideno1.Name = "txtSlideno1"
        Me.txtSlideno1.Size = New System.Drawing.Size(20, 21)
        Me.txtSlideno1.TabIndex = 129
        Me.txtSlideno1.Tag = "02"
        Me.txtSlideno1.Text = "B"
        Me.txtSlideno1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'FGPOPUPST_BM_NMC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(712, 690)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSlideno2)
        Me.Controls.Add(Me.txtSlideno1)
        Me.Controls.Add(Me.lblSlide)
        Me.Controls.Add(Me.lblK)
        Me.Controls.Add(Me.btnHelp_hemato)
        Me.Controls.Add(Me.txtHemato)
        Me.Controls.Add(Me.lblHemato)
        Me.Controls.Add(Me.btnHelp_bm)
        Me.Controls.Add(Me.txtBm)
        Me.Controls.Add(Me.lblBM)
        Me.Controls.Add(Me.btnHelp_mega)
        Me.Controls.Add(Me.lblMega)
        Me.Controls.Add(Me.btnHelp_oth)
        Me.Controls.Add(Me.lblOther)
        Me.Controls.Add(Me.txtCell)
        Me.Controls.Add(Me.lblCell)
        Me.Controls.Add(Me.btnHelp_p)
        Me.Controls.Add(Me.btnHelp_w)
        Me.Controls.Add(Me.btnHelp_r)
        Me.Controls.Add(Me.txtMega)
        Me.Controls.Add(Me.txtOther)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtCBC_PLT)
        Me.Controls.Add(Me.txtCBC_WBC)
        Me.Controls.Add(Me.txtCBC_Hb)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblPb)
        Me.Controls.Add(Me.lblE)
        Me.Controls.Add(Me.txtM)
        Me.Controls.Add(Me.lblM)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTCount)
        Me.Controls.Add(Me.lblTcount)
        Me.Controls.Add(Me.lblDiif)
        Me.Controls.Add(Me.txtPLT)
        Me.Controls.Add(Me.txtWBC)
        Me.Controls.Add(Me.txtRBC)
        Me.Controls.Add(Me.lblPLT)
        Me.Controls.Add(Me.lblWBC)
        Me.Controls.Add(Me.lblRBC)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGPOPUPST_BM_NMC"
        Me.Text = "특수검사 모듈 (BM)"
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdDiff, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblRBC As System.Windows.Forms.Label
    Friend WithEvents lblWBC As System.Windows.Forms.Label
    Friend WithEvents lblPLT As System.Windows.Forms.Label
    Friend WithEvents txtRBC As System.Windows.Forms.TextBox
    Friend WithEvents txtWBC As System.Windows.Forms.TextBox
    Friend WithEvents txtPLT As System.Windows.Forms.TextBox
    Friend WithEvents lblDiif As System.Windows.Forms.Label
    Friend WithEvents lblTcount As System.Windows.Forms.Label
    Friend WithEvents txtTCount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblM As System.Windows.Forms.Label
    Friend WithEvents txtM As System.Windows.Forms.TextBox
    Friend WithEvents lblE As System.Windows.Forms.Label
    Friend WithEvents lblPb As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCBC_Hb As System.Windows.Forms.TextBox
    Friend WithEvents txtCBC_WBC As System.Windows.Forms.TextBox
    Friend WithEvents txtCBC_PLT As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdDiff As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtOther As System.Windows.Forms.TextBox
    Friend WithEvents txtMega As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_r As System.Windows.Forms.Button
    Friend WithEvents btnHelp_w As System.Windows.Forms.Button
    Friend WithEvents btnHelp_p As System.Windows.Forms.Button
    Friend WithEvents txtCell As System.Windows.Forms.TextBox
    Friend WithEvents lblCell As System.Windows.Forms.Label
    Friend WithEvents lblOther As System.Windows.Forms.Label
    Friend WithEvents btnHelp_oth As System.Windows.Forms.Button
    Friend WithEvents lblMega As System.Windows.Forms.Label
    Friend WithEvents btnHelp_mega As System.Windows.Forms.Button
    Friend WithEvents lblBM As System.Windows.Forms.Label
    Friend WithEvents txtBm As System.Windows.Forms.TextBox
    Friend WithEvents btnHelp_bm As System.Windows.Forms.Button
    Friend WithEvents lblHemato As System.Windows.Forms.Label
    Friend WithEvents btnHelp_hemato As System.Windows.Forms.Button
    Friend WithEvents txtHemato As System.Windows.Forms.TextBox
    Friend WithEvents lblK As System.Windows.Forms.Label
    Friend WithEvents lblSlide As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSlideno2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSlideno1 As System.Windows.Forms.TextBox
End Class
