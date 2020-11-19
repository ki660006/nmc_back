<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR16
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
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR16))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.btnChg_regno = New System.Windows.Forms.Button
        Me.txtRegno = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtRegNo_chg = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.btnQuery_regno = New CButtonLib.CButton
        Me.chkSel = New System.Windows.Forms.CheckBox
        Me.txtIdNoR_chg = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtIdNoL_chg = New System.Windows.Forms.TextBox
        Me.txtPatnm_chg = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblIdNoR = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.lblIdnoL = New System.Windows.Forms.Label
        Me.lblPatnm = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.spdOrdList = New AxFPSpreadADO.AxfpSpread
        Me.Label32 = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.Label14 = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.btnQueryC = New CButtonLib.CButton
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtRegNo_qry = New System.Windows.Forms.TextBox
        Me.spdUpList = New AxFPSpreadADO.AxfpSpread
        Me.btnQuery = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpDateE_qry = New System.Windows.Forms.DateTimePicker
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtpDateS_qry = New System.Windows.Forms.DateTimePicker
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.spdOrdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.spdUpList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnChg_regno
        '
        Me.btnChg_regno.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnChg_regno.BackColor = System.Drawing.Color.Red
        Me.btnChg_regno.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnChg_regno.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.btnChg_regno.Location = New System.Drawing.Point(8, 547)
        Me.btnChg_regno.Name = "btnChg_regno"
        Me.btnChg_regno.Size = New System.Drawing.Size(243, 36)
        Me.btnChg_regno.TabIndex = 0
        Me.btnChg_regno.Text = "등록번호변경"
        Me.btnChg_regno.UseVisualStyleBackColor = False
        '
        'txtRegno
        '
        Me.txtRegno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegno.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtRegno.Location = New System.Drawing.Point(98, 38)
        Me.txtRegno.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegno.MaxLength = 8
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(155, 21)
        Me.txtRegno.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(7, 38)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 21)
        Me.Label1.TabIndex = 73
        Me.Label1.Text = "등록번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo_chg
        '
        Me.txtRegNo_chg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo_chg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo_chg.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtRegNo_chg.Location = New System.Drawing.Point(98, 175)
        Me.txtRegNo_chg.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegNo_chg.MaxLength = 8
        Me.txtRegNo_chg.Name = "txtRegNo_chg"
        Me.txtRegNo_chg.Size = New System.Drawing.Size(153, 21)
        Me.txtRegNo_chg.TabIndex = 74
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(6, 175)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(91, 21)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "변경등록번호"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(987, 616)
        Me.TabControl1.TabIndex = 76
        '
        'TabPage1
        '
        Me.TabPage1.AllowDrop = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.btnQuery_regno)
        Me.TabPage1.Controls.Add(Me.chkSel)
        Me.TabPage1.Controls.Add(Me.txtIdNoR_chg)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.txtIdNoL_chg)
        Me.TabPage1.Controls.Add(Me.txtPatnm_chg)
        Me.TabPage1.Controls.Add(Me.Label22)
        Me.TabPage1.Controls.Add(Me.Label18)
        Me.TabPage1.Controls.Add(Me.Label19)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.lblIdNoR)
        Me.TabPage1.Controls.Add(Me.Label21)
        Me.TabPage1.Controls.Add(Me.lblIdnoL)
        Me.TabPage1.Controls.Add(Me.lblPatnm)
        Me.TabPage1.Controls.Add(Me.Label13)
        Me.TabPage1.Controls.Add(Me.Label15)
        Me.TabPage1.Controls.Add(Me.spdOrdList)
        Me.TabPage1.Controls.Add(Me.Label32)
        Me.TabPage1.Controls.Add(Me.dtpDateE)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.dtpDateS)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.btnChg_regno)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.txtRegno)
        Me.TabPage1.Controls.Add(Me.txtRegNo_chg)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(979, 591)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "등록번호변경"
        '
        'btnQuery_regno
        '
        Me.btnQuery_regno.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery_regno.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = True
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_regno.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery_regno.ColorFillBlend = CBlendItems1
        Me.btnQuery_regno.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery_regno.Corners.All = CType(6, Short)
        Me.btnQuery_regno.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery_regno.Corners.LowerRight = CType(6, Short)
        Me.btnQuery_regno.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery_regno.Corners.UpperRight = CType(6, Short)
        Me.btnQuery_regno.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery_regno.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery_regno.FocalPoints.CenterPtX = 0.3970588!
        Me.btnQuery_regno.FocalPoints.CenterPtY = 0.2727273!
        Me.btnQuery_regno.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery_regno.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_regno.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery_regno.Image = Nothing
        Me.btnQuery_regno.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_regno.ImageIndex = 0
        Me.btnQuery_regno.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery_regno.Location = New System.Drawing.Point(537, 7)
        Me.btnQuery_regno.Name = "btnQuery_regno"
        Me.btnQuery_regno.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery_regno.SideImage = Nothing
        Me.btnQuery_regno.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery_regno.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery_regno.Size = New System.Drawing.Size(76, 22)
        Me.btnQuery_regno.TabIndex = 162
        Me.btnQuery_regno.Text = "조회"
        Me.btnQuery_regno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_regno.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery_regno.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkSel
        '
        Me.chkSel.AutoSize = True
        Me.chkSel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.chkSel.Location = New System.Drawing.Point(306, 36)
        Me.chkSel.Name = "chkSel"
        Me.chkSel.Size = New System.Drawing.Size(15, 14)
        Me.chkSel.TabIndex = 161
        Me.chkSel.UseVisualStyleBackColor = False
        '
        'txtIdNoR_chg
        '
        Me.txtIdNoR_chg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNoR_chg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdNoR_chg.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtIdNoR_chg.Location = New System.Drawing.Point(172, 219)
        Me.txtIdNoR_chg.MaxLength = 7
        Me.txtIdNoR_chg.Name = "txtIdNoR_chg"
        Me.txtIdNoR_chg.Size = New System.Drawing.Size(79, 21)
        Me.txtIdNoR_chg.TabIndex = 160
        Me.txtIdNoR_chg.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label5.Location = New System.Drawing.Point(160, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 159
        Me.Label5.Text = "-"
        Me.Label5.Visible = False
        '
        'txtIdNoL_chg
        '
        Me.txtIdNoL_chg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNoL_chg.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIdNoL_chg.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtIdNoL_chg.Location = New System.Drawing.Point(98, 219)
        Me.txtIdNoL_chg.MaxLength = 6
        Me.txtIdNoL_chg.Name = "txtIdNoL_chg"
        Me.txtIdNoL_chg.Size = New System.Drawing.Size(61, 21)
        Me.txtIdNoL_chg.TabIndex = 158
        Me.txtIdNoL_chg.Visible = False
        '
        'txtPatnm_chg
        '
        Me.txtPatnm_chg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatnm_chg.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtPatnm_chg.Location = New System.Drawing.Point(98, 197)
        Me.txtPatnm_chg.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPatnm_chg.MaxLength = 18
        Me.txtPatnm_chg.Name = "txtPatnm_chg"
        Me.txtPatnm_chg.Size = New System.Drawing.Size(153, 21)
        Me.txtPatnm_chg.TabIndex = 157
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(6, 144)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(245, 30)
        Me.Label22.TabIndex = 156
        Me.Label22.Text = "변경 데이타"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(6, 219)
        Me.Label18.Margin = New System.Windows.Forms.Padding(0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(91, 21)
        Me.Label18.TabIndex = 155
        Me.Label18.Text = "주민번호"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label18.Visible = False
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.Label19.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.White
        Me.Label19.Location = New System.Drawing.Point(6, 197)
        Me.Label19.Margin = New System.Windows.Forms.Padding(0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(91, 21)
        Me.Label19.TabIndex = 154
        Me.Label19.Text = "환자성명"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label4.Location = New System.Drawing.Point(160, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(11, 12)
        Me.Label4.TabIndex = 153
        Me.Label4.Text = "-"
        Me.Label4.Visible = False
        '
        'lblIdNoR
        '
        Me.lblIdNoR.BackColor = System.Drawing.Color.Gainsboro
        Me.lblIdNoR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdNoR.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNoR.ForeColor = System.Drawing.Color.Black
        Me.lblIdNoR.Location = New System.Drawing.Point(173, 83)
        Me.lblIdNoR.Name = "lblIdNoR"
        Me.lblIdNoR.Size = New System.Drawing.Size(80, 22)
        Me.lblIdNoR.TabIndex = 152
        Me.lblIdNoR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblIdNoR.Visible = False
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Black
        Me.Label21.Location = New System.Drawing.Point(7, 7)
        Me.Label21.Margin = New System.Windows.Forms.Padding(0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(246, 30)
        Me.Label21.TabIndex = 151
        Me.Label21.Text = "이전 데이타"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdnoL
        '
        Me.lblIdnoL.BackColor = System.Drawing.Color.Gainsboro
        Me.lblIdnoL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdnoL.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdnoL.ForeColor = System.Drawing.Color.Black
        Me.lblIdnoL.Location = New System.Drawing.Point(98, 83)
        Me.lblIdnoL.Name = "lblIdnoL"
        Me.lblIdnoL.Size = New System.Drawing.Size(61, 22)
        Me.lblIdnoL.TabIndex = 150
        Me.lblIdnoL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblIdnoL.Visible = False
        '
        'lblPatnm
        '
        Me.lblPatnm.BackColor = System.Drawing.Color.Gainsboro
        Me.lblPatnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPatnm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatnm.ForeColor = System.Drawing.Color.Black
        Me.lblPatnm.Location = New System.Drawing.Point(98, 60)
        Me.lblPatnm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPatnm.Name = "lblPatnm"
        Me.lblPatnm.Size = New System.Drawing.Size(155, 22)
        Me.lblPatnm.TabIndex = 149
        Me.lblPatnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(7, 83)
        Me.Label13.Margin = New System.Windows.Forms.Padding(0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 22)
        Me.Label13.TabIndex = 148
        Me.Label13.Text = "주민번호"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label13.Visible = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(7, 60)
        Me.Label15.Margin = New System.Windows.Forms.Padding(0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(90, 22)
        Me.Label15.TabIndex = 147
        Me.Label15.Text = "환자성명"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdOrdList
        '
        Me.spdOrdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdOrdList.DataSource = Nothing
        Me.spdOrdList.Location = New System.Drawing.Point(265, 31)
        Me.spdOrdList.Name = "spdOrdList"
        Me.spdOrdList.OcxState = CType(resources.GetObject("spdOrdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrdList.Size = New System.Drawing.Size(708, 553)
        Me.spdOrdList.TabIndex = 132
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(429, 11)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(14, 12)
        Me.Label32.TabIndex = 130
        Me.Label32.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(446, 7)
        Me.dtpDateE.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 129
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(265, 7)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 21)
        Me.Label14.TabIndex = 128
        Me.Label14.Text = "처방일시"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(338, 7)
        Me.dtpDateS.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS.TabIndex = 127
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'TabPage3
        '
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.btnQueryC)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.txtRegNo_qry)
        Me.TabPage3.Controls.Add(Me.spdUpList)
        Me.TabPage3.Controls.Add(Me.btnQuery)
        Me.TabPage3.Controls.Add(Me.Label3)
        Me.TabPage3.Controls.Add(Me.dtpDateE_qry)
        Me.TabPage3.Controls.Add(Me.Label6)
        Me.TabPage3.Controls.Add(Me.dtpDateS_qry)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(979, 591)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "변경리스트"
        '
        'btnQueryC
        '
        Me.btnQueryC.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQueryC.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQueryC.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems2.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQueryC.ColorFillBlend = CBlendItems2
        Me.btnQueryC.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQueryC.Corners.All = CType(6, Short)
        Me.btnQueryC.Corners.LowerLeft = CType(6, Short)
        Me.btnQueryC.Corners.LowerRight = CType(6, Short)
        Me.btnQueryC.Corners.UpperLeft = CType(6, Short)
        Me.btnQueryC.Corners.UpperRight = CType(6, Short)
        Me.btnQueryC.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQueryC.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQueryC.FocalPoints.CenterPtX = 0.3552631!
        Me.btnQueryC.FocalPoints.CenterPtY = 0.3181818!
        Me.btnQueryC.FocalPoints.FocusPtX = 0.0!
        Me.btnQueryC.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQueryC.FocusPtTracker = DesignerRectTracker4
        Me.btnQueryC.Image = Nothing
        Me.btnQueryC.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQueryC.ImageIndex = 0
        Me.btnQueryC.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQueryC.Location = New System.Drawing.Point(464, 10)
        Me.btnQueryC.Name = "btnQueryC"
        Me.btnQueryC.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQueryC.SideImage = Nothing
        Me.btnQueryC.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQueryC.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQueryC.Size = New System.Drawing.Size(76, 22)
        Me.btnQueryC.TabIndex = 163
        Me.btnQueryC.Text = "조회"
        Me.btnQueryC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQueryC.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQueryC.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(12, 10)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 21)
        Me.Label7.TabIndex = 144
        Me.Label7.Text = "등록번호"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo_qry
        '
        Me.txtRegNo_qry.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo_qry.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo_qry.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtRegNo_qry.Location = New System.Drawing.Point(85, 10)
        Me.txtRegNo_qry.MaxLength = 8
        Me.txtRegNo_qry.Name = "txtRegNo_qry"
        Me.txtRegNo_qry.Size = New System.Drawing.Size(89, 21)
        Me.txtRegNo_qry.TabIndex = 143
        '
        'spdUpList
        '
        Me.spdUpList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.spdUpList.DataSource = Nothing
        Me.spdUpList.Location = New System.Drawing.Point(12, 34)
        Me.spdUpList.Name = "spdUpList"
        Me.spdUpList.OcxState = CType(resources.GetObject("spdUpList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdUpList.Size = New System.Drawing.Size(956, 546)
        Me.spdUpList.TabIndex = 142
        '
        'btnQuery
        '
        Me.btnQuery.Location = New System.Drawing.Point(458, 9)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(91, 23)
        Me.btnQuery.TabIndex = 141
        Me.btnQuery.Text = "조회"
        Me.btnQuery.UseVisualStyleBackColor = True
        Me.btnQuery.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(344, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(14, 12)
        Me.Label3.TabIndex = 140
        Me.Label3.Text = "~"
        '
        'dtpDateE_qry
        '
        Me.dtpDateE_qry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE_qry.Location = New System.Drawing.Point(364, 10)
        Me.dtpDateE_qry.Name = "dtpDateE_qry"
        Me.dtpDateE_qry.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE_qry.TabIndex = 139
        Me.dtpDateE_qry.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(177, 10)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 21)
        Me.Label6.TabIndex = 138
        Me.Label6.Text = "변경일자"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS_qry
        '
        Me.dtpDateS_qry.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS_qry.Location = New System.Drawing.Point(250, 10)
        Me.dtpDateS_qry.Name = "dtpDateS_qry"
        Me.dtpDateS_qry.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS_qry.TabIndex = 137
        Me.dtpDateS_qry.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'FGR16
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1011, 640)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "FGR16"
        Me.Text = "등록번호 변경"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        CType(Me.spdOrdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.spdUpList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnChg_regno As System.Windows.Forms.Button
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRegNo_chg As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents spdOrdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents spdUpList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpDateE_qry As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpDateS_qry As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtRegNo_qry As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblIdNoR As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents lblIdnoL As System.Windows.Forms.Label
    Friend WithEvents lblPatnm As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtIdNoR_chg As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtIdNoL_chg As System.Windows.Forms.TextBox
    Friend WithEvents txtPatnm_chg As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents chkSel As System.Windows.Forms.CheckBox
    Friend WithEvents btnQuery_regno As CButtonLib.CButton
    Friend WithEvents btnQueryC As CButtonLib.CButton
End Class
