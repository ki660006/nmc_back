<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGPISC02
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGPISC02))
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
        Dim DesignerRectTracker11 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems6 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker12 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker13 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems7 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker14 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker15 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems8 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker16 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker17 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems9 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker18 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker19 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems10 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker20 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker21 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems11 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker22 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim StU_PatInfo1 As COMMON.SVar.STU_PatInfo = New COMMON.SVar.STU_PatInfo
        Me.grpQryInfo = New System.Windows.Forms.GroupBox
        Me.btnQuery = New CButtonLib.CButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.rdoIoGbnO = New System.Windows.Forms.RadioButton
        Me.rdoIoGbnI = New System.Windows.Forms.RadioButton
        Me.lblDptOrWard = New System.Windows.Forms.Label
        Me.cboDptOrWard = New System.Windows.Forms.ComboBox
        Me.txtPatNm = New System.Windows.Forms.TextBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.lblRegno = New System.Windows.Forms.Label
        Me.lblDate = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.pnlCollGbn = New System.Windows.Forms.Panel
        Me.rdoNoColl = New System.Windows.Forms.RadioButton
        Me.rdoAll = New System.Windows.Forms.RadioButton
        Me.rdoColl = New System.Windows.Forms.RadioButton
        Me.lblPatnm = New System.Windows.Forms.Label
        Me.lblOrdDt = New System.Windows.Forms.Label
        Me.lblIoGbn = New System.Windows.Forms.Label
        Me.lblCollGbn = New System.Windows.Forms.Label
        Me.lblLineQry = New System.Windows.Forms.Label
        Me.lblLine = New System.Windows.Forms.Label
        Me.axPatInfo = New AxAckPatientInfo.AxPatientInfo
        Me.grpList = New System.Windows.Forms.GroupBox
        Me.lblPatCount = New System.Windows.Forms.Label
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnQuery_Unfit = New CButtonLib.CButton
        Me.btnReg_Pat = New CButtonLib.CButton
        Me.pnlPrintInfo = New System.Windows.Forms.Panel
        Me.lblBarPrinter = New System.Windows.Forms.Label
        Me.btnPrint_Set = New System.Windows.Forms.Button
        Me.lblPrint = New System.Windows.Forms.Label
        Me.btnPrint_BC = New CButtonLib.CButton
        Me.btnPrint_Label = New CButtonLib.CButton
        Me.btnPrint_Doc = New CButtonLib.CButton
        Me.btnCancel_coll = New CButtonLib.CButton
        Me.lblOrder = New System.Windows.Forms.Label
        Me.btnReg_Coll = New CButtonLib.CButton
        Me.chkHopeDay = New System.Windows.Forms.CheckBox
        Me.axCollBcNos = New AxAckCollector.AxCollBcNos
        Me.axCollList = New AxAckCollector.AxCollList_pis
        Me.grpQryInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlCollGbn.SuspendLayout()
        Me.grpList.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.pnlPrintInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpQryInfo
        '
        Me.grpQryInfo.BackColor = System.Drawing.Color.Transparent
        Me.grpQryInfo.Controls.Add(Me.btnQuery)
        Me.grpQryInfo.Controls.Add(Me.Panel1)
        Me.grpQryInfo.Controls.Add(Me.lblDptOrWard)
        Me.grpQryInfo.Controls.Add(Me.cboDptOrWard)
        Me.grpQryInfo.Controls.Add(Me.txtPatNm)
        Me.grpQryInfo.Controls.Add(Me.txtRegNo)
        Me.grpQryInfo.Controls.Add(Me.lblRegno)
        Me.grpQryInfo.Controls.Add(Me.lblDate)
        Me.grpQryInfo.Controls.Add(Me.dtpDateE)
        Me.grpQryInfo.Controls.Add(Me.dtpDateS)
        Me.grpQryInfo.Controls.Add(Me.pnlCollGbn)
        Me.grpQryInfo.Controls.Add(Me.lblPatnm)
        Me.grpQryInfo.Controls.Add(Me.lblOrdDt)
        Me.grpQryInfo.Controls.Add(Me.lblIoGbn)
        Me.grpQryInfo.Controls.Add(Me.lblCollGbn)
        Me.grpQryInfo.Location = New System.Drawing.Point(3, -5)
        Me.grpQryInfo.Name = "grpQryInfo"
        Me.grpQryInfo.Size = New System.Drawing.Size(273, 152)
        Me.grpQryInfo.TabIndex = 68
        Me.grpQryInfo.TabStop = False
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnQuery.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems1.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems1
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 1.0!
        Me.btnQuery.FocalPoints.CenterPtY = 1.0!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker2
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(219, 104)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(48, 42)
        Me.btnQuery.TabIndex = 68
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.rdoIoGbnO)
        Me.Panel1.Controls.Add(Me.rdoIoGbnI)
        Me.Panel1.ForeColor = System.Drawing.Color.DarkGreen
        Me.Panel1.Location = New System.Drawing.Point(83, 104)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(132, 22)
        Me.Panel1.TabIndex = 67
        '
        'rdoIoGbnO
        '
        Me.rdoIoGbnO.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnO.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoIoGbnO.ForeColor = System.Drawing.Color.Black
        Me.rdoIoGbnO.Location = New System.Drawing.Point(60, 2)
        Me.rdoIoGbnO.Name = "rdoIoGbnO"
        Me.rdoIoGbnO.Size = New System.Drawing.Size(61, 18)
        Me.rdoIoGbnO.TabIndex = 6
        Me.rdoIoGbnO.Tag = "0"
        Me.rdoIoGbnO.Text = "외래"
        Me.rdoIoGbnO.UseCompatibleTextRendering = True
        '
        'rdoIoGbnI
        '
        Me.rdoIoGbnI.Checked = True
        Me.rdoIoGbnI.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoIoGbnI.ForeColor = System.Drawing.Color.Black
        Me.rdoIoGbnI.Location = New System.Drawing.Point(4, 3)
        Me.rdoIoGbnI.Name = "rdoIoGbnI"
        Me.rdoIoGbnI.Size = New System.Drawing.Size(46, 18)
        Me.rdoIoGbnI.TabIndex = 5
        Me.rdoIoGbnI.TabStop = True
        Me.rdoIoGbnI.Tag = "1"
        Me.rdoIoGbnI.Text = "입원"
        Me.rdoIoGbnI.UseCompatibleTextRendering = True
        '
        'lblDptOrWard
        '
        Me.lblDptOrWard.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDptOrWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDptOrWard.ForeColor = System.Drawing.Color.Black
        Me.lblDptOrWard.Location = New System.Drawing.Point(3, 126)
        Me.lblDptOrWard.Name = "lblDptOrWard"
        Me.lblDptOrWard.Size = New System.Drawing.Size(79, 21)
        Me.lblDptOrWard.TabIndex = 66
        Me.lblDptOrWard.Text = "병    동"
        Me.lblDptOrWard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboDptOrWard
        '
        Me.cboDptOrWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDptOrWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDptOrWard.FormattingEnabled = True
        Me.cboDptOrWard.Items.AddRange(New Object() {"EN", "EM", "GS", "CP", "DEP", ""})
        Me.cboDptOrWard.Location = New System.Drawing.Point(83, 126)
        Me.cboDptOrWard.Name = "cboDptOrWard"
        Me.cboDptOrWard.Size = New System.Drawing.Size(132, 20)
        Me.cboDptOrWard.TabIndex = 8
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
        Me.txtPatNm.Text = "나환자"
        '
        'txtRegNo
        '
        Me.txtRegNo.Location = New System.Drawing.Point(83, 12)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(186, 21)
        Me.txtRegNo.TabIndex = 0
        Me.txtRegNo.Tag = "REGNO"
        Me.txtRegNo.Text = "012345678"
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
        'pnlCollGbn
        '
        Me.pnlCollGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlCollGbn.Controls.Add(Me.rdoNoColl)
        Me.pnlCollGbn.Controls.Add(Me.rdoAll)
        Me.pnlCollGbn.Controls.Add(Me.rdoColl)
        Me.pnlCollGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlCollGbn.Location = New System.Drawing.Point(83, 79)
        Me.pnlCollGbn.Name = "pnlCollGbn"
        Me.pnlCollGbn.Size = New System.Drawing.Size(186, 22)
        Me.pnlCollGbn.TabIndex = 25
        '
        'rdoNoColl
        '
        Me.rdoNoColl.Checked = True
        Me.rdoNoColl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoNoColl.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoNoColl.ForeColor = System.Drawing.Color.Black
        Me.rdoNoColl.Location = New System.Drawing.Point(117, 3)
        Me.rdoNoColl.Name = "rdoNoColl"
        Me.rdoNoColl.Size = New System.Drawing.Size(61, 18)
        Me.rdoNoColl.TabIndex = 6
        Me.rdoNoColl.TabStop = True
        Me.rdoNoColl.Tag = "0"
        Me.rdoNoColl.Text = "미채혈"
        Me.rdoNoColl.UseCompatibleTextRendering = True
        '
        'rdoAll
        '
        Me.rdoAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAll.ForeColor = System.Drawing.Color.Black
        Me.rdoAll.Location = New System.Drawing.Point(4, 3)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(46, 18)
        Me.rdoAll.TabIndex = 4
        Me.rdoAll.Tag = "1"
        Me.rdoAll.Text = "전체"
        Me.rdoAll.UseCompatibleTextRendering = True
        '
        'rdoColl
        '
        Me.rdoColl.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoColl.ForeColor = System.Drawing.Color.Black
        Me.rdoColl.Location = New System.Drawing.Point(61, 3)
        Me.rdoColl.Name = "rdoColl"
        Me.rdoColl.Size = New System.Drawing.Size(46, 18)
        Me.rdoColl.TabIndex = 5
        Me.rdoColl.Tag = "1"
        Me.rdoColl.Text = "채혈"
        Me.rdoColl.UseCompatibleTextRendering = True
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
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.Color.Black
        Me.lblOrdDt.Location = New System.Drawing.Point(3, 56)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(79, 21)
        Me.lblOrdDt.TabIndex = 23
        Me.lblOrdDt.Text = "처방일자"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIoGbn
        '
        Me.lblIoGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblIoGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIoGbn.ForeColor = System.Drawing.Color.Black
        Me.lblIoGbn.Location = New System.Drawing.Point(3, 103)
        Me.lblIoGbn.Name = "lblIoGbn"
        Me.lblIoGbn.Size = New System.Drawing.Size(79, 21)
        Me.lblIoGbn.TabIndex = 39
        Me.lblIoGbn.Text = "입외구분"
        Me.lblIoGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollGbn
        '
        Me.lblCollGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblCollGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollGbn.ForeColor = System.Drawing.Color.Black
        Me.lblCollGbn.Location = New System.Drawing.Point(3, 79)
        Me.lblCollGbn.Name = "lblCollGbn"
        Me.lblCollGbn.Size = New System.Drawing.Size(79, 22)
        Me.lblCollGbn.TabIndex = 26
        Me.lblCollGbn.Text = "채혈구분"
        Me.lblCollGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLineQry
        '
        Me.lblLineQry.ForeColor = System.Drawing.Color.Gray
        Me.lblLineQry.Location = New System.Drawing.Point(6, 145)
        Me.lblLineQry.Name = "lblLineQry"
        Me.lblLineQry.Size = New System.Drawing.Size(272, 8)
        Me.lblLineQry.TabIndex = 69
        Me.lblLineQry.Text = "━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'lblLine
        '
        Me.lblLine.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLine.ForeColor = System.Drawing.Color.Gray
        Me.lblLine.Location = New System.Drawing.Point(279, 145)
        Me.lblLine.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLine.Name = "lblLine"
        Me.lblLine.Size = New System.Drawing.Size(890, 10)
        Me.lblLine.TabIndex = 70
        Me.lblLine.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'axPatInfo
        '
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axPatInfo.Location = New System.Drawing.Point(281, 4)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.Size = New System.Drawing.Size(878, 143)
        Me.axPatInfo.TabIndex = 71
        '
        'grpList
        '
        Me.grpList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpList.Controls.Add(Me.lblPatCount)
        Me.grpList.Controls.Add(Me.spdList)
        Me.grpList.Location = New System.Drawing.Point(3, 148)
        Me.grpList.Name = "grpList"
        Me.grpList.Size = New System.Drawing.Size(272, 405)
        Me.grpList.TabIndex = 72
        Me.grpList.TabStop = False
        '
        'lblPatCount
        '
        Me.lblPatCount.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatCount.Location = New System.Drawing.Point(5, 10)
        Me.lblPatCount.Name = "lblPatCount"
        Me.lblPatCount.Size = New System.Drawing.Size(178, 18)
        Me.lblPatCount.TabIndex = 41
        Me.lblPatCount.Text = ">> 대상환자 건수 : 0 건"
        Me.lblPatCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'spdList
        '
        Me.spdList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdList.DataSource = Nothing
        Me.spdList.Location = New System.Drawing.Point(3, 31)
        Me.spdList.Margin = New System.Windows.Forms.Padding(0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(265, 371)
        Me.spdList.TabIndex = 9
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnQuery_Unfit)
        Me.pnlBottom.Controls.Add(Me.btnReg_Pat)
        Me.pnlBottom.Controls.Add(Me.pnlPrintInfo)
        Me.pnlBottom.Controls.Add(Me.btnPrint_BC)
        Me.pnlBottom.Controls.Add(Me.btnPrint_Label)
        Me.pnlBottom.Controls.Add(Me.btnPrint_Doc)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 555)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1162, 32)
        Me.pnlBottom.TabIndex = 73
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.5!
        Me.btnExit.FocalPoints.CenterPtY = 0.0!
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
        Me.btnExit.Location = New System.Drawing.Point(1041, 2)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(114, 25)
        Me.btnExit.TabIndex = 223
        Me.btnExit.Text = "종료(ESC)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
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
        Me.btnClear.Location = New System.Drawing.Point(925, 2)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(114, 25)
        Me.btnClear.TabIndex = 222
        Me.btnClear.Text = "화면정리"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems4
        Me.btnReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg.Corners.All = CType(6, Short)
        Me.btnReg.Corners.LowerLeft = CType(6, Short)
        Me.btnReg.Corners.LowerRight = CType(6, Short)
        Me.btnReg.Corners.UpperLeft = CType(6, Short)
        Me.btnReg.Corners.UpperRight = CType(6, Short)
        Me.btnReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg.FocalPoints.CenterPtX = 0.5!
        Me.btnReg.FocalPoints.CenterPtY = 0.0!
        Me.btnReg.FocalPoints.FocusPtX = 0.0!
        Me.btnReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker8
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(809, 2)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(114, 25)
        Me.btnReg.TabIndex = 220
        Me.btnReg.Text = "바코드 발행(F2)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery_Unfit
        '
        Me.btnQuery_Unfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_Unfit.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery_Unfit.ColorFillBlend = CBlendItems5
        Me.btnQuery_Unfit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery_Unfit.Corners.All = CType(6, Short)
        Me.btnQuery_Unfit.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery_Unfit.Corners.LowerRight = CType(6, Short)
        Me.btnQuery_Unfit.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery_Unfit.Corners.UpperRight = CType(6, Short)
        Me.btnQuery_Unfit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery_Unfit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery_Unfit.FocalPoints.CenterPtX = 0.5!
        Me.btnQuery_Unfit.FocalPoints.CenterPtY = 0.0!
        Me.btnQuery_Unfit.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery_Unfit.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery_Unfit.FocusPtTracker = DesignerRectTracker10
        Me.btnQuery_Unfit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery_Unfit.ForeColor = System.Drawing.Color.White
        Me.btnQuery_Unfit.Image = Nothing
        Me.btnQuery_Unfit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_Unfit.ImageIndex = 0
        Me.btnQuery_Unfit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery_Unfit.Location = New System.Drawing.Point(577, 2)
        Me.btnQuery_Unfit.Margin = New System.Windows.Forms.Padding(0)
        Me.btnQuery_Unfit.Name = "btnQuery_Unfit"
        Me.btnQuery_Unfit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery_Unfit.SideImage = Nothing
        Me.btnQuery_Unfit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnQuery_Unfit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery_Unfit.Size = New System.Drawing.Size(114, 25)
        Me.btnQuery_Unfit.TabIndex = 217
        Me.btnQuery_Unfit.Text = "부적합검체 조회"
        Me.btnQuery_Unfit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery_Unfit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery_Unfit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg_Pat
        '
        Me.btnReg_Pat.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker11.IsActive = False
        DesignerRectTracker11.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker11.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Pat.CenterPtTracker = DesignerRectTracker11
        CBlendItems6.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems6.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnReg_Pat.ColorFillBlend = CBlendItems6
        Me.btnReg_Pat.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_Pat.Corners.All = CType(6, Short)
        Me.btnReg_Pat.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_Pat.Corners.LowerRight = CType(6, Short)
        Me.btnReg_Pat.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_Pat.Corners.UpperRight = CType(6, Short)
        Me.btnReg_Pat.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_Pat.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_Pat.FocalPoints.CenterPtX = 0.5!
        Me.btnReg_Pat.FocalPoints.CenterPtY = 0.0!
        Me.btnReg_Pat.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_Pat.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker12.IsActive = False
        DesignerRectTracker12.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker12.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Pat.FocusPtTracker = DesignerRectTracker12
        Me.btnReg_Pat.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg_Pat.ForeColor = System.Drawing.Color.White
        Me.btnReg_Pat.Image = Nothing
        Me.btnReg_Pat.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Pat.ImageIndex = 0
        Me.btnReg_Pat.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_Pat.Location = New System.Drawing.Point(461, 2)
        Me.btnReg_Pat.Margin = New System.Windows.Forms.Padding(0)
        Me.btnReg_Pat.Name = "btnReg_Pat"
        Me.btnReg_Pat.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_Pat.SideImage = Nothing
        Me.btnReg_Pat.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_Pat.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_Pat.Size = New System.Drawing.Size(114, 25)
        Me.btnReg_Pat.TabIndex = 216
        Me.btnReg_Pat.Text = "특이사항등록"
        Me.btnReg_Pat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Pat.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_Pat.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlPrintInfo
        '
        Me.pnlPrintInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlPrintInfo.Controls.Add(Me.lblBarPrinter)
        Me.pnlPrintInfo.Controls.Add(Me.btnPrint_Set)
        Me.pnlPrintInfo.Controls.Add(Me.lblPrint)
        Me.pnlPrintInfo.Location = New System.Drawing.Point(3, 2)
        Me.pnlPrintInfo.Name = "pnlPrintInfo"
        Me.pnlPrintInfo.Size = New System.Drawing.Size(300, 25)
        Me.pnlPrintInfo.TabIndex = 179
        '
        'lblBarPrinter
        '
        Me.lblBarPrinter.BackColor = System.Drawing.Color.Lavender
        Me.lblBarPrinter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBarPrinter.ForeColor = System.Drawing.Color.Indigo
        Me.lblBarPrinter.Location = New System.Drawing.Point(82, 2)
        Me.lblBarPrinter.Name = "lblBarPrinter"
        Me.lblBarPrinter.Size = New System.Drawing.Size(191, 21)
        Me.lblBarPrinter.TabIndex = 181
        Me.lblBarPrinter.Text = "AUTO LABELER (외래채혈실)"
        Me.lblBarPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnPrint_Set
        '
        Me.btnPrint_Set.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPrint_Set.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPrint_Set.Image = CType(resources.GetObject("btnPrint_Set.Image"), System.Drawing.Image)
        Me.btnPrint_Set.Location = New System.Drawing.Point(273, 1)
        Me.btnPrint_Set.Name = "btnPrint_Set"
        Me.btnPrint_Set.Size = New System.Drawing.Size(26, 23)
        Me.btnPrint_Set.TabIndex = 180
        Me.btnPrint_Set.UseVisualStyleBackColor = True
        '
        'lblPrint
        '
        Me.lblPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblPrint.ForeColor = System.Drawing.Color.Black
        Me.lblPrint.Location = New System.Drawing.Point(2, 2)
        Me.lblPrint.Name = "lblPrint"
        Me.lblPrint.Size = New System.Drawing.Size(80, 21)
        Me.lblPrint.TabIndex = 178
        Me.lblPrint.Text = "바코드프린터"
        Me.lblPrint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnPrint_BC
        '
        Me.btnPrint_BC.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker13.IsActive = False
        DesignerRectTracker13.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker13.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_BC.CenterPtTracker = DesignerRectTracker13
        CBlendItems7.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems7.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint_BC.ColorFillBlend = CBlendItems7
        Me.btnPrint_BC.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint_BC.Corners.All = CType(6, Short)
        Me.btnPrint_BC.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint_BC.Corners.LowerRight = CType(6, Short)
        Me.btnPrint_BC.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint_BC.Corners.UpperRight = CType(6, Short)
        Me.btnPrint_BC.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint_BC.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint_BC.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint_BC.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint_BC.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint_BC.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker14.IsActive = False
        DesignerRectTracker14.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker14.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_BC.FocusPtTracker = DesignerRectTracker14
        Me.btnPrint_BC.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint_BC.ForeColor = System.Drawing.Color.White
        Me.btnPrint_BC.Image = Nothing
        Me.btnPrint_BC.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_BC.ImageIndex = 0
        Me.btnPrint_BC.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint_BC.Location = New System.Drawing.Point(809, 2)
        Me.btnPrint_BC.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint_BC.Name = "btnPrint_BC"
        Me.btnPrint_BC.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint_BC.SideImage = Nothing
        Me.btnPrint_BC.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint_BC.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint_BC.Size = New System.Drawing.Size(114, 25)
        Me.btnPrint_BC.TabIndex = 221
        Me.btnPrint_BC.Text = "바코드 재출력"
        Me.btnPrint_BC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_BC.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint_BC.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnPrint_BC.Visible = False
        '
        'btnPrint_Label
        '
        Me.btnPrint_Label.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker15.IsActive = False
        DesignerRectTracker15.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker15.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_Label.CenterPtTracker = DesignerRectTracker15
        CBlendItems8.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems8.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint_Label.ColorFillBlend = CBlendItems8
        Me.btnPrint_Label.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint_Label.Corners.All = CType(6, Short)
        Me.btnPrint_Label.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint_Label.Corners.LowerRight = CType(6, Short)
        Me.btnPrint_Label.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint_Label.Corners.UpperRight = CType(6, Short)
        Me.btnPrint_Label.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint_Label.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint_Label.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint_Label.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint_Label.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint_Label.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker16.IsActive = False
        DesignerRectTracker16.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker16.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_Label.FocusPtTracker = DesignerRectTracker16
        Me.btnPrint_Label.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint_Label.ForeColor = System.Drawing.Color.White
        Me.btnPrint_Label.Image = Nothing
        Me.btnPrint_Label.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_Label.ImageIndex = 0
        Me.btnPrint_Label.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint_Label.Location = New System.Drawing.Point(693, 2)
        Me.btnPrint_Label.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint_Label.Name = "btnPrint_Label"
        Me.btnPrint_Label.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint_Label.SideImage = Nothing
        Me.btnPrint_Label.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint_Label.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint_Label.Size = New System.Drawing.Size(114, 25)
        Me.btnPrint_Label.TabIndex = 219
        Me.btnPrint_Label.Text = "라벨출력"
        Me.btnPrint_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_Label.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint_Label.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnPrint_Doc
        '
        Me.btnPrint_Doc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker17.IsActive = False
        DesignerRectTracker17.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker17.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_Doc.CenterPtTracker = DesignerRectTracker17
        CBlendItems9.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems9.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint_Doc.ColorFillBlend = CBlendItems9
        Me.btnPrint_Doc.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint_Doc.Corners.All = CType(6, Short)
        Me.btnPrint_Doc.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint_Doc.Corners.LowerRight = CType(6, Short)
        Me.btnPrint_Doc.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint_Doc.Corners.UpperRight = CType(6, Short)
        Me.btnPrint_Doc.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint_Doc.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint_Doc.FocalPoints.CenterPtX = 0.5!
        Me.btnPrint_Doc.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint_Doc.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint_Doc.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker18.IsActive = False
        DesignerRectTracker18.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker18.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint_Doc.FocusPtTracker = DesignerRectTracker18
        Me.btnPrint_Doc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint_Doc.ForeColor = System.Drawing.Color.White
        Me.btnPrint_Doc.Image = Nothing
        Me.btnPrint_Doc.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_Doc.ImageIndex = 0
        Me.btnPrint_Doc.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint_Doc.Location = New System.Drawing.Point(693, 2)
        Me.btnPrint_Doc.Margin = New System.Windows.Forms.Padding(0)
        Me.btnPrint_Doc.Name = "btnPrint_Doc"
        Me.btnPrint_Doc.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint_Doc.SideImage = Nothing
        Me.btnPrint_Doc.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint_Doc.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint_Doc.Size = New System.Drawing.Size(114, 25)
        Me.btnPrint_Doc.TabIndex = 218
        Me.btnPrint_Doc.Text = "문서 재출력"
        Me.btnPrint_Doc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint_Doc.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint_Doc.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnPrint_Doc.Visible = False
        '
        'btnCancel_coll
        '
        Me.btnCancel_coll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel_coll.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker19.IsActive = False
        DesignerRectTracker19.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker19.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel_coll.CenterPtTracker = DesignerRectTracker19
        CBlendItems10.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems10.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnCancel_coll.ColorFillBlend = CBlendItems10
        Me.btnCancel_coll.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCancel_coll.Corners.All = CType(6, Short)
        Me.btnCancel_coll.Corners.LowerLeft = CType(6, Short)
        Me.btnCancel_coll.Corners.LowerRight = CType(6, Short)
        Me.btnCancel_coll.Corners.UpperLeft = CType(6, Short)
        Me.btnCancel_coll.Corners.UpperRight = CType(6, Short)
        Me.btnCancel_coll.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCancel_coll.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCancel_coll.FocalPoints.CenterPtX = 0.5066667!
        Me.btnCancel_coll.FocalPoints.CenterPtY = 0.68!
        Me.btnCancel_coll.FocalPoints.FocusPtX = 0.0!
        Me.btnCancel_coll.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker20.IsActive = False
        DesignerRectTracker20.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker20.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel_coll.FocusPtTracker = DesignerRectTracker20
        Me.btnCancel_coll.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel_coll.ForeColor = System.Drawing.Color.Black
        Me.btnCancel_coll.Image = Nothing
        Me.btnCancel_coll.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel_coll.ImageIndex = 0
        Me.btnCancel_coll.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCancel_coll.Location = New System.Drawing.Point(1084, 153)
        Me.btnCancel_coll.Name = "btnCancel_coll"
        Me.btnCancel_coll.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCancel_coll.SideImage = Nothing
        Me.btnCancel_coll.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel_coll.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCancel_coll.Size = New System.Drawing.Size(75, 25)
        Me.btnCancel_coll.TabIndex = 75
        Me.btnCancel_coll.Text = "채혈취소"
        Me.btnCancel_coll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel_coll.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCancel_coll.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblOrder
        '
        Me.lblOrder.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrder.Location = New System.Drawing.Point(282, 157)
        Me.lblOrder.Name = "lblOrder"
        Me.lblOrder.Size = New System.Drawing.Size(134, 21)
        Me.lblOrder.TabIndex = 74
        Me.lblOrder.Text = ">> 처방상세 내역"
        Me.lblOrder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnReg_Coll
        '
        Me.btnReg_Coll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReg_Coll.BorderColor = System.Drawing.Color.DarkGray
        DesignerRectTracker21.IsActive = False
        DesignerRectTracker21.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker21.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Coll.CenterPtTracker = DesignerRectTracker21
        CBlendItems11.iColor = New System.Drawing.Color() {System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(240, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(255, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer)), System.Drawing.Color.Navy}
        CBlendItems11.iPoint = New Single() {0.0!, 0.8723404!, 0.9969605!, 1.0!}
        Me.btnReg_Coll.ColorFillBlend = CBlendItems11
        Me.btnReg_Coll.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnReg_Coll.Corners.All = CType(6, Short)
        Me.btnReg_Coll.Corners.LowerLeft = CType(6, Short)
        Me.btnReg_Coll.Corners.LowerRight = CType(6, Short)
        Me.btnReg_Coll.Corners.UpperLeft = CType(6, Short)
        Me.btnReg_Coll.Corners.UpperRight = CType(6, Short)
        Me.btnReg_Coll.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnReg_Coll.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnReg_Coll.FocalPoints.CenterPtX = 0.6739131!
        Me.btnReg_Coll.FocalPoints.CenterPtY = 0.48!
        Me.btnReg_Coll.FocalPoints.FocusPtX = 0.0!
        Me.btnReg_Coll.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker22.IsActive = False
        DesignerRectTracker22.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker22.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg_Coll.FocusPtTracker = DesignerRectTracker22
        Me.btnReg_Coll.Image = Nothing
        Me.btnReg_Coll.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Coll.ImageIndex = 0
        Me.btnReg_Coll.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg_Coll.Location = New System.Drawing.Point(990, 153)
        Me.btnReg_Coll.Name = "btnReg_Coll"
        Me.btnReg_Coll.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg_Coll.SideImage = Nothing
        Me.btnReg_Coll.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg_Coll.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg_Coll.Size = New System.Drawing.Size(92, 25)
        Me.btnReg_Coll.TabIndex = 81
        Me.btnReg_Coll.Text = "채혈일시 등록"
        Me.btnReg_Coll.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg_Coll.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg_Coll.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'chkHopeDay
        '
        Me.chkHopeDay.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkHopeDay.AutoSize = True
        Me.chkHopeDay.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkHopeDay.Location = New System.Drawing.Point(549, 158)
        Me.chkHopeDay.Name = "chkHopeDay"
        Me.chkHopeDay.Size = New System.Drawing.Size(157, 15)
        Me.chkHopeDay.TabIndex = 83
        Me.chkHopeDay.Text = "최근 희망일 기준 자동선택"
        Me.chkHopeDay.UseVisualStyleBackColor = True
        Me.chkHopeDay.Visible = False
        '
        'axCollBcNos
        '
        Me.axCollBcNos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axCollBcNos.BcPrinterParams = Nothing
        Me.axCollBcNos.Location = New System.Drawing.Point(278, 527)
        Me.axCollBcNos.Margin = New System.Windows.Forms.Padding(0)
        Me.axCollBcNos.Name = "axCollBcNos"
        Me.axCollBcNos.Size = New System.Drawing.Size(881, 26)
        Me.axCollBcNos.TabIndex = 84
        '
        'axCollList
        '
        Me.axCollList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axCollList.BcPrinterParams = Nothing
        Me.axCollList.CallForm = AxAckCollector.enumCollectCallForm.CollectIn
        Me.axCollList.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.axCollList.Location = New System.Drawing.Point(281, 179)
        Me.axCollList.Name = "axCollList"
        Me.axCollList.PatInfo = StU_PatInfo1
        Me.axCollList.Size = New System.Drawing.Size(876, 348)
        Me.axCollList.TabIndex = 85
        '
        'FGPISC02
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1162, 587)
        Me.Controls.Add(Me.axCollList)
        Me.Controls.Add(Me.axCollBcNos)
        Me.Controls.Add(Me.chkHopeDay)
        Me.Controls.Add(Me.btnReg_Coll)
        Me.Controls.Add(Me.btnCancel_coll)
        Me.Controls.Add(Me.lblOrder)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.axPatInfo)
        Me.Controls.Add(Me.lblLine)
        Me.Controls.Add(Me.grpQryInfo)
        Me.Controls.Add(Me.lblLineQry)
        Me.Controls.Add(Me.grpList)
        Me.KeyPreview = True
        Me.Name = "FGPISC02"
        Me.Text = "병동간호채혈"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.grpQryInfo.ResumeLayout(False)
        Me.grpQryInfo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlCollGbn.ResumeLayout(False)
        Me.grpList.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlPrintInfo.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lbltitleDeptCd As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Protected Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblHypnOrdDay As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Protected Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents label56 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents grpQryInfo As System.Windows.Forms.GroupBox
    Friend WithEvents cboDptOrWard As System.Windows.Forms.ComboBox
    Friend WithEvents lblIoGbn As System.Windows.Forms.Label
    Protected Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Protected Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents lblRegno As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCollGbn As System.Windows.Forms.Label
    Friend WithEvents pnlCollGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoNoColl As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents rdoColl As System.Windows.Forms.RadioButton
    Protected Friend WithEvents lblPatnm As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents lblDptOrWard As System.Windows.Forms.Label
    Friend WithEvents lblLineQry As System.Windows.Forms.Label
    Friend WithEvents lblLine As System.Windows.Forms.Label
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxPatientInfo
    Friend WithEvents grpList As System.Windows.Forms.GroupBox
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents pnlPrintInfo As System.Windows.Forms.Panel
    Friend WithEvents btnPrint_Set As System.Windows.Forms.Button
    Friend WithEvents lblPrint As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents lblPatCount As System.Windows.Forms.Label
    Friend WithEvents btnCancel_coll As CButtonLib.CButton
    Friend WithEvents lblOrder As System.Windows.Forms.Label
    Friend WithEvents btnReg_Coll As CButtonLib.CButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rdoIoGbnO As System.Windows.Forms.RadioButton
    Friend WithEvents rdoIoGbnI As System.Windows.Forms.RadioButton
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents chkHopeDay As System.Windows.Forms.CheckBox
    Friend WithEvents axCollBcNos As AxAckCollector.AxCollBcNos
    Friend WithEvents lblBarPrinter As System.Windows.Forms.Label
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnPrint_BC As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnPrint_Label As CButtonLib.CButton
    Friend WithEvents btnPrint_Doc As CButtonLib.CButton
    Friend WithEvents btnQuery_Unfit As CButtonLib.CButton
    Friend WithEvents btnReg_Pat As CButtonLib.CButton
    Friend WithEvents axCollList As AxAckCollector.AxCollList_pis
End Class
