﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGS10
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
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGS10))
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnOK = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExcel = New CButtonLib.CButton
        Me.btnPrint = New CButtonLib.CButton
        Me.btnQuery = New CButtonLib.CButton
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.cboDeptCd = New System.Windows.Forms.ComboBox
        Me.cboWard = New System.Windows.Forms.ComboBox
        Me.lblDeptWard = New System.Windows.Forms.Label
        Me.txtPatnm = New System.Windows.Forms.TextBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.lblPidNm = New System.Windows.Forms.Label
        Me.lblRegNO = New System.Windows.Forms.Label
        Me.pnlMid = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.lblDr = New System.Windows.Forms.Label
        Me.cboDr = New System.Windows.Forms.ComboBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.rdoCfmOk = New System.Windows.Forms.RadioButton
        Me.rdoCfmNo = New System.Windows.Forms.RadioButton
        Me.rdoCfmAll = New System.Windows.Forms.RadioButton
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboCmtCont = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoIogbnI = New System.Windows.Forms.RadioButton
        Me.rdoIogbnO = New System.Windows.Forms.RadioButton
        Me.rdoIogbnA = New System.Windows.Forms.RadioButton
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.cboPartSlip = New System.Windows.Forms.ComboBox
        Me.lblDate = New System.Windows.Forms.Label
        Me.lblPartSlip = New System.Windows.Forms.Label
        Me.lblTitleDt = New System.Windows.Forms.Label
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.cboPart = New System.Windows.Forms.ComboBox
        Me.pnlBottom.SuspendLayout()
        Me.pnlMid.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnOK)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExcel)
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Controls.Add(Me.chkPreview)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 596)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(987, 34)
        Me.pnlBottom.TabIndex = 126
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOK.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnOK.ColorFillBlend = CBlendItems4
        Me.btnOK.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnOK.Corners.All = CType(6, Short)
        Me.btnOK.Corners.LowerLeft = CType(6, Short)
        Me.btnOK.Corners.LowerRight = CType(6, Short)
        Me.btnOK.Corners.UpperLeft = CType(6, Short)
        Me.btnOK.Corners.UpperRight = CType(6, Short)
        Me.btnOK.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnOK.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnOK.FocalPoints.CenterPtX = 0.5416667!
        Me.btnOK.FocalPoints.CenterPtY = 0.16!
        Me.btnOK.FocalPoints.FocusPtX = 0.0!
        Me.btnOK.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOK.FocusPtTracker = DesignerRectTracker8
        Me.btnOK.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Image = Nothing
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOK.ImageIndex = 0
        Me.btnOK.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOK.Location = New System.Drawing.Point(388, 4)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOK.SideImage = Nothing
        Me.btnOK.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOK.Size = New System.Drawing.Size(96, 25)
        Me.btnOK.TabIndex = 201
        Me.btnOK.Text = "확인"
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOK.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker10
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(881, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(97, 25)
        Me.btnExit.TabIndex = 200
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems6
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
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker12
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(780, 4)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(100, 25)
        Me.btnClear.TabIndex = 195
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExcel.ColorFillBlend = CBlendItems7
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
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExcel.FocusPtTracker = DesignerRectTracker14
        Me.btnExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExcel.ForeColor = System.Drawing.Color.White
        Me.btnExcel.Image = Nothing
        Me.btnExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExcel.ImageIndex = 0
        Me.btnExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExcel.Location = New System.Drawing.Point(679, 4)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(0)
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
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
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
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(582, 4)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(96, 25)
        Me.btnPrint.TabIndex = 199
        Me.btnPrint.Text = "출  력(F5)"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems2
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.5416667!
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
        Me.btnQuery.Location = New System.Drawing.Point(485, 4)
        Me.btnQuery.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(96, 25)
        Me.btnQuery.TabIndex = 198
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkPreview
        '
        Me.chkPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPreview.Checked = True
        Me.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPreview.Location = New System.Drawing.Point(295, 9)
        Me.chkPreview.Margin = New System.Windows.Forms.Padding(0)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(83, 18)
        Me.chkPreview.TabIndex = 128
        Me.chkPreview.Text = "미리보기"
        Me.chkPreview.Visible = False
        '
        'cboDeptCd
        '
        Me.cboDeptCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDeptCd.FormattingEnabled = True
        Me.cboDeptCd.Items.AddRange(New Object() {"[  ] 전체", "검사그룹", "작업그룹"})
        Me.cboDeptCd.Location = New System.Drawing.Point(561, 13)
        Me.cboDeptCd.Margin = New System.Windows.Forms.Padding(0)
        Me.cboDeptCd.Name = "cboDeptCd"
        Me.cboDeptCd.Size = New System.Drawing.Size(207, 20)
        Me.cboDeptCd.TabIndex = 35
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.FormattingEnabled = True
        Me.cboWard.Items.AddRange(New Object() {"[전체]", "검사그룹", "작업그룹"})
        Me.cboWard.Location = New System.Drawing.Point(562, 13)
        Me.cboWard.Margin = New System.Windows.Forms.Padding(0)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(206, 20)
        Me.cboWard.TabIndex = 33
        Me.cboWard.Visible = False
        '
        'lblDeptWard
        '
        Me.lblDeptWard.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDeptWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptWard.ForeColor = System.Drawing.Color.Black
        Me.lblDeptWard.Location = New System.Drawing.Point(481, 13)
        Me.lblDeptWard.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDeptWard.Name = "lblDeptWard"
        Me.lblDeptWard.Size = New System.Drawing.Size(80, 21)
        Me.lblDeptWard.TabIndex = 32
        Me.lblDeptWard.Text = "진료과"
        Me.lblDeptWard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatnm
        '
        Me.txtPatnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatnm.Location = New System.Drawing.Point(562, 35)
        Me.txtPatnm.Margin = New System.Windows.Forms.Padding(0)
        Me.txtPatnm.MaxLength = 9
        Me.txtPatnm.Name = "txtPatnm"
        Me.txtPatnm.Size = New System.Drawing.Size(60, 21)
        Me.txtPatnm.TabIndex = 31
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Location = New System.Drawing.Point(400, 35)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(0)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(63, 21)
        Me.txtRegNo.TabIndex = 30
        '
        'lblPidNm
        '
        Me.lblPidNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPidNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPidNm.Location = New System.Drawing.Point(481, 35)
        Me.lblPidNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPidNm.Name = "lblPidNm"
        Me.lblPidNm.Size = New System.Drawing.Size(80, 21)
        Me.lblPidNm.TabIndex = 27
        Me.lblPidNm.Text = "성명"
        Me.lblPidNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNO
        '
        Me.lblRegNO.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblRegNO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNO.ForeColor = System.Drawing.Color.Black
        Me.lblRegNO.Location = New System.Drawing.Point(319, 35)
        Me.lblRegNO.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRegNO.Name = "lblRegNO"
        Me.lblRegNO.Size = New System.Drawing.Size(80, 21)
        Me.lblRegNO.TabIndex = 26
        Me.lblRegNO.Text = "등록번호"
        Me.lblRegNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMid
        '
        Me.pnlMid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMid.Controls.Add(Me.spdList)
        Me.pnlMid.Location = New System.Drawing.Point(6, 85)
        Me.pnlMid.Name = "pnlMid"
        Me.pnlMid.Size = New System.Drawing.Size(976, 505)
        Me.pnlMid.TabIndex = 128
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
        Me.spdList.Size = New System.Drawing.Size(975, 505)
        Me.spdList.TabIndex = 0
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.cboPart)
        Me.GroupBox6.Controls.Add(Me.cboDr)
        Me.GroupBox6.Controls.Add(Me.Panel2)
        Me.GroupBox6.Controls.Add(Me.Label2)
        Me.GroupBox6.Controls.Add(Me.cboCmtCont)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Controls.Add(Me.Panel1)
        Me.GroupBox6.Controls.Add(Me.lblPidNm)
        Me.GroupBox6.Controls.Add(Me.txtPatnm)
        Me.GroupBox6.Controls.Add(Me.dtpDateE)
        Me.GroupBox6.Controls.Add(Me.txtRegNo)
        Me.GroupBox6.Controls.Add(Me.lblDeptWard)
        Me.GroupBox6.Controls.Add(Me.cboPartSlip)
        Me.GroupBox6.Controls.Add(Me.lblDate)
        Me.GroupBox6.Controls.Add(Me.lblPartSlip)
        Me.GroupBox6.Controls.Add(Me.lblTitleDt)
        Me.GroupBox6.Controls.Add(Me.dtpDateS)
        Me.GroupBox6.Controls.Add(Me.lblRegNO)
        Me.GroupBox6.Controls.Add(Me.cboDeptCd)
        Me.GroupBox6.Controls.Add(Me.cboWard)
        Me.GroupBox6.Controls.Add(Me.lblDr)
        Me.GroupBox6.Location = New System.Drawing.Point(5, -3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Size = New System.Drawing.Size(772, 84)
        Me.GroupBox6.TabIndex = 136
        Me.GroupBox6.TabStop = False
        '
        'lblDr
        '
        Me.lblDr.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDr.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDr.ForeColor = System.Drawing.Color.White
        Me.lblDr.Location = New System.Drawing.Point(5, 59)
        Me.lblDr.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDr.Name = "lblDr"
        Me.lblDr.Size = New System.Drawing.Size(80, 21)
        Me.lblDr.TabIndex = 204
        Me.lblDr.Text = "의뢰의사"
        Me.lblDr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblDr.Visible = False
        '
        'cboDr
        '
        Me.cboDr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDr.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDr.Location = New System.Drawing.Point(651, 34)
        Me.cboDr.Margin = New System.Windows.Forms.Padding(1)
        Me.cboDr.Name = "cboDr"
        Me.cboDr.Size = New System.Drawing.Size(163, 20)
        Me.cboDr.TabIndex = 203
        Me.cboDr.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Lavender
        Me.Panel2.Controls.Add(Me.rdoCfmOk)
        Me.Panel2.Controls.Add(Me.rdoCfmNo)
        Me.Panel2.Controls.Add(Me.rdoCfmAll)
        Me.Panel2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Panel2.Location = New System.Drawing.Point(86, 13)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(225, 21)
        Me.Panel2.TabIndex = 202
        '
        'rdoCfmOk
        '
        Me.rdoCfmOk.AutoSize = True
        Me.rdoCfmOk.Location = New System.Drawing.Point(131, 2)
        Me.rdoCfmOk.Name = "rdoCfmOk"
        Me.rdoCfmOk.Size = New System.Drawing.Size(47, 16)
        Me.rdoCfmOk.TabIndex = 187
        Me.rdoCfmOk.Text = "확인"
        Me.rdoCfmOk.UseVisualStyleBackColor = True
        '
        'rdoCfmNo
        '
        Me.rdoCfmNo.AutoSize = True
        Me.rdoCfmNo.Location = New System.Drawing.Point(62, 2)
        Me.rdoCfmNo.Name = "rdoCfmNo"
        Me.rdoCfmNo.Size = New System.Drawing.Size(59, 16)
        Me.rdoCfmNo.TabIndex = 186
        Me.rdoCfmNo.Text = "미확인"
        Me.rdoCfmNo.UseVisualStyleBackColor = True
        '
        'rdoCfmAll
        '
        Me.rdoCfmAll.AutoSize = True
        Me.rdoCfmAll.Checked = True
        Me.rdoCfmAll.Location = New System.Drawing.Point(8, 2)
        Me.rdoCfmAll.Name = "rdoCfmAll"
        Me.rdoCfmAll.Size = New System.Drawing.Size(47, 16)
        Me.rdoCfmAll.TabIndex = 188
        Me.rdoCfmAll.TabStop = True
        Me.rdoCfmAll.Text = "전체"
        Me.rdoCfmAll.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(5, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 21)
        Me.Label2.TabIndex = 201
        Me.Label2.Text = "구    분"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboCmtCont
        '
        Me.cboCmtCont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCmtCont.Location = New System.Drawing.Point(400, 58)
        Me.cboCmtCont.Margin = New System.Windows.Forms.Padding(1)
        Me.cboCmtCont.Name = "cboCmtCont"
        Me.cboCmtCont.Size = New System.Drawing.Size(368, 20)
        Me.cboCmtCont.TabIndex = 200
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(319, 58)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 21)
        Me.Label1.TabIndex = 199
        Me.Label1.Text = "사    유"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Lavender
        Me.Panel1.Controls.Add(Me.rdoIogbnI)
        Me.Panel1.Controls.Add(Me.rdoIogbnO)
        Me.Panel1.Controls.Add(Me.rdoIogbnA)
        Me.Panel1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Panel1.Location = New System.Drawing.Point(319, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(159, 21)
        Me.Panel1.TabIndex = 198
        '
        'rdoIogbnI
        '
        Me.rdoIogbnI.AutoSize = True
        Me.rdoIogbnI.Location = New System.Drawing.Point(108, 2)
        Me.rdoIogbnI.Name = "rdoIogbnI"
        Me.rdoIogbnI.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnI.TabIndex = 187
        Me.rdoIogbnI.Text = "입원"
        Me.rdoIogbnI.UseVisualStyleBackColor = True
        '
        'rdoIogbnO
        '
        Me.rdoIogbnO.AutoSize = True
        Me.rdoIogbnO.Location = New System.Drawing.Point(57, 2)
        Me.rdoIogbnO.Name = "rdoIogbnO"
        Me.rdoIogbnO.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnO.TabIndex = 186
        Me.rdoIogbnO.Text = "외래"
        Me.rdoIogbnO.UseVisualStyleBackColor = True
        '
        'rdoIogbnA
        '
        Me.rdoIogbnA.AutoSize = True
        Me.rdoIogbnA.Checked = True
        Me.rdoIogbnA.Location = New System.Drawing.Point(8, 2)
        Me.rdoIogbnA.Name = "rdoIogbnA"
        Me.rdoIogbnA.Size = New System.Drawing.Size(47, 16)
        Me.rdoIogbnA.TabIndex = 188
        Me.rdoIogbnA.TabStop = True
        Me.rdoIogbnA.Text = "전체"
        Me.rdoIogbnA.UseVisualStyleBackColor = True
        '
        'dtpDateE
        '
        Me.dtpDateE.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateE.Location = New System.Drawing.Point(209, 35)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateE.TabIndex = 15
        Me.dtpDateE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'cboPartSlip
        '
        Me.cboPartSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPartSlip.Location = New System.Drawing.Point(151, 59)
        Me.cboPartSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboPartSlip.Name = "cboPartSlip"
        Me.cboPartSlip.Size = New System.Drawing.Size(160, 20)
        Me.cboPartSlip.TabIndex = 90
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(193, 39)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(11, 12)
        Me.lblDate.TabIndex = 16
        Me.lblDate.Text = "~"
        '
        'lblPartSlip
        '
        Me.lblPartSlip.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPartSlip.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPartSlip.ForeColor = System.Drawing.Color.White
        Me.lblPartSlip.Location = New System.Drawing.Point(5, 58)
        Me.lblPartSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.lblPartSlip.Name = "lblPartSlip"
        Me.lblPartSlip.Size = New System.Drawing.Size(80, 21)
        Me.lblPartSlip.TabIndex = 89
        Me.lblPartSlip.Text = "부서/분야"
        Me.lblPartSlip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTitleDt
        '
        Me.lblTitleDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTitleDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitleDt.ForeColor = System.Drawing.Color.White
        Me.lblTitleDt.Location = New System.Drawing.Point(5, 35)
        Me.lblTitleDt.Name = "lblTitleDt"
        Me.lblTitleDt.Size = New System.Drawing.Size(80, 21)
        Me.lblTitleDt.TabIndex = 14
        Me.lblTitleDt.Text = "통보일자"
        Me.lblTitleDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateS
        '
        Me.dtpDateS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateS.Location = New System.Drawing.Point(86, 35)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(102, 21)
        Me.dtpDateS.TabIndex = 13
        Me.dtpDateS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'cboPart
        '
        Me.cboPart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPart.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboPart.Items.AddRange(New Object() {"부서", "분야"})
        Me.cboPart.Location = New System.Drawing.Point(86, 59)
        Me.cboPart.Name = "cboPart"
        Me.cboPart.Size = New System.Drawing.Size(64, 20)
        Me.cboPart.TabIndex = 206
        '
        'FGS10
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(987, 630)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.pnlMid)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Name = "FGS10"
        Me.Text = "특이결과 조회"
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlMid.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents pnlMid As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtPatnm As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPidNm As System.Windows.Forms.Label
    Friend WithEvents lblRegNO As System.Windows.Forms.Label
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents lblDeptWard As System.Windows.Forms.Label
    Friend WithEvents cboDeptCd As System.Windows.Forms.ComboBox
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnExcel As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboPartSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblPartSlip As System.Windows.Forms.Label
    Friend WithEvents lblTitleDt As System.Windows.Forms.Label
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoIogbnI As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIogbnO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIogbnA As System.Windows.Forms.RadioButton
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents cboCmtCont As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As CButtonLib.CButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents rdoCfmOk As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCfmNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCfmAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDr As System.Windows.Forms.Label
    Friend WithEvents cboDr As System.Windows.Forms.ComboBox
    Friend WithEvents cboPart As System.Windows.Forms.ComboBox
End Class
