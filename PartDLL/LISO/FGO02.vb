'>>> MJOCS 처방내역

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports common.commlogin.login

Imports LISAPP.APP_DB
Imports LISAPP.APP_O
Imports LISAPP.APP_O.OrdFn

Public Class FGO02
    Inherits System.Windows.Forms.Form
    Private Const sFile As String = "File : FGO02.vb, Class : O01" & vbTab

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        fnFormInitialize()
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
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lbldtpOrd As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents pnlPatientGbn As System.Windows.Forms.Panel
    Friend WithEvents rdoPatientGbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPatientGbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dtpOrdDT1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpOrdDT0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents chkCOMMCD As System.Windows.Forms.CheckBox
    Friend WithEvents btnReg As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNotRcv As System.Windows.Forms.Button
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents chkOrdDay As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGO02))
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.lblUserId = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.chkCOMMCD = New System.Windows.Forms.CheckBox
        Me.btnReg = New System.Windows.Forms.Button
        Me.Label38 = New System.Windows.Forms.Label
        Me.pnlPatientGbn = New System.Windows.Forms.Panel
        Me.rdoPatientGbn1 = New System.Windows.Forms.RadioButton
        Me.rdoPatientGbn0 = New System.Windows.Forms.RadioButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSearch = New System.Windows.Forms.Button
        Me.lbldtpOrd = New System.Windows.Forms.Label
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.dtpOrdDT1 = New System.Windows.Forms.DateTimePicker
        Me.dtpOrdDT0 = New System.Windows.Forms.DateTimePicker
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.btnNotRcv = New System.Windows.Forms.Button
        Me.chkOrdDay = New System.Windows.Forms.CheckBox
        Me.Panel4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlPatientGbn.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.btnExit)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Controls.Add(Me.lblUserId)
        Me.Panel4.Controls.Add(Me.lblUserNm)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 595)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1012, 34)
        Me.Panel4.TabIndex = 8
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
        Me.btnExit.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(896, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(107, 25)
        Me.btnExit.TabIndex = 189
        Me.btnExit.Text = "종  료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnClear.Location = New System.Drawing.Point(788, 5)
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
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(4, 7)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(84, 20)
        Me.lblUserId.TabIndex = 154
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(92, 7)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(84, 20)
        Me.lblUserNm.TabIndex = 155
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.txtRegNo)
        Me.GroupBox1.Controls.Add(Me.btnCancel)
        Me.GroupBox1.Controls.Add(Me.chkCOMMCD)
        Me.GroupBox1.Controls.Add(Me.btnReg)
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.pnlPatientGbn)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.lbldtpOrd)
        Me.GroupBox1.Controls.Add(Me.lblRegNo)
        Me.GroupBox1.Controls.Add(Me.dtpOrdDT1)
        Me.GroupBox1.Controls.Add(Me.dtpOrdDT0)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(484, 80)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtRegNo.Location = New System.Drawing.Point(92, 48)
        Me.txtRegNo.MaxLength = 8
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(82, 21)
        Me.txtRegNo.TabIndex = 165
        '
        'btnCancel
        '
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancel.Location = New System.Drawing.Point(372, 48)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 22)
        Me.btnCancel.TabIndex = 164
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "D/C 처리"
        '
        'chkCOMMCD
        '
        Me.chkCOMMCD.Checked = True
        Me.chkCOMMCD.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCOMMCD.Location = New System.Drawing.Point(180, 48)
        Me.chkCOMMCD.Name = "chkCOMMCD"
        Me.chkCOMMCD.Size = New System.Drawing.Size(84, 24)
        Me.chkCOMMCD.TabIndex = 163
        Me.chkCOMMCD.Text = "성분제제"
        '
        'btnReg
        '
        Me.btnReg.Enabled = False
        Me.btnReg.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnReg.Location = New System.Drawing.Point(340, 48)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Size = New System.Drawing.Size(60, 22)
        Me.btnReg.TabIndex = 4
        Me.btnReg.TabStop = False
        Me.btnReg.Text = "저  장"
        Me.btnReg.Visible = False
        '
        'Label38
        '
        Me.Label38.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label38.ForeColor = System.Drawing.Color.White
        Me.Label38.Location = New System.Drawing.Point(8, 76)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(84, 22)
        Me.Label38.TabIndex = 160
        Me.Label38.Text = "외래/입원구분"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label38.Visible = False
        '
        'pnlPatientGbn
        '
        Me.pnlPatientGbn.BackColor = System.Drawing.Color.Thistle
        Me.pnlPatientGbn.Controls.Add(Me.rdoPatientGbn1)
        Me.pnlPatientGbn.Controls.Add(Me.rdoPatientGbn0)
        Me.pnlPatientGbn.Controls.Add(Me.Label31)
        Me.pnlPatientGbn.ForeColor = System.Drawing.Color.Indigo
        Me.pnlPatientGbn.Location = New System.Drawing.Point(92, 76)
        Me.pnlPatientGbn.Name = "pnlPatientGbn"
        Me.pnlPatientGbn.Size = New System.Drawing.Size(104, 22)
        Me.pnlPatientGbn.TabIndex = 161
        Me.pnlPatientGbn.TabStop = True
        Me.pnlPatientGbn.Visible = False
        '
        'rdoPatientGbn1
        '
        Me.rdoPatientGbn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoPatientGbn1.Location = New System.Drawing.Point(52, 1)
        Me.rdoPatientGbn1.Name = "rdoPatientGbn1"
        Me.rdoPatientGbn1.Size = New System.Drawing.Size(48, 20)
        Me.rdoPatientGbn1.TabIndex = 1
        Me.rdoPatientGbn1.Tag = "2"
        Me.rdoPatientGbn1.Text = "입원"
        '
        'rdoPatientGbn0
        '
        Me.rdoPatientGbn0.Checked = True
        Me.rdoPatientGbn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoPatientGbn0.Location = New System.Drawing.Point(4, 1)
        Me.rdoPatientGbn0.Name = "rdoPatientGbn0"
        Me.rdoPatientGbn0.Size = New System.Drawing.Size(48, 20)
        Me.rdoPatientGbn0.TabIndex = 0
        Me.rdoPatientGbn0.TabStop = True
        Me.rdoPatientGbn0.Tag = "1"
        Me.rdoPatientGbn0.Text = "외래"
        '
        'Label31
        '
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(104, 22)
        Me.Label31.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(184, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 12)
        Me.Label1.TabIndex = 118
        Me.Label1.Text = "~"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSearch
        '
        Me.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSearch.Location = New System.Drawing.Point(272, 48)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(94, 22)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.TabStop = False
        Me.btnSearch.Text = "검  색"
        '
        'lbldtpOrd
        '
        Me.lbldtpOrd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lbldtpOrd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lbldtpOrd.ForeColor = System.Drawing.Color.White
        Me.lbldtpOrd.Location = New System.Drawing.Point(8, 16)
        Me.lbldtpOrd.Name = "lbldtpOrd"
        Me.lbldtpOrd.Size = New System.Drawing.Size(84, 22)
        Me.lbldtpOrd.TabIndex = 12
        Me.lbldtpOrd.Text = "처방일자"
        Me.lbldtpOrd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.Color.White
        Me.lblRegNo.Location = New System.Drawing.Point(8, 48)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(84, 22)
        Me.lblRegNo.TabIndex = 5
        Me.lblRegNo.Text = "등록번호"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpOrdDT1
        '
        Me.dtpOrdDT1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDT1.Location = New System.Drawing.Point(200, 16)
        Me.dtpOrdDT1.Name = "dtpOrdDT1"
        Me.dtpOrdDT1.Size = New System.Drawing.Size(92, 21)
        Me.dtpOrdDT1.TabIndex = 1
        Me.dtpOrdDT1.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpOrdDT0
        '
        Me.dtpOrdDT0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDT0.Location = New System.Drawing.Point(92, 16)
        Me.dtpOrdDT0.Name = "dtpOrdDT0"
        Me.dtpOrdDT0.Size = New System.Drawing.Size(92, 21)
        Me.dtpOrdDT0.TabIndex = 0
        Me.dtpOrdDT0.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(4, 84)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1004, 508)
        Me.Panel1.TabIndex = 1
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1000, 504)
        Me.spdList.TabIndex = 0
        '
        'btnNotRcv
        '
        Me.btnNotRcv.Location = New System.Drawing.Point(496, 36)
        Me.btnNotRcv.Name = "btnNotRcv"
        Me.btnNotRcv.Size = New System.Drawing.Size(216, 44)
        Me.btnNotRcv.TabIndex = 9
        Me.btnNotRcv.Text = "미수납처방 조회"
        Me.btnNotRcv.Visible = False
        '
        'chkOrdDay
        '
        Me.chkOrdDay.Location = New System.Drawing.Point(496, 12)
        Me.chkOrdDay.Name = "chkOrdDay"
        Me.chkOrdDay.Size = New System.Drawing.Size(228, 20)
        Me.chkOrdDay.TabIndex = 10
        Me.chkOrdDay.Text = "미수납처방 조회 시 처방일자 적용"
        Me.chkOrdDay.Visible = False
        '
        'FGO02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1012, 629)
        Me.Controls.Add(Me.chkOrdDay)
        Me.Controls.Add(Me.btnNotRcv)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel4)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGO02"
        Me.Text = "MJOCS 처방내역"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel4.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlPatientGbn.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " 메인버튼 처리 "
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            fnFormClear()

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " 폼내부 함수 "
    ' 폼 초기설정
    Private Sub fnFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"
        Dim CommFN As New Fn
        Dim ServerDT As New ServerDateTime

        Try
            Me.Tag = "Load"
            Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo

            ' 서버날짜로 설정
            dtpOrdDT0.Value = CDate(ServerDT.GetDate("-"))
            dtpOrdDT1.Value = dtpOrdDT0.Value

            fnFormClear()

            ' 로그인정보 설정
            lblUserId.Text = USER_INFO.USRID
            lblUserNm.Text = USER_INFO.USRNM

            If USER_INFO.USRLVL = "S" Then
                btnReg.Enabled = True
            Else
                btnReg.Enabled = False
            End If

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    ' 화면정리
    Private Sub fnFormClear()
        Dim sFn As String = "Private Sub fnFormClear()"

        Try
            Me.txtRegNo.Text = ""
            Me.spdList.MaxRows = 0

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' 칼럼 Hidden 유무
    Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)
        Dim sFn As String = "Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)"

        Try

        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

