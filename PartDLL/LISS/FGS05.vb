'>>> 채혈통계 조회

Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Printing

Imports COMMON.CommFN
Imports common.commlogin.login
Imports COMMON.SVar

Imports LISAPP.APP_S.CollTkFn

Public Class FGS05
    Inherits System.Windows.Forms.Form
    Private mbQuery As Boolean = False

    Friend WithEvents btnClear_coll As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_coll As System.Windows.Forms.Button
    Friend WithEvents txtCollIds As System.Windows.Forms.TextBox
    Friend WithEvents lblTest As System.Windows.Forms.Label
    Friend WithEvents btnClear_dept As System.Windows.Forms.Button
    Friend WithEvents btnCdHelp_Dept As System.Windows.Forms.Button
    Friend WithEvents txtDept As System.Windows.Forms.TextBox
    Friend WithEvents lblDept As System.Windows.Forms.Label
    Friend WithEvents txtCollId As System.Windows.Forms.TextBox
    Friend WithEvents chkDelGbn As System.Windows.Forms.CheckBox

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()
    End Sub

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents lblDat As System.Windows.Forms.Label
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents grpTop As System.Windows.Forms.GroupBox
    Friend WithEvents pnlBottomList As System.Windows.Forms.Panel
    Friend WithEvents lblIOGBN As System.Windows.Forms.Label
    Friend WithEvents pnlIOGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoIoGbnA As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIoGbnI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIoGbnO As System.Windows.Forms.RadioButton
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdStaTubeCnt As AxFPSpreadADO.AxfpSpread
    Friend WithEvents btnFilterY As System.Windows.Forms.Button
    Friend WithEvents btnFilterN As System.Windows.Forms.Button
    Friend WithEvents cboOp As System.Windows.Forms.ComboBox
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents lblFilTer As System.Windows.Forms.Label
    Friend WithEvents txtFilter As System.Windows.Forms.TextBox
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS05))
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
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnQuery = New CButtonLib.CButton
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.txtFilter = New System.Windows.Forms.TextBox
        Me.cboOp = New System.Windows.Forms.ComboBox
        Me.btnFilterN = New System.Windows.Forms.Button
        Me.btnFilterY = New System.Windows.Forms.Button
        Me.btnPrint = New CButtonLib.CButton
        Me.lblFilTer = New System.Windows.Forms.Label
        Me.cboFilter = New System.Windows.Forms.ComboBox
        Me.lblDat = New System.Windows.Forms.Label
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker
        Me.lblDate = New System.Windows.Forms.Label
        Me.pnlList = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.grpTop = New System.Windows.Forms.GroupBox
        Me.chkDelGbn = New System.Windows.Forms.CheckBox
        Me.txtCollId = New System.Windows.Forms.TextBox
        Me.btnClear_dept = New System.Windows.Forms.Button
        Me.btnCdHelp_Dept = New System.Windows.Forms.Button
        Me.txtDept = New System.Windows.Forms.TextBox
        Me.lblDept = New System.Windows.Forms.Label
        Me.btnClear_coll = New System.Windows.Forms.Button
        Me.btnCdHelp_coll = New System.Windows.Forms.Button
        Me.txtCollIds = New System.Windows.Forms.TextBox
        Me.lblTest = New System.Windows.Forms.Label
        Me.lblIOGBN = New System.Windows.Forms.Label
        Me.pnlIOGbn = New System.Windows.Forms.Panel
        Me.rdoIoGbnA = New System.Windows.Forms.RadioButton
        Me.rdoIoGbnO = New System.Windows.Forms.RadioButton
        Me.rdoIoGbnI = New System.Windows.Forms.RadioButton
        Me.pnlBottomList = New System.Windows.Forms.Panel
        Me.spdStaTubeCnt = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom.SuspendLayout()
        Me.pnlList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTop.SuspendLayout()
        Me.pnlIOGbn.SuspendLayout()
        Me.pnlBottomList.SuspendLayout()
        CType(Me.spdStaTubeCnt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.cboPrint)
        Me.pnlBottom.Controls.Add(Me.txtFilter)
        Me.pnlBottom.Controls.Add(Me.cboOp)
        Me.pnlBottom.Controls.Add(Me.btnFilterN)
        Me.pnlBottom.Controls.Add(Me.btnFilterY)
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.lblFilTer)
        Me.pnlBottom.Controls.Add(Me.cboFilter)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 595)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(963, 34)
        Me.pnlBottom.TabIndex = 5
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
        Me.btnExit.Location = New System.Drawing.Point(859, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 198
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(758, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 197
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems3
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
        Me.btnExcel.FocalPoints.FocusPtX = 0.03738318!
        Me.btnExcel.FocalPoints.FocusPtY = 0.04!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker6
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(657, 4)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(100, 25)
        Me.btnExcel.TabIndex = 196
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems4
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
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
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker8
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(405, 4)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(100, 25)
        Me.btnQuery.TabIndex = 193
        Me.btnQuery.Text = "조  회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'cboPrint
        '
        Me.cboPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.Items.AddRange(New Object() {"위", "아래"})
        Me.cboPrint.Location = New System.Drawing.Point(506, 6)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(48, 20)
        Me.cboPrint.TabIndex = 83
        '
        'txtFilter
        '
        Me.txtFilter.Location = New System.Drawing.Point(252, 7)
        Me.txtFilter.MaxLength = 50
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(101, 21)
        Me.txtFilter.TabIndex = 81
        '
        'cboOp
        '
        Me.cboOp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOp.Items.AddRange(New Object() {"=", "Like", "<>"})
        Me.cboOp.Location = New System.Drawing.Point(188, 7)
        Me.cboOp.MaxDropDownItems = 10
        Me.cboOp.Name = "cboOp"
        Me.cboOp.Size = New System.Drawing.Size(64, 20)
        Me.cboOp.TabIndex = 80
        '
        'btnFilterN
        '
        Me.btnFilterN.Location = New System.Drawing.Point(444, 5)
        Me.btnFilterN.Name = "btnFilterN"
        Me.btnFilterN.Size = New System.Drawing.Size(84, 24)
        Me.btnFilterN.TabIndex = 7
        Me.btnFilterN.Text = "필터해제"
        '
        'btnFilterY
        '
        Me.btnFilterY.Location = New System.Drawing.Point(356, 5)
        Me.btnFilterY.Name = "btnFilterY"
        Me.btnFilterY.Size = New System.Drawing.Size(84, 24)
        Me.btnFilterY.TabIndex = 6
        Me.btnFilterY.Text = "필터적용"
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems5
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker10
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(556, 4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(100, 25)
        Me.btnPrint.TabIndex = 206
        Me.btnPrint.Text = "출  력"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblFilTer
        '
        Me.lblFilTer.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblFilTer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFilTer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFilTer.Location = New System.Drawing.Point(8, 6)
        Me.lblFilTer.Name = "lblFilTer"
        Me.lblFilTer.Size = New System.Drawing.Size(72, 22)
        Me.lblFilTer.TabIndex = 78
        Me.lblFilTer.Text = "필터옵션"
        Me.lblFilTer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Location = New System.Drawing.Point(80, 7)
        Me.cboFilter.MaxDropDownItems = 10
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(108, 20)
        Me.cboFilter.TabIndex = 79
        '
        'lblDat
        '
        Me.lblDat.AutoSize = True
        Me.lblDat.Location = New System.Drawing.Point(173, 18)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(11, 12)
        Me.lblDat.TabIndex = 10
        Me.lblDat.Text = "~"
        Me.lblDat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(187, 14)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(84, 21)
        Me.dtpDate1.TabIndex = 8
        Me.dtpDate1.Value = New Date(2004, 9, 8, 19, 25, 0, 0)
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(86, 14)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(84, 21)
        Me.dtpDate0.TabIndex = 7
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.White
        Me.lblDate.Location = New System.Drawing.Point(8, 13)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(77, 22)
        Me.lblDate.TabIndex = 9
        Me.lblDate.Text = "채혈일자"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlList
        '
        Me.pnlList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlList.Controls.Add(Me.spdList)
        Me.pnlList.Location = New System.Drawing.Point(8, 113)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(947, 333)
        Me.pnlList.TabIndex = 7
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(943, 329)
        Me.spdList.TabIndex = 0
        '
        'grpTop
        '
        Me.grpTop.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.grpTop.Controls.Add(Me.chkDelGbn)
        Me.grpTop.Controls.Add(Me.txtCollId)
        Me.grpTop.Controls.Add(Me.btnClear_dept)
        Me.grpTop.Controls.Add(Me.btnCdHelp_Dept)
        Me.grpTop.Controls.Add(Me.txtDept)
        Me.grpTop.Controls.Add(Me.lblDept)
        Me.grpTop.Controls.Add(Me.btnClear_coll)
        Me.grpTop.Controls.Add(Me.btnCdHelp_coll)
        Me.grpTop.Controls.Add(Me.txtCollIds)
        Me.grpTop.Controls.Add(Me.lblTest)
        Me.grpTop.Controls.Add(Me.lblIOGBN)
        Me.grpTop.Controls.Add(Me.pnlIOGbn)
        Me.grpTop.Controls.Add(Me.dtpDate0)
        Me.grpTop.Controls.Add(Me.lblDate)
        Me.grpTop.Controls.Add(Me.lblDat)
        Me.grpTop.Controls.Add(Me.dtpDate1)
        Me.grpTop.Location = New System.Drawing.Point(8, -3)
        Me.grpTop.Name = "grpTop"
        Me.grpTop.Size = New System.Drawing.Size(943, 112)
        Me.grpTop.TabIndex = 8
        Me.grpTop.TabStop = False
        '
        'chkDelGbn
        '
        Me.chkDelGbn.AutoSize = True
        Me.chkDelGbn.Location = New System.Drawing.Point(566, 17)
        Me.chkDelGbn.Name = "chkDelGbn"
        Me.chkDelGbn.Size = New System.Drawing.Size(102, 16)
        Me.chkDelGbn.TabIndex = 194
        Me.chkDelGbn.Text = "통계에서 제외"
        Me.chkDelGbn.UseVisualStyleBackColor = True
        '
        'txtCollId
        '
        Me.txtCollId.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.txtCollId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCollId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtCollId.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCollId.ForeColor = System.Drawing.Color.Black
        Me.txtCollId.Location = New System.Drawing.Point(86, 83)
        Me.txtCollId.MaxLength = 10
        Me.txtCollId.Name = "txtCollId"
        Me.txtCollId.ReadOnly = True
        Me.txtCollId.Size = New System.Drawing.Size(60, 22)
        Me.txtCollId.TabIndex = 193
        '
        'btnClear_dept
        '
        Me.btnClear_dept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_dept.Location = New System.Drawing.Point(8, 60)
        Me.btnClear_dept.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_dept.Name = "btnClear_dept"
        Me.btnClear_dept.Size = New System.Drawing.Size(50, 21)
        Me.btnClear_dept.TabIndex = 192
        Me.btnClear_dept.Text = "Clear"
        Me.btnClear_dept.UseVisualStyleBackColor = True
        '
        'btnCdHelp_Dept
        '
        Me.btnCdHelp_Dept.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_Dept.Image = CType(resources.GetObject("btnCdHelp_Dept.Image"), System.Drawing.Image)
        Me.btnCdHelp_Dept.Location = New System.Drawing.Point(58, 60)
        Me.btnCdHelp_Dept.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_Dept.Name = "btnCdHelp_Dept"
        Me.btnCdHelp_Dept.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_Dept.TabIndex = 191
        Me.btnCdHelp_Dept.UseVisualStyleBackColor = True
        '
        'txtDept
        '
        Me.txtDept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDept.BackColor = System.Drawing.Color.Thistle
        Me.txtDept.ForeColor = System.Drawing.Color.Brown
        Me.txtDept.Location = New System.Drawing.Point(86, 39)
        Me.txtDept.Multiline = True
        Me.txtDept.Name = "txtDept"
        Me.txtDept.ReadOnly = True
        Me.txtDept.Size = New System.Drawing.Size(844, 42)
        Me.txtDept.TabIndex = 190
        '
        'lblDept
        '
        Me.lblDept.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDept.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDept.ForeColor = System.Drawing.Color.Black
        Me.lblDept.Location = New System.Drawing.Point(8, 39)
        Me.lblDept.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDept.Name = "lblDept"
        Me.lblDept.Size = New System.Drawing.Size(77, 21)
        Me.lblDept.TabIndex = 189
        Me.lblDept.Text = "진료과"
        Me.lblDept.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClear_coll
        '
        Me.btnClear_coll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnClear_coll.Location = New System.Drawing.Point(881, 84)
        Me.btnClear_coll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear_coll.Name = "btnClear_coll"
        Me.btnClear_coll.Size = New System.Drawing.Size(49, 21)
        Me.btnClear_coll.TabIndex = 188
        Me.btnClear_coll.Text = "Clear"
        Me.btnClear_coll.UseVisualStyleBackColor = True
        '
        'btnCdHelp_coll
        '
        Me.btnCdHelp_coll.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCdHelp_coll.Image = CType(resources.GetObject("btnCdHelp_coll.Image"), System.Drawing.Image)
        Me.btnCdHelp_coll.Location = New System.Drawing.Point(147, 84)
        Me.btnCdHelp_coll.Margin = New System.Windows.Forms.Padding(0)
        Me.btnCdHelp_coll.Name = "btnCdHelp_coll"
        Me.btnCdHelp_coll.Size = New System.Drawing.Size(26, 21)
        Me.btnCdHelp_coll.TabIndex = 187
        Me.btnCdHelp_coll.UseVisualStyleBackColor = True
        '
        'txtCollIds
        '
        Me.txtCollIds.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCollIds.BackColor = System.Drawing.Color.Thistle
        Me.txtCollIds.ForeColor = System.Drawing.Color.Brown
        Me.txtCollIds.Location = New System.Drawing.Point(174, 84)
        Me.txtCollIds.Multiline = True
        Me.txtCollIds.Name = "txtCollIds"
        Me.txtCollIds.ReadOnly = True
        Me.txtCollIds.Size = New System.Drawing.Size(706, 21)
        Me.txtCollIds.TabIndex = 186
        '
        'lblTest
        '
        Me.lblTest.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTest.ForeColor = System.Drawing.Color.Black
        Me.lblTest.Location = New System.Drawing.Point(8, 84)
        Me.lblTest.Margin = New System.Windows.Forms.Padding(1)
        Me.lblTest.Name = "lblTest"
        Me.lblTest.Size = New System.Drawing.Size(77, 21)
        Me.lblTest.TabIndex = 185
        Me.lblTest.Text = "채혈자"
        Me.lblTest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIOGBN
        '
        Me.lblIOGBN.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIOGBN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIOGBN.ForeColor = System.Drawing.Color.White
        Me.lblIOGBN.Location = New System.Drawing.Point(296, 14)
        Me.lblIOGBN.Name = "lblIOGBN"
        Me.lblIOGBN.Size = New System.Drawing.Size(90, 22)
        Me.lblIOGBN.TabIndex = 162
        Me.lblIOGBN.Text = "외래/입원구분"
        Me.lblIOGBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlIOGbn
        '
        Me.pnlIOGbn.BackColor = System.Drawing.Color.AliceBlue
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnA)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnO)
        Me.pnlIOGbn.Controls.Add(Me.rdoIoGbnI)
        Me.pnlIOGbn.ForeColor = System.Drawing.Color.Navy
        Me.pnlIOGbn.Location = New System.Drawing.Point(387, 14)
        Me.pnlIOGbn.Name = "pnlIOGbn"
        Me.pnlIOGbn.Size = New System.Drawing.Size(173, 22)
        Me.pnlIOGbn.TabIndex = 163
        Me.pnlIOGbn.TabStop = True
        '
        'rdoIoGbnA
        '
        Me.rdoIoGbnA.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnA.Location = New System.Drawing.Point(8, 1)
        Me.rdoIoGbnA.Name = "rdoIoGbnA"
        Me.rdoIoGbnA.Size = New System.Drawing.Size(48, 20)
        Me.rdoIoGbnA.TabIndex = 3
        Me.rdoIoGbnA.Tag = "0"
        Me.rdoIoGbnA.Text = "전체"
        '
        'rdoIoGbnO
        '
        Me.rdoIoGbnO.Checked = True
        Me.rdoIoGbnO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnO.Location = New System.Drawing.Point(64, 1)
        Me.rdoIoGbnO.Name = "rdoIoGbnO"
        Me.rdoIoGbnO.Size = New System.Drawing.Size(48, 20)
        Me.rdoIoGbnO.TabIndex = 0
        Me.rdoIoGbnO.TabStop = True
        Me.rdoIoGbnO.Tag = "1"
        Me.rdoIoGbnO.Text = "외래"
        '
        'rdoIoGbnI
        '
        Me.rdoIoGbnI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnI.Location = New System.Drawing.Point(120, 1)
        Me.rdoIoGbnI.Name = "rdoIoGbnI"
        Me.rdoIoGbnI.Size = New System.Drawing.Size(48, 20)
        Me.rdoIoGbnI.TabIndex = 1
        Me.rdoIoGbnI.Tag = "2"
        Me.rdoIoGbnI.Text = "입원"
        '
        'pnlBottomList
        '
        Me.pnlBottomList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBottomList.BackColor = System.Drawing.Color.White
        Me.pnlBottomList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottomList.Controls.Add(Me.spdStaTubeCnt)
        Me.pnlBottomList.Location = New System.Drawing.Point(8, 448)
        Me.pnlBottomList.Name = "pnlBottomList"
        Me.pnlBottomList.Size = New System.Drawing.Size(947, 144)
        Me.pnlBottomList.TabIndex = 10
        '
        'spdStaTubeCnt
        '
        Me.spdStaTubeCnt.DataSource = Nothing
        Me.spdStaTubeCnt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdStaTubeCnt.Location = New System.Drawing.Point(0, 0)
        Me.spdStaTubeCnt.Name = "spdStaTubeCnt"
        Me.spdStaTubeCnt.OcxState = CType(resources.GetObject("spdStaTubeCnt.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdStaTubeCnt.Size = New System.Drawing.Size(943, 140)
        Me.spdStaTubeCnt.TabIndex = 0
        '
        'FGS05
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(963, 629)
        Me.Controls.Add(Me.pnlBottomList)
        Me.Controls.Add(Me.grpTop)
        Me.Controls.Add(Me.pnlList)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS05"
        Me.Text = "채혈자통계 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.pnlList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTop.ResumeLayout(False)
        Me.grpTop.PerformLayout()
        Me.pnlIOGbn.ResumeLayout(False)
        Me.pnlBottomList.ResumeLayout(False)
        CType(Me.spdStaTubeCnt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " 메인 버튼 처리 "
    ' Function Key정의
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                ' 화면정리
                btnClear_Click(Nothing, Nothing)

            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = "Private Sub btnQuery_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.ButtonClick"

        Me.Cursor = Cursors.WaitCursor

        sbQuery()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        Try
            sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            sbExecl(Me.cboPrint.SelectedIndex)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

#Region " Form내부 함수 "

    Public Sub sbDisplay_Collect_TubeNm()
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Tube_List()

            With Me.spdStaTubeCnt
                If dt.Rows.Count > 0 Then
                    .MaxCols = dt.Rows.Count + 1 + 2

                    For i As Integer = 1 To 3
                        .Row = 0 : .Col = i : .ColID = .Text
                    Next

                    For intCnt As Integer = 0 To dt.Rows.Count - 1
                        .Row = 0
                        .Col = intCnt + 2 + 2
                        .Text = dt.Rows(intCnt).Item("tubenmd").ToString
                        .ColID = .Text
                    Next
                End If
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    '< add freety 2005/04/04 : Filter Off
    Private Sub sbFilterOff()
        With Me.spdList
            .ReDraw = False

            For i As Integer = 1 To .MaxRows
                .Row = i
                If .RowHidden Then
                    .RowHidden = False
                End If
            Next

            .ShadowColor = System.Drawing.Color.FromArgb(224, 224, 224)

            sbTubeStatistics()

            .ReDraw = True
        End With
    End Sub

    '< add freety 2005/04/04 : Filter On
    Private Sub sbFilterOn()
        Dim iCol As Integer = 0
        Dim bFilter As Boolean = False

        With Me.spdList
            .ReDraw = False

            For i As Integer = 1 To .MaxCols
                .Col = i
                .Row = 0

                If .Text = Me.cboFilter.Text Then
                    iCol = i

                    Exit For
                End If
            Next

            If iCol = 0 Then Return
            If Me.cboOp.SelectedIndex < 0 Then Return
            If Me.txtFilter.Text = "" Then Return

            For j As Integer = 1 To .MaxRows
                .Col = iCol
                .Row = j

                If Me.cboOp.Text = "=" Then
                    If Not .Text = Me.txtFilter.Text Then
                        .RowHidden = True
                        bFilter = True
                    End If
                ElseIf Me.cboOp.Text.ToUpper() = "LIKE" Then
                    If Not .Text.IndexOf(Me.txtFilter.Text) >= 0 Then
                        .RowHidden = True
                        bFilter = True
                    End If
                ElseIf Me.cboOp.Text = "<>" Then
                    If .Text = Me.txtFilter.Text Then
                        .RowHidden = True
                        bFilter = True
                    End If
                End If
            Next

            If bFilter Then
                .ShadowColor = System.Drawing.Color.LightSteelBlue

                sbTubeStatistics()
            End If

            .ReDraw = True
        End With
    End Sub

    ' Form초기화
    Private Sub sbFormInitialize()

        Try
            Me.Tag = "Load"

            '-- 서버날짜로 설정
            Me.dtpDate1.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            Me.dtpDate0.Value = dtpDate1.Value

            sbFormClear(0)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try


    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal aiPhase As Integer)

        Try
            If InStr("0", aiPhase.ToString, CompareMethod.Text) > 0 Then
                spdList.MaxRows = 0

                With spdStaTubeCnt
                    .MaxCols = 12 + 2 : .MaxRows = 0

                    .Row = 0 : .Row2 = 0
                    .Col = 2 + 2 : .Col2 = .MaxCols
                    .BlockMode = True
                    .Text = ""
                    .BlockMode = False
                End With

            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    ' 출력
    Private Sub sbExecl(ByVal aiMode As Integer)
        Dim sBuf As String = ""

        Select Case aiMode
            Case 0
                With Me.spdList
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                    .MaxRows = .MaxRows + 1
                    .InsertRows(1, 1)

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf
                    Next

                    If .ExportToExcel("collect_list.xls", "collect list", "") Then
                        Process.Start("collect_list.xls")
                    End If

                    .DeleteRows(1, 1)
                    .MaxRows -= 1
                End With

            Case 1
                With Me.spdStaTubeCnt
                    .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                    .MaxRows = .MaxRows + 1
                    .InsertRows(1, 1)

                    For i As Integer = 1 To .MaxCols
                        .Col = i : .Row = 0 : sBuf = .Text
                        .Col = i : .Row = 1 : .Text = sBuf
                    Next

                    If .ExportToExcel("collect_sum.xls", "collect sum", "") Then
                        Process.Start("collect_sum.xls")
                    End If

                    .DeleteRows(1, 1)
                    .MaxRows -= 1
                End With

        End Select
    End Sub

    Private Sub sbQuery()

        Try
            Dim sIoGbn As String = ""
            Dim sDeptWards As String = ""
            Dim sCollIds As String = ""

            Dim alKeys As New ArrayList
            Dim sCollDt_old As String = ""
            Dim iNo As Integer

            If rdoIoGbnO.Checked = True Then sIoGbn = "O"
            If rdoIoGbnI.Checked = True Then sIoGbn = "I"


            If Me.txtDept.Text <> "" Then sDeptWards = Me.txtDept.Tag.ToString
            If Me.txtCollId.Text <> "" Then sCollIds = Me.txtCollIds.Tag.ToString

            ' 리스트 조회
            Dim dt As DataTable = fnGet_Collect_List(dtpDate0.Text, dtpDate1.Text, sIoGbn, chkDelGbn.Checked, sDeptWards, sCollIds)

            If dt.Rows.Count > 0 Then
                With spdList
                    .ReDraw = False
                    .MaxRows = 0
                    For ix As Integer = 0 To dt.Rows.Count - 1
                        .MaxRows += 1

                        .Row = .MaxRows

                        If alKeys.Contains(dt.Rows(ix).Item("regno").ToString + dt.Rows(ix).Item("colldt").ToString) Then
                            .Col = .GetColFromID("colldt") : .ForeColor = .BackColor
                            .Col = .GetColFromID("regno") : .ForeColor = .BackColor
                            .Col = .GetColFromID("patnm") : .ForeColor = .BackColor
                            .Col = .GetColFromID("sexage") : .ForeColor = .BackColor
                            .Col = .GetColFromID("doctornm") : .ForeColor = .BackColor
                            .Col = .GetColFromID("deptcd") : .ForeColor = .BackColor
                            .Col = .GetColFromID("wardroom") : .ForeColor = .BackColor
                        Else

                            iNo += 1

                            .Col = 0 : .Text = iNo.ToString

                            ' Line 그리기
                            If alKeys.Count > 0 Then Fn.DrawBorderLineTop(spdList, .Row + 1)

                            alKeys.Add(dt.Rows(ix).Item("regno").ToString + dt.Rows(ix).Item("colldt").ToString)

                        End If

                        .Col = .GetColFromID("colldt") : .Text = dt.Rows(ix).Item("colldt").ToString
                        .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString
                        .Col = .GetColFromID("patnm") : .Text = dt.Rows(ix).Item("patnm").ToString
                        .Col = .GetColFromID("sexage") : .Text = dt.Rows(ix).Item("sexage").ToString
                        .Col = .GetColFromID("doctornm") : .Text = dt.Rows(ix).Item("doctornm").ToString
                        .Col = .GetColFromID("deptcd") : .Text = dt.Rows(ix).Item("deptcd").ToString
                        .Col = .GetColFromID("wardroom") : .Text = dt.Rows(ix).Item("wardroom").ToString
                        .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString
                        .Col = .GetColFromID("bcno") : .Text = Fn.BCNO_View(dt.Rows(ix).Item("bcno").ToString)
                        .Col = .GetColFromID("tubenmd") : .Text = dt.Rows(ix).Item("tubenmd").ToString
                        .Col = .GetColFromID("collnm") : .Text = dt.Rows(ix).Item("collnm").ToString
                    Next

                    .ReDraw = True
                End With

                dt = Nothing

                ' 통계 조회
                sbTubeStatistics()

            Else
                Me.spdList.MaxRows = 0
                Me.spdStaTubeCnt.MaxRows = 0

                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조건에 해당하는 데이타가 없습니다")
            End If


        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdList.ReDraw = True
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub sbSetFilterColumn()
        With Me.spdList
            .Row = 0

            For j As Integer = 1 To .MaxCols
                .Col = j

                If .ColHidden = False Then
                    Me.cboFilter.Items.Add(.Text)
                End If
            Next
        End With
    End Sub

    Private Sub sbTubeStatistics()
        Try
            With Me.spdStaTubeCnt
                .MaxRows = 0
                .ReDraw = False

                .MaxRows = 1

                .Row = 1
                .Col = -1
                .BackColor = Drawing.Color.FromArgb(244, 244, 255)

                '환자수
                .Row = 1 : .Row2 = .MaxRows
                .Col = .GetColFromID("환자수") : .Col2 = .GetColFromID("환자수")
                .BlockMode = True : .BackColor = Drawing.Color.FromArgb(213, 255, 213) : .BlockMode = False

                '용기수
                .Row = 1 : .Row2 = .MaxRows
                .Col = .GetColFromID("용기수") : .Col2 = .GetColFromID("용기수")
                .BlockMode = True : .BackColor = Drawing.Color.FromArgb(234, 234, 255) : .BlockMode = False

                For j As Integer = 1 To .MaxCols
                    .Col = j

                    If j = .GetColFromID("채혈일자") Then
                        .Text = "총   계"
                    Else
                        .Text = "0"
                    End If
                Next
            End With

            Dim lTotTubeCnt As Long = 0
            Dim lTotPatCnt As Long = 0
            Dim sTotRegNo_Comp As String = ""
            Dim sCollDay_C As String = "", sCollDay_P As String = ""
            Dim sRegNo_C As String = "", sCollDt_C As String
            Dim sTubeNm_C As String = ""
            Dim iCurRow_spdStaTubeCnt As Integer = 0

            With Me.spdList
                For i As Integer = 1 To .MaxRows
                    .Row = i

                    If .RowHidden = False Then
                        .Col = .GetColFromID("colldt")
                        sCollDay_C = Format(Convert.ToDateTime(.Text), "yyyy-MM-dd")

                        '채혈일자가 바뀔 경우
                        If sCollDay_C <> sCollDay_P And sCollDay_P <> "" Then
                            With Me.spdStaTubeCnt
                                '총계
                                .Col = .GetColFromID("환자수")
                                .Row = 1
                                .Text = (Val(.Text) + lTotPatCnt).ToString()

                                .Col = .GetColFromID("용기수")
                                .Row = 1
                                .Text = (Val(.Text) + lTotTubeCnt).ToString()

                                '채혈일자별 소계
                                .Col = .GetColFromID("용기수")
                                .Row = .MaxRows
                                .Text = lTotTubeCnt.ToString()

                                .Col = .GetColFromID("환자수")
                                .Row = .MaxRows
                                .Text = lTotPatCnt.ToString()

                                lTotTubeCnt = 0
                                lTotPatCnt = 0
                                sTotRegNo_Comp = ""
                            End With
                        End If

                        lTotTubeCnt += 1

                        .Col = .GetColFromID("regno") : sRegNo_C = .Text
                        .Col = .GetColFromID("colldt") : sCollDt_C = .Text

                        .Col = .GetColFromID("tubenmd")
                        sTubeNm_C = .Text

                        If sTotRegNo_Comp.IndexOf("'" + sRegNo_C + sCollDt_C + "'") < 0 Then
                            lTotPatCnt += 1

                            If sTotRegNo_Comp = "" Then
                                sTotRegNo_Comp = "'" + sRegNo_C + sCollDt_C + "'"
                            Else
                                sTotRegNo_Comp += ",'" + sRegNo_C + sCollDt_C + "'"
                            End If
                        End If

                        With Me.spdStaTubeCnt
                            '용기별 총계
                            .Col = .GetColFromID(sTubeNm_C)
                            .Row = 1
                            .Text = (Val(.Text) + 1).ToString()

                            '용기별 채혈일자별 소계
                            iCurRow_spdStaTubeCnt = 0

                            For k As Integer = 1 To .MaxRows
                                .Row = k
                                .Col = .GetColFromID("채혈일자")
                                If .Text = sCollDay_C Then
                                    iCurRow_spdStaTubeCnt = k
                                    Exit For
                                End If
                            Next

                            If iCurRow_spdStaTubeCnt = 0 Then
                                .MaxRows += 1
                                .Col = .GetColFromID("채혈일자")
                                .Row = .MaxRows
                                .Text = sCollDay_C
                                iCurRow_spdStaTubeCnt = .MaxRows
                            End If

                            .Col = .GetColFromID(sTubeNm_C)
                            .Row = iCurRow_spdStaTubeCnt

                            .Text = (Val(.Text) + 1).ToString()

                        End With

                        sCollDay_P = sCollDay_C
                    End If
                Next

                If lTotTubeCnt > 0 And lTotPatCnt > 0 Then

                    With Me.spdStaTubeCnt
                        '총계
                        .Col = .GetColFromID("환자수")
                        .Row = 1
                        .Text = (Val(.Text) + lTotPatCnt).ToString()

                        .Col = .GetColFromID("용기수")
                        .Row = 1
                        .Text = (Val(.Text) + lTotTubeCnt).ToString()

                        '채혈일자별 소계
                        .Col = .GetColFromID("용기수")
                        .Row = .MaxRows
                        .Text = lTotTubeCnt.ToString()

                        .Col = .GetColFromID("환자수")
                        .Row = .MaxRows
                        .Text = lTotPatCnt.ToString()

                        lTotTubeCnt = 0
                        lTotPatCnt = 0
                        sTotRegNo_Comp = ""
                    End With
                End If

                '용기별 총계가 0 인 컬럼 숨기기
                With Me.spdStaTubeCnt
                    .Row = 1

                    For j As Integer = 1 To .MaxCols
                        .Col = j

                        If Not j = .GetColFromID("채혈일자") Then
                            If .Text = "0" Then
                                .ColHidden = True
                            Else
                                .ColHidden = False
                            End If
                        End If
                    Next
                End With
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.spdStaTubeCnt.ReDraw = True
        End Try
    End Sub
#End Region

#Region " Control Event 처리 "

    Private Sub rdoIoGbn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoIoGbnA.Click, rdoIoGbnO.Click, rdoIoGbnI.Click
        Dim sFn As String = "Handles rdoIoGbnA.Click, rdoIoGbnO.Click, rdoIoGbnI.Click"

        Dim intItemCnt As Integer = 0
        Dim intRow As Integer = 0
        Dim intCol As Integer = -1

        Dim bColHidden As Boolean

        btnClear_dept_Click(Nothing, Nothing)

        Try
            If CType(sender, Windows.Forms.RadioButton).Text = "전체" Then
                ' 전체
                bColHidden = False
                btnCdHelp_Dept.Enabled = False

            ElseIf CType(sender, Windows.Forms.RadioButton).Text = "외래" Then
                ' 외래
                bColHidden = True
                btnCdHelp_Dept.Enabled = True
                lblDept.Text = "진료과"

            ElseIf CType(sender, Windows.Forms.RadioButton).Text = "병동" Then
                ' 병동
                bColHidden = False
                btnCdHelp_Dept.Enabled = True
                lblDept.Text = "병  동"

            End If

            With spdList
                .Col = .GetColFromID("wardroom") : .ColHidden = bColHidden
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

#End Region

    Private Sub MyBase_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        If Me.Tag.ToString = "Load" Then
            Me.rdoIoGbnO.Checked = True
            Me.rdoIoGbn_Click(Me.rdoIoGbnO, Nothing)
            sbDisplay_Collect_TubeNm()
            Me.Tag = ""

            '< add freety 2005/04/06
            sbSetFilterColumn()
            Me.cboPrint.SelectedIndex = 0
            '> add freety 2005/04/06
        End If

    End Sub

    Private Sub btnFilterY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterY.Click
        sbFilterOn()
    End Sub

    Private Sub btnFilterN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilterN.Click
        sbFilterOff()
    End Sub

    Private Sub FGS05_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mbQuery Then Return

        Try
            Dim sReturn As String = ""
            If Me.cboPrint.Text = "위" Then
                Dim invas_buf As New InvAs

                With invas_buf
                    .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                    .SetProperty("UserID", "")

                    Dim a_objParam() As Object
                    ReDim a_objParam(1)

                    a_objParam(0) = Me
                    a_objParam(1) = fnGet_prt_iteminfo()

                    sReturn = CType(.InvokeMember("Display_Result", a_objParam), String)

                    If sReturn Is Nothing Then Return
                    If sReturn.Length < 1 Then Return

                End With
            Else
                Dim alPrtItem = fnGet_prt_iteminfo()

                For ix As Integer = 0 To alPrtItem.Count - 1
                    If ix > 0 Then sReturn += "|"

                    sReturn += CType(alPrtItem(ix), STU_PrtItemInfo).TITLE + "^" + CType(alPrtItem(ix), STU_PrtItemInfo).FIELD + "^" + CType(alPrtItem(ix), STU_PrtItemInfo).WIDTH
                Next

            End If

            sbPrint_Data(sReturn)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        If cboPrint.Text = "위" Then
            With spdList
                For ix As Integer = 1 To .MaxCols

                    .Row = 0 : .Col = ix
                    If .ColHidden = False Then
                        stu_item = New STU_PrtItemInfo

                        stu_item.CHECK = "1"
                        stu_item.TITLE = .Text
                        stu_item.FIELD = .ColID
                        If .ColID = "tatcont" Then
                            stu_item.WIDTH = (.get_ColWidth(ix) * 10 + 50).ToString
                        Else
                            stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString
                        End If
                        alItems.Add(stu_item)
                    End If
                Next
            End With
        Else
            With spdStaTubeCnt
                For ix As Integer = 1 To .MaxCols

                    .Row = 0 : .Col = ix
                    If .ColHidden = False Then
                        stu_item = New STU_PrtItemInfo

                        stu_item.CHECK = "1"

                        stu_item.TITLE = .Text
                        stu_item.FIELD = .ColID

                        stu_item.WIDTH = (.get_ColWidth(ix) * 10).ToString

                        alItems.Add(stu_item)
                    End If

                Next

            End With
        End If
        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)
        Dim arlPrint As New ArrayList

        Try

            If Me.cboPrint.SelectedIndex = 0 Then

                With spdList
                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        Dim strBuf() As String = rsTitle_Item.Split("|"c)
                        Dim arlItem As New ArrayList

                        For intIdx As Integer = 0 To strBuf.Length - 1

                            If strBuf(intIdx) = "" Then Exit For

                            Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                            If intCol > 0 Then

                                Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                                Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                                Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                                .Row = intRow
                                .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                                arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                            End If
                        Next

                        Dim objPat As New FGS00_PATINFO

                        With objPat
                            .alItem = arlItem
                        End With
                        arlPrint.Add(objPat)
                    Next
                End With

                If arlPrint.Count > 0 Then
                    Dim prt As New FGS00_PRINT

                    prt.mbLandscape = True  '-- false : 세로, true : 가로
                    prt.msTitle = "채혈 통계 조회"
                    prt.maPrtData = arlPrint
                    prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                    prt.sbPrint_Preview()
                End If

            Else
                'Dim arlPrint As New ArrayList

                With spdStaTubeCnt
                    For intRow As Integer = 1 To .MaxRows

                        Dim strBuf() As String = rsTitle_Item.Split("|"c)
                        Dim arlItem As New ArrayList

                        For intIdx As Integer = 0 To 0 'strBuf.Length - 1

                            If strBuf(intIdx) = "" Then Exit For

                            Dim intCol As Integer = .GetColFromID(strBuf(0).Split("^"c)(1))

                            If intCol > 0 Then

                                Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                                Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                                Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)


                                .Row = intRow
                                .Col = .GetColFromID(strField) : Dim strVal As String = .Text

                                arlItem.Add(strVal + "^" + strTitle + "^" + strWidth + "^")
                            End If
                        Next

                        Dim strTnms As String = "", strRsts As String = ""

                        For intIdx As Integer = 1 To strBuf.Length - 1

                            If strBuf(intIdx) = "" Then Exit For
                            Dim intCol As Integer = .GetColFromID(strBuf(intIdx).Split("^"c)(1))

                            If intCol > 0 Then

                                Dim strTitle As String = strBuf(intIdx).Split("^"c)(0)
                                Dim strField As String = strBuf(intIdx).Split("^"c)(1)
                                Dim strWidth As String = strBuf(intIdx).Split("^"c)(2)

                                Dim strTnm As String = "", strRst As String = ""
                                .Row = 0 : .Col = intCol : strTnm = .Text
                                .Row = intRow : .Col = .GetColFromID(strField) : strRst = .Text

                                If strTnm = "" Then Exit For

                                strTnms += strTnm + "|"
                                strRsts += strRst + "|"

                            End If
                        Next

                        Dim objPat As New FGS05_PATINFO
                        With objPat
                            .alItem = arlItem

                            .sItemNm = strTnms
                            .sItemCnt = strRsts
                        End With

                        arlPrint.Add(objPat)

                    Next
                End With

                If arlPrint.Count > 0 Then
                    Dim prt As New FGS05_PRINT
                    Dim MaxCnt() As String = rsTitle_Item.Split("|"c)

                    prt.mbLandscape = True
                    prt.msTitle = "채혈 검체별 통계"
                    prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP
                    prt.maPrtData = arlPrint
                    prt.miTotExmCnt = MaxCnt.Length - 1

                    prt.msgExmWidth = 50.1

                    prt.sbPrint_Preview(True)

                End If
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnClear_coll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_coll.Click
        Me.txtCollId.Text = "" : Me.txtCollIds.Text = "" : Me.txtCollIds.Tag = ""
    End Sub

    Private Sub btnClear_dept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear_dept.Click
        Me.txtDept.Text = "" : Me.txtDept.Tag = ""
    End Sub

    Private Sub btnCdHelp_coll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_coll.Click

        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_coll) + Me.btnCdHelp_coll.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_coll)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList
            Dim dt As New DataTable

            dt = LISAPP.COMM.CdFn.fnGet_Usr_List(True)

            objHelp.FormText = "사용자 정보"
            objHelp.OnRowReturnYN = True
            objHelp.MaxRows = 15

            objHelp.AddField("chk", "", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("usrid", "코드", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("usrnm", "사용자명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            aryList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If aryList.Count > 0 Then
                If Me.txtCollId.Text = "" Then
                    Me.txtCollIds.Text = "" : Me.txtCollIds.Tag = ""
                End If

                For ix As Integer = 0 To aryList.Count - 1
                    Me.txtCollId.Text += IIf(Me.txtCollId.Text = "", "", ",").ToString + aryList.Item(ix).ToString.Split("|"c)(1)
                    Me.txtCollIds.Text += IIf(Me.txtCollIds.Text = "", "", ",").ToString + aryList.Item(ix).ToString.Split("|"c)(2)
                    Me.txtCollIds.Tag = Me.txtCollIds.Tag.ToString + IIf(Me.txtCollIds.Tag.ToString = "", "", ",").ToString + aryList.Item(ix).ToString.Split("|"c)(1)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try

    End Sub

    Private Sub btnCdHelp_Dept_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCdHelp_Dept.Click
        Try
            'Top --> 아래쪽에 맞춰지도록 설정
            Dim iTop As Integer = Ctrl.FindControlTop(Me.btnCdHelp_Dept) + Me.btnCdHelp_Dept.Height

            'Left --> 왼쪽에 맞춰지도록 설정
            Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnCdHelp_Dept)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As New DataTable

            If rdoIoGbnI.Checked Then
                dt = OCSAPP.OcsLink.SData.fnGet_WardList()

                objHelp.FormText = "병동 정보"
                objHelp.OnRowReturnYN = True
                objHelp.MaxRows = 15

                objHelp.AddField("chk", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("wardno", "병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("wardnm", "병동명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Else
                dt = OCSAPP.OcsLink.SData.fnGet_DeptList()

                objHelp.FormText = "진료과 정보"
                objHelp.OnRowReturnYN = True
                objHelp.MaxRows = 15

                objHelp.AddField("chk", "", 3, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
                objHelp.AddField("deptcd", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("deptnm", "진료과명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            End If

            alList = objHelp.Display_Result(Me, iLeft, iTop, dt)
            If alList.Count > 0 Then
                Me.txtDept.Text = "" : Me.txtDept.Tag = ""
                For ix As Integer = 0 To alList.Count - 1
                    Me.txtDept.Text += IIf(ix = 0, "", ",").ToString + alList.Item(ix).ToString.Split("|"c)(1)
                    Me.txtDept.Tag = Me.txtDept.Tag.ToString + IIf(ix = 0, "", ",").ToString + alList.Item(ix).ToString.Split("|"c)(0)
                Next
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub txtCollId_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCollId.GotFocus
        Me.txtCollId.SelectionStart = 0
        Me.txtCollId.SelectAll()
    End Sub

    Private Sub txtCollId_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCollId.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtCollId.Text = "" Then Return

        btnCdHelp_coll_Click(Nothing, Nothing)

        Me.txtCollId.Text = ""

    End Sub
End Class


Public Class FGS05_PATINFO
    Public alItem As New ArrayList

    Public sItemNm As String = ""
    Public sItemCnt As String = ""

End Class

Public Class FGS05_PRINT
    Private Const msFile As String = "File : FGS05.vb, Class : S01" & vbTab

    Private miPageNo As Integer = 0
    Private miCIdx As Integer = 0
    Private miTitle_ExmCnt As Integer = 0
    Private miCCol As Integer = 1
    Public mbLandscape As Boolean = False

    Private msgWidth As Single = 0
    Private msgHeight As Single = 0
    Private msgLeft As Single = 10
    Private msgTop As Single = 20

    Private msgPosX() As Single
    Private msgPosY() As Single

    Public msgExmWidth As Single
    Public msTitle As String
    Public maPrtData As ArrayList
    Public msJobGbn As String = ""
    Public msTitle_Date As String
    Public msTitle_Time As String = Format(Now, "yyyy-MM-dd HH:mm")
    Public miTotExmCnt As Integer = 0
    Public miTitleCnt As Integer = 0
    Public mbUseBarNo As Boolean = False
    Public msTitle_sub_right_1 As String = ""

    Public Sub sbPrint_Preview(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint_Preview(boolean)"

        Try
            Dim prtRView As New PrintPreviewDialog
            Dim prtR As New PrintDocument
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtR.DefaultPageSettings.Landscape = mbLandscape
            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then
                prtR.DocumentName = "ACK_" + msTitle


                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
            

                prtRView.Document = prtR
                prtRView.ShowDialog()

                'prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))

        End Try
    End Sub

    Public Sub sbPrint(ByVal rbFixed As Boolean)
        Dim sFn As String = "Sub sbPrint(boolean)"

        Dim prtR As New PrintDocument

        Try
            Dim prtDialog As New PrintDialog
            Dim prtBPress As New DialogResult

            prtDialog.Document = prtR
            prtBPress = prtDialog.ShowDialog

            If prtBPress = DialogResult.OK Then

                prtR.DocumentName = "ACK_" + msTitle

                AddHandler prtR.PrintPage, AddressOf sbPrintPage
                AddHandler prtR.BeginPrint, AddressOf sbPrintData
                AddHandler prtR.EndPrint, AddressOf sbReport
                prtR.Print()
            End If
        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Throw (New Exception(ex.Message + " @" + msFile + sFn, ex))
        End Try

    End Sub

    Private Sub sbReport(ByVal sender As Object, ByVal e As PrintEventArgs)

    End Sub

    Private Sub sbPrintData(ByVal sender As Object, ByVal e As PrintEventArgs)
        miPageNo = 0
        miCIdx = 0
        miCCol = 1
    End Sub

    Public Overridable Sub sbPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)

        Dim intPage As Integer = 0
        Dim sngPosY As Single = 0
        Dim sngPrtH As Single = 0

        Dim fnt_Title As New Font("굴림체", 10, FontStyle.Bold)
        Dim fnt_Body As New Font("굴림체", 10, FontStyle.Regular)
        Dim fnt_Bottom As New Font("굴림체", 9, FontStyle.Regular)

        Dim fnt_BarCd As New Font("Code39(2:3)", 18, FontStyle.Regular)
        Dim fnt_BarCd_Str As New Font("굴림체", 6, FontStyle.Regular)

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        msgWidth = e.PageBounds.Width - 15
        msgHeight = e.PageBounds.Bottom - 12
        msgLeft = 5
        msgTop = 40

        Dim sngTmp As Single = 0
        For ix As Integer = 0 To CType(maPrtData.Item(0), FGS05_PATINFO).alItem.Count - 1
            sngTmp += Convert.ToSingle(CType(maPrtData.Item(0), FGS05_PATINFO).alItem(ix).ToString.Split("^"c)(2))
        Next

        If sngTmp + 40 > msgWidth Then Return

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrtH = Convert.ToSingle(fnt_Body.GetHeight(e.Graphics) * 1.5)

        Dim rect As New Drawing.RectangleF
        Dim intCnt As Integer = 0

        If miCIdx = 0 Then miPageNo = 0

        Dim intCol As Integer = miCCol
        Dim intLine As Integer = 0

        For intCol = miCCol To miTotExmCnt Step miTitle_ExmCnt

            For intIdx As Integer = miCIdx To maPrtData.Count - 1
                If sngPosY = 0 Then
                    sngPosY = fnPrtTitle(e, CType(maPrtData.Item(intIdx), FGS05_PATINFO).sItemNm.Split("|"c), miCCol)
                End If

                '-- 번호
                rect = New Drawing.RectangleF(msgPosX(0), sngPosY + sngPrtH * intLine, msgPosX(1) - msgPosX(0), sngPrtH)
                e.Graphics.DrawString((intIdx + 1).ToString, fnt_Body, Drawing.Brushes.Black, rect, sf_c)

                For ix As Integer = 1 To CType(maPrtData.Item(intIdx), FGS05_PATINFO).alItem.Count
                    rect = New Drawing.RectangleF(msgPosX(ix), sngPosY + sngPrtH * intLine, msgPosX(ix + 1) - msgPosX(ix), sngPrtH)
                    Dim strTmp As String = CType(maPrtData.Item(intIdx), FGS05_PATINFO).alItem(ix - 1).ToString.Split("^"c)(0)

                    e.Graphics.DrawString(strTmp, fnt_Body, Drawing.Brushes.Black, rect, sf_c)
                Next

                Dim sItemNm() As String = CType(maPrtData.Item(intIdx), FGS05_PATINFO).sItemNm.Split("|"c)
                Dim sStCnt() As String = CType(maPrtData.Item(intIdx), FGS05_PATINFO).sItemCnt.Split("|"c)

                intCnt = 0 : Dim intTitleCnt As Integer = CType(maPrtData.Item(intIdx), FGS05_PATINFO).alItem.Count + 1

                For intIx1 As Integer = miCCol To miCCol + miTitle_ExmCnt
                    intCnt += 1
                    If intCnt > miTitle_ExmCnt + 1 Or intIx1 > miTotExmCnt Then
                        Exit For
                    End If

                    If intIx1 > sStCnt.Length Then
                        Exit For
                    End If

                    '-- 건수
                    rect = New Drawing.RectangleF(msgPosX(intTitleCnt + intCnt - 1), sngPosY + sngPrtH * intLine, msgPosX(intTitleCnt + intCnt) - msgPosX(intTitleCnt + intCnt - 1), sngPrtH)
                    e.Graphics.DrawString(sStCnt(intIx1 - 1), fnt_Body, Drawing.Brushes.Black, rect, sf_r)
                Next

                'sngPosY += sngPrtH
                intLine += 1
                If msgHeight - sngPrtH * 3 < sngPosY + sngPrtH * intLine Then miCIdx += 1 : Exit For
                e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrtH * intLine, msgWidth, sngPosY + sngPrtH * intLine)

                miCIdx += 1

            Next

            If miCIdx >= maPrtData.Count Then
                miCCol += miTitle_ExmCnt + 1
                If miCCol < miTotExmCnt Then miCIdx = 0
            End If

            Exit For

        Next
        miPageNo += 1

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY, msgPosX(ix), msgHeight - sngPrtH * 2)
        Next

        '-- 라인
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, msgHeight - sngPrtH * 2, msgWidth, msgHeight - sngPrtH * 2)

        e.Graphics.DrawString(PRG_CONST.Tail_WorkList, fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_r)
        e.Graphics.DrawString("- " + miPageNo.ToString + " -", fnt_Bottom, Drawing.Brushes.Black, New Drawing.RectangleF(msgLeft, msgHeight - sngPrtH * 2, msgWidth - msgLeft - 25, sngPrtH), sf_c)

        If miCIdx < maPrtData.Count Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
        End If

    End Sub

    Public Overridable Function fnPrtTitle(ByVal e As PrintPageEventArgs, ByRef rsItemNm() As String, ByVal riColS As Integer) As Single

        Dim fnt_Title As New Font("굴림체", 16, FontStyle.Bold Or FontStyle.Underline)
        Dim fnt_Head As New Font("굴림체", 9, FontStyle.Regular)
        Dim sngPrt As Single = 0
        Dim sngPosY As Single = 0
        Dim intCnt As Integer = 1
        Dim sngTmp As Single = 0

        miTitleCnt = CType(maPrtData.Item(0), FGS05_PATINFO).alItem.Count + 1

        Dim sngPosX(0 To 1) As Single

        sngPosX(0) = msgLeft
        sngPosX(1) = sngPosX(0) + 40

        For ix As Integer = 1 To miTitleCnt - 1
            ReDim Preserve sngPosX(ix + 1)

            sngPosX(ix + 1) = sngPosX(ix) + Convert.ToSingle(CType(maPrtData.Item(0), FGS05_PATINFO).alItem(ix - 1).ToString.Split("^"c)(2))
        Next

        sngTmp = msgWidth - msgLeft - sngPosX(sngPosX.Length - 1)

        intCnt = Convert.ToInt16(sngTmp / msgExmWidth)
        If intCnt * msgExmWidth > sngTmp Then intCnt -= 1
        miTitle_ExmCnt = intCnt

        'MsgBox(sngPosX.Length.ToString)

        For ix As Integer = 1 To miTitle_ExmCnt + 1
            ReDim Preserve sngPosX(miTitleCnt + ix)
            sngPosX(miTitleCnt + ix) = sngPosX(miTitleCnt + ix - 1) + msgExmWidth
        Next

        sngPosX(sngPosX.Length - 1) = msgWidth

        msgPosX = sngPosX

        Dim sf_c As New Drawing.StringFormat
        Dim sf_l As New Drawing.StringFormat
        Dim sf_r As New Drawing.StringFormat

        sf_c.LineAlignment = StringAlignment.Center : sf_c.Alignment = Drawing.StringAlignment.Center
        sf_l.LineAlignment = StringAlignment.Center : sf_l.Alignment = Drawing.StringAlignment.Near
        sf_r.LineAlignment = StringAlignment.Center : sf_r.Alignment = Drawing.StringAlignment.Far

        sngPrt = Convert.ToSingle(fnt_Title.GetHeight(e.Graphics) * 1.5)

        Dim rectt As New Drawing.RectangleF(msgLeft, msgTop, msgWidth, sngPrt)

        '-- 출력정보
        If msTitle_sub_right_1.Length > msTitle_Time.Length + 6 Then
            msTitle_Time = msTitle_Time.PadRight(msTitle_sub_right_1.Length - 6)
        Else
            msTitle_sub_right_1 = msTitle_sub_right_1.PadRight(msTitle_Time.Length + 6)
        End If

        If msTitle_sub_right_1 <> "" Then
            e.Graphics.DrawString(msTitle_sub_right_1, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * msTitle_sub_right_1.Length, sngPosY + 90, msgWidth - 8 * msTitle_sub_right_1.Length, sngPrt), sf_l)
        End If

        '-- 타이틀
        e.Graphics.DrawString(msTitle, fnt_Title, Drawing.Brushes.Black, rectt, sf_c)

        sngPosY = msgTop + sngPrt * 2
        sngPrt = fnt_Head.GetHeight(e.Graphics)

        '-- 날짜구간
        e.Graphics.DrawString(msTitle_Date, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, msgWidth - sngPosX(0), sngPrt), sf_l)

        '-- 출력시간
        e.Graphics.DrawString("출력시간: " + msTitle_Time, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(msgWidth - 8 * (msTitle_Time.Length + 6), sngPosY, msgWidth - 8 * (msTitle_Time.Length + 6), sngPrt), sf_l)

        sngPosY += sngPrt + sngPrt / 2

        e.Graphics.DrawString("번호", fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(0), sngPosY, sngPosX(1) - sngPosX(0), sngPrt * 2), sf_c)

        For ix As Integer = 1 To CType(maPrtData.Item(0), FGS05_PATINFO).alItem.Count

            Dim strTmp As String = CType(maPrtData.Item(0), FGS05_PATINFO).alItem(ix - 1).ToString.Split("^"c)(1)

            e.Graphics.DrawString(strTmp, fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(ix), sngPosY, sngPosX(ix + 1) - sngPosX(ix), sngPrt * 2), sf_c)
        Next

        intCnt = 0

        For intIdx As Integer = riColS To riColS + miTitle_ExmCnt
            If intIdx > miTotExmCnt Then Exit For

            If intIdx > rsItemNm.Length Then
                Exit For
            End If
            e.Graphics.DrawString(rsItemNm(intIdx - 1), fnt_Head, Drawing.Brushes.Black, New Drawing.RectangleF(sngPosX(miTitleCnt + intCnt), sngPosY, sngPosX(miTitleCnt + 1 + intCnt) - sngPosX(miTitleCnt + intCnt), sngPrt * 2), sf_l)
            intCnt += 1
        Next

        '-- 세로라인
        For ix As Integer = 0 To msgPosX.Length - 1
            e.Graphics.DrawLine(Drawing.Pens.Black, msgPosX(ix), sngPosY - sngPrt / 2, msgPosX(ix), sngPosY + sngPrt * 2)
        Next

        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY - sngPrt / 2, msgWidth, sngPosY - sngPrt / 2)
        e.Graphics.DrawLine(Drawing.Pens.Black, msgLeft, sngPosY + sngPrt * 2, msgWidth, sngPosY + sngPrt * 2)

        msgPosX = sngPosX

        Return sngPosY + sngPrt * 2


    End Function

End Class