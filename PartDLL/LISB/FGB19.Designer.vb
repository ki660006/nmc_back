<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB19
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
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB19))
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
        Me.Label98 = New System.Windows.Forms.Label
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlSearchGbn = New System.Windows.Forms.Panel
        Me.rdoIn = New System.Windows.Forms.RadioButton
        Me.rdoOut = New System.Windows.Forms.RadioButton
        Me.lblSGbn = New System.Windows.Forms.Label
        Me.btnTExcel = New CButtonLib.CButton
        Me.btnSearch = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.pnlSList = New System.Windows.Forms.Panel
        Me.spdTopList = New AxFPSpreadADO.AxfpSpread
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lbTxt = New System.Windows.Forms.Label
        Me.pnlDetail = New System.Windows.Forms.Panel
        Me.spdDetail = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.pnlSearchGbn.SuspendLayout()
        Me.pnlSList.SuspendLayout()
        CType(Me.spdTopList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlDetail.SuspendLayout()
        CType(Me.spdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label98.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.White
        Me.Label98.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label98.Location = New System.Drawing.Point(4, 3)
        Me.Label98.Margin = New System.Windows.Forms.Padding(1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(80, 21)
        Me.Label98.TabIndex = 213
        Me.Label98.Text = "조회년월"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.CustomFormat = "yyyy-MM"
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate0.Location = New System.Drawing.Point(85, 3)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(70, 21)
        Me.dtpDate0.TabIndex = 212
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(3, 22)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1005, 9)
        Me.Label1.TabIndex = 214
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'pnlSearchGbn
        '
        Me.pnlSearchGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchGbn.Controls.Add(Me.rdoIn)
        Me.pnlSearchGbn.Controls.Add(Me.rdoOut)
        Me.pnlSearchGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlSearchGbn.Location = New System.Drawing.Point(280, 3)
        Me.pnlSearchGbn.Name = "pnlSearchGbn"
        Me.pnlSearchGbn.Size = New System.Drawing.Size(153, 22)
        Me.pnlSearchGbn.TabIndex = 216
        '
        'rdoIn
        '
        Me.rdoIn.Checked = True
        Me.rdoIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIn.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoIn.Location = New System.Drawing.Point(9, 3)
        Me.rdoIn.Margin = New System.Windows.Forms.Padding(1)
        Me.rdoIn.Name = "rdoIn"
        Me.rdoIn.Size = New System.Drawing.Size(56, 18)
        Me.rdoIn.TabIndex = 5
        Me.rdoIn.TabStop = True
        Me.rdoIn.Tag = "1"
        Me.rdoIn.Text = "입고"
        Me.rdoIn.UseCompatibleTextRendering = True
        '
        'rdoOut
        '
        Me.rdoOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoOut.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoOut.Location = New System.Drawing.Point(74, 3)
        Me.rdoOut.Margin = New System.Windows.Forms.Padding(1)
        Me.rdoOut.Name = "rdoOut"
        Me.rdoOut.Size = New System.Drawing.Size(67, 18)
        Me.rdoOut.TabIndex = 6
        Me.rdoOut.Tag = "1"
        Me.rdoOut.Text = "출고"
        Me.rdoOut.UseCompatibleTextRendering = True
        '
        'lblSGbn
        '
        Me.lblSGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSGbn.ForeColor = System.Drawing.Color.White
        Me.lblSGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSGbn.Location = New System.Drawing.Point(199, 3)
        Me.lblSGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSGbn.Name = "lblSGbn"
        Me.lblSGbn.Size = New System.Drawing.Size(80, 21)
        Me.lblSGbn.TabIndex = 215
        Me.lblSGbn.Text = "조회구분"
        Me.lblSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnTExcel
        '
        Me.btnTExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTExcel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnTExcel.ColorFillBlend = CBlendItems1
        Me.btnTExcel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnTExcel.Corners.All = CType(6, Short)
        Me.btnTExcel.Corners.LowerLeft = CType(6, Short)
        Me.btnTExcel.Corners.LowerRight = CType(6, Short)
        Me.btnTExcel.Corners.UpperLeft = CType(6, Short)
        Me.btnTExcel.Corners.UpperRight = CType(6, Short)
        Me.btnTExcel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnTExcel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnTExcel.FocalPoints.CenterPtX = 0.4672897!
        Me.btnTExcel.FocalPoints.CenterPtY = 0.16!
        Me.btnTExcel.FocalPoints.FocusPtX = 0.0!
        Me.btnTExcel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTExcel.FocusPtTracker = DesignerRectTracker2
        Me.btnTExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTExcel.ForeColor = System.Drawing.Color.White
        Me.btnTExcel.Image = Nothing
        Me.btnTExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTExcel.ImageIndex = 0
        Me.btnTExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnTExcel.Location = New System.Drawing.Point(573, 3)
        Me.btnTExcel.Name = "btnTExcel"
        Me.btnTExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnTExcel.SideImage = Nothing
        Me.btnTExcel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnTExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnTExcel.TabIndex = 187
        Me.btnTExcel.Text = "To Excel"
        Me.btnTExcel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnTExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems2
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4672897!
        Me.btnSearch.FocalPoints.CenterPtY = 0.16!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker4
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(681, 3)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 186
        Me.btnSearch.Text = "조   회(F6)"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems3
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.5408163!
        Me.btnExit.FocalPoints.CenterPtY = 0.6!
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
        Me.btnExit.Location = New System.Drawing.Point(897, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 184
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems4
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 0.4672897!
        Me.btnClear.FocalPoints.CenterPtY = 0.16!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker8
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(789, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 183
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlSList
        '
        Me.pnlSList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlSList.Controls.Add(Me.spdTopList)
        Me.pnlSList.Location = New System.Drawing.Point(4, 34)
        Me.pnlSList.Name = "pnlSList"
        Me.pnlSList.Size = New System.Drawing.Size(997, 493)
        Me.pnlSList.TabIndex = 242
        '
        'spdTopList
        '
        Me.spdTopList.DataSource = Nothing
        Me.spdTopList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdTopList.Location = New System.Drawing.Point(0, 0)
        Me.spdTopList.Margin = New System.Windows.Forms.Padding(1)
        Me.spdTopList.Name = "spdTopList"
        Me.spdTopList.OcxState = CType(resources.GetObject("spdTopList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTopList.Size = New System.Drawing.Size(997, 493)
        Me.spdTopList.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(4, 528)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 19)
        Me.PictureBox1.TabIndex = 244
        Me.PictureBox1.TabStop = False
        '
        'lbTxt
        '
        Me.lbTxt.AutoSize = True
        Me.lbTxt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbTxt.Location = New System.Drawing.Point(25, 531)
        Me.lbTxt.Margin = New System.Windows.Forms.Padding(1)
        Me.lbTxt.Name = "lbTxt"
        Me.lbTxt.Size = New System.Drawing.Size(106, 12)
        Me.lbTxt.TabIndex = 243
        Me.lbTxt.Text = "입고 혈액 리스트"
        '
        'pnlDetail
        '
        Me.pnlDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlDetail.Controls.Add(Me.spdDetail)
        Me.pnlDetail.Location = New System.Drawing.Point(4, 548)
        Me.pnlDetail.Margin = New System.Windows.Forms.Padding(1)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(997, 119)
        Me.pnlDetail.TabIndex = 245
        '
        'spdDetail
        '
        Me.spdDetail.DataSource = Nothing
        Me.spdDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdDetail.Location = New System.Drawing.Point(0, 0)
        Me.spdDetail.Margin = New System.Windows.Forms.Padding(1)
        Me.spdDetail.Name = "spdDetail"
        Me.spdDetail.OcxState = CType(resources.GetObject("spdDetail.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDetail.Size = New System.Drawing.Size(997, 119)
        Me.spdDetail.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnTExcel)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 674)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1005, 32)
        Me.Panel1.TabIndex = 246
        '
        'FGB19
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1005, 706)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlDetail)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lbTxt)
        Me.Controls.Add(Me.pnlSList)
        Me.Controls.Add(Me.pnlSearchGbn)
        Me.Controls.Add(Me.lblSGbn)
        Me.Controls.Add(Me.Label98)
        Me.Controls.Add(Me.dtpDate0)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FGB19"
        Me.Text = "혈액 입고/출고 월별 현황 조회"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSearchGbn.ResumeLayout(False)
        Me.pnlSList.ResumeLayout(False)
        CType(Me.spdTopList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlDetail.ResumeLayout(False)
        CType(Me.spdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlSearchGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoIn As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOut As System.Windows.Forms.RadioButton
    Friend WithEvents lblSGbn As System.Windows.Forms.Label
    Friend WithEvents btnTExcel As CButtonLib.CButton
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents pnlSList As System.Windows.Forms.Panel
    Friend WithEvents spdTopList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lbTxt As System.Windows.Forms.Label
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents spdDetail As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
