'>>> 가출고 취소

Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_DB
Imports LISAPP.APP_BT

Public Class FGB08
    Inherits System.Windows.Forms.Form
    Dim COM_01 As New COMMON.CommFN.Fn
    Friend WithEvents btnExeCancel As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Dim User_Id As String = USER_INFO.USRID

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents spdTransBloodList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents rdoGbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoGbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents spdBefBldList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtHour As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB08))
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker7 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems4 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker8 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtHour = New System.Windows.Forms.TextBox
        Me.rdoGbn0 = New System.Windows.Forms.RadioButton
        Me.rdoGbn1 = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spdTransBloodList = New AxFPSpreadADO.AxfpSpread
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdBefBldList = New AxFPSpreadADO.AxfpSpread
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.btnExeCancel = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdTransBloodList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.spdBefBldList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpDateE)
        Me.GroupBox1.Controls.Add(Me.dtpDateS)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(544, 104)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.GhostWhite
        Me.Panel1.Controls.Add(Me.txtHour)
        Me.Panel1.Controls.Add(Me.rdoGbn0)
        Me.Panel1.Controls.Add(Me.rdoGbn1)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.Panel1.Location = New System.Drawing.Point(132, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(388, 56)
        Me.Panel1.TabIndex = 113
        '
        'txtHour
        '
        Me.txtHour.BackColor = System.Drawing.Color.FromArgb(CType(CType(237, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(236, Byte), Integer))
        Me.txtHour.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtHour.Location = New System.Drawing.Point(208, 8)
        Me.txtHour.Name = "txtHour"
        Me.txtHour.Size = New System.Drawing.Size(28, 14)
        Me.txtHour.TabIndex = 6
        Me.txtHour.Text = "72"
        Me.txtHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'rdoGbn0
        '
        Me.rdoGbn0.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn0.Checked = True
        Me.rdoGbn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn0.Location = New System.Drawing.Point(12, 6)
        Me.rdoGbn0.Name = "rdoGbn0"
        Me.rdoGbn0.Size = New System.Drawing.Size(360, 20)
        Me.rdoGbn0.TabIndex = 0
        Me.rdoGbn0.TabStop = True
        Me.rdoGbn0.Tag = "0"
        Me.rdoGbn0.Text = "수혈의뢰 접수후 출고 미완료로        시간이상 경과된 Order"
        Me.rdoGbn0.UseVisualStyleBackColor = False
        '
        'rdoGbn1
        '
        Me.rdoGbn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn1.Location = New System.Drawing.Point(12, 30)
        Me.rdoGbn1.Name = "rdoGbn1"
        Me.rdoGbn1.Size = New System.Drawing.Size(328, 20)
        Me.rdoGbn1.TabIndex = 1
        Me.rdoGbn1.Tag = "1"
        Me.rdoGbn1.Text = "출고 미완료로 수술(수혈) 예정일이 지나버린 Order"
        Me.rdoGbn1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(388, 56)
        Me.Label3.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(16, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 22)
        Me.Label1.TabIndex = 112
        Me.Label1.Text = "조 회 조 건"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(404, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(116, 22)
        Me.btnSearch.TabIndex = 111
        Me.btnSearch.Text = "조회"
        Me.btnSearch.UseVisualStyleBackColor = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(16, 12)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(112, 22)
        Me.Label14.TabIndex = 110
        Me.Label14.Text = "수혈의뢰접수일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(224, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(16, 16)
        Me.Label4.TabIndex = 109
        Me.Label4.Text = "~"
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(244, 12)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 108
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(132, 12)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateS.TabIndex = 107
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.spdTransBloodList)
        Me.Panel2.Location = New System.Drawing.Point(8, 112)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1000, 272)
        Me.Panel2.TabIndex = 1
        '
        'spdTransBloodList
        '
        Me.spdTransBloodList.DataSource = Nothing
        Me.spdTransBloodList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdTransBloodList.Location = New System.Drawing.Point(0, 0)
        Me.spdTransBloodList.Name = "spdTransBloodList"
        Me.spdTransBloodList.OcxState = CType(resources.GetObject("spdTransBloodList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdTransBloodList.Size = New System.Drawing.Size(996, 268)
        Me.spdTransBloodList.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.spdBefBldList)
        Me.Panel3.Location = New System.Drawing.Point(8, 412)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1000, 180)
        Me.Panel3.TabIndex = 100
        '
        'spdBefBldList
        '
        Me.spdBefBldList.DataSource = Nothing
        Me.spdBefBldList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdBefBldList.Location = New System.Drawing.Point(0, 0)
        Me.spdBefBldList.Name = "spdBefBldList"
        Me.spdBefBldList.OcxState = CType(resources.GetObject("spdBefBldList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBefBldList.Size = New System.Drawing.Size(996, 176)
        Me.spdBefBldList.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.SlateGray
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(8, 388)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(252, 24)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "가출고 혈액리스트"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(601, 11)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 152
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(529, 11)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 151
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.btnExeCancel)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.lblUserNm)
        Me.pnlBottom.Controls.Add(Me.lblUserId)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 595)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1012, 34)
        Me.pnlBottom.TabIndex = 153
        '
        'btnExeCancel
        '
        Me.btnExeCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExeCancel.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExeCancel.ColorFillBlend = CBlendItems3
        Me.btnExeCancel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExeCancel.Corners.All = CType(6, Short)
        Me.btnExeCancel.Corners.LowerLeft = CType(6, Short)
        Me.btnExeCancel.Corners.LowerRight = CType(6, Short)
        Me.btnExeCancel.Corners.UpperLeft = CType(6, Short)
        Me.btnExeCancel.Corners.UpperRight = CType(6, Short)
        Me.btnExeCancel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExeCancel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExeCancel.FocalPoints.CenterPtX = 0.4579439!
        Me.btnExeCancel.FocalPoints.CenterPtY = 0.32!
        Me.btnExeCancel.FocalPoints.FocusPtX = 0.0!
        Me.btnExeCancel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExeCancel.FocusPtTracker = DesignerRectTracker6
        Me.btnExeCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExeCancel.ForeColor = System.Drawing.Color.White
        Me.btnExeCancel.Image = Nothing
        Me.btnExeCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExeCancel.ImageIndex = 0
        Me.btnExeCancel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExeCancel.Location = New System.Drawing.Point(691, 4)
        Me.btnExeCancel.Name = "btnExeCancel"
        Me.btnExeCancel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExeCancel.SideImage = Nothing
        Me.btnExeCancel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExeCancel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExeCancel.Size = New System.Drawing.Size(107, 25)
        Me.btnExeCancel.TabIndex = 190
        Me.btnExeCancel.Tag = "availdt"
        Me.btnExeCancel.Text = "가출고취소(F8)"
        Me.btnExeCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExeCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExeCancel.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.Location = New System.Drawing.Point(907, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 189
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems1
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
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker2
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(799, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 188
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGB08
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1012, 629)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGB08"
        Me.Text = "가출고 취소"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdTransBloodList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdBefBldList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "
    Private Sub Form_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        fnSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub
#End Region

    Private Sub FGB08_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    ' Function Key정의
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub sbFormInitialize()
        Dim objCommFn As New Fn

        Try
            Dim objServerTime As New ServerDateTime

            'DT_ED.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            With objServerTime
                Me.dtpDateE.Value = CDate(.GetDate("-"))
                Me.dtpDateS.Value = dtpDateE.Value.AddMonths(-1)
            End With

            'DT_ED.Value = Format(Now, "yyyy-MM-dd")
            'DT_ST.Value = DT_ED.Value.AddMonths(-1)

            sbDisplay_Init()

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            ''Spread Header이름을 컬럼명으로 설정
            objCommFn.SpdSetColName(spdTransBloodList)
            'objCommFn.SpdSetColName(spdBefBldList)

            fnSpreadColHidden(True)

            'btnSearch_Click(Nothing, Nothing)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 칼럼 Hidden 유무
    Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)

        With Me.spdBefBldList
            .Col = .GetColFromID("ABO") : .ColHidden = abFlag
            .Col = .GetColFromID("Rh") : .ColHidden = abFlag

            .Col = .GetColFromID("OWNGBN") : .ColHidden = abFlag
            .Col = .GetColFromID("IOGBN") : .ColHidden = abFlag
            .Col = .GetColFromID("SPCCD") : .ColHidden = abFlag
            .Col = .GetColFromID("COMSTATE") : .ColHidden = abFlag
            .Col = .GetColFromID("FKOCS") : .ColHidden = abFlag
        End With

    End Sub

    Private Sub sbDisplay_Init()
        Me.spdTransBloodList.MaxRows = 0
        Me.spdBefBldList.MaxRows = 0       ' 가출고 혈액리스트
    End Sub

    Private Sub ShowBefOutList(ByVal rsTnsJubsuNm As String, ByVal rsComCd As String)    ' 가출고 혈액리스트를 보여준다

        Try
            Dim dt As DataTable = CGDA_BT.Select_BefOutInfo(rsTnsJubsuNm, rsComCd)
            If dt.Rows.Count > 0 Then
                With Me.spdBefBldList
                    .MaxRows = dt.Rows.Count

                    For ix = 0 To .MaxRows - 1
                        .Row = ix + 1

                        Select Case dt.Rows(ix).Item("dongbn").ToString().Trim
                            Case "0" : .Col = .GetColFromID("dongbn") : .Text = "혈액원"
                            Case "1" : .Col = .GetColFromID("dongbn") : .Text = "일반"
                            Case "2" : .Col = .GetColFromID("dongbn") : .Text = "지정"
                            Case "3" : .Col = .GetColFromID("dongbn") : .Text = "성분"
                            Case "4" : .Col = .GetColFromID("dongbn") : .Text = "자가"
                        End Select


                        .Col = .GetColFromID("bldno") : .Text = dt.Rows(ix).Item("bldno").ToString().Trim
                        .Col = .GetColFromID("comnmd") : .Text = dt.Rows(ix).Item("comnm").ToString().Trim
                        .Col = .GetColFromID("comcd") : .Text = dt.Rows(ix).Item("comcd").ToString().Trim
                        .Col = .GetColFromID("comcd_out") : .Text = dt.Rows(ix).Item("comcd_out").ToString().Trim
                        .Col = .GetColFromID("tnsjubsuno") : .Text = dt.Rows(ix).Item("tnsjubsuno").ToString().Trim


                        .Col = .GetColFromID("aborh") : .Text = dt.Rows(ix).Item("aborh").ToString().Trim
                        .Col = .GetColFromID("abo") : .Text = dt.Rows(ix).Item("abo").ToString().Trim
                        .Col = .GetColFromID("rh") : .Text = dt.Rows(ix).Item("rh").ToString().Trim

                        .Col = .GetColFromID("befoutdt") : .Text = dt.Rows(ix).Item("befoutdt").ToString().Trim
                        .Col = .GetColFromID("testnm") : .Text = dt.Rows(ix).Item("testnm").ToString().Trim
                        .Col = .GetColFromID("dondt") : .Text = dt.Rows(ix).Item("dondt").ToString().Trim
                        .Col = .GetColFromID("indt") : .Text = dt.Rows(ix).Item("indt").ToString().Trim
                        .Col = .GetColFromID("availdt") : .Text = dt.Rows(ix).Item("availdt").ToString().Trim

                        .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString().Trim
                        .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString().Trim
                        .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString().Trim
                        .Col = .GetColFromID("statecd") : .Text = dt.Rows(ix).Item("statecd").ToString().Trim
                        .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString().Trim
                        .Col = .GetColFromID("orddt") : .Text = dt.Rows(ix).Item("orddt").ToString().Trim
                        .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString().Trim

                        .Col = .GetColFromID("chk") : .Text = "1"
                    Next
                End With
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "조회된 가출고 혈액리스트가 없습니다")
                Return
            End If
        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub rdoGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGbn0.Click, rdoGbn1.Click
        Dim objRdo As Windows.Forms.RadioButton = CType(sender, Windows.Forms.RadioButton)
        Dim sTag As String = CType(objRdo.Tag, String)

        If sTag = "0" Then
            Me.Label14.Text = "수혈의뢰접수일자"
        Else
            Me.Label14.Text = "수술예정일"
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sbDisplay_Init()

        Dim objComn As New ServerDateTime
        Dim sQryGbn As String = ""
        Dim NowTime As Date
        Dim sTnsJubsu As String = ""
        Dim sPKey As String = ""
        Dim sTime As String = ""

        NowTime = objComn.GetDateTime

        If Me.rdoGbn0.Checked Then  ' 출고미완료로 시간 경과된 order 
            sQryGbn = "0"
        Else
            ' 출고미완료로 수술예정일이 지나버린 order 
            sQryGbn = "1"
        End If

        Try
            Dim dt As DataTable = CGDA_BT.Select_TnsJubsu(Me.txtHour.Text.Trim, Me.dtpDateS.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), Me.dtpDateE.Text.Replace("-", "").Replace(":", "").Replace(" ", ""), sQryGbn)

            If dt.Rows.Count > 0 Then
                With Me.spdTransBloodList
                    .MaxRows = dt.Rows.Count

                    For ix As Integer = 0 To .MaxRows - 1
                        .Row = ix + 1
                        sTnsJubsu = dt.Rows(ix).Item("tnsjubsuno").ToString()

                        If sPKey <> sTnsJubsu Then
                            .Col = .GetColFromID("수혈의뢰접수번호") : .Text = Fn.TNSNO_View(sTnsJubsu)
                            .Col = .GetColFromID("접수일시") : .Text = dt.Rows(ix).Item("jubsudt").ToString()
                            sTime = .Text

                            .Col = .GetColFromID("경과시간:분:초")    ' 접수시간으로 부터 얼마나 경과했는지!!
                            .Text = COM_01.TimeElapsed(CType(sTime, Date), NowTime)   ' CGCOMMON01.vb 

                            .Col = .GetColFromID("등록번호") : .Text = dt.Rows(ix).Item("regno").ToString()
                            .Col = .GetColFromID("성명") : .Text = dt.Rows(ix).Item("patnm").ToString()
                            .Col = .GetColFromID("Sex/Age") : .Text = dt.Rows(ix).Item("sexage").ToString()
                            .Col = .GetColFromID("의뢰의사") : .Text = dt.Rows(ix).Item("doctornm").ToString()
                            .Col = .GetColFromID("진료과") : .Text = dt.Rows(ix).Item("deptcd").ToString()
                            .Col = .GetColFromID("병동") : .Text = dt.Rows(ix).Item("wardno").ToString()
                            .Col = .GetColFromID("수술예정일") : .Text = dt.Rows(ix).Item("opdt").ToString()

                            sPKey = sTnsJubsu
                        End If

                        .Col = .GetColFromID("접수번호") : .Text = dt.Rows(ix).Item("tnsjubsuno").ToString() ' hidden 된 column임!!
                        .Col = .GetColFromID("성분제제코드") : .Text = dt.Rows(ix).Item("comcd").ToString() ' hidden 된 column임!!
                        .Col = .GetColFromID("성분제제") : .Text = dt.Rows(ix).Item("comnmd").ToString()
                        .Col = .GetColFromID("IR") : .Text = dt.Rows(ix).Item("ir").ToString()
                        .Col = .GetColFromID("Filter") : .Text = dt.Rows(ix).Item("filter").ToString()
                        .Col = .GetColFromID("의뢰") : .Text = dt.Rows(ix).Item("reqqnt").ToString()
                        .Col = .GetColFromID("가출고") : .Text = dt.Rows(ix).Item("befoutqnt").ToString()
                        .Col = .GetColFromID("출고") : .Text = dt.Rows(ix).Item("outqnt").ToString()
                        .Col = .GetColFromID("반납") : .Text = dt.Rows(ix).Item("rtnqnt").ToString()
                        .Col = .GetColFromID("폐기") : .Text = dt.Rows(ix).Item("abnqnt").ToString()
                        .Col = .GetColFromID("취소") : .Text = dt.Rows(ix).Item("cancelqnt").ToString()
                        .Col = .GetColFromID("Remark") : .Text = dt.Rows(ix).Item("doctorrmk").ToString()
                    Next
                End With

            Else
                MsgBox("조회된 데이터가 없습니다. 접수일자를 다시 확인하세요", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdTransBloodList_ClickEvnent(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdTransBloodList.ClickEvent
        Dim sTnsJubsuNm As String = ""
        Dim sComCd As String = ""

        If e.row < 1 Then Exit Sub

        Me.spdBefBldList.MaxRows = 0

        With Me.spdTransBloodList
            .Row = e.row
            .Col = .GetColFromID("접수번호") : sTnsJubsuNm = .Text
            .Col = .GetColFromID("성분제제코드") : sComCd = .Text

            .Row = -1 : .Col = 1
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture

            .Row = e.row : .Col = 1
            .CellType = FPSpreadADO.CellTypeConstants.CellTypePicture
            .TypePictPicture = GetImgList.getSingleSel()
            .TypePictCenter = True
        End With

        ShowBefOutList(sTnsJubsuNm, sComCd)

    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.rdoGbn0.Checked = True
        sbDisplay_Init()
    End Sub

    Private Sub btnExeCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExeCancel.Click

        Try
            Dim lal_arg As New ArrayList
            Dim li_chkcnt As Integer = 0
            Dim ls_chk As String
            Dim lb_ok As Boolean
            Dim blnAutoSearch As Boolean = False

            ' 취소 처리
            li_chkcnt = 0

            Dim li_stbyCnt As Integer = 0
            Dim li_outCnt As Integer = 0
            Dim li_rtnCnt As Integer = 0
            Dim ls_stcd As String

            With spdBefBldList
                For i As Integer = 0 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("chk") : ls_chk = .Text

                    If ls_chk = "1" Then
                        li_chkcnt += 1

                        .Col = .GetColFromID("statecd") : ls_stcd = .Text

                        If ls_stcd = "3"c Then                        ' 가출고 자료 체크
                            li_stbyCnt += 1
                        ElseIf ls_stcd = "4"c Then                    ' 출고 체크
                            li_outCnt += 1
                        ElseIf ls_stcd = "5"c Or ls_stcd = "6"c Then  ' 반납/폐기 체크
                            li_rtnCnt += 1
                        End If

                        Dim lcls_jubsu As New STU_TnsJubsu

                        .Col = .GetColFromID("tnsjubsuno") : lcls_jubsu.TNSJUBSUNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("comcd_out") : lcls_jubsu.COMCD_OUT = .Text
                        .Col = .GetColFromID("comcd") : lcls_jubsu.COMCD = .Text
                        .Col = .GetColFromID("comordcd") : lcls_jubsu.COMORDCD = .Text
                        .Col = .GetColFromID("owngbn") : lcls_jubsu.OWNGBN = .Text
                        .Col = .GetColFromID("iogbn") : lcls_jubsu.IOGBN = .Text
                        .Col = .GetColFromID("fkocs") : lcls_jubsu.FKOCS = .Text
                        .Col = .GetColFromID("bldno") : lcls_jubsu.BLDNO = .Text.Replace("-", "")
                        .Col = .GetColFromID("statecd") : lcls_jubsu.STATE = .Text
                        .Col = .GetColFromID("regno") : lcls_jubsu.REGNO = .Text
                        .Col = .GetColFromID("orddt") : lcls_jubsu.ORDDATE = .Text.Replace("-", "") '.Substring(0, 8)
                        .Col = .GetColFromID("spccd") : lcls_jubsu.SPCCD = .Text

                        If ls_stcd = "3"c Then lal_arg.Add(lcls_jubsu)

                    End If
                Next

                If li_chkcnt < 1 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "접수 취소할 항목을 선택 하시기 바랍니다.")
                    Return
                End If

                If li_outCnt > 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "취소선택 자료중 반납/폐기된 자료가 선택되었습니다.")
                    Return
                End If

                If li_rtnCnt > 0 Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "출고된 항목을 선택시 취소 작업을 할 수 없습니다.")
                    Return
                End If


                lb_ok = (New JubSu).fn_CntTnsJubsuData(lal_arg)

                If lb_ok = True Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "접수 취소 처리 되었습니다.")
                    btnSearch_Click(Nothing, Nothing)
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, "접수 취소 처리중 오류가 발생 하였습니다.")
                    btnSearch_Click(Nothing, Nothing)
                End If

            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub FGB08_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_FormDesige.sbInti(Me)

    End Sub
End Class
