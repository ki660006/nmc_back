<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR08_S04
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR08_S04))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.btnQuery_regno = New CButtonLib.CButton()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.btnQuery = New CButtonLib.CButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rboNoSend = New System.Windows.Forms.RadioButton()
        Me.rboAll = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlgbn = New System.Windows.Forms.Panel()
        Me.rboRstdt = New System.Windows.Forms.RadioButton()
        Me.rboTkdt = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlbottom = New System.Windows.Forms.Panel()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnUpload = New CButtonLib.CButton()
        Me.pnlCenter = New System.Windows.Forms.Panel()
        Me.chkall = New System.Windows.Forms.CheckBox()
        Me.rtbStRst = New AxAckRichTextBox.AxAckRichTextBox()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlgbn.SuspendLayout()
        Me.pnlbottom.SuspendLayout()
        Me.pnlCenter.SuspendLayout()
        Me.SuspendLayout()
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(740, 377)
        Me.spdList.TabIndex = 132
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(173, 11)
        Me.dtpDateS.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS.TabIndex = 127
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(281, 11)
        Me.dtpDateE.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 129
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'btnQuery_regno
        '
        Me.btnQuery_regno.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery_regno.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
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
        Me.btnQuery_regno.FocalPoints.CenterPtX = 0.4605263!
        Me.btnQuery_regno.FocalPoints.CenterPtY = 0.5!
        Me.btnQuery_regno.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery_regno.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_regno.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery_regno.Image = Nothing
        Me.btnQuery_regno.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_regno.ImageIndex = 0
        Me.btnQuery_regno.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery_regno.Location = New System.Drawing.Point(566, 0)
        Me.btnQuery_regno.Name = "btnQuery_regno"
        Me.btnQuery_regno.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery_regno.SideImage = Nothing
        Me.btnQuery_regno.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery_regno.Size = New System.Drawing.Size(76, 22)
        Me.btnQuery_regno.TabIndex = 162
        Me.btnQuery_regno.Text = "조회"
        Me.btnQuery_regno.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery_regno.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.btnQuery)
        Me.pnlTop.Controls.Add(Me.Panel1)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.pnlgbn)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.dtpDateS)
        Me.pnlTop.Controls.Add(Me.dtpDateE)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(743, 44)
        Me.pnlTop.TabIndex = 164
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems2
        Me.btnQuery.ColorFillSolid = System.Drawing.Color.White
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.4892086!
        Me.btnQuery.FocalPoints.CenterPtY = 0.1304348!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker4
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(592, 11)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(139, 23)
        Me.btnQuery.TabIndex = 195
        Me.btnQuery.Text = "조  회 (F2)"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rboNoSend)
        Me.Panel1.Controls.Add(Me.rboAll)
        Me.Panel1.Location = New System.Drawing.Point(452, 11)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(126, 24)
        Me.Panel1.TabIndex = 134
        '
        'rboNoSend
        '
        Me.rboNoSend.AutoSize = True
        Me.rboNoSend.BackColor = System.Drawing.Color.White
        Me.rboNoSend.Checked = True
        Me.rboNoSend.Location = New System.Drawing.Point(59, 4)
        Me.rboNoSend.Name = "rboNoSend"
        Me.rboNoSend.Size = New System.Drawing.Size(59, 16)
        Me.rboNoSend.TabIndex = 133
        Me.rboNoSend.TabStop = True
        Me.rboNoSend.Text = "미전송"
        Me.rboNoSend.UseVisualStyleBackColor = False
        '
        'rboAll
        '
        Me.rboAll.AutoSize = True
        Me.rboAll.BackColor = System.Drawing.Color.White
        Me.rboAll.Location = New System.Drawing.Point(6, 4)
        Me.rboAll.Name = "rboAll"
        Me.rboAll.Size = New System.Drawing.Size(47, 16)
        Me.rboAll.TabIndex = 131
        Me.rboAll.Text = "전체"
        Me.rboAll.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Location = New System.Drawing.Point(383, 11)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 22)
        Me.Label2.TabIndex = 133
        Me.Label2.Text = "전송구분"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlgbn
        '
        Me.pnlgbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.pnlgbn.Controls.Add(Me.rboRstdt)
        Me.pnlgbn.Controls.Add(Me.rboTkdt)
        Me.pnlgbn.Location = New System.Drawing.Point(12, 9)
        Me.pnlgbn.Name = "pnlgbn"
        Me.pnlgbn.Size = New System.Drawing.Size(157, 24)
        Me.pnlgbn.TabIndex = 132
        '
        'rboRstdt
        '
        Me.rboRstdt.AutoSize = True
        Me.rboRstdt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rboRstdt.Checked = True
        Me.rboRstdt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rboRstdt.ForeColor = System.Drawing.Color.White
        Me.rboRstdt.Location = New System.Drawing.Point(81, 4)
        Me.rboRstdt.Name = "rboRstdt"
        Me.rboRstdt.Size = New System.Drawing.Size(75, 16)
        Me.rboRstdt.TabIndex = 133
        Me.rboRstdt.TabStop = True
        Me.rboRstdt.Text = "보고일자"
        Me.rboRstdt.UseVisualStyleBackColor = False
        '
        'rboTkdt
        '
        Me.rboTkdt.AutoSize = True
        Me.rboTkdt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.rboTkdt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rboTkdt.ForeColor = System.Drawing.Color.White
        Me.rboTkdt.Location = New System.Drawing.Point(3, 4)
        Me.rboTkdt.Name = "rboTkdt"
        Me.rboTkdt.Size = New System.Drawing.Size(75, 16)
        Me.rboTkdt.TabIndex = 131
        Me.rboTkdt.Text = "접수일자"
        Me.rboTkdt.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(264, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 12)
        Me.Label1.TabIndex = 130
        Me.Label1.Text = "~"
        '
        'pnlbottom
        '
        Me.pnlbottom.Controls.Add(Me.btnClear)
        Me.pnlbottom.Controls.Add(Me.btnUpload)
        Me.pnlbottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlbottom.Location = New System.Drawing.Point(0, 433)
        Me.pnlbottom.Name = "pnlbottom"
        Me.pnlbottom.Size = New System.Drawing.Size(743, 28)
        Me.pnlbottom.TabIndex = 165
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.Color.White
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4859813!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(521, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(109, 23)
        Me.btnClear.TabIndex = 197
        Me.btnClear.Text = "화면정리 (F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnUpload
        '
        Me.btnUpload.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = True
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnUpload.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnUpload.ColorFillBlend = CBlendItems4
        Me.btnUpload.ColorFillSolid = System.Drawing.Color.White
        Me.btnUpload.Corners.All = CType(6, Short)
        Me.btnUpload.Corners.LowerLeft = CType(6, Short)
        Me.btnUpload.Corners.LowerRight = CType(6, Short)
        Me.btnUpload.Corners.UpperLeft = CType(6, Short)
        Me.btnUpload.Corners.UpperRight = CType(6, Short)
        Me.btnUpload.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnUpload.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnUpload.FocalPoints.CenterPtX = 0.4954129!
        Me.btnUpload.FocalPoints.CenterPtY = 0.3478261!
        Me.btnUpload.FocalPoints.FocusPtX = 0.0!
        Me.btnUpload.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnUpload.FocusPtTracker = DesignerRectTracker8
        Me.btnUpload.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnUpload.ForeColor = System.Drawing.Color.White
        Me.btnUpload.Image = Nothing
        Me.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnUpload.ImageIndex = 0
        Me.btnUpload.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnUpload.Location = New System.Drawing.Point(631, 3)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnUpload.SideImage = Nothing
        Me.btnUpload.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnUpload.Size = New System.Drawing.Size(109, 23)
        Me.btnUpload.TabIndex = 196
        Me.btnUpload.Text = "이미지올리기"
        Me.btnUpload.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnUpload.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlCenter
        '
        Me.pnlCenter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlCenter.Controls.Add(Me.chkall)
        Me.pnlCenter.Controls.Add(Me.rtbStRst)
        Me.pnlCenter.Controls.Add(Me.spdList)
        Me.pnlCenter.Location = New System.Drawing.Point(0, 50)
        Me.pnlCenter.Name = "pnlCenter"
        Me.pnlCenter.Size = New System.Drawing.Size(740, 377)
        Me.pnlCenter.TabIndex = 166
        '
        'chkall
        '
        Me.chkall.AutoSize = True
        Me.chkall.BackColor = System.Drawing.SystemColors.Control
        Me.chkall.Location = New System.Drawing.Point(36, 7)
        Me.chkall.Name = "chkall"
        Me.chkall.Size = New System.Drawing.Size(15, 14)
        Me.chkall.TabIndex = 134
        Me.chkall.UseVisualStyleBackColor = False
        '
        'rtbStRst
        '
        Me.rtbStRst.Location = New System.Drawing.Point(320, 181)
        Me.rtbStRst.Name = "rtbStRst"
        Me.rtbStRst.Size = New System.Drawing.Size(565, 78)
        Me.rtbStRst.TabIndex = 133
        Me.rtbStRst.Visible = False
        '
        'FGR08_S04
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(743, 461)
        Me.Controls.Add(Me.pnlCenter)
        Me.Controls.Add(Me.pnlbottom)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.btnQuery_regno)
        Me.Name = "FGR08_S04"
        Me.Text = "이미지 일괄전송"
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlgbn.ResumeLayout(False)
        Me.pnlgbn.PerformLayout()
        Me.pnlbottom.ResumeLayout(False)
        Me.pnlCenter.ResumeLayout(False)
        Me.pnlCenter.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnQuery_regno As CButtonLib.CButton
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlbottom As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rboTkdt As System.Windows.Forms.RadioButton
    Friend WithEvents pnlgbn As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rboRstdt As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rboNoSend As System.Windows.Forms.RadioButton
    Friend WithEvents rboAll As System.Windows.Forms.RadioButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnUpload As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents pnlCenter As System.Windows.Forms.Panel
    Friend WithEvents rtbStRst As AxAckRichTextBox.AxAckRichTextBox
    Friend WithEvents chkall As System.Windows.Forms.CheckBox
End Class