#End Region

#Region " Control Event 처리 "
    Private Sub FGO01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If CType(Me.Tag, String) = "Load" Then
            txtRegNo.Focus()

            Me.Tag = ""
        End If
    End Sub

    Private Sub FGO02_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGO01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)
        End Select

    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Dim sFn As String = "btnCancel_Click"

        Dim sOwnGbn As String = ""
        Dim sIoGbn As String = ""
        Dim sFkOCs As String = ""
        Dim sSPCFLG_2 As String = ""
        Dim sMsg As String = ""

        Try
            Dim iDiff% = 0

            With spdList
                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("spcflg_2") : sSPCFLG_2 = .Text

                    '< mod freety 2005/09/15 : Reject의 경우이외에도, 미채혈인 경우도 가능하도록
                    '                          <-- 자체처방 잘못된 경우의 취소, 수혈의뢰접수 화면에 안 보이도록 설정위해
                    If sSPCFLG_2.Equals("R") Then sSPCFLG_2 = ""

                    .Col = .GetColFromID("owngbn") : sOwnGbn = .Text
                    .Col = .GetColFromID("in_out_gubun") : sIoGbn = .Text
                    .Col = .GetColFromID("fkocs") : sFkOCs = .Text

                    .Col = .GetColFromID("bunho")

                    sMsg = ""
                    sMsg &= "등록번호 : " & .Text & vbCrLf & vbCrLf

                    .Col = .GetColFromID("suname")
                    sMsg &= "성명 : " & .Text & vbCrLf & vbCrLf

                    .Col = .GetColFromID("orddt")
                    sMsg &= "처방일시 : " & .Text & vbCrLf & vbCrLf

                    .Col = .GetColFromID("tnmd")
                    sMsg &= "검사 : " & .Text & vbCrLf & vbCrLf

                    sMsg &= "를 취소하시겠습니까?"

                    If MsgBox(sMsg, MsgBoxStyle.YesNo Or MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then Exit Sub

                    fnExe_Order_dcyn(sOwnGbn, sIoGbn, sFkOCs)
                Next

                MsgBox("정상적으로 취소 되었습니다.", MsgBoxStyle.Information, Me.Text)
            End With
        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try
    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String
        Dim sOwnGbn As String = ""
        Dim sIoGbn As String = ""
        Dim sFkOcs As String = ""
        Dim sSpcFlg As String = ""
        Dim sSpcFlg_2 As String = ""

        Try
            Dim iDiff% = 0

            With spdList
                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("owngbn") : sOwnGbn = .Text
                    .Col = .GetColFromID("in_out_gubun") : sIoGbn = .Text
                    .Col = .GetColFromID("fkocs") : sFkOcs = .Text
                    .Col = .GetColFromID("spcflg") : sSpcFlg = .Text
                    .Col = .GetColFromID("spcflg_2") : sSpcFlg_2 = .Text

                    If sSpcFlg <> sSpcFlg_2 Then
                        iDiff += 1
                    End If
                Next

                If iDiff = 0 Then
                    MsgBox("수정된 내역이 없습니다!!", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If

                For i As Integer = 1 To .MaxRows
                    .Row = i
                    .Col = .GetColFromID("owngbn") : sOwnGbn = .Text
                    .Col = .GetColFromID("in_out_gubun") : sIoGbn = .Text
                    .Col = .GetColFromID("fkocs") : sFkOcs = .Text
                    .Col = .GetColFromID("spcflg") : sSpcFlg = .Text
                    .Col = .GetColFromID("spcflg_2") : sSpcFlg_2 = .Text

                    If sSpcFlg = "0" Then sSpcFlg = ""

                    If sSpcFlg <> sSpcFlg_2 Then
                        iDiff += 1
                        fnExe_Order_Status(sOwnGbn, sIoGbn, sFkOcs, sSpcFlg)
                    End If
                Next

                MsgBox("정상적으로 저장 되었습니다.", MsgBoxStyle.Information, Me.Text)
            End With
        Catch ex As Exception
            Fn.log(sFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim sFn As String

        Try

            If Me.txtRegNo.Text <> "" Then
                If IsNumeric(Me.txtRegNo.Text.Substring(0, 1)) Then
                    Me.txtRegNo.Text = Me.txtRegNo.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    Me.txtRegNo.Text = Me.txtRegNo.Text.Substring(0, 1) + Me.txtRegNo.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
            End If

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
            Dim dt As DataTable = fnGet_Order_Info(Me.dtpOrdDT0.Text.Replace("-", ""), Me.dtpOrdDT1.Text.Replace("-", ""), Me.txtRegNo.Text, Me.chkCOMMCD.Checked)
            If dt.Rows.Count < 1 Then spdList.MaxRows = 0 : Return

            With spdList
                .MaxRows = 0

                .MaxRows = dt.Rows.Count
                .MaxCols = dt.Columns.Count

                For iRow As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(iRow).Item("append_yn").ToString = "Y" Then
                        .Row = iRow + 1
                        .Col = -1
                        .ForeColor = Color.Blue

                    End If

                    If dt.Rows(iRow).Item("dc_yn").ToString = "Y" Then
                        .Row = iRow + 1
                        .Col = -1 : .Col2 = .MaxCols
                        .ForeColor = Color.Red
                        .FontStrikethru = True
                        .set_RowHeight(iRow + 1, 12.27)
                    End If

                    If dt.Rows(iRow).Item("tnmd").ToString = "" Then
                        .Row = iRow + 1
                        .Col = -1 : .Col2 = .MaxCols
                        .BackColor = Color.Thistle
                    End If

                    .Row = iRow
                    For iCol As Integer = 0 To dt.Columns.Count - 1
                        If iRow = 0 Then
                            .Col = iCol + 1
                            .Row = 0
                            .Text = dt.Columns(iCol).ColumnName
                            .ColID = dt.Columns(iCol).ColumnName.ToLower()
                        End If

                        .Col = iCol + 1
                        .Row = iRow + 1
                        .Text = dt.Rows(iRow).Item(iCol).ToString

                        '< add freety 2004/11/05
                        '# SPCFLG가 문자인 경우 R, D -> 미채혈 상태이므로 No Check
                        If dt.Columns(iCol).ColumnName = "spcflg" Then
                            If Val(dt.Rows(iRow).Item(iCol).ToString) = 0 Then .Text = ""
                        End If
                        '> add freety 2004/11/05
                    Next
                Next

                .Col = .GetColFromID("spcflg") : .Col2 = .GetColFromID("spcflg") : .Row = 1 : .Row2 = .MaxRows
                .BlockMode = True
                .CellType = FPSpreadADO.CellTypeConstants.CellTypeCheckBox : .Lock = True
                .BlockMode = False

                .Col = .GetColFromID("spcflg_2") : .Col2 = .GetColFromID("spcflg_2") : .Row = 1 : .Row2 = .MaxRows
            End With

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub chkCOMMCD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCOMMCD.CheckedChanged
        btnReg.Enabled = chkCOMMCD.Checked
    End Sub

    Private Sub ntxtRegNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True ': SendKeys.Send("{TAB}")

            btnSearch_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        btnSearch_Click(Nothing, Nothing)

    End Sub
End Class
