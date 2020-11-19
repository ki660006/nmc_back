'>>> 부적합검체 등록

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn

Public Class FGUNFITSPC
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGUNFITSPC.vb, Class : AxAckResult.FGUNFITSPC" + vbTab

    Private msSPDAction As String = ""
    Private msBcNo As String = ""
    Private malTcslCds As New ArrayList

    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents lblSpcFlag4 As System.Windows.Forms.Label
    Friend WithEvents lblSpcFlag3 As System.Windows.Forms.Label
    Private mbLoad As Boolean = False

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        fnFormInitialize()

    End Sub

    Public Sub New(ByVal rsBcNo As String, ByVal ra_TclsCds As ArrayList)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        fnFormInitialize()
        msBcNo = rsBcNo
        malTcslCds = ra_TclsCds

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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents lblCancelDesc As System.Windows.Forms.Label
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lblSpcFlag2 As System.Windows.Forms.Label
    Friend WithEvents lblSpcFlag1 As System.Windows.Forms.Label
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents lblWard_SR As System.Windows.Forms.Label
    Friend WithEvents lblDeptNm As System.Windows.Forms.Label
    Friend WithEvents lblIdNo As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lblDoctor As System.Windows.Forms.Label
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents lblCollectDt As System.Windows.Forms.Label
    Friend WithEvents lblCollectID As System.Windows.Forms.Label
    Friend WithEvents lblTkDt As System.Windows.Forms.Label
    Friend WithEvents lblTkID As System.Windows.Forms.Label
    Friend WithEvents cboCancel As System.Windows.Forms.ComboBox
    Friend WithEvents txtCmtCd As System.Windows.Forms.TextBox
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblBCNO As System.Windows.Forms.Label
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents txtCmtCont As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGUNFITSPC))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnToggle = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.lblSearch = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdList = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblWard_SR = New System.Windows.Forms.Label
        Me.Label30 = New System.Windows.Forms.Label
        Me.lblDeptNm = New System.Windows.Forms.Label
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblIdNo = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.lblSexAge = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.lblPatNm = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblTkID = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.lblCollectDt = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.lblCollectID = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.lblOrdDt = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblDoctor = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblTkDt = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblSpcFlag4 = New System.Windows.Forms.Label
        Me.lblSpcFlag3 = New System.Windows.Forms.Label
        Me.lblSpcFlag2 = New System.Windows.Forms.Label
        Me.lblSpcFlag1 = New System.Windows.Forms.Label
        Me.Label34 = New System.Windows.Forms.Label
        Me.lblBCNO = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.txtCmtCd = New System.Windows.Forms.TextBox
        Me.cboCancel = New System.Windows.Forms.ComboBox
        Me.txtCmtCont = New System.Windows.Forms.TextBox
        Me.lblCancelDesc = New System.Windows.Forms.Label
        Me.pnlBottom = New System.Windows.Forms.Panel
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.btnReg = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnToggle)
        Me.GroupBox2.Controls.Add(Me.txtSearch)
        Me.GroupBox2.Controls.Add(Me.lblSearch)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(263, 36)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(223, 11)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(36, 21)
        Me.btnToggle.TabIndex = 1
        Me.btnToggle.Text = "<->"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(85, 11)
        Me.txtSearch.MaxLength = 18
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(137, 21)
        Me.txtSearch.TabIndex = 0
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(4, 11)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(80, 21)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel1.Location = New System.Drawing.Point(5, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(617, 548)
        Me.Panel1.TabIndex = 2
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(613, 544)
        Me.spdList.TabIndex = 0
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
        Me.GroupBox3.Controls.Add(Me.GroupBox5)
        Me.GroupBox3.Controls.Add(Me.lblBCNO)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(625, 36)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(424, 273)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'lblWard_SR
        '
        Me.lblWard_SR.BackColor = System.Drawing.Color.White
        Me.lblWard_SR.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWard_SR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard_SR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWard_SR.Location = New System.Drawing.Point(296, 154)
        Me.lblWard_SR.Name = "lblWard_SR"
        Me.lblWard_SR.Size = New System.Drawing.Size(120, 22)
        Me.lblWard_SR.TabIndex = 29
        Me.lblWard_SR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label30.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.White
        Me.Label30.Location = New System.Drawing.Point(210, 154)
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
        Me.lblDeptNm.Location = New System.Drawing.Point(296, 131)
        Me.lblDeptNm.Name = "lblDeptNm"
        Me.lblDeptNm.Size = New System.Drawing.Size(120, 22)
        Me.lblDeptNm.TabIndex = 27
        Me.lblDeptNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label28.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.White
        Me.Label28.Location = New System.Drawing.Point(210, 131)
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
        Me.lblIdNo.Location = New System.Drawing.Point(296, 200)
        Me.lblIdNo.Name = "lblIdNo"
        Me.lblIdNo.Size = New System.Drawing.Size(120, 22)
        Me.lblIdNo.TabIndex = 25
        Me.lblIdNo.Text = "030405-123567"
        Me.lblIdNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.White
        Me.Label26.Location = New System.Drawing.Point(210, 200)
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
        Me.lblSexAge.Location = New System.Drawing.Point(89, 200)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(120, 22)
        Me.lblSexAge.TabIndex = 23
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label24.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.White
        Me.Label24.Location = New System.Drawing.Point(8, 200)
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
        Me.lblPatNm.Location = New System.Drawing.Point(89, 177)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(120, 22)
        Me.lblPatNm.TabIndex = 21
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label22.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.White
        Me.Label22.Location = New System.Drawing.Point(8, 177)
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
        Me.lblTkID.Location = New System.Drawing.Point(296, 246)
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
        Me.Label20.Location = New System.Drawing.Point(210, 223)
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
        Me.lblCollectDt.Location = New System.Drawing.Point(89, 223)
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
        Me.Label16.Location = New System.Drawing.Point(8, 246)
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
        Me.lblCollectID.Location = New System.Drawing.Point(296, 223)
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
        Me.Label18.Location = New System.Drawing.Point(210, 246)
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
        Me.lblOrdDt.Location = New System.Drawing.Point(89, 131)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(120, 22)
        Me.lblOrdDt.TabIndex = 13
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(8, 131)
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
        Me.lblRegNo.Location = New System.Drawing.Point(296, 177)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(120, 22)
        Me.lblRegNo.TabIndex = 11
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.Location = New System.Drawing.Point(210, 177)
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
        Me.lblDoctor.Location = New System.Drawing.Point(89, 154)
        Me.lblDoctor.Name = "lblDoctor"
        Me.lblDoctor.Size = New System.Drawing.Size(120, 22)
        Me.lblDoctor.TabIndex = 8
        Me.lblDoctor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(8, 154)
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
        Me.lblTkDt.Location = New System.Drawing.Point(89, 246)
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
        Me.Label5.Location = New System.Drawing.Point(8, 223)
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
        Me.lblSpcNm.Location = New System.Drawing.Point(89, 108)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(156, 22)
        Me.lblSpcNm.TabIndex = 4
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(8, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 22)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "검체명"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblSpcFlag4)
        Me.GroupBox5.Controls.Add(Me.lblSpcFlag3)
        Me.GroupBox5.Controls.Add(Me.lblSpcFlag2)
        Me.GroupBox5.Controls.Add(Me.lblSpcFlag1)
        Me.GroupBox5.Controls.Add(Me.Label34)
        Me.GroupBox5.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(3, 35)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(417, 41)
        Me.GroupBox5.TabIndex = 32
        Me.GroupBox5.TabStop = False
        '
        'lblSpcFlag4
        '
        Me.lblSpcFlag4.BackColor = System.Drawing.Color.Silver
        Me.lblSpcFlag4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcFlag4.ForeColor = System.Drawing.Color.White
        Me.lblSpcFlag4.Location = New System.Drawing.Point(333, 12)
        Me.lblSpcFlag4.Name = "lblSpcFlag4"
        Me.lblSpcFlag4.Size = New System.Drawing.Size(80, 24)
        Me.lblSpcFlag4.TabIndex = 4
        Me.lblSpcFlag4.Text = "접 수"
        Me.lblSpcFlag4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcFlag3
        '
        Me.lblSpcFlag3.BackColor = System.Drawing.Color.Silver
        Me.lblSpcFlag3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcFlag3.ForeColor = System.Drawing.Color.White
        Me.lblSpcFlag3.Location = New System.Drawing.Point(252, 12)
        Me.lblSpcFlag3.Name = "lblSpcFlag3"
        Me.lblSpcFlag3.Size = New System.Drawing.Size(80, 24)
        Me.lblSpcFlag3.TabIndex = 3
        Me.lblSpcFlag3.Text = "검체전달"
        Me.lblSpcFlag3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcFlag2
        '
        Me.lblSpcFlag2.BackColor = System.Drawing.Color.Silver
        Me.lblSpcFlag2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcFlag2.ForeColor = System.Drawing.Color.White
        Me.lblSpcFlag2.Location = New System.Drawing.Point(171, 12)
        Me.lblSpcFlag2.Name = "lblSpcFlag2"
        Me.lblSpcFlag2.Size = New System.Drawing.Size(80, 24)
        Me.lblSpcFlag2.TabIndex = 2
        Me.lblSpcFlag2.Text = "채 혈"
        Me.lblSpcFlag2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcFlag1
        '
        Me.lblSpcFlag1.BackColor = System.Drawing.Color.Silver
        Me.lblSpcFlag1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcFlag1.ForeColor = System.Drawing.Color.White
        Me.lblSpcFlag1.Location = New System.Drawing.Point(86, 12)
        Me.lblSpcFlag1.Name = "lblSpcFlag1"
        Me.lblSpcFlag1.Size = New System.Drawing.Size(84, 24)
        Me.lblSpcFlag1.TabIndex = 1
        Me.lblSpcFlag1.Text = "바코드출력"
        Me.lblSpcFlag1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label34.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label34.ForeColor = System.Drawing.Color.White
        Me.Label34.Location = New System.Drawing.Point(5, 12)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(80, 24)
        Me.Label34.TabIndex = 0
        Me.Label34.Text = "검체상태"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBCNO
        '
        Me.lblBCNO.BackColor = System.Drawing.Color.White
        Me.lblBCNO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBCNO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBCNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBCNO.Location = New System.Drawing.Point(89, 85)
        Me.lblBCNO.Name = "lblBCNO"
        Me.lblBCNO.Size = New System.Drawing.Size(156, 22)
        Me.lblBCNO.TabIndex = 13
        Me.lblBCNO.Text = "20030902-A0-0001"
        Me.lblBCNO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(8, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 22)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "검체번호"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.Label35)
        Me.GroupBox4.Controls.Add(Me.txtCmtCd)
        Me.GroupBox4.Controls.Add(Me.cboCancel)
        Me.GroupBox4.Controls.Add(Me.txtCmtCont)
        Me.GroupBox4.Controls.Add(Me.lblCancelDesc)
        Me.GroupBox4.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(625, 314)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(424, 276)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        '
        'Label35
        '
        Me.Label35.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label35.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label35.ForeColor = System.Drawing.Color.White
        Me.Label35.Location = New System.Drawing.Point(4, 34)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(84, 20)
        Me.Label35.TabIndex = 12
        Me.Label35.Text = "부적합코드"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCd.Location = New System.Drawing.Point(89, 34)
        Me.txtCmtCd.Multiline = True
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(85, 20)
        Me.txtCmtCd.TabIndex = 0
        '
        'cboCancel
        '
        Me.cboCancel.BackColor = System.Drawing.SystemColors.Window
        Me.cboCancel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCancel.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboCancel.ItemHeight = 12
        Me.cboCancel.Location = New System.Drawing.Point(175, 34)
        Me.cboCancel.Name = "cboCancel"
        Me.cboCancel.Size = New System.Drawing.Size(244, 20)
        Me.cboCancel.TabIndex = 1
        '
        'txtCmtCont
        '
        Me.txtCmtCont.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtCmtCont.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCont.Location = New System.Drawing.Point(4, 55)
        Me.txtCmtCont.Multiline = True
        Me.txtCmtCont.Name = "txtCmtCont"
        Me.txtCmtCont.Size = New System.Drawing.Size(415, 216)
        Me.txtCmtCont.TabIndex = 2
        Me.txtCmtCont.Text = "혈액용혈"
        '
        'lblCancelDesc
        '
        Me.lblCancelDesc.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblCancelDesc.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCancelDesc.ForeColor = System.Drawing.Color.White
        Me.lblCancelDesc.Location = New System.Drawing.Point(4, 11)
        Me.lblCancelDesc.Name = "lblCancelDesc"
        Me.lblCancelDesc.Size = New System.Drawing.Size(415, 22)
        Me.lblCancelDesc.TabIndex = 8
        Me.lblCancelDesc.Text = "부적합 사유"
        Me.lblCancelDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlBottom
        '
        Me.pnlBottom.Controls.Add(Me.lblUserNm)
        Me.pnlBottom.Controls.Add(Me.lblUserId)
        Me.pnlBottom.Controls.Add(Me.btnReg)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.btnExit)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlBottom.Location = New System.Drawing.Point(0, 595)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1052, 34)
        Me.pnlBottom.TabIndex = 5
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(564, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 159
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(492, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 158
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
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
        Me.btnReg.Location = New System.Drawing.Point(712, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(135, 25)
        Me.btnReg.TabIndex = 186
        Me.btnReg.Text = "부적합검체등록(F5)"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems2
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
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker4
        Me.btnClear.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(848, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 184
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.FocalPoints.CenterPtY = 0.44!
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
        Me.btnExit.Location = New System.Drawing.Point(956, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(91, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'FGUNFITSPC
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1052, 629)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.pnlBottom)
        Me.KeyPreview = True
        Me.Name = "FGUNFITSPC"
        Me.Text = "부적합 검체 등록"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "

    'Private Sub FGJ02_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    '    If mbLoad = False Then
    '        If txtSearch.Text <> "" Then txtSearch_KeyDown(txtSearch, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
    '    End If
    '    mbLoad = True

    'End Sub

#End Region

#Region " 메인 버튼 처리 "
    
    Public Sub Set_StartInfo(ByVal rsRegNo As String)

        Me.txtSearch.Text = rsRegNo

        Me.txtSearch_KeyDown(Nothing, New System.Windows.Forms.KeyEventArgs(Keys.Enter))

    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Handles btnReg.Click"
        Try

            If fnValidation() = False Then Exit Sub

            sbCancel()
            Me.Close()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"
        Try
            sbFormClear(0)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " Form내부 함수 "
    ' Form초기화
    Private Sub fnFormInitialize()
        Dim sFn As String = "Private Sub fnFormInitialize()"
        Dim objCommFn As New Fn

        Try
            sbFormClear(0)

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            sbSpreadColHidden(True)

            Call sbSetcboCancel()  ''' 취소사유 콤보에 select 값 없으면 에러남 

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal riPhase As Integer)
        Dim sFn As String = "Private Sub fnFormClear(ByVal aiPhase As Integer)"

        Try
            If InStr("01", riPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdList.MaxRows = 0

                '''lblSpcFlag1.Visible = False
                '''lblSpcFlag2.Visible = False
                Me.lblBCNO.Text = ""
                Me.lblOrdDt.Text = ""
                Me.lblRegNo.Text = ""
                Me.lblPatNm.Text = ""
                Me.lblSexAge.Text = ""
                Me.lblIdNo.Text = ""
                Me.lblDoctor.Text = ""
                Me.lblDeptNm.Text = ""
                Me.lblWard_SR.Text = ""
                Me.lblSpcNm.Text = ""

                Me.lblCollectDt.Text = "" : Me.lblCollectID.Text = ""
                Me.lblTkDt.Text = "" : Me.lblTkID.Text = ""

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal abFlag As Boolean)
        Dim sFn As String = "Private Sub fnSpreadColHidden(ByVal abFlag As Boolean)"

        Try
            With spdList
                .Col = .GetColFromID("bcno") : .ColHidden = abFlag
                .Col = .GetColFromID("rstflg") : .ColHidden = abFlag
                .Col = .GetColFromID("spccd") : .ColHidden = abFlag
                .Col = .GetColFromID("tcdgbn") : .ColHidden = abFlag
                .Col = .GetColFromID("iogbn") : .ColHidden = abFlag
                .Col = .GetColFromID("fkocs") : .ColHidden = abFlag
                .Col = .GetColFromID("owngbn") : .ColHidden = abFlag
                .Col = .GetColFromID("bcclscd") : .ColHidden = abFlag
                '< yjlee 
                .Col = .GetColFromID("tordcd") : .ColHidden = abFlag
                '> 
                .Col = .GetColFromID("spcflg") : .ColHidden = abFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))
        End Try

    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        fnValidation = False
        Try
            ' Reject
            If Not USER_SKILL.Authority("J01", 3, "부적합 검체 등록") Then
                MsgBox("[부적합 검체 등록] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                Return False
            End If


            If Me.spdList.MaxRows < 1 Then
                MsgBox(btnReg.Text + "할 검체번호를 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtSearch.Focus()
                Return False
            End If

            If Me.txtCmtCont.Text = "" Then
                MsgBox(btnReg.Text + " 사유를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtCmtCont.Focus()
                Exit Function
            End If

            Return True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return False
        End Try

    End Function

    ' 검사항목 표시 
    Private Sub sbDisplay_DataView(ByVal rsBcNo As String)
        Dim sFn As String = "Private Sub sbDisplay_DataView(String)"

        Try
            sbFormClear(1)
            If rsBcNo = "" Then Return

            Dim dt As DataTable = FGJ02_GetOrderList(rsBcNo, True)
            If dt.Rows.Count = 0 Then Return

            Dim sSpcFlg As String = ""
            Dim sRstFlg As String = ""

            With spdList
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1

                    sSpcFlg = dt.Rows(ix).Item("spcflg").ToString
                    If sRstFlg < dt.Rows(ix).Item("rstflg").ToString Then sRstFlg = dt.Rows(ix).Item("rstflg").ToString

                    .Row = ix + 1

                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString
                    .Col = .GetColFromID("tclscd") : .Text = dt.Rows(ix).Item("tclscd").ToString
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString
                    .Col = .GetColFromID("doctorrmk") : .Text = dt.Rows(ix).Item("doctorrmk").ToString

                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString
                    .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString
                    .Col = .GetColFromID("rstflg") : .Text = dt.Rows(ix).Item("rstflg").ToString
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString
                    .Col = .GetColFromID("bcclscd") : .Text = dt.Rows(ix).Item("bcclscd").ToString
                    .Col = .GetColFromID("tordcd") : .Text = dt.Rows(ix).Item("tordcd").ToString
                    .Col = .GetColFromID("spcflg") : .Text = dt.Rows(ix).Item("spcflg_j1").ToString

                    Select Case dt.Rows(ix).Item("rstflg").ToString
                        Case "1"
                            .Col = .GetColFromID("rststate") : .Text = "검사중"
                        Case "2"
                            .Col = .GetColFromID("rststate") : .Text = "중간보고"
                        Case "3"
                            .Col = .GetColFromID("rststate") : .Text = "최종보고"
                    End Select


                    If malTcslCds.Contains(dt.Rows(ix).Item("tclscd").ToString.Trim) Then
                        .Col = .GetColFromID("chk") : .Text = "1"
                    End If


                    If ix = 0 Then

                        Me.lblBCNO.Text = Fn.BCNO_View(dt.Rows(ix).Item("bcno").ToString, True)
                        Me.lblOrdDt.Text = dt.Rows(ix).Item("orddt").ToString
                        Me.lblRegNo.Text = dt.Rows(ix).Item("regno").ToString
                        Me.lblPatNm.Text = dt.Rows(ix).Item("patnm").ToString
                        Me.lblSexAge.Text = dt.Rows(ix).Item("sexage").ToString

                        Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c) ''' 정은수정 
                        Me.lblIdNo.Text = sPatInfo(3) ''' 정은 수정 
                        Me.lblDoctor.Text = dt.Rows(ix).Item("doctornm").ToString
                        Me.lblDeptNm.Text = dt.Rows(ix).Item("deptnm").ToString
                        Me.lblWard_SR.Text = dt.Rows(ix).Item("wardroom").ToString
                        Me.lblSpcNm.Text = dt.Rows(ix).Item("spcnmd").ToString

                        Me.lblCollectDt.Text = dt.Rows(ix).Item("colldt").ToString
                        Me.lblCollectID.Text = dt.Rows(ix).Item("collnm").ToString
                        Me.lblTkDt.Text = dt.Rows(ix).Item("tkdt").ToString
                        Me.lblTkID.Text = dt.Rows(ix).Item("tknm").ToString

                    End If

                Next
            End With

            With spdList
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1
                    .Row = ix + 1
                    .Col = .GetColFromID("chk") : .Text = "1" '<20141126 부적합검체 시작시 체크
                Next
            End With

            If sSpcFlg <= "3" Then  ''' 바코드출력 바뀐 상태값 적용   정은 2010-10-11
                Me.lblSpcFlag1.BackColor = System.Drawing.Color.FromArgb(165, 0, 123) ''' 바코드출력
                Me.lblSpcFlag2.BackColor = System.Drawing.Color.LightGray             ''' 채혈    
                Me.lblSpcFlag3.BackColor = System.Drawing.Color.LightGray             ''' 검체전달
                Me.lblSpcFlag4.BackColor = System.Drawing.Color.LightGray             ''' 접수

            ElseIf sSpcFlg = "4" Then  ''' 접수상태 바뀐 상태값 적용   정은 2010-10-11
                Me.lblSpcFlag1.BackColor = System.Drawing.Color.LightGray             ''' 바코드출력
                Me.lblSpcFlag2.BackColor = System.Drawing.Color.LightGray             ''' 채혈     
                Me.lblSpcFlag3.BackColor = System.Drawing.Color.LightGray             ''' 검체전달
                Me.lblSpcFlag4.BackColor = System.Drawing.Color.FromArgb(165, 0, 123)

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 취소할 항목 ArrayList에 Add
    Private Function fnGet_CancleInfo() As ArrayList
        Dim sFn As String = "Private Sub fnGet_CancleInfo() As ArrayList"

        Dim stu As STU_CANCELINFO

        Dim sRstStat As String = ""
        Dim sOrdNm As String = ""
        Dim al_CancelInfo As New ArrayList


        Try
            With spdList
                For intCnt = 1 To .MaxRows
                    .Row = intCnt
                    .Col = .GetColFromID("rstflg") : sRstStat = .Text
                    .Col = .GetColFromID("tnmd") : sOrdNm = .Text

                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then

                        stu = New STU_CANCELINFO

                        stu.REGNO = Me.lblRegNo.Text

                        .Col = .GetColFromID("bcno") : stu.BCNO = .Text
                        .Col = .GetColFromID("tclscd") : stu.TCLSCD = .Text
                        .Col = .GetColFromID("spccd") : stu.SPCCD = .Text
                        .Col = .GetColFromID("tcdgbn") : stu.TCDGBN = .Text
                        .Col = .GetColFromID("iogbn") : stu.IOGBN = .Text
                        .Col = .GetColFromID("fkocs") : stu.FKOCS = .Text
                        .Col = .GetColFromID("owngbn") : stu.OWNGBN = .Text
                        .Col = .GetColFromID("bcclscd") : stu.BCCLSCD = .Text
                        .Col = .GetColFromID("doctorrmk") : stu.CANCELCMT = .Text

                        '< yjlee
                        .Col = .GetColFromID("tordcd") : stu.TORDCD = .Text
                        '> 
                        .Col = .GetColFromID("spcflg") : stu.SPCFLG = .Text

                        al_CancelInfo.Add(stu)
                    End If
                Next
            End With
            al_CancelInfo.TrimToSize()

            Return al_CancelInfo

        Catch ex As Exception
            Throw (New Exception(ex.Message, ex))
        End Try

    End Function

    ' 취소 
    Private Sub sbCancel()
        Dim sFn As String = "Private Sub fnJubSu_Cancel()"
        Dim alOrdList As New ArrayList
        Try

            'If fnValidation() = False Then Exit Sub

            alOrdList = fnGet_CancleInfo()
            If alOrdList.Count > 0 Then
                If MsgBox("선택항목을 정말로 " + btnReg.Text + " 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If

                With (New Cancel)
                    .CancelTItem = alOrdList
                    .CancelCmt = Me.txtCmtCont.Text
                    .CancelCd = Me.txtCmtCd.Text

                    ' 관리자 Wittyman만 가능함 MTS적용 유무
                    .NotApplyMTS = False

                    Dim sRet As String = .ExecuteDo(enumCANCEL.부적합검등록, Me.lblUserId.Text)


                    If sRet <> "" Then
                        Throw (New Exception(sRet))
                    Else
                        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 " + Me.btnReg.Text + " 되었습니다.")
                        sbFormClear(0)
                    End If
                End With

            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, Me.btnReg.Text + "할 검사코드를 선택해 주십시오.")
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    Private Sub sbCancel_web()
        Dim sFn As String = "Private Sub sbCancel_web()"
        Dim al_CancelInto As New ArrayList
        Try

            'If fnValidation() = False Then Exit Sub

            al_CancelInto = fnGet_CancleInfo()
            If al_CancelInto.Count > 0 Then
                If MsgBox("선택항목을 정말로 " + btnReg.Text + " 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If

                Dim stu As New STU_CANCELWEB

                stu.JOBGBN = "6"
                stu.CMTCD = Me.txtCmtCd.Text
                stu.CMTCONT = Me.txtCmtCont.Text

                For ix As Integer = 0 To al_CancelInto.Count - 1
                    stu.REGNO = CType(al_CancelInto(ix), STU_CANCELINFO).REGNO
                    stu.OWNGBN = CType(al_CancelInto(ix), STU_CANCELINFO).OWNGBN
                    stu.SPCCD = CType(al_CancelInto(ix), STU_CANCELINFO).SPCCD

                    If ix > 0 Then
                        stu.BCNOS += ","
                        stu.TESTCDS += ","
                        stu.FKOCSS += ","
                    End If

                    stu.BCNOS += CType(al_CancelInto(ix), STU_CANCELINFO).BCNO
                    stu.TESTCDS += CType(al_CancelInto(ix), STU_CANCELINFO).TCLSCD
                    stu.FKOCSS += CType(al_CancelInto(ix), STU_CANCELINFO).FKOCS
                Next

                Dim sRet As String = (New WEBSERVER.CGWEB_J).ExecuteDo_Cancel(stu, "lis")

                If sRet.StartsWith("00") Then
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "정상적으로 " + Me.btnReg.Text + " 되었습니다.")
                    sbFormClear(0)
                Else
                    Throw (New Exception(sRet))
                End If

            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, Me.btnReg.Text + "할 검사코드를 선택해 주십시오.")
            End If

        Catch ex As Exception
            Throw (New Exception(ex.Message))
        End Try

    End Sub

    Private Sub sbSetcboCancel()

        Dim dt As DataTable = LISAPP.COMM.cdfn.fnGet_cmtcont_etc("E", True)

        Me.cboCancel.Items.Clear()
        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboCancel.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
            Next
        End If

        If Me.cboCancel.Items.Count > 0 Then Me.cboCancel.SelectedIndex = 0

    End Sub
#End Region

#Region " Control Event 처리 "
    Private Sub btnToggle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnToggle.Click
        Dim CommFn As New COMMON.CommFN.Fn
        CommFn.SearchToggle(lblSearch, btnToggle, enumToggle.BcnoToRegno, txtSearch)

        txtSearch.Focus()

        If USER_INFO.USRLVL = "P" Then
            ' 진료지원과는 등록번호선택시 등록번호 표시 
            If CType(btnToggle.Tag, String) = "" Then
                txtSearch.Text = ""
            Else
                txtSearch.Text = USER_INFO.OTHER
            End If
        End If
    End Sub

    Private Sub rdoGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sfn As String = "Private Sub rdoGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGbn0.Click, rdoGbn1.Click, rdoGbn2.Click, rdoGbn3.Click"
        Dim objRButton As Windows.Forms.RadioButton = CType(sender, Windows.Forms.RadioButton)
        Dim strTag As String = CType(objRButton.Tag, String)

        Try
            If strTag = "0" Then
                btnReg.Text = "채혈/접수 취소"
                lblCancelDesc.Text = "취소사유"
            ElseIf strTag = "1" Then
                btnReg.Text = "채혈취소"
                lblCancelDesc.Text = "취소사유"
            ElseIf strTag = "2" Then
                btnReg.Text = "접수취소"
                lblCancelDesc.Text = "취소사유"
            ElseIf strTag = "3" Then
                btnReg.Text = "REJECT"
                lblCancelDesc.Text = "REJECT 사유"
            End If

            With spdList
                If strTag = "2" Then
                    .Col = .GetColFromID("chk")
                    .ColHidden = True

                Else
                    .Col = .GetColFromID("chk")
                    .ColHidden = False
                End If

                .Row = -1
                .Text = "1"

            End With

        Catch ex As Exception
            Fn.log(msFile & sfn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        txtSearch.Focus()
        txtSearch.SelectAll()
    End Sub


    Private Sub cboCancel_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboCancel.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : SendKeys.SendWait("{TAB}")
        End If
    End Sub

    Private Sub spdList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdList.ButtonClicked

        Dim strChk As String
        Dim strFKocs As String
        Dim strTmp As String

        If msSPDAction <> "" Then Exit Sub

        With spdList
            .Row = e.row
            .Col = 1 : strChk = .Text
            .Col = .GetColFromID("fkocs") : strFKocs = .Text
            msSPDAction = "1"
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("fkocs") : strTmp = .Text
                If i <> e.row And strTmp = strFKocs Then
                    .Row = i
                    .Col = 1 : .Text = strChk
                End If
            Next
            msSPDAction = ""
        End With
    End Sub
#End Region

    Private Sub cboCancel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCancel.SelectedValueChanged

        If Me.cboCancel.Text <> "" Then
            Me.txtCmtCont.Text = Ctrl.Get_Name(cboCancel)
            Me.txtCmtCd.Text = Ctrl.Get_Code(cboCancel)
        End If

        If Me.txtCmtCont.Text = "" Then
            Me.txtCmtCont.Focus()
        Else
            Me.btnReg.Focus()
        End If

    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        Me.txtSearch.Focus()
        Me.txtSearch.SelectAll()
    End Sub

    Private Sub FGUNFITSPC_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        ' Function Key정의

        'F4 : 화면정리 
        'F5 : 채혈/접수 취소 
        'F10: 화면종료

        If e.KeyCode = Keys.F5 Then
            btnReg_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then
            btnClear_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()

        End If

    End Sub

    Private Sub txtCancelCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown
        Dim sFn As String = "Handles txtCmtCd.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtCmtCd.Text = "" Then Return

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList

            Dim strCds As String = txtCmtCd.Text
            If strCds.IndexOf("?") < 0 Then strCds += "%"

            objHelp.FormText = "취소 사유 내용"
            objHelp.TableNm = "LF410M"

            objHelp.Where = "CMTGBN = '1' and and (CMTCD like '" + strCds.Replace("?", "%") + "' or CMTCONT like '" + strCds.Replace("?", "%") + "')"

            objHelp.GroupBy = ""
            objHelp.OrderBy = "CMTCONT"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("CMTCONT", "내용", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("CMTGBN || CMTCD CMTCD", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "CMTCD")

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtCmtCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtCmtCd.Height + 80)

            If alList.Count > 0 Then
                Me.txtCmtCont.Text += alList.Item(0).ToString.Split("|"c)(0) + vbCrLf
                Me.txtCmtCd.Text = alList.Item(0).ToString.Split("|"c)(1)

                For ix As Integer = 0 To cboCancel.Items.Count - 1
                    Me.cboCancel.SelectedIndex = ix
                    If cboCancel.Text = "[" + Me.txtCmtCd.Text + "] " + Me.txtCmtCont.Text Then
                        cboCancel.SelectedIndex = ix
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)
        End Try

    End Sub

    Private Sub FGUNFITSPC_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.txtSearch.Text = msBcNo
        If Me.txtSearch.Text <> "" Then sbDisplay_DataView(msBcNo)

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim sRegNo As String = ""
            Dim sBcNo As String = ""

            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

            If Not Me.txtSearch.Text.Equals("") Then
                If Me.lblSearch.Text = "검체번호" Then
                    '검체번호 선택시 처리내용
                    If Me.txtSearch.Text.Length.Equals(11) Then
                        ' 바코드에서 직접 입력시

                        ' 바코드번호(검체번호)를 표시형 검체번호로 변경
                        Dim objCommDBFN As New LISAPP.APP_DB.DbFn
                        Me.txtSearch.Text = objCommDBFN.GetBCPrtToView(Me.txtSearch.Text) '.Substring(0, 15)

                    ElseIf Me.txtSearch.Text.Length < 14 Then
                        MsgBox("잘못된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                        Me.txtSearch.Focus()
                        Return
                    End If
                    sBcNo = Me.txtSearch.Text
                Else
                    If IsNumeric(Me.txtSearch.Text.Substring(0, 1)) Then
                        Me.txtSearch.Text = Me.txtSearch.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                    Else
                        Me.txtSearch.Text = Me.txtSearch.Text.Substring(0, 1).ToUpper + Me.txtSearch.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                    End If
                    sRegNo = Me.txtSearch.Text

                End If

                Dim objHelp As New CDHELP.FGCDHELP01
                Dim alList As New ArrayList

                Dim dt As DataTable = fnGet_PatInfo_List(sRegNo, sBcNo, IIf(USER_INFO.USRLVL = "O", USER_INFO.N_WARDorDEPT, "").ToString, IIf(USER_INFO.USRLVL = "W", USER_INFO.N_WARDorDEPT, "").ToString)

                objHelp.FormText = "환자정보"
                objHelp.MaxRows = 15
                objHelp.OnRowReturnYN = True

                objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("regno", "등록번호", 9, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("patnm", "성명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("sexage", "성별/나이", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("orddt", "처방일시", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("doctornm", "의뢰의사", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("deptward", "진료과 및 병동", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
                objHelp.AddField("tnmds", "검사명", 30, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
                objHelp.AddField("spcflg", "상태", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

                Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
                Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtSearch)

                alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtSearch.Height + 80, dt)

                If alList.Count > 0 Then
                    sbDisplay_DataView(alList.Item(0).ToString.Split("|"c)(0).Replace("-", ""))

                    Me.txtSearch.Text = ""
                Else
                    MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub pnlBottom_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles pnlBottom.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub

    
    
End Class
