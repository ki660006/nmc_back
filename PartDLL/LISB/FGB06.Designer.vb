<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGB06
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB06))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
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
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.Label99 = New System.Windows.Forms.Label()
        Me.lblComcd = New System.Windows.Forms.Label()
        Me.lblSGbn = New System.Windows.Forms.Label()
        Me.txtRegno = New System.Windows.Forms.TextBox()
        Me.btnPatPop = New System.Windows.Forms.Button()
        Me.txtPatNm = New System.Windows.Forms.TextBox()
        Me.cboComCd = New System.Windows.Forms.ComboBox()
        Me.pnlSearchGbn = New System.Windows.Forms.Panel()
        Me.cboFilter = New System.Windows.Forms.ComboBox()
        Me.rdoNoJub = New System.Windows.Forms.RadioButton()
        Me.rdoJubsu = New System.Windows.Forms.RadioButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cboTnsGbn = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.axPatInfo = New AxAckPatientInfo.AxTnsPatinfo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnSearch = New CButtonLib.CButton()
        Me.btnExecute = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.pnlBtn = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblUnConfirmAlarm = New System.Windows.Forms.Label()
        Me.lblAutoQuery = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtUnCfmAlarmSec = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtAutoSearchSec = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.pnlWorkList = New System.Windows.Forms.Panel()
        Me.spdWorkList = New AxFPSpreadADO.AxfpSpread()
        Me.tmrReq = New System.Windows.Forms.Timer(Me.components)
        Me.tmrAlarm = New System.Windows.Forms.Timer(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlKeepList = New System.Windows.Forms.Panel()
        Me.spdKeepList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlPastTns = New System.Windows.Forms.Panel()
        Me.spdPastTns = New AxFPSpreadADO.AxfpSpread()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnOrdbld = New CButtonLib.CButton()
        Me.pnlSearchGbn.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnlWorkList.SuspendLayout()
        CType(Me.spdWorkList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlKeepList.SuspendLayout()
        CType(Me.spdKeepList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPastTns.SuspendLayout()
        CType(Me.spdPastTns, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDate.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.White
        Me.lblDate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDate.Location = New System.Drawing.Point(3, 25)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(80, 21)
        Me.lblDate.TabIndex = 100
        Me.lblDate.Text = "처방일자"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(84, 25)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate0.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(179, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 12)
        Me.Label4.TabIndex = 114
        Me.Label4.Text = "~"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(200, 25)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(88, 21)
        Me.dtpDate1.TabIndex = 2
        '
        'Label99
        '
        Me.Label99.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label99.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label99.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label99.ForeColor = System.Drawing.Color.White
        Me.Label99.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label99.Location = New System.Drawing.Point(3, 47)
        Me.Label99.Margin = New System.Windows.Forms.Padding(1)
        Me.Label99.Name = "Label99"
        Me.Label99.Size = New System.Drawing.Size(80, 21)
        Me.Label99.TabIndex = 115
        Me.Label99.Text = "등록번호"
        Me.Label99.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblComcd
        '
        Me.lblComcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComcd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblComcd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComcd.ForeColor = System.Drawing.Color.White
        Me.lblComcd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComcd.Location = New System.Drawing.Point(3, 69)
        Me.lblComcd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComcd.Name = "lblComcd"
        Me.lblComcd.Size = New System.Drawing.Size(80, 21)
        Me.lblComcd.TabIndex = 116
        Me.lblComcd.Text = "성분제제"
        Me.lblComcd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSGbn
        '
        Me.lblSGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSGbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSGbn.ForeColor = System.Drawing.Color.White
        Me.lblSGbn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSGbn.Location = New System.Drawing.Point(3, 3)
        Me.lblSGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSGbn.Name = "lblSGbn"
        Me.lblSGbn.Size = New System.Drawing.Size(80, 21)
        Me.lblSGbn.TabIndex = 117
        Me.lblSGbn.Text = "조회구분"
        Me.lblSGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegno
        '
        Me.txtRegno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRegno.Location = New System.Drawing.Point(84, 47)
        Me.txtRegno.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegno.MaxLength = 8
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(73, 21)
        Me.txtRegno.TabIndex = 0
        '
        'btnPatPop
        '
        Me.btnPatPop.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnPatPop.Image = CType(resources.GetObject("btnPatPop.Image"), System.Drawing.Image)
        Me.btnPatPop.Location = New System.Drawing.Point(158, 47)
        Me.btnPatPop.Name = "btnPatPop"
        Me.btnPatPop.Size = New System.Drawing.Size(21, 21)
        Me.btnPatPop.TabIndex = 181
        Me.btnPatPop.UseVisualStyleBackColor = True
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.SystemColors.Window
        Me.txtPatNm.Location = New System.Drawing.Point(180, 47)
        Me.txtPatNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPatNm.MaxLength = 50
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(108, 21)
        Me.txtPatNm.TabIndex = 182
        '
        'cboComCd
        '
        Me.cboComCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboComCd.FormattingEnabled = True
        Me.cboComCd.Location = New System.Drawing.Point(84, 69)
        Me.cboComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboComCd.MaxDropDownItems = 20
        Me.cboComCd.Name = "cboComCd"
        Me.cboComCd.Size = New System.Drawing.Size(204, 20)
        Me.cboComCd.TabIndex = 1
        '
        'pnlSearchGbn
        '
        Me.pnlSearchGbn.BackColor = System.Drawing.Color.Transparent
        Me.pnlSearchGbn.Controls.Add(Me.cboFilter)
        Me.pnlSearchGbn.Controls.Add(Me.rdoNoJub)
        Me.pnlSearchGbn.Controls.Add(Me.rdoJubsu)
        Me.pnlSearchGbn.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlSearchGbn.Location = New System.Drawing.Point(84, 3)
        Me.pnlSearchGbn.Name = "pnlSearchGbn"
        Me.pnlSearchGbn.Size = New System.Drawing.Size(204, 22)
        Me.pnlSearchGbn.TabIndex = 184
        '
        'cboFilter
        '
        Me.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFilter.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboFilter.FormattingEnabled = True
        Me.cboFilter.Items.AddRange(New Object() {"[ ] 전체", "[0] 미완료", "[1] 완료"})
        Me.cboFilter.Location = New System.Drawing.Point(118, 0)
        Me.cboFilter.Name = "cboFilter"
        Me.cboFilter.Size = New System.Drawing.Size(86, 20)
        Me.cboFilter.TabIndex = 7
        Me.cboFilter.Visible = False
        '
        'rdoNoJub
        '
        Me.rdoNoJub.AutoSize = True
        Me.rdoNoJub.Checked = True
        Me.rdoNoJub.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoNoJub.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoNoJub.Location = New System.Drawing.Point(4, 1)
        Me.rdoNoJub.Name = "rdoNoJub"
        Me.rdoNoJub.Size = New System.Drawing.Size(59, 20)
        Me.rdoNoJub.TabIndex = 5
        Me.rdoNoJub.TabStop = True
        Me.rdoNoJub.Tag = "1"
        Me.rdoNoJub.Text = "미접수"
        Me.rdoNoJub.UseCompatibleTextRendering = True
        '
        'rdoJubsu
        '
        Me.rdoJubsu.AutoSize = True
        Me.rdoJubsu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoJubsu.ForeColor = System.Drawing.SystemColors.WindowText
        Me.rdoJubsu.Location = New System.Drawing.Point(68, 1)
        Me.rdoJubsu.Name = "rdoJubsu"
        Me.rdoJubsu.Size = New System.Drawing.Size(46, 20)
        Me.rdoJubsu.TabIndex = 6
        Me.rdoJubsu.Tag = "1"
        Me.rdoJubsu.Text = "접수"
        Me.rdoJubsu.UseCompatibleTextRendering = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.cboTnsGbn)
        Me.Panel3.Controls.Add(Me.Label6)
        Me.Panel3.Controls.Add(Me.pnlSearchGbn)
        Me.Panel3.Controls.Add(Me.Label99)
        Me.Panel3.Controls.Add(Me.cboComCd)
        Me.Panel3.Controls.Add(Me.lblComcd)
        Me.Panel3.Controls.Add(Me.txtPatNm)
        Me.Panel3.Controls.Add(Me.lblSGbn)
        Me.Panel3.Controls.Add(Me.btnPatPop)
        Me.Panel3.Controls.Add(Me.txtRegno)
        Me.Panel3.Controls.Add(Me.dtpDate1)
        Me.Panel3.Controls.Add(Me.lblDate)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.dtpDate0)
        Me.Panel3.Location = New System.Drawing.Point(3, 1)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(292, 114)
        Me.Panel3.TabIndex = 188
        '
        'cboTnsGbn
        '
        Me.cboTnsGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTnsGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboTnsGbn.FormattingEnabled = True
        Me.cboTnsGbn.Location = New System.Drawing.Point(84, 91)
        Me.cboTnsGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTnsGbn.MaxDropDownItems = 20
        Me.cboTnsGbn.Name = "cboTnsGbn"
        Me.cboTnsGbn.Size = New System.Drawing.Size(204, 20)
        Me.cboTnsGbn.TabIndex = 186
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(3, 91)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 21)
        Me.Label6.TabIndex = 185
        Me.Label6.Text = "수혈구분"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'axPatInfo
        '
        Me.axPatInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.Location = New System.Drawing.Point(293, -3)
        Me.axPatInfo.Margin = New System.Windows.Forms.Padding(1)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.Size = New System.Drawing.Size(891, 168)
        Me.axPatInfo.TabIndex = 189
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.ForeColor = System.Drawing.Color.Gray
        Me.Label1.Location = New System.Drawing.Point(3, 162)
        Me.Label1.Margin = New System.Windows.Forms.Padding(0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1183, 9)
        Me.Label1.TabIndex = 191
        Me.Label1.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'btnSearch
        '
        Me.btnSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnSearch.ColorFillBlend = CBlendItems1
        Me.btnSearch.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnSearch.Corners.All = CType(6, Short)
        Me.btnSearch.Corners.LowerLeft = CType(6, Short)
        Me.btnSearch.Corners.LowerRight = CType(6, Short)
        Me.btnSearch.Corners.UpperLeft = CType(6, Short)
        Me.btnSearch.Corners.UpperRight = CType(6, Short)
        Me.btnSearch.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnSearch.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnSearch.FocalPoints.CenterPtX = 0.4672897!
        Me.btnSearch.FocalPoints.CenterPtY = 0.2!
        Me.btnSearch.FocalPoints.FocusPtX = 0.0!
        Me.btnSearch.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnSearch.FocusPtTracker = DesignerRectTracker2
        Me.btnSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.ForeColor = System.Drawing.Color.White
        Me.btnSearch.Image = Nothing
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnSearch.ImageIndex = 0
        Me.btnSearch.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnSearch.Location = New System.Drawing.Point(754, 3)
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
        'btnExecute
        '
        Me.btnExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExecute.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExecute.FocusPtTracker = DesignerRectTracker4
        Me.btnExecute.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExecute.ForeColor = System.Drawing.Color.White
        Me.btnExecute.Image = Nothing
        Me.btnExecute.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExecute.ImageIndex = 0
        Me.btnExecute.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExecute.Location = New System.Drawing.Point(862, 3)
        Me.btnExecute.Name = "btnExecute"
        Me.btnExecute.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExecute.SideImage = Nothing
        Me.btnExecute.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExecute.Size = New System.Drawing.Size(107, 25)
        Me.btnExecute.TabIndex = 185
        Me.btnExecute.Text = "접   수(F7)"
        Me.btnExecute.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExecute.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4725275!
        Me.btnExit.FocalPoints.CenterPtY = 0.64!
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
        Me.btnExit.Location = New System.Drawing.Point(1078, 3)
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
        Me.btnClear.Location = New System.Drawing.Point(970, 3)
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
        'pnlBtn
        '
        Me.pnlBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBtn.Location = New System.Drawing.Point(534, 449)
        Me.pnlBtn.Name = "pnlBtn"
        Me.pnlBtn.Size = New System.Drawing.Size(535, 31)
        Me.pnlBtn.TabIndex = 186
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblUnConfirmAlarm)
        Me.GroupBox1.Controls.Add(Me.lblAutoQuery)
        Me.GroupBox1.Controls.Add(Me.Panel5)
        Me.GroupBox1.Controls.Add(Me.Panel4)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 107)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 56)
        Me.GroupBox1.TabIndex = 193
        Me.GroupBox1.TabStop = False
        '
        'lblUnConfirmAlarm
        '
        Me.lblUnConfirmAlarm.BackColor = System.Drawing.Color.FromArgb(CType(CType(211, Byte), Integer), CType(CType(193, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.lblUnConfirmAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnConfirmAlarm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblUnConfirmAlarm.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUnConfirmAlarm.Location = New System.Drawing.Point(4, 34)
        Me.lblUnConfirmAlarm.Name = "lblUnConfirmAlarm"
        Me.lblUnConfirmAlarm.Size = New System.Drawing.Size(106, 21)
        Me.lblUnConfirmAlarm.TabIndex = 1
        Me.lblUnConfirmAlarm.Text = "미확인 알림 OFF"
        Me.lblUnConfirmAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAutoQuery
        '
        Me.lblAutoQuery.BackColor = System.Drawing.Color.FromArgb(CType(CType(179, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(147, Byte), Integer))
        Me.lblAutoQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAutoQuery.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAutoQuery.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAutoQuery.Location = New System.Drawing.Point(4, 11)
        Me.lblAutoQuery.Name = "lblAutoQuery"
        Me.lblAutoQuery.Size = New System.Drawing.Size(106, 21)
        Me.lblAutoQuery.TabIndex = 0
        Me.lblAutoQuery.Text = "자동조회 OFF"
        Me.lblAutoQuery.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.Controls.Add(Me.txtUnCfmAlarmSec)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.ForeColor = System.Drawing.Color.DarkOrchid
        Me.Panel5.Location = New System.Drawing.Point(111, 34)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(177, 21)
        Me.Panel5.TabIndex = 3
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(159, 5)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(15, 12)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "초"
        '
        'txtUnCfmAlarmSec
        '
        Me.txtUnCfmAlarmSec.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.txtUnCfmAlarmSec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUnCfmAlarmSec.Location = New System.Drawing.Point(132, 4)
        Me.txtUnCfmAlarmSec.Margin = New System.Windows.Forms.Padding(1)
        Me.txtUnCfmAlarmSec.Name = "txtUnCfmAlarmSec"
        Me.txtUnCfmAlarmSec.Size = New System.Drawing.Size(28, 14)
        Me.txtUnCfmAlarmSec.TabIndex = 1
        Me.txtUnCfmAlarmSec.Text = "30"
        Me.txtUnCfmAlarmSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(3, 5)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 12)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "확인시 까지 알람간격"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.MintCream
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.txtAutoSearchSec)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.ForeColor = System.Drawing.Color.DarkGreen
        Me.Panel4.Location = New System.Drawing.Point(112, 11)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(176, 21)
        Me.Panel4.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(157, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(16, 12)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "초"
        '
        'txtAutoSearchSec
        '
        Me.txtAutoSearchSec.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(250, Byte), Integer), CType(CType(182, Byte), Integer))
        Me.txtAutoSearchSec.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtAutoSearchSec.Location = New System.Drawing.Point(130, 3)
        Me.txtAutoSearchSec.Margin = New System.Windows.Forms.Padding(1)
        Me.txtAutoSearchSec.Name = "txtAutoSearchSec"
        Me.txtAutoSearchSec.Size = New System.Drawing.Size(28, 14)
        Me.txtAutoSearchSec.TabIndex = 1
        Me.txtAutoSearchSec.Text = "30"
        Me.txtAutoSearchSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(2, 5)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(88, 12)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "자동 조회 간격 "
        '
        'pnlWorkList
        '
        Me.pnlWorkList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlWorkList.Controls.Add(Me.spdWorkList)
        Me.pnlWorkList.Controls.Add(Me.pnlBtn)
        Me.pnlWorkList.Location = New System.Drawing.Point(5, 292)
        Me.pnlWorkList.Name = "pnlWorkList"
        Me.pnlWorkList.Size = New System.Drawing.Size(1172, 345)
        Me.pnlWorkList.TabIndex = 194
        '
        'spdWorkList
        '
        Me.spdWorkList.DataSource = Nothing
        Me.spdWorkList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdWorkList.Location = New System.Drawing.Point(0, 0)
        Me.spdWorkList.Name = "spdWorkList"
        Me.spdWorkList.OcxState = CType(resources.GetObject("spdWorkList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdWorkList.Size = New System.Drawing.Size(1172, 345)
        Me.spdWorkList.TabIndex = 0
        '
        'tmrReq
        '
        '
        'tmrAlarm
        '
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.ForeColor = System.Drawing.Color.Gray
        Me.Label2.Location = New System.Drawing.Point(3, 281)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(1183, 11)
        Me.Label2.TabIndex = 195
        Me.Label2.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 173)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 12)
        Me.Label3.TabIndex = 196
        Me.Label3.Text = "보관검체정보"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.Location = New System.Drawing.Point(682, 173)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 12)
        Me.Label5.TabIndex = 197
        Me.Label5.Text = "과거수혈경력"
        '
        'pnlKeepList
        '
        Me.pnlKeepList.Controls.Add(Me.spdKeepList)
        Me.pnlKeepList.Location = New System.Drawing.Point(5, 189)
        Me.pnlKeepList.Name = "pnlKeepList"
        Me.pnlKeepList.Size = New System.Drawing.Size(645, 92)
        Me.pnlKeepList.TabIndex = 198
        '
        'spdKeepList
        '
        Me.spdKeepList.DataSource = Nothing
        Me.spdKeepList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdKeepList.Location = New System.Drawing.Point(0, 0)
        Me.spdKeepList.Name = "spdKeepList"
        Me.spdKeepList.OcxState = CType(resources.GetObject("spdKeepList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdKeepList.Size = New System.Drawing.Size(645, 92)
        Me.spdKeepList.TabIndex = 0
        '
        'pnlPastTns
        '
        Me.pnlPastTns.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPastTns.Controls.Add(Me.spdPastTns)
        Me.pnlPastTns.Location = New System.Drawing.Point(659, 189)
        Me.pnlPastTns.Name = "pnlPastTns"
        Me.pnlPastTns.Size = New System.Drawing.Size(516, 92)
        Me.pnlPastTns.TabIndex = 199
        '
        'spdPastTns
        '
        Me.spdPastTns.DataSource = Nothing
        Me.spdPastTns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdPastTns.Location = New System.Drawing.Point(0, 0)
        Me.spdPastTns.Name = "spdPastTns"
        Me.spdPastTns.OcxState = CType(resources.GetObject("spdPastTns.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPastTns.Size = New System.Drawing.Size(516, 92)
        Me.spdPastTns.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(5, 169)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 19)
        Me.PictureBox1.TabIndex = 202
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(658, 169)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(20, 19)
        Me.PictureBox2.TabIndex = 203
        Me.PictureBox2.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.btnOrdbld)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnExecute)
        Me.Panel1.Controls.Add(Me.btnClear)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 640)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1183, 32)
        Me.Panel1.TabIndex = 204
        '
        'btnOrdbld
        '
        DesignerRectTracker9.IsActive = False
        DesignerRectTracker9.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker9.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOrdbld.CenterPtTracker = DesignerRectTracker9
        CBlendItems5.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems5.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnOrdbld.ColorFillBlend = CBlendItems5
        Me.btnOrdbld.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnOrdbld.Corners.All = CType(6, Short)
        Me.btnOrdbld.Corners.LowerLeft = CType(6, Short)
        Me.btnOrdbld.Corners.LowerRight = CType(6, Short)
        Me.btnOrdbld.Corners.UpperLeft = CType(6, Short)
        Me.btnOrdbld.Corners.UpperRight = CType(6, Short)
        Me.btnOrdbld.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnOrdbld.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnOrdbld.FocalPoints.CenterPtX = 0.4672897!
        Me.btnOrdbld.FocalPoints.CenterPtY = 0.2!
        Me.btnOrdbld.FocalPoints.FocusPtX = 0.0!
        Me.btnOrdbld.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker10.IsActive = False
        DesignerRectTracker10.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker10.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOrdbld.FocusPtTracker = DesignerRectTracker10
        Me.btnOrdbld.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOrdbld.ForeColor = System.Drawing.Color.White
        Me.btnOrdbld.Image = Nothing
        Me.btnOrdbld.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOrdbld.ImageIndex = 0
        Me.btnOrdbld.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOrdbld.Location = New System.Drawing.Point(0, 3)
        Me.btnOrdbld.Name = "btnOrdbld"
        Me.btnOrdbld.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOrdbld.SideImage = Nothing
        Me.btnOrdbld.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOrdbld.Size = New System.Drawing.Size(107, 25)
        Me.btnOrdbld.TabIndex = 187
        Me.btnOrdbld.Text = "수혈처방조회"
        Me.btnOrdbld.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOrdbld.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGB06
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1183, 672)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.pnlPastTns)
        Me.Controls.Add(Me.pnlKeepList)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.pnlWorkList)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.axPatInfo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.KeyPreview = True
        Me.Name = "FGB06"
        Me.Text = "수혈 의뢰 접수"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlSearchGbn.ResumeLayout(False)
        Me.pnlSearchGbn.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.pnlWorkList.ResumeLayout(False)
        CType(Me.spdWorkList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlKeepList.ResumeLayout(False)
        CType(Me.spdKeepList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPastTns.ResumeLayout(False)
        CType(Me.spdPastTns, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label99 As System.Windows.Forms.Label
    Friend WithEvents lblComcd As System.Windows.Forms.Label
    Friend WithEvents lblSGbn As System.Windows.Forms.Label
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents btnPatPop As System.Windows.Forms.Button
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents cboComCd As System.Windows.Forms.ComboBox
    Friend WithEvents pnlSearchGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoNoJub As System.Windows.Forms.RadioButton
    Friend WithEvents rdoJubsu As System.Windows.Forms.RadioButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxTnsPatinfo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExecute As CButtonLib.CButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblUnConfirmAlarm As System.Windows.Forms.Label
    Friend WithEvents txtAutoSearchSec As System.Windows.Forms.TextBox
    Friend WithEvents lblAutoQuery As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtUnCfmAlarmSec As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents pnlWorkList As System.Windows.Forms.Panel
    Friend WithEvents spdWorkList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlBtn As System.Windows.Forms.Panel
    Friend WithEvents btnSearch As CButtonLib.CButton
    Friend WithEvents tmrReq As System.Windows.Forms.Timer
    Friend WithEvents tmrAlarm As System.Windows.Forms.Timer
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlKeepList As System.Windows.Forms.Panel
    Friend WithEvents pnlPastTns As System.Windows.Forms.Panel
    Friend WithEvents spdKeepList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdPastTns As AxFPSpreadADO.AxfpSpread
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents cboTnsGbn As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboFilter As System.Windows.Forms.ComboBox
    Friend WithEvents btnOrdbld As CButtonLib.CButton
End Class
