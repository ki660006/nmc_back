'>>> 환자/검체 이력 조회

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports common.commlogin.login

Imports LISAPP.APP_DB
Imports COMMON.SVar


Public Class FGS06
    Inherits System.Windows.Forms.Form

    Private Const mc_iMaxDayDiff As Integer = 31
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton

    Private miProcessing As Integer = 0
    Private mbQuery As Boolean = False
    Private msRegno As String = ""
    Private msOrdDtS As String = ""
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Private msOrdDtE As String = ""

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    Public Sub New(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        msRegno = rsRegNo
        msOrdDtS = rsOrdDtS
        msOrdDtE = rsOrdDtE

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
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlMid2 As System.Windows.Forms.Panel
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents tbpDirectQry0 As System.Windows.Forms.TabPage
    Friend WithEvents tbpDirectQry1 As System.Windows.Forms.TabPage
    Friend WithEvents lblBcnoSearch As System.Windows.Forms.Label
    Friend WithEvents lblORdDT As System.Windows.Forms.Label
    Friend WithEvents tbcQryOpt As System.Windows.Forms.TabControl
    Friend WithEvents lblDat As System.Windows.Forms.Label
    Friend WithEvents spdPatInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtBcNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpOrdDtE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpOrdDtS As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lblOCSInFo As System.Windows.Forms.Label
    Friend WithEvents chkVwPat As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS06))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlTop = New System.Windows.Forms.Panel
        Me.spdPatInfo = New AxFPSpreadADO.AxfpSpread
        Me.tbcQryOpt = New System.Windows.Forms.TabControl
        Me.tbpDirectQry0 = New System.Windows.Forms.TabPage
        Me.txtBcNo = New System.Windows.Forms.TextBox
        Me.lblBcnoSearch = New System.Windows.Forms.Label
        Me.tbpDirectQry1 = New System.Windows.Forms.TabPage
        Me.cboPart = New System.Windows.Forms.ComboBox
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.dtpOrdDtE = New System.Windows.Forms.DateTimePicker
        Me.lblDat = New System.Windows.Forms.Label
        Me.dtpOrdDtS = New System.Windows.Forms.DateTimePicker
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.lblORdDT = New System.Windows.Forms.Label
        Me.btnToggle = New System.Windows.Forms.Button
        Me.pnlMid2 = New System.Windows.Forms.Panel
        Me.btnPrint = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.chkVwPat = New System.Windows.Forms.CheckBox
        Me.lblOCSInFo = New System.Windows.Forms.Label
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.pnlTop.SuspendLayout()
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbcQryOpt.SuspendLayout()
        Me.tbpDirectQry0.SuspendLayout()
        Me.tbpDirectQry1.SuspendLayout()
        Me.pnlMid2.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlTop
        '
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlTop.Controls.Add(Me.spdPatInfo)
        Me.pnlTop.Location = New System.Drawing.Point(364, 31)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(629, 53)
        Me.pnlTop.TabIndex = 0
        '
        'spdPatInfo
        '
        Me.spdPatInfo.DataSource = Nothing
        Me.spdPatInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdPatInfo.Location = New System.Drawing.Point(0, 0)
        Me.spdPatInfo.Name = "spdPatInfo"
        Me.spdPatInfo.OcxState = CType(resources.GetObject("spdPatInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPatInfo.Size = New System.Drawing.Size(625, 49)
        Me.spdPatInfo.TabIndex = 0
        Me.spdPatInfo.TabStop = False
        '
        'tbcQryOpt
        '
        Me.tbcQryOpt.Controls.Add(Me.tbpDirectQry0)
        Me.tbcQryOpt.Controls.Add(Me.tbpDirectQry1)
        Me.tbcQryOpt.HotTrack = True
        Me.tbcQryOpt.ItemSize = New System.Drawing.Size(60, 18)
        Me.tbcQryOpt.Location = New System.Drawing.Point(4, 1)
        Me.tbcQryOpt.Name = "tbcQryOpt"
        Me.tbcQryOpt.SelectedIndex = 0
        Me.tbcQryOpt.Size = New System.Drawing.Size(355, 86)
        Me.tbcQryOpt.TabIndex = 0
        '
        'tbpDirectQry0
        '
        Me.tbpDirectQry0.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbpDirectQry0.Controls.Add(Me.txtBcNo)
        Me.tbpDirectQry0.Controls.Add(Me.lblBcnoSearch)
        Me.tbpDirectQry0.Location = New System.Drawing.Point(4, 22)
        Me.tbpDirectQry0.Name = "tbpDirectQry0"
        Me.tbpDirectQry0.Size = New System.Drawing.Size(347, 60)
        Me.tbpDirectQry0.TabIndex = 1
        Me.tbpDirectQry0.Text = "검체번호 조회"
        '
        'txtBcNo
        '
        Me.txtBcNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBcNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBcNo.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtBcNo.Location = New System.Drawing.Point(90, 15)
        Me.txtBcNo.MaxLength = 18
        Me.txtBcNo.Multiline = True
        Me.txtBcNo.Name = "txtBcNo"
        Me.txtBcNo.Size = New System.Drawing.Size(170, 21)
        Me.txtBcNo.TabIndex = 0
        '
        'lblBcnoSearch
        '
        Me.lblBcnoSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblBcnoSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblBcnoSearch.ForeColor = System.Drawing.Color.White
        Me.lblBcnoSearch.Location = New System.Drawing.Point(15, 15)
        Me.lblBcnoSearch.Name = "lblBcnoSearch"
        Me.lblBcnoSearch.Size = New System.Drawing.Size(75, 21)
        Me.lblBcnoSearch.TabIndex = 1
        Me.lblBcnoSearch.Text = "검체번호"
        Me.lblBcnoSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tbpDirectQry1
        '
        Me.tbpDirectQry1.Controls.Add(Me.cboPart)
        Me.tbpDirectQry1.Controls.Add(Me.lblRegNo)
        Me.tbpDirectQry1.Controls.Add(Me.dtpOrdDtE)
        Me.tbpDirectQry1.Controls.Add(Me.lblDat)
        Me.tbpDirectQry1.Controls.Add(Me.dtpOrdDtS)
        Me.tbpDirectQry1.Controls.Add(Me.txtRegNo)
        Me.tbpDirectQry1.Controls.Add(Me.lblORdDT)
        Me.tbpDirectQry1.Controls.Add(Me.btnToggle)
        Me.tbpDirectQry1.Location = New System.Drawing.Point(4, 22)
        Me.tbpDirectQry1.Name = "tbpDirectQry1"
        Me.tbpDirectQry1.Size = New System.Drawing.Size(347, 60)
        Me.tbpDirectQry1.TabIndex = 0
        Me.tbpDirectQry1.Text = "등록번호 조회"
        Me.tbpDirectQry1.Visible = False
        '
        'cboPart
        '
        Me.cboPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPart.Location = New System.Drawing.Point(226, 9)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(117, 20)
        Me.cboPart.TabIndex = 115
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblRegNo.ForeColor = System.Drawing.Color.White
        Me.lblRegNo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblRegNo.Location = New System.Drawing.Point(5, 9)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(72, 21)
        Me.lblRegNo.TabIndex = 6
        Me.lblRegNo.Text = "등록번호"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpOrdDtE
        '
        Me.dtpOrdDtE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDtE.Location = New System.Drawing.Point(176, 31)
        Me.dtpOrdDtE.Name = "dtpOrdDtE"
        Me.dtpOrdDtE.Size = New System.Drawing.Size(86, 21)
        Me.dtpOrdDtE.TabIndex = 9
        '
        'lblDat
        '
        Me.lblDat.BackColor = System.Drawing.Color.Transparent
        Me.lblDat.Location = New System.Drawing.Point(163, 35)
        Me.lblDat.Name = "lblDat"
        Me.lblDat.Size = New System.Drawing.Size(12, 12)
        Me.lblDat.TabIndex = 10
        Me.lblDat.Text = "~"
        '
        'dtpOrdDtS
        '
        Me.dtpOrdDtS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDtS.Location = New System.Drawing.Point(78, 31)
        Me.dtpOrdDtS.Name = "dtpOrdDtS"
        Me.dtpOrdDtS.Size = New System.Drawing.Size(86, 21)
        Me.dtpOrdDtS.TabIndex = 8
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegNo.Location = New System.Drawing.Point(78, 9)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(98, 21)
        Me.txtRegNo.TabIndex = 2
        Me.txtRegNo.Text = "000000000000000"
        '
        'lblORdDT
        '
        Me.lblORdDT.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblORdDT.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblORdDT.ForeColor = System.Drawing.Color.White
        Me.lblORdDT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblORdDT.Location = New System.Drawing.Point(5, 31)
        Me.lblORdDT.Name = "lblORdDT"
        Me.lblORdDT.Size = New System.Drawing.Size(72, 21)
        Me.lblORdDT.TabIndex = 7
        Me.lblORdDT.Tag = "0"
        Me.lblORdDT.Text = "처방일자"
        Me.lblORdDT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnToggle
        '
        Me.btnToggle.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(177, 9)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(48, 21)
        Me.btnToggle.TabIndex = 3
        Me.btnToggle.Text = "↔"
        Me.btnToggle.UseVisualStyleBackColor = False
        '
        'pnlMid2
        '
        Me.pnlMid2.Controls.Add(Me.btnPrint)
        Me.pnlMid2.Controls.Add(Me.btnClear)
        Me.pnlMid2.Controls.Add(Me.btnExit)
        Me.pnlMid2.Controls.Add(Me.chkVwPat)
        Me.pnlMid2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlMid2.Location = New System.Drawing.Point(0, 610)
        Me.pnlMid2.Name = "pnlMid2"
        Me.pnlMid2.Size = New System.Drawing.Size(1155, 34)
        Me.pnlMid2.TabIndex = 4
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
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker2
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(843, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(100, 25)
        Me.btnPrint.TabIndex = 201
        Me.btnPrint.Text = "출  력(F5)"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnClear.Location = New System.Drawing.Point(944, 5)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 200
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
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1045, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(100, 25)
        Me.btnExit.TabIndex = 199
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkVwPat
        '
        Me.chkVwPat.Location = New System.Drawing.Point(8, 7)
        Me.chkVwPat.Name = "chkVwPat"
        Me.chkVwPat.Size = New System.Drawing.Size(145, 20)
        Me.chkVwPat.TabIndex = 3
        Me.chkVwPat.Text = "LIS 환자 신상 보기"
        '
        'lblOCSInFo
        '
        Me.lblOCSInFo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOCSInFo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblOCSInFo.ForeColor = System.Drawing.Color.White
        Me.lblOCSInFo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblOCSInFo.Location = New System.Drawing.Point(365, 8)
        Me.lblOCSInFo.Name = "lblOCSInFo"
        Me.lblOCSInFo.Size = New System.Drawing.Size(211, 21)
        Me.lblOCSInFo.TabIndex = 7
        Me.lblOCSInFo.Text = "최신 OCS 처방 및 환자 정보"
        Me.lblOCSInFo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(4, 89)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1147, 515)
        Me.spdList.TabIndex = 8
        '
        'FGS06
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1155, 644)
        Me.Controls.Add(Me.spdList)
        Me.Controls.Add(Me.tbcQryOpt)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.lblOCSInFo)
        Me.Controls.Add(Me.pnlMid2)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGS06"
        Me.Text = "환자/검체이력 조회"
        Me.pnlTop.ResumeLayout(False)
        CType(Me.spdPatInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbcQryOpt.ResumeLayout(False)
        Me.tbpDirectQry0.ResumeLayout(False)
        Me.tbpDirectQry0.PerformLayout()
        Me.tbpDirectQry1.ResumeLayout(False)
        Me.tbpDirectQry1.PerformLayout()
        Me.pnlMid2.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_part()

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Part_List()

            Me.cboPart.Items.Clear()
            Me.cboPart.Items.Add("[  ] 전체")

            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboPart.Items.Add("[" + dt.Rows(ix).Item("partcd").ToString + "] " + dt.Rows(ix).Item("partnmd").ToString)
            Next

            If Me.cboPart.Items.Count > 0 Then Me.cboPart.SelectedIndex = 0

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbDisplay_OrderHistory(ByVal rsRegNo As String, ByVal rsOrdDtS As String, ByVal rsOrdDtE As String, _
                                       Optional ByVal rsBcNo As String = "", Optional ByVal rsTag As String = "0")
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            COMMON.CommFN.MdiMain.DB_Active_YN = "Y"

            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor()
            miProcessing = 1

            Dim dt As DataTable
            If Me.lblORdDT.Text = "처방일자" Then
                dt = OCSAPP.OcsLink.SData.fnGet_OrdHistory_LIS(rsRegNo, rsOrdDtS, rsOrdDtE, Ctrl.Get_Code(Me.cboPart))
                'Else
                '    dt = OCSAPP.OcsLink.SData.fnGet_OrdHistory_CollDt_FGS06(rsRegNo, rsOrdDtS, rsOrdDtE)
            End If

            Dim dr As DataRow()
            Dim sWhere As String = ""

            If rsBcNo <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "bcno_t = '" + rsBcNo + "'"
            If Ctrl.Get_Code(cboPart) <> "" Then sWhere += IIf(sWhere = "", "", " AND ").ToString + "partcd = '" + Ctrl.Get_Code(cboPart) + "'"

            dr = dt.Select(sWhere, "orddt desc, iogbn, fkocs, bcno, tclscd, compdt")

            dt = Fn.ChangeToDataTable(dr)

            With spd
                .MaxRows = 0

                If dt.Rows.Count < 1 Then Return

                .ReDraw = False
                .MaxRows = dt.Rows.Count

                For i As Integer = 1 To dt.Rows.Count
                    For j As Integer = 1 To dt.Columns.Count
                        Dim iCol As Integer = .GetColFromID(dt.Columns(j - 1).ColumnName.ToLower())

                        If iCol > 0 Then
                            .Col = iCol
                            .Row = i
                            .Text = dt.Rows(i - 1).Item(j - 1).ToString()
                        End If
                    Next
                Next
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            COMMON.CommFN.MdiMain.DB_Active_YN = ""
            spd.ReDraw = True
            miProcessing = 0
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_PatInfo(ByVal rsBcNo As String, ByVal rsRegNo As String)

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor()
            miProcessing = 1

            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_PatInfo_FGS06(rsBcNo, rsRegNo)

            sbDisplayInit_PatInfo()

            If dt.Rows.Count < 1 Then Return

            With Me.spdPatInfo
                Dim sPatInfo() As String = dt.Rows(0).Item("patinfo").ToString.Split("|"c)

                .Row = 1
                .Col = .GetColFromID("orddt") : .Text = dt.Rows(0).Item("orddt").ToString.Trim
                .Col = .GetColFromID("regno") : .Text = dt.Rows(0).Item("regno").ToString.Trim
                .Col = .GetColFromID("patnm") : .Text = sPatInfo(0).Trim
                .Col = .GetColFromID("sexage") : .Text = dt.Rows(0).Item("sexage").ToString.Trim
                .Col = .GetColFromID("idno") : .Text = sPatInfo(3).Trim
                .Col = .GetColFromID("iogbn") : .Text = dt.Rows(0).Item("iogbn").ToString.Trim
                .Col = .GetColFromID("doctornm") : .Text = dt.Rows(0).Item("doctornm").ToString.Trim
                .Col = .GetColFromID("deptcd") : .Text = dt.Rows(0).Item("deptcd").ToString.Trim
                .Col = .GetColFromID("wardroom") : .Text = dt.Rows(0).Item("wardroom").ToString.Trim

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            miProcessing = 0
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit()

        Try

            sbDisplay_part()

            sbDisplayInit_QryOpt()

            sbDisplayInit_PatInfo()

            sbDisplayInit_spdList()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_PatInfo()

        Try
            With Me.spdPatInfo
                .ClearRange(1, 1, .MaxCols, 1, True)
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_QryOpt()

        Try
            Me.txtBcNo.Text = ""

            Me.txtRegNo.Text = ""

            Me.dtpOrdDtE.Value = Convert.ToDateTime(New ServerDateTime().GetDate("-")).AddDays(1)
            Me.dtpOrdDtS.Value = Me.dtpOrdDtE.Value.AddDays(-365)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit_spdList()

        Try
            With Me.spdList
                .Col = .GetColFromID("orddt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("gwa_name")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("doctor_name")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("wardroom")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("state")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("bcno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("workno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("patnm")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("sexage")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("idno")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("colldt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("collid")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("bcno_t")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("tkdt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("tkid")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("bcno_f")
                .ColMerge = FPSpreadADO.MergeConstants.MergeAlways

                .Col = .GetColFromID("fndt")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("fnid")
                .ColMerge = FPSpreadADO.MergeConstants.MergeRestricted

                .Col = .GetColFromID("patnm")
                .ColHidden = True

                .Col = .GetColFromID("sexage")
                .ColHidden = True

                .Col = .GetColFromID("idno")
                .ColHidden = True

                .ColsFrozen = .GetColFromID("bcno")

                .MaxRows = 0

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub sbExcel()

        Try
            With Me.spdList
                If .MaxRows < 1 Then Return

                .ExportToExcel("", "", "")
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGS06_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGS06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.F5
                btnPrint_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub FGS06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo
        sbDisplayInit()

        If msRegno <> "" Then

            Me.txtRegNo.Text = msRegno
            Me.dtpOrdDtS.Text = msOrdDtS
            Me.dtpOrdDtE.Text = msOrdDtE

            Me.txtRegNo_KeyDown(Me.txtRegNo, New System.Windows.Forms.KeyEventArgs(Keys.Enter))

            Me.tbcQryOpt.SelectedIndex = 1
        End If

        Me.WindowState = FormWindowState.Maximized
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbDisplayInit()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If mbQuery Then Return

        Try
            Dim invas_buf As New InvAs

            With invas_buf
                .LoadAssembly(Windows.Forms.Application.StartupPath + "\LISS.dll", "LISS.FGS00")

                .SetProperty("UserID", "")

                Dim a_objParam() As Object
                ReDim a_objParam(1)

                a_objParam(0) = Me
                a_objParam(1) = fnGet_prt_iteminfo()

                Dim strReturn As String = CType(.InvokeMember("Display_Result", a_objParam), String)

                If strReturn Is Nothing Then Return
                If strReturn.Length < 1 Then Return

                sbPrint_Data(strReturn)

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Function fnGet_prt_iteminfo() As ArrayList
        Dim alItems As New ArrayList
        Dim stu_item As STU_PrtItemInfo

        With spdList
            For ix As Integer = 1 To .MaxCols

                .Row = 0 : .Col = ix
                If .ColHidden = False Then
                    stu_item = New STU_PrtItemInfo

                    If .ColID = "order_dt" Or .ColID = "bcno" Or .ColID = "tnms" Or .ColID = "spcnms" Or .ColID = "state" Or .ColID = "gwa_name" Or .ColID = "doctor_name" Or _
                       .ColID = "wardroom" Or .ColID = "patnm" Or .ColID = "sexage" Then
                        stu_item.CHECK = "1"
                    Else
                        stu_item.CHECK = "0"
                    End If
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

        Return alItems

    End Function

    Private Sub sbPrint_Data(ByVal rsTitle_Item As String)

        Try
            Dim arlPrint As New ArrayList

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
                prt.msTitle = "환자/검체 이력조회"
                prt.maPrtData = arlPrint
                prt.msTitle_sub_right_1 = "출력정보: " + USER_INFO.USRID + "/" + USER_INFO.LOCALIP

                prt.sbPrint_Preview()
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnToggle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Me.txtRegNo.Focus()

        Dim CMFN As New COMMON.CommFN.Fn
        CMFN.SearchToggle(Me.lblRegNo, Me.btnToggle, enumToggle.RegnoToName, Me.txtRegNo)
    End Sub

    Private Sub chkVwPat_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkVwPat.CheckedChanged
        With Me.spdList
            If Me.chkVwPat.Checked Then
                .Col = .GetColFromID("patnm")
                .ColHidden = False

                .Col = .GetColFromID("sexage")
                .ColHidden = False

                .Col = .GetColFromID("idno")
                .ColHidden = False
            Else
                .Col = .GetColFromID("patnm")
                .ColHidden = True

                .Col = .GetColFromID("sexage")
                .ColHidden = True

                .Col = .GetColFromID("idno")
                .ColHidden = True
            End If
        End With
    End Sub

    Private Sub Label1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblORdDT.DoubleClick
        With Me.lblORdDT
            If .Text.Trim() = "처방일자" Then
                .BackColor = Color.ForestGreen
                .Text = "채혈일자"
                .Tag = 1
                Me.dtpOrdDtS.Value = Me.dtpOrdDtE.Value.AddDays(-30)
            Else
                .BackColor = Color.Navy
                .Text = "처방일자"
                .Tag = 0
                Me.dtpOrdDtS.Value = Me.dtpOrdDtE.Value.AddDays(-365)
            End If
        End With

    End Sub

    Private Sub txtRegNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRegNo.Click, txtBcNo.Click

        CType(sender, Windows.Forms.TextBox).SelectionStart = 0
        CType(sender, Windows.Forms.TextBox).SelectAll()
    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown, txtBcNo.KeyDown

        Try
            If e.KeyCode <> Keys.Enter Then Return

            CType(sender, System.Windows.Forms.TextBox).SelectAll()

            Dim sRegNo As String = ""
            Dim sOrdDtS As String = ""
            Dim sOrdDtE As String = ""

            If CType(sender, TextBox).Name.ToUpper.EndsWith("BCNO") Then
                Me.txtBcNo.Text = Me.txtBcNo.Text.Replace("-", "").Trim

                If Len(txtBcNo.Text) = 11 Or Len(txtBcNo.Text) = 12 Then
                    Me.txtBcNo.Text = (New LISAPP.APP_DB.DbFn).GetBCPrtToView(Me.txtBcNo.Text)
                End If

                Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_PatInfo_FGS06(Me.txtBcNo.Text, "")

                If dt.Rows.Count < 1 Then
                    MsgBox("해당 검체번호의 정보가 없습니다. 확인하여 주십시요!!")
                    Return
                End If

                sRegNo = dt.Rows(0).Item("regno").ToString()
                sOrdDtS = dt.Rows(0).Item("orddt").ToString().Replace("-", "").Substring(0, 8)
                sOrdDtE = sOrdDtS

                Me.txtRegNo.Text = ""
            Else
                If Me.lblRegNo.Text = "등록번호" Then
                    sRegNo = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Me.txtRegNo.Text = sRegNo
                Else
                    Dim objHelp As New CDHELP.FGCDHELP01
                    Dim alList As New ArrayList
                    Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Patinfo(IIf(Me.lblRegNo.Text = "등록번호", Me.txtRegNo.Text, "").ToString, IIf(Me.lblRegNo.Text <> "등록번호", "", Me.txtRegNo.Text).ToString)

                    objHelp.MaxRows = 15
                    objHelp.Distinct = True
                    objHelp.OnRowReturnYN = True

                    objHelp.AddField("bunho", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                    objHelp.AddField("suname", "성명", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                    objHelp.AddField("idno_full", "주민번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)

                    Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
                    Dim pntFrmXY As Point = Fn.CtrlLocationXY(CType(sender, Windows.Forms.TextBox))

                    alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + CType(sender, Windows.Forms.TextBox).Height + 80, dt)

                    If alList.Count > 0 Then
                        sRegNo = alList.Item(0).ToString.Split("|"c)(0)
                    End If
                End If

                sOrdDtS = Me.dtpOrdDtS.Text.Replace("-", "")
                sOrdDtE = Me.dtpOrdDtE.Text.Replace("-", "")

                Me.txtBcNo.Text = ""
            End If

            If sRegNo = "" Or sOrdDtS = "" Or sOrdDtE = "" Then
                MsgBox("해당 환자가 존재하지 않습니다. 확인하여 주십시요!!")

                Return
            End If

            sbDisplay_OrderHistory(sRegNo, sOrdDtS, sOrdDtE, Me.txtBcNo.Text)
            sbDisplay_PatInfo("", sRegNo)

            If CType(sender, TextBox).Name.ToUpper.EndsWith("BCNO") Then
                Me.txtBcNo.SelectAll()
                Me.txtBcNo.Focus()
            Else
                Me.txtRegNo.SelectAll()
                Me.txtRegNo.Focus()
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub
End Class
