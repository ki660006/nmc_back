<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS08
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
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS08))
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnQuery = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.grpTop = New System.Windows.Forms.GroupBox
        Me.pnlIOGbn = New System.Windows.Forms.Panel
        Me.rdoIoGbnA = New System.Windows.Forms.RadioButton
        Me.rdoIoGbnO = New System.Windows.Forms.RadioButton
        Me.rdoIoGbnI = New System.Windows.Forms.RadioButton
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.lblCencelDate = New System.Windows.Forms.Label
        Me.lblDat = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.pnlMid = New System.Windows.Forms.Panel
        Me.cmuList = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuRst_h = New System.Windows.Forms.ToolStripMenuItem
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.chkCollMove = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.txt = New System.Windows.Forms.TextBox
        Me.cboWard = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlBottom.SuspendLayout()
        Me.grpTop.SuspendLayout()
        Me.pnlIOGbn.SuspendLayout()
        Me.pnlMid.SuspendLayout()
        Me.cmuList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 642)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1031, 34)
        Me.pnlBottom.TabIndex = 6
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems3
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5!
        Me.btnQuery.FocalPoints.CenterPtY = 0.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker6
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(723, 6)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(100, 25)
        Me.btnQuery.TabIndex = 203
        Me.btnQuery.Text = "조  회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems4
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.5!
        Me.btnClear.FocalPoints.CenterPtY = 0.0!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker8
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(824, 6)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 201
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems1
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker2
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(925, 6)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 200
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpTop
        '
        Me.grpTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTop.Controls.Add(Me.Label3)
        Me.grpTop.Controls.Add(Me.Label2)
        Me.grpTop.Controls.Add(Me.cboWard)
        Me.grpTop.Controls.Add(Me.txt)
        Me.grpTop.Controls.Add(Me.txtRegNo)
        Me.grpTop.Controls.Add(Me.Label1)
        Me.grpTop.Controls.Add(Me.pnlIOGbn)
        Me.grpTop.Controls.Add(Me.dtpDateS)
        Me.grpTop.Controls.Add(Me.lblCencelDate)
        Me.grpTop.Controls.Add(Me.lblDat)
        Me.grpTop.Controls.Add(Me.dtpDateE)
        Me.grpTop.Location = New System.Drawing.Point(8, -3)
        Me.grpTop.Margin = New System.Windows.Forms.Padding(0)
        Me.grpTop.Name = "grpTop"
        Me.grpTop.Size = New System.Drawing.Size(909, 82)
        Me.grpTop.TabIndex = 9
        Me.grpTop.TabStop = False
        '
        'pnlIOGbn
        '
        Me.pnlIOGbn.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnA)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnO)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnI)
        Me.pnlIOGbn.ForeColor = System.Drawing.Color.Navy
        Me.pnlIOGbn.Location = New System.Drawing.Point(494, 20)
        Me.pnlIOGbn.Name = "pnlIOGbn"
        Me.pnlIOGbn.Size = New System.Drawing.Size(186, 21)
        Me.pnlIOGbn.TabIndex = 163
        Me.pnlIOGbn.TabStop = True
        '
        'rdoIoGbnA
        '
        Me.rdoIoGbnA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnA.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoIoGbnA.ForeColor = System.Drawing.Color.Black
        Me.rdoIoGbnA.Location = New System.Drawing.Point(9, 2)
        Me.rdoIoGbnA.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoIoGbnA.Name = "rdoIoGbnA"
        Me.rdoIoGbnA.Size = New System.Drawing.Size(48, 18)
        Me.rdoIoGbnA.TabIndex = 3
        Me.rdoIoGbnA.Tag = "0"
        Me.rdoIoGbnA.Text = "전체"
        '
        'rdoIoGbnO
        '
        Me.rdoIoGbnO.Checked = True
        Me.rdoIoGbnO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoIoGbnO.ForeColor = System.Drawing.Color.Black
        Me.rdoIoGbnO.Location = New System.Drawing.Point(65, 2)
        Me.rdoIoGbnO.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoIoGbnO.Name = "rdoIoGbnO"
        Me.rdoIoGbnO.Size = New System.Drawing.Size(48, 18)
        Me.rdoIoGbnO.TabIndex = 0
        Me.rdoIoGbnO.TabStop = True
        Me.rdoIoGbnO.Tag = "1"
        Me.rdoIoGbnO.Text = "외래"
        '
        'rdoIoGbnI
        '
        Me.rdoIoGbnI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnI.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoIoGbnI.ForeColor = System.Drawing.Color.Black
        Me.rdoIoGbnI.Location = New System.Drawing.Point(121, 2)
        Me.rdoIoGbnI.Margin = New System.Windows.Forms.Padding(0)
        Me.rdoIoGbnI.Name = "rdoIoGbnI"
        Me.rdoIoGbnI.Size = New System.Drawing.Size(48, 18)
        Me.rdoIoGbnI.TabIndex = 1
        Me.rdoIoGbnI.Tag = "2"
        Me.rdoIoGbnI.Text = "입원"
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(98, 46)
        Me.dtpDateS.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(84, 21)
        Me.dtpDateS.TabIndex = 7
        '
        'lblCencelDate
        '
        Me.lblCencelDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCencelDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCencelDate.ForeColor = System.Drawing.Color.White
        Me.lblCencelDate.Location = New System.Drawing.Point(5, 45)
        Me.lblCencelDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCencelDate.Name = "lblCencelDate"
        Me.lblCencelDate.Size = New System.Drawing.Size(92, 21)
        Me.lblCencelDate.TabIndex = 9
        Me.lblCencelDate.Text = "처방일자"
        Me.lblCencelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDat
        '
        Me.lblDat.AutoSize = True
        Me.lblDat.Location = New System.Drawing.Point(185, 51)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(11, 12)
        Me.lblDat.TabIndex = 10
        Me.lblDat.Text = "~"
        Me.lblDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(199, 46)
        Me.dtpDateE.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(84, 21)
        Me.dtpDateE.TabIndex = 8
        Me.dtpDateE.Value = New Date(2004, 9, 8, 19, 25, 0, 0)
        '
        'pnlMid
        '
        Me.pnlMid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMid.ContextMenuStrip = Me.cmuList
        Me.pnlMid.Controls.Add(Me.spdList)
        Me.pnlMid.Location = New System.Drawing.Point(8, 85)
        Me.pnlMid.Name = "pnlMid"
        Me.pnlMid.Size = New System.Drawing.Size(1015, 551)
        Me.pnlMid.TabIndex = 11
        '
        'cmuList
        '
        Me.cmuList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRst_h})
        Me.cmuList.Name = "cmuRstList"
        Me.cmuList.Size = New System.Drawing.Size(168, 26)
        Me.cmuList.Text = "상황에 맞는 메뉴"
        '
        'mnuRst_h
        '
        Me.mnuRst_h.Name = "mnuRst_h"
        Me.mnuRst_h.Size = New System.Drawing.Size(167, 22)
        Me.mnuRst_h.Text = "REJECT 결과 보기"
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(18, 15)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(979, 542)
        Me.spdList.TabIndex = 0
        '
        'chkCollMove
        '
        Me.chkCollMove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCollMove.AutoSize = True
        Me.chkCollMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkCollMove.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkCollMove.Location = New System.Drawing.Point(920, 64)
        Me.chkCollMove.Name = "chkCollMove"
        Me.chkCollMove.Size = New System.Drawing.Size(102, 15)
        Me.chkCollMove.TabIndex = 13
        Me.chkCollMove.Text = "컬럼이동모드"
        Me.chkCollMove.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(5, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 21)
        Me.Label1.TabIndex = 164
        Me.Label1.Text = "등록번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Location = New System.Drawing.Point(99, 20)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(97, 21)
        Me.txtRegNo.TabIndex = 165
        '
        'txt
        '
        Me.txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txt.Location = New System.Drawing.Point(199, 20)
        Me.txt.Margin = New System.Windows.Forms.Padding(0)
        Me.txt.MaxLength = 8
        Me.txt.Name = "txt"
        Me.txt.Size = New System.Drawing.Size(115, 21)
        Me.txt.TabIndex = 166
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.FormattingEnabled = True
        Me.cboWard.Items.AddRange(New Object() {"[전체]", "검사그룹", "작업그룹"})
        Me.cboWard.Location = New System.Drawing.Point(494, 46)
        Me.cboWard.Margin = New System.Windows.Forms.Padding(0)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(206, 20)
        Me.cboWard.TabIndex = 167
        Me.cboWard.Visible = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(398, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 21)
        Me.Label2.TabIndex = 168
        Me.Label2.Text = "입/외 구분"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(398, 45)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 21)
        Me.Label3.TabIndex = 169
        Me.Label3.Text = "진료과"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGS08
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1031, 676)
        Me.Controls.Add(Me.pnlMid)
        Me.Controls.Add(Me.chkCollMove)
        Me.Controls.Add(Me.grpTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS08"
        Me.Text = "미채혈 사유 대장"
        Me.pnlBottom.ResumeLayout(False)
        Me.grpTop.ResumeLayout(False)
        Me.grpTop.PerformLayout()
        Me.pnlIOGbn.ResumeLayout(False)
        Me.pnlMid.ResumeLayout(False)
        Me.cmuList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents grpTop As System.Windows.Forms.GroupBox
    Friend WithEvents pnlIOGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoIoGbnA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIoGbnI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIoGbnO As System.Windows.Forms.RadioButton
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCencelDate As System.Windows.Forms.Label
    Friend WithEvents lblDat As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlMid As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cmuList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRst_h As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkCollMove As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txt As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
