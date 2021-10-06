<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR05
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR05))
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
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnRst_ocs = New System.Windows.Forms.Button()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnFN = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.grpQryInfo = New System.Windows.Forms.GroupBox()
        Me.lblDptOrWard = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDptOrWard = New System.Windows.Forms.ComboBox()
        Me.cboTest = New System.Windows.Forms.ComboBox()
        Me.txtPatNm = New System.Windows.Forms.TextBox()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.lblRegno = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.lblPatnm = New System.Windows.Forms.Label()
        Me.lblOrdDt = New System.Windows.Forms.Label()
        Me.btnQuery = New CButtonLib.CButton()
        Me.lblLineQry = New System.Windows.Forms.Label()
        Me.AxPatInfo = New AxAckResult.AxRstPatInfo()
        Me.lblLine = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboRstFlg = New System.Windows.Forms.ComboBox()
        Me.axResult = New AxAckResult.AxRstInput_poct()
        Me.Panel3.SuspendLayout()
        Me.grpQryInfo.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.btnRst_ocs)
        Me.Panel3.Controls.Add(Me.txtID)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Controls.Add(Me.btnFN)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 640)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1343, 32)
        Me.Panel3.TabIndex = 163
        '
        'btnRst_ocs
        '
        Me.btnRst_ocs.Location = New System.Drawing.Point(1, 2)
        Me.btnRst_ocs.Name = "btnRst_ocs"
        Me.btnRst_ocs.Size = New System.Drawing.Size(75, 26)
        Me.btnRst_ocs.TabIndex = 30
        Me.btnRst_ocs.TabStop = False
        Me.btnRst_ocs.Text = "OCS"
        Me.btnRst_ocs.UseVisualStyleBackColor = True
        Me.btnRst_ocs.Visible = False
        '
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(236, 4)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(116, 21)
        Me.txtID.TabIndex = 153
        Me.txtID.Text = "ACK"
        Me.txtID.Visible = False
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
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1242, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(93, 25)
        Me.btnExit.TabIndex = 37
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnFN
        '
        Me.btnFN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnFN.ColorFillBlend = CBlendItems2
        Me.btnFN.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnFN.Corners.All = CType(6, Short)
        Me.btnFN.Corners.LowerLeft = CType(6, Short)
        Me.btnFN.Corners.LowerRight = CType(6, Short)
        Me.btnFN.Corners.UpperLeft = CType(6, Short)
        Me.btnFN.Corners.UpperRight = CType(6, Short)
        Me.btnFN.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnFN.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnFN.FocalPoints.CenterPtX = 0.5!
        Me.btnFN.FocalPoints.CenterPtY = 0.08!
        Me.btnFN.FocalPoints.FocusPtX = 0.0!
        Me.btnFN.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnFN.FocusPtTracker = DesignerRectTracker4
        Me.btnFN.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnFN.ForeColor = System.Drawing.Color.White
        Me.btnFN.Image = Nothing
        Me.btnFN.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnFN.ImageIndex = 0
        Me.btnFN.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnFN.Location = New System.Drawing.Point(1047, 3)
        Me.btnFN.Name = "btnFN"
        Me.btnFN.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnFN.SideImage = Nothing
        Me.btnFN.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnFN.Size = New System.Drawing.Size(96, 25)
        Me.btnFN.TabIndex = 33
        Me.btnFN.Text = "결과저장(F12)"
        Me.btnFN.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnFN.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1144, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(97, 25)
        Me.btnClear.TabIndex = 36
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'grpQryInfo
        '
        Me.grpQryInfo.BackColor = System.Drawing.Color.Transparent
        Me.grpQryInfo.Controls.Add(Me.lblDptOrWard)
        Me.grpQryInfo.Controls.Add(Me.Label1)
        Me.grpQryInfo.Controls.Add(Me.cboDptOrWard)
        Me.grpQryInfo.Controls.Add(Me.cboTest)
        Me.grpQryInfo.Controls.Add(Me.txtPatNm)
        Me.grpQryInfo.Controls.Add(Me.txtRegNo)
        Me.grpQryInfo.Controls.Add(Me.lblRegno)
        Me.grpQryInfo.Controls.Add(Me.lblDate)
        Me.grpQryInfo.Controls.Add(Me.dtpDateE)
        Me.grpQryInfo.Controls.Add(Me.dtpDateS)
        Me.grpQryInfo.Controls.Add(Me.lblPatnm)
        Me.grpQryInfo.Controls.Add(Me.lblOrdDt)
        Me.grpQryInfo.Location = New System.Drawing.Point(4, -4)
        Me.grpQryInfo.Name = "grpQryInfo"
        Me.grpQryInfo.Size = New System.Drawing.Size(273, 124)
        Me.grpQryInfo.TabIndex = 164
        Me.grpQryInfo.TabStop = False
        '
        'lblDptOrWard
        '
        Me.lblDptOrWard.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDptOrWard.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDptOrWard.ForeColor = System.Drawing.Color.Black
        Me.lblDptOrWard.Location = New System.Drawing.Point(3, 78)
        Me.lblDptOrWard.Name = "lblDptOrWard"
        Me.lblDptOrWard.Size = New System.Drawing.Size(79, 21)
        Me.lblDptOrWard.TabIndex = 66
        Me.lblDptOrWard.Text = "병    동"
        Me.lblDptOrWard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 21)
        Me.Label1.TabIndex = 71
        Me.Label1.Text = "검사항목"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDptOrWard
        '
        Me.cboDptOrWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDptOrWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDptOrWard.FormattingEnabled = True
        Me.cboDptOrWard.Items.AddRange(New Object() {"EN", "EM", "GS", "CP", "DEP", ""})
        Me.cboDptOrWard.Location = New System.Drawing.Point(83, 78)
        Me.cboDptOrWard.Name = "cboDptOrWard"
        Me.cboDptOrWard.Size = New System.Drawing.Size(185, 20)
        Me.cboDptOrWard.TabIndex = 8
        '
        'cboTest
        '
        Me.cboTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTest.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTest.FormattingEnabled = True
        Me.cboTest.ItemHeight = 12
        Me.cboTest.Items.AddRange(New Object() {"[A] 전체", "[3] 완료", "[2] Review", "[1] 검사", "[0] 미결과"})
        Me.cboTest.Location = New System.Drawing.Point(83, 100)
        Me.cboTest.Name = "cboTest"
        Me.cboTest.Size = New System.Drawing.Size(185, 20)
        Me.cboTest.TabIndex = 72
        '
        'txtPatNm
        '
        Me.txtPatNm.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtPatNm.Location = New System.Drawing.Point(83, 34)
        Me.txtPatNm.MaxLength = 8
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.Size = New System.Drawing.Size(186, 21)
        Me.txtPatNm.TabIndex = 1
        Me.txtPatNm.Tag = "PATNM"
        '
        'txtRegNo
        '
        Me.txtRegNo.Location = New System.Drawing.Point(83, 12)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(186, 21)
        Me.txtRegNo.TabIndex = 0
        Me.txtRegNo.Tag = "REGNO"
        '
        'lblRegno
        '
        Me.lblRegno.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegno.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegno.ForeColor = System.Drawing.Color.White
        Me.lblRegno.Location = New System.Drawing.Point(3, 12)
        Me.lblRegno.Name = "lblRegno"
        Me.lblRegno.Size = New System.Drawing.Size(79, 21)
        Me.lblRegno.TabIndex = 30
        Me.lblRegno.Text = "등록번호"
        Me.lblRegno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(169, 59)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(14, 12)
        Me.lblDate.TabIndex = 29
        Me.lblDate.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(183, 56)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(86, 21)
        Me.dtpDateE.TabIndex = 3
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(83, 56)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(86, 21)
        Me.dtpDateS.TabIndex = 2
        '
        'lblPatnm
        '
        Me.lblPatnm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPatnm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatnm.ForeColor = System.Drawing.Color.White
        Me.lblPatnm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblPatnm.Location = New System.Drawing.Point(3, 34)
        Me.lblPatnm.Margin = New System.Windows.Forms.Padding(3)
        Me.lblPatnm.Name = "lblPatnm"
        Me.lblPatnm.Size = New System.Drawing.Size(79, 21)
        Me.lblPatnm.TabIndex = 24
        Me.lblPatnm.Tag = "성명"
        Me.lblPatnm.Text = "성    명"
        Me.lblPatnm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.Color.Black
        Me.lblOrdDt.Location = New System.Drawing.Point(3, 56)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(79, 21)
        Me.lblOrdDt.TabIndex = 23
        Me.lblOrdDt.Text = "처방일자"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems4.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems4
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.9375!
        Me.btnQuery.FocalPoints.CenterPtY = 0.952381!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker8
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(213, 11)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(55, 24)
        Me.btnQuery.TabIndex = 68
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblLineQry
        '
        Me.lblLineQry.ForeColor = System.Drawing.Color.Gray
        Me.lblLineQry.Location = New System.Drawing.Point(6, 119)
        Me.lblLineQry.Name = "lblLineQry"
        Me.lblLineQry.Size = New System.Drawing.Size(275, 8)
        Me.lblLineQry.TabIndex = 165
        Me.lblLineQry.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'AxPatInfo
        '
        Me.AxPatInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AxPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AxPatInfo.BcNo = ""
        Me.AxPatInfo.Location = New System.Drawing.Point(281, 4)
        Me.AxPatInfo.Name = "AxPatInfo"
        Me.AxPatInfo.RegNo = ""
        Me.AxPatInfo.Size = New System.Drawing.Size(1060, 116)
        Me.AxPatInfo.SlipCd = ""
        Me.AxPatInfo.TabIndex = 166
        Me.AxPatInfo.TabStop = False
        '
        'lblLine
        '
        Me.lblLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLine.ForeColor = System.Drawing.Color.Gray
        Me.lblLine.Location = New System.Drawing.Point(282, 119)
        Me.lblLine.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(1069, 10)
        Me.lblLine.TabIndex = 167
        Me.lblLine.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.spdList)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cboRstFlg)
        Me.GroupBox1.Controls.Add(Me.btnQuery)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 122)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(273, 514)
        Me.GroupBox1.TabIndex = 168
        Me.GroupBox1.TabStop = False
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(3, 37)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(264, 472)
        Me.spdList.TabIndex = 73
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(3, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 21)
        Me.Label3.TabIndex = 69
        Me.Label3.Text = "검사상태"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboRstFlg
        '
        Me.cboRstFlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRstFlg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRstFlg.FormattingEnabled = True
        Me.cboRstFlg.ItemHeight = 12
        Me.cboRstFlg.Items.AddRange(New Object() {"[A] 전체", "[3] 완료", "[0] 미결과"})
        Me.cboRstFlg.Location = New System.Drawing.Point(76, 13)
        Me.cboRstFlg.Name = "cboRstFlg"
        Me.cboRstFlg.Size = New System.Drawing.Size(130, 20)
        Me.cboRstFlg.TabIndex = 70
        '
        'axResult
        '
        Me.axResult.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axResult.ColHiddenYn = False
        Me.axResult.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axResult.Location = New System.Drawing.Point(281, 130)
        Me.axResult.Name = "axResult"
        Me.axResult.Size = New System.Drawing.Size(1056, 503)
        Me.axResult.TabIndex = 169
        '
        'FGR05
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1343, 672)
        Me.Controls.Add(Me.axResult)
        Me.Controls.Add(Me.AxPatInfo)
        Me.Controls.Add(Me.grpQryInfo)
        Me.Controls.Add(Me.lblLineQry)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.lblLine)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FGR05"
        Me.Text = "현장검사 결과등록"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.grpQryInfo.ResumeLayout(False)
        Me.grpQryInfo.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnRst_ocs As System.Windows.Forms.Button
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnFN As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents grpQryInfo As System.Windows.Forms.GroupBox
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents lblDptOrWard As System.Windows.Forms.Label
    Friend WithEvents cboDptOrWard As System.Windows.Forms.ComboBox
    Protected Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents lblRegno As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Protected Friend WithEvents lblPatnm As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents lblLineQry As System.Windows.Forms.Label
    Friend WithEvents AxPatInfo As AxAckResult.AxRstPatInfo
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboTest As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboRstFlg As System.Windows.Forms.ComboBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents axResult As AxAckResult.AxRstInput_poct
End Class
