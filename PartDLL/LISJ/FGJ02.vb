'>>> 채혈/접수 취소
Imports System.Drawing
Imports System.Windows.Forms

Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN
Imports COMMON.SVar

Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn

Public Class FGJ02
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGJ02.vb, Class : J01" & vbTab

    Private mstrSPDAction As String = ""
    Private msRegNo As String = ""
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents lblSpcFlag4 As System.Windows.Forms.Label
    Friend WithEvents lblSpcFlag3 As System.Windows.Forms.Label
    Friend WithEvents lblPassId As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblPassDt As System.Windows.Forms.Label
    Private mbLoad As Boolean = False

    Private msnCov As Boolean = False 'jjh 특수보고서 결과ㄹ

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()

    End Sub

    Public Sub New(ByVal rsRegNo As String)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbFormInitialize()
        msRegNo = rsRegNo

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
    Friend WithEvents Label33 As System.Windows.Forms.Label
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
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents rdoGbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoGbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label98 As System.Windows.Forms.Label
    Friend WithEvents rdoGbn3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoGbn2 As System.Windows.Forms.RadioButton
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pnlNotApplyMTS As System.Windows.Forms.Panel
    Friend WithEvents chkNotApplyMTS As System.Windows.Forms.CheckBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGJ02))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems()
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.rdoGbn3 = New System.Windows.Forms.RadioButton()
        Me.rdoGbn2 = New System.Windows.Forms.RadioButton()
        Me.rdoGbn0 = New System.Windows.Forms.RadioButton()
        Me.rdoGbn1 = New System.Windows.Forms.RadioButton()
        Me.Label98 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblPassId = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblPassDt = New System.Windows.Forms.Label()
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblSpcFlag4 = New System.Windows.Forms.Label()
        Me.lblSpcFlag3 = New System.Windows.Forms.Label()
        Me.lblSpcFlag2 = New System.Windows.Forms.Label()
        Me.lblSpcFlag1 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.lblBCNO = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtCmtCd = New System.Windows.Forms.TextBox()
        Me.cboCancel = New System.Windows.Forms.ComboBox()
        Me.txtCmtCont = New System.Windows.Forms.TextBox()
        Me.lblCancelDesc = New System.Windows.Forms.Label()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.btnReg = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.pnlNotApplyMTS = New System.Windows.Forms.Panel()
        Me.chkNotApplyMTS = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.pnlBottom.SuspendLayout()
        Me.pnlNotApplyMTS.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel15)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(268, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(564, 36)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.LavenderBlush
        Me.Panel15.Controls.Add(Me.rdoGbn3)
        Me.Panel15.Controls.Add(Me.rdoGbn2)
        Me.Panel15.Controls.Add(Me.rdoGbn0)
        Me.Panel15.Controls.Add(Me.rdoGbn1)
        Me.Panel15.Controls.Add(Me.Label98)
        Me.Panel15.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Panel15.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.Panel15.Location = New System.Drawing.Point(89, 11)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(460, 22)
        Me.Panel15.TabIndex = 99
        '
        'rdoGbn3
        '
        Me.rdoGbn3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbn3.ForeColor = System.Drawing.Color.Black
        Me.rdoGbn3.Location = New System.Drawing.Point(379, 0)
        Me.rdoGbn3.Name = "rdoGbn3"
        Me.rdoGbn3.Size = New System.Drawing.Size(88, 20)
        Me.rdoGbn3.TabIndex = 3
        Me.rdoGbn3.Tag = "3"
        Me.rdoGbn3.Text = "REJECT(&4)"
        Me.rdoGbn3.UseVisualStyleBackColor = False
        '
        'rdoGbn2
        '
        Me.rdoGbn2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbn2.ForeColor = System.Drawing.Color.Black
        Me.rdoGbn2.Location = New System.Drawing.Point(267, 0)
        Me.rdoGbn2.Name = "rdoGbn2"
        Me.rdoGbn2.Size = New System.Drawing.Size(104, 20)
        Me.rdoGbn2.TabIndex = 2
        Me.rdoGbn2.Tag = "2"
        Me.rdoGbn2.Text = "접수취소(&3)"
        Me.rdoGbn2.UseVisualStyleBackColor = False
        '
        'rdoGbn0
        '
        Me.rdoGbn0.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn0.Checked = True
        Me.rdoGbn0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn0.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbn0.ForeColor = System.Drawing.Color.Black
        Me.rdoGbn0.Location = New System.Drawing.Point(13, 0)
        Me.rdoGbn0.Name = "rdoGbn0"
        Me.rdoGbn0.Size = New System.Drawing.Size(132, 20)
        Me.rdoGbn0.TabIndex = 0
        Me.rdoGbn0.TabStop = True
        Me.rdoGbn0.Tag = "0"
        Me.rdoGbn0.Text = "채혈/접수 취소(&1)"
        Me.rdoGbn0.UseVisualStyleBackColor = False
        '
        'rdoGbn1
        '
        Me.rdoGbn1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rdoGbn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoGbn1.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.rdoGbn1.ForeColor = System.Drawing.Color.Black
        Me.rdoGbn1.Location = New System.Drawing.Point(157, 0)
        Me.rdoGbn1.Name = "rdoGbn1"
        Me.rdoGbn1.Size = New System.Drawing.Size(104, 20)
        Me.rdoGbn1.TabIndex = 1
        Me.rdoGbn1.Tag = "1"
        Me.rdoGbn1.Text = "채혈취소(&2)"
        Me.rdoGbn1.UseVisualStyleBackColor = False
        '
        'Label98
        '
        Me.Label98.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label98.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label98.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label98.Location = New System.Drawing.Point(0, 0)
        Me.Label98.Name = "Label98"
        Me.Label98.Size = New System.Drawing.Size(460, 22)
        Me.Label98.TabIndex = 2
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label33.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(4, 11)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(80, 21)
        Me.Label33.TabIndex = 3
        Me.Label33.Text = "취소방법"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.GroupBox3.Controls.Add(Me.lblPassId)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblPassDt)
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
        Me.GroupBox3.Size = New System.Drawing.Size(424, 292)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'lblPassId
        '
        Me.lblPassId.BackColor = System.Drawing.Color.White
        Me.lblPassId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPassId.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPassId.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPassId.Location = New System.Drawing.Point(296, 242)
        Me.lblPassId.Name = "lblPassId"
        Me.lblPassId.Size = New System.Drawing.Size(120, 22)
        Me.lblPassId.TabIndex = 36
        Me.lblPassId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(8, 242)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 22)
        Me.Label6.TabIndex = 35
        Me.Label6.Text = "전달일시"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(210, 242)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 22)
        Me.Label8.TabIndex = 34
        Me.Label8.Text = "전달자"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPassDt
        '
        Me.lblPassDt.BackColor = System.Drawing.Color.White
        Me.lblPassDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPassDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPassDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPassDt.Location = New System.Drawing.Point(89, 242)
        Me.lblPassDt.Name = "lblPassDt"
        Me.lblPassDt.Size = New System.Drawing.Size(120, 22)
        Me.lblPassDt.TabIndex = 33
        Me.lblPassDt.Text = "2003-01-01 15:00"
        Me.lblPassDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblWard_SR
        '
        Me.lblWard_SR.BackColor = System.Drawing.Color.White
        Me.lblWard_SR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblWard_SR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWard_SR.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblWard_SR.Location = New System.Drawing.Point(296, 150)
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
        Me.Label30.Location = New System.Drawing.Point(210, 150)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(85, 22)
        Me.Label30.TabIndex = 28
        Me.Label30.Text = "병동/병실"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDeptNm
        '
        Me.lblDeptNm.BackColor = System.Drawing.Color.White
        Me.lblDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDeptNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDeptNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDeptNm.Location = New System.Drawing.Point(296, 127)
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
        Me.Label28.Location = New System.Drawing.Point(210, 127)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(85, 22)
        Me.Label28.TabIndex = 26
        Me.Label28.Text = "진료과"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdNo
        '
        Me.lblIdNo.BackColor = System.Drawing.Color.White
        Me.lblIdNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblIdNo.Location = New System.Drawing.Point(296, 196)
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
        Me.Label26.Location = New System.Drawing.Point(210, 196)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(85, 22)
        Me.Label26.TabIndex = 24
        Me.Label26.Text = "주민등록번호"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.White
        Me.lblSexAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSexAge.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSexAge.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSexAge.Location = New System.Drawing.Point(89, 196)
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
        Me.Label24.Location = New System.Drawing.Point(8, 196)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(80, 22)
        Me.Label24.TabIndex = 22
        Me.Label24.Text = "Sex/Age"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.White
        Me.lblPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPatNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPatNm.Location = New System.Drawing.Point(89, 173)
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
        Me.Label22.Location = New System.Drawing.Point(8, 173)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 22)
        Me.Label22.TabIndex = 20
        Me.Label22.Text = "성명"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkID
        '
        Me.lblTkID.BackColor = System.Drawing.Color.White
        Me.lblTkID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTkID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkID.Location = New System.Drawing.Point(296, 265)
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
        Me.Label20.Location = New System.Drawing.Point(210, 219)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(85, 22)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "채혈자"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollectDt
        '
        Me.lblCollectDt.BackColor = System.Drawing.Color.White
        Me.lblCollectDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCollectDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollectDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollectDt.Location = New System.Drawing.Point(89, 219)
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
        Me.Label16.Location = New System.Drawing.Point(8, 265)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 22)
        Me.Label16.TabIndex = 16
        Me.Label16.Text = "접수일시"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCollectID
        '
        Me.lblCollectID.BackColor = System.Drawing.Color.White
        Me.lblCollectID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCollectID.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCollectID.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCollectID.Location = New System.Drawing.Point(296, 219)
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
        Me.Label18.Location = New System.Drawing.Point(210, 265)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(85, 22)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "접수자"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.White
        Me.lblOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOrdDt.Location = New System.Drawing.Point(89, 127)
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
        Me.Label14.Location = New System.Drawing.Point(8, 127)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 22)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "처방일시"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.White
        Me.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblRegNo.Location = New System.Drawing.Point(296, 173)
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
        Me.Label12.Location = New System.Drawing.Point(210, 173)
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
        Me.lblDoctor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDoctor.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblDoctor.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblDoctor.Location = New System.Drawing.Point(89, 150)
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
        Me.Label7.Location = New System.Drawing.Point(8, 150)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 22)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "의뢰의사"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblTkDt
        '
        Me.lblTkDt.BackColor = System.Drawing.Color.White
        Me.lblTkDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTkDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTkDt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTkDt.Location = New System.Drawing.Point(89, 265)
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
        Me.Label5.Location = New System.Drawing.Point(8, 219)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 22)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "채취일시"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSpcNm
        '
        Me.lblSpcNm.BackColor = System.Drawing.Color.White
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSpcNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcNm.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSpcNm.Location = New System.Drawing.Point(89, 104)
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
        Me.Label1.Location = New System.Drawing.Point(8, 104)
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
        Me.lblBCNO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBCNO.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblBCNO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBCNO.Location = New System.Drawing.Point(89, 81)
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
        Me.Label3.Location = New System.Drawing.Point(8, 81)
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
        Me.GroupBox4.Location = New System.Drawing.Point(625, 324)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(424, 264)
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
        Me.Label35.Size = New System.Drawing.Size(69, 20)
        Me.Label35.TabIndex = 12
        Me.Label35.Text = "취소코드"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCmtCd
        '
        Me.txtCmtCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCmtCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtCmtCd.Location = New System.Drawing.Point(74, 34)
        Me.txtCmtCd.Multiline = True
        Me.txtCmtCd.Name = "txtCmtCd"
        Me.txtCmtCd.Size = New System.Drawing.Size(100, 20)
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
        Me.txtCmtCont.Size = New System.Drawing.Size(415, 204)
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
        Me.lblCancelDesc.Text = "취소사유"
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
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(135, 25)
        Me.btnReg.TabIndex = 186
        Me.btnReg.Text = "채혈/접수취소(F5)"
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
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 184
        Me.btnClear.Text = "화면정리(F4)"
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
        DesignerRectTracker6.IsActive = True
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
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(91, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'pnlNotApplyMTS
        '
        Me.pnlNotApplyMTS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlNotApplyMTS.BackColor = System.Drawing.Color.Salmon
        Me.pnlNotApplyMTS.Controls.Add(Me.chkNotApplyMTS)
        Me.pnlNotApplyMTS.Controls.Add(Me.Label2)
        Me.pnlNotApplyMTS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.pnlNotApplyMTS.ForeColor = System.Drawing.Color.White
        Me.pnlNotApplyMTS.Location = New System.Drawing.Point(880, 11)
        Me.pnlNotApplyMTS.Name = "pnlNotApplyMTS"
        Me.pnlNotApplyMTS.Size = New System.Drawing.Size(160, 21)
        Me.pnlNotApplyMTS.TabIndex = 161
        '
        'chkNotApplyMTS
        '
        Me.chkNotApplyMTS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkNotApplyMTS.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.chkNotApplyMTS.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkNotApplyMTS.ForeColor = System.Drawing.Color.Black
        Me.chkNotApplyMTS.Location = New System.Drawing.Point(10, 1)
        Me.chkNotApplyMTS.Name = "chkNotApplyMTS"
        Me.chkNotApplyMTS.Size = New System.Drawing.Size(138, 20)
        Me.chkNotApplyMTS.TabIndex = 1
        Me.chkNotApplyMTS.TabStop = False
        Me.chkNotApplyMTS.Text = "취소시 MTS 적용안함"
        Me.chkNotApplyMTS.UseVisualStyleBackColor = False
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 21)
        Me.Label2.TabIndex = 0
        '
        'FGJ02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1052, 629)
        Me.Controls.Add(Me.pnlNotApplyMTS)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.pnlBottom)
        Me.KeyPreview = True
        Me.Name = "FGJ02"
        Me.Text = "채혈/접수 취소"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlNotApplyMTS.ResumeLayout(False)
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
    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

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

    Public Sub Set_StartInfo(ByVal rsRegNo As String)

        Me.txtSearch.Text = rsRegNo

        txtSearch_KeyDown(Nothing, New System.Windows.Forms.KeyEventArgs(Keys.Enter))

    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Handles btnReg.Click"

        Try

            If fnValidation() = False Then Exit Sub

            Dim al_CancelInto As ArrayList = fnGet_CancleInfo()

            If al_CancelInto.Count > 0 Then
                If MsgBox("선택항목을 정말로 " + Me.btnReg.Text + " 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If
                Dim sJobGbn As String = ""

                If rdoGbn0.Checked = True Then
                    sJobGbn = "0"
                ElseIf rdoGbn1.Checked = True Then
                    sJobGbn = "1"
                ElseIf rdoGbn2.Checked = True Then
                    sJobGbn = "2"
                ElseIf rdoGbn3.Checked = True Then
                    sJobGbn = "3"
                End If

                Dim stu As New STU_CANCELWEB

                stu.JOBGBN = sJobGbn
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
                'Dim sRet As String = (New Cancel).ExecuteDo(sJobGbn, al_CancelInto)

                If sRet = "00" Then

                    If (stu.JOBGBN = "2" Or stu.JOBGBN = "3") And msnCov Then 'jjh 특수보고서 결과값 삭제 (접수취소/reject)
                        Call FnExe_Delete_LRS17M(stu.BCNOS, stu.TESTCDS)
                    End If

                    If stu.JOBGBN = "0" Or stu.JOBGBN = "1" Or stu.JOBGBN = "3" Then 'jjh 자체응급 삭제 (채혈접수취소/채혈취소/REJECT)
                        Call FnExe_Delete_LJ015M(stu.BCNOS, stu.REGNO)
                    End If


                    sbFormClear(0)
                    Me.txtSearch.Focus()

                    If Me.lblSearch.Text = "등록번호" Then
                        Dim dt As DataTable = fnGet_PatInfo_List(Me.txtSearch.Text, "", IIf(USER_INFO.USRLVL = "O", USER_INFO.N_WARDorDEPT, "").ToString, IIf(USER_INFO.USRLVL = "W", USER_INFO.N_WARDorDEPT, "").ToString)

                        If dt.Rows.Count > 0 Then
                            If MsgBox("계속 진행하시겠습니까?", MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                                txtSearch_KeyDown(Nothing, New System.Windows.Forms.KeyEventArgs(Keys.Enter))
                            End If
                        End If
                    End If
                Else
                    CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, sRet)
                End If
            Else
                CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, Me.btnReg.Text + "할 검사코드를 선택해 주십시오.")
            End If

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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
#End Region

