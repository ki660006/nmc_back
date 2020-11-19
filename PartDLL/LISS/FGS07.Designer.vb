<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS07
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
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS07))
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnPrint = New CButtonLib.CButton
        Me.btnQuery = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.txtFilter = New System.Windows.Forms.TextBox
        Me.cboOp = New System.Windows.Forms.ComboBox
        Me.cboFilter = New System.Windows.Forms.ComboBox
        Me.lblFilTer = New System.Windows.Forms.Label
        Me.btnFilterN = New System.Windows.Forms.Button
        Me.btnFilterY = New System.Windows.Forms.Button
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.grpTop = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkUnfit = New System.Windows.Forms.CheckBox
        Me.chkReject = New System.Windows.Forms.CheckBox
        Me.chkTk = New System.Windows.Forms.CheckBox
        Me.chkColl = New System.Windows.Forms.CheckBox
        Me.chkDelGbn = New System.Windows.Forms.CheckBox
        Me.btnClear_dept = New System.Windows.Forms.Button
        Me.btnCdHelp_Dept = New System.Windows.Forms.Button
        Me.txtDept = New System.Windows.Forms.TextBox
        Me.lblDept = New System.Windows.Forms.Label
        Me.cboSlip = New System.Windows.Forms.ComboBox
        Me.lblSlip = New System.Windows.Forms.Label
        Me.lblGbn = New System.Windows.Forms.Label
        Me.lblIOGbn = New System.Windows.Forms.Label
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
        Me.pnlBottomList = New System.Windows.Forms.Panel
        Me.spdStList = New AxFPSpreadADO.AxfpSpread
        Me.chkCollMove = New System.Windows.Forms.CheckBox
        Me.cboPart = New System.Windows.Forms.ComboBox
        Me.pnlBottom.SuspendLayout()
        Me.grpTop.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlIOGbn.SuspendLayout()
        Me.pnlMid.SuspendLayout()
        Me.cmuList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottomList.SuspendLayout()
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.cboPrint)
        Me.pnlBottom.Controls.Add(Me.txtFilter)
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
        Me.pnlBottom.Size = New System.Drawing.Size(1031, 34)
        Me.pnlBottom.TabIndex = 6
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems6
        Me.btnPrint.ColorFillSolid = System.Drawing.Color.White
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint.FocalPoints.CenterPtY = 0.32!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker12
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(622, 6)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(100, 25)
        Me.btnPrint.TabIndex = 204
        Me.btnPrint.Text = "출  력"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(472, 6)
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
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems2
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.0!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker4
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(723, 6)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 202
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
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
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
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
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
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
        'cboPrint
        '
        Me.cboPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.Items.AddRange(New Object() {"위", "아래"})
        Me.cboPrint.Location = New System.Drawing.Point(573, 9)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(48, 20)
        Me.cboPrint.TabIndex = 83
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(249, 7)
        Me.txtFilter.MaxLength = 50
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(97, 21)
        Me.txtFilter.TabIndex = 81
        '
        'cboOp
        '
        Me.cboOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOp.Items.AddRange(New Object() {"=", "Like", "<>"})
        Me.cboOp.Location = New System.Drawing.Point(184, 7)
        Me.cboOp.MaxDropDownItems = 10
        Me.cboOp.Name = "cboOp"
        Me.cboOp.Size = New System.Drawing.Size(64, 20)
        Me.cboOp.TabIndex = 80
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Location = New System.Drawing.Point(80, 7)
        Me.cboFilter.MaxDropDownItems = 10
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(103, 20)
        Me.cboFilter.TabIndex = 79
        '
        'lblFilTer
        '
        Me.lblFilTer.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblFilTer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilTer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilTer.Location = New System.Drawing.Point(7, 6)
        Me.lblFilTer.Name = "lblFilTer"
        Me.lblFilTer.Size = New System.Drawing.Size(72, 22)
        Me.lblFilTer.TabIndex = 78
        Me.lblFilTer.Text = "필터옵션"
        Me.lblFilTer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnFilterN
        '
        Me.btnFilterN.BackColor = System.Drawing.Color.White
        Me.btnFilterN.Location = New System.Drawing.Point(408, 6)
        Me.btnFilterN.Name = "btnFilterN"
        Me.btnFilterN.Size = New System.Drawing.Size(62, 24)
        Me.btnFilterN.TabIndex = 7
        Me.btnFilterN.Text = "필터해제"
        Me.btnFilterN.UseVisualStyleBackColor = False
        '
        'btnFilterY
        '
        Me.btnFilterY.BackColor = System.Drawing.Color.White
        Me.btnFilterY.Location = New System.Drawing.Point(346, 6)
        Me.btnFilterY.Name = "btnFilterY"
        Me.btnFilterY.Size = New System.Drawing.Size(63, 24)
        Me.btnFilterY.TabIndex = 6
        Me.btnFilterY.Text = "필터적용"
        Me.btnFilterY.UseVisualStyleBackColor = False
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
        Me.grpTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTop.Controls.Add(Me.cboPart)
        Me.grpTop.Controls.Add(Me.Panel1)
        Me.grpTop.Controls.Add(Me.chkDelGbn)
        Me.grpTop.Controls.Add(Me.btnClear_dept)
        Me.grpTop.Controls.Add(Me.btnCdHelp_Dept)
        Me.grpTop.Controls.Add(Me.txtDept)
        Me.grpTop.Controls.Add(Me.lblDept)
        Me.grpTop.Controls.Add(Me.cboSlip)
        Me.grpTop.Controls.Add(Me.lblSlip)
        Me.grpTop.Controls.Add(Me.lblGbn)
        Me.grpTop.Controls.Add(Me.lblIOGbn)
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel1.Controls.Add(Me.chkUnfit)
        Me.Panel1.Controls.Add(Me.chkReject)
        Me.Panel1.Controls.Add(Me.chkTk)
        Me.Panel1.Controls.Add(Me.chkColl)
        Me.Panel1.ForeColor = System.Drawing.Color.Navy
        Me.Panel1.Location = New System.Drawing.Point(98, 33)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(317, 21)
        Me.Panel1.TabIndex = 200
        Me.Panel1.TabStop = True
        '
        'chkUnfit
        '
        Me.chkUnfit.AutoSize = True
        Me.chkUnfit.Location = New System.Drawing.Point(228, 3)
        Me.chkUnfit.Name = "chkUnfit"
        Me.chkUnfit.Size = New System.Drawing.Size(84, 16)
        Me.chkUnfit.TabIndex = 3
        Me.chkUnfit.Text = "부적합검체"
        Me.chkUnfit.UseVisualStyleBackColor = True
        '
        'chkReject
        '
        Me.chkReject.AutoSize = True
        Me.chkReject.Location = New System.Drawing.Point(161, 3)
        Me.chkReject.Name = "chkReject"
        Me.chkReject.Size = New System.Drawing.Size(60, 16)
        Me.chkReject.TabIndex = 2
        Me.chkReject.Text = "Reject"
        Me.chkReject.UseVisualStyleBackColor = True
        '
        'chkTk
        '
        Me.chkTk.AutoSize = True
        Me.chkTk.Location = New System.Drawing.Point(82, 3)
        Me.chkTk.Name = "chkTk"
        Me.chkTk.Size = New System.Drawing.Size(72, 16)
        Me.chkTk.TabIndex = 1
        Me.chkTk.Text = "접수취소"
        Me.chkTk.UseVisualStyleBackColor = True
        '
        'chkColl
        '
        Me.chkColl.AutoSize = True
        Me.chkColl.Checked = True
        Me.chkColl.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkColl.Location = New System.Drawing.Point(4, 3)
        Me.chkColl.Name = "chkColl"
        Me.chkColl.Size = New System.Drawing.Size(72, 16)
        Me.chkColl.TabIndex = 0
        Me.chkColl.Text = "채혈취소"
        Me.chkColl.UseVisualStyleBackColor = True
        '
        'chkDelGbn
        '
        Me.chkDelGbn.AutoSize = True
        Me.chkDelGbn.Location = New System.Drawing.Point(707, 16)
        Me.chkDelGbn.Name = "chkDelGbn"
        Me.chkDelGbn.Size = New System.Drawing.Size(102, 16)
        Me.chkDelGbn.TabIndex = 199
        Me.chkDelGbn.Text = "통계에서 제외"
        Me.chkDelGbn.UseVisualStyleBackColor = True
        '
        'btnClear_dept
        '
        Me.btnClear_dept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_dept.Location = New System.Drawing.Point(435, 56)
        Me.btnClear_dept.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_dept.Name = "btnClear_dept"
        Me.btnClear_dept.Size = New System.Drawing.Size(50, 21)
        Me.btnClear_dept.TabIndex = 198
        Me.btnClear_dept.Text = "Clear"
        Me.btnClear_dept.UseVisualStyleBackColor = True
        '
        'btnCdHelp_Dept
        '
        Me.btnCdHelp_Dept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_Dept.Image = CType(resources.GetObject("btnCdHelp_Dept.Image"), System.Drawing.Image)
        Me.btnCdHelp_Dept.Location = New System.Drawing.Point(485, 56)
        Me.btnCdHelp_Dept.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_Dept.Name = "btnCdHelp_Dept"
        Me.btnCdHelp_Dept.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_Dept.TabIndex = 197
        Me.btnCdHelp_Dept.UseVisualStyleBackColor = True
        '
        'txtDept
        '
        Me.txtDept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDept.BackColor = System.Drawing.Color.Thistle
        Me.txtDept.ForeColor = System.Drawing.Color.Brown
        Me.txtDept.Location = New System.Drawing.Point(514, 35)
        Me.txtDept.Multiline = True
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReadOnly = True
        Me.txtDept.Size = New System.Drawing.Size(390, 42)
        Me.txtDept.TabIndex = 196
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDept.ForeColor = System.Drawing.Color.Black
        Me.lblDept.Location = New System.Drawing.Point(435, 35)
        Me.lblDept.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(77, 21)
        Me.lblDept.TabIndex = 195
        Me.lblDept.Text = "진료과"
        Me.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSlip
        '
        Me.cboSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSlip.FormattingEnabled = True
        Me.cboSlip.Location = New System.Drawing.Point(175, 56)
        Me.cboSlip.Name = "cboSlip"
        Me.cboSlip.Size = New System.Drawing.Size(215, 20)
        Me.cboSlip.TabIndex = 167
        '
        'lblSlip
        '
        Me.lblSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSlip.ForeColor = System.Drawing.Color.Black
        Me.lblSlip.Location = New System.Drawing.Point(5, 55)
        Me.lblSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSlip.Name = "lblSlip"
        Me.lblSlip.Size = New System.Drawing.Size(92, 21)
        Me.lblSlip.TabIndex = 166
        Me.lblSlip.Text = "검사부서/분야"
        Me.lblSlip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblGbn
        '
        Me.lblGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblGbn.ForeColor = System.Drawing.Color.Black
        Me.lblGbn.Location = New System.Drawing.Point(5, 33)
        Me.lblGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblGbn.Name = "lblGbn"
        Me.lblGbn.Size = New System.Drawing.Size(92, 21)
        Me.lblGbn.TabIndex = 164
        Me.lblGbn.Text = "구    분"
        Me.lblGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIOGbn
        '
        Me.lblIOGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIOGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGbn.ForeColor = System.Drawing.Color.Black
        Me.lblIOGbn.Location = New System.Drawing.Point(435, 12)
        Me.lblIOGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblIOGbn.Name = "lblIOGbn"
        Me.lblIOGbn.Size = New System.Drawing.Size(77, 21)
        Me.lblIOGbn.TabIndex = 162
        Me.lblIOGbn.Text = "외/입구분"
        Me.lblIOGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlIOGbn
        '
        Me.pnlIOGbn.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnA)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnO)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnI)
        Me.pnlIOGbn.ForeColor = System.Drawing.Color.Navy
        Me.pnlIOGbn.Location = New System.Drawing.Point(513, 12)
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
        Me.dtpDateS.Location = New System.Drawing.Point(98, 11)
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
        Me.lblCencelDate.Location = New System.Drawing.Point(5, 11)
        Me.lblCencelDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblCencelDate.Name = "lblCencelDate"
        Me.lblCencelDate.Size = New System.Drawing.Size(92, 21)
        Me.lblCencelDate.TabIndex = 9
        Me.lblCencelDate.Text = "취소일자"
        Me.lblCencelDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDat
        '
        Me.lblDat.AutoSize = True
        Me.lblDat.Location = New System.Drawing.Point(185, 15)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(11, 12)
        Me.lblDat.TabIndex = 10
        Me.lblDat.Text = "~"
        Me.lblDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(200, 12)
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
        Me.pnlMid.Size = New System.Drawing.Size(1015, 328)
        Me.pnlMid.TabIndex = 11
        '
        'cmuList
        '
        Me.cmuList.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuRst_h})
        Me.cmuList.Name = "cmuRstList"
        Me.cmuList.Size = New System.Drawing.Size(180, 26)
        Me.cmuList.Text = "상황에 맞는 메뉴"
        '
        'mnuRst_h
        '
        Me.mnuRst_h.Name = "mnuRst_h"
        Me.mnuRst_h.Size = New System.Drawing.Size(179, 22)
        Me.mnuRst_h.Text = "REJECT 결과 보기"
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
        Me.spdList.Size = New System.Drawing.Size(1014, 327)
        Me.spdList.TabIndex = 0
        '
        'pnlBottomList
        '
        Me.pnlBottomList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBottomList.Controls.Add(Me.spdStList)
        Me.pnlBottomList.Location = New System.Drawing.Point(8, 418)
        Me.pnlBottomList.Name = "pnlBottomList"
        Me.pnlBottomList.Size = New System.Drawing.Size(1015, 212)
        Me.pnlBottomList.TabIndex = 12
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
        Me.spdStList.Size = New System.Drawing.Size(1015, 212)
        Me.spdStList.TabIndex = 0
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
        'cboPart
        '
        Me.cboPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPart.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboPart.Location = New System.Drawing.Point(98, 56)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(76, 20)
        Me.cboPart.TabIndex = 201
        '
        'FGS07
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1031, 676)
        Me.Controls.Add(Me.pnlBottomList)
        Me.Controls.Add(Me.pnlMid)
        Me.Controls.Add(Me.chkCollMove)
        Me.Controls.Add(Me.grpTop)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS07"
        Me.Text = "채혈/접수 취소 내역"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.grpTop.ResumeLayout(False)
        Me.grpTop.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlIOGbn.ResumeLayout(False)
        Me.pnlMid.ResumeLayout(False)
        Me.cmuList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottomList.ResumeLayout(False)
        CType(Me.spdStList, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents lblIOGbn As System.Windows.Forms.Label
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
    Friend WithEvents pnlBottomList As System.Windows.Forms.Panel
    Friend WithEvents spdStList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblGbn As System.Windows.Forms.Label
    Friend WithEvents cmuList As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuRst_h As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSlip As System.Windows.Forms.Label
    Friend WithEvents chkCollMove As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents chkDelGbn As System.Windows.Forms.CheckBox
    Friend WithEvents btnClear_dept As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_Dept As System.Windows.Forms.Button
    Friend WithEvents txtDept As System.Windows.Forms.TextBox
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkReject As System.Windows.Forms.CheckBox
    Friend WithEvents chkTk As System.Windows.Forms.CheckBox
    Friend WithEvents chkColl As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnfit As System.Windows.Forms.CheckBox
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
End Class
