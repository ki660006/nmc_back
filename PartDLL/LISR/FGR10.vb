'>>> 종합검증 대상자 접수/취소
Imports System.Drawing

Imports COMMON.CommFN
Imports common.commlogin.login

Public Class FGR10
    Inherits System.Windows.Forms.Form

    Private Const mc_sFile As String = "File : FGR10.vb, Class : FGR10" & vbTab
    Private Const mc_iTkMax As Integer = 15
    Private Const mc_sChk As String = "√ "
    Private Const mc_sToDo As String = "↑ "

    Private miProcessing As Integer = 0

    Private mbLoaded As Boolean = False
    Friend WithEvents spclst01 As AxAckResultViewer.SPCLIST03
    Friend WithEvents btnTk As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents btnHelp_Diag As System.Windows.Forms.Button
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox

    Private m_dt_OrdInfo As DataTable

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        miProcessing = 1

        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        miProcessing = 0
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
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents pnlOptUser As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents pnlList As System.Windows.Forms.Panel
    Friend WithEvents pnlBoard As System.Windows.Forms.Panel
    Friend WithEvents spdBoard As AxFPSpreadADO.AxfpSpread
    Protected WithEvents grpLeft As System.Windows.Forms.GroupBox
    Friend WithEvents grpRight As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDayS As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSDayE As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpSDayS As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdoUserExc As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUserInc As System.Windows.Forms.RadioButton
    Friend WithEvents btnSearchB As System.Windows.Forms.Button
    Friend WithEvents btnQuery As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkUserInc As System.Windows.Forms.CheckBox
    Friend WithEvents chkUserExc As System.Windows.Forms.CheckBox
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlTkMt As System.Windows.Forms.Panel
    Friend WithEvents rdoTkMtC As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTkMtT As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTkMtR As System.Windows.Forms.RadioButton
    Friend WithEvents btnGv As System.Windows.Forms.Button
    Friend WithEvents lblChkNo As System.Windows.Forms.Label
    Friend WithEvents lblToDoNo As System.Windows.Forms.Label
    Friend WithEvents sortspd1 As AxAckSortSpd.AxAckSortSpd
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGR10))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.grpLeft = New System.Windows.Forms.GroupBox
        Me.lblDay = New System.Windows.Forms.Label
        Me.sortspd1 = New AxAckSortSpd.AxAckSortSpd
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.btnQuery = New System.Windows.Forms.Button
        Me.pnlOptUser = New System.Windows.Forms.Panel
        Me.rdoUserExc = New System.Windows.Forms.RadioButton
        Me.rdoUserInc = New System.Windows.Forms.RadioButton
        Me.pnlList = New System.Windows.Forms.Panel
        Me.dtpDayE = New System.Windows.Forms.DateTimePicker
        Me.dtpDayS = New System.Windows.Forms.DateTimePicker
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.grpRight = New System.Windows.Forms.GroupBox
        Me.lblOrdDt = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkUserExc = New System.Windows.Forms.CheckBox
        Me.chkUserInc = New System.Windows.Forms.CheckBox
        Me.btnSearchB = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.dtpSDayE = New System.Windows.Forms.DateTimePicker
        Me.dtpSDayS = New System.Windows.Forms.DateTimePicker
        Me.pnlBoard = New System.Windows.Forms.Panel
        Me.spdBoard = New AxFPSpreadADO.AxfpSpread
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.btnTk = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.lblToDoNo = New System.Windows.Forms.Label
        Me.lblChkNo = New System.Windows.Forms.Label
        Me.btnGv = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.pnlTkMt = New System.Windows.Forms.Panel
        Me.rdoTkMtC = New System.Windows.Forms.RadioButton
        Me.rdoTkMtT = New System.Windows.Forms.RadioButton
        Me.rdoTkMtR = New System.Windows.Forms.RadioButton
        Me.spclst01 = New AxAckResultViewer.SPCLIST03
        Me.btnHelp_Diag = New System.Windows.Forms.Button
        Me.grpLeft.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOptUser.SuspendLayout()
        Me.pnlList.SuspendLayout()
        Me.grpRight.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlBoard.SuspendLayout()
        CType(Me.spdBoard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.pnlTkMt.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLeft
        '
        Me.grpLeft.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpLeft.Controls.Add(Me.lblDay)
        Me.grpLeft.Controls.Add(Me.sortspd1)
        Me.grpLeft.Controls.Add(Me.btnQuery)
        Me.grpLeft.Controls.Add(Me.pnlOptUser)
        Me.grpLeft.Controls.Add(Me.pnlList)
        Me.grpLeft.Controls.Add(Me.dtpDayE)
        Me.grpLeft.Controls.Add(Me.dtpDayS)
        Me.grpLeft.Controls.Add(Me.Label32)
        Me.grpLeft.Location = New System.Drawing.Point(0, 0)
        Me.grpLeft.Name = "grpLeft"
        Me.grpLeft.Size = New System.Drawing.Size(374, 611)
        Me.grpLeft.TabIndex = 0
        Me.grpLeft.TabStop = False
        '
        'lblDay
        '
        Me.lblDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDay.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDay.ForeColor = System.Drawing.Color.White
        Me.lblDay.Location = New System.Drawing.Point(8, 14)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(79, 21)
        Me.lblDay.TabIndex = 31
        Me.lblDay.Text = "입원일자"
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'sortspd1
        '
        Me.sortspd1.BackColor = System.Drawing.SystemColors.Control
        Me.sortspd1.ColNumber = 3
        Me.sortspd1.ColWidth = 63
        Me.sortspd1.Location = New System.Drawing.Point(6, 86)
        Me.sortspd1.Name = "sortspd1"
        Me.sortspd1.RowNumber = 1
        Me.sortspd1.Size = New System.Drawing.Size(298, 27)
        Me.sortspd1.Spread6ToSort = Me.spdList
        Me.sortspd1.TabIndex = 9
        Me.sortspd1.UseSortButton = True
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(360, 490)
        Me.spdList.TabIndex = 3
        '
        'btnQuery
        '
        Me.btnQuery.BackColor = System.Drawing.Color.AliceBlue
        Me.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuery.Location = New System.Drawing.Point(183, 39)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Size = New System.Drawing.Size(137, 45)
        Me.btnQuery.TabIndex = 8
        Me.btnQuery.Text = "대상자 조회(&S)"
        Me.btnQuery.UseVisualStyleBackColor = False
        '
        'pnlOptUser
        '
        Me.pnlOptUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlOptUser.Controls.Add(Me.rdoUserExc)
        Me.pnlOptUser.Controls.Add(Me.rdoUserInc)
        Me.pnlOptUser.Location = New System.Drawing.Point(8, 39)
        Me.pnlOptUser.Name = "pnlOptUser"
        Me.pnlOptUser.Size = New System.Drawing.Size(168, 45)
        Me.pnlOptUser.TabIndex = 7
        '
        'rdoUserExc
        '
        Me.rdoUserExc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUserExc.ForeColor = System.Drawing.Color.Black
        Me.rdoUserExc.Location = New System.Drawing.Point(20, 24)
        Me.rdoUserExc.Name = "rdoUserExc"
        Me.rdoUserExc.Size = New System.Drawing.Size(124, 18)
        Me.rdoUserExc.TabIndex = 1
        Me.rdoUserExc.Text = "전문의(타인) 접수"
        '
        'rdoUserInc
        '
        Me.rdoUserInc.Checked = True
        Me.rdoUserInc.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoUserInc.ForeColor = System.Drawing.Color.Black
        Me.rdoUserInc.Location = New System.Drawing.Point(20, 3)
        Me.rdoUserInc.Name = "rdoUserInc"
        Me.rdoUserInc.Size = New System.Drawing.Size(124, 18)
        Me.rdoUserInc.TabIndex = 0
        Me.rdoUserInc.TabStop = True
        Me.rdoUserInc.Text = "전문의(본인) 접수"
        '
        'pnlList
        '
        Me.pnlList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlList.Controls.Add(Me.spdList)
        Me.pnlList.Location = New System.Drawing.Point(8, 113)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(360, 490)
        Me.pnlList.TabIndex = 10
        '
        'dtpDayE
        '
        Me.dtpDayE.CustomFormat = "yyyy-MM"
        Me.dtpDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayE.Location = New System.Drawing.Point(223, 14)
        Me.dtpDayE.Name = "dtpDayE"
        Me.dtpDayE.Size = New System.Drawing.Size(96, 21)
        Me.dtpDayE.TabIndex = 6
        Me.dtpDayE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpDayS
        '
        Me.dtpDayS.CustomFormat = "yyyy-MM"
        Me.dtpDayS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDayS.Location = New System.Drawing.Point(88, 14)
        Me.dtpDayS.Name = "dtpDayS"
        Me.dtpDayS.Size = New System.Drawing.Size(96, 21)
        Me.dtpDayS.TabIndex = 4
        Me.dtpDayS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(197, 18)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(14, 12)
        Me.Label32.TabIndex = 5
        Me.Label32.Text = "~"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.BackColor = System.Drawing.Color.DarkSlateGray
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(379, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(238, 22)
        Me.Label2.TabIndex = 93
        Me.Label2.Text = "처방 / 접수 / 결과   상태"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpRight
        '
        Me.grpRight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpRight.Controls.Add(Me.lblOrdDt)
        Me.grpRight.Controls.Add(Me.Panel1)
        Me.grpRight.Controls.Add(Me.btnSearchB)
        Me.grpRight.Controls.Add(Me.Label5)
        Me.grpRight.Controls.Add(Me.dtpSDayE)
        Me.grpRight.Controls.Add(Me.dtpSDayS)
        Me.grpRight.Controls.Add(Me.pnlBoard)
        Me.grpRight.Location = New System.Drawing.Point(701, 0)
        Me.grpRight.Name = "grpRight"
        Me.grpRight.Size = New System.Drawing.Size(347, 611)
        Me.grpRight.TabIndex = 2
        Me.grpRight.TabStop = False
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.Color.Black
        Me.lblOrdDt.Location = New System.Drawing.Point(8, 16)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(63, 21)
        Me.lblOrdDt.TabIndex = 24
        Me.lblOrdDt.Text = "검증현황"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Panel1.Controls.Add(Me.chkUserExc)
        Me.Panel1.Controls.Add(Me.chkUserInc)
        Me.Panel1.Location = New System.Drawing.Point(8, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(254, 22)
        Me.Panel1.TabIndex = 4
        '
        'chkUserExc
        '
        Me.chkUserExc.Location = New System.Drawing.Point(130, 4)
        Me.chkUserExc.Name = "chkUserExc"
        Me.chkUserExc.Size = New System.Drawing.Size(123, 15)
        Me.chkUserExc.TabIndex = 3
        Me.chkUserExc.Text = "전문의(타인) 접수"
        Me.chkUserExc.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'chkUserInc
        '
        Me.chkUserInc.Checked = True
        Me.chkUserInc.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUserInc.Location = New System.Drawing.Point(5, 4)
        Me.chkUserInc.Name = "chkUserInc"
        Me.chkUserInc.Size = New System.Drawing.Size(123, 15)
        Me.chkUserInc.TabIndex = 2
        Me.chkUserInc.Text = "전문의(본인) 접수"
        Me.chkUserInc.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'btnSearchB
        '
        Me.btnSearchB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSearchB.BackColor = System.Drawing.Color.AliceBlue
        Me.btnSearchB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchB.Location = New System.Drawing.Point(267, 16)
        Me.btnSearchB.Name = "btnSearchB"
        Me.btnSearchB.Size = New System.Drawing.Size(73, 47)
        Me.btnSearchB.TabIndex = 5
        Me.btnSearchB.Text = "조회"
        Me.btnSearchB.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(160, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(14, 12)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "~"
        '
        'dtpSDayE
        '
        Me.dtpSDayE.CustomFormat = "yyyy-MM"
        Me.dtpSDayE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSDayE.Location = New System.Drawing.Point(174, 16)
        Me.dtpSDayE.Name = "dtpSDayE"
        Me.dtpSDayE.Size = New System.Drawing.Size(88, 21)
        Me.dtpSDayE.TabIndex = 3
        Me.dtpSDayE.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'dtpSDayS
        '
        Me.dtpSDayS.CustomFormat = "yyyy-MM"
        Me.dtpSDayS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpSDayS.Location = New System.Drawing.Point(72, 16)
        Me.dtpSDayS.Name = "dtpSDayS"
        Me.dtpSDayS.Size = New System.Drawing.Size(88, 21)
        Me.dtpSDayS.TabIndex = 1
        Me.dtpSDayS.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'pnlBoard
        '
        Me.pnlBoard.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBoard.Controls.Add(Me.spdBoard)
        Me.pnlBoard.Location = New System.Drawing.Point(8, 64)
        Me.pnlBoard.Name = "pnlBoard"
        Me.pnlBoard.Size = New System.Drawing.Size(332, 539)
        Me.pnlBoard.TabIndex = 6
        '
        'spdBoard
        '
        Me.spdBoard.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdBoard.DataSource = Nothing
        Me.spdBoard.Location = New System.Drawing.Point(0, 0)
        Me.spdBoard.Name = "spdBoard"
        Me.spdBoard.OcxState = CType(resources.GetObject("spdBoard.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBoard.Size = New System.Drawing.Size(332, 539)
        Me.spdBoard.TabIndex = 4
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.txtRegNo)
        Me.pnlBottom.Controls.Add(Me.btnTk)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Controls.Add(Me.lblToDoNo)
        Me.pnlBottom.Controls.Add(Me.lblChkNo)
        Me.pnlBottom.Controls.Add(Me.btnGv)
        Me.pnlBottom.Controls.Add(Me.Label3)
        Me.pnlBottom.Controls.Add(Me.pnlTkMt)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 612)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1052, 34)
        Me.pnlBottom.TabIndex = 3
        '
        'txtRegNo
        '
        Me.txtRegNo.Location = New System.Drawing.Point(162, 9)
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.Size = New System.Drawing.Size(77, 21)
        Me.txtRegNo.TabIndex = 207
        Me.txtRegNo.Visible = False
        '
        'btnTk
        '
        Me.btnTk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTk.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnTk.ColorFillBlend = CBlendItems1
        Me.btnTk.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnTk.Corners.All = CType(6, Short)
        Me.btnTk.Corners.LowerLeft = CType(6, Short)
        Me.btnTk.Corners.LowerRight = CType(6, Short)
        Me.btnTk.Corners.UpperLeft = CType(6, Short)
        Me.btnTk.Corners.UpperRight = CType(6, Short)
        Me.btnTk.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnTk.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnTk.FocalPoints.CenterPtX = 0.5!
        Me.btnTk.FocalPoints.CenterPtY = 0.08!
        Me.btnTk.FocalPoints.FocusPtX = 0.0!
        Me.btnTk.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTk.FocusPtTracker = DesignerRectTracker2
        Me.btnTk.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTk.ForeColor = System.Drawing.Color.White
        Me.btnTk.Image = Nothing
        Me.btnTk.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTk.ImageIndex = 0
        Me.btnTk.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnTk.Location = New System.Drawing.Point(848, 5)
        Me.btnTk.Name = "btnTk"
        Me.btnTk.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnTk.SideImage = Nothing
        Me.btnTk.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTk.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnTk.Size = New System.Drawing.Size(96, 25)
        Me.btnTk.TabIndex = 206
        Me.btnTk.Text = "대상자 접수"
        Me.btnTk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTk.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnTk.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
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
        Me.btnExit.FocalPoints.CenterPtY = 0.08!
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
        Me.btnExit.Location = New System.Drawing.Point(947, 5)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(96, 25)
        Me.btnExit.TabIndex = 205
        Me.btnExit.Text = "종  료(ESC)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'lblToDoNo
        '
        Me.lblToDoNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToDoNo.BackColor = System.Drawing.Color.Transparent
        Me.lblToDoNo.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblToDoNo.ForeColor = System.Drawing.Color.Brown
        Me.lblToDoNo.Location = New System.Drawing.Point(797, 7)
        Me.lblToDoNo.Name = "lblToDoNo"
        Me.lblToDoNo.Size = New System.Drawing.Size(40, 19)
        Me.lblToDoNo.TabIndex = 7
        Me.lblToDoNo.Text = "↑ 15"
        Me.lblToDoNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblChkNo
        '
        Me.lblChkNo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblChkNo.BackColor = System.Drawing.Color.Transparent
        Me.lblChkNo.Font = New System.Drawing.Font("굴림", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblChkNo.Location = New System.Drawing.Point(753, 7)
        Me.lblChkNo.Name = "lblChkNo"
        Me.lblChkNo.Size = New System.Drawing.Size(40, 19)
        Me.lblChkNo.TabIndex = 6
        Me.lblChkNo.Text = "√ 15"
        Me.lblChkNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnGv
        '
        Me.btnGv.BackColor = System.Drawing.Color.AliceBlue
        Me.btnGv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGv.Location = New System.Drawing.Point(8, 4)
        Me.btnGv.Name = "btnGv"
        Me.btnGv.Size = New System.Drawing.Size(130, 25)
        Me.btnGv.TabIndex = 0
        Me.btnGv.Tag = "종합검증"
        Me.btnGv.Text = "→ 종합검증"
        Me.btnGv.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(417, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 22)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "접수방법"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlTkMt
        '
        Me.pnlTkMt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTkMt.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.pnlTkMt.Controls.Add(Me.rdoTkMtC)
        Me.pnlTkMt.Controls.Add(Me.rdoTkMtT)
        Me.pnlTkMt.Controls.Add(Me.rdoTkMtR)
        Me.pnlTkMt.Location = New System.Drawing.Point(493, 6)
        Me.pnlTkMt.Name = "pnlTkMt"
        Me.pnlTkMt.Size = New System.Drawing.Size(256, 22)
        Me.pnlTkMt.TabIndex = 2
        '
        'rdoTkMtC
        '
        Me.rdoTkMtC.Checked = True
        Me.rdoTkMtC.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTkMtC.ForeColor = System.Drawing.Color.Black
        Me.rdoTkMtC.Location = New System.Drawing.Point(172, 2)
        Me.rdoTkMtC.Name = "rdoTkMtC"
        Me.rdoTkMtC.Size = New System.Drawing.Size(72, 18)
        Me.rdoTkMtC.TabIndex = 2
        Me.rdoTkMtC.TabStop = True
        Me.rdoTkMtC.Text = "Checked"
        '
        'rdoTkMtT
        '
        Me.rdoTkMtT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTkMtT.ForeColor = System.Drawing.Color.Black
        Me.rdoTkMtT.Location = New System.Drawing.Point(105, 2)
        Me.rdoTkMtT.Name = "rdoTkMtT"
        Me.rdoTkMtT.Size = New System.Drawing.Size(48, 18)
        Me.rdoTkMtT.TabIndex = 1
        Me.rdoTkMtT.Text = "Top"
        '
        'rdoTkMtR
        '
        Me.rdoTkMtR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoTkMtR.ForeColor = System.Drawing.Color.Black
        Me.rdoTkMtR.Location = New System.Drawing.Point(16, 3)
        Me.rdoTkMtR.Name = "rdoTkMtR"
        Me.rdoTkMtR.Size = New System.Drawing.Size(72, 18)
        Me.rdoTkMtR.TabIndex = 0
        Me.rdoTkMtR.Text = "Random"
        '
        'spclst01
        '
        Me.spclst01.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spclst01.CheckUseMode = False
        Me.spclst01.Location = New System.Drawing.Point(379, 33)
        Me.spclst01.Name = "spclst01"
        Me.spclst01.Size = New System.Drawing.Size(318, 570)
        Me.spclst01.TabIndex = 94
        Me.spclst01.UseDebug = False
        Me.spclst01.UseMode = 0
        Me.spclst01.UseTempRstState = False
        '
        'btnHelp_Diag
        '
        Me.btnHelp_Diag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnHelp_Diag.BackColor = System.Drawing.Color.AliceBlue
        Me.btnHelp_Diag.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHelp_Diag.Location = New System.Drawing.Point(572, 8)
        Me.btnHelp_Diag.Name = "btnHelp_Diag"
        Me.btnHelp_Diag.Size = New System.Drawing.Size(125, 22)
        Me.btnHelp_Diag.TabIndex = 95
        Me.btnHelp_Diag.Text = "진단 조회"
        Me.btnHelp_Diag.UseVisualStyleBackColor = False
        '
        'FGR10
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1052, 646)
        Me.Controls.Add(Me.btnHelp_Diag)
        Me.Controls.Add(Me.spclst01)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pnlBottom)
        Me.Controls.Add(Me.grpLeft)
        Me.Controls.Add(Me.grpRight)
        Me.KeyPreview = True
        Me.Name = "FGR10"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "종합검증 대상자 접수/취소"
        Me.grpLeft.ResumeLayout(False)
        Me.grpLeft.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOptUser.ResumeLayout(False)
        Me.pnlList.ResumeLayout(False)
        Me.grpRight.ResumeLayout(False)
        Me.grpRight.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlBoard.ResumeLayout(False)
        CType(Me.spdBoard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.pnlTkMt.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function fnGv_Tk_Rows() As ArrayList
        Dim sFn As String = "fnGv_Tk_Rows"

        Dim al_return As New ArrayList

        Dim iToDo As Integer = CType(Me.lblToDoNo.Text.Replace(mc_sToDo, ""), Integer)
        Dim iChk As Integer = 0

        Try
            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Dim iMin As Integer = 1
            Dim iMax As Integer = spd.MaxRows
            Dim iRow As Integer = 0

            If Me.rdoTkMtC.Checked Then
                'Check
                For i As Integer = 1 To spd.MaxRows
                    spd.Col = spd.GetColFromID("chk")
                    spd.Row = i

                    If spd.Text = "1" Then
                        iChk += 1

                        If iChk <= iToDo Then
                            al_return.Add(i)
                        Else
                            Exit For
                        End If
                    End If
                Next

            ElseIf Me.rdoTkMtR.Checked Then
                'Random
                Randomize()

                Do
                    iRow = CInt((iMax * Rnd()) + iMin)

                    If al_return.Contains(iRow) = False Then
                        al_return.Add(iRow)
                    End If
                Loop Until al_return.Count = iToDo

            ElseIf Me.rdoTkMtT.Checked Then
                'Top
                For i As Integer = 1 To iToDo
                    If i <= spd.MaxRows Then
                        al_return.Add(i)
                    End If
                Next

            End If

            al_return.Sort()

            Return al_return

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Function

    Private Sub sbConfig_SortSpd(ByVal riType As Integer)
        Dim sFn As String = "sbConfig_SortSpd"

        Try
            Dim al_sortinfo As New ArrayList
            Dim si As AxAckSortSpd.SortingInfo

            Select Case riType
                Case 0
                    With Me.spdList
                        'DPC가중치
                        si = New AxAckSortSpd.SortingInfo

                        .Col = .GetColFromID("dpc_weight")
                        .Row = 0

                        si.ColumnName = .Text
                        si.ColumnId = .ColID
                        si.ColumnNo = .Col
                        si.ColumnDesc = True

                        al_sortinfo.Add(si)

                        si = Nothing

                        '입원일자
                        si = New AxAckSortSpd.SortingInfo

                        .Col = .GetColFromID("baseday")
                        .Row = 0

                        si.ColumnName = .Text
                        si.ColumnId = .ColID
                        si.ColumnNo = .Col
                        si.ColumnDesc = False

                        al_sortinfo.Add(si)

                        si = Nothing

                        '등록번호
                        si = New AxAckSortSpd.SortingInfo

                        .Col = .GetColFromID("regno")
                        .Row = 0

                        si.ColumnName = .Text
                        si.ColumnId = .ColID
                        si.ColumnNo = .Col
                        si.ColumnDesc = False

                        al_sortinfo.Add(si)

                        si = Nothing

                        '성명
                        si = New AxAckSortSpd.SortingInfo

                        .Col = .GetColFromID("patnm")
                        .Row = 0

                        si.ColumnName = .Text
                        si.ColumnId = .ColID
                        si.ColumnNo = .Col
                        si.ColumnDesc = False

                        al_sortinfo.Add(si)

                        si = Nothing

                        '미생물
                        si = New AxAckSortSpd.SortingInfo

                        .Col = .GetColFromID("mb_yn")
                        .Row = 0

                        si.ColumnName = .Text
                        si.ColumnId = .ColID
                        si.ColumnNo = .Col
                        si.ColumnDesc = False

                        al_sortinfo.Add(si)

                        si = Nothing
                    End With

                    Me.sortspd1.set_ColumnRowSize(3, 1)
                    Me.sortspd1.Columns = al_sortinfo
                    Me.sortspd1.Spread6ToSort = Me.spdList

                Case 1

            End Select

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplay_Board()
        Dim sFn As String = "sbDisplay_Board"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdBoard

            Dim dt As DataTable

            Dim iMode As Integer = 0

            '화면초기화
            sbDisplayInit_Right_List()

            If Me.chkUserInc.Checked Then
                iMode += 1
            End If

            If Me.chkUserExc.Checked Then
                iMode += 2
            End If

            If iMode = 0 Then
                MsgBox(Me.chkUserInc.Text + " 또는 " + Me.chkUserExc.Text + "를 선택하여 주십시요!!", MsgBoxStyle.Information)

                Return
            End If

            dt = LISAPP.APP_G.CommFn.fnGet_Board(Me.dtpSDayS.Text.Replace("-", ""), _
                                      Me.dtpSDayE.Text.Replace("-", ""), iMode, USER_INFO.USRID)

            If dt.Rows.Count > 0 Then
                Ctrl.DisplayAfterSelect(spd, dt, True)
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Chk_Current(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Chk_Current"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim iChk As Integer = 0

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            miProcessing = 1

            If Me.rdoTkMtC.Checked Then
                For i As Integer = 1 To spd.MaxRows
                    spd.Col = spd.GetColFromID("chk")
                    spd.Row = i

                    If spd.Text = "1" Then
                        iChk += 1
                    End If
                Next

                If Integer.Parse(Me.lblToDoNo.Text.Replace(mc_sToDo, "")) + 5 < iChk Then
                    MsgBox("선택할 수 있는 개수를 초과하였습니다!!", MsgBoxStyle.Information)

                    If riRow > 0 Then
                        spd.Col = spd.GetColFromID("chk")
                        spd.Row = riRow
                        spd.Text = ""

                        iChk -= 1
                    End If
                End If

                Me.lblChkNo.Text = mc_sChk + iChk.ToString()
            Else
                If spd.MaxRows > 0 Then
                    spd.ClearRange(spd.GetColFromID("chk"), 1, spd.GetColFromID("chk"), spd.MaxRows, True)
                End If

                Me.lblChkNo.Text = mc_sChk + 0.ToString()
            End If

            miProcessing = 0

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_List_ToTk()
        Dim sFn As String = "sbDisplay_List_ToTk"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Dim dt As DataTable
            Dim a_dr As DataRow()
            Dim iMode As Integer = 0

            '화면초기화
            sbDisplayInit_Left_List(0)

            Me.spclst01.Clear()

            If Me.OwnedForms.Length > 0 Then
                CType(Me.OwnedForms(0), POPUPWIN.FGPOPUPRST).Clear()
            End If

            'Data Access
            dt = LISAPP.APP_G.CommFn.Get_List_ToTk(Me.dtpDayS.Text.Replace("-", ""), Me.dtpDayE.Text.Replace("-", ""))

            If dt.Rows.Count > 0 Then
                a_dr = dt.Select("gv_yn = 'N'", "baseday asc, regno asc")
                Ctrl.DisplayAfterSelect(spd, a_dr, True)

                For intRow As Integer = 1 To spd.MaxRows
                    Dim intCol As Integer = spd.GetColFromID("gv_yn")
                    If intCol > 0 Then
                        spd.Row = intRow
                        spd.Col = intCol
                        If spd.Text = "Y" Then
                            spd.Row = intRow
                            spd.Action = FPSpreadADO.ActionConstants.ActionDeleteRow
                            spd.MaxRows -= 1
                        End If
                    End If
                Next

                spd.SetActiveCell(0, 0)

                'SortSpd 설정하기
                sbConfig_SortSpd(0)
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_List_ToTk_RegNo(ByVal rsRegNo As String, ByVal rsEntDay As String)
        Dim sFn As String = "sbDisplay_List_ToTk_RegNo"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Dim dt As DataTable
            Dim iMode As Integer = 0

            dt = LISAPP.APP_G.CommFn.Get_List_ToTk_RegNo(rsRegNo, rsEntDay.Replace("-", ""))

            m_dt_OrdInfo = dt.Copy()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_List_ToTk_RegNo(ByVal rsRegNo As String, ByVal rsOrdDt As String, ByVal rsFkOcs As String)
        Dim sFn As String = "sbDisplay_List_ToTk_RegNo(String, String, String)"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

            Dim dt As DataTable
            Dim iMode As Integer = 0

            dt = LISAPP.APP_G.CommFn.Get_List_ToTk_RegNo(rsRegNo, rsOrdDt.Replace("-", ""), rsFkOcs)

            m_dt_OrdInfo = dt.Copy()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_ToDo_Today()
        Dim sFn As String = "sbDisplay_ToDo_Today"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            '전문의(본인) 접수 현황 재조사
            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Dim dt As DataTable = LISAPP.APP_G.CommFn.fnGet_Board(dtNow.ToShortDateString().Replace("-", ""), dtNow.ToShortDateString().Replace("-", ""), _
                                                        1, USER_INFO.USRID)

            Dim iToDo As Integer = mc_iTkMax
            Dim iChk As Integer = 0

            If dt.Rows.Count > 0 Then
                iToDo -= CType(dt.Rows(0).Item("tkcnt"), Integer)
            End If

            Me.lblToDoNo.Text = mc_sToDo + iToDo.ToString()

            sbDisplay_Chk_Current(0)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplay_Status(ByVal riRow As Integer)
        Dim sFn As String = "sbDisplay_Status"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            Dim sEntDt As String = ""
            Dim sOrdDay_Last As String = ""
            Dim sRegNo As String = ""

            With Me.spdList
                .Row = riRow
                .Col = .GetColFromID("baseday") : sEntDt = .Text
                .Col = .GetColFromID("regno") : sRegNo = .Text : Me.txtRegNo.Text = sRegNo
                .Col = .GetColFromID("ordday_last") : sOrdDay_Last = .Text
            End With

            Me.spclst01.Display_OrderList(sRegNo, sEntDt.Replace("-", ""), sOrdDay_Last.Replace("-", ""))

            If Me.OwnedForms.Length > 0 Then
                CType(Me.OwnedForms(0), POPUPWIN.FGPOPUPRST).Clear()
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "sbDisplayInit"

        Try
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            miProcessing = 1

            sbDisplayInit_Left()

            sbDisplayInit_Center()

            sbDisplayInit_Right()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            miProcessing = 0

            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbDisplayInit_Center()
        Dim sFn As String = "sbDisplayInit_Center"

        Try
            Me.spclst01.Clear()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Left()
        Dim sFn As String = "sbDisplayInit_Left"

        Try
            sbDisplayInit_Left_Day(0)

            sbDisplayInit_Left_Opt(0)

            sbDisplayInit_Left_List(0)

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Left_Day(ByVal riMode As Integer)
        Dim sFn As String = "sbDisplayInit_Left_Day"

        Try
            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Select Case riMode
                Case 0
                    Me.lblDay.Text = "입원일자"
                    Me.lblDay.BackColor = Drawing.Color.Brown

                    Me.dtpDayE.Value = dtNow.AddDays(-1)
                    Me.dtpDayS.Value = dtNow.AddDays(-3)

                    With spdList
                        .Col = .GetColFromID("orddt") : .ColHidden = True
                        .Col = .GetColFromID("baseday") : .ColHidden = False
                    End With
                Case 1
                    Me.lblDay.Text = "접수일자"
                    Me.lblDay.BackColor = Drawing.Color.Navy

                    Me.dtpDayS.Value = Me.dtpDayE.Value

                Case 2
                    Me.lblDay.Text = "처방일자"
                    Me.lblDay.BackColor = Drawing.Color.Brown

                    Me.dtpDayE.Value = dtNow '.AddDays(-1)
                    Me.dtpDayS.Value = dtNow.AddDays(-3)

                    With spdList
                        .Col = .GetColFromID("orddt") : .ColHidden = False
                        .Col = .GetColFromID("baseday") : .ColHidden = True
                    End With

            End Select

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Left_List(ByVal riMode As Integer)
        Dim sFn As String = "sbDisplayInit_Left_List"

        Try
            With Me.spdList
                .SetText(.GetColFromID("baseday"), 0, Me.lblDay.Text)

                .UserColAction = FPSpreadADO.UserColActionConstants.UserColActionSort

                Select Case riMode
                    Case 0
                        .Col = .GetColFromID("bcnor")
                        .ColHidden = True

                        .Col = .GetColFromID("d")
                        .ColHidden = False

                        .Col = .GetColFromID("p")
                        .ColHidden = False

                        .Col = .GetColFromID("c")
                        .ColHidden = False

                        .Col = .GetColFromID("r")
                        .ColHidden = True

                    Case 1
                        .Col = .GetColFromID("bcnor")
                        .ColHidden = False

                        .Col = .GetColFromID("d")
                        .ColHidden = True

                        .Col = .GetColFromID("p")
                        .ColHidden = True

                        .Col = .GetColFromID("c")
                        .ColHidden = True

                        .Col = .GetColFromID("r")
                        .ColHidden = False

                End Select

#If DEBUG Then
                .Col = .GetColFromID("rstflg")
                .ColHidden = False

                .Col = .GetColFromID("bcno")
                .ColHidden = False

                .Col = .GetColFromID("ordday_last")
                .ColHidden = False
#Else
                .Col = .GetColFromID("rstflag")
                .ColHidden = True

                .Col = .GetColFromID("bcno")
                .ColHidden = True

                .Col = .GetColFromID("ordday_last")
                .ColHidden = True
#End If

                .set_ColUserSortIndicator(.GetColFromID("rstflag"), FPSpreadADO.ColUserSortIndicatorConstants.ColUserSortIndicatorAscending)

                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Left_Opt(ByVal riMode As Integer)
        Dim sFn As String = "sbDisplayInit_Left_Opt"

        Try
            Select Case riMode
                Case 0
                    Me.pnlOptUser.Enabled = False

                    Me.btnQuery.Text = "대상자 조회(&S)"

                Case 1
                    Me.pnlOptUser.Enabled = True

                    Me.btnQuery.Text = "접수자 조회(&S)"

            End Select

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Right()
        Dim sFn As String = "sbDisplayInit_Right"

        Try
            Dim dtNow As Date = New LISAPP.APP_DB.ServerDateTime().GetDateTime

            Me.dtpSDayE.Value = dtNow
            Me.dtpSDayS.Value = dtNow

            Me.spdBoard.MaxRows = 0

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbDisplayInit_Right_List()
        Dim sFn As String = "sbDisplayInit_Right_List"

        Try
            With Me.spdBoard
                .MaxRows = 0
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub sbGv_Tk()
        Dim sFn As String = "sbGv_Tk"

        Dim al_rows As ArrayList = fnGv_Tk_Rows()
        Dim al_sucs As New ArrayList
        Dim si As SYSIF01.SYSIF
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            Dim dtSysDate As Date = Fn.GetServerDateTime()

            si = New SYSIF01.SYSIF(USER_INFO.USRID, USER_INFO.LOCALIP)

            Dim oi As SYSIF01.OrderInfo

            For i As Integer = 1 To al_rows.Count
                oi = New SYSIF01.OrderInfo

                Dim al_testcd As New ArrayList
                Dim al_spccd As New ArrayList
                Dim al_edigbn As New ArrayList
                Dim al_trmk As New ArrayList
                Dim al_emer As New ArrayList
                Dim al_fkocs As New ArrayList

                Dim iSuc As Integer = 0
                Dim sErrMsg As String = ""
                Dim al_return As New ArrayList

                If spd.GetColFromID("regno") * spd.GetColFromID("baseday") = 0 Then
                    MsgBox("Column ID 오류 발생!!")

                    Return
                End If

                spd.Row = Convert.ToInt32(al_rows(i - 1))
                spd.Col = spd.GetColFromID("regno") : Dim sRegNo As String = spd.Text
                spd.Col = spd.GetColFromID("baseday") : Dim sEntDay As String = spd.Text
                spd.Col = spd.GetColFromID("owngbn") : Dim sOwnGbn As String = spd.Text

                sbDisplay_List_ToTk_RegNo(sRegNo, sEntDay) 'pkg_ack_gv.pkg_get_totake_regno (환자의 종합검증 접수안된검사(오더)정보 조회)

                If Not m_dt_OrdInfo Is Nothing Then
                    Dim sPatInfo() As String = m_dt_OrdInfo.Rows(0).Item("patinfo").ToString().Split("|"c)
                    '< 나이계산
                    Dim dtBirthDay As Date = CDate(sPatInfo(2).Trim)
                    Dim iAge As Integer = CType(DateDiff(DateInterval.Year, dtBirthDay, dtSysDate), Integer)

                    If Format(dtBirthDay, "MMdd").ToString > Format(dtSysDate, "MMdd").ToString Then iAge -= 1
                    '>

                    'Order Info
                    oi.OrderDay = m_dt_OrdInfo.Rows(0).Item("orderday").ToString()
                    oi.RegNo = sRegNo
                    oi.PatNm = sPatInfo(0)
                    oi.BirthDay = IIf(sPatInfo(2).Trim.Length = 10, sPatInfo(2), Fn.Format_Day8ToDay10(sPatInfo(2).Trim)).ToString
                    oi.Sex = sPatInfo(1)
                    oi.Age = iAge.ToString
                    oi.DAge = CType(DateDiff(DateInterval.Day, CDate(oi.BirthDay), dtSysDate), String)
                    oi.IdNoL = sPatInfo(6)
                    oi.IdNoR = sPatInfo(7)
                    oi.TEL1 = sPatInfo(4)
                    oi.TEL2 = sPatInfo(5)
                    'oi.DoctorCd = m_dt_OrdInfo.Rows(0).Item("doctorcd").ToString()
                    'oi.DoctorNm = m_dt_OrdInfo.Rows(0).Item("doctornm").ToString()
                    oi.DoctorCd = USER_INFO.USRID '<20140917 종합검증처방의 
                    oi.DoctorNm = USER_INFO.USRNM
                    oi.DeptCd = m_dt_OrdInfo.Rows(0).Item("deptcd").ToString()
                    oi.DeptNm = m_dt_OrdInfo.Rows(0).Item("deptnm").ToString()
                    oi.OrdDt_org = m_dt_OrdInfo.Rows(0).Item("orddt").ToString
                    oi.IoGbn_org = m_dt_OrdInfo.Rows(0).Item("iogbn").ToString
                    oi.FkOcs_org = m_dt_OrdInfo.Rows(0).Item("ocs_key").ToString
                    oi.Chos_No = m_dt_OrdInfo.Rows(0).Item("chos_no").ToString

                    If IsDate(m_dt_OrdInfo.Rows(0).Item("opdt")) Then
                        oi.OpDt = m_dt_OrdInfo.Rows(0).Item("opdt").ToString
                    End If

                    oi.IOGbn = "I"
                    oi.OwnGbn = m_dt_OrdInfo.Rows(0).Item("owngbn").ToString

                    'If oi.OwnGbn = "L" Then
                    al_testcd.Add(PRG_CONST.TEST_GV)
                    'Else
                    '    al_testcd.Add(PRG_CONST.TEST_GV_ORDCD)
                    'End If

                    al_spccd.Add(PRG_CONST.SPC_GV)
                    al_edigbn.Add("N")

                    al_trmk.Add("")
                    al_emer.Add("")
                    al_fkocs.Add("")

                    oi.TestCds = al_testcd
                    oi.SpcCds = al_spccd
                    oi.EdiGbns = al_edigbn
                    oi.TRemarks = al_trmk
                    oi.EmerYNs = al_emer
                    oi.FKOCSs = al_fkocs

                    If IsDate(sEntDay) Then
                        oi.EntDt = sEntDay
                        oi.WardNo = m_dt_OrdInfo.Rows(0).Item("wardno").ToString()
                        oi.RoomNo = m_dt_OrdInfo.Rows(0).Item("roomno").ToString()
                        oi.BedNo = ""
                    Else
                        MsgBox("입원일자 오류 발생!!")

                        Return
                    End If

                    si.OrdInfo = oi

                    al_return = si.fnExe_CollectToTake(sErrMsg)

                    If al_return Is Nothing Then
                        iSuc = 0
                    Else
                        If al_return.Count > 0 Then
                            iSuc = 1
                        Else
                            iSuc = 0
                        End If
                    End If
                End If

                oi = Nothing

                If iSuc = 0 Then
                    '실패
                    MsgBox(sErrMsg)

                    Return
                Else
                    '성공
                    al_sucs.Add(al_rows(i - 1))
                End If
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            si = Nothing

            For i As Integer = al_sucs.Count To 1 Step -1
                spd.DeleteRows(Convert.ToInt32(al_sucs(i - 1)), 1)
                spd.MaxRows -= 1
            Next

            sbDisplay_ToDo_Today()

        End Try
    End Sub

    Private Sub sbGv_hit()
        Dim sFn As String = "sbGv_hit"

        Dim al_rows As ArrayList = fnGv_Tk_Rows()
        Dim al_sucs As New ArrayList
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList

        Try
            Dim sDeptInf As String = LISAPP.APP_G.CommFn.fnGet_Usr_Dept_info(USER_INFO.USRID)

            For i As Integer = 1 To al_rows.Count
                If spd.GetColFromID("regno") * spd.GetColFromID("baseday") = 0 Then
                    MsgBox("Column ID 오류 발생!!")

                    Return
                End If

                Dim stu As New COMMON.SVar.STU_GVINFO

                spd.Row = Convert.ToInt32(al_rows(i - 1))
                spd.Col = spd.GetColFromID("regno") : stu.REGNO = spd.Text

                stu.ORDCD = PRG_CONST.TEST_GV_ORDCD.Split("/"c)(0)
                stu.SUGACD = PRG_CONST.TEST_GV_ORDCD.Split("/"c)(1)

                If sDeptInf.IndexOf("/") >= 0 Then
                    stu.DEPTCD_USR = sDeptInf.Split("/"c)(0)
                    stu.DEPTNM_USR = sDeptInf.Split("/"c)(1)
                Else
                    stu.DEPTCD_USR = ""
                    stu.DEPTNM_USR = ""
                End If

                stu.SPCCD = PRG_CONST.SPC_GV
                stu.STATUS = "I,G"

                Dim sRet As String = (New WEBSERVER.CGWEB_G).ExecuteDo(stu)

                If sRet.StartsWith("00") Then
                    '성공
                    al_sucs.Add(al_rows(i - 1))
                Else
                    '실패
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sRet.Substring(2))
                    Return
                End If
            Next

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally

            For i As Integer = al_sucs.Count To 1 Step -1
                spd.DeleteRows(Convert.ToInt32(al_sucs(i - 1)), 1)
                spd.MaxRows -= 1
            Next

            sbDisplay_ToDo_Today()

        End Try
    End Sub

    Private Sub sbGv_Tk_ocs()
        Dim sFn As String = "sbGv_Tk_ocs"

        Dim al_rows As ArrayList = fnGv_Tk_Rows()
        Dim si As SYSIF01.SYSIF
        Dim spd As AxFPSpreadADO.AxfpSpread = Me.spdList
        Dim al_sucs As New ArrayList

        Try
            si = New SYSIF01.SYSIF(USER_INFO.USRID, USER_INFO.LOCALIP)

            For i As Integer = 1 To al_rows.Count

                Dim sRegNo As String = ""
                Dim sEntDay As String = ""
                Dim sOrdDt As String = ""
                Dim sFkOcs As String = ""

                Dim sErrMsg As String = ""

                If spd.GetColFromID("regno") * spd.GetColFromID("baseday") = 0 Then
                    MsgBox("Column ID 오류 발생!!")

                    Return
                End If

                spd.Col = spd.GetColFromID("regno") : spd.Row = Convert.ToInt32(al_rows(i - 1)) : sRegNo = spd.Text
                spd.Col = spd.GetColFromID("orddt") : spd.Row = Convert.ToInt32(al_rows(i - 1)) : sOrdDt = spd.Text
                spd.Col = spd.GetColFromID("fkocs") : spd.Row = Convert.ToInt32(al_rows(i - 1)) : sFkOcs = spd.Text
                spd.Col = spd.GetColFromID("baseday") : spd.Row = Convert.ToInt32(al_rows(i - 1)) : sEntDay = spd.Text


                sErrMsg = si.fnExe_CollectToTake(sRegNo, sOrdDt, sFkOcs)

                If sErrMsg = "" Then
                    Return
                Else
                    If sErrMsg.Substring(0, 2) <> "00" Then
                        '실패
                        MsgBox(sErrMsg.Substring(2).Trim())

                        Return
                    Else
                        '성공
                        al_sucs.Add(al_rows(i - 1))
                    End If

                End If
            Next

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            si = Nothing

            For i As Integer = al_sucs.Count To 1 Step -1
                spd.DeleteRows(Convert.ToInt32(al_sucs(i - 1)), 1)
                spd.MaxRows -= 1
            Next

            sbDisplay_ToDo_Today()

        End Try
    End Sub


    Private Sub sbSelect_Deselect_List()
        Dim sFn As String = "sbSelect_Deselect_List"

        Try
            With Me.spdList
                If .ActiveRow < 1 Then Return

                miProcessing = 1

                .Col = .GetColFromID("chk")
                .Row = .ActiveRow

                If .Text = "1" Then
                    .Text = ""
                Else
                    .Text = "1"

                    '접수방법 -> Checked
                    Me.rdoTkMtC.Checked = True
                End If

                miProcessing = 0

                sbDisplay_Chk_Current(.ActiveRow)

                .Focus()
            End With

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    '<----- Control Event ----->
    Private Sub FGR10_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Space
                sbSelect_Deselect_List()

        End Select
    End Sub

    Private Sub FGR10_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sFn As String = "FGR10_Load"

        Try
            DS_FormDesige.sbInti(Me)

            sbDisplayInit()
            'sbDisplay_Board()
            sbDisplayInit_Right_List()
            'sbDisplay_ToDo_Today()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            mbLoaded = True

        End Try
    End Sub


    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnGv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGv.Click
        Dim frm As Windows.Forms.Form

        frm = Ctrl.CheckFormObject(Me, Me.btnGv.Tag.ToString())

        If frm Is Nothing Then
            frm = New LISR.FGR09(1, PRG_CONST.TEST_GV)
            CType(frm, LISR.FGR09).msUse_PartCd = PRG_CONST.PART_GeneralVerify
        End If

        frm.MdiParent = Me.MdiParent
        frm.WindowState = Windows.Forms.FormWindowState.Maximized
        frm.Text = Me.btnGv.Tag.ToString()
        frm.Activate()
        frm.Show()
    End Sub

    Private Sub btnSearchB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearchB.Click
        Dim sFn As String = "btnSearchB_Click"

        Try
            sbDisplay_Board()

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally

        End Try
    End Sub

    Private Sub btnSearchL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = "btnSearchL_Click"

        Try
            Me.Cursor = Windows.Forms.Cursors.WaitCursor

            If CType(sender, Windows.Forms.Button).Text.StartsWith("대상자") Then
                sbDisplay_List_ToTk()
            Else
                'sbDisplay_List_ToCs()
            End If

        Catch ex As Exception
            Fn.log(mc_sFile + sFn, Err)
            MsgBox(mc_sFile + sFn + vbCrLf + ex.Message)

        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub btnTk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTk.Click
        'sbGv_HIT()
        sbGv_Tk()       '-- 후 처방인 경우 사용
        'sbGv_Tk_ocs()   '-- OCS에서 처방이 내려지는 경우 
    End Sub

    Private Sub rdoTkMt_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoTkMtC.CheckedChanged, rdoTkMtR.CheckedChanged, rdoTkMtT.CheckedChanged
        If mbLoaded = False Then Return
        If CType(sender, Windows.Forms.RadioButton).Checked = False Then Return

        sbDisplay_Chk_Current(0)
    End Sub

    Private Sub spdList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdList.ButtonClicked
        If miProcessing = 1 Then Return

        If e.col = Me.spdList.GetColFromID("chk") Then
            If e.col = Me.spdList.ActiveCol Then
                Me.spdList.SetActiveCell(e.col + 1, e.row)
            End If

            sbDisplay_Chk_Current(e.row)
        Else
            Return
        End If
    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent

        With Me.spdList
            .Row = e.row
            .Col = .GetColFromID("regno") : Me.txtRegNo.Text = .Text
        End With

    End Sub

    Private Sub spdList_LeaveCell(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_LeaveCellEvent) Handles spdList.LeaveCell
        If e.newRow < 1 Then Return
        If e.row = e.newRow Then Return

        sbDisplay_Status(e.newRow)
    End Sub

    Private Sub FGR_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub spclst01_ChangeSelectedRow(ByVal r_al_bcno As System.Collections.ArrayList, ByVal r_al_TOrdSlip As System.Collections.ArrayList) Handles spclst01.ChangeSelectedRow
        If r_al_bcno.Count < 1 Then Return

        Dim frm As Windows.Forms.Form

        If Me.OwnedForms.Length = 0 Then
            frm = New POPUPWIN.FGPOPUPRST

            frm.Owner = Me

            CType(frm, POPUPWIN.FGPOPUPRST).Left = Ctrl.FindControlLeft(Me.spclst01)
            CType(frm, POPUPWIN.FGPOPUPRST).Top = Ctrl.FindControlTop(Me.grpRight) + Ctrl.menuHeight
            'CType(frm, POPUPWIN.FGPOPUPRST).Height = Me.grpRight.Height

            AddHandler CType(frm, POPUPWIN.FGPOPUPRST).OnKeyDown_Space, AddressOf sbSelect_Deselect_List
        Else
            frm = Me.OwnedForms(0)
        End If

        CType(frm, POPUPWIN.FGPOPUPRST).Display_Result(r_al_bcno, "")
    End Sub

    Private Sub btnHelp_Diag_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHelp_Diag.Click
        Dim sFn As String = "Handles btnBldInfo_Click.Click"
        If Me.txtRegNo.Text = "" Then Return

        Try

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Diag_Info(Me.txtRegNo.Text)

            objHelp.FormText = "검체정보"

            objHelp.MaxRows = 10
            objHelp.Distinct = True

            objHelp.AddField("orddt", "진료일", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("engname", "진단명", 50, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            objHelp.Display_Result(Me, Me.Left + Me.btnHelp_Diag.Left, Me.Top + Me.btnHelp_Diag.Top + Me.btnHelp_Diag.Height, dt, True)

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try
    End Sub

    Private Sub spdBoard_Advance(ByVal sender As System.Object, ByVal e As AxFPSpreadADO._DSpreadEvents_AdvanceEvent) Handles spdBoard.Advance

    End Sub
End Class
