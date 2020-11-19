<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxCollList_pis
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxCollList_pis))
        Me.pnl = New System.Windows.Forms.Panel
        Me.chkSel = New System.Windows.Forms.CheckBox
        Me.Panel5 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblOrdFlgB = New System.Windows.Forms.Label
        Me.lblBcColor3 = New System.Windows.Forms.Label
        Me.lblBcColor2 = New System.Windows.Forms.Label
        Me.lblBcColor1 = New System.Windows.Forms.Label
        Me.lblBcColor0 = New System.Windows.Forms.Label
        Me.Label54 = New System.Windows.Forms.Label
        Me.lblRstFlgF = New System.Windows.Forms.Label
        Me.Label52 = New System.Windows.Forms.Label
        Me.lblRstFlgM = New System.Windows.Forms.Label
        Me.Label51 = New System.Windows.Forms.Label
        Me.lblRstFlgR = New System.Windows.Forms.Label
        Me.Label49 = New System.Windows.Forms.Label
        Me.Label47 = New System.Windows.Forms.Label
        Me.lblOrdFlgT = New System.Windows.Forms.Label
        Me.Label45 = New System.Windows.Forms.Label
        Me.lblOrdFlgC = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.lblNoColl = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblBcclsNm3 = New System.Windows.Forms.Label
        Me.lblBcclsNm2 = New System.Windows.Forms.Label
        Me.lblBcclsNm1 = New System.Windows.Forms.Label
        Me.lblBcclsNm0 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblErFlgE = New System.Windows.Forms.Label
        Me.lblErFlgB = New System.Windows.Forms.Label
        Me.spdOrdList = New AxFPSpreadADO.AxfpSpread
        Me.lstMsg = New System.Windows.Forms.ListBox
        Me.lblMsg = New System.Windows.Forms.Label
        Me.tooltip = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnl.SuspendLayout()
        Me.Panel5.SuspendLayout()
        CType(Me.spdOrdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnl
        '
        Me.pnl.Controls.Add(Me.chkSel)
        Me.pnl.Controls.Add(Me.Panel5)
        Me.pnl.Controls.Add(Me.spdOrdList)
        Me.pnl.Controls.Add(Me.lstMsg)
        Me.pnl.Controls.Add(Me.lblMsg)
        Me.pnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnl.Location = New System.Drawing.Point(0, 0)
        Me.pnl.Name = "pnl"
        Me.pnl.Size = New System.Drawing.Size(1000, 600)
        Me.pnl.TabIndex = 0
        '
        'chkSel
        '
        Me.chkSel.AutoSize = True
        Me.chkSel.Location = New System.Drawing.Point(69, 26)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 57
        Me.chkSel.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Controls.Add(Me.lblOrdFlgB)
        Me.Panel5.Controls.Add(Me.lblBcColor3)
        Me.Panel5.Controls.Add(Me.lblBcColor2)
        Me.Panel5.Controls.Add(Me.lblBcColor1)
        Me.Panel5.Controls.Add(Me.lblBcColor0)
        Me.Panel5.Controls.Add(Me.Label54)
        Me.Panel5.Controls.Add(Me.lblRstFlgF)
        Me.Panel5.Controls.Add(Me.Label52)
        Me.Panel5.Controls.Add(Me.lblRstFlgM)
        Me.Panel5.Controls.Add(Me.Label51)
        Me.Panel5.Controls.Add(Me.lblRstFlgR)
        Me.Panel5.Controls.Add(Me.Label49)
        Me.Panel5.Controls.Add(Me.Label47)
        Me.Panel5.Controls.Add(Me.lblOrdFlgT)
        Me.Panel5.Controls.Add(Me.Label45)
        Me.Panel5.Controls.Add(Me.lblOrdFlgC)
        Me.Panel5.Controls.Add(Me.Label32)
        Me.Panel5.Controls.Add(Me.lblNoColl)
        Me.Panel5.Controls.Add(Me.Label26)
        Me.Panel5.Controls.Add(Me.lblBcclsNm3)
        Me.Panel5.Controls.Add(Me.lblBcclsNm2)
        Me.Panel5.Controls.Add(Me.lblBcclsNm1)
        Me.Panel5.Controls.Add(Me.lblBcclsNm0)
        Me.Panel5.Controls.Add(Me.Label34)
        Me.Panel5.Controls.Add(Me.lblErFlgE)
        Me.Panel5.Controls.Add(Me.lblErFlgB)
        Me.Panel5.Location = New System.Drawing.Point(0, 573)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1000, 27)
        Me.Panel5.TabIndex = 56
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(151, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 22)
        Me.Label1.TabIndex = 209
        Me.Label1.Text = "바코드발행"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgB
        '
        Me.lblOrdFlgB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgB.BackColor = System.Drawing.Color.Moccasin
        Me.lblOrdFlgB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgB.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgB.Location = New System.Drawing.Point(132, 5)
        Me.lblOrdFlgB.Name = "lblOrdFlgB"
        Me.lblOrdFlgB.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgB.TabIndex = 208
        Me.lblOrdFlgB.Text = "바"
        Me.lblOrdFlgB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor3
        '
        Me.lblBcColor3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor3.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblBcColor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor3.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor3.Location = New System.Drawing.Point(944, 5)
        Me.lblBcColor3.Name = "lblBcColor3"
        Me.lblBcColor3.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor3.TabIndex = 205
        Me.lblBcColor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor2
        '
        Me.lblBcColor2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblBcColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor2.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor2.Location = New System.Drawing.Point(862, 5)
        Me.lblBcColor2.Name = "lblBcColor2"
        Me.lblBcColor2.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor2.TabIndex = 204
        Me.lblBcColor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor1
        '
        Me.lblBcColor1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblBcColor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor1.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor1.Location = New System.Drawing.Point(779, 5)
        Me.lblBcColor1.Name = "lblBcColor1"
        Me.lblBcColor1.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor1.TabIndex = 203
        Me.lblBcColor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor0
        '
        Me.lblBcColor0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcColor0.BackColor = System.Drawing.Color.White
        Me.lblBcColor0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor0.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor0.Location = New System.Drawing.Point(696, 4)
        Me.lblBcColor0.Name = "lblBcColor0"
        Me.lblBcColor0.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor0.TabIndex = 202
        Me.lblBcColor0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label54
        '
        Me.Label54.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label54.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label54.ForeColor = System.Drawing.Color.Black
        Me.Label54.Location = New System.Drawing.Point(563, 1)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(56, 22)
        Me.Label54.TabIndex = 201
        Me.Label54.Text = "검사완료"
        Me.Label54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRstFlgF
        '
        Me.lblRstFlgF.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRstFlgF.BackColor = System.Drawing.Color.White
        Me.lblRstFlgF.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRstFlgF.ForeColor = System.Drawing.Color.Black
        Me.lblRstFlgF.Image = CType(resources.GetObject("lblRstFlgF.Image"), System.Drawing.Image)
        Me.lblRstFlgF.Location = New System.Drawing.Point(543, 5)
        Me.lblRstFlgF.Name = "lblRstFlgF"
        Me.lblRstFlgF.Size = New System.Drawing.Size(18, 16)
        Me.lblRstFlgF.TabIndex = 200
        Me.lblRstFlgF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label52
        '
        Me.Label52.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label52.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label52.ForeColor = System.Drawing.Color.Black
        Me.Label52.Location = New System.Drawing.Point(485, 1)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(54, 22)
        Me.Label52.TabIndex = 199
        Me.Label52.Text = "예비보고"
        Me.Label52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRstFlgM
        '
        Me.lblRstFlgM.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRstFlgM.BackColor = System.Drawing.Color.White
        Me.lblRstFlgM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRstFlgM.ForeColor = System.Drawing.Color.Black
        Me.lblRstFlgM.Image = CType(resources.GetObject("lblRstFlgM.Image"), System.Drawing.Image)
        Me.lblRstFlgM.Location = New System.Drawing.Point(465, 5)
        Me.lblRstFlgM.Name = "lblRstFlgM"
        Me.lblRstFlgM.Size = New System.Drawing.Size(18, 16)
        Me.lblRstFlgM.TabIndex = 198
        Me.lblRstFlgM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label51
        '
        Me.Label51.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label51.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label51.ForeColor = System.Drawing.Color.Black
        Me.Label51.Location = New System.Drawing.Point(416, 2)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(44, 22)
        Me.Label51.TabIndex = 197
        Me.Label51.Text = "검사중"
        Me.Label51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRstFlgR
        '
        Me.lblRstFlgR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRstFlgR.BackColor = System.Drawing.Color.White
        Me.lblRstFlgR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRstFlgR.ForeColor = System.Drawing.Color.Black
        Me.lblRstFlgR.Image = CType(resources.GetObject("lblRstFlgR.Image"), System.Drawing.Image)
        Me.lblRstFlgR.Location = New System.Drawing.Point(396, 5)
        Me.lblRstFlgR.Name = "lblRstFlgR"
        Me.lblRstFlgR.Size = New System.Drawing.Size(18, 16)
        Me.lblRstFlgR.TabIndex = 196
        Me.lblRstFlgR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label49
        '
        Me.Label49.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label49.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label49.ForeColor = System.Drawing.Color.Black
        Me.Label49.Location = New System.Drawing.Point(322, -1)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(66, 27)
        Me.Label49.TabIndex = 195
        Me.Label49.Text = "결과범례"
        Me.Label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label47
        '
        Me.Label47.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label47.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label47.ForeColor = System.Drawing.Color.Black
        Me.Label47.Location = New System.Drawing.Point(289, 2)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(31, 22)
        Me.Label47.TabIndex = 194
        Me.Label47.Text = "접수"
        Me.Label47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgT
        '
        Me.lblOrdFlgT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgT.BackColor = System.Drawing.Color.SkyBlue
        Me.lblOrdFlgT.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgT.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgT.Location = New System.Drawing.Point(271, 5)
        Me.lblOrdFlgT.Name = "lblOrdFlgT"
        Me.lblOrdFlgT.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgT.TabIndex = 193
        Me.lblOrdFlgT.Text = "접"
        Me.lblOrdFlgT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label45.ForeColor = System.Drawing.Color.Black
        Me.Label45.Location = New System.Drawing.Point(239, 2)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(31, 22)
        Me.Label45.TabIndex = 192
        Me.Label45.Text = "채혈"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblOrdFlgC
        '
        Me.lblOrdFlgC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblOrdFlgC.BackColor = System.Drawing.Color.Goldenrod
        Me.lblOrdFlgC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdFlgC.ForeColor = System.Drawing.Color.Black
        Me.lblOrdFlgC.Location = New System.Drawing.Point(219, 5)
        Me.lblOrdFlgC.Name = "lblOrdFlgC"
        Me.lblOrdFlgC.Size = New System.Drawing.Size(18, 16)
        Me.lblOrdFlgC.TabIndex = 191
        Me.lblOrdFlgC.Text = "채"
        Me.lblOrdFlgC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label32.ForeColor = System.Drawing.Color.Black
        Me.Label32.Location = New System.Drawing.Point(90, 2)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(42, 22)
        Me.Label32.TabIndex = 190
        Me.Label32.Text = "미채혈"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNoColl
        '
        Me.lblNoColl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNoColl.BackColor = System.Drawing.Color.White
        Me.lblNoColl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNoColl.ForeColor = System.Drawing.Color.Black
        Me.lblNoColl.Location = New System.Drawing.Point(71, 5)
        Me.lblNoColl.Name = "lblNoColl"
        Me.lblNoColl.Size = New System.Drawing.Size(18, 16)
        Me.lblNoColl.TabIndex = 189
        Me.lblNoColl.Text = "미"
        Me.lblNoColl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(-1, -1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(66, 27)
        Me.Label26.TabIndex = 188
        Me.Label26.Text = "처방범례"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm3
        '
        Me.lblBcclsNm3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcclsNm3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm3.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm3.Location = New System.Drawing.Point(962, 3)
        Me.lblBcclsNm3.Name = "lblBcclsNm3"
        Me.lblBcclsNm3.Size = New System.Drawing.Size(32, 21)
        Me.lblBcclsNm3.TabIndex = 187
        Me.lblBcclsNm3.Text = "기타"
        Me.lblBcclsNm3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm2
        '
        Me.lblBcclsNm2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcclsNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm2.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm2.Location = New System.Drawing.Point(881, 3)
        Me.lblBcclsNm2.Name = "lblBcclsNm2"
        Me.lblBcclsNm2.Size = New System.Drawing.Size(56, 21)
        Me.lblBcclsNm2.TabIndex = 186
        Me.lblBcclsNm2.Text = "외부의뢰"
        Me.lblBcclsNm2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm1
        '
        Me.lblBcclsNm1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcclsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm1.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm1.Location = New System.Drawing.Point(800, 3)
        Me.lblBcclsNm1.Name = "lblBcclsNm1"
        Me.lblBcclsNm1.Size = New System.Drawing.Size(56, 21)
        Me.lblBcclsNm1.TabIndex = 185
        Me.lblBcclsNm1.Text = "혈액은행"
        Me.lblBcclsNm1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm0
        '
        Me.lblBcclsNm0.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblBcclsNm0.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm0.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm0.Location = New System.Drawing.Point(717, 3)
        Me.lblBcclsNm0.Name = "lblBcclsNm0"
        Me.lblBcclsNm0.Size = New System.Drawing.Size(56, 21)
        Me.lblBcclsNm0.TabIndex = 184
        Me.lblBcclsNm0.Text = "진단검사"
        Me.lblBcclsNm0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(624, -1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(66, 27)
        Me.Label34.TabIndex = 179
        Me.Label34.Text = "검사범례"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblErFlgE
        '
        Me.lblErFlgE.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblErFlgE.BackColor = System.Drawing.Color.White
        Me.lblErFlgE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErFlgE.Font = New System.Drawing.Font("돋움체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblErFlgE.ForeColor = System.Drawing.Color.Crimson
        Me.lblErFlgE.Location = New System.Drawing.Point(469, 28)
        Me.lblErFlgE.Name = "lblErFlgE"
        Me.lblErFlgE.Size = New System.Drawing.Size(56, 19)
        Me.lblErFlgE.TabIndex = 207
        Me.lblErFlgE.Text = "E 응급"
        Me.lblErFlgE.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lblErFlgE.UseCompatibleTextRendering = True
        Me.lblErFlgE.Visible = False
        '
        'lblErFlgB
        '
        Me.lblErFlgB.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblErFlgB.BackColor = System.Drawing.Color.White
        Me.lblErFlgB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblErFlgB.Font = New System.Drawing.Font("돋움체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblErFlgB.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblErFlgB.Location = New System.Drawing.Point(469, 46)
        Me.lblErFlgB.Name = "lblErFlgB"
        Me.lblErFlgB.Size = New System.Drawing.Size(56, 19)
        Me.lblErFlgB.TabIndex = 206
        Me.lblErFlgB.Text = "B 진료전"
        Me.lblErFlgB.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.lblErFlgB.UseCompatibleTextRendering = True
        Me.lblErFlgB.Visible = False
        '
        'spdOrdList
        '
        Me.spdOrdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdOrdList.DataSource = Nothing
        Me.spdOrdList.Location = New System.Drawing.Point(0, 0)
        Me.spdOrdList.Name = "spdOrdList"
        Me.spdOrdList.OcxState = CType(resources.GetObject("spdOrdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdList.Size = New System.Drawing.Size(1000, 548)
        Me.spdOrdList.TabIndex = 24
        '
        'lstMsg
        '
        Me.lstMsg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstMsg.FormattingEnabled = True
        Me.lstMsg.ItemHeight = 12
        Me.lstMsg.Location = New System.Drawing.Point(0, 548)
        Me.lstMsg.Name = "lstMsg"
        Me.lstMsg.ScrollAlwaysVisible = True
        Me.lstMsg.Size = New System.Drawing.Size(1000, 26)
        Me.lstMsg.TabIndex = 22
        '
        'lblMsg
        '
        Me.lblMsg.BackColor = System.Drawing.Color.Thistle
        Me.lblMsg.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(117, 46)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(549, 40)
        Me.lblMsg.TabIndex = 18
        Me.lblMsg.Text = "채혈할 검사항목이 없습니다!!"
        Me.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblMsg.Visible = False
        '
        'AxCollList_pis
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.Controls.Add(Me.pnl)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "AxCollList_pis"
        Me.Size = New System.Drawing.Size(1000, 600)
        Me.pnl.ResumeLayout(False)
        Me.pnl.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        CType(Me.spdOrdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnl As System.Windows.Forms.Panel
    Friend WithEvents lblMsg As System.Windows.Forms.Label
    Friend WithEvents lstMsg As System.Windows.Forms.ListBox
    Friend WithEvents tooltip As System.Windows.Forms.ToolTip
    Public WithEvents spdOrdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Public WithEvents lblBcColor3 As System.Windows.Forms.Label
    Public WithEvents lblBcColor2 As System.Windows.Forms.Label
    Public WithEvents lblBcColor1 As System.Windows.Forms.Label
    Public WithEvents lblBcColor0 As System.Windows.Forms.Label
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents lblRstFlgF As System.Windows.Forms.Label
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents lblRstFlgM As System.Windows.Forms.Label
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents lblRstFlgR As System.Windows.Forms.Label
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Public WithEvents lblOrdFlgT As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Public WithEvents lblOrdFlgC As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Public WithEvents lblNoColl As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Public WithEvents lblBcclsNm3 As System.Windows.Forms.Label
    Public WithEvents lblBcclsNm2 As System.Windows.Forms.Label
    Public WithEvents lblBcclsNm1 As System.Windows.Forms.Label
    Public WithEvents lblBcclsNm0 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents lblErFlgE As System.Windows.Forms.Label
    Friend WithEvents lblErFlgB As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lblOrdFlgB As System.Windows.Forms.Label
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox

End Class
