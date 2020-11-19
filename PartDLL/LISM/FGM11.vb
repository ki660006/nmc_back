'>>> 양성자 조회

Imports COMMON.CommFN
Imports System.Windows.Forms

Public Class FGM11
    Inherits System.Windows.Forms.Form

    Private Const mc_sFile As String = "File : FGM11.vb, Class : FGM11" & vbTab

    Private Const mc_sShow As String = "보이기"
    Private Const mc_sHide As String = "숨기기"

    Private miProcessing As Integer = 0

    Private m_dt_AntiList As DataTable
    Private ma_dr_AntiList As DataRow()
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnFilter As System.Windows.Forms.Button

    Private m_fpopup_f As FPOPUPFT

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents lblDateGbn As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents tbcAntiList As System.Windows.Forms.TabControl
    Friend WithEvents tpgVertical As System.Windows.Forms.TabPage
    Friend WithEvents tpgHorizontal As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblFilter As System.Windows.Forms.Label
    Friend WithEvents btnShowHide As System.Windows.Forms.Button
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearchF As System.Windows.Forms.Button
    Friend WithEvents spdAntiListV As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdAntiListH As AxFPSpreadADO.AxfpSpread
    Friend WithEvents grpSearchOpt As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGM11))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.grpSearchOpt = New System.Windows.Forms.GroupBox
        Me.btnToggle = New System.Windows.Forms.Button
        Me.lblDateGbn = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdAntiListV = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnExcel = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.tbcAntiList = New System.Windows.Forms.TabControl
        Me.tpgVertical = New System.Windows.Forms.TabPage
        Me.tpgHorizontal = New System.Windows.Forms.TabPage
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdAntiListH = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnFilter = New System.Windows.Forms.Button
        Me.lblFilter = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnShowHide = New System.Windows.Forms.Button
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSearchF = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.grpSearchOpt.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdAntiListV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.tbcAntiList.SuspendLayout()
        Me.tpgVertical.SuspendLayout()
        Me.tpgHorizontal.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdAntiListH, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpSearchOpt
        '
        Me.grpSearchOpt.Controls.Add(Me.btnToggle)
        Me.grpSearchOpt.Controls.Add(Me.lblDateGbn)
        Me.grpSearchOpt.Controls.Add(Me.Label3)
        Me.grpSearchOpt.Controls.Add(Me.dtpDate1)
        Me.grpSearchOpt.Controls.Add(Me.dtpDate0)
        Me.grpSearchOpt.Controls.Add(Me.btnSearch)
        Me.grpSearchOpt.Location = New System.Drawing.Point(0, 0)
        Me.grpSearchOpt.Name = "grpSearchOpt"
        Me.grpSearchOpt.Size = New System.Drawing.Size(404, 48)
        Me.grpSearchOpt.TabIndex = 0
        Me.grpSearchOpt.TabStop = False
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(85, 16)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(36, 21)
        Me.btnToggle.TabIndex = 111
        Me.btnToggle.Text = "↔"
        '
        'lblDateGbn
        '
        Me.lblDateGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDateGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDateGbn.ForeColor = System.Drawing.Color.White
        Me.lblDateGbn.Location = New System.Drawing.Point(8, 16)
        Me.lblDateGbn.Name = "lblDateGbn"
        Me.lblDateGbn.Size = New System.Drawing.Size(76, 21)
        Me.lblDateGbn.TabIndex = 12
        Me.lblDateGbn.Text = "보고일자"
        Me.lblDateGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(214, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(11, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "~"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(228, 16)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate1.TabIndex = 1
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(122, 16)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate0.TabIndex = 0
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Location = New System.Drawing.Point(320, 16)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(76, 21)
        Me.btnSearch.TabIndex = 39
        Me.btnSearch.Text = "조회(F5)"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.spdAntiListV)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 497)
        Me.Panel1.TabIndex = 48
        '
        'spdAntiListV
        '
        Me.spdAntiListV.DataSource = Nothing
        Me.spdAntiListV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdAntiListV.Location = New System.Drawing.Point(0, 0)
        Me.spdAntiListV.Name = "spdAntiListV"
        Me.spdAntiListV.OcxState = CType(resources.GetObject("spdAntiListV.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAntiListV.Size = New System.Drawing.Size(1002, 495)
        Me.spdAntiListV.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 580)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1016, 32)
        Me.pnlBottom.TabIndex = 3
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = True
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems1
        Me.btnExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExcel.Corners.All = CType(6, Short)
        Me.btnExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnExcel.Corners.LowerRight = CType(6, Short)
        Me.btnExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnExcel.Corners.UpperRight = CType(6, Short)
        Me.btnExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExcel.FocalPoints.CenterPtX = 0.5!
        Me.btnExcel.FocalPoints.CenterPtY = 0.08!
        Me.btnExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker2
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(726, 3)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExcel.SideImage = Nothing
        Me.btnExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExcel.Size = New System.Drawing.Size(91, 25)
        Me.btnExcel.TabIndex = 204
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExcel.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(819, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 203
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems3
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
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker6
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(917, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 202
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'tbcAntiList
        '
        Me.tbcAntiList.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tbcAntiList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbcAntiList.Controls.Add(Me.tpgVertical)
        Me.tbcAntiList.Controls.Add(Me.tpgHorizontal)
        Me.tbcAntiList.Location = New System.Drawing.Point(0, 52)
        Me.tbcAntiList.Name = "tbcAntiList"
        Me.tbcAntiList.SelectedIndex = 1 '< 20121009 초기화시 항균제 가로조회로 셋팅 
        Me.tbcAntiList.Size = New System.Drawing.Size(1012, 522)
        Me.tbcAntiList.TabIndex = 2
        '
        'tpgVertical
        '
        Me.tpgVertical.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgVertical.Controls.Add(Me.Panel1)
        Me.tpgVertical.Location = New System.Drawing.Point(4, 4)
        Me.tpgVertical.Name = "tpgVertical"
        Me.tpgVertical.Size = New System.Drawing.Size(1004, 497)
        Me.tpgVertical.TabIndex = 0
        Me.tpgVertical.Text = "항균제 세로 조회"
        Me.tpgVertical.UseVisualStyleBackColor = True
        '
        'tpgHorizontal
        '
        Me.tpgHorizontal.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tpgHorizontal.Controls.Add(Me.Panel3)
        Me.tpgHorizontal.Location = New System.Drawing.Point(4, 4)
        Me.tpgHorizontal.Name = "tpgHorizontal"
        Me.tpgHorizontal.Size = New System.Drawing.Size(1004, 497)
        Me.tpgHorizontal.TabIndex = 1
        Me.tpgHorizontal.Text = "항균제 가로 조회"
        Me.tpgHorizontal.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.spdAntiListH)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1004, 497)
        Me.Panel3.TabIndex = 49
        '
        'spdAntiListH
        '
        Me.spdAntiListH.DataSource = Nothing
        Me.spdAntiListH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdAntiListH.Location = New System.Drawing.Point(0, 0)
        Me.spdAntiListH.Name = "spdAntiListH"
        Me.spdAntiListH.OcxState = CType(resources.GetObject("spdAntiListH.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdAntiListH.Size = New System.Drawing.Size(1002, 495)
        Me.spdAntiListH.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnFilter)
        Me.GroupBox2.Controls.Add(Me.lblFilter)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnShowHide)
        Me.GroupBox2.Controls.Add(Me.cboColumns)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.btnSearchF)
        Me.GroupBox2.Location = New System.Drawing.Point(401, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(615, 48)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'btnFilter
        '
        Me.btnFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnFilter.Image = CType(resources.GetObject("btnFilter.Image"), System.Drawing.Image)
        Me.btnFilter.Location = New System.Drawing.Point(352, 16)
        Me.btnFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFilter.Name = "btnFilter"
        Me.btnFilter.Size = New System.Drawing.Size(26, 21)
        Me.btnFilter.TabIndex = 192
        Me.btnFilter.UseVisualStyleBackColor = True
        '
        'lblFilter
        '
        Me.lblFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFilter.BackColor = System.Drawing.Color.Thistle
        Me.lblFilter.ForeColor = System.Drawing.Color.Brown
        Me.lblFilter.Location = New System.Drawing.Point(379, 16)
        Me.lblFilter.Name = "lblFilter"
        Me.lblFilter.Size = New System.Drawing.Size(146, 21)
        Me.lblFilter.TabIndex = 64
        Me.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(288, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 21)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "필터 선택"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Location = New System.Drawing.Point(280, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(2, 40)
        Me.Label2.TabIndex = 62
        '
        'btnShowHide
        '
        Me.btnShowHide.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnShowHide.Location = New System.Drawing.Point(196, 16)
        Me.btnShowHide.Name = "btnShowHide"
        Me.btnShowHide.Size = New System.Drawing.Size(76, 21)
        Me.btnShowHide.TabIndex = 61
        Me.btnShowHide.Text = "숨기기"
        Me.btnShowHide.UseVisualStyleBackColor = False
        '
        'cboColumns
        '
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Location = New System.Drawing.Point(77, 16)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(114, 20)
        Me.cboColumns.TabIndex = 60
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(12, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 21)
        Me.Label1.TabIndex = 59
        Me.Label1.Text = "컬럼명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSearchF
        '
        Me.btnSearchF.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchF.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearchF.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearchF.Location = New System.Drawing.Point(528, 16)
        Me.btnSearchF.Name = "btnSearchF"
        Me.btnSearchF.Size = New System.Drawing.Size(76, 21)
        Me.btnSearchF.TabIndex = 56
        Me.btnSearchF.Text = "검색"
        Me.btnSearchF.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 100)
        Me.Panel2.TabIndex = 0
        '
        'FGM11
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 612)
        Me.Controls.Add(Me.tbcAntiList)
        Me.Controls.Add(Me.grpSearchOpt)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(1024, 600)
        Me.Name = "FGM11"
        Me.Text = "양성자 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpSearchOpt.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdAntiListV, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.tbcAntiList.ResumeLayout(False)
        Me.tpgVertical.ResumeLayout(False)
        Me.tpgHorizontal.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdAntiListH, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function fnFind_Column_Hidden() As Boolean
        Dim sFn As String = "fnFind_Column_Hidden"

        Try
            Dim sColID As String = Ctrl.Get_Code(Me.cboColumns)

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiListV

            With spd
                Dim iCol As Integer = .GetColFromID(sColID)

                If iCol > 0 Then
                    .Col = iCol

                    If .ColHidden Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Function fnGet_AntiCd(ByVal ra_dr As DataRow()) As ArrayList
        Dim sFn As String = ""

        Try
            Dim al_AntiCd As New ArrayList

            For i As Integer = 1 To ra_dr.Length
                Dim sAntiCd As String = ra_dr(i - 1).Item("anticd").ToString()

                '없는 경우만 추가
                If Not sAntiCd = "" Then
                    If al_AntiCd.Contains(sAntiCd) = False Then
                        al_AntiCd.Add(sAntiCd)
                    End If
                End If
            Next

            al_AntiCd.Sort()

            Return al_AntiCd

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Sub sbDisplay_Clear()
        Dim sFn As String = "sbDisplay_Clear"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            sbDisplayInit_tbcAntiList()

            sbDisplayInit_spdAntiListV()
            sbDisplayInit_spdAntiListH()

            sbDisplayInit_Filter()

            sbLoad_Popup_Filter()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            sbDisplayInit_dtpDate()

            sbDisplayInit_cboColumns()

            sbDisplayInit_Filter()

            sbDisplayInit_spdAntiList(Me.spdAntiListV)

            sbDisplayInit_spdAntiList(Me.spdAntiListH)

            sbLoad_Popup_Filter()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit_cboColumns()
        Dim sFn As String = "sbDisplayInit_cboColumns"

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiListV

            With spd
                Me.cboColumns.Items.Clear()

                For j As Integer = 1 To .MaxCols
                    .Col = j
                    .Row = 0

                    Dim sHeader As String = .Text

                    If Not sHeader = "sortkey" Then
                        sHeader += "".PadRight(100) + "[" + .ColID + "]"

                        Me.cboColumns.Items.Add(sHeader)
                    End If
                Next
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_dtpDate()
        Dim sFn As String = "sbDisplayInit_dtpDate"

        Try
            Me.dtpDate0.Value = New LISAPP.APP_DB.ServerDateTime().GetDateTime
            Me.dtpDate1.Value = Me.dtpDate0.Value

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_Filter()
        Dim sFn As String = "sbDisplayInit_Filter"

        Try
            Me.lblFilter.Text = ""

            m_dt_AntiList = Nothing
            ma_dr_AntiList = Nothing

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_spdAntiList(ByVal r_spd As AxFPSpreadADO.AxfpSpread)
        Dim sFn As String = "sbDisplayInit_spdAntiList"

        Try
            With r_spd
                For j As Integer = 1 To r_spd.MaxCols
                    r_spd.Col = j

                    If r_spd.ColID.EndsWith("cd") Or r_spd.ColID.EndsWith("key") Then
                        r_spd.ColHidden = True
                    End If
                Next
            End With

            If r_spd Is Me.spdAntiListV Then
                sbDisplayInit_spdAntiListV()
            Else
                sbDisplayInit_spdAntiListH()
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_spdAntiListH()
        Dim sFn As String = "sbDisplayInit_spdAntiListH"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiListH

        Try
            spd.ReDraw = False

            With spd
                .MaxCols = .GetColFromID("sortkey")

                .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort

                For i As Integer = 1 To .MaxCols
                    .set_ColUserSortIndicator(i, FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorNone)
                Next

                .ColsFrozen = 0
                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplayInit_spdAntiListV()
        Dim sFn As String = "sbDisplayInit_spdAntiListV"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiListV

        Try
            spd.ReDraw = False

            With spd
                .MaxCols = .GetColFromID("sortkey")

                .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionDefault

                For i As Integer = 1 To .GetColFromID("testmtd")
                    .Col = i

                    If i = 1 Then
                        .ColMerge = FPSpreadADO.MergeConstants.MergeAlways
                    Else
                        .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted
                    End If
                Next

                .ColsFrozen = 0

                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True

        End Try
    End Sub

    Private Sub sbDisplayInit_tbcAntiList()
        Dim sFn As String = "sbDisplayInit_tbcAntiList"

        Try
            miProcessing = 1

            '< 20121009 조회후 항균제가로조회로 초기화 
            'Me.tbcAntiList.SelectedTab = Me.tpgVertical
            Me.tbcAntiList.SelectedTab = Me.tpgHorizontal

            miProcessing = 0

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_AntiList()
        Dim sFn As String = "sbDisplay_AntiList"

        Try
            'Clear
            sbDisplay_Clear()

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim sOpt As String = ""

            If Me.lblDateGbn.Text = "보고일자" Then
                sOpt = "F"
            Else
                sOpt = "O"
            End If

            

            Dim dt As DataTable = LISAPP.APP_M.CommFn.fnGet_Rst_Growth(Me.dtpDate0.Value.ToShortDateString.Replace("-", ""), Me.dtpDate1.Value.ToShortDateString.Replace("-", ""), sOpt)

            m_dt_AntiList = dt

            ma_dr_AntiList = dt.Select()


            '< 20121009 초기화시 항균제 가로조회로 셋팅 
            'Ctrl.DisplayAfterSelect(Me.spdAntiListH, ma_dr_AntiList, True, False)
            Ctrl.DisplayAfterSelect(Me.spdAntiListV, ma_dr_AntiList, True, False)

            'If miProcessing = 1 Then Return

            If Me.tbcAntiList.SelectedTab Is Me.tpgHorizontal Then
                If Me.spdAntiListH.MaxRows = 0 Then
                    sbDisplay_AntiList_Horizontal()
                End If
            End If


        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_AntiList_Filter()
        Dim sFn As String = "sbDisplay_AntiList_Filter"

        Try
            If m_dt_AntiList Is Nothing Then
                MsgBox("조회를 한 이후에 필터할 수 있습니다!!")
                Return
            End If

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            ma_dr_AntiList = m_dt_AntiList.Select(Me.lblFilter.AccessibleName)

            If ma_dr_AntiList.Length < 1 Then
                MsgBox("해당 필터 조건에(" + Me.lblFilter.Text + ") 해당하는 검색 자료가 없습니다!!")
                Return
            End If

            '항균제 세로 조회를 재검색 함 --> 항균제 가로 조회 초기화
            sbDisplayInit_tbcAntiList()
            sbDisplayInit_spdAntiListH()

            Ctrl.DisplayAfterSelect(Me.spdAntiListV, ma_dr_AntiList, True, False)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_AntiList_Horizontal()
        Dim sFn As String = "sbDisplay_AntiList_Horizontal"

        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdAntiListH

        Try
            If ma_dr_AntiList Is Nothing Then Return

            Me.Cursor() = System.Windows.Forms.Cursors.WaitCursor

            Dim al_AntiCd As ArrayList = fnGet_AntiCd(ma_dr_AntiList)

            With spd
                .ReDraw = False

                'AntiCd Column(추가)
                .MaxCols = .GetColFromID("sortkey") + al_AntiCd.Count

                If al_AntiCd.Count > 0 Then
                    .Col = .GetColFromID("sortkey") + 1 : .Col2 = .GetColFromID("sortkey") + al_AntiCd.Count
                    .Row = -1 : .Row2 = -1
                    .BlockMode = True
                    .CellType = FPSpreadADO.CellTypeConstants.CellTypeStaticText
                    .BlockMode = False

                    For j As Integer = 1 To al_AntiCd.Count
                        .Col = .GetColFromID("sortkey") + j
                        .Row = 0
                        .Text = al_AntiCd(j - 1).ToString()
                        .ColID = al_AntiCd(j - 1).ToString()
                        .set_ColWidth(.GetColFromID("sortkey") + j, 6)
                    Next
                End If

                'Data 표시
                Dim a_dr As DataRow() = ma_dr_AntiList

                Dim sSortKeyP As String = ""
                Dim sSortKeyC As String = ""

                For i As Integer = 1 To a_dr.Length
                    sSortKeyC = a_dr(i - 1).Item("sortkey").ToString()

                    If sSortKeyC <> sSortKeyP Then .MaxRows += 1

                    Dim iRow As Integer = .MaxRows

                    For j As Integer = 1 To a_dr(i - 1).Table.Columns.Count
                        Dim iCol As Integer = .GetColFromID(a_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower)

                        If iCol > 0 Then
                            .SetText(iCol, iRow, a_dr(i - 1).Item(j - 1).ToString())
                        Else
                            If a_dr(i - 1).Table.Columns(j - 1).ColumnName.ToLower = "decrst" Then
                                iCol = .GetColFromID(a_dr(i - 1).Item("anticd").ToString())

                                If iCol > 0 Then
                                    .SetText(iCol, iRow, a_dr(i - 1).Item(j - 1).ToString())
                                End If
                            End If
                        End If
                    Next

                    sSortKeyP = sSortKeyC
                Next
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Return_Filter(ByVal rsCont As String, ByVal rsSyntax As String)
        Me.lblFilter.Text = rsCont
        Me.lblFilter.AccessibleName = rsSyntax
    End Sub

    Private Sub sbDisplay_ShowHide()
        Dim sFn As String = "sbDisplay_ShowHide"

        Try
            Dim spd_v As AxFPSpreadADO.AxfpSpread = Me.spdAntiListV
            Dim spd_h As AxFPSpreadADO.AxfpSpread = Me.spdAntiListH

            Dim sColID As String = Ctrl.Get_Code(Me.cboColumns)

            With spd_v
                Dim iCol As Integer = .GetColFromID(sColID)

                If iCol > 0 Then
                    .Col = iCol

                    If Me.btnShowHide.Text = mc_sShow Then
                        .ColHidden = False
                    Else
                        .ColHidden = True
                    End If
                End If
            End With

            With spd_h
                Dim iCol As Integer = .GetColFromID(sColID)

                If iCol > 0 Then
                    .Col = iCol

                    If Me.btnShowHide.Text = mc_sShow Then
                        .ColHidden = False
                    Else
                        .ColHidden = True
                    End If
                End If
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbExcel()
        Dim sFn As String = "sbExcel"

        Dim spd As AxFPSpreadADO.AxfpSpread

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            If Me.tbcAntiList.SelectedTab Is Me.tpgVertical Then
                spd = Me.spdAntiListV
            Else
                spd = Me.spdAntiListH
            End If

            With spd
                .ReDraw = False

                .Col = 1 : .Row = 1 : If .Text = "" Then Exit Sub

                .MaxRows += 1
                .InsertRows(1, 1)

                .Col = 1
                .Col2 = .MaxCols
                .Row = 0
                .Row2 = 0
                Dim sTitle As String = .Clip

                .Col = 1
                .Col2 = .MaxCols
                .Row = 1
                .Row2 = 1
                .Clip = sTitle

                If .ExportToExcel("AntiList.xls", "Anti List", "") Then
                    Process.Start("AntiList.xls")
                End If

                .DeleteRows(1, 1)
                .MaxRows -= 1

                .ReDraw = True
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            spd.ReDraw = True
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbLoad_Popup_Filter()
        Dim sFn As String = "sbLoad_Popup_Filter"

        Try
            Dim al_columns As New ArrayList

            With Me.cboColumns
                For i As Integer = 1 To .Items.Count
                    al_columns.Add(.Items.Item(i - 1))
                Next
            End With

            If Not m_fpopup_f Is Nothing Then
                m_fpopup_f.Close()
                RemoveHandler m_fpopup_f.ReturnPopupFilter, AddressOf sbDisplay_Return_Filter
            End If

            m_fpopup_f = New FPOPUPFT

            With m_fpopup_f
                .Columns = al_columns
                .DisplayInit()
            End With

            m_fpopup_f.TopMost = True
            m_fpopup_f.Hide()

            AddHandler m_fpopup_f.ReturnPopupFilter, AddressOf sbDisplay_Return_Filter

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub FGM11_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnSearch_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select
    End Sub

    '<------- Control Event ------->

    Private Sub FGM11_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "FGM11_Load"

        Try
            DS_FormDesige.sbInti(Me)
            sbDisplayInit()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplay_Clear()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        sbExcel()
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        If m_dt_AntiList Is Nothing Then
            MsgBox("조회를 한 이후에 필터할 수 있습니다!!")
            Return
        End If

        'Top --> btnFilter의 아래쪽에 맞춰지도록 설정
        Dim iTop As Integer = Ctrl.FindControlTop(Me.btnFilter) + Me.btnFilter.Height + Ctrl.menuHeight

        'Left --> btnFilter와 같이 설정
        Dim iLeft As Integer = Ctrl.FindControlLeft(Me.btnFilter)

        With m_fpopup_f
            .TopPoint = iTop
            .LeftPoint = iLeft
            .Display()
        End With
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            sbDisplay_AntiList()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"
        End Try
    End Sub

    Private Sub btnSearchF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchF.Click
        sbDisplay_AntiList_Filter()
    End Sub

    Private Sub btnShowHide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowHide.Click
        sbDisplay_ShowHide()
    End Sub

    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn

        CommFn.SearchToggle(Me.lblDateGbn, Me.btnToggle, enumToggle.ReportdtToJubsudt)

        Me.dtpDate0.Focus()
    End Sub

    Private Sub cboColumns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboColumns.SelectedIndexChanged
        If fnFind_Column_Hidden() Then
            Me.btnShowHide.Text = mc_sShow
        Else
            Me.btnShowHide.Text = mc_sHide
        End If
    End Sub

    Private Sub spd_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles spdAntiListH.Resize, spdAntiListV.Resize
        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

            With spd
                .ReDraw = False
                .Hide()
                .Show()
                .ReDraw = True
            End With

        Catch ex As Exception

        End Try
    End Sub

    Private Sub tbcAntiList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcAntiList.SelectedIndexChanged
        If miProcessing = 1 Then Return

        If Me.tbcAntiList.SelectedTab Is Me.tpgHorizontal Then
            If Me.spdAntiListH.MaxRows = 0 Then
                sbDisplay_AntiList_Horizontal()
            End If
        End If
    End Sub
End Class