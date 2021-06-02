'>>> 바코드 재출력
Imports System.Windows.Forms

Imports COMMON.SVar
Imports COMMON.CommFN
Imports COMMON.CommLogin.LOGIN

Imports PRTAPP.APP_BC.BCPrinter

Imports LISAPP.APP_DB
Imports LISAPP.APP_J
Imports LISAPP.APP_J.TkFn
Imports PRTAPP.APP_BC

Public Class FGJ03
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGJ03.vb, Class : J01" & vbTab

    Private msRegNo As String = ""
    Private mbLoad As Boolean = False

    Friend WithEvents cboSR As System.Windows.Forms.ComboBox
    Friend WithEvents cboDeptCd As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblBcColor3 As System.Windows.Forms.Label
    Friend WithEvents lblBcColor2 As System.Windows.Forms.Label
    Friend WithEvents lblBcColor1 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm3 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm2 As System.Windows.Forms.Label
    Friend WithEvents lblBcclsNm1 As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnPrint As CButtonLib.CButton
    Friend WithEvents btnSelBCPRT As System.Windows.Forms.Button
    Friend WithEvents btnQuery As CButtonLib.CButton
    Friend WithEvents ntxtPrtCount As AxAckNumericTextBox.NumericTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkBar_cult As System.Windows.Forms.CheckBox
    Friend WithEvents chkBarInit As System.Windows.Forms.CheckBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents chkMultiBc As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label


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
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents spdList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents btnToggle As System.Windows.Forms.Button
    Friend WithEvents pnlBottom As System.Windows.Forms.Panel
    Friend WithEvents dtpDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDate0 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblBarPrinter As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBccls As System.Windows.Forms.ComboBox
    Friend WithEvents cboWard As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chk As System.Windows.Forms.CheckBox
    Friend WithEvents chkMoveColl As System.Windows.Forms.CheckBox
    Friend WithEvents lblLabel As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGJ03))
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
        Me.btnToggle = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboDeptCd = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboSR = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboWard = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboBccls = New System.Windows.Forms.ComboBox()
        Me.lblLabel = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpDate1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpDate0 = New System.Windows.Forms.DateTimePicker()
        Me.chkMoveColl = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chk = New System.Windows.Forms.CheckBox()
        Me.spdList = New AxFPSpreadADO.AxfpSpread()
        Me.pnlBottom = New System.Windows.Forms.Panel()
        Me.chkMultiBc = New System.Windows.Forms.CheckBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.chkBar_cult = New System.Windows.Forms.CheckBox()
        Me.btnClear = New CButtonLib.CButton()
        Me.ntxtPrtCount = New AxAckNumericTextBox.NumericTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.chkBarInit = New System.Windows.Forms.CheckBox()
        Me.btnSelBCPRT = New System.Windows.Forms.Button()
        Me.lblBarPrinter = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblUserNm = New System.Windows.Forms.Label()
        Me.lblUserId = New System.Windows.Forms.Label()
        Me.btnPrint = New CButtonLib.CButton()
        Me.btnQuery = New CButtonLib.CButton()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblBcColor3 = New System.Windows.Forms.Label()
        Me.lblBcColor2 = New System.Windows.Forms.Label()
        Me.lblBcColor1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblBcclsNm3 = New System.Windows.Forms.Label()
        Me.lblBcclsNm2 = New System.Windows.Forms.Label()
        Me.lblBcclsNm1 = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.btnExit = New CButtonLib.CButton()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlBottom.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnToggle
        '
        Me.btnToggle.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnToggle.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnToggle.Location = New System.Drawing.Point(543, 11)
        Me.btnToggle.Margin = New System.Windows.Forms.Padding(0)
        Me.btnToggle.Name = "btnToggle"
        Me.btnToggle.Size = New System.Drawing.Size(33, 21)
        Me.btnToggle.TabIndex = 3
        Me.btnToggle.Text = "<->"
        '
        'txtSearch
        '
        Me.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearch.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSearch.Font = New System.Drawing.Font("굴림체", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(391, 11)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(1)
        Me.txtSearch.Multiline = True
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(151, 21)
        Me.txtSearch.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblSearch.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSearch.ForeColor = System.Drawing.Color.White
        Me.lblSearch.Location = New System.Drawing.Point(310, 11)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(80, 21)
        Me.lblSearch.TabIndex = 2
        Me.lblSearch.Text = "검체번호"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboDeptCd)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cboSR)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cboWard)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboBccls)
        Me.GroupBox1.Controls.Add(Me.lblLabel)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtpDate1)
        Me.GroupBox1.Controls.Add(Me.dtpDate0)
        Me.GroupBox1.Controls.Add(Me.lblSearch)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.btnToggle)
        Me.GroupBox1.Controls.Add(Me.chkMoveColl)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1083, 58)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'cboDeptCd
        '
        Me.cboDeptCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDeptCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboDeptCd.Location = New System.Drawing.Point(257, 33)
        Me.cboDeptCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboDeptCd.Name = "cboDeptCd"
        Me.cboDeptCd.Size = New System.Drawing.Size(174, 20)
        Me.cboDeptCd.TabIndex = 56
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(176, 33)
        Me.Label6.Margin = New System.Windows.Forms.Padding(0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "진료과"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboSR
        '
        Me.cboSR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSR.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboSR.Location = New System.Drawing.Point(257, 33)
        Me.cboSR.Margin = New System.Windows.Forms.Padding(1)
        Me.cboSR.Name = "cboSR"
        Me.cboSR.Size = New System.Drawing.Size(64, 20)
        Me.cboSR.TabIndex = 54
        Me.cboSR.Visible = False
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(176, 33)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 20)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "병실"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label5.Visible = False
        '
        'cboWard
        '
        Me.cboWard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboWard.Location = New System.Drawing.Point(85, 33)
        Me.cboWard.Margin = New System.Windows.Forms.Padding(1)
        Me.cboWard.Name = "cboWard"
        Me.cboWard.Size = New System.Drawing.Size(90, 20)
        Me.cboWard.TabIndex = 49
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(4, 33)
        Me.Label4.Margin = New System.Windows.Forms.Padding(0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 50
        Me.Label4.Text = "병동"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboBccls
        '
        Me.cboBccls.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBccls.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBccls.Location = New System.Drawing.Point(513, 33)
        Me.cboBccls.Margin = New System.Windows.Forms.Padding(1)
        Me.cboBccls.Name = "cboBccls"
        Me.cboBccls.Size = New System.Drawing.Size(129, 20)
        Me.cboBccls.TabIndex = 47
        '
        'lblLabel
        '
        Me.lblLabel.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblLabel.ForeColor = System.Drawing.Color.Black
        Me.lblLabel.Location = New System.Drawing.Point(432, 33)
        Me.lblLabel.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLabel.Name = "lblLabel"
        Me.lblLabel.Size = New System.Drawing.Size(80, 20)
        Me.lblLabel.TabIndex = 48
        Me.lblLabel.Text = "검체분류"
        Me.lblLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(4, 11)
        Me.Label14.Margin = New System.Windows.Forms.Padding(0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 21)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "채혈일자"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(178, 16)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "~"
        '
        'dtpDate1
        '
        Me.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate1.Location = New System.Drawing.Point(194, 11)
        Me.dtpDate1.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate1.Name = "dtpDate1"
        Me.dtpDate1.Size = New System.Drawing.Size(90, 21)
        Me.dtpDate1.TabIndex = 1
        '
        'dtpDate0
        '
        Me.dtpDate0.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDate0.Location = New System.Drawing.Point(85, 11)
        Me.dtpDate0.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpDate0.Name = "dtpDate0"
        Me.dtpDate0.Size = New System.Drawing.Size(90, 21)
        Me.dtpDate0.TabIndex = 0
        '
        'chkMoveColl
        '
        Me.chkMoveColl.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMoveColl.Font = New System.Drawing.Font("굴림체", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.chkMoveColl.Location = New System.Drawing.Point(975, 10)
        Me.chkMoveColl.Name = "chkMoveColl"
        Me.chkMoveColl.Size = New System.Drawing.Size(96, 15)
        Me.chkMoveColl.TabIndex = 53
        Me.chkMoveColl.Text = "컬럼이동모드"
        Me.chkMoveColl.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.chkMoveColl.UseVisualStyleBackColor = True
        Me.chkMoveColl.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.chk)
        Me.Panel1.Controls.Add(Me.spdList)
        Me.Panel1.Location = New System.Drawing.Point(4, 65)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1084, 492)
        Me.Panel1.TabIndex = 3
        '
        'chk
        '
        Me.chk.AutoSize = True
        Me.chk.Location = New System.Drawing.Point(37, 9)
        Me.chk.Name = "chk"
        Me.chk.Size = New System.Drawing.Size(15, 14)
        Me.chk.TabIndex = 1
        Me.chk.UseVisualStyleBackColor = True
        '
        'spdList
        '
        Me.spdList.DataSource = Nothing
        Me.spdList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdList.Location = New System.Drawing.Point(0, 0)
        Me.spdList.Name = "spdList"
        Me.spdList.OcxState = CType(resources.GetObject("spdList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdList.Size = New System.Drawing.Size(1082, 490)
        Me.spdList.TabIndex = 0
        '
        'pnlBottom
        '
        Me.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlBottom.Controls.Add(Me.chkMultiBc)
        Me.pnlBottom.Controls.Add(Me.btnTest)
        Me.pnlBottom.Controls.Add(Me.chkBar_cult)
        Me.pnlBottom.Controls.Add(Me.btnClear)
        Me.pnlBottom.Controls.Add(Me.ntxtPrtCount)
        Me.pnlBottom.Controls.Add(Me.Label1)
        Me.pnlBottom.Controls.Add(Me.Panel5)
        Me.pnlBottom.Controls.Add(Me.lblUserNm)
        Me.pnlBottom.Controls.Add(Me.lblUserId)
        Me.pnlBottom.Controls.Add(Me.btnPrint)
        Me.pnlBottom.Controls.Add(Me.btnQuery)
        Me.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom.Location = New System.Drawing.Point(0, 595)
        Me.pnlBottom.Name = "pnlBottom"
        Me.pnlBottom.Size = New System.Drawing.Size(1091, 34)
        Me.pnlBottom.TabIndex = 4
        '
        'chkMultiBc
        '
        Me.chkMultiBc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkMultiBc.AutoSize = True
        Me.chkMultiBc.Location = New System.Drawing.Point(521, 9)
        Me.chkMultiBc.Name = "chkMultiBc"
        Me.chkMultiBc.Size = New System.Drawing.Size(72, 16)
        Me.chkMultiBc.TabIndex = 190
        Me.chkMultiBc.Text = "다중출력"
        Me.chkMultiBc.UseVisualStyleBackColor = True
        '
        'btnTest
        '
        Me.btnTest.Location = New System.Drawing.Point(428, 3)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(75, 23)
        Me.btnTest.TabIndex = 189
        Me.btnTest.Text = "테스트"
        Me.btnTest.UseVisualStyleBackColor = True
        '
        'chkBar_cult
        '
        Me.chkBar_cult.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkBar_cult.AutoSize = True
        Me.chkBar_cult.Location = New System.Drawing.Point(600, 8)
        Me.chkBar_cult.Name = "chkBar_cult"
        Me.chkBar_cult.Size = New System.Drawing.Size(84, 16)
        Me.chkBar_cult.TabIndex = 188
        Me.chkBar_cult.Text = "배지바코드"
        Me.chkBar_cult.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnClear.Location = New System.Drawing.Point(886, 3)
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
        'ntxtPrtCount
        '
        Me.ntxtPrtCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ntxtPrtCount.Location = New System.Drawing.Point(382, 7)
        Me.ntxtPrtCount.Name = "ntxtPrtCount"
        Me.ntxtPrtCount.Size = New System.Drawing.Size(27, 21)
        Me.ntxtPrtCount.TabIndex = 163
        Me.ntxtPrtCount.Text = "999.99"
        Me.ntxtPrtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.SlateGray
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(317, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 23)
        Me.Label1.TabIndex = 164
        Me.Label1.Text = "출력장수"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.chkBarInit)
        Me.Panel5.Controls.Add(Me.btnSelBCPRT)
        Me.Panel5.Controls.Add(Me.lblBarPrinter)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Location = New System.Drawing.Point(4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(306, 24)
        Me.Panel5.TabIndex = 162
        '
        'chkBarInit
        '
        Me.chkBarInit.AutoSize = True
        Me.chkBarInit.Location = New System.Drawing.Point(73, 5)
        Me.chkBarInit.Name = "chkBarInit"
        Me.chkBarInit.Size = New System.Drawing.Size(15, 14)
        Me.chkBarInit.TabIndex = 225
        Me.chkBarInit.UseVisualStyleBackColor = True
        '
        'btnSelBCPRT
        '
        Me.btnSelBCPRT.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSelBCPRT.ForeColor = System.Drawing.Color.Black
        Me.btnSelBCPRT.Image = CType(resources.GetObject("btnSelBCPRT.Image"), System.Drawing.Image)
        Me.btnSelBCPRT.Location = New System.Drawing.Point(276, 0)
        Me.btnSelBCPRT.Name = "btnSelBCPRT"
        Me.btnSelBCPRT.Size = New System.Drawing.Size(30, 24)
        Me.btnSelBCPRT.TabIndex = 188
        Me.btnSelBCPRT.UseVisualStyleBackColor = False
        '
        'lblBarPrinter
        '
        Me.lblBarPrinter.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBarPrinter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBarPrinter.ForeColor = System.Drawing.Color.Black
        Me.lblBarPrinter.Location = New System.Drawing.Point(94, 1)
        Me.lblBarPrinter.Name = "lblBarPrinter"
        Me.lblBarPrinter.Size = New System.Drawing.Size(184, 23)
        Me.lblBarPrinter.TabIndex = 102
        Me.lblBarPrinter.Text = "AUTO LABELER (외래채혈실)"
        Me.lblBarPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(0, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 23)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = " 출력프린터"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUserNm
        '
        Me.lblUserNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserNm.ForeColor = System.Drawing.Color.White
        Me.lblUserNm.Location = New System.Drawing.Point(492, 8)
        Me.lblUserNm.Name = "lblUserNm"
        Me.lblUserNm.Size = New System.Drawing.Size(76, 20)
        Me.lblUserNm.TabIndex = 161
        Me.lblUserNm.Text = "관리자"
        Me.lblUserNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserNm.Visible = False
        '
        'lblUserId
        '
        Me.lblUserId.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUserId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUserId.ForeColor = System.Drawing.Color.White
        Me.lblUserId.Location = New System.Drawing.Point(420, 8)
        Me.lblUserId.Name = "lblUserId"
        Me.lblUserId.Size = New System.Drawing.Size(68, 20)
        Me.lblUserId.TabIndex = 160
        Me.lblUserId.Text = "ACK"
        Me.lblUserId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblUserId.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnPrint.ColorFillBlend = CBlendItems2
        Me.btnPrint.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnPrint.Corners.All = CType(6, Short)
        Me.btnPrint.Corners.LowerLeft = CType(6, Short)
        Me.btnPrint.Corners.LowerRight = CType(6, Short)
        Me.btnPrint.Corners.UpperLeft = CType(6, Short)
        Me.btnPrint.Corners.UpperRight = CType(6, Short)
        Me.btnPrint.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnPrint.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnPrint.FocalPoints.CenterPtX = 0.371134!
        Me.btnPrint.FocalPoints.CenterPtY = 0.0!
        Me.btnPrint.FocalPoints.FocusPtX = 0.0!
        Me.btnPrint.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnPrint.FocusPtTracker = DesignerRectTracker4
        Me.btnPrint.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Image = Nothing
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnPrint.ImageIndex = 0
        Me.btnPrint.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnPrint.Location = New System.Drawing.Point(788, 3)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnPrint.SideImage = Nothing
        Me.btnPrint.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnPrint.Size = New System.Drawing.Size(97, 25)
        Me.btnPrint.TabIndex = 186
        Me.btnPrint.Text = "재출력(F5)"
        Me.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnPrint.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnQuery
        '
        Me.btnQuery.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnQuery.ColorFillBlend = CBlendItems3
        Me.btnQuery.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnQuery.Corners.All = CType(6, Short)
        Me.btnQuery.Corners.LowerLeft = CType(6, Short)
        Me.btnQuery.Corners.LowerRight = CType(6, Short)
        Me.btnQuery.Corners.UpperLeft = CType(6, Short)
        Me.btnQuery.Corners.UpperRight = CType(6, Short)
        Me.btnQuery.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnQuery.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnQuery.FocalPoints.CenterPtX = 0.3814433!
        Me.btnQuery.FocalPoints.CenterPtY = 0.48!
        Me.btnQuery.FocalPoints.FocusPtX = 0.0!
        Me.btnQuery.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnQuery.FocusPtTracker = DesignerRectTracker6
        Me.btnQuery.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnQuery.ForeColor = System.Drawing.Color.White
        Me.btnQuery.Image = Nothing
        Me.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnQuery.ImageIndex = 0
        Me.btnQuery.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnQuery.Location = New System.Drawing.Point(690, 3)
        Me.btnQuery.Name = "btnQuery"
        Me.btnQuery.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnQuery.SideImage = Nothing
        Me.btnQuery.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnQuery.Size = New System.Drawing.Size(97, 25)
        Me.btnQuery.TabIndex = 187
        Me.btnQuery.Text = "조회"
        Me.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnQuery.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.lblBcColor3)
        Me.Panel3.Controls.Add(Me.lblBcColor2)
        Me.Panel3.Controls.Add(Me.lblBcColor1)
        Me.Panel3.Controls.Add(Me.Label9)
        Me.Panel3.Controls.Add(Me.Label45)
        Me.Panel3.Controls.Add(Me.Label46)
        Me.Panel3.Controls.Add(Me.Label32)
        Me.Panel3.Controls.Add(Me.Label44)
        Me.Panel3.Controls.Add(Me.Label26)
        Me.Panel3.Controls.Add(Me.lblBcclsNm3)
        Me.Panel3.Controls.Add(Me.lblBcclsNm2)
        Me.Panel3.Controls.Add(Me.lblBcclsNm1)
        Me.Panel3.Controls.Add(Me.Label40)
        Me.Panel3.Controls.Add(Me.Label34)
        Me.Panel3.Location = New System.Drawing.Point(4, 562)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(751, 27)
        Me.Panel3.TabIndex = 56
        '
        'lblBcColor3
        '
        Me.lblBcColor3.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(82, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.lblBcColor3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor3.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor3.Location = New System.Drawing.Point(324, 5)
        Me.lblBcColor3.Name = "lblBcColor3"
        Me.lblBcColor3.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor3.TabIndex = 205
        Me.lblBcColor3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor2
        '
        Me.lblBcColor2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.lblBcColor2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor2.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor2.Location = New System.Drawing.Point(240, 5)
        Me.lblBcColor2.Name = "lblBcColor2"
        Me.lblBcColor2.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor2.TabIndex = 204
        Me.lblBcColor2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcColor1
        '
        Me.lblBcColor1.BackColor = System.Drawing.Color.FromArgb(CType(CType(205, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(19, Byte), Integer))
        Me.lblBcColor1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBcColor1.ForeColor = System.Drawing.Color.Black
        Me.lblBcColor1.Location = New System.Drawing.Point(155, 5)
        Me.lblBcColor1.Name = "lblBcColor1"
        Me.lblBcColor1.Size = New System.Drawing.Size(18, 16)
        Me.lblBcColor1.TabIndex = 203
        Me.lblBcColor1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.White
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(72, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(18, 16)
        Me.Label9.TabIndex = 202
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label45
        '
        Me.Label45.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label45.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label45.Location = New System.Drawing.Point(562, 2)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(47, 22)
        Me.Label45.TabIndex = 192
        Me.Label45.Text = "진료전"
        Me.Label45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label46
        '
        Me.Label46.BackColor = System.Drawing.Color.White
        Me.Label46.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label46.ForeColor = System.Drawing.Color.MediumBlue
        Me.Label46.Location = New System.Drawing.Point(542, 5)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(18, 16)
        Me.Label46.TabIndex = 191
        Me.Label46.Text = "B"
        Me.Label46.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label32.ForeColor = System.Drawing.Color.Crimson
        Me.Label32.Location = New System.Drawing.Point(500, 2)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(34, 22)
        Me.Label32.TabIndex = 190
        Me.Label32.Text = "응급"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.BackColor = System.Drawing.Color.White
        Me.Label44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label44.ForeColor = System.Drawing.Color.Crimson
        Me.Label44.Location = New System.Drawing.Point(481, 5)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(18, 16)
        Me.Label44.TabIndex = 189
        Me.Label44.Text = "E"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label26
        '
        Me.Label26.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label26.ForeColor = System.Drawing.Color.Black
        Me.Label26.Location = New System.Drawing.Point(403, -1)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(66, 27)
        Me.Label26.TabIndex = 188
        Me.Label26.Text = "응급범례"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblBcclsNm3
        '
        Me.lblBcclsNm3.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm3.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm3.Location = New System.Drawing.Point(345, 3)
        Me.lblBcclsNm3.Name = "lblBcclsNm3"
        Me.lblBcclsNm3.Size = New System.Drawing.Size(53, 21)
        Me.lblBcclsNm3.TabIndex = 187
        Me.lblBcclsNm3.Text = "기타"
        Me.lblBcclsNm3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm2
        '
        Me.lblBcclsNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm2.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm2.Location = New System.Drawing.Point(261, 3)
        Me.lblBcclsNm2.Name = "lblBcclsNm2"
        Me.lblBcclsNm2.Size = New System.Drawing.Size(56, 21)
        Me.lblBcclsNm2.TabIndex = 186
        Me.lblBcclsNm2.Text = "외부의뢰"
        Me.lblBcclsNm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblBcclsNm1
        '
        Me.lblBcclsNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblBcclsNm1.ForeColor = System.Drawing.Color.Black
        Me.lblBcclsNm1.Location = New System.Drawing.Point(176, 3)
        Me.lblBcclsNm1.Name = "lblBcclsNm1"
        Me.lblBcclsNm1.Size = New System.Drawing.Size(56, 21)
        Me.lblBcclsNm1.TabIndex = 185
        Me.lblBcclsNm1.Text = "혈액은행"
        Me.lblBcclsNm1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label40
        '
        Me.Label40.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Label40.ForeColor = System.Drawing.Color.Black
        Me.Label40.Location = New System.Drawing.Point(93, 3)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(56, 21)
        Me.Label40.TabIndex = 184
        Me.Label40.Text = "진단검사"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label34
        '
        Me.Label34.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label34.ForeColor = System.Drawing.Color.Black
        Me.Label34.Location = New System.Drawing.Point(-1, -1)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(66, 27)
        Me.Label34.TabIndex = 179
        Me.Label34.Text = "검사범례"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4615385!
        Me.btnExit.FocalPoints.CenterPtY = 0.72!
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
        Me.btnExit.Location = New System.Drawing.Point(996, 600)
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
        'FGJ03
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1091, 629)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlBottom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGJ03"
        Me.Text = "바코드 재출력"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.spdList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlBottom.ResumeLayout(False)
        Me.pnlBottom.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보이기/숨김 "

    Private Sub FGJ03_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        Me.txtSearch.Focus()
        If mbLoad = False Then
            If txtSearch.Text <> "" Then btnQuery_Click(Nothing, Nothing)
        End If
        mbLoad = True

    End Sub
    Private Sub Form_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.DoubleClick

        If USER_INFO.USRLVL <> "S" Then Exit Sub

#If DEBUG Then
        Static blnChk As Boolean = False

        '-- 컬럼내용모두 보기/감추기
        sbSpreadColHidden(blnChk)
        blnChk = Not blnChk
#End If
    End Sub
#End Region

#Region " 메인 버튼 처리 "

    Private Sub FGJ03_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Dim sFn As String = ""

        Try
            spdList.MaxRows = 0
            chk.Checked = False
            spdList.AllowColMove = False

            MdiTabControl.sbTabPageMove(Me)
        Catch ex As Exception

        End Try
    End Sub


    ' Function Key정의
    Private Sub FGC01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        'F2 : 포커스이동 
        'F4 : 화면정리 
        'F5 : 바코드출력
        'F10: 화면종료

        If e.KeyCode = Keys.F2 Then
            txtSearch.Focus()

        ElseIf e.KeyCode = Keys.F5 Then
            btnPrint_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.F4 Then
            btnClear_Click(Nothing, Nothing)

        ElseIf e.KeyCode = Keys.Escape Then
            Me.Close()

        End If

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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim sFn As String = "Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarPrint.ButtonClick"
        Try
            sbBCPrint()
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

    '-- 2007-11-20 YEJ add
    Private Sub sbDisplay_Bccls()

        Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Bccls_List()
        lblLabel.Text = "검체분류"
        cboBccls.Items.Clear()
        cboBccls.Items.Add("[  ] 전체")

        For ix As Integer = 0 To dt.Rows.Count - 1
            Dim strTmp As String = ""
            strTmp = "[" + dt.Rows(ix).Item("bcclscd").ToString + "] " + dt.Rows(ix).Item("bcclsnmd").ToString
            cboBccls.Items.Add(strTmp)
        Next
        cboBccls.DropDownStyle = ComboBoxStyle.DropDownList
        Me.cboBccls.SelectedIndex = 0

    End Sub

    Private Sub sbDisplay_Ward()

        Dim dt As New DataTable

        '-- 병동정보
        dt = OCSAPP.OcsLink.SData.fnGet_WardList()

        Me.cboWard.Items.Clear()
        Me.cboWard.Items.Add("전체" + Space(100) + "|")

        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                Me.cboWard.Items.Add(dt.Rows(i - 1).Item("wardnm").ToString + Space(100) + "|" + dt.Rows(i - 1).Item("wardno").ToString)
            Next
        End If

        Me.cboWard.SelectedIndex = 0

    End Sub

    Private Sub sbDisplay_Dept()

        Dim dt As New DataTable

        '-- 진료과 정보
        dt = OCSAPP.OcsLink.SData.fnGet_DeptList()
        cboDeptCd.Items.Clear()
        cboDeptCd.Items.Add("전체" + Space(100) + "|")

        If dt.Rows.Count > 0 Then
            For i As Integer = 1 To dt.Rows.Count
                Me.cboDeptCd.Items.Add(dt.Rows(i - 1).Item("deptnm").ToString + Space(100) + "|" + dt.Rows(i - 1).Item("deptcd").ToString)
            Next
        End If

        Me.cboDeptCd.SelectedIndex = 0
    End Sub

    ' Form초기화
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim objCommFn As New Fn
        Dim objComm As New ServerDateTime

        Try
            'Me.txtSearch.MaxLength = PRG_CONST.Len_RegNo
            Me.txtSearch.MaxLength = 15

            '-- 서버날짜로 설정
            Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))
            Me.dtpDate0.Value = CDate((New LISAPP.APP_DB.ServerDateTime).GetDate("-"))

            Me.btnToggle.Tag = "0" ' 검체번호 먼저 표시 20121214

            sbFormClear(0)

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            sbSpreadColHidden(True)

            ' 기본 바코드프린터 설정
            Me.lblBarPrinter.Text = (New PRTAPP.APP_BC.BCPrinter(Me.Name)).GetInfo.PRTNM

            ' 진료지원과인경우 등록번호 표시 
            If USER_INFO.USRLVL = "P" Then
                Me.txtSearch.Text = USER_INFO.N_REGNO
            ElseIf USER_INFO.USRLVL = "W" Then
                ' txtSearch.Text = USER_INFO.USRSECT
            End If

            If msRegNo <> "" Then Me.txtSearch.Text = msRegNo

            sbDisplay_Bccls()
            sbDisplay_Color_bccls() ''' 범례 
            sbDisplay_Ward()
            sbDisplay_Dept()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    ' 화면정리
    Private Sub sbFormClear(ByVal riPhase As Integer)
        Dim sFn As String = "Private Sub sbFormClear(ByVal aiPhase As Integer)"

        Try
            If InStr("0", riPhase.ToString, CompareMethod.Text) > 0 Then
                If USER_INFO.USRLVL <> "P" Then Me.txtSearch.Text = ""
                ntxtPrtCount.Text = "1"
            End If

            If InStr("01", riPhase.ToString, CompareMethod.Text) > 0 Then
                Me.spdList.MaxRows = 0
                Me.chk.Checked = False
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal rbFlag As Boolean)
        Dim sFn As String = "Private Sub sbSpreadColHidden(ByVal abFlag As Boolean)"

        Try
            With spdList
                .Col = .GetColFromID("bcprtno") : .ColHidden = rbFlag
                .Col = .GetColFromID("spcnmbp") : .ColHidden = rbFlag
                .Col = .GetColFromID("bcclscd") : .ColHidden = rbFlag
                .Col = .GetColFromID("iogbn") : .ColHidden = rbFlag
                .Col = .GetColFromID("tubenmbp") : .ColHidden = rbFlag
                .Col = .GetColFromID("tnmbp") : .ColHidden = rbFlag
                .Col = .GetColFromID("tgrpnmbp") : .ColHidden = rbFlag
                .Col = .GetColFromID("eryn") : .ColHidden = rbFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 채혈 리스트 조회
    Private Sub sbListView(ByVal r_dt As DataTable, Optional ByVal rbChkMode As Boolean = False)
        Dim sFn As String = "Private Sub fnListView(ByVal aoDTable As DataTable)"

        Try
            With spdList
                .ReDraw = False


                For ix As Integer = 0 To r_dt.Rows.Count - 1
                    Dim iRow As Integer = .SearchCol(.GetColFromID("bcno"), 0, .MaxRows, r_dt.Rows(ix).Item("bcno").ToString.Trim, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    If iRow > 0 Then Continue For

                    .MaxRows += 1
                    .Row = .MaxRows
                    .Col = .GetColFromID("status") : .Text = r_dt.Rows(ix).Item("status").ToString.Trim
                    .Col = .GetColFromID("orddt") : .Text = r_dt.Rows(ix).Item("orddt").ToString.Trim
                    .Col = .GetColFromID("bcno") : .Text = r_dt.Rows(ix).Item("bcno").ToString.Trim
                    .Col = .GetColFromID("regno") : .Text = r_dt.Rows(ix).Item("regno").ToString.Trim
                    .Col = .GetColFromID("patnm") : .Text = r_dt.Rows(ix).Item("patnm").ToString.Trim
                    .Col = .GetColFromID("sexage") : .Text = r_dt.Rows(ix).Item("sexage").ToString.Trim
                    .Col = .GetColFromID("deptward") : .Text = r_dt.Rows(ix).Item("deptward").ToString.Trim
                    .Col = .GetColFromID("doctornm") : .Text = r_dt.Rows(ix).Item("doctornm").ToString.Trim
                    .Col = .GetColFromID("spcnmd") : .Text = r_dt.Rows(ix).Item("spcnmd").ToString.Trim
                    .Col = .GetColFromID("tgrpnmbp") : .Text = r_dt.Rows(ix).Item("tgrpnmbp").ToString.Trim
                    .Col = .GetColFromID("bcprtno") : .Text = r_dt.Rows(ix).Item("bcprtno").ToString.Trim
                    .Col = .GetColFromID("bcclscd") : .Text = r_dt.Rows(ix).Item("bcclscd").ToString.Trim
                    .Col = .GetColFromID("iogbn") : .Text = r_dt.Rows(ix).Item("iogbn").ToString.Trim
                    .Col = .GetColFromID("tnmbp") : .Text = r_dt.Rows(ix).Item("tnmbp").ToString.Trim
                    .Col = .GetColFromID("spcnmbp") : .Text = r_dt.Rows(ix).Item("spcnmbp").ToString.Trim
                    .Col = .GetColFromID("tubenmbp") : .Text = r_dt.Rows(ix).Item("tubenmbp").ToString.Trim
                    .Col = .GetColFromID("testcd") : .Text = r_dt.Rows(ix).Item("testcd").ToString.Trim
                    .Col = .GetColFromID("tnmd") : .Text = r_dt.Rows(ix).Item("tnmd").ToString.Trim.Replace("'&apos;", "`")

                    If r_dt.Rows(ix).Item("colorgbn").ToString = "1" Then
                        .BackColor = Me.lblBcColor1.BackColor
                        .ForeColor = Me.lblBcclsNm1.ForeColor
                    ElseIf r_dt.Rows(ix).Item("colorgbn").ToString = "2" Then
                        .BackColor = Me.lblBcColor2.BackColor
                        .ForeColor = Me.lblBcclsNm2.ForeColor
                    ElseIf r_dt.Rows(ix).Item("colorgbn").ToString = "3" Then
                        .BackColor = Me.lblBcColor3.BackColor
                        .ForeColor = Me.lblBcclsNm3.ForeColor
                    End If

                    .Col = .GetColFromID("statgbn")
                    If r_dt.Rows(ix).Item("statgbn").ToString.Trim <> "" Then
                        .ForeColor = System.Drawing.Color.Red : .FontBold = True
                        .Text = "Y"
                        .set_RowHeight(.Row, 12.27)
                    Else
                        .Text = ""
                    End If

                    .Col = .GetColFromID("doctorrmk") : .Text = r_dt.Rows(ix).Item("doctorrmk").ToString.Trim
                    .Col = .GetColFromID("bccnt") : .Text = r_dt.Rows(ix).Item("bccnt").ToString.Trim

                    .Col = .GetColFromID("chk") : .Text = IIf(rbChkMode, "1", "").ToString

                    '<<JJH 자체응급 추가
                    .Col = .GetColFromID("eryn") : .Text = r_dt.Rows(ix).Item("eryn").ToString.Trim()
                Next

                .ReDraw = True
            End With


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub
    ' 바코드 조회시 팝업
    Private Sub sbAlertstate(ByVal r_dt As DataTable, ByVal rsbcno As String)
        Dim sFn As String = "Private Sub fnListView(ByVal aoDTable As DataTable)"

        Try
            With spdList
                .ReDraw = False
                Dim sState As String = ""
                For ix As Integer = 0 To .MaxRows - 1
                    Dim iRow As Integer = .SearchCol(.GetColFromID("bcno"), 0, .MaxRows, r_dt.Rows(0).Item("bcno").ToString.Trim, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)

                    .Row = iRow
                    .Col = .GetColFromID("status") : sState = .Text

                    If sState = "접수" Then
                        Exit Sub
                    Else
                        MsgBox("" + sState + " 상태의 검체입니다.")
                    End If

                Next

                .ReDraw = True
            End With


        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub


    ' 바코드 재출력
    Private Sub sbBCPrint()
        Dim sFn As String = "Private Sub fnBCPrint()"
        Dim aoBCPrt_Message As New PRTAPP.APP_BC.BCPrinter.clsAutoLabelerDF

        Try
            If Fn.SpdColSearch(spdList, "1", spdList.GetColFromID("chk")) = 0 Then
                MsgBox("재출력할 검체번호를 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If ntxtPrtCount.Text = "" Then
                MsgBox("바코드 출력장수를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            Else
                If CInt(ntxtPrtCount.Text) < 1 Then
                    MsgBox("바코드 출력장수가 1장 보다 작습니다.", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If
            End If

            Dim alBcData As New ArrayList
            Dim alBcNo_cult As New ArrayList

            With spdList
                For intRow As Integer = 1 To spdList.MaxRows
                    .Row = intRow : .Col = 1
                    If .Text = "1" Then
                        .Row = intRow
                        .Col = .GetColFromID("orddt") : Dim sOrdDt As String = .Text.Trim
                        .Col = .GetColFromID("bcno") : Dim sBcNo As String = .Text
                        .Col = .GetColFromID("bcprtno") : Dim sBcPrtNo As String = .Text.Trim
                        .Col = .GetColFromID("regno") : Dim sRegNo As String = .Text.Trim
                        .Col = .GetColFromID("patnm") : Dim sPatnm As String = .Text.Trim
                        .Col = .GetColFromID("sexage") : Dim sSexAge = .Text.Trim
                        .Col = .GetColFromID("bcclscd") : Dim sBcclsCd As String = .Text.Trim
                        .Col = .GetColFromID("deptward") : Dim sDeptWard = .Text.Trim
                        .Col = .GetColFromID("iogbn") : Dim sIoGbn As String = .Text.Trim
                        .Col = .GetColFromID("spcnmbp") : Dim sSpcNmbp As String = .Text.Trim
                        .Col = .GetColFromID("tubenmbp") : Dim sTubeNmbp As String = .Text.Trim
                        .Col = .GetColFromID("tnmbp") : Dim sTnmpb As String = .Text.Trim
                        .Col = .GetColFromID("statgbn") : Dim sStatGbn As String = .Text.Trim()
                        .Col = .GetColFromID("doctorrmk") : Dim sDoctorRmk As String = .Text.Trim()
                        .Col = .GetColFromID("tgrpnmbp") : Dim sTgrpNmbp As String = .Text.Trim
                        .Col = .GetColFromID("bccnt") : Dim sBcCnt As String = .Text.Trim
                        .Col = .GetColFromID("testcd") : Dim sTestcd As String = .Text.Trim() '20210429 jhs 검사코드 등록
                        .Col = .GetColFromID("eryn") : Dim sEryn As String = .Text.Trim()

                        Dim stu_bcdata As New STU_BCPRTINFO

                        With stu_bcdata
                            .BCNOPRT = sBcPrtNo
                            .REGNO = sRegNo
                            .PATNM = sPatnm
                            .SEXAGE = sSexAge
                            .BCCLSCD = sBcclsCd
                            .DEPTWARD = sDeptWard
                            .IOGBN = sIoGbn
                            .BCNO = sBcNo
                            .SPCNM = sSpcNmbp
                            .TUBENM = sTubeNmbp
                            .TESTNMS = sTnmpb
                            .TESTCD = sTestcd '20210429 jhs 검사코드 등록
                            '.EMER = sStatGbn
                            .EMER = IIf(sStatGbn = "Y", "E", "").ToString
                            .INFINFO = LISAPP.APP_C.Collfn.FindInfectionInfoD(.REGNO) '20140704 바코드 재출력시감염정보변경 
                            .TGRPNM = sTgrpNmbp.Replace(",", "")
                            .BCCNT = IIf(sBcCnt = "B", sBcCnt, Me.ntxtPrtCount.Text).ToString
                            .REMARK = sDoctorRmk

                            '<< JJH 자체응급
                            .ERPRTYN = sEryn

                            ' <--- 2019-04-19 혈액형여부 표시 (있을때 공란, 없을때 * 표시)
                            Dim ABOCHK As String = OCSAPP.OcsLink.SData.fnget_ABO(sRegNo)

                            .ABOCHK = ABOCHK
                            ' ---->

                        End With

                        alBcData.Add(stu_bcdata)

                        alBcNo_cult.Add(sBcNo.Replace("-", ""))

                    End If
                Next

                If alBcData.Count > 0 Then

                    If Me.chkBar_cult.Checked Then
                        '-- 배지바코드
                        Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)
                        '20210218 jhs 다중으로 뽑을 수 있도록 수정 
                        'objBCPrt.PrintDo_Micro(alBcNo_cult, "1")
                        objBCPrt.PrintDo_Micro(alBcNo_cult, Me.ntxtPrtCount.Text, Me.chkMultiBc.Checked)
                        '------------------------------------------


                Else
                    ' 바코드 출력
                    Call (New BCPrinter(Me.Name)).PrintDo(alBcData, False)
                End If

                End If

            End With

            MsgBox("정상적으로 출력 했습니다.", MsgBoxStyle.Information, Me.Text)

            'spdList.MaxRows = 0
            chk.Checked = False

            Me.txtSearch.Text = ""
            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    Public Function fnGet_Hangle_Font_3(ByVal rsValue As String) As String
        '한글(굴림(고딕))
        Try
            Dim btBuf As Byte() = System.Text.Encoding.Default.GetBytes(rsValue)

            Dim sFont As String = ""
            Dim ix As Integer = 0
            Dim iPos As Integer = 0

            Do While ix < btBuf.Length
                If btBuf(ix) > 128 Then
                    'sFont += Chr(27) + "K2B" + Chr(btBuf(ix) - 128) + Chr(btBuf(ix + 1) - 128)
                    'sFont += Chr(27) + "PR" + Chr(27) + "RF010002," + rsValue.Substring(iPos, 1)
                    sFont += Chr(27) + "PR" + Chr(27) + "RF020002," + rsValue.Substring(iPos, 1)
                    ix += 2
                Else
                    sFont += Chr(27) + "PS" + Chr(27) + "RF020002," + "0" + rsValue.Substring(iPos, 1)
                    ix += 1
                End If
                iPos += 1
            Loop

            Return sFont

        Catch ex As Exception
            Return ""
            MsgBox(ex.Message)
        End Try
    End Function

    ' 바코드 재출력 테스트
    Private Sub sbBCPrintTest()
        Dim sFn As String = "Private Sub fnBCPrint()"
        Dim aoBCPrt_Message As New PRTAPP.APP_BC.BCPrinter.clsAutoLabelerDF

        Try
           

            Dim alBcData As New ArrayList
            Dim alBcNo_cult As New ArrayList

            Dim sOrdDt As String = "999999999999999"  '15
            Dim sBcNo As String = "BCBCBCBCBCBCBCBCBC" '16
            Dim sBcPrtNo As String = "1234567891011121" '15
            Dim sRegNo As String = "REGNO" '15
            Dim sPatnm As String = "환자명환" '9 (한)
            Dim sSexAge = "sex/age"
            Dim sBcclsCd As String = "PT"
            Dim sDeptWard = "DEPT"
            Dim sIoGbn As String = "IO"
            Dim sSpcNmbp As String = "SPCSPCSPCS" '10 ''''''
            Dim sTubeNmbp As String = "TUBETUBET" '9 ''''''
            Dim sTnmpb As String = "TNMDTNMDTNMTNMDTNMDTNMD" '24 ''''''
            Dim sStatGbn As String = "ER" '15
            Dim sDoctorRmk As String = "DRRMK" '15
            Dim sTgrpNmbp As String = "TG" '12
            Dim sBcCnt As String = "BCCNT" '8

            Dim stu_bcdata As New STU_BCPRTINFO

            With stu_bcdata
                .BCNOPRT = sBcPrtNo
                .REGNO = sRegNo
                .PATNM = sPatnm
                .SEXAGE = sSexAge
                .BCCLSCD = sBcclsCd
                .DEPTWARD = sDeptWard
                .IOGBN = sIoGbn
                .BCNO = sBcNo
                .SPCNM = sSpcNmbp
                .TUBENM = sTubeNmbp
                .TESTNMS = sTnmpb
                .EMER = sStatGbn
                .INFINFO = LISAPP.APP_C.Collfn.FindInfectionInfoD(.REGNO) '20140704 바코드 재출력시감염정보변경 
                .TGRPNM = sTgrpNmbp.Replace(",", "")
                .BCCNT = IIf(sBcCnt = "B", sBcCnt, Me.ntxtPrtCount.Text).ToString
                .REMARK = sDoctorRmk
            End With

            alBcData.Add(stu_bcdata)

            alBcNo_cult.Add(sBcNo.Replace("-", ""))

              

            If alBcData.Count > 0 Then

                If Me.chkBar_cult.Checked Then
                    '-- 배지바코드
                    Dim objBCPrt As New PRTAPP.APP_BC.BCPrinter(Me.Name)
                    objBCPrt.PrintDo_Micro(alBcNo_cult, "1")

                Else
                    ' 바코드 출력
                    Call (New BCPrinter(Me.Name)).PrintDo(alBcData, False)
                End If

            End If



            MsgBox("정상적으로 출력 했습니다.", MsgBoxStyle.Information, Me.Text)

            'spdList.MaxRows = 0
            chk.Checked = False

            Me.txtSearch.Text = ""
            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

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
                txtSearch.Text = USER_INFO.N_REGNO
            End If
        End If
    End Sub

    Private Sub txtSearch_CursorChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.CursorChanged

    End Sub

    Private Sub txtSearch_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.GotFocus
        txtSearch.SelectAll()
    End Sub

    Private Sub ntxtPrtCount_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ntxtPrtCount.SelectAll()
    End Sub

    Private Sub dtpDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpDate0.KeyPress, dtpDate1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click
        Dim sFn As String = "Private Sub btnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuery.Click"

        Try
            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "")

            Me.spdList.MaxRows = 0
            chk.Checked = False

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor()

            sbSearch()

            Me.txtSearch.Focus()
            Me.txtSearch.SelectAll()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        Finally
            Cursor.Current = System.Windows.Forms.Cursors.Default

        End Try
    End Sub

    Private Sub sbSearch(Optional ByVal rbChk As Boolean = False)
        Dim sFn As String = "sbSearch"
        Try

            Dim sBcNo As String = ""
            Dim sRegNo As String = ""

            If Me.lblSearch.Text = "검체번호" And Me.txtSearch.Text <> "" Then
                Dim sTmp As String = ""

                '검체번호 선택시 처리내용
                If Me.txtSearch.Text.Length.Equals(11) Then
                    ' 바코드에서 직접 입력시

                    ' 바코드번호(검체번호)를 표시형 검체번호로 변경
                    Dim objCommDBFN As New LISAPP.APP_DB.DbFn
                    sTmp = objCommDBFN.GetBCPrtToView(txtSearch.Text.Trim)  '.Substring(0, 14)

                    If sTmp = "" Then
                        MsgBox("잘못된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                        Me.txtSearch.Focus()
                        Return
                    Else
                        Me.txtSearch.Text = sTmp
                    End If

                ElseIf txtSearch.Text.Length.Equals(14) Then
                    ' 복수구분 없이 입력

                ElseIf txtSearch.Text.Length.Equals(15) Then
                    ' 복수구분 입력시
                    'txtSearch.Text = txtSearch.Text.Substring(0, 14)

                Else
                    MsgBox("잘못된 검체번호 입니다.", MsgBoxStyle.Information, Me.Text)
                    Me.txtSearch.Text = ""
                    Return
                End If

                sBcNo = Me.txtSearch.Text

            ElseIf Me.txtSearch.Text <> "" Then

                If IsNumeric(txtSearch.Text.Substring(0, 1)) Then
                    Me.txtSearch.Text = txtSearch.Text.PadLeft(PRG_CONST.Len_RegNo, "0"c)
                Else
                    Me.txtSearch.Text = txtSearch.Text.Substring(0, 1).ToUpper + txtSearch.Text.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
                End If
                sRegNo = txtSearch.Text

                Me.spdList.MaxRows = 0
                chk.Checked = False
            End If

            Dim sWardNo As String = ""
            Dim sDeptCd As String = ""

            If Me.cboWard.Text.IndexOf("|") >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(1)
            If Me.cboDeptCd.Text.IndexOf("|") >= 0 Then sDeptCd = Me.cboDeptCd.Text.Split("|"c)(1)

            Dim dt As DataTable = FGJ03_ListView(Me.dtpDate0.Text.Replace("-", ""), Me.dtpDate1.Text.Replace("-", ""), sRegNo, sBcNo, _
                                                 Ctrl.Get_Code(Me.cboBccls), sDeptCd, sWardNo, "")

            If dt.Rows.Count > 0 Then
                sbListView(dt, rbChk)
                'If dt.Rows.Count > 1 Then '검체번호 리딩시 팝업기능
                'Else
                '    sbAlertstate(dt, sBcNo)
                'End If
            Else
                MsgBox("채혈일자 구간에 해당하는 데이타가 없습니다.")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub spdList_TextTipFetch(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_TextTipFetchEvent) Handles spdList.TextTipFetch
        Dim objSpd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)
        Dim strTText As String = ""

        If e.row < 1 Then Exit Sub
        With objSpd
            .SetTextTipAppearance("굴림체", 9, False, False, &HDFFFFF&, &H800000)
            e.multiLine = FPSpreadADO.TextTipFetchMultilineConstants.TextTipFetchMultilineMultiple : e.showTip = True

            Select Case e.col
                Case .GetColFromID("처방일시")
                    .Row = e.row
                    .Col = .GetColFromID("처방일시") : strTText = vbCrLf & " " & CDate(.Text.ToString).ToShortDateString & " " & vbCrLf
                Case .GetColFromID("검사항목")
                    .Row = e.row
                    .Col = .GetColFromID("검사항목") : strTText = vbCrLf & " " & .Text.ToString & " " & vbCrLf
            End Select

            e.tipWidth = Fn.GetToolTipWidth(Me.CreateGraphics, strTText, .Font)
            e.tipText = strTText
        End With
    End Sub

    Private Sub btnSelBCPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click
        Dim sFn As String = "Private Sub btnSelBCPRT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelBCPRT.Click"
        Dim objFrm As New POPUPPRT.FGPOUP_PRTBC("FGJ03", Me.chkBarInit.Checked)

        Try
            objFrm.ShowDialog()
            Me.lblBarPrinter.Text = objFrm.mPrinterName

            objFrm.Dispose()
            objFrm = Nothing

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Sub

    Private Sub spdList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdList.ClickEvent
        If e.row < 1 Then Exit Sub
        With spdList
            'If e.col <> 1 Then
            .Row = e.row : .Col = .GetColFromID("chk")
            If .Text.Trim = "" Then .Text = "1" Else .Text = ""
            'End If
        End With
    End Sub
#End Region

    Private Sub txtSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        Me.txtSearch.SelectAll()
    End Sub

    Private Sub cboWard_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWard.SelectedIndexChanged
        Me.cboSR.Items.Clear()

        Me.spdList.MaxRows = 0
        chk.Checked = False

        Me.cboSR.Items.Add("")
        Dim sWardNo As String = ""

        If Me.cboWard.Text.IndexOf("|"c) >= 0 Then sWardNo = Me.cboWard.Text.Split("|"c)(1)

        Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_RoomList(sWardNo)

        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                With dt.Rows(i)
                    Me.cboSR.Items.Add(.Item("roomno").ToString)
                End With
            Next
        End If

        Me.cboSR.SelectedIndex = 0
    End Sub

    Private Sub chk_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk.CheckedChanged
        Dim sFn As String = "chk_CheckedChanged"

        Try
            With spdList
                .Col = .GetColFromID("chk")

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    If chk.Checked Then
                        .Text = "1"
                    Else
                        .Text = ""
                    End If
                Next
            End With
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkMoveColl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMoveColl.Click
        Dim sFn As String = ""

        Try
            Me.spdList.AllowColMove = chkMoveColl.Checked
        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGJ03_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If msRegNo <> "" Then
            Me.txtSearch.Text = msRegNo
        End If

        If USER_INFO.N_IOGBN = "WARD" And USER_INFO.N_WARDorDEPT <> "" Then
            For ix As Integer = 0 To cboWard.Items.Count - 1

                Me.cboWard.SelectedIndex = ix
                If Me.cboWard.Text.IndexOf("|" + USER_INFO.N_WARDorDEPT) >= 0 Then
                    Exit For
                End If
            Next
        End If

        If USER_INFO.USRLVL = "O" And USER_INFO.N_WARDorDEPT <> "" Then
            For ix As Integer = 0 To cboDeptCd.Items.Count - 1

                Me.cboDeptCd.SelectedIndex = ix
                If Me.cboDeptCd.Text.IndexOf("|" + USER_INFO.N_WARDorDEPT) >= 0 Then
                    Exit For
                End If
            Next

        End If

    End Sub

    Private Sub sbDisplay_Color_bccls()
        Dim sFn As String = "Private Sub sbGet_Data_LisCmt"
        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_bccls_color
            If dt.Rows.Count > 0 Then
                For ix As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(ix).Item("colorgbn").ToString
                        Case "1"
                            lblBcclsNm1.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor1.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor1.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "2"
                            lblBcclsNm2.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor2.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor2.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                        Case "3"
                            lblBcclsNm3.Text = dt.Rows(ix).Item("bcclsnmd").ToString

                            lblBcColor3.BackColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                            lblBcColor3.ForeColor = COLOR_BCCLSCD.BkColor(dt.Rows(ix).Item("colorgbn").ToString)
                    End Select
                Next
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
        End Try

    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        Dim sFn As String = "Private Sub txtSearch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress"
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Me.chk.Checked = True

            Me.txtSearch.Text = Me.txtSearch.Text.Replace("-", "").Trim()
            Call sbSearch(True)

            Me.txtSearch.SelectAll()
            Me.txtSearch.Focus()

        Catch ex As Exception
            CDHELP.FGCDHELPFN.fn_PopMsg(Me, "E"c, ex.Message)

        End Try
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        'sbBCPrintTest()

    End Sub

    Private Sub chkMultiBc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMultiBc.CheckedChanged
        If Me.chkBar_cult.Checked Then
        Else
            Me.chkMultiBc.Checked = False
        End If
    End Sub

    Private Sub chkBar_cult_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBar_cult.CheckedChanged
        If Me.chkBar_cult.Checked = False Then
            Me.chkMultiBc.Checked = False
        Else
        End If
    End Sub
End Class
