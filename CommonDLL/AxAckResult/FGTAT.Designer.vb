<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGTAT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGTAT))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblWard_SR = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblDeptNm = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lblIdNo = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblSexAge = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lblPatNm = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.lblTkID = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.lblCollectDt = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblCollectID = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblOrdDt = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblRegNo = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDoctor = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTkDt = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSpcNm = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblBCNO = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtCmtCd = New System.Windows.Forms.TextBox()
        Me.cboCmtCont = New System.Windows.Forms.ComboBox()
        Me.txtCmtCont = New System.Windows.Forms.TextBox()
        Me.lblCancelDesc = New System.Windows.Forms.Label()
        Me.txtTatCont = New System.Windows.Forms.TextBox()
        Me.pnlBottom.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 471)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1011, 34)
        Me.pnlBottom.TabIndex = 6
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems1
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.4740741!
        Me.btnReg.FocalPoints.CenterPtY = 0.28!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker2
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(796, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(116, 25)
        Me.btnReg.TabIndex = 186
        Me.btnReg.Text = "TAT 사유 등록(F5)"
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems2
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 0.4725275!
        Me.btnExit.FocalPoints.CenterPtY = 0.44!
        Me.btnExit.FocalPoints.FocusPtX = 0.0!
        Me.btnExit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker4
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(913, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(90, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "닫기(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(10, 11)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(559, 333)
        Me.spdList.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.lblWard_SR)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.lblDeptNm)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.lblIdNo)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.lblSexAge)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.lblPatNm)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.lblTkID)
        Me.GroupBox3.Controls.Add(Me.Label20)
        Me.GroupBox3.Controls.Add(Me.lblCollectDt)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.lblCollectID)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.lblOrdDt)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.lblRegNo)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblDoctor)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblTkDt)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.lblSpcNm)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.lblBCNO)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(576, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(424, 227)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        '
        'lblWard_SR
        '
        Me.lblWard_SR.BackColor = System.Drawing.Color.White
        Me.lblWard_SR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWard_SR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard_SR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWard_SR.Location = New System.Drawing.Point(296, 106)
        Me.lblWard_SR.Name = "lblWard_SR"
        Me.lblWard_SR.Size = New System.Drawing.Size(120, 22)
        Me.lblWard_SR.TabIndex = 29
        Me.lblWard_SR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label30.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(210, 106)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(85, 22)
        Me.Label30.TabIndex = 28
        Me.Label30.Text = "병동/병실"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptNm
        '
        Me.lblDeptNm.BackColor = System.Drawing.Color.White
        Me.lblDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptNm.Location = New System.Drawing.Point(296, 83)
        Me.lblDeptNm.Name = "lblDeptNm"
        Me.lblDeptNm.Size = New System.Drawing.Size(120, 22)
        Me.lblDeptNm.TabIndex = 27
        Me.lblDeptNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label28.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(210, 83)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(85, 22)
        Me.Label28.TabIndex = 26
        Me.Label28.Text = "진료과"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.BackColor = System.Drawing.Color.White
        Me.lblIdNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblIdNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdNo.Location = New System.Drawing.Point(296, 152)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.Size = New System.Drawing.Size(120, 22)
        Me.lblIdNo.TabIndex = 25
        Me.lblIdNo.Text = "030405-123567"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(210, 152)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(85, 22)
        Me.Label26.TabIndex = 24
        Me.Label26.Text = "주민등록번호"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.White
        Me.lblSexAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSexAge.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSexAge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSexAge.Location = New System.Drawing.Point(89, 152)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(120, 22)
        Me.lblSexAge.TabIndex = 23
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label24.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(8, 152)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(80, 22)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "Sex/Age"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.White
        Me.lblPatNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPatNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPatNm.Location = New System.Drawing.Point(89, 129)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(120, 22)
        Me.lblPatNm.TabIndex = 21
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(8, 129)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 22)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "성명"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkID
        '
        Me.lblTkID.BackColor = System.Drawing.Color.White
        Me.lblTkID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkID.Location = New System.Drawing.Point(296, 198)
        Me.lblTkID.Name = "lblTkID"
        Me.lblTkID.Size = New System.Drawing.Size(120, 22)
        Me.lblTkID.TabIndex = 19
        Me.lblTkID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label20.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.White
        Me.Label20.Location = New System.Drawing.Point(210, 175)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(85, 22)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "채혈자"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollectDt
        '
        Me.lblCollectDt.BackColor = System.Drawing.Color.White
        Me.lblCollectDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollectDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollectDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollectDt.Location = New System.Drawing.Point(89, 175)
        Me.lblCollectDt.Name = "lblCollectDt"
        Me.lblCollectDt.Size = New System.Drawing.Size(120, 22)
        Me.lblCollectDt.TabIndex = 17
        Me.lblCollectDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label16.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(8, 198)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 22)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "접수일시"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollectID
        '
        Me.lblCollectID.BackColor = System.Drawing.Color.White
        Me.lblCollectID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCollectID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollectID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollectID.Location = New System.Drawing.Point(296, 175)
        Me.lblCollectID.Name = "lblCollectID"
        Me.lblCollectID.Size = New System.Drawing.Size(120, 22)
        Me.lblCollectID.TabIndex = 15
        Me.lblCollectID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label18.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.White
        Me.Label18.Location = New System.Drawing.Point(210, 198)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 22)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "접수자"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.White
        Me.lblOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrdDt.Location = New System.Drawing.Point(89, 83)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(120, 22)
        Me.lblOrdDt.TabIndex = 13
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(8, 83)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 22)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "처방일시"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.White
        Me.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegNo.Location = New System.Drawing.Point(296, 129)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(120, 22)
        Me.lblRegNo.TabIndex = 11
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(210, 129)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(85, 22)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "등록번호"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(4, 12)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(415, 22)
        Me.Label9.TabIndex = 9
        Me.Label9.Text = "환자 기본정보"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctor
        '
        Me.lblDoctor.BackColor = System.Drawing.Color.White
        Me.lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoctor.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoctor.Location = New System.Drawing.Point(89, 106)
        Me.lblDoctor.Name = "lblDoctor"
        Me.lblDoctor.Size = New System.Drawing.Size(120, 22)
        Me.lblDoctor.TabIndex = 8
        Me.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 22)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "의뢰의사"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkDt
        '
        Me.lblTkDt.BackColor = System.Drawing.Color.White
        Me.lblTkDt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTkDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkDt.Location = New System.Drawing.Point(89, 198)
        Me.lblTkDt.Name = "lblTkDt"
        Me.lblTkDt.Size = New System.Drawing.Size(120, 22)
        Me.lblTkDt.TabIndex = 6
        Me.lblTkDt.Text = "2003-01-01 15:00"
        Me.lblTkDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(8, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 22)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "채취일시"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.White
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSpcNm.Location = New System.Drawing.Point(89, 60)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(156, 22)
        Me.lblSpcNm.TabIndex = 4
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 22)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "검체명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBCNO
        '
        Me.lblBCNO.BackColor = System.Drawing.Color.White
        Me.lblBCNO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBCNO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBCNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBCNO.Location = New System.Drawing.Point(89, 37)
        Me.lblBCNO.Name = "lblBCNO"
        Me.lblBCNO.Size = New System.Drawing.Size(156, 22)
        Me.lblBCNO.TabIndex = 13
        Me.lblBCNO.Text = "20030902-A0-0001"
        Me.lblBCNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(8, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "검체번호"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label35
        '
        Me.Label35.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label35.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.White
        Me.Label35.Location = New System.Drawing.Point(576, 259)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(97, 20)
        Me.Label35.TabIndex = 12
        Me.Label35.Text = "TAT 코드"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCmtCd
        '
        Me.txtCmtCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCd.Location = New System.Drawing.Point(674, 259)
        Me.txtCmtCd.Multiline = True
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(72, 20)
        Me.txtCmtCd.TabIndex = 0
        '
        'cboCmtCont
        '
        Me.cboCmtCont.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCmtCont.BackColor = System.Drawing.SystemColors.Window
        Me.cboCmtCont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCmtCont.ItemHeight = 12
        Me.cboCmtCont.Location = New System.Drawing.Point(747, 259)
        Me.cboCmtCont.Name = "cboCmtCont"
        Me.cboCmtCont.Size = New System.Drawing.Size(252, 20)
        Me.cboCmtCont.TabIndex = 1
        '
        'txtCmtCont
        '
        Me.txtCmtCont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(576, 281)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(423, 180)
        Me.txtCmtCont.TabIndex = 2
        '
        'lblCancelDesc
        '
        Me.lblCancelDesc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancelDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCancelDesc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCancelDesc.ForeColor = System.Drawing.Color.White
        Me.lblCancelDesc.Location = New System.Drawing.Point(576, 236)
        Me.lblCancelDesc.Name = "lblCancelDesc"
        Me.lblCancelDesc.Size = New System.Drawing.Size(424, 22)
        Me.lblCancelDesc.TabIndex = 8
        Me.lblCancelDesc.Text = "TAT 사유"
        Me.lblCancelDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTatCont
        '
        Me.txtTatCont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTatCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTatCont.Location = New System.Drawing.Point(10, 347)
        Me.txtTatCont.Multiline = True
        Me.txtTatCont.Name = "txtTatCont"
        Me.txtTatCont.Size = New System.Drawing.Size(560, 114)
        Me.txtTatCont.TabIndex = 13
        '
        'FGTAT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1011, 505)
        Me.Controls.Add(Me.txtTatCont)
        Me.Controls.Add(Me.txtCmtCd)
        Me.Controls.Add(Me.Label35)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.cboCmtCont)
        Me.Controls.Add(Me.spdList)
        Me.Controls.Add(Me.txtCmtCont)
        Me.Controls.Add(Me.lblCancelDesc)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGTAT"
        Me.Text = "TAT 사유 등록"
        Me.pnlBottom.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblWard_SR As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblDeptNm As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblIdNo As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblTkID As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lblCollectDt As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblCollectID As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblDoctor As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTkDt As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblBCNO As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents cboCmtCont As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    Friend WithEvents lblCancelDesc As System.Windows.Forms.Label
    Friend WithEvents txtTatCont As System.Windows.Forms.TextBox
End Class