#Region " Form내부 함수 "
    ' Form초기화
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim objCommFn As New Fn

        Try
            sbFormClear(0)

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            sbSpreadColHidden(True)

            If USER_INFO.USRLVL = "W" Or USER_INFO.USRLVL = "O" Or USER_INFO.USRLVL = "P" Then
                Me.btnToggle_Click(Nothing, Nothing)
            End If

            ' MTS미적용처리버튼 임시????
            '--------------------------------------------------------------
            If USER_INFO.USRLVL = "S" Then
            Else
                Me.pnlNotApplyMTS.Visible = False
            End If

#If DEBUG Then
            If USER_INFO.USRLVL = "S" Then
                Me.pnlNotApplyMTS.Visible = True
            End If
#End If
            '--------------------------------------------------------------

            Call sbDisplay_Cancel()  ''' 취소사유 콤보에 select 값 없으면 에러남 

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal riPhase As Integer)
        Dim sFn As String = "Private Sub fnFormClear(ByVal aiPhase As Integer)"

        Try
            If InStr("0", riPhase.ToString, CompareMethod.Text) > 0 Then
                'If USER_INFO.USRLVL = "P" Or USER_INFO.USRLVL = "W" Then txtSearch.Text = ""
                chkNotApplyMTS.Checked = False
            End If

            If InStr("01", riPhase.ToString, CompareMethod.Text) > 0 Then
                spdList.MaxRows = 0

                '''lblSpcFlag1.Visible = False
                '''lblSpcFlag2.Visible = False
                lblBCNO.Text = ""
                lblOrdDt.Text = ""
                lblRegNo.Text = ""
                lblPatNm.Text = ""
                lblSexAge.Text = ""
                lblIdNo.Text = ""
                lblDoctor.Text = ""
                lblDeptNm.Text = ""
                lblWard_SR.Text = ""
                lblSpcNm.Text = ""

                lblCollectDt.Text = "" : lblCollectID.Text = ""
                lblPassDt.Text = "" : lblPassId.Text = ""
                lblTkDt.Text = "" : lblTkID.Text = ""

                'txtCmtCd.Text = ""
                'txtCmtCont.Text = ""
                'If cboCancel.Items.Count > 0 Then cboCancel.SelectedIndex = 0

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

        Try
            ' 기능사용 유무 
            Dim sDesc As String = ""
            If rdoGbn0.Checked Or rdoGbn2.Checked Then
                ' 접수취소 
                If Not USER_SKILL.Authority("J01", 2, sDesc) Then
                    MsgBox("[" & sDesc & "] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                    Return False
                End If
            End If

            If rdoGbn1.Checked Then
                ' 채혈취소
                If Not USER_SKILL.Authority("J01", 1, sDesc) Then
                    MsgBox("[" & sDesc & "] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                    Return False
                End If
            End If

            If rdoGbn3.Checked Then
                ' Reject
                If Not USER_SKILL.Authority("J01", 3, sDesc) Then
                    MsgBox("[" & sDesc & "] 권한이 없어 처리할 수 없습니다", MsgBoxStyle.Information, Me.Text)
                    Return False
                End If
            End If


            If Me.spdList.MaxRows < 1 Then
                MsgBox(Me.btnReg.Text & "할 검체번호를 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtSearch.Focus()
                Return False
            End If

            If Me.txtCmtCont.Text = "" Then
                MsgBox(Me.btnReg.Text & " 사유를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Me.txtCmtCont.Focus()
                Return False
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

            Dim dt As DataTable = FGJ02_GetOrderList(rsBcNo)
            If dt.Rows.Count = 0 Then Return

            Dim sSpcFlg As String = ""
            Dim sRstFlg As String = ""

            With spdList
                .MaxRows = dt.Rows.Count
                For ix As Integer = 0 To dt.Rows.Count - 1

                    sSpcFlg = dt.Rows(ix).Item("spcflg").ToString.Trim
                    If sRstFlg < dt.Rows(ix).Item("rstflg").ToString Then sRstFlg = dt.Rows(ix).Item("rstflg").ToString.Trim

                    .Row = ix + 1
                    .Col = .GetColFromID("bcno") : .Text = dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("tclscd") : .Text = dt.Rows(ix).Item("tclscd").ToString.Trim
                    .Col = .GetColFromID("tnmd") : .Text = dt.Rows(ix).Item("tnmd").ToString.Trim
                    .Col = .GetColFromID("doctorrmk") : .Text = dt.Rows(ix).Item("doctorrmk").ToString.Trim

                    .Col = .GetColFromID("spccd") : .Text = dt.Rows(ix).Item("spccd").ToString.Trim
                    .Col = .GetColFromID("tcdgbn") : .Text = dt.Rows(ix).Item("tcdgbn").ToString.Trim
                    .Col = .GetColFromID("rstflg") : .Text = dt.Rows(ix).Item("rstflg").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("iogbn") : .Text = dt.Rows(ix).Item("iogbn").ToString.Trim
                    .Col = .GetColFromID("fkocs") : .Text = dt.Rows(ix).Item("fkocs").ToString.Trim
                    .Col = .GetColFromID("owngbn") : .Text = dt.Rows(ix).Item("owngbn").ToString.Trim
                    .Col = .GetColFromID("bcclscd") : .Text = dt.Rows(ix).Item("bcclscd").ToString.Trim
                    .Col = .GetColFromID("tordcd") : .Text = dt.Rows(ix).Item("tordcd").ToString.Trim
                    .Col = .GetColFromID("spcflg") : .Text = dt.Rows(ix).Item("spcflg_j1").ToString.Trim
                    .Col = .GetColFromID("cfmsign") : .Text = dt.Rows(ix).Item("cfmsign").ToString.Trim

                    Select Case dt.Rows(ix).Item("rstflg").ToString.Trim
                        Case "1"
                            .Col = .GetColFromID("rststate") : .Text = "검사중"
                        Case "2"
                            .Col = .GetColFromID("rststate") : .Text = "중간보고"
                        Case "3"
                            .Col = .GetColFromID("rststate") : .Text = "최종보고"
                    End Select


                    If ix = 0 Then
                        Me.lblBCNO.Text = Fn.BCNO_View(dt.Rows(ix).Item("bcno").ToString.Trim, True)
                        Me.lblOrdDt.Text = dt.Rows(ix).Item("orddt").ToString.Trim
                        Me.lblRegNo.Text = dt.Rows(ix).Item("regno").ToString.Trim
                        Me.lblPatNm.Text = dt.Rows(ix).Item("patnm").ToString.Trim
                        Me.lblSexAge.Text = dt.Rows(ix).Item("sexage").ToString.Trim

                        Dim sPatInfo() As String = dt.Rows(ix).Item("patinfo").ToString.Split("|"c) ''' 정은수정 
                        Me.lblIdNo.Text = sPatInfo(3).Trim ''' 정은 수정 
                        Me.lblDoctor.Text = dt.Rows(ix).Item("doctornm").ToString.Trim
                        Me.lblDeptNm.Text = dt.Rows(ix).Item("deptnm").ToString.Trim
                        Me.lblWard_SR.Text = dt.Rows(ix).Item("wardroom").ToString.Trim
                        Me.lblSpcNm.Text = dt.Rows(ix).Item("spcnmd").ToString.Trim

                        Me.lblCollectDt.Text = dt.Rows(ix).Item("colldt").ToString.Trim
                        Me.lblCollectID.Text = dt.Rows(ix).Item("collnm").ToString.Trim
                        Me.lblPassDt.Text = dt.Rows(ix).Item("passdt").ToString.Trim
                        Me.lblPassId.Text = dt.Rows(ix).Item("passnm").ToString.Trim
                        Me.lblTkDt.Text = dt.Rows(ix).Item("tkdt").ToString.Trim
                        Me.lblTkID.Text = dt.Rows(ix).Item("tknm").ToString.Trim

                    End If

                Next
            End With

            If sSpcFlg <= "3" Then  ''' 바코드출력 바뀐 상태값 적용   정은 2010-10-11
                Select Case sSpcFlg
                    Case "1"
                        Me.lblSpcFlag1.BackColor = System.Drawing.Color.FromArgb(165, 0, 123) ''' 바코드출력
                        Me.lblSpcFlag2.BackColor = System.Drawing.Color.LightGray             ''' 채혈    
                        Me.lblSpcFlag3.BackColor = System.Drawing.Color.LightGray             ''' 검체전달
                    Case "2"
                        Me.lblSpcFlag1.BackColor = System.Drawing.Color.LightGray ''' 바코드출력
                        Me.lblSpcFlag2.BackColor = System.Drawing.Color.FromArgb(165, 0, 123)              ''' 채혈    
                        Me.lblSpcFlag3.BackColor = System.Drawing.Color.LightGray             ''' 검체전달
                    Case "3"
                        Me.lblSpcFlag1.BackColor = System.Drawing.Color.LightGray ''' 바코드출력
                        Me.lblSpcFlag2.BackColor = System.Drawing.Color.LightGray             ''' 채혈    
                        Me.lblSpcFlag3.BackColor = System.Drawing.Color.FromArgb(165, 0, 123)             ''' 검체전달
                End Select

                Me.lblSpcFlag4.BackColor = System.Drawing.Color.LightGray             ''' 접수

                rdoGbn0.Enabled = False  ''' 채혈/접수취소 
                rdoGbn1.Enabled = True   ''' 채혈취소 
                rdoGbn2.Enabled = False  ''' 접수취소 
                rdoGbn3.Enabled = False  ''' Reject 

                rdoGbn1.Checked = True
                rdoGbn_Click(rdoGbn1, Nothing)

            ElseIf sSpcFlg = "4" Then  ''' 접수상태 바뀐 상태값 적용   정은 2010-10-11
                lblSpcFlag1.BackColor = System.Drawing.Color.LightGray             ''' 바코드출력
                lblSpcFlag2.BackColor = System.Drawing.Color.LightGray             ''' 채혈     
                lblSpcFlag3.BackColor = System.Drawing.Color.LightGray             ''' 검체전달
                lblSpcFlag4.BackColor = System.Drawing.Color.FromArgb(165, 0, 123) ''' 접수

                rdoGbn1.Enabled = False  ''' 채혈취소 
                rdoGbn3.Enabled = True   ''' Reject 

                If sRstFlg > "0" Then
                    rdoGbn0.Enabled = False   ''' 채혈/접수취소 
                    rdoGbn2.Enabled = False   ''' 접수취소 
                    rdoGbn3.Checked = True

                    rdoGbn_Click(rdoGbn3, Nothing)

                Else
                    rdoGbn0.Enabled = True   ''' 채혈/접수취소 
                    rdoGbn2.Enabled = True   ''' 접수취소 
                    rdoGbn2.Checked = True

                    rdoGbn_Click(rdoGbn2, Nothing)

                End If


            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 취소할 항목 ArrayList에 Add
    Private Function fnGet_CancleInfo() As ArrayList
        Dim sFn As String = "Private Sub fnGet_CancleInfo( ArrayList)"
        Try
            Dim stu As STU_CANCELINFO

            Dim sRstStat As String = ""
            Dim sOrdNm As String = ""
            Dim al_CancelInfo As New ArrayList

            msnCov = False

            With Me.spdList
                For ix As Integer = 1 To .MaxRows
                    .Row = ix
                    .Col = .GetColFromID("rstflg") : sRstStat = .Text
                    .Col = .GetColFromID("tnmd") : sOrdNm = .Text
                    .Col = .GetColFromID("cfmsign") : Dim sCfmsign As String = .Text

                    .Col = .GetColFromID("chk")
                    If .Text = "1" Then

                        If sCfmsign <> "" Then
                            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "I"c, "인증 보고된 자료라 취소할 수 없습니다.!!")
                            Return New ArrayList
                        End If

                        If Not (sRstStat = "0" Or sRstStat = "") And Me.rdoGbn3.Checked = False Then
                            MsgBox("[" + sOrdNm + "]은(는) 결과상태가 " + sRstStat + "이므로 " + Me.btnReg.Text + "할 수 없습니다.", MsgBoxStyle.Information, Me.Text)
                            Return New ArrayList
                        End If

                        stu = New STU_CANCELINFO
                        .Col = .GetColFromID("bcno") : stu.BCNO = .Text
                        .Col = .GetColFromID("tclscd") : stu.TCLSCD = .Text
                        .Col = .GetColFromID("spccd") : stu.SPCCD = .Text
                        .Col = .GetColFromID("tcdgbn") : stu.TCDGBN = .Text
                        .Col = .GetColFromID("regno") : stu.REGNO = .Text
                        .Col = .GetColFromID("iogbn") : stu.IOGBN = .Text
                        .Col = .GetColFromID("fkocs") : stu.FKOCS = .Text
                        .Col = .GetColFromID("owngbn") : stu.OWNGBN = .Text
                        .Col = .GetColFromID("bcclscd") : stu.BCCLSCD = .Text

                        '< yjlee
                        .Col = .GetColFromID("tordcd") : stu.TORDCD = .Text
                        '> 
                        .Col = .GetColFromID("spcflg") : stu.SPCFLG = .Text


                        stu.CANCELCD = Me.txtCmtCd.Text
                        stu.CANCELCMT = Me.txtCmtCont.Text

                        '<JJH 코로나 특수보고서
                        Dim nCovTestcd_dt As DataTable = LISAPP.COMM.RstFn.fnGet_BfRst_Testcd()
                        Dim nCovTestcd As String() = nCovTestcd_dt.Rows(0).Item("CLSVAL").ToString.Split("/"c)

                        For i As Integer = 0 To nCovTestcd.Count - 1
                            If nCovTestcd(i).ToString = stu.TCLSCD Then
                                msnCov = True
                            End If
                        Next
                        '>


                        al_CancelInfo.Add(stu)
                    End If
                Next
            End With
            al_CancelInfo.TrimToSize()

            Return al_CancelInfo

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

            Return New ArrayList
        End Try

    End Function

    ' 취소 
    Private Sub sbCancel()
        Dim sFn As String = "Private Sub sbCancel()"
        Dim alOrdList As New ArrayList
        Try

            alOrdList = fnGet_CancleInfo()
            If alOrdList.Count > 0 Then
                If MsgBox("선택항목을 정말로 " + Me.btnReg.Text + " 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If

                With (New Cancel)
                    .CancelTItem = alOrdList
                    .CancelCmt = Me.txtCmtCont.Text
                    .CancelCd = Me.txtCmtCd.Text

                    ' 관리자 Wittyman만 가능함 MTS적용 유무
                    .NotApplyMTS = chkNotApplyMTS.Checked

                    Dim sRet As String = ""
                    If rdoGbn0.Checked = True Then
                        sRet = .ExecuteDo(enumCANCEL.채혈접수취소, lblUserId.Text)
                    ElseIf rdoGbn1.Checked = True Then
                        sRet = .ExecuteDo(enumCANCEL.채혈취소, lblUserId.Text)
                    ElseIf rdoGbn2.Checked = True Then
                        sRet = .ExecuteDo(enumCANCEL.접수취소, lblUserId.Text)
                    ElseIf rdoGbn3.Checked = True Then
                        sRet = .ExecuteDo(enumCANCEL.REJECT, lblUserId.Text)
                    End If

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

    Private Sub sbDisplay_Cancel()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_cmtcont_etc(IIf(USER_INFO.USRLVL = "W", "0", "1").ToString, True)

        cboCancel.Items.Clear()
        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboCancel.Items.Add("[" + dt.Rows(ix).Item("cmtcd").ToString + "] " + dt.Rows(ix).Item("cmtcont").ToString)
            Next
        End If

        '< yjlee 2009-03-04
        ' 부천순천향병원 병동에서 호출한 접수취소화면일 경우 취소사유 디폴트 설정
        Select Case USER_INFO.USRLVL
            Case "W", "N"
                If cboCancel.Items.Count > 0 Then
                    cboCancel.SelectedIndex = 1
                Else
                    cboCancel.SelectedIndex = 0
                End If
            Case Else
                cboCancel.SelectedIndex = 0
        End Select
        '> yjlee 2009-03-04 

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

    Private Sub rdoGbn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGbn0.Click, rdoGbn1.Click, rdoGbn2.Click, rdoGbn3.Click
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
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub txtSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Me.txtSearch.Focus()
        Me.txtSearch.SelectAll()
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

        If mstrSPDAction <> "" Then Exit Sub

        With spdList
            .Row = e.row
            .Col = 1 : strChk = .Text
            .Col = .GetColFromID("fkocs") : strFKocs = .Text
            mstrSPDAction = "1"
            For i As Integer = 1 To .MaxRows
                .Row = i
                .Col = .GetColFromID("fkocs") : strTmp = .Text
                If i <> e.row And strTmp = strFKocs Then
                    .Row = i
                    .Col = 1 : .Text = strChk
                End If
            Next
            mstrSPDAction = ""
        End With
    End Sub
#End Region

    Private Sub cboCancel_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCancel.SelectedValueChanged

        If cboCancel.Text <> "" Then
            txtCmtCont.Text = Ctrl.Get_Name(cboCancel)
            txtCmtCd.Text = Ctrl.Get_Code(cboCancel)
        End If

        If txtCmtCont.Text = "" Then
            txtCmtCont.Focus()
        Else
            btnReg.Focus()
        End If

    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        txtSearch.Focus()
        txtSearch.SelectAll()
    End Sub

    Private Sub txtCancelCd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCmtCd.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        If txtCmtCd.Text = "" Then Return

        Dim sFn As String = "Sub btnCdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCdHelp.Click"

        Try
            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            Dim strCds As String = txtCmtCd.Text
            If strCds.IndexOf("?") < 0 Then strCds += "%"

            objHelp.FormText = "취소 사유 내용"
            objHelp.TableNm = "LF410M"

            If USER_INFO.USRLVL = "W" Then
                objHelp.Where = "CMTGBN = '0' and and (CMTCD like '" + strCds.Replace("?", "%") + "' or CMTCONT like '" + strCds.Replace("?", "%") + "')"
            Else
                objHelp.Where = "CMTGBN = '1' and and (CMTCD like '" + strCds.Replace("?", "%") + "' or CMTCONT like '" + strCds.Replace("?", "%") + "')"
            End If
            objHelp.GroupBy = ""
            objHelp.OrderBy = "CMTCONT"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("CMTCONT", "내용", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("CMTGBN || CMTCD CMTCD", "코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "CMTCD")

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(txtCmtCd)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtCmtCd.Height + 80)

            If aryList.Count > 0 Then
                txtCmtCont.Text += aryList.Item(0).ToString.Split("|"c)(0) + vbCrLf
                txtCmtCd.Text = aryList.Item(0).ToString.Split("|"c)(1)

                For intIdx As Integer = 0 To cboCancel.Items.Count - 1
                    cboCancel.SelectedIndex = intIdx
                    If cboCancel.Text = "[" + txtCmtCd.Text + "] " + txtCmtCont.Text Then
                        cboCancel.SelectedIndex = intIdx
                        Exit For
                    End If
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub FGJ02_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Me.lblSearch.Text = "등록번호" Then Me.txtSearch.Text = msRegNo

    End Sub


    Private Sub FGJ_close(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress"

        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim sRegNo As String = ""
            Dim sBcNo As String = ""

            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()

            If Me.txtSearch.Text.Equals("") Then Return

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

                If Me.lblSearch.Text = "검체번호" Then Me.txtSearch.Text = ""
            Else
                MsgBox("해당하는 환자가 없습니다.", MsgBoxStyle.Information, Me.Text)
            End If
            Me.txtSearch.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

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
