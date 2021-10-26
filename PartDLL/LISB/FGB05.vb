'>>> ���� �԰�

Imports System.Windows.Forms
Imports System.Drawing
Imports System.IO

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports COMMON.CommLogin.LOGIN

Imports LISAPP.APP_DB
Imports LISAPP.APP_BT

Imports CDHELP.FGCDHELPFN


Public Class FGB05
    Inherits System.Windows.Forms.Form

    Private mobjDAF As New LISAPP.APP_F_COMCD
    Dim msABOType As String = ""
    Dim msInDate As String = ""  ' �԰�ð��� �����ϰ� ����
    Dim msNowTime As String = ""
    Dim NoRst_Flag As String = "" ' �˻���� �����ε��� ���� �԰� ��Ű���� �Ҷ��� ��Ÿ���� ���� flag
    Dim m_al_ComCd As New ArrayList ' spdComList �� ���� �ѷ��ֱ� ���� �������� ���� �������� �ڵ带 �������� ����Ʈ
    Dim User_Id As String = USER_INFO.USRID
    Dim mi_MaxRow As Integer = 0
    Dim mi_InCnt As Integer = 0
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnBldIn As CButtonLib.CButton
    Friend WithEvents cboComCd As System.Windows.Forms.ComboBox
    Friend WithEvents lblComcd As System.Windows.Forms.Label
    Friend WithEvents cboTimeE As System.Windows.Forms.ComboBox
    Friend WithEvents cboTimeS As System.Windows.Forms.ComboBox
    Friend WithEvents btnCSV As CButtonLib.CButton

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    '-- �� �ȿö�
    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.

        Me.cboInPlace.Items.Add(PRG_CONST.HOSPITAL_NAME + " ��������")

        sbForm_Clear("0")

    End Sub

    'Form�� Dispose�� �������Ͽ� ���� ��� ����� �����մϴ�.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form �����̳ʿ� �ʿ��մϴ�.
    Private components As System.ComponentModel.IContainer

    '����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
    'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.  
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents rdoBldQnt0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBldQnt1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBld0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBld1 As System.Windows.Forms.RadioButton
    Friend WithEvents lblBType As System.Windows.Forms.Label
    Friend WithEvents txtBldQnt As System.Windows.Forms.TextBox
    Friend WithEvents txtBType As System.Windows.Forms.TextBox
    Friend WithEvents cboInPlace As System.Windows.Forms.ComboBox
    Friend WithEvents spdComList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents cboRH As System.Windows.Forms.ComboBox
    Friend WithEvents cboBType As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtBldNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents spdBldInList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblDateGbn As System.Windows.Forms.Label
    Friend WithEvents dtpDateE As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents dtpDateS As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents spdPastList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpDonDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents rdoAuto0 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAuto1 As System.Windows.Forms.RadioButton
    Friend WithEvents lblNewGbn As System.Windows.Forms.Label
    Friend WithEvents rdoBld2 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBld3 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBld4 As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPatnm As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB05))
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtComment = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.spdComList = New AxFPSpreadADO.AxfpSpread()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblNewGbn = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.rdoAuto0 = New System.Windows.Forms.RadioButton()
        Me.rdoAuto1 = New System.Windows.Forms.RadioButton()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboRH = New System.Windows.Forms.ComboBox()
        Me.cboBType = New System.Windows.Forms.ComboBox()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.rdoBldQnt0 = New System.Windows.Forms.RadioButton()
        Me.rdoBldQnt1 = New System.Windows.Forms.RadioButton()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.rdoBld4 = New System.Windows.Forms.RadioButton()
        Me.rdoBld3 = New System.Windows.Forms.RadioButton()
        Me.rdoBld2 = New System.Windows.Forms.RadioButton()
        Me.rdoBld0 = New System.Windows.Forms.RadioButton()
        Me.rdoBld1 = New System.Windows.Forms.RadioButton()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.lblBType = New System.Windows.Forms.Label()
        Me.txtBldQnt = New System.Windows.Forms.TextBox()
        Me.txtBType = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboInPlace = New System.Windows.Forms.ComboBox()
        Me.txtBldNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.spdBldInList = New AxFPSpreadADO.AxfpSpread()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cboTimeE = New System.Windows.Forms.ComboBox()
        Me.cboTimeS = New System.Windows.Forms.ComboBox()
        Me.cboComCd = New System.Windows.Forms.ComboBox()
        Me.lblComcd = New System.Windows.Forms.Label()
        Me.btnExcel = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDateGbn = New System.Windows.Forms.Label()
        Me.dtpDateE = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.dtpDateS = New System.Windows.Forms.DateTimePicker()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.spdPastList = New AxFPSpreadADO.AxfpSpread()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpDonDt = New System.Windows.Forms.DateTimePicker()
        Me.dtpInDt = New System.Windows.Forms.DateTimePicker()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCSV = New CButtonLib.CButton()
        Me.btnBldIn = New CButtonLib.CButton()
        Me.btnClear = New CButtonLib.CButton()
        Me.btnExit = New CButtonLib.CButton()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.txtPatnm = New System.Windows.Forms.TextBox()
        Me.txtRegNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.spdBldInList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.spdPastList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.txtComment)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 288)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(380, 550)
        Me.GroupBox1.TabIndex = 97
        Me.GroupBox1.TabStop = False
        '
        'txtComment
        '
        Me.txtComment.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComment.Location = New System.Drawing.Point(8, 511)
        Me.txtComment.MaxLength = 100
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(364, 30)
        Me.txtComment.TabIndex = 19
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Location = New System.Drawing.Point(8, 487)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 24)
        Me.Label11.TabIndex = 106
        Me.Label11.Text = "���׺��"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.spdComList)
        Me.Panel2.Location = New System.Drawing.Point(8, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(364, 470)
        Me.Panel2.TabIndex = 100
        '
        'spdComList
        '
        Me.spdComList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.spdComList.DataSource = Nothing
        Me.spdComList.Location = New System.Drawing.Point(0, 0)
        Me.spdComList.Name = "spdComList"
        Me.spdComList.OcxState = CType(resources.GetObject("spdComList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdComList.Size = New System.Drawing.Size(360, 466)
        Me.spdComList.TabIndex = 18
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblNewGbn)
        Me.GroupBox2.Controls.Add(Me.Panel5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cboRH)
        Me.GroupBox2.Controls.Add(Me.cboBType)
        Me.GroupBox2.Controls.Add(Me.Panel7)
        Me.GroupBox2.Controls.Add(Me.Panel6)
        Me.GroupBox2.Controls.Add(Me.lblBType)
        Me.GroupBox2.Controls.Add(Me.txtBldQnt)
        Me.GroupBox2.Controls.Add(Me.txtBType)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cboInPlace)
        Me.GroupBox2.Controls.Add(Me.txtBldNo)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(380, 192)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'lblNewGbn
        '
        Me.lblNewGbn.BackColor = System.Drawing.Color.Black
        Me.lblNewGbn.ForeColor = System.Drawing.Color.White
        Me.lblNewGbn.Location = New System.Drawing.Point(273, 77)
        Me.lblNewGbn.Name = "lblNewGbn"
        Me.lblNewGbn.Size = New System.Drawing.Size(72, 23)
        Me.lblNewGbn.TabIndex = 111
        Me.lblNewGbn.Text = "lblNewGbn"
        Me.lblNewGbn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblNewGbn.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel5.Controls.Add(Me.rdoAuto0)
        Me.Panel5.Controls.Add(Me.rdoAuto1)
        Me.Panel5.Controls.Add(Me.Label10)
        Me.Panel5.ForeColor = System.Drawing.Color.Indigo
        Me.Panel5.Location = New System.Drawing.Point(154, 44)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(76, 52)
        Me.Panel5.TabIndex = 110
        '
        'rdoAuto0
        '
        Me.rdoAuto0.Checked = True
        Me.rdoAuto0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAuto0.Location = New System.Drawing.Point(3, 4)
        Me.rdoAuto0.Name = "rdoAuto0"
        Me.rdoAuto0.Size = New System.Drawing.Size(77, 20)
        Me.rdoAuto0.TabIndex = 2
        Me.rdoAuto0.TabStop = True
        Me.rdoAuto0.Tag = "0"
        Me.rdoAuto0.Text = "����(F11)"
        '
        'rdoAuto1
        '
        Me.rdoAuto1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoAuto1.Location = New System.Drawing.Point(3, 28)
        Me.rdoAuto1.Name = "rdoAuto1"
        Me.rdoAuto1.Size = New System.Drawing.Size(80, 20)
        Me.rdoAuto1.TabIndex = 3
        Me.rdoAuto1.Tag = "1"
        Me.rdoAuto1.Text = "�ϰ�(F12)"
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label10.Location = New System.Drawing.Point(0, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 52)
        Me.Label10.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(285, 140)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(28, 16)
        Me.Label6.TabIndex = 109
        Me.Label6.Text = "RH:"
        '
        'cboRH
        '
        Me.cboRH.Font = New System.Drawing.Font("����", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboRH.Items.AddRange(New Object() {"+", "-"})
        Me.cboRH.Location = New System.Drawing.Point(317, 136)
        Me.cboRH.Name = "cboRH"
        Me.cboRH.Size = New System.Drawing.Size(48, 23)
        Me.cboRH.TabIndex = 11
        Me.cboRH.Text = "+"
        '
        'cboBType
        '
        Me.cboBType.Font = New System.Drawing.Font("����", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboBType.Items.AddRange(New Object() {"A", "B", "O", "AB"})
        Me.cboBType.Location = New System.Drawing.Point(217, 136)
        Me.cboBType.Name = "cboBType"
        Me.cboBType.Size = New System.Drawing.Size(60, 23)
        Me.cboBType.TabIndex = 10
        Me.cboBType.Text = "A"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.SeaShell
        Me.Panel7.Controls.Add(Me.rdoBldQnt0)
        Me.Panel7.Controls.Add(Me.rdoBldQnt1)
        Me.Panel7.Controls.Add(Me.Label17)
        Me.Panel7.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.Panel7.Location = New System.Drawing.Point(216, 164)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(148, 24)
        Me.Panel7.TabIndex = 99
        '
        'rdoBldQnt0
        '
        Me.rdoBldQnt0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBldQnt0.Location = New System.Drawing.Point(8, 3)
        Me.rdoBldQnt0.Name = "rdoBldQnt0"
        Me.rdoBldQnt0.Size = New System.Drawing.Size(56, 20)
        Me.rdoBldQnt0.TabIndex = 13
        Me.rdoBldQnt0.Tag = "0"
        Me.rdoBldQnt0.Text = "400��"
        '
        'rdoBldQnt1
        '
        Me.rdoBldQnt1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBldQnt1.Location = New System.Drawing.Point(84, 3)
        Me.rdoBldQnt1.Name = "rdoBldQnt1"
        Me.rdoBldQnt1.Size = New System.Drawing.Size(56, 20)
        Me.rdoBldQnt1.TabIndex = 14
        Me.rdoBldQnt1.Tag = "1"
        Me.rdoBldQnt1.Text = "320��"
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label17.Location = New System.Drawing.Point(0, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(148, 24)
        Me.Label17.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Lavender
        Me.Panel6.Controls.Add(Me.rdoBld4)
        Me.Panel6.Controls.Add(Me.rdoBld3)
        Me.Panel6.Controls.Add(Me.rdoBld2)
        Me.Panel6.Controls.Add(Me.rdoBld0)
        Me.Panel6.Controls.Add(Me.rdoBld1)
        Me.Panel6.Controls.Add(Me.Label63)
        Me.Panel6.ForeColor = System.Drawing.Color.Navy
        Me.Panel6.Location = New System.Drawing.Point(88, 104)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(288, 24)
        Me.Panel6.TabIndex = 97
        '
        'rdoBld4
        '
        Me.rdoBld4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBld4.Location = New System.Drawing.Point(238, 3)
        Me.rdoBld4.Name = "rdoBld4"
        Me.rdoBld4.Size = New System.Drawing.Size(48, 20)
        Me.rdoBld4.TabIndex = 8
        Me.rdoBld4.Tag = "4"
        Me.rdoBld4.Text = "�ڰ�"
        '
        'rdoBld3
        '
        Me.rdoBld3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBld3.Location = New System.Drawing.Point(186, 3)
        Me.rdoBld3.Name = "rdoBld3"
        Me.rdoBld3.Size = New System.Drawing.Size(48, 20)
        Me.rdoBld3.TabIndex = 7
        Me.rdoBld3.Tag = "3"
        Me.rdoBld3.Text = "����"
        '
        'rdoBld2
        '
        Me.rdoBld2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBld2.Location = New System.Drawing.Point(132, 3)
        Me.rdoBld2.Name = "rdoBld2"
        Me.rdoBld2.Size = New System.Drawing.Size(48, 20)
        Me.rdoBld2.TabIndex = 6
        Me.rdoBld2.Tag = "2"
        Me.rdoBld2.Text = "����"
        '
        'rdoBld0
        '
        Me.rdoBld0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBld0.Location = New System.Drawing.Point(9, 3)
        Me.rdoBld0.Name = "rdoBld0"
        Me.rdoBld0.Size = New System.Drawing.Size(60, 20)
        Me.rdoBld0.TabIndex = 4
        Me.rdoBld0.Tag = "0"
        Me.rdoBld0.Text = "���׿�"
        '
        'rdoBld1
        '
        Me.rdoBld1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoBld1.Location = New System.Drawing.Point(75, 3)
        Me.rdoBld1.Name = "rdoBld1"
        Me.rdoBld1.Size = New System.Drawing.Size(48, 20)
        Me.rdoBld1.TabIndex = 5
        Me.rdoBld1.Tag = "1"
        Me.rdoBld1.Text = "����"
        '
        'Label63
        '
        Me.Label63.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label63.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label63.Location = New System.Drawing.Point(0, 0)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(288, 24)
        Me.Label63.TabIndex = 0
        '
        'lblBType
        '
        Me.lblBType.BackColor = System.Drawing.Color.White
        Me.lblBType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBType.Font = New System.Drawing.Font("Arial Black", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBType.ForeColor = System.Drawing.Color.Crimson
        Me.lblBType.Location = New System.Drawing.Point(234, 12)
        Me.lblBType.Name = "lblBType"
        Me.lblBType.Size = New System.Drawing.Size(139, 84)
        Me.lblBType.TabIndex = 52
        Me.lblBType.Text = "AB+"
        Me.lblBType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBldQnt
        '
        Me.txtBldQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldQnt.Location = New System.Drawing.Point(88, 164)
        Me.txtBldQnt.MaxLength = 10
        Me.txtBldQnt.Name = "txtBldQnt"
        Me.txtBldQnt.Size = New System.Drawing.Size(124, 21)
        Me.txtBldQnt.TabIndex = 12
        '
        'txtBType
        '
        Me.txtBType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBType.Location = New System.Drawing.Point(88, 136)
        Me.txtBType.MaxLength = 10
        Me.txtBType.Name = "txtBType"
        Me.txtBType.Size = New System.Drawing.Size(124, 21)
        Me.txtBType.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(4, 164)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 21)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "��������"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(4, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 22)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "������"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.Location = New System.Drawing.Point(4, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 24)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "����"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(4, 44)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 24)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "���׹�ȣ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboInPlace
        '
        Me.cboInPlace.Font = New System.Drawing.Font("����", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboInPlace.Location = New System.Drawing.Point(72, 14)
        Me.cboInPlace.Name = "cboInPlace"
        Me.cboInPlace.Size = New System.Drawing.Size(159, 21)
        Me.cboInPlace.TabIndex = 0
        Me.cboInPlace.TabStop = False
        Me.cboInPlace.Tag = "0"
        Me.cboInPlace.Text = "��õ����õ���� ��������"
        '
        'txtBldNo
        '
        Me.txtBldNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBldNo.Location = New System.Drawing.Point(4, 72)
        Me.txtBldNo.MaxLength = 10
        Me.txtBldNo.Name = "txtBldNo"
        Me.txtBldNo.Size = New System.Drawing.Size(146, 21)
        Me.txtBldNo.TabIndex = 1
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label21.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label21.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label21.Location = New System.Drawing.Point(4, 13)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(67, 22)
        Me.Label21.TabIndex = 5
        Me.Label21.Text = "�԰����"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.Beige
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.Font = New System.Drawing.Font("����", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Green
        Me.Label12.Location = New System.Drawing.Point(4, 8)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(870, 24)
        Me.Label12.TabIndex = 103
        Me.Label12.Text = "�԰� ����Ʈ"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdBldInList)
        Me.Panel1.Location = New System.Drawing.Point(4, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(871, 232)
        Me.Panel1.TabIndex = 137
        '
        'spdBldInList
        '
        Me.spdBldInList.DataSource = Nothing
        Me.spdBldInList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdBldInList.Location = New System.Drawing.Point(0, 0)
        Me.spdBldInList.Name = "spdBldInList"
        Me.spdBldInList.OcxState = CType(resources.GetObject("spdBldInList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdBldInList.Size = New System.Drawing.Size(867, 228)
        Me.spdBldInList.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.cboTimeE)
        Me.GroupBox3.Controls.Add(Me.cboTimeS)
        Me.GroupBox3.Controls.Add(Me.cboComCd)
        Me.GroupBox3.Controls.Add(Me.lblComcd)
        Me.GroupBox3.Controls.Add(Me.btnExcel)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.lblDateGbn)
        Me.GroupBox3.Controls.Add(Me.dtpDateE)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.btnSearch)
        Me.GroupBox3.Controls.Add(Me.dtpDateS)
        Me.GroupBox3.Controls.Add(Me.Panel4)
        Me.GroupBox3.Location = New System.Drawing.Point(390, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(878, 566)
        Me.GroupBox3.TabIndex = 138
        Me.GroupBox3.TabStop = False
        '
        'cboTimeE
        '
        Me.cboTimeE.FormattingEnabled = True
        Me.cboTimeE.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.cboTimeE.Location = New System.Drawing.Point(324, 17)
        Me.cboTimeE.Name = "cboTimeE"
        Me.cboTimeE.Size = New System.Drawing.Size(41, 20)
        Me.cboTimeE.TabIndex = 204
        '
        'cboTimeS
        '
        Me.cboTimeS.FormattingEnabled = True
        Me.cboTimeS.Items.AddRange(New Object() {"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23"})
        Me.cboTimeS.Location = New System.Drawing.Point(179, 17)
        Me.cboTimeS.Name = "cboTimeS"
        Me.cboTimeS.Size = New System.Drawing.Size(41, 20)
        Me.cboTimeS.TabIndex = 203
        '
        'cboComCd
        '
        Me.cboComCd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComCd.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.cboComCd.FormattingEnabled = True
        Me.cboComCd.Location = New System.Drawing.Point(430, 17)
        Me.cboComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.cboComCd.MaxDropDownItems = 20
        Me.cboComCd.Name = "cboComCd"
        Me.cboComCd.Size = New System.Drawing.Size(224, 20)
        Me.cboComCd.TabIndex = 199
        '
        'lblComcd
        '
        Me.lblComcd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblComcd.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComcd.ForeColor = System.Drawing.Color.Black
        Me.lblComcd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblComcd.Location = New System.Drawing.Point(366, 17)
        Me.lblComcd.Margin = New System.Windows.Forms.Padding(1)
        Me.lblComcd.Name = "lblComcd"
        Me.lblComcd.Size = New System.Drawing.Size(63, 21)
        Me.lblComcd.TabIndex = 200
        Me.lblComcd.Text = "��������"
        Me.lblComcd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnExcel
        '
        Me.btnExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExcel.Location = New System.Drawing.Point(707, 15)
        Me.btnExcel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(80, 24)
        Me.btnExcel.TabIndex = 155
        Me.btnExcel.Text = "To Excel"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Location = New System.Drawing.Point(791, 15)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(1)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(80, 24)
        Me.btnDelete.TabIndex = 154
        Me.btnDelete.Text = "�� ��"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label9.Font = New System.Drawing.Font("����", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Green
        Me.Label9.Location = New System.Drawing.Point(4, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(870, 26)
        Me.Label9.TabIndex = 153
        Me.Label9.Text = "���� �԰� ����Ʈ ��ȸ"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDateGbn
        '
        Me.lblDateGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblDateGbn.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblDateGbn.ForeColor = System.Drawing.Color.White
        Me.lblDateGbn.Location = New System.Drawing.Point(6, 17)
        Me.lblDateGbn.Name = "lblDateGbn"
        Me.lblDateGbn.Size = New System.Drawing.Size(83, 21)
        Me.lblDateGbn.TabIndex = 147
        Me.lblDateGbn.Text = "�԰�����"
        Me.lblDateGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDateE
        '
        Me.dtpDateE.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateE.Location = New System.Drawing.Point(235, 17)
        Me.dtpDateE.Name = "dtpDateE"
        Me.dtpDateE.Size = New System.Drawing.Size(88, 21)
        Me.dtpDateE.TabIndex = 150
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(223, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(11, 12)
        Me.Label8.TabIndex = 151
        Me.Label8.Text = "~"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(656, 15)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(1)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(80, 24)
        Me.btnSearch.TabIndex = 152
        Me.btnSearch.Text = "��ȸ"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'dtpDateS
        '
        Me.dtpDateS.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpDateS.Location = New System.Drawing.Point(90, 17)
        Me.dtpDateS.Name = "dtpDateS"
        Me.dtpDateS.Size = New System.Drawing.Size(87, 21)
        Me.dtpDateS.TabIndex = 148
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.spdPastList)
        Me.Panel4.Location = New System.Drawing.Point(4, 72)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(872, 490)
        Me.Panel4.TabIndex = 146
        '
        'spdPastList
        '
        Me.spdPastList.DataSource = Nothing
        Me.spdPastList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdPastList.Location = New System.Drawing.Point(0, 0)
        Me.spdPastList.Name = "spdPastList"
        Me.spdPastList.OcxState = CType(resources.GetObject("spdPastList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdPastList.Size = New System.Drawing.Size(868, 486)
        Me.spdPastList.TabIndex = 139
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(4, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 22)
        Me.Label5.TabIndex = 142
        Me.Label5.Text = "�԰�����"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(4, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 22)
        Me.Label7.TabIndex = 141
        Me.Label7.Text = "��������"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpDonDt
        '
        Me.dtpDonDt.CustomFormat = "yyyy-MM-dd HH:mm"
        Me.dtpDonDt.Font = New System.Drawing.Font("����", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpDonDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDonDt.Location = New System.Drawing.Point(88, 38)
        Me.dtpDonDt.Name = "dtpDonDt"
        Me.dtpDonDt.Size = New System.Drawing.Size(184, 22)
        Me.dtpDonDt.TabIndex = 16
        '
        'dtpInDt
        '
        Me.dtpInDt.Font = New System.Drawing.Font("����", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.dtpInDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInDt.Location = New System.Drawing.Point(88, 11)
        Me.dtpInDt.Name = "dtpInDt"
        Me.dtpInDt.Size = New System.Drawing.Size(184, 22)
        Me.dtpInDt.TabIndex = 15
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.btnCSV)
        Me.Panel3.Controls.Add(Me.btnBldIn)
        Me.Panel3.Controls.Add(Me.btnClear)
        Me.Panel3.Controls.Add(Me.btnExit)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 841)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1272, 34)
        Me.Panel3.TabIndex = 148
        '
        'btnCSV
        '
        Me.btnCSV.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCSV.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnCSV.ColorFillBlend = CBlendItems1
        Me.btnCSV.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCSV.Corners.All = CType(6, Short)
        Me.btnCSV.Corners.LowerLeft = CType(6, Short)
        Me.btnCSV.Corners.LowerRight = CType(6, Short)
        Me.btnCSV.Corners.UpperLeft = CType(6, Short)
        Me.btnCSV.Corners.UpperRight = CType(6, Short)
        Me.btnCSV.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCSV.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCSV.FocalPoints.CenterPtX = 1.0!
        Me.btnCSV.FocalPoints.CenterPtY = 0.72!
        Me.btnCSV.FocalPoints.FocusPtX = 0!
        Me.btnCSV.FocalPoints.FocusPtY = 0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCSV.FocusPtTracker = DesignerRectTracker2
        Me.btnCSV.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCSV.ForeColor = System.Drawing.Color.White
        Me.btnCSV.Image = Nothing
        Me.btnCSV.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCSV.ImageIndex = 0
        Me.btnCSV.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCSV.Location = New System.Drawing.Point(842, 4)
        Me.btnCSV.Name = "btnCSV"
        Me.btnCSV.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCSV.SideImage = Nothing
        Me.btnCSV.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCSV.Size = New System.Drawing.Size(107, 25)
        Me.btnCSV.TabIndex = 187
        Me.btnCSV.Text = "CSV �԰�"
        Me.btnCSV.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCSV.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnBldIn
        '
        Me.btnBldIn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnBldIn.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnBldIn.ColorFillBlend = CBlendItems2
        Me.btnBldIn.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnBldIn.Corners.All = CType(6, Short)
        Me.btnBldIn.Corners.LowerLeft = CType(6, Short)
        Me.btnBldIn.Corners.LowerRight = CType(6, Short)
        Me.btnBldIn.Corners.UpperLeft = CType(6, Short)
        Me.btnBldIn.Corners.UpperRight = CType(6, Short)
        Me.btnBldIn.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnBldIn.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnBldIn.FocalPoints.CenterPtX = 1.0!
        Me.btnBldIn.FocalPoints.CenterPtY = 0.72!
        Me.btnBldIn.FocalPoints.FocusPtX = 0!
        Me.btnBldIn.FocalPoints.FocusPtY = 0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnBldIn.FocusPtTracker = DesignerRectTracker4
        Me.btnBldIn.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnBldIn.ForeColor = System.Drawing.Color.White
        Me.btnBldIn.Image = Nothing
        Me.btnBldIn.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnBldIn.ImageIndex = 0
        Me.btnBldIn.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnBldIn.Location = New System.Drawing.Point(950, 4)
        Me.btnBldIn.Name = "btnBldIn"
        Me.btnBldIn.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnBldIn.SideImage = Nothing
        Me.btnBldIn.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnBldIn.Size = New System.Drawing.Size(107, 25)
        Me.btnBldIn.TabIndex = 188
        Me.btnBldIn.Text = "��  ��(F2)"
        Me.btnBldIn.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnBldIn.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnClear.ColorFillBlend = CBlendItems3
        Me.btnClear.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnClear.Corners.All = CType(6, Short)
        Me.btnClear.Corners.LowerLeft = CType(6, Short)
        Me.btnClear.Corners.LowerRight = CType(6, Short)
        Me.btnClear.Corners.UpperLeft = CType(6, Short)
        Me.btnClear.Corners.UpperRight = CType(6, Short)
        Me.btnClear.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnClear.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnClear.FocalPoints.CenterPtX = 1.0!
        Me.btnClear.FocalPoints.CenterPtY = 0.72!
        Me.btnClear.FocalPoints.FocusPtX = 0!
        Me.btnClear.FocalPoints.FocusPtY = 0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(1058, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 186
        Me.btnClear.Text = "ȭ������(F4)"
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnExit.ColorFillBlend = CBlendItems4
        Me.btnExit.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnExit.Corners.All = CType(6, Short)
        Me.btnExit.Corners.LowerLeft = CType(6, Short)
        Me.btnExit.Corners.LowerRight = CType(6, Short)
        Me.btnExit.Corners.UpperLeft = CType(6, Short)
        Me.btnExit.Corners.UpperRight = CType(6, Short)
        Me.btnExit.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnExit.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnExit.FocalPoints.CenterPtX = 1.0!
        Me.btnExit.FocalPoints.CenterPtY = 0.76!
        Me.btnExit.FocalPoints.FocusPtX = 0!
        Me.btnExit.FocalPoints.FocusPtY = 0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnExit.FocusPtTracker = DesignerRectTracker8
        Me.btnExit.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnExit.ForeColor = System.Drawing.Color.White
        Me.btnExit.Image = Nothing
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnExit.ImageIndex = 0
        Me.btnExit.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnExit.Location = New System.Drawing.Point(1166, 4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 185
        Me.btnExit.Text = "����(Esc)"
        Me.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnExit.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.dtpInDt)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.dtpDonDt)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 192)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(380, 64)
        Me.GroupBox4.TabIndex = 149
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Panel1)
        Me.GroupBox5.Location = New System.Drawing.Point(389, 566)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(879, 272)
        Me.GroupBox5.TabIndex = 150
        Me.GroupBox5.TabStop = False
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.txtPatnm)
        Me.GroupBox6.Controls.Add(Me.txtRegNo)
        Me.GroupBox6.Controls.Add(Me.Label13)
        Me.GroupBox6.Location = New System.Drawing.Point(8, 253)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(380, 39)
        Me.GroupBox6.TabIndex = 151
        Me.GroupBox6.TabStop = False
        '
        'txtPatnm
        '
        Me.txtPatnm.BackColor = System.Drawing.Color.LightGray
        Me.txtPatnm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatnm.Location = New System.Drawing.Point(215, 10)
        Me.txtPatnm.MaxLength = 0
        Me.txtPatnm.Name = "txtPatnm"
        Me.txtPatnm.ReadOnly = True
        Me.txtPatnm.Size = New System.Drawing.Size(101, 21)
        Me.txtPatnm.TabIndex = 144
        '
        'txtRegNo
        '
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Location = New System.Drawing.Point(88, 10)
        Me.txtRegNo.MaxLength = 9
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(124, 21)
        Me.txtRegNo.TabIndex = 17
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label13.Location = New System.Drawing.Point(4, 10)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(83, 21)
        Me.Label13.TabIndex = 143
        Me.Label13.Tag = "0"
        Me.Label13.Text = "��Ϲ�ȣ"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FGB05
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1272, 875)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGB05"
        Me.Text = "�����԰�"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdBldInList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        CType(Me.spdPastList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " ���ι�ư ó�� "

    Private Sub FGB05_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        CDHELP.FGCDHELPFN.fn_PopMsg(Me, "S"c, "")
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    'Function Key����()
    Private Sub MyBase_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F2
                btnBldIn_ButtonClick(Nothing, Nothing)

            Case Keys.F4
                ' ȭ������
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select

    End Sub

    Private Sub btnBldIn_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBldIn.Click
        Try
            Dim sAboType As String = "", sRhType As String = "", sDonGbn As String = "", sInPlace As String, sDonQnt As String = ""

            If Me.txtBldNo.Text.Equals("") Then
                fn_PopMsg(Me, "I"c, "���׹�ȣ�� �Է��ϼ���.!!")
                Return
            End If

            If Len(Me.txtBldNo.Text.Replace("-", "").Trim) < 10 Then
                fn_PopMsg(Me, "I"c, "���׹�ȣ�� Ʋ���ϴ�.  Ȯ���ϼ���.!!")
                Return
            End If

            If rdoBld0.Checked = True Then ' �������� -> ���׿�
                sDonGbn = "0"
            ElseIf rdoBld1.Checked = True Then
                sDonGbn = "1"
            ElseIf rdoBld2.Checked = True Then
                sDonGbn = "2"
            ElseIf rdoBld3.Checked = True Then
                sDonGbn = "3"
            Else
                sDonGbn = "4"
            End If

            If Me.cboInPlace.SelectedIndex = -1 Or Me.cboInPlace.SelectedIndex = 0 Then
                sInPlace = "0"     ' �԰���� ���� combobox
            Else
                sInPlace = Me.cboInPlace.SelectedIndex.ToString()
            End If

            If Me.cboBType.SelectedIndex = -1 Or Me.cboBType.SelectedIndex = 0 Then
                sAboType = "A"
            Else
                sAboType = Me.cboBType.Text
            End If

            If Me.cboRH.SelectedIndex = -1 Or Me.cboRH.SelectedIndex = 0 Then
                sRhType = "+"
            Else
                sRhType = Me.cboRH.Text
            End If

            If Me.rdoBldQnt0.Checked = True Then
                sDonQnt = "0"   ' 400ml
            ElseIf rdoBldQnt1.Checked = True Then
                sDonQnt = "1"   ' 320ml
            End If

            Dim sInState As String = "0"     ' �����԰� ���� -> 0:�����, 1:�����, 2:���, 3: �и�(���ƿ�)  

            ' üũ�� ���������� ���׹�ȣ�� ������ ������ �԰��Ŵ!
            Dim alInList As New ArrayList     ' �԰���Ʈ�� �Ѱ��� ������������Ʈ ����

            '-- ����, ����, �ڰ��� ��� ��Ϲ�ȣ Ȯ��
            If sDonGbn = "2" Or sDonGbn = "3" Or sDonGbn = "4" Then
                If Me.txtRegNo.Text = "" Then
                    fn_PopMsg(Me, "I"c, "����, ����, �ڰ� ������ ���� ��Ϲ�ȣ�� �Է��ؾ� �մϴ�.")
                    Exit Sub
                End If
                If Me.txtPatnm.Text = "" Then
                    Me.txtPatnm.Text = BldIn.fnGet_PatName(Me.txtRegNo.Text)
                    If Me.txtPatnm.Text = "" Then
                        If MsgBox("��Ϲ�ȣ�� �������� �ʽ��ϴ�.  �׷��� �԰� �Ͻðڽ��ϱ�?", MsgBoxStyle.Question Or MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Cancel Then
                            Return
                        End If
                    End If
                End If
            End If

            ' ��������, �԰����� Ȯ���ϱ�!!
            If fnDate_Check(dtpDonDt.Value, "��������") = False Or fnDate_Check(dtpInDt.Value, "�԰�����") = False Then Return

            ' �������� > �԰������� ���� ���� �ȵ�!!!
            If CDate(Format(Me.dtpDonDt.Value, "yyyy-MM-dd HH:mm")) > CDate(Format(Me.dtpInDt.Value, "yyyy-MM-dd HH:mm")) Then
                fn_PopMsg(Me, "I"c, "�������ڰ� �԰����ں��� ũ�Ƿ� �԰� �Ұ��� �մϴ�.")
                Return
            End If

            Dim alBldList As New ArrayList

            With Me.spdComList
                Dim iCheck As Integer = 0   ' �԰��Ű�� ���� üũ�� �ϳ��� �ȵǾ��� ��츦 üũ��
                Dim sComCd As String, sAvailDt As String, sAvailDt1, sAvailDt2, sComNm As String
                Dim sbldcd As String '<20130410 �����ڵ� �߰�

                For iRow As Integer = 1 To .MaxRows
                    .Row = iRow
                    .Col = .GetColFromID("üũ")

                    If .Text = "1" Then ' ���������� üũ�� ���
                        .Col = .GetColFromID("��ȿ����") : sAvailDt1 = .Text : iCheck += 1
                        .Col = .GetColFromID("�Ͻ�") : sAvailDt2 = .Text
                        .Col = .GetColFromID("�ڵ�") : sComCd = .Text
                        .Col = .GetColFromID("��������") : sComNm = .Text
                        .Col = .GetColFromID("�����ڵ�") : sbldcd = .Text

                        Dim dt2 As DataTable = CGDA_BT.fnGet_ComCd(sbldcd) '<20130410 �����Ǳ��� 

                        Dim sTime As String = Me.dtpDonDt.Text
                        '<20130329 �����԰�� �ð����� ���ɱ�� �߰� 
                        If sTime = "" Then
                            sTime = "23:59:59"
                        Else
                            sTime = sTime.Substring(11, 5) + ":00"
                        End If


                        If dt2.Rows(0).Item("platyn").ToString = "N" Then '<20130410 �Ϲ������϶��� �Ƚ�
                            sAvailDt2 = "23:59:59"
                        Else
                            sAvailDt1 = CStr(DateAdd(DateInterval.Day, 1, CDate(sAvailDt1)))
                            sAvailDt2 = sTime
                        End If

                        sAvailDt = sAvailDt1 + " " + sAvailDt2

                        ' ������ �԰�� ���׹�ȣ + ������������ üũ��! ( �ߺ��԰��ϸ� unique error �߻��ϹǷ� ������ ����!! )
                        Dim dt As DataTable = BldIn.fnGet_BldNo_Info(txtBldNo.Text.Trim, sComCd)

                        If dt.Rows.Count > 0 Then
                            fn_PopMsg(Me, "I"c, "�̹� �԰�� �����Դϴ�. �ٽ� Ȯ�����ּ���!!")
                            Return
                        End If

                        ' �˻����� ���µ� ���� �԰��ϰڴٰ� �ϰ� comment �ȳ־����� �԰�ȵ���!!
                        If NoRst_Flag = "Yes" And Me.txtComment.Text = "" Then
                            fn_PopMsg(Me, "E"c, "�˻�������� �԰�� Comment�� �־�� �մϴ�")
                            Me.txtComment.Focus()
                            Return
                        End If

                        Dim stuBld As New STU_BldInfo

                        With stuBld
                            .ComCd = sComCd
                            .BldNo = Me.txtBldNo.Text.Replace("-", "")
                            .Bldno_Full = Fn.BLDNO_View(Me.txtBldNo.Text)
                            .InDt = Me.dtpInDt.Text
                            .InPlace = sInPlace
                            .Abo = sAboType
                            .Rh = sRhType
                            .DonQnt = sDonQnt
                            '.DonDt = Me.dtpDonDt.Text + " 00:00:00"
                            .DonDt = Me.dtpDonDt.Text + ":00"
                            .AvailDt = sAvailDt
                            .Cmt = Me.txtComment.Text
                            .RegNo = Me.txtRegNo.Text
                            .DonGbn = sDonGbn
                        End With

                        If BldIn.fnExe_BldIn(stuBld) Then
                            alInList.Add(stuBld)    ' ������������ �迭����Ʈ�� �߰��Ѵ�.
                        End If

                    End If
                Next

                If iCheck < 1 Then
                    fn_PopMsg(Me, "I"c, "���õ� ���������� �����ϴ�. �ٽ� Ȯ���ϼ���!")
                    Exit Sub
                End If

            End With

            ' �԰���Ʈ�� �����ֱ�!!!
            sbDisplay_BldInList(alInList)
            ' MsgBox("���������� �԰�Ǿ����ϴ�", MsgBoxStyle.Information, Me.Text)

            ' �����԰�� ������ �ʱ�ȭ
            If Me.rdoAuto0.Checked = True Then sbForm_Clear("2")

            Me.txtBldNo.Focus()
            Me.txtBldNo.SelectAll()

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub btnCSV_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCSV.Click

        Me.txtBldNo.Text = ""
        sbForm_Clear("2")

        Dim openFileDialog1 As New OpenFileDialog

        openFileDialog1.InitialDirectory = "C:\"
        openFileDialog1.Filter = "csv files (*.csv)|*.csv"
        openFileDialog1.FilterIndex = 2    ' ���� ��ȭ ���ڿ��� ���� ������ ������ �ε����� �������ų� ����
        openFileDialog1.RestoreDirectory = True  ' ��ȭ���ڸ� �ݱ��� ��ȭ���ڿ��� ���� ���͸��� �������� ���θ� ��Ÿ���� ���� �������ų� ����

        If openFileDialog1.ShowDialog() = DialogResult.OK Then

            Dim objFrm As New LISB.FGB05_S01
            Dim alList As New ArrayList

            alList = objFrm.Display_Result(openFileDialog1.FileName, User_Id)

            If alList Is Nothing Then Return

            ' �԰���Ʈ�� �����ֱ�
            With Me.spdBldInList
                For iRow As Integer = 0 To alList.Count - 1
                    Dim objProvList As New STU_BldInfo
                    objProvList = CType(alList.Item(iRow), STU_BldInfo)

                    .MaxRows += 1
                    .Row = .MaxRows

                    .Col = .GetColFromID("�԰��Ͻ�") : .Text = Format(dtpInDt.Value, "yyyy-MM-dd")
                    .Col = .GetColFromID("���׹�ȣ") : .Text = objProvList.Bldno_Full
                    .Col = .GetColFromID("��������") : .Text = objProvList.ComNmd
                    .Col = .GetColFromID("ABO") : .Text = objProvList.Abo
                    .Col = .GetColFromID("Rh") : .Text = objProvList.Rh
                    .Col = .GetColFromID("�����Ͻ�") : .Text = objProvList.DonDt
                    .Col = .GetColFromID("��ȿ�Ͻ�") : .Text = objProvList.AvailDt
                    .Col = .GetColFromID("����") : .Text = "���׿�"
                    .Col = .GetColFromID("����") : .Text = "���԰�"
                Next
            End With
        End If

    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        sbForm_Clear("0")
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

    Private Sub sbDisplay_BldInList(ByVal r_al_BldList As ArrayList)

        If mi_MaxRow > 0 And mi_InCnt > 0 Then    ' �ٷ����� �԰���Ʈ�� �߰��Ǿ��� row���� ������ ������� ����
            Dim iStart As Integer = mi_MaxRow - mi_InCnt + 1

            With spdBldInList
                For iCnt As Integer = iStart To mi_MaxRow
                    .Col = 1 : .Col2 = .MaxCols : .Row = iCnt : .Row2 = iCnt
                    .BlockMode = True      ' ¦��row : ���λ��� EvenRowBackColor / Ȧ��row: White

                    If iCnt Mod 2 = 0 Then  ' ¦��
                        .BackColor = System.Drawing.Color.FromArgb(237, 255, 230)
                    Else    ' Ȧ��
                        .BackColor = System.Drawing.Color.White
                    End If

                    .BlockMode = False
                Next
            End With
        End If


        With Me.spdBldInList

            For ix = 0 To r_al_BldList.Count - 1
                .MaxRows += 1

                Dim sDonGbn As String = ""
                If CType(r_al_BldList(0), STU_BldInfo).DonGbn = "0" Then
                    sDonGbn = "���׿�"
                ElseIf CType(r_al_BldList(0), STU_BldInfo).DonGbn = "1" Then
                    sDonGbn = "����"
                ElseIf CType(r_al_BldList(0), STU_BldInfo).DonGbn = "2" Then
                    sDonGbn = "����"
                ElseIf CType(r_al_BldList(0), STU_BldInfo).DonGbn = "3" Then
                    sDonGbn = "����"
                Else
                    sDonGbn = "�ڰ�"
                End If

                Dim sInState As String = ""
                If CType(r_al_BldList(0), STU_BldInfo).UsedGbn = "0" Then sInState = "���԰�" ' �űԷ� �԰��ϴ� ���

                .Row = .MaxRows
                .Col = .GetColFromID("�԰��Ͻ�") : .Text = CType(r_al_BldList(ix), STU_BldInfo).InDt
                .Col = .GetColFromID("���׹�ȣ") : .Text = CType(r_al_BldList(ix), STU_BldInfo).Bldno_Full
                .Col = .GetColFromID("��������") : .Text = CType(r_al_BldList(ix), STU_BldInfo).ComNmd
                .Col = .GetColFromID("�뷮") : .Text = CType(r_al_BldList(ix), STU_BldInfo).DonQnt
                .Col = .GetColFromID("ABO") : .Text = CType(r_al_BldList(ix), STU_BldInfo).Abo
                .Col = .GetColFromID("Rh") : .Text = CType(r_al_BldList(ix), STU_BldInfo).Rh
                .Col = .GetColFromID("�����Ͻ�") : .Text = CType(r_al_BldList(ix), STU_BldInfo).DonDt
                .Col = .GetColFromID("��ȿ�Ͻ�") : .Text = CType(r_al_BldList(ix), STU_BldInfo).AvailDt
                .Col = .GetColFromID("����") : .Text = sDonGbn
                .Col = .GetColFromID("����") : .Text = sInState
                .Col = .GetColFromID("��Ϲ�ȣ") : .Text = CType(r_al_BldList(ix), STU_BldInfo).RegNo

                .Col = 1 : .Col2 = .MaxCols : .Row = .MaxRows : .Row2 = .MaxRows
                .BlockMode = True
                .BackColor = System.Drawing.Color.FromArgb(187, 219, 203)
                .BlockMode = False
            Next

            mi_MaxRow = .MaxRows
            mi_InCnt = r_al_BldList.Count

        End With

    End Sub

    Private Sub rdoBldQnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBldQnt0.Click, rdoBldQnt1.Click
        sbQnt_Click()

    End Sub

    Private Sub sbQnt_Click()
        msNowTime = Format((New ServerDateTime).GetDateTime, "yyyy-MM-dd hh:mm:ss").ToString

        Try
            Dim sBldQnt As String = ""

            If Me.rdoBldQnt0.Checked = True Then
                sBldQnt = "400"
            ElseIf Me.rdoBldQnt1.Checked = True Then
                sBldQnt = "320"
            End If

            sbDisplay_ComList(sBldQnt, msNowTime)
            sbDisplay_Com_refresh()    ' �������� ����

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Public Sub sbDisplay_ComList(Optional ByVal rsBldQnt As String = "", Optional ByVal rsSearchTime As String = "", Optional ByVal rsNew As String = "")

        Try
            Dim dt As New DataTable
            Dim sBldQnt As String = rsBldQnt

            If rsNew = "Yes" Then  ' ���� �ѷ��ִ� ��� 
                dt = BldIn.fnGet_Comcd_List(sBldQnt, rsSearchTime, m_al_ComCd)

                m_al_ComCd.Clear()
                m_al_ComCd.TrimToSize()
            Else
                If Me.spdComList.MaxRows = 0 Then
                    dt = BldIn.fnGet_Comcd_List(sBldQnt, rsSearchTime)  ' ������ ���
                Else
                    If Me.txtBldNo.Text.Trim.Equals("") Then     ' �ƹ��͵� ���� ���¿��� �뷮�� �������� ��� -> ����ð����� ������ ��������.
                        dt = BldIn.fnGet_Comcd_List(sBldQnt, rsSearchTime)
                    Else
                        If Me.spdPastList.MaxRows = 0 And Me.rdoBld0.Checked = True Then
                            dt = BldIn.fnGet_Comcd_List(sBldQnt, rsSearchTime)
                        Else
                            Return
                        End If
                    End If
                End If
            End If

            If dt.Rows.Count > 0 Then
                With Me.spdComList
                    .MaxRows = dt.Rows.Count

                    For ix As Integer = 0 To .MaxRows - 1
                        .Row = ix + 1

                        .Col = .GetColFromID("üũ") : .Text = "" ' üũ���� ���� ����
                        .Col = .GetColFromID("��������") : .Text = dt.Rows(ix).Item("comnmd").ToString().Trim ' ��������
                        'freety ����
                        .Col = .GetColFromID("��ȿ����") : .Text = ""
                        .Col = .GetColFromID("�Ͻ�") : .Text = ""
                        'freety ����
                        .Col = .GetColFromID("�ڵ�") : .Text = dt.Rows(ix).Item("comcd").ToString().Trim ' �ڵ�
                        .Col = .GetColFromID("��ȿ�Ⱓ") : .Text = dt.Rows(ix).Item("availmi").ToString().Trim ' ��ȿ����
                        .Col = .GetColFromID("�����ڵ�") : .Text = dt.Rows(ix).Item("bldcd").ToString().Trim
                        ' ������ �ڵ�
                    Next
                End With

                If Me.txtBldNo.Text.Trim.Equals("") Or Me.spdPastList.MaxRows = 0 Then     ' �ƹ��͵� ���� ���¿��� �뷮�� �������� ��� -> ����ð����� ������ ��������.
                    sbDisplay_Com_refresh()
                End If
            Else
                fn_PopMsg(Me, "S"c, "��ȸ�� �����Ͱ� �����ϴ�.")
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub cboBType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboBType.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            cboBType.Items.Add(cboBType.Text)   '���ο� �������� �Է��ؾ��ϴ� ��� ���
        End If
    End Sub

    Private Sub cboRH_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cboRH.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            cboRH.Items.Add(cboRH.Text)
        End If
    End Sub


    Private Sub txtBldNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldNo.KeyDown

        If e.KeyCode <> Windows.Forms.Keys.Enter Then Return

        Try

            ' ���׹�ȣ üũ
            If Me.txtBldNo.Text.Equals("") Then
                sbForm_Clear("2") : Exit Sub
            ElseIf Len(Me.txtBldNo.Text.Trim) < 10 Then
                sbForm_Clear("2")
                fn_PopMsg(Me, "I"c, "�߸��� ���׹�ȣ �Դϴ�. �ٽ� Ȯ���� �ּ���.")
                Return
            End If

            Dim dt As DataTable = BldIn.fnGet_BldNo_Info(Me.txtBldNo.Text.Trim, "") ' ���׹�ȣ�� �̿��Ͽ� �԰�� �������� ������ ȭ�鿡 �ѷ���

            If dt.Rows.Count > 0 Then
                '�Է� ���׹�ȣ

                sbForm_Clear("2")

                sbDisplay_BldnoList(dt)   ' ���� �԰� ����Ʈ ��ȸ spread�� �ѷ��ֱ�!!
                sbDispaly_Data(dt, "1")

                Me.lblNewGbn.Text = "No"

            Else
                '�ű� ���׹�ȣ

                '���� �԰�� ȭ�� �ʱ�ȭ
                If Me.rdoAuto0.Checked = True Or Me.lblNewGbn.Text = "No" Then sbForm_Clear("2")

                If Me.txtBldNo.Text.Trim.Substring(0, 2) = COMMON.CommLogin.LOGIN.PRG_CONST.Bank_DonorBldNo Then
                    ' ������ ����
                    dt = LISAPP.APP_BD.DonFn.fnGet_Doner_BldInfo(Me.txtBldNo.Text.Trim)
                    If dt.Rows.Count > 0 Then
                        Dim dt_don As DataTable
                        Dim sDonRegNo As String = ""

                        sDonRegNo = dt.Rows(0).Item("donregno").ToString()

                        dt_don = LISAPP.APP_BD.DonFn.fnGet_JudgRst(sDonRegNo)

                        If dt_don.Rows.Count > 0 Then
                            ' ������� ���̴� �԰��� �� ����!!!
                            If dt_don.Rows(0).Item("JUDGRST").ToString = "" Then  ' ��������� ������
                                If MsgBox("�˻���� ������ �԰� �Ұ����մϴ�" + "vbcrlf" & "�׷��� �԰��Ͻðڽ��ϱ�?", MsgBoxStyle.Exclamation, "�԰����") = MsgBoxResult.Ok Then
                                    NoRst_Flag = "Yes"  ' �˻��� ���̵� ���� �԰��� ��츦 ��Ÿ��!!
                                    txtComment.Focus()  ' commment�� �� �Է��ؾ���!!
                                End If
                            ElseIf dt_don.Rows(0).Item("JUDGRST").ToString.Trim = "������" Then
                                fn_PopMsg(Me, "I"c, "�˻����� �������̹Ƿ� �԰� �Ұ����մϴ�.")
                                Exit Sub
                            Else    ' �����ΰ��
                                sbDisplay_Doner(dt)
                            End If
                        Else
                            ' ���������� �����ش�. 
                            sbDisplay_Doner(dt)
                        End If
                    Else
                        fn_PopMsg(Me, "I"c, "���������� �����ϴ�. �ٽ� Ȯ�����ּ���.")
                        Exit Sub
                    End If

                Else
                    ' ���׿� ����
                    Me.rdoBld0.Checked = True

                End If

                Me.lblNewGbn.Text = ""

            End If

            If Me.rdoAuto0.Checked = True Then
                ' ���� �԰�
                Me.txtBType.Focus()    ' ���������� ��Ŀ���̵�

            Else
                ' �ϰ� �԰�
                Me.txtBldNo.Focus()
                Me.txtBldNo.SelectAll()

            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDispaly_Data(ByVal r_dt As DataTable, ByVal rsWhat As String)

        Try
            With r_dt.Rows(0)
                Me.cboInPlace.SelectedIndex = CType(.Item("inplace").ToString(), Integer)

                If .Item("dongbn").ToString() = "0" Then
                    Me.rdoBld0.Checked = True
                ElseIf .Item("dongbn").ToString() = "1" Then
                    Me.rdoBld1.Checked = True
                ElseIf .Item("dongbn").ToString() = "2" Then
                    Me.rdoBld2.Checked = True
                ElseIf .Item("dongbn").ToString() = "3" Then
                    Me.rdoBld3.Checked = True
                Else
                    Me.rdoBld4.Checked = True
                End If

                Me.cboBType.Text = .Item("abo").ToString()
                Me.cboRH.Text = .Item("rh").ToString()
                Me.lblBType.Text = Me.cboBType.Text + Me.cboRH.Text

                Me.dtpDonDt.Value = CType(.Item("dondt").ToString(), Date)  ' ��������
                Me.dtpInDt.Value = CType(.Item("indt").ToString(), Date)    ' �԰�����

                msInDate = Format(Me.dtpInDt.Value, "yyyy-MM-dd")

                If .Item("donqnt").ToString() = "0" Then
                    Me.rdoBldQnt0.Checked = True   ' 400ml
                    If rsWhat = "1" Then
                        sbDisplay_ComList("400", msInDate, "Yes")
                    Else
                        sbDisplay_ComList("400", msInDate)
                    End If

                ElseIf .Item("donqnt").ToString() = "1" Then    '320ml
                    Me.rdoBldQnt1.Checked = True
                    If rsWhat = "1" Then
                        sbDisplay_ComList("320", msInDate, "Yes")
                    Else
                        sbDisplay_ComList("320", msInDate)
                    End If
                End If

                Me.dtpDonDt_CloseUp(Nothing, New System.EventArgs)
                sbDisplay_Com_refresh()

            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbDisplay_Doner(ByVal r_dt As DataTable)

        Try
            Dim sDonRegNo As String  ' �����ڵ�Ϲ�ȣ (ABO,RH �˻����� �����ֱ����� VIEW ��ȸ�� �ʿ�)
            Dim sDonGbn As String = ""

            msNowTime = Format((New ServerDateTime).GetDateTime, "yyyy-MM-dd hh:mm:ss").ToString

            With r_dt.Rows(0)

                sDonRegNo = .Item("donregno").ToString()
                Me.dtpDonDt.Value = CType(.Item("dondt").ToString(), Date)  ' ��������
                Me.dtpDonDt_CloseUp(Nothing, New System.EventArgs)

                If .Item("dongbn").ToString() = "1" Then
                    sDonGbn = "0"
                    Me.rdoBld1.Checked = True

                ElseIf .Item("dongbn").ToString() = "2" Then
                    sDonGbn = "1"
                    rdoBld2.Checked = True

                ElseIf .Item("dongbn").ToString() = "3" Then
                    sDonGbn = "2"
                    rdoBld3.Checked = True

                ElseIf .Item("dongbn").ToString() = "4" Then
                    sDonGbn = "2"
                    rdoBld4.Checked = True
                Else
                    rdoBld0.Checked = True
                End If

                If .Item("bldqnt").ToString() = "0" Then
                    rdoBldQnt0.Checked = True   ' 400ml
                    sbDisplay_ComList("400", msNowTime)   ' ó���԰� �ϹǷ� ����ð����� �ڵ����� ��ȿ�Ⱓ�� ������ �����´�.
                ElseIf .Item("BLDQNT").ToString() = "1" Then    '320ml
                    rdoBldQnt1.Checked = True
                    sbDisplay_ComList("320", msNowTime)
                End If

                Me.dtpDonDt_CloseUp(Nothing, New System.EventArgs)
            End With

            Dim dt As DataTable = LISAPP.APP_BD.DonFn.fnGet_Doner_Info(sDonRegNo, sDonGbn)

            If dt.Rows.Count > 0 Then
                Me.lblBType.Text = dt.Rows(0).Item("viewrst").ToString() + dt.Rows(1).Item("viewrst").ToString()

                Me.cboBType.SelectedItem = dt.Rows(0).Item("viewrst").ToString()
                Me.cboRH.SelectedItem = dt.Rows(1).Item("viewrst").ToString()
            Else
                Me.lblBType.Text = "" : Me.cboBType.SelectedIndex = 0 : Me.cboRH.SelectedIndex = 0
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub sbForm_Clear(ByVal rsStep As String)

        Try
            If InStr("0", rsStep.ToString, CompareMethod.Text) > 0 Then
                Me.txtBldNo.Text = ""
                Me.spdBldInList.MaxRows = 0    ' �԰���Ʈ

                mi_MaxRow = 0 : mi_InCnt = 0
            End If

            If InStr("02", rsStep.ToString, CompareMethod.Text) > 0 Then

                Me.cboInPlace.SelectedIndex = 0 : Me.rdoBld0.Checked = True : Me.rdoBld1.Checked = False
                Me.cboBType.SelectedIndex = 0 : Me.cboRH.SelectedIndex = 0 : Me.txtBType.Text = "" : Me.txtBldQnt.Text = ""

                Me.dtpDonDt.Value = (New ServerDateTime).GetDateTime
                Me.dtpInDt.Value = (New ServerDateTime).GetDateTime
                Me.txtComment.Text = "" : Me.lblBType.Text = ""
                Me.txtRegNo.Text = "" : Me.txtRegNo.ReadOnly = True : Me.txtPatnm.Text = ""

                Me.dtpDonDt_CloseUp(Nothing, New System.EventArgs)

                Me.spdComList.MaxRows = 0
                Me.spdPastList.MaxRows = 0

                Me.rdoBldQnt1.Checked = True
                sbQnt_Click()
                '>
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub rdoBld_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoBld0.Click, rdoBld1.Click, rdoBld2.Click, rdoBld3.Click, rdoBld4.Click
        Dim objRdo As Windows.Forms.RadioButton = CType(sender, Windows.Forms.RadioButton)
        Dim sTag As String = CType(objRdo.Tag, String)

        If sTag = "0" Then            ' ���׿�
            Me.txtRegNo.ReadOnly = True
        ElseIf sTag = "1" Then        ' ����
            Me.txtRegNo.ReadOnly = True
        ElseIf sTag = "2" Then        ' ����
            Me.txtRegNo.ReadOnly = False
        ElseIf sTag = "3" Then        ' ����
            Me.txtRegNo.ReadOnly = False
        Else                            ' �ڰ�
            Me.txtRegNo.ReadOnly = False
        End If

    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        sbDisplay_BldnoList()

    End Sub

    Private Sub sbDisplay_BldnoList(Optional ByVal r_dt As DataTable = Nothing)
        Dim ix As Integer
        Dim sDateS As String = dtpDateS.Text.Replace("-", "").Replace(":", "").Replace(" ", "")
        Dim sDateE As String = dtpDateE.Text.Replace("-", "").Replace(":", "").Replace(" ", "")
        Dim sRef As String = ""   ' �԰����ڷ� ��ȸ�ϴ��� ���׹�ȣ�� ������ ������ ���Ÿ���Ʈ���� �����ϱ� ����
        Dim dt As New DataTable
        'sDateS += cboTimeS.Text
        'sDateE += cboTimeE.Text
        Try
            If r_dt Is Nothing Then  ' �԰����ڷ� ��ȸ�ϴ� ���
                dt = BldIn.fnGge_Bldno_List(Me.dtpDateS.Text.Replace("-", "") + cboTimeS.Text, Me.dtpDateE.Text.Replace("-", "") + cboTimeE.Text, Ctrl.Get_Code(cboComCd))
                sRef = ""
            Else
                dt = r_dt  ' ���׹�ȣ�� �ļ� ��ȸ�غ��� ���
                sRef = "Blood"
            End If

            Me.spdPastList.MaxRows = 0

            If dt.Rows.Count > 0 Then
                With spdPastList
                    .MaxRows = dt.Rows.Count

                    For ix = 0 To dt.Rows.Count - 1
                        .Row = ix + 1

                        .Col = .GetColFromID("���׹�ȣ") : .Text = dt.Rows(ix).Item("bldno").ToString().Trim
                        .Col = .GetColFromID("ABO") : .Text = dt.Rows(ix).Item("abo").ToString().Trim
                        .Col = .GetColFromID("Rh") : .Text = dt.Rows(ix).Item("rh").ToString().Trim
                        .Col = .GetColFromID("�����Ͻ�") : .Text = dt.Rows(ix).Item("dondt").ToString().Trim
                        .Col = .GetColFromID("����") : .Text = dt.Rows(ix).Item("de_dongbn").ToString().Trim

                        .Col = .GetColFromID("�԰��Ͻ�") : .Text = dt.Rows(ix).Item("indt").ToString().Trim
                        .Col = .GetColFromID("��������") : .Text = dt.Rows(ix).Item("comnmd").ToString().Trim
                        .Col = .GetColFromID("��ȿ�Ͻ�") : .Text = dt.Rows(ix).Item("availdt").ToString().Trim
                        .Col = .GetColFromID("����") : .Text = dt.Rows(ix).Item("de_state").ToString().Trim
                        .Col = .GetColFromID("�԰����") : .Text = dt.Rows(ix).Item("de_inplace").ToString().Trim

                        .Col = .GetColFromID("�ڵ�") : .Text = dt.Rows(ix).Item("comcd").ToString().Trim
                        If sRef = "Blood" Then     ' ���׹�ȣ�� �ļ� ��ȸ�غ��� ���
                            m_al_ComCd.Add(.Text)    ' �ѷ��ٶ� ������ �������� �ڵ带 �߰���Ŵ
                        End If

                        .Col = .GetColFromID("��ȿ�Ⱓ") : .Text = dt.Rows(ix).Item("availmi").ToString().Trim
                        '------------ hidden ---------------------------------------------------------------------------------
                        .Col = .GetColFromID("�������׹�ȣ") : .Text = dt.Rows(ix).Item("bldno").ToString().Trim
                        .Col = .GetColFromID("CMT") : .Text = dt.Rows(ix).Item("cmt").ToString().Trim
                        .Col = .GetColFromID("������") : .Text = dt.Rows(ix).Item("abo_rh").ToString().Trim
                        '-----------------------------------------------------------------------------------------------------
                        .Col = .GetColFromID("�԰���") : .Text = dt.Rows(ix).Item("usrnm").ToString().Trim
                        .Col = .GetColFromID("��Ϲ�ȣ") : .Text = dt.Rows(ix).Item("regno").ToString().Trim

                    Next

                End With

                m_al_ComCd.TrimToSize()
            Else
                fn_PopMsg(Me, "I"c, "��ȸ�� �����Ͱ� �����ϴ�. ������ �ٽ� �����ϼ���")
                Exit Sub
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "I"c, ex.Message)
        End Try

    End Sub

    Private Sub dtpInDt_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpInDt.CloseUp
        fnDate_Check(dtpInDt.Value, "�԰�����")

    End Sub

    Private Sub dtpDonDt_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpDonDt.CloseUp
        fnDate_Check(dtpDonDt.Value, "��������")

        With spdComList
            For intLoop As Integer = 1 To .MaxRows
                .Col = 1 : .Col2 = .MaxCols : .Row = intLoop : .Row2 = intLoop
                .BlockMode = True
                .Lock = False : .BackColor = System.Drawing.Color.White
                .BlockMode = False
            Next
        End With

        sbDisplay_Com_refresh()    ' �������ڸ� ����

    End Sub

    Private Sub sbDisplay_Com_refresh()     ' spdComList�� ���� �ѷ��ֱ� ����

        Try
            Dim iAvailDay As Integer = 0
            Dim dteDate_Cur As Date

            With spdComList
                .ReDraw = False
                Dim sTime As String = Me.dtpDonDt.Text
                '<20130329 �����԰�� �ð����� ���ɱ�� �߰� 
                If sTime = "" Then
                    sTime = "23:59:59"
                Else
                    sTime = sTime.Substring(11, 5) + ":00"
                End If

                For ix As Integer = 1 To .MaxRows
                    .Row = ix

                    .Col = .GetColFromID("�Ͻ�") : .Text = sTime '.Text = "23:59:59"

                    .Col = .GetColFromID("��ȿ�Ⱓ")
                    If .Text.Trim = "" Then .Text = "720" ' �⺻�Ⱓ -> ���氡��
                    iAvailDay = CType(.Text, Integer)

                    If .Text.Trim = "525600" Then   'FFP ���� ��� ��ȿ�Ⱓ�� 1���̴�. (525600 ��)
                        'dteDate_Cur = DateAdd(DateInterval.Day, -1, (DateAdd(DateInterval.Year, 1, CDate(Format(dtpDonDt.Value, "yyyy-MM-dd")))))
                        dteDate_Cur = DateAdd(DateInterval.Day, -1, (DateAdd(DateInterval.Year, 1, CDate(Format(dtpDonDt.Value, "yyyy-MM-dd HH:mm")))))
                    Else
                        'dteDate_Cur = DateAdd(DateInterval.Day, -1, (DateAdd(DateInterval.Minute, iAvailDay, CDate(Format(dtpDonDt.Value, "yyyy-MM-dd")))))
                        dteDate_Cur = DateAdd(DateInterval.Day, -1, (DateAdd(DateInterval.Minute, iAvailDay, CDate(Format(dtpDonDt.Value, "yyyy-MM-dd HH:mm")))))
                    End If

                    .Col = .GetColFromID("��ȿ����") : .Text = Format(dteDate_Cur, "yyyy-MM-dd").ToString

                    If dteDate_Cur < (New ServerDateTime).GetDateTime Then     ' ��ȿ���� < ����ð� -> ���� �Ұ��� (��ȿ�Ⱓ ������ �԰��ؼ� ����!!! )
                        .Col = 1 : .Col2 = .MaxCols : .Row = ix : .Row2 = ix
                        .BlockMode = True
                        .Lock = True : .BackColor = System.Drawing.Color.LightGray
                        .BlockMode = False
                    Else
                        .Col = 1 : .Col2 = .MaxCols : .Row = ix : .Row2 = ix
                        .BlockMode = True
                        .Lock = False : .BackColor = System.Drawing.Color.White
                        .BlockMode = False
                    End If
                Next

                .ReDraw = True
            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub dtpDonDt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpDonDt.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            fnDate_Check(Me.dtpDonDt.Value, "��������")
            '<20130401 �ð������� ���ͽ� �������ڼ����� ���� 
            With spdComList
                For intLoop As Integer = 1 To .MaxRows
                    .Col = 1 : .Col2 = .MaxCols : .Row = intLoop : .Row2 = intLoop
                    .BlockMode = True
                    .Lock = False : .BackColor = System.Drawing.Color.White
                    .BlockMode = False
                Next
            End With

            sbDisplay_Com_refresh()    ' �������ڸ� ����
        End If
    End Sub

    Private Sub DT_INTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpInDt.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            fnDate_Check(dtpInDt.Value, "�԰�����")
        End If
    End Sub

    Private Function fnDate_Check(ByVal r_dtePassDate As Date, ByVal rsRef As String) As Boolean
        Dim dteDate_Cur = (New ServerDateTime).GetDateTime     ' ���� �ð�

        'Dim dtePassTime As Date = CDate(Format(r_dtePassDate, "yyyy-MM-dd") + " " + "00:00:00")
        Dim dtePassTime As Date = CDate(Format(r_dtePassDate, "yyyy-MM-dd HH:mm"))


        If dtePassTime > dteDate_Cur Then   ' ���纸�� ū �̷��� ���ڴ� ���� �Ұ���

            fn_PopMsg(Me, "I"c, rsRef & "�� ���� �ð����� ũ�Ƿ� ������ �� �����ϴ�")

            If rsRef = "��������" Then
                Me.dtpDonDt.Value = dteDate_Cur
            Else
                Me.dtpInDt.Value = dteDate_Cur
            End If

            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        ' ����üũ -> ���������� �ִ� ������� ���θ� �Ǻ�
        If Validation_Check() = False Then Exit Sub

        Dim sBldNm As String = ""   ' ���� ���׹�ȣ ex) 2002000050
        Dim sBlood As String = ""   ' ���׹�ȣ ex) 20-03-000050
        Dim sComnMd As String = ""  ' ����������
        Dim sComCd As String = ""   ' �������� �ڵ�
        Dim sState As String = ""   ' "���԰�" �϶��� ���� ������

        With spdPastList
            .Row = .ActiveRow

            .Col = .GetColFromID("�������׹�ȣ") : sBldNm = .Text.Replace("-", "")
            .Col = .GetColFromID("�ڵ�") : sComCd = .Text.Trim
            .Col = .GetColFromID("��������") : sComnMd = .Text.Trim
            .Col = .GetColFromID("����") : sState = .Text.Trim

            If sBldNm.Length >= 10 Then
                sBlood = sBldNm.Substring(0, 2) & "-" & sBldNm.Substring(2, 2) & "-" & sBldNm.Substring(4, 6)
            End If

            If sState.Trim = "���԰�" Then
                If MsgBox("���׹�ȣ: [" & sBlood & "] �� ��������: [" & sComnMd & "] �� �����Ͻðڽ��ϱ�?", MsgBoxStyle.OkCancel, Me.Text) = MsgBoxResult.Ok Then

                    If BldIn.fnExe_BldIn_Del(sBldNm, sComCd) = True Then

                        fn_PopMsg(Me, "S"c, "���������� �����Ǿ����ϴ�")

                        If .ActiveRow = .MaxRows Then   ' �� �Ʒ����� �����Ǵ°�� ActiveRow�� ������ row�� ����ش�
                            .DeleteRows(.ActiveRow, 1)  ' spread �󿡼� delete��

                            .Row = .MaxRows - 1 : .Col = 1
                            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                        Else
                            .DeleteRows(.ActiveRow, 1)  ' spread �󿡼� delete��
                        End If

                        .MaxRows -= 1
                    Else
                        fn_PopMsg(Me, "I"c, "�������� ���߽��ϴ�")
                        Exit Sub
                    End If
                Else    ' ��� �Ǵ� x ��ư�� ���� â�� �������
                    Exit Sub
                End If

                '< add freety 2006/09/19
            ElseIf sState.Trim = "���԰�" Then
                If MsgBox("���׹�ȣ: [" & sBlood & "], ��������: [" & sComnMd & "] �� ���԰�� �����Ͻðڽ��ϱ�?", MsgBoxStyle.OkCancel Or MsgBoxStyle.DefaultButton2, Me.Text) = MsgBoxResult.Ok Then
                    If BldIn.fnExe_BldIn_Change(sBldNm, sComCd) Then
                        fn_PopMsg(Me, "I"c, "���԰�� ����Ǿ����ϴ�!!")

                        sbDisplay_BldnoList()
                    End If
                End If
                '>
            End If
        End With

    End Sub

    Private Function Validation_Check() As Boolean  ' ��ȿ�� üũ�ϱ�

        Try
            Dim strDESC As String = ""

            If btnDelete.Text.Trim = "�� ��" Then
                If Not USER_SKILL.Authority("B01", 5, strDESC) Then
                    fn_PopMsg(Me, "I"c, "[" & strDESC & "] ������ ���� ó���� �� �����ϴ�")
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Function

    Private Sub spdPastList_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdPastList.ClickEvent
        With Me.spdPastList
            .Row = e.row : .Col = .GetColFromID("CMT")
            Me.txtComment.Text = .Text
        End With
    End Sub

    ' 2004-06-18 JJH �߰� ���׹�ȣ �Է½� �ڵ� Return ��/�� RadionButton
    Private Sub rdoAuto1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAuto0.Click
        '���׹�ȣ �Է����� Focus�̵�
        'Clear_Step("2")
        Me.txtBldNo.Focus()
    End Sub


    Private Sub spdComList_ButtonClicked(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ButtonClickedEvent) Handles spdComList.ButtonClicked
        Me.txtBldNo.Focus()
    End Sub

    ' ���� �԰���Ʈ Excel ����ϱ�
    Private Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Dim sTime As String = Format(Now, "yyyyMMdd")

        With Me.spdPastList
            .ReDraw = False

            .MaxRows += 6
            .InsertRows(1, 6)

            .Row = 1
            .Col = 2
            .Text = "���� �԰� ����Ʈ ��ȸ"
            .FontBold = True
            .FontSize = 15
            .ForeColor = System.Drawing.Color.Red

            .Row = 3
            .Col = 2
            .Text = "��ȸ���� : " & Format(dtpDateS.Value, "yyyy-MM-dd") & " ~ " & Format(dtpDateE.Value, "yyyy-MM-dd") & "   (�԰����� ����)"

            .Row = 5
            .Col = .GetColFromID("�԰��Ͻ�")
            .Text = "�԰�����"
            .Col = .GetColFromID("������")
            .Text = "������"
            .Col = .GetColFromID("��������")
            .Text = "��������"
            .Col = .GetColFromID("���׹�ȣ")
            .Text = "���׹�ȣ"
            .Col = .GetColFromID("�����Ͻ�")
            .Text = "�����Ͻ�"
            .Col = .GetColFromID("��ȿ�Ͻ�")
            .Text = "��ȿ�Ͻ�"
            .Col = .GetColFromID("����")
            .Text = "����"

            .Col = .GetColFromID("����")
            .ColHidden = True
            .Col = .GetColFromID("�԰����")
            .ColHidden = True

            If spdPastList.ExportToExcel("c:\�����԰���Ʈ_" & sTime & ".xls", "�����԰���Ʈ", "") Then
                Process.Start("c:\�����԰���Ʈ_" & sTime & ".xls")
            End If

            .Col = .GetColFromID("����")
            .ColHidden = False
            .Col = .GetColFromID("�԰����")
            .ColHidden = False

            .DeleteRows(1, 6)
            .MaxRows -= 6

            .ReDraw = True
        End With
    End Sub

    Private Sub txtBldNm_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBldNo.GotFocus, txtBldQnt.GotFocus, txtBType.GotFocus, txtComment.GotFocus, txtRegNo.GotFocus

        Me.txtBldNo.SelectAll()

    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegNo.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            e.Handled = True
        End If
    End Sub

    Private Sub rdoAuto1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAuto1.Click

        Dim objFrm As New LISB.FGB05_S02
        Dim alList As ArrayList = objFrm.Display_Result(User_Id)

        If alList Is Nothing Then Return

        ' �԰���Ʈ�� �����ֱ�
        With spdBldInList
            For intRow As Integer = 0 To alList.Count - 1
                Dim objProvList As New STU_BldInfo
                objProvList = CType(alList.Item(intRow), STU_BldInfo)

                .MaxRows += 1
                .Row = .MaxRows

                .Col = .GetColFromID("�԰��Ͻ�") : .Text = Format(dtpInDt.Value, "yyyy-MM-dd")
                .Col = .GetColFromID("���׹�ȣ") : .Text = objProvList.Bldno_Full
                .Col = .GetColFromID("��������") : .Text = objProvList.ComNmd
                .Col = .GetColFromID("ABO") : .Text = objProvList.Abo
                .Col = .GetColFromID("Rh") : .Text = objProvList.Rh
                .Col = .GetColFromID("�����Ͻ�") : .Text = objProvList.DonDt
                .Col = .GetColFromID("��ȿ�Ͻ�") : .Text = objProvList.AvailDt
                .Col = .GetColFromID("��Ϲ�ȣ") : .Text = objProvList.RegNo
                .Col = .GetColFromID("����") : .Text = "���׿�"
                .Col = .GetColFromID("����") : .Text = "���԰�"
            Next
        End With

    End Sub

    Private Sub txtBldNm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBldNo.Click
        Me.txtBldNo.SelectAll()
    End Sub


    Private Sub Label13_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label13.DoubleClick

        If Not Me.txtRegNo.ReadOnly Then
            If Me.Label13.Tag.ToString = "0" Then

                Me.Label13.Text = "����"
                Me.Label13.BackColor = Color.MediumOrchid
                Me.Label13.Tag = 1

            Else
                Me.Label13.Text = "��Ϲ�ȣ"
                Me.Label13.BackColor = Color.SlateGray
                Me.Label13.Tag = 0
            End If
        End If
    End Sub

    Private Sub FGB05_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DS_SpreadDesige.sbInti(spdComList)
        DS_SpreadDesige.sbInti(spdPastList)
        DS_SpreadDesige.sbInti(spdBldInList)

        cboTimeS.SelectedIndex = 0
        cboTimeE.SelectedIndex = 23

        '20170810 ������ �߰�
        Dim dt As DataTable = mobjDAF.GetComCdInfo("")

        Me.cboComCd.Items.Clear()
        Me.cboComCd.Items.Add("[ALL] ��ü")
        If dt.Rows.Count > 0 Then
            With Me.cboComCd
                For i As Integer = 0 To dt.Rows.Count - 1
                    .Items.Add(dt.Rows(i).Item("COMNMD"))
                Next
            End With
        End If
        Me.cboComCd.SelectedIndex = 0


        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo
    End Sub

    Private Sub txtBldQnt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBldQnt.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        ' �ڵ����� �뷮 ���õǾ� �ش��ϴ� �뷮�� �������� ����Ʈ�� �ѷ��ش�.
        Try
            Dim dt As DataTable = BldIn.fnGet_BldCdToComcd(Me.txtBldQnt.Text)
            Dim sComCd As String = ""
            Dim sDonQnt As String = ""

            If dt.Rows.Count > 0 Then
                sComCd = dt.Rows(0).Item("comcd").ToString
                sDonQnt = dt.Rows(0).Item("donqnt").ToString
            End If

            If sDonQnt = "400" Then
                Me.rdoBldQnt0.Checked = True
            Else
                Me.rdoBldQnt1.Checked = True
            End If

            sbQnt_Click()

            ' �ش� ����������(check)

            With Me.spdComList
                For iLoop As Integer = 0 To .MaxRows - 1
                    .Row = iLoop + 1
                    .Col = .GetColFromID("�ڵ�")

                    If sComCd.Equals(.Text) Then
                        .Col = .GetColFromID("üũ")
                        .Text = "1"
                    End If
                Next
            End With

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
            Me.txtBldQnt.SelectAll()
            Me.txtBldQnt.Focus()
        End Try
    End Sub

    Private Sub txtBType_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBType.KeyDown
        If e.KeyCode <> Keys.Enter Then Return

        Try
            Dim dt As DataTable = BldIn.fnGet_BldCdToBType(Me.txtBType.Text)

            If dt.Rows.Count < 1 Then
                Me.cboBType.SelectedIndex = -1
                Me.cboRH.SelectedIndex = -1
                Me.lblBType.Text = ""
                Return
            End If

            Me.cboBType.Text = dt.Rows(0).Item("infofld1").ToString     ' ABO
            Me.cboRH.Text = dt.Rows(0).Item("infofld2").ToString       ' RH

            Me.lblBType.Text = Me.cboBType.Text + Me.cboRH.Text
            Me.lblBType.ForeColor = fnGet_BloodColor(Me.cboBType.Text)
            If Me.cboRH.Text = "-" Then
                Me.lblBType.BackColor = Color.Red
            Else
                Me.lblBType.BackColor = Color.White
            End If

            Me.txtBldQnt.Focus()   ' �뷮 �Է¶����� ��Ŀ�� �̵�
        Catch ex As Exception
            Me.txtBType.SelectAll()
            Me.txtBType.Focus()
        End Try
    End Sub
End Class

