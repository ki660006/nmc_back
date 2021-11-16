<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB21
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
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB21))
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
        Dim DesignerRectTracker9 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems5 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker10 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.btnExecute = New CButtonLib.CButton()
        Me.btnTExcel = New CButtonLib.CButton()
        Me.btnSearch = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.pnlSearchGbn = New System.Windows.Forms.Panel()
        Me.rdoComplete = New System.Windows.Forms.RadioButton()
        Me.rdoUnCom = New System.Windows.Forms.RadioButton()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.cboComCd = New System.Windows.Forms.ComboBox()
        Me.lblComcd = New System.Windows.Forms.Label()
        Me.txtPatNm = New System.Windows.Forms.TextBox()
        Me.lblSGbn = New System.Windows.Forms.Label()
        Me.btnPatPop = New System.Windows.Forms.Button()
        Me.txtRegno = New System.Windows.Forms.TextBox()
        Me.AxTnsPatinfo1 = New AxAckPatientInfo.AxTnsPatinfo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlList = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3.SuspendLayout()
        Me.pnlSearchGbn.SuspendLayout()
        Me.pnlList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExecute.ColorFillBlend = CBlendItems1
        Me.btnExecute.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExecute.Corners.All = CType(6, Short)
        Me.btnExecute.Corners.LowerLeft = CType(6, Short)
        Me.btnExecute.Corners.LowerRight = CType(6, Short)
        Me.btnExecute.Corners.UpperLeft = CType(6, Short)
        Me.btnExecute.Corners.UpperRight = CType(6, Short)
        Me.btnExecute.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExecute.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExecute.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExecute.FocalPoints.CenterPtY = 0.16!
        Me.btnExecute.FocalPoints.FocusPtX = 0.0!
        Me.btnExecute.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.FocusPtTracker = DesignerRectTracker2
        Me.btnExecute.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.Image = Nothing
        Me.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExecute.ImageIndex = 0
        Me.btnExecute.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExecute.Location = New System.Drawing.Point(958, 2)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExecute.SideImage = Nothing
        Me.btnExecute.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExecute.Size = New System.Drawing.Size(107, 25)
        Me.btnExecute.TabIndex = 188
        Me.btnExecute.Tag = "availdt"
        Me.btnExecute.Text = "저  장(F7)"
        Me.btnExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExecute.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnTExcel
        '
        Me.btnTExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTExcel.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnTExcel.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTExcel.FocusPtTracker = DesignerRectTracker4
        Me.btnTExcel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTExcel.ForeColor = System.Drawing.Color.White
        Me.btnTExcel.Image = Nothing
        Me.btnTExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTExcel.ImageIndex = 0
        Me.btnTExcel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnTExcel.Location = New System.Drawing.Point(742, 2)
        Me.btnTExcel.Name = "btnTExcel"
        Me.btnTExcel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnTExcel.SideImage = Nothing
        Me.btnTExcel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnTExcel.Size = New System.Drawing.Size(107, 25)
        Me.btnTExcel.TabIndex = 187
        Me.btnTExcel.Text = "To Excel"
        Me.btnTExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnTExcel.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems3
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
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker6
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(850, 2)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnSearch.SideImage = Nothing
        Me.btnSearch.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnSearch.Size = New System.Drawing.Size(107, 25)
        Me.btnSearch.TabIndex = 186
        Me.btnSearch.Text = "조   회(F6)"
        Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnSearch.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.4725275!
        Me.btnExit.FocalPoints.CenterPtY = 0.64!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1174, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 184
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems5
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
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker10
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1066, 2)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 183
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label98)
        Me.Panel3.Controls.Add(Me.dtpDate1)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.dtpDate0)
        Me.Panel3.Controls.Add(Me.pnlSearchGbn)
        Me.Panel3.Controls.Add(Me.Label99)
        Me.Panel3.Controls.Add(Me.cboComCd)
        Me.Panel3.Controls.Add(Me.lblComcd)
        Me.Panel3.Controls.Add(Me.txtPatNm)
        Me.Panel3.Controls.Add(Me.lblSGbn)
        Me.Panel3.Controls.Add(Me.btnPatPop)
        Me.Panel3.Controls.Add(Me.txtRegno)
        Me.Panel3.Location = New System.Drawing.Point(3, 2)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(292, 140)
        Me.Panel3.TabIndex = 243
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label98.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.ForeColor = System.Drawing.Color.White
        Me.Label98.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label98.Location = New System.Drawing.Point(3, 3)
        Me.Label98.Margin = New System.Windows.Forms.Padding(1)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(80, 21)
        Me.Label98.TabIndex = 100
        Me.Label98.Text = "접수일자"
        Me.Label98.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(200, 3)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate1.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(179, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 12)
        Me.Label4.TabIndex = 114
        Me.Label4.Text = "~"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(84, 3)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate0.TabIndex = 1
        '
        'pnlSearchGbn
        '
        Me.pnlSearchGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchGbn.Controls.Add(Me.rdoComplete)
        Me.pnlSearchGbn.Controls.Add(Me.rdoUnCom)
        Me.pnlSearchGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlSearchGbn.Location = New System.Drawing.Point(84, 69)
        Me.pnlSearchGbn.Name = "pnlSearchGbn"
        Me.pnlSearchGbn.Size = New System.Drawing.Size(204, 22)
        Me.pnlSearchGbn.TabIndex = 184
        '
        'rdoComplete
        '
        Me.rdoComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoComplete.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoComplete.Location = New System.Drawing.Point(85, 2)
        Me.rdoComplete.Name = "rdoComplete"
        Me.rdoComplete.Size = New System.Drawing.Size(80, 18)
        Me.rdoComplete.TabIndex = 7
        Me.rdoComplete.Tag = "1"
        Me.rdoComplete.Text = "결과완료"
        Me.rdoComplete.UseCompatibleTextRendering = True
        '
        'rdoUnCom
        '
        Me.rdoUnCom.Checked = True
        Me.rdoUnCom.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUnCom.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoUnCom.Location = New System.Drawing.Point(5, 3)
        Me.rdoUnCom.Name = "rdoUnCom"
        Me.rdoUnCom.Size = New System.Drawing.Size(67, 18)
        Me.rdoUnCom.TabIndex = 6
        Me.rdoUnCom.TabStop = True
        Me.rdoUnCom.Tag = "1"
        Me.rdoUnCom.Text = "미결과"
        Me.rdoUnCom.UseCompatibleTextRendering = True
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label99.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label99.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label99.ForeColor = System.Drawing.Color.White
        Me.Label99.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label99.Location = New System.Drawing.Point(3, 25)
        Me.Label99.Margin = New System.Windows.Forms.Padding(1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(80, 21)
        Me.Label99.TabIndex = 115
        Me.Label99.Text = "등록번호"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboComCd
        '
        Me.cboComCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboComCd.FormattingEnabled = True
        Me.cboComCd.Location = New System.Drawing.Point(84, 47)
        Me.cboComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboComCd.MaxDropDownItems = 20
        Me.cboComCd.Name = "cboComCd"
        Me.cboComCd.Size = New System.Drawing.Size(204, 20)
        Me.cboComCd.TabIndex = 1
        '
        'lblComcd
        '
        Me.lblComcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComcd.ForeColor = System.Drawing.Color.White
        Me.lblComcd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComcd.Location = New System.Drawing.Point(3, 47)
        Me.lblComcd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComcd.Name = "lblComcd"
        Me.lblComcd.Size = New System.Drawing.Size(80, 21)
        Me.lblComcd.TabIndex = 116
        Me.lblComcd.Text = "성분제제"
        Me.lblComcd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.SystemColors.Window
        Me.txtPatNm.Location = New System.Drawing.Point(172, 25)
        Me.txtPatNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPatNm.MaxLength = 50
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(116, 21)
        Me.txtPatNm.TabIndex = 182
        '
        'lblSGbn
        '
        Me.lblSGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSGbn.ForeColor = System.Drawing.Color.White
        Me.lblSGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSGbn.Location = New System.Drawing.Point(3, 69)
        Me.lblSGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSGbn.Name = "lblSGbn"
        Me.lblSGbn.Size = New System.Drawing.Size(80, 21)
        Me.lblSGbn.TabIndex = 117
        Me.lblSGbn.Text = "조회구분"
        Me.lblSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPatPop
        '
        Me.btnPatPop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPatPop.Image = CType(resources.GetObject("btnPatPop.Image"), System.Drawing.Image)
        Me.btnPatPop.Location = New System.Drawing.Point(150, 25)
        Me.btnPatPop.Name = "btnPatPop"
        Me.btnPatPop.Size = New System.Drawing.Size(21, 21)
        Me.btnPatPop.TabIndex = 181
        Me.btnPatPop.UseVisualStyleBackColor = True
        '
        'txtRegno
        '
        Me.txtRegno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegno.Location = New System.Drawing.Point(84, 25)
        Me.txtRegno.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegno.MaxLength = 8
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(65, 21)
        Me.txtRegno.TabIndex = 0
        '
        'AxTnsPatinfo1
        '
        Me.AxTnsPatinfo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxTnsPatinfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AxTnsPatinfo1.Location = New System.Drawing.Point(294, -4)
        Me.AxTnsPatinfo1.Margin = New System.Windows.Forms.Padding(1)
        Me.AxTnsPatinfo1.Name = "AxTnsPatinfo1"
        Me.AxTnsPatinfo1.Size = New System.Drawing.Size(992, 168)
        Me.AxTnsPatinfo1.TabIndex = 244
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(4, 159)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1284, 9)
        Me.Label1.TabIndex = 245
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'pnlList
        '
        Me.pnlList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlList.Controls.Add(Me.spdList)
        Me.pnlList.Location = New System.Drawing.Point(3, 171)
        Me.pnlList.Margin = New System.Windows.Forms.Padding(1)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(1277, 663)
        Me.pnlList.TabIndex = 246
        '
        'spdList
        '
        'Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1277, 663)
        Me.spdList.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnExecute)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnTExcel)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 843)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1284, 32)
        Me.Panel1.TabIndex = 247
        '
        'FGB21
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1284, 875)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlList)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.AxTnsPatinfo1)
        Me.Controls.Add(Me.Label1)
        Me.KeyPreview = True
        Me.Name = "FGB21"
        Me.Text = "CrossMating 결과 수정"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.pnlSearchGbn.ResumeLayout(False)
        Me.pnlList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnTExcel As CButtonLib.CButton
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlSearchGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoComplete As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUnCom As System.Windows.Forms.RadioButton
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents cboComCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblComcd As System.Windows.Forms.Label
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents lblSGbn As System.Windows.Forms.Label
    Friend WithEvents btnPatPop As System.Windows.Forms.Button
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents AxTnsPatinfo1 As AxAckPatientInfo.AxTnsPatinfo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExecute As CButtonLib.CButton
    Friend WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
