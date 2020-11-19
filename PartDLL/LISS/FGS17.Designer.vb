<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS17
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
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS17))
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
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
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnPrint = New CButtonLib.CButton()
        Me.btnQuery = New CButtonLib.CButton()
        Me.cboPrint = New System.Windows.Forms.ComboBox()
        Me.btnClear = New CButtonLib.CButton()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.btnExcel = New CButtonLib.CButton()
        Me.cboOp = New System.Windows.Forms.ComboBox()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.lblFilTer = New System.Windows.Forms.Label()
        Me.btnFilterN = New System.Windows.Forms.Button()
        Me.btnFilterY = New System.Windows.Forms.Button()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.grpTop = New System.Windows.Forms.GroupBox()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.cboPartSlip = New System.Windows.Forms.ComboBox()
        Me.lblSlip = New System.Windows.Forms.Label()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.lblRstDT = New System.Windows.Forms.Label()
        Me.lblDat = New System.Windows.Forms.Label()
        Me.pnlMid = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlMid2 = New System.Windows.Forms.Panel()
        Me.spdStList = New AxFPSpreadADO.AxfpSpread()
        Me.chkCollMove = New System.Windows.Forms.CheckBox()
        Me.SplMid = New System.Windows.Forms.SplitContainer()
        Me.pnlBottom.SuspendLayout()
        Me.grpTop.SuspendLayout()
        Me.pnlMid.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlMid2.SuspendLayout()
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplMid.Panel1.SuspendLayout()
        Me.SplMid.Panel2.SuspendLayout()
        Me.SplMid.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.cboPrint)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.txtFilter)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.cboOp)
        Me.pnlBottom.Controls.Add(Me.cboFilter)
        Me.pnlBottom.Controls.Add(Me.lblFilTer)
        Me.pnlBottom.Controls.Add(Me.btnFilterN)
        Me.pnlBottom.Controls.Add(Me.btnFilterY)
        Me.pnlBottom.Controls.Add(Me.lblUserNm)
        Me.pnlBottom.Controls.Add(Me.lblUserId)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 642)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1149, 34)
        Me.pnlBottom.TabIndex = 6
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems6
        Me.btnExit.ColorFillSolid = System.Drawing.Color.White
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
        DesignerRectTracker12.IsActive = True
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker12
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1045, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 205
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems1
        Me.btnPrint.ColorFillSolid = System.Drawing.Color.White
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.4859813!
        Me.btnPrint.FocalPoints.CenterPtY = 0.16!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker2
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(742, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(100, 25)
        Me.btnPrint.TabIndex = 204
        Me.btnPrint.Text = "출  력"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnQuery.FocalPoints.CenterPtX = 0.4859813!
        Me.btnQuery.FocalPoints.CenterPtY = 0.16!
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
        Me.btnQuery.Location = New System.Drawing.Point(592, 4)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(100, 25)
        Me.btnQuery.TabIndex = 201
        Me.btnQuery.Text = "조  회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboPrint
        '
        Me.cboPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.Items.AddRange(New Object() {"위", "아래"})
        Me.cboPrint.Location = New System.Drawing.Point(693, 7)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(48, 20)
        Me.cboPrint.TabIndex = 83
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnClear.FocalPoints.CenterPtX = 0.51!
        Me.btnClear.FocalPoints.CenterPtY = 0.4!
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
        Me.btnClear.Location = New System.Drawing.Point(944, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 203
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(255, 7)
        Me.txtFilter.MaxLength = 50
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(101, 21)
        Me.txtFilter.TabIndex = 81
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems4
        Me.btnExcel.ColorFillSolid = System.Drawing.Color.White
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.03738318!
        Me.btnExcel.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker8
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(843, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 202
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboOp
        '
        Me.cboOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOp.Items.AddRange(New Object() {"=", "Like", "<>"})
        Me.cboOp.Location = New System.Drawing.Point(190, 7)
        Me.cboOp.MaxDropDownItems = 10
        Me.cboOp.Name = "cboOp"
        Me.cboOp.Size = New System.Drawing.Size(64, 20)
        Me.cboOp.TabIndex = 80
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Location = New System.Drawing.Point(81, 7)
        Me.cboFilter.MaxDropDownItems = 10
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(108, 20)
        Me.cboFilter.TabIndex = 79
        '
        'lblFilTer
        '
        Me.lblFilTer.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblFilTer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilTer.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblFilTer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilTer.Location = New System.Drawing.Point(8, 6)
        Me.lblFilTer.Name = "lblFilTer"
        Me.lblFilTer.Size = New System.Drawing.Size(72, 22)
        Me.lblFilTer.TabIndex = 78
        Me.lblFilTer.Text = "필터옵션"
        Me.lblFilTer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFilterN
        '
        Me.btnFilterN.Location = New System.Drawing.Point(440, 6)
        Me.btnFilterN.Name = "btnFilterN"
        Me.btnFilterN.Size = New System.Drawing.Size(84, 24)
        Me.btnFilterN.TabIndex = 7
        Me.btnFilterN.Text = "필터해제"
        '
        'btnFilterY
        '
        Me.btnFilterY.Location = New System.Drawing.Point(357, 6)
        Me.btnFilterY.Name = "btnFilterY"
        Me.btnFilterY.Size = New System.Drawing.Size(84, 24)
        Me.btnFilterY.TabIndex = 6
        Me.btnFilterY.Text = "필터적용"
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(84, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 4
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(12, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 3
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'grpTop
        '
        Me.grpTop.Controls.Add(Me.dtpDate1)
        Me.grpTop.Controls.Add(Me.cboPartSlip)
        Me.grpTop.Controls.Add(Me.lblSlip)
        Me.grpTop.Controls.Add(Me.dtpDate0)
        Me.grpTop.Controls.Add(Me.lblRstDT)
        Me.grpTop.Controls.Add(Me.lblDat)
        Me.grpTop.Location = New System.Drawing.Point(3, 4)
        Me.grpTop.Margin = New System.Windows.Forms.Padding(0)
        Me.grpTop.Name = "grpTop"
        Me.grpTop.Size = New System.Drawing.Size(292, 60)
        Me.grpTop.TabIndex = 9
        Me.grpTop.TabStop = False
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(198, 11)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(84, 21)
        Me.dtpDate1.TabIndex = 168
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.FormattingEnabled = True
        Me.cboPartSlip.Location = New System.Drawing.Point(96, 34)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(186, 20)
        Me.cboPartSlip.TabIndex = 167
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.White
        Me.lblSlip.Location = New System.Drawing.Point(7, 33)
        Me.lblSlip.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(88, 21)
        Me.lblSlip.TabIndex = 166
        Me.lblSlip.Text = "검사분야"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(96, 11)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(0)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(84, 21)
        Me.dtpDate0.TabIndex = 7
        '
        'lblRstDT
        '
        Me.lblRstDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRstDT.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRstDT.ForeColor = System.Drawing.Color.White
        Me.lblRstDT.Location = New System.Drawing.Point(7, 11)
        Me.lblRstDT.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRstDT.Name = "lblRstDT"
        Me.lblRstDT.Size = New System.Drawing.Size(88, 21)
        Me.lblRstDT.TabIndex = 9
        Me.lblRstDT.Text = "처리일자"
        Me.lblRstDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDat
        '
        Me.lblDat.AutoSize = True
        Me.lblDat.Location = New System.Drawing.Point(183, 15)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(11, 12)
        Me.lblDat.TabIndex = 10
        Me.lblDat.Text = "~"
        Me.lblDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMid
        '
        Me.pnlMid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMid.Controls.Add(Me.spdList)
        Me.pnlMid.Location = New System.Drawing.Point(3, 3)
        Me.pnlMid.Name = "pnlMid"
        Me.pnlMid.Size = New System.Drawing.Size(1143, 334)
        Me.pnlMid.TabIndex = 11
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1142, 333)
        Me.spdList.TabIndex = 0
        '
        'pnlMid2
        '
        Me.pnlMid2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMid2.Controls.Add(Me.spdStList)
        Me.pnlMid2.Location = New System.Drawing.Point(3, 3)
        Me.pnlMid2.Name = "pnlMid2"
        Me.pnlMid2.Size = New System.Drawing.Size(1143, 225)
        Me.pnlMid2.TabIndex = 12
        '
        'spdStList
        '
        Me.spdStList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdStList.DataSource = Nothing
        Me.spdStList.Location = New System.Drawing.Point(0, 0)
        Me.spdStList.Name = "spdStList"
        Me.spdStList.OcxState = CType(resources.GetObject("spdStList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStList.Size = New System.Drawing.Size(1143, 225)
        Me.spdStList.TabIndex = 0
        '
        'chkCollMove
        '
        Me.chkCollMove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkCollMove.AutoSize = True
        Me.chkCollMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkCollMove.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkCollMove.Location = New System.Drawing.Point(1049, 47)
        Me.chkCollMove.Name = "chkCollMove"
        Me.chkCollMove.Size = New System.Drawing.Size(96, 15)
        Me.chkCollMove.TabIndex = 13
        Me.chkCollMove.Text = "컬럼이동모드"
        Me.chkCollMove.UseVisualStyleBackColor = False
        '
        'SplMid
        '
        Me.SplMid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplMid.Location = New System.Drawing.Point(0, 68)
        Me.SplMid.Name = "SplMid"
        Me.SplMid.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplMid.Panel1
        '
        Me.SplMid.Panel1.Controls.Add(Me.pnlMid)
        '
        'SplMid.Panel2
        '
        Me.SplMid.Panel2.Controls.Add(Me.pnlMid2)
        Me.SplMid.Size = New System.Drawing.Size(1149, 575)
        Me.SplMid.SplitterDistance = 340
        Me.SplMid.TabIndex = 14
        '
        'FGS17
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1149, 676)
        Me.Controls.Add(Me.chkCollMove)
        Me.Controls.Add(Me.SplMid)
        Me.Controls.Add(Me.grpTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS17"
        Me.Text = "재검 내역 조회"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.grpTop.ResumeLayout(False)
        Me.grpTop.PerformLayout()
        Me.pnlMid.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlMid2.ResumeLayout(False)
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplMid.Panel1.ResumeLayout(False)
        Me.SplMid.Panel2.ResumeLayout(False)
        Me.SplMid.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents cboOp As System.Windows.Forms.ComboBox
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilTer As System.Windows.Forms.Label
    Friend WithEvents btnFilterN As System.Windows.Forms.Button
    Friend WithEvents btnFilterY As System.Windows.Forms.Button
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents grpTop As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRstDT As System.Windows.Forms.Label
    Friend WithEvents lblDat As System.Windows.Forms.Label
    Friend WithEvents pnlMid As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlMid2 As System.Windows.Forms.Panel
    Friend WithEvents spdStList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents chkCollMove As System.Windows.Forms.CheckBox
    Friend WithEvents SplMid As System.Windows.Forms.SplitContainer
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
End Class
