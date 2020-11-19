'>>> Differential count ��� �Է�

Imports COMMON.CommFN
Imports System.Windows.Forms

Public Class FGDIFF02
    Inherits System.Windows.Forms.Form

    Private Const msFile As String = "File : FGR06.vb, Class : FGR06" & vbTab

    Private msRegNo As String = ""
    Private msPatNm As String = ""
    Private msSexAge As String = ""
    Private msWBCcnt As String = ""      '-- 2007/11/01 ssh �߰�
    Private m_al_Rst As New ArrayList      '-- �˻��׸��ڵ�, �˻��׸��, ���
    Private msDiffCmt As String = ""     '-- 2007/11/13 ssh �߰�
    Private msTestCd As String = ""      '-- �˻��ڵ�
    Private msSpcCd As String = ""       '-- ��ü�ڵ�
    Private msBFViewRsts As String = ""  '-- �������

    Private m_frm As Windows.Forms.Form
    Private miLeftPos As Integer = 0
    Private miTopPos As Integer = 0
    Private mbSave As Boolean = False

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        'fKeyPadSetting()

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
    Friend WithEvents grpKeypad As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnKey14 As System.Windows.Forms.Button
    Friend WithEvents btnKey04 As System.Windows.Forms.Button
    Friend WithEvents btnKey03 As System.Windows.Forms.Button
    Friend WithEvents btnKey05 As System.Windows.Forms.Button
    Friend WithEvents btnKey15 As System.Windows.Forms.Button
    Friend WithEvents btnKey00 As System.Windows.Forms.Button
    Friend WithEvents btnKey02 As System.Windows.Forms.Button
    Friend WithEvents btnKey08 As System.Windows.Forms.Button
    Friend WithEvents btnKey09 As System.Windows.Forms.Button
    Friend WithEvents btnKey16 As System.Windows.Forms.Button
    Friend WithEvents btnKey07 As System.Windows.Forms.Button
    Friend WithEvents btnKey06 As System.Windows.Forms.Button
    Friend WithEvents btnKey12 As System.Windows.Forms.Button
    Friend WithEvents btnKey01 As System.Windows.Forms.Button
    Friend WithEvents btnKey13 As System.Windows.Forms.Button
    Friend WithEvents btnKey11 As System.Windows.Forms.Button
    Friend WithEvents btnKey10 As System.Windows.Forms.Button
    Friend WithEvents grpSetting As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents pnlResult As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblSexAge As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents spdKeyInfo As AxFPSpreadADO.AxfpSpread
    Friend WithEvents spdDiffCount As AxFPSpreadADO.AxfpSpread
    Friend WithEvents cbxMaxCount As System.Windows.Forms.ComboBox
    Friend WithEvents btnKeySave As System.Windows.Forms.Button
    Friend WithEvents lblKey01 As System.Windows.Forms.Label
    Friend WithEvents lblKey02 As System.Windows.Forms.Label
    Friend WithEvents lblKey03 As System.Windows.Forms.Label
    Friend WithEvents lblKey04 As System.Windows.Forms.Label
    Friend WithEvents lblKey05 As System.Windows.Forms.Label
    Friend WithEvents lblKey06 As System.Windows.Forms.Label
    Friend WithEvents lblKey07 As System.Windows.Forms.Label
    Friend WithEvents lblKey08 As System.Windows.Forms.Label
    Friend WithEvents lblKey09 As System.Windows.Forms.Label
    Friend WithEvents lblKey10 As System.Windows.Forms.Label
    Friend WithEvents lblKey13 As System.Windows.Forms.Label
    Friend WithEvents lblKey12 As System.Windows.Forms.Label
    Friend WithEvents lblKey11 As System.Windows.Forms.Label
    Friend WithEvents lblKey14 As System.Windows.Forms.Label
    Friend WithEvents lblKey16 As System.Windows.Forms.Label
    Friend WithEvents lblKey15 As System.Windows.Forms.Label
    Friend WithEvents pnlKey As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCnt As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblWBC As System.Windows.Forms.Label
    Friend WithEvents lblKey17 As System.Windows.Forms.Label
    Friend WithEvents btnKey17 As System.Windows.Forms.Button
    Friend WithEvents lblKey00 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As CButtonLib.CButton
    Friend WithEvents btnOK As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnMove As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGDIFF02))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems1 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker2 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker3 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems2 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker4 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim DesignerRectTracker5 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim CBlendItems3 As CButtonLib.cBlendItems = New CButtonLib.cBlendItems
        Dim DesignerRectTracker6 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Me.grpKeypad = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.pnlKey = New System.Windows.Forms.Panel
        Me.lblKey00 = New System.Windows.Forms.Label
        Me.lblKey17 = New System.Windows.Forms.Label
        Me.btnKey17 = New System.Windows.Forms.Button
        Me.lblKey14 = New System.Windows.Forms.Label
        Me.lblKey16 = New System.Windows.Forms.Label
        Me.lblKey15 = New System.Windows.Forms.Label
        Me.lblKey13 = New System.Windows.Forms.Label
        Me.lblKey12 = New System.Windows.Forms.Label
        Me.lblKey11 = New System.Windows.Forms.Label
        Me.lblKey10 = New System.Windows.Forms.Label
        Me.lblKey09 = New System.Windows.Forms.Label
        Me.lblKey08 = New System.Windows.Forms.Label
        Me.lblKey07 = New System.Windows.Forms.Label
        Me.lblKey06 = New System.Windows.Forms.Label
        Me.lblKey05 = New System.Windows.Forms.Label
        Me.lblKey04 = New System.Windows.Forms.Label
        Me.lblKey03 = New System.Windows.Forms.Label
        Me.lblKey02 = New System.Windows.Forms.Label
        Me.lblKey01 = New System.Windows.Forms.Label
        Me.btnKey14 = New System.Windows.Forms.Button
        Me.btnKey04 = New System.Windows.Forms.Button
        Me.btnKey03 = New System.Windows.Forms.Button
        Me.btnKey05 = New System.Windows.Forms.Button
        Me.btnKey15 = New System.Windows.Forms.Button
        Me.btnKey00 = New System.Windows.Forms.Button
        Me.btnKey02 = New System.Windows.Forms.Button
        Me.btnKey08 = New System.Windows.Forms.Button
        Me.btnKey09 = New System.Windows.Forms.Button
        Me.btnKey16 = New System.Windows.Forms.Button
        Me.btnKey07 = New System.Windows.Forms.Button
        Me.btnKey06 = New System.Windows.Forms.Button
        Me.btnKey12 = New System.Windows.Forms.Button
        Me.btnKey01 = New System.Windows.Forms.Button
        Me.btnKey13 = New System.Windows.Forms.Button
        Me.btnKey11 = New System.Windows.Forms.Button
        Me.btnKey10 = New System.Windows.Forms.Button
        Me.grpSetting = New System.Windows.Forms.GroupBox
        Me.btnKeySave = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdKeyInfo = New AxFPSpreadADO.AxfpSpread
        Me.lblTitle = New System.Windows.Forms.Label
        Me.pnlResult = New System.Windows.Forms.Panel
        Me.spdDiffCount = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.lblWBC = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblCnt = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cbxMaxCount = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.lblSexAge = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblName = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnMove = New System.Windows.Forms.Button
        Me.btnCancel = New CButtonLib.CButton
        Me.btnOK = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.grpKeypad.SuspendLayout()
        Me.pnlKey.SuspendLayout()
        Me.grpSetting.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdKeyInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlResult.SuspendLayout()
        CType(Me.spdDiffCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpKeypad
        '
        Me.grpKeypad.Controls.Add(Me.Label1)
        Me.grpKeypad.Controls.Add(Me.pnlKey)
        Me.grpKeypad.Location = New System.Drawing.Point(312, 69)
        Me.grpKeypad.Name = "grpKeypad"
        Me.grpKeypad.Size = New System.Drawing.Size(220, 287)
        Me.grpKeypad.TabIndex = 112
        Me.grpKeypad.TabStop = False
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(213, 31)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "����Ű ����"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlKey
        '
        Me.pnlKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlKey.Controls.Add(Me.lblKey00)
        Me.pnlKey.Controls.Add(Me.lblKey17)
        Me.pnlKey.Controls.Add(Me.btnKey17)
        Me.pnlKey.Controls.Add(Me.lblKey14)
        Me.pnlKey.Controls.Add(Me.lblKey16)
        Me.pnlKey.Controls.Add(Me.lblKey15)
        Me.pnlKey.Controls.Add(Me.lblKey13)
        Me.pnlKey.Controls.Add(Me.lblKey12)
        Me.pnlKey.Controls.Add(Me.lblKey11)
        Me.pnlKey.Controls.Add(Me.lblKey10)
        Me.pnlKey.Controls.Add(Me.lblKey09)
        Me.pnlKey.Controls.Add(Me.lblKey08)
        Me.pnlKey.Controls.Add(Me.lblKey07)
        Me.pnlKey.Controls.Add(Me.lblKey06)
        Me.pnlKey.Controls.Add(Me.lblKey05)
        Me.pnlKey.Controls.Add(Me.lblKey04)
        Me.pnlKey.Controls.Add(Me.lblKey03)
        Me.pnlKey.Controls.Add(Me.lblKey02)
        Me.pnlKey.Controls.Add(Me.lblKey01)
        Me.pnlKey.Controls.Add(Me.btnKey14)
        Me.pnlKey.Controls.Add(Me.btnKey04)
        Me.pnlKey.Controls.Add(Me.btnKey03)
        Me.pnlKey.Controls.Add(Me.btnKey05)
        Me.pnlKey.Controls.Add(Me.btnKey15)
        Me.pnlKey.Controls.Add(Me.btnKey00)
        Me.pnlKey.Controls.Add(Me.btnKey02)
        Me.pnlKey.Controls.Add(Me.btnKey08)
        Me.pnlKey.Controls.Add(Me.btnKey09)
        Me.pnlKey.Controls.Add(Me.btnKey16)
        Me.pnlKey.Controls.Add(Me.btnKey07)
        Me.pnlKey.Controls.Add(Me.btnKey06)
        Me.pnlKey.Controls.Add(Me.btnKey12)
        Me.pnlKey.Controls.Add(Me.btnKey01)
        Me.pnlKey.Controls.Add(Me.btnKey13)
        Me.pnlKey.Controls.Add(Me.btnKey11)
        Me.pnlKey.Controls.Add(Me.btnKey10)
        Me.pnlKey.Location = New System.Drawing.Point(4, 48)
        Me.pnlKey.Name = "pnlKey"
        Me.pnlKey.Size = New System.Drawing.Size(212, 224)
        Me.pnlKey.TabIndex = 17
        '
        'lblKey00
        '
        Me.lblKey00.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey00.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey00.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey00.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey00.Location = New System.Drawing.Point(3, 28)
        Me.lblKey00.Name = "lblKey00"
        Me.lblKey00.Size = New System.Drawing.Size(46, 12)
        Me.lblKey00.TabIndex = 37
        Me.lblKey00.Text = "-"
        '
        'lblKey17
        '
        Me.lblKey17.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey17.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey17.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey17.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey17.Location = New System.Drawing.Point(54, 203)
        Me.lblKey17.Name = "lblKey17"
        Me.lblKey17.Size = New System.Drawing.Size(46, 12)
        Me.lblKey17.TabIndex = 34
        Me.lblKey17.Text = "-"
        Me.lblKey17.Visible = False
        '
        'btnKey17
        '
        Me.btnKey17.Enabled = False
        Me.btnKey17.Location = New System.Drawing.Point(52, 175)
        Me.btnKey17.Name = "btnKey17"
        Me.btnKey17.Size = New System.Drawing.Size(52, 44)
        Me.btnKey17.TabIndex = 33
        Me.btnKey17.Text = ">"
        Me.btnKey17.Visible = False
        '
        'lblKey14
        '
        Me.lblKey14.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey14.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey14.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey14.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey14.Location = New System.Drawing.Point(107, 160)
        Me.lblKey14.Name = "lblKey14"
        Me.lblKey14.Size = New System.Drawing.Size(46, 12)
        Me.lblKey14.TabIndex = 32
        Me.lblKey14.Text = "-"
        '
        'lblKey16
        '
        Me.lblKey16.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey16.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey16.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey16.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey16.Location = New System.Drawing.Point(2, 203)
        Me.lblKey16.Name = "lblKey16"
        Me.lblKey16.Size = New System.Drawing.Size(46, 12)
        Me.lblKey16.TabIndex = 31
        Me.lblKey16.Text = "-"
        Me.lblKey16.Visible = False
        '
        'lblKey15
        '
        Me.lblKey15.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey15.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey15.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey15.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey15.Location = New System.Drawing.Point(159, 158)
        Me.lblKey15.Name = "lblKey15"
        Me.lblKey15.Size = New System.Drawing.Size(45, 12)
        Me.lblKey15.TabIndex = 30
        Me.lblKey15.Text = "-"
        '
        'lblKey13
        '
        Me.lblKey13.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey13.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey13.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey13.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey13.Location = New System.Drawing.Point(54, 160)
        Me.lblKey13.Name = "lblKey13"
        Me.lblKey13.Size = New System.Drawing.Size(46, 12)
        Me.lblKey13.TabIndex = 29
        Me.lblKey13.Text = "-"
        '
        'lblKey12
        '
        Me.lblKey12.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey12.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey12.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey12.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey12.Location = New System.Drawing.Point(2, 160)
        Me.lblKey12.Name = "lblKey12"
        Me.lblKey12.Size = New System.Drawing.Size(46, 12)
        Me.lblKey12.TabIndex = 28
        Me.lblKey12.Text = "-"
        '
        'lblKey11
        '
        Me.lblKey11.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey11.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey11.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey11.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey11.Location = New System.Drawing.Point(158, 115)
        Me.lblKey11.Name = "lblKey11"
        Me.lblKey11.Size = New System.Drawing.Size(46, 12)
        Me.lblKey11.TabIndex = 27
        Me.lblKey11.Text = "-"
        '
        'lblKey10
        '
        Me.lblKey10.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey10.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey10.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey10.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey10.Location = New System.Drawing.Point(106, 116)
        Me.lblKey10.Name = "lblKey10"
        Me.lblKey10.Size = New System.Drawing.Size(46, 12)
        Me.lblKey10.TabIndex = 26
        Me.lblKey10.Text = "-"
        '
        'lblKey09
        '
        Me.lblKey09.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey09.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey09.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey09.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey09.Location = New System.Drawing.Point(54, 116)
        Me.lblKey09.Name = "lblKey09"
        Me.lblKey09.Size = New System.Drawing.Size(46, 12)
        Me.lblKey09.TabIndex = 25
        Me.lblKey09.Text = "-"
        '
        'lblKey08
        '
        Me.lblKey08.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey08.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey08.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey08.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey08.Location = New System.Drawing.Point(2, 116)
        Me.lblKey08.Name = "lblKey08"
        Me.lblKey08.Size = New System.Drawing.Size(46, 12)
        Me.lblKey08.TabIndex = 24
        Me.lblKey08.Text = "-"
        '
        'lblKey07
        '
        Me.lblKey07.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey07.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey07.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey07.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey07.Location = New System.Drawing.Point(158, 72)
        Me.lblKey07.Name = "lblKey07"
        Me.lblKey07.Size = New System.Drawing.Size(46, 12)
        Me.lblKey07.TabIndex = 23
        Me.lblKey07.Text = "-"
        '
        'lblKey06
        '
        Me.lblKey06.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey06.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey06.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey06.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey06.Location = New System.Drawing.Point(107, 72)
        Me.lblKey06.Name = "lblKey06"
        Me.lblKey06.Size = New System.Drawing.Size(46, 12)
        Me.lblKey06.TabIndex = 22
        Me.lblKey06.Text = "-"
        '
        'lblKey05
        '
        Me.lblKey05.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey05.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey05.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey05.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey05.Location = New System.Drawing.Point(54, 72)
        Me.lblKey05.Name = "lblKey05"
        Me.lblKey05.Size = New System.Drawing.Size(46, 12)
        Me.lblKey05.TabIndex = 21
        Me.lblKey05.Text = "-"
        '
        'lblKey04
        '
        Me.lblKey04.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey04.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey04.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey04.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey04.Location = New System.Drawing.Point(2, 72)
        Me.lblKey04.Name = "lblKey04"
        Me.lblKey04.Size = New System.Drawing.Size(46, 12)
        Me.lblKey04.TabIndex = 20
        Me.lblKey04.Text = "-"
        '
        'lblKey03
        '
        Me.lblKey03.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey03.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey03.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey03.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey03.Location = New System.Drawing.Point(159, 28)
        Me.lblKey03.Name = "lblKey03"
        Me.lblKey03.Size = New System.Drawing.Size(46, 12)
        Me.lblKey03.TabIndex = 19
        Me.lblKey03.Text = "-"
        '
        'lblKey02
        '
        Me.lblKey02.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey02.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey02.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey02.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey02.Location = New System.Drawing.Point(107, 28)
        Me.lblKey02.Name = "lblKey02"
        Me.lblKey02.Size = New System.Drawing.Size(46, 12)
        Me.lblKey02.TabIndex = 18
        Me.lblKey02.Text = "-"
        '
        'lblKey01
        '
        Me.lblKey01.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lblKey01.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.lblKey01.Font = New System.Drawing.Font("����", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblKey01.ForeColor = System.Drawing.SystemColors.Desktop
        Me.lblKey01.Location = New System.Drawing.Point(54, 28)
        Me.lblKey01.Name = "lblKey01"
        Me.lblKey01.Size = New System.Drawing.Size(46, 12)
        Me.lblKey01.TabIndex = 17
        Me.lblKey01.Text = "-"
        '
        'btnKey14
        '
        Me.btnKey14.Enabled = False
        Me.btnKey14.Location = New System.Drawing.Point(104, 132)
        Me.btnKey14.Name = "btnKey14"
        Me.btnKey14.Size = New System.Drawing.Size(52, 44)
        Me.btnKey14.TabIndex = 14
        Me.btnKey14.Text = "N"
        '
        'btnKey04
        '
        Me.btnKey04.Enabled = False
        Me.btnKey04.Location = New System.Drawing.Point(0, 44)
        Me.btnKey04.Name = "btnKey04"
        Me.btnKey04.Size = New System.Drawing.Size(52, 44)
        Me.btnKey04.TabIndex = 4
        Me.btnKey04.Text = "G"
        '
        'btnKey03
        '
        Me.btnKey03.Enabled = False
        Me.btnKey03.Location = New System.Drawing.Point(156, 0)
        Me.btnKey03.Name = "btnKey03"
        Me.btnKey03.Size = New System.Drawing.Size(52, 44)
        Me.btnKey03.TabIndex = 3
        Me.btnKey03.Text = "F"
        '
        'btnKey05
        '
        Me.btnKey05.Enabled = False
        Me.btnKey05.Location = New System.Drawing.Point(52, 44)
        Me.btnKey05.Name = "btnKey05"
        Me.btnKey05.Size = New System.Drawing.Size(52, 44)
        Me.btnKey05.TabIndex = 5
        Me.btnKey05.Text = "H"
        '
        'btnKey15
        '
        Me.btnKey15.Enabled = False
        Me.btnKey15.Location = New System.Drawing.Point(156, 132)
        Me.btnKey15.Name = "btnKey15"
        Me.btnKey15.Size = New System.Drawing.Size(52, 44)
        Me.btnKey15.TabIndex = 15
        Me.btnKey15.Tag = ""
        Me.btnKey15.Text = "M"
        '
        'btnKey00
        '
        Me.btnKey00.Enabled = False
        Me.btnKey00.Location = New System.Drawing.Point(0, 0)
        Me.btnKey00.Name = "btnKey00"
        Me.btnKey00.Size = New System.Drawing.Size(52, 44)
        Me.btnKey00.TabIndex = 0
        Me.btnKey00.Text = "A"
        '
        'btnKey02
        '
        Me.btnKey02.Enabled = False
        Me.btnKey02.Location = New System.Drawing.Point(104, 0)
        Me.btnKey02.Name = "btnKey02"
        Me.btnKey02.Size = New System.Drawing.Size(52, 44)
        Me.btnKey02.TabIndex = 2
        Me.btnKey02.Text = "D"
        '
        'btnKey08
        '
        Me.btnKey08.Enabled = False
        Me.btnKey08.Location = New System.Drawing.Point(0, 88)
        Me.btnKey08.Name = "btnKey08"
        Me.btnKey08.Size = New System.Drawing.Size(52, 44)
        Me.btnKey08.TabIndex = 8
        Me.btnKey08.Text = "L"
        '
        'btnKey09
        '
        Me.btnKey09.Enabled = False
        Me.btnKey09.Location = New System.Drawing.Point(52, 88)
        Me.btnKey09.Name = "btnKey09"
        Me.btnKey09.Size = New System.Drawing.Size(52, 44)
        Me.btnKey09.TabIndex = 9
        Me.btnKey09.Text = "Z"
        '
        'btnKey16
        '
        Me.btnKey16.Enabled = False
        Me.btnKey16.Location = New System.Drawing.Point(0, 175)
        Me.btnKey16.Name = "btnKey16"
        Me.btnKey16.Size = New System.Drawing.Size(52, 44)
        Me.btnKey16.TabIndex = 16
        Me.btnKey16.Text = "<"
        Me.btnKey16.Visible = False
        '
        'btnKey07
        '
        Me.btnKey07.Enabled = False
        Me.btnKey07.Location = New System.Drawing.Point(156, 44)
        Me.btnKey07.Name = "btnKey07"
        Me.btnKey07.Size = New System.Drawing.Size(52, 44)
        Me.btnKey07.TabIndex = 7
        Me.btnKey07.Text = "K"
        '
        'btnKey06
        '
        Me.btnKey06.Enabled = False
        Me.btnKey06.Location = New System.Drawing.Point(104, 44)
        Me.btnKey06.Name = "btnKey06"
        Me.btnKey06.Size = New System.Drawing.Size(52, 44)
        Me.btnKey06.TabIndex = 6
        Me.btnKey06.Text = "J"
        '
        'btnKey12
        '
        Me.btnKey12.Enabled = False
        Me.btnKey12.Location = New System.Drawing.Point(0, 132)
        Me.btnKey12.Name = "btnKey12"
        Me.btnKey12.Size = New System.Drawing.Size(52, 44)
        Me.btnKey12.TabIndex = 12
        Me.btnKey12.Text = "V"
        '
        'btnKey01
        '
        Me.btnKey01.Enabled = False
        Me.btnKey01.Location = New System.Drawing.Point(52, 0)
        Me.btnKey01.Name = "btnKey01"
        Me.btnKey01.Size = New System.Drawing.Size(52, 44)
        Me.btnKey01.TabIndex = 1
        Me.btnKey01.Text = "S"
        '
        'btnKey13
        '
        Me.btnKey13.Enabled = False
        Me.btnKey13.Location = New System.Drawing.Point(52, 132)
        Me.btnKey13.Name = "btnKey13"
        Me.btnKey13.Size = New System.Drawing.Size(52, 44)
        Me.btnKey13.TabIndex = 13
        Me.btnKey13.Text = "B"
        '
        'btnKey11
        '
        Me.btnKey11.Enabled = False
        Me.btnKey11.Location = New System.Drawing.Point(156, 87)
        Me.btnKey11.Name = "btnKey11"
        Me.btnKey11.Size = New System.Drawing.Size(52, 44)
        Me.btnKey11.TabIndex = 11
        Me.btnKey11.Text = "C"
        '
        'btnKey10
        '
        Me.btnKey10.Enabled = False
        Me.btnKey10.Location = New System.Drawing.Point(104, 88)
        Me.btnKey10.Name = "btnKey10"
        Me.btnKey10.Size = New System.Drawing.Size(52, 44)
        Me.btnKey10.TabIndex = 10
        Me.btnKey10.Text = "X"
        '
        'grpSetting
        '
        Me.grpSetting.Controls.Add(Me.btnKeySave)
        Me.grpSetting.Controls.Add(Me.Label2)
        Me.grpSetting.Controls.Add(Me.Panel3)
        Me.grpSetting.Location = New System.Drawing.Point(536, 69)
        Me.grpSetting.Name = "grpSetting"
        Me.grpSetting.Size = New System.Drawing.Size(224, 286)
        Me.grpSetting.TabIndex = 116
        Me.grpSetting.TabStop = False
        '
        'btnKeySave
        '
        Me.btnKeySave.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnKeySave.Location = New System.Drawing.Point(4, 236)
        Me.btnKeySave.Name = "btnKeySave"
        Me.btnKeySave.Size = New System.Drawing.Size(216, 32)
        Me.btnKeySave.TabIndex = 20
        Me.btnKeySave.Tag = "V"
        Me.btnKeySave.Text = "Ű �� ��(F3)"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label2.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(217, 31)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "����Ű ���� ����"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel3.Controls.Add(Me.spdKeyInfo)
        Me.Panel3.Location = New System.Drawing.Point(4, 47)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 187)
        Me.Panel3.TabIndex = 2
        '
        'spdKeyInfo
        '
        Me.spdKeyInfo.Location = New System.Drawing.Point(0, 0)
        Me.spdKeyInfo.Name = "spdKeyInfo"
        Me.spdKeyInfo.OcxState = CType(resources.GetObject("spdKeyInfo.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdKeyInfo.Size = New System.Drawing.Size(212, 185)
        Me.spdKeyInfo.TabIndex = 2
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("����", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(0, 1)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(288, 43)
        Me.lblTitle.TabIndex = 117
        Me.lblTitle.Text = "Differential Count"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlResult
        '
        Me.pnlResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlResult.Controls.Add(Me.spdDiffCount)
        Me.pnlResult.Controls.Add(Me.lblTitle)
        Me.pnlResult.Location = New System.Drawing.Point(4, 78)
        Me.pnlResult.Name = "pnlResult"
        Me.pnlResult.Size = New System.Drawing.Size(288, 341)
        Me.pnlResult.TabIndex = 113
        '
        'spdDiffCount
        '
        Me.spdDiffCount.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.spdDiffCount.Location = New System.Drawing.Point(0, 47)
        Me.spdDiffCount.Name = "spdDiffCount"
        Me.spdDiffCount.OcxState = CType(resources.GetObject("spdDiffCount.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDiffCount.Size = New System.Drawing.Size(284, 292)
        Me.spdDiffCount.TabIndex = 0
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblWBC)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblCnt)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.cbxMaxCount)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.lblSexAge)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.lblName)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.lblRegNo)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Location = New System.Drawing.Point(5, -1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(448, 71)
        Me.GroupBox3.TabIndex = 118
        Me.GroupBox3.TabStop = False
        '
        'lblWBC
        '
        Me.lblWBC.BackColor = System.Drawing.Color.White
        Me.lblWBC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWBC.Font = New System.Drawing.Font("����", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblWBC.ForeColor = System.Drawing.Color.Black
        Me.lblWBC.Location = New System.Drawing.Point(89, 44)
        Me.lblWBC.Name = "lblWBC"
        Me.lblWBC.Size = New System.Drawing.Size(58, 21)
        Me.lblWBC.TabIndex = 28
        Me.lblWBC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(8, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 20)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "WBC Count"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCnt
        '
        Me.lblCnt.BackColor = System.Drawing.Color.White
        Me.lblCnt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCnt.Font = New System.Drawing.Font("����", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblCnt.ForeColor = System.Drawing.Color.Black
        Me.lblCnt.Location = New System.Drawing.Point(233, 44)
        Me.lblCnt.Name = "lblCnt"
        Me.lblCnt.Size = New System.Drawing.Size(57, 21)
        Me.lblCnt.TabIndex = 26
        Me.lblCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(152, 44)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Total Count"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cbxMaxCount
        '
        Me.cbxMaxCount.BackColor = System.Drawing.Color.White
        Me.cbxMaxCount.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.cbxMaxCount.Items.AddRange(New Object() {"50", "100", "150", "200", "250", "300"})
        Me.cbxMaxCount.Location = New System.Drawing.Point(387, 44)
        Me.cbxMaxCount.Name = "cbxMaxCount"
        Me.cbxMaxCount.Size = New System.Drawing.Size(51, 20)
        Me.cbxMaxCount.TabIndex = 24
        Me.cbxMaxCount.Text = "100"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(294, 44)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 20)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Maxium Count"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Pink
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.Location = New System.Drawing.Point(860, 252)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(96, 20)
        Me.Label11.TabIndex = 22
        Me.Label11.Text = "Maxium Count"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label10.Location = New System.Drawing.Point(8, 38)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(431, 2)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Label10"
        '
        'lblSexAge
        '
        Me.lblSexAge.BackColor = System.Drawing.Color.White
        Me.lblSexAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSexAge.ForeColor = System.Drawing.Color.Black
        Me.lblSexAge.Location = New System.Drawing.Point(377, 13)
        Me.lblSexAge.Name = "lblSexAge"
        Me.lblSexAge.Size = New System.Drawing.Size(60, 21)
        Me.lblSexAge.TabIndex = 19
        Me.lblSexAge.Text = "M/27"
        Me.lblSexAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(312, 13)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(64, 20)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Sex/Age"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblName
        '
        Me.lblName.BackColor = System.Drawing.Color.White
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(233, 13)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(76, 21)
        Me.lblName.TabIndex = 17
        Me.lblName.Text = "���̾�����"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(176, 13)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 20)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "����"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.White
        Me.lblRegNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRegNo.ForeColor = System.Drawing.Color.Black
        Me.lblRegNo.Location = New System.Drawing.Point(89, 13)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(84, 21)
        Me.lblRegNo.TabIndex = 15
        Me.lblRegNo.Text = "1234567"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(8, 13)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 20)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "��Ϲ�ȣ"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMove
        '
        Me.btnMove.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Location = New System.Drawing.Point(295, 78)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(15, 341)
        Me.btnMove.TabIndex = 163
        Me.btnMove.Text = "��"
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnCancel.ColorFillBlend = CBlendItems1
        Me.btnCancel.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnCancel.Corners.All = CType(6, Short)
        Me.btnCancel.Corners.LowerLeft = CType(6, Short)
        Me.btnCancel.Corners.LowerRight = CType(6, Short)
        Me.btnCancel.Corners.UpperLeft = CType(6, Short)
        Me.btnCancel.Corners.UpperRight = CType(6, Short)
        Me.btnCancel.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnCancel.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnCancel.FocalPoints.CenterPtX = 0.5!
        Me.btnCancel.FocalPoints.CenterPtY = 0.08!
        Me.btnCancel.FocalPoints.FocusPtX = 0.0!
        Me.btnCancel.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnCancel.FocusPtTracker = DesignerRectTracker2
        Me.btnCancel.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Image = Nothing
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.ImageIndex = 0
        Me.btnCancel.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnCancel.Location = New System.Drawing.Point(655, 380)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnCancel.SideImage = Nothing
        Me.btnCancel.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnCancel.Size = New System.Drawing.Size(96, 25)
        Me.btnCancel.TabIndex = 201
        Me.btnCancel.Text = "���"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnCancel.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnCancel.Visible = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOK.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnOK.ColorFillBlend = CBlendItems2
        Me.btnOK.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnOK.Corners.All = CType(6, Short)
        Me.btnOK.Corners.LowerLeft = CType(6, Short)
        Me.btnOK.Corners.LowerRight = CType(6, Short)
        Me.btnOK.Corners.UpperLeft = CType(6, Short)
        Me.btnOK.Corners.UpperRight = CType(6, Short)
        Me.btnOK.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnOK.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnOK.FocalPoints.CenterPtX = 0.5!
        Me.btnOK.FocalPoints.CenterPtY = 0.08!
        Me.btnOK.FocalPoints.FocusPtX = 0.0!
        Me.btnOK.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnOK.FocusPtTracker = DesignerRectTracker4
        Me.btnOK.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOK.ForeColor = System.Drawing.Color.White
        Me.btnOK.Image = Nothing
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOK.ImageIndex = 0
        Me.btnOK.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnOK.Location = New System.Drawing.Point(557, 380)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnOK.SideImage = Nothing
        Me.btnOK.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnOK.Size = New System.Drawing.Size(96, 25)
        Me.btnOK.TabIndex = 200
        Me.btnOK.Text = "Ȯ��"
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnOK.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnOK.Visible = False
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
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
        Me.btnClear.FocalPoints.CenterPtY = 0.08!
        Me.btnClear.FocalPoints.FocusPtX = 0.0!
        Me.btnClear.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnClear.FocusPtTracker = DesignerRectTracker6
        Me.btnClear.Font = New System.Drawing.Font("����ü", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnClear.ForeColor = System.Drawing.Color.White
        Me.btnClear.Image = Nothing
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.ImageIndex = 0
        Me.btnClear.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnClear.Location = New System.Drawing.Point(459, 380)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(96, 25)
        Me.btnClear.TabIndex = 199
        Me.btnClear.Text = "�ʱ�ȭ"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        Me.btnClear.Visible = False
        '
        'FGDIFF02
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(763, 424)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnClear)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.pnlResult)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.grpKeypad)
        Me.Controls.Add(Me.grpSetting)
        Me.ForeColor = System.Drawing.Color.Navy
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGDIFF02"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Differential Count ����Է�"
        Me.grpKeypad.ResumeLayout(False)
        Me.pnlKey.ResumeLayout(False)
        Me.grpSetting.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdKeyInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlResult.ResumeLayout(False)
        CType(Me.spdDiffCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Function Display_Result(ByVal r_frm As Windows.Forms.Form, ByVal riLeftPos As Integer, ByVal riTopPos As Integer, _
                                   ByVal rsTestCd As String, ByVal rsSpcCd As String, ByVal rsRegNo As String, ByVal rsPatNm As String, ByVal rsSexAge As String, _
                                   ByVal rsWBCrst As String, ByVal rsBfViewRsts As String, ByRef r_al_RstInfo As ArrayList) As String

        Dim sFn As String = "Function Display_Result"

        m_frm = r_frm
        miLeftPos = riLeftPos
        miTopPos = riTopPos

        msTestCd = rsTestCd
        msSpcCd = rsSpcCd

        msRegNo = rsRegNo
        msPatNm = rsPatNm
        msSexAge = rsSexAge

        msWBCcnt = rsWBCrst
        msBFViewRsts = rsBfViewRsts
        m_al_Rst = r_al_RstInfo

        Try

            Me.ShowDialog(r_frm)

            Return msDiffCmt
        Catch ex As Exception
            MsgBox(msFile + sFn + vbCrLf + ex.Message)

            Return Nothing
        Finally
            Me.Cursor = Windows.Forms.Cursors.Default

        End Try
    End Function
    Private Sub fDisplayKeySetting()
        Dim sKey As String = ""
        Dim sTNm As String = ""

        With Me.spdKeyInfo
            For iRow As Integer = 0 To 7
                For iCol As Integer = 0 To 1
                    .Row = iRow + 1

                    .Col = 1 + iCol * 2
                    sKey = .Text

                    .Col = 2 + iCol * 2
                    sTNm = .Text

                    For Each btnBuf As Control In Me.pnlKey.Controls
                        If btnBuf.Name.StartsWith("btn") And btnBuf.Text.StartsWith(sKey) Then
                            For Each lblBuf As Control In Me.pnlKey.Controls
                                If lblBuf.Name.StartsWith("lbl") And lblBuf.Name.EndsWith(btnBuf.Name.Substring(3)) Then
                                    lblBuf.Text = sTNm

                                    Exit For
                                End If
                            Next

                            Exit For
                        End If
                    Next
                Next
            Next
        End With
    End Sub

    Private Sub fKeyPadSetting()
        Dim sDiffCountOrdNm(16) As String

        For i As Integer = 0 To 15
            sDiffCountOrdNm(i) = ""
        Next

        Dim dt As DataTable = LISAPP.COMM.RstFn.fnGet_ManualDiff(msTestCd, msSpcCd)

        For ix As Integer = 0 To dt.Rows.Count - 1
            Dim sTnmd As String = dt.Rows(ix).Item("tnmd").ToString
            Dim sTestCd As String = dt.Rows(ix).Item("testcd").ToString

            sDiffCountOrdNm(ix) = sTnmd

            With spdDiffCount
                .Row = ix + 1
                .Col = .GetColFromID("tnmd") : .Text = sTnmd
                .Col = .GetColFromID("testcd") : .Text = sTestCd

                If dt.Rows(ix).Item("reqsub").ToString = "1" Then
                    .Col = .GetColFromID("count") : .Text = "0"
                End If

            End With
        Next

        With spdKeyInfo

            For ix As Integer = 0 To 7
                .Row = ix + 1
                .Col = 2 : .Text = sDiffCountOrdNm(ix).ToString
                .Col = 4 : .Text = sDiffCountOrdNm(ix + 8).ToString
            Next
        End With

    End Sub

    Private Function getDiffName(ByVal sStr As String) As String
        Dim iPos As Integer
        Dim sDiffName As String
        With spdKeyInfo
            iPos = .SearchCol(1, 0, .MaxRows, sStr, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
            If iPos > 0 Then
                .Col = 2
                .Row = iPos
                sDiffName = .Text
            Else
                iPos = .SearchCol(3, 0, .MaxRows, sStr, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                .Col = 4
                .Row = iPos
                sDiffName = .Text
            End If
        End With
        getDiffName = sDiffName
    End Function

    Private Sub FGR06_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim vbKey As New System.Windows.Forms.Keys
        Dim strCount As String = ""
        Dim intRow As Integer = 0

        Dim iPos As Integer = 0
        Dim sDiffName As String = ""

        Select Case e.KeyCode
            Case Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, _
                Keys.J, Keys.K, Keys.L, Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M
                With spdKeyInfo
                    iPos = .SearchCol(1, 0, .MaxRows, Chr(e.KeyCode), FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                    If iPos > 0 Then
                        .Col = 2
                        .Row = iPos
                        sDiffName = .Text
                    Else
                        iPos = .SearchCol(3, 0, .MaxRows, Chr(e.KeyCode), FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
                        .Col = 4
                        .Row = iPos
                        sDiffName = .Text
                    End If
                End With
            Case Keys.Add
                sDiffName = getDiffName("+")
            Case Keys.Decimal
                sDiffName = getDiffName(".")
            Case Keys.Divide
                sDiffName = getDiffName("/")
            Case Keys.Multiply
                sDiffName = getDiffName("*")
            Case Keys.Subtract
                sDiffName = getDiffName("-")
            Case Keys.Enter
                'sDiffName = getDiffName("E")
                btnOk_Click(Nothing, Nothing)
            Case Keys.F3
                btnKeySave_Click(Nothing, Nothing)
            Case Keys.Escape
                btnCancel_Click(Nothing, Nothing)
            Case Else
                Exit Sub

        End Select

        If sDiffName = "" Then
            Exit Sub
        End If

        If CInt(Val(Me.lblCnt.Text)) >= CInt(cbxMaxCount.Text) Then
            For i As Integer = 1 To 5
                System.Threading.Thread.Sleep(100)
                Beep()
            Next

            Return
        End If

        With spdDiffCount
            iPos = .SearchCol(.GetColFromID("tnmd"), 0, .MaxRows, sDiffName, FPSpreadADO.SearchFlagsConstants.SearchFlagsNone)
            intRow = iPos
        End With

        Beep()

        With spdDiffCount
            .Col = .GetColFromID("count")
            .Row = intRow
            strCount = .Text

            If strCount.ToString = "" Or IsNothing(strCount) Then strCount = "0"
            strCount = CStr(CInt(strCount) + 1)

            .SetText(.GetColFromID("count"), intRow, strCount)

            'NRBC�� ��쿡�� ��ġ�� �ʵ��� Return
            If sDiffName.ToUpper.IndexOf("NRBC") >= 0 Then
                Return
            End If


            Dim iCountAll As Integer = 0
            For i As Integer = 1 To .MaxRows
                If i <> intRow Then
                    .Col = .GetColFromID("tnmd")
                    .Row = i

                    'NRBC�� �����ϰ� ��ħ
                    If .Text.ToUpper.IndexOf("NRBC") < 0 Then
                        .Col = .GetColFromID("count")
                        .Row = i
                        iCountAll += Convert.ToInt32(Val(.Text))
                    End If
                End If
            Next

            iCountAll += CInt(strCount)

            Me.lblCnt.Text = iCountAll.ToString

            ''-- Real WBC Cnt ���..
            'If Me.lblWBC.Text <> "" Then
            '    Dim dbWBC As Double
            '    Dim cntMax As Integer = Convert.ToInt16(cbxMaxCount.Text)
            '    Dim dbRealCnt As Double

            '    If IsNumeric(lblWBC.Text.ToString) = True Then
            '        dbWBC = Convert.ToDouble(lblWBC.Text)

            '        If Convert.ToInt16(strCount) > 0 Then
            '            dbRealCnt = dbWBC * (Convert.ToInt16(strCount) / 100)
            '            dbRealCnt = Val(Format(dbRealCnt, "##0.0#"))

            '            .SetText(.GetColFromID("percent"), intRow, dbRealCnt)
            '        End If
            '    End If

            'End If
            ''-- Real WBC Cnt ���..


            '-- WBC�� ������� �ʰ�, ī��Ʈ�� ������ ����ϴ� ���
            Dim dbWBC As Double
            Dim cntMax As Integer = Convert.ToInt16(cbxMaxCount.Text)
            Dim dbRealCnt As Double

            If IsNumeric(lblWBC.Text.ToString) = True Then
                dbWBC = Convert.ToDouble(lblWBC.Text)

                If Convert.ToInt16(strCount) > 0 Then
                    dbRealCnt = (Convert.ToInt16(strCount) / Val(Me.lblCnt.Text)) * 100
                    dbRealCnt = Val(Format(dbRealCnt, "##0.0#"))

                    .SetText(.GetColFromID("percent"), intRow, dbRealCnt)
                End If
            End If
            '-- WBC�� ������� �ʰ�, ī��Ʈ�� ������ ����ϴ� ���


        End With

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        '-- 2007.11.02 ssh ���� : ���ڷº��� �����п�.

        Dim sTestCd As String = ""
        Dim sPercent As String = ""
        Dim sCount As String = ""
        Dim sTnmd As String = ""
        Dim bNRBC As Boolean = False

        With spdDiffCount
            Dim iCount As Integer = 0

            If lblCnt.Text.Trim() = "" Then
                iCount = 0
            Else
                iCount = Convert.ToInt32(Val(lblCnt.Text))
            End If

            Dim bMaxCount As Boolean = True

            If iCount < Val(cbxMaxCount.Text) Then
                If MsgBox("������ Max Count " & cbxMaxCount.Text & " ���� ���� ���� " & iCount & " �Դϴ�." & Chr(13) & Chr(10) & " ��� �Է��Ͻðڽ��ϱ�?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Exit Sub
                End If
                bMaxCount = False
            End If

            Dim sWBCVal As String = ""

            For intRow As Integer = 1 To .MaxRows
                .Row = intRow
                .Col = .GetColFromID("count") : sCount = .Text
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("tnmd") : sTnmd = .Text

                If sTnmd.IndexOf("NRBC") >= 0 And sCount <> "" Then
                    Dim strWbcTCd As String = LISAPP.COMM.RstFn.fnGet_ManualDiff_WBC_TestCd(msTestCd, msSpcCd)
                    If strWbcTCd <> "" Then
                        Dim oRst As New ResultInfo_Test

                        oRst.mTestCd = strWbcTCd
                        oRst.mOrgRst = Format((Val(lblWBC.Text) / (100 + Val(sCount)) * 100), "0.00").ToString

                        m_al_Rst.Add(oRst)

                        bNRBC = True
                        sWBCVal = oRst.mOrgRst
                    End If
                End If
            Next

            For iRow As Integer = 1 To .MaxRows

                .Row = iRow
                .Col = .GetColFromID("count") : sCount = .Text
                .Col = .GetColFromID("testcd") : sTestCd = .Text
                .Col = .GetColFromID("tnmd") : sTnmd = .Text

                '-- % ���..
                If sTestCd <> "" Then ' And strTnmd.IndexOf("NRBC") < 0 Then
                    Dim oRst As New ResultInfo_Test

                    oRst.mTestCd = sTestCd
                    If sCount = "" Then
                        oRst.mOrgRst = sCount
                    Else
                        oRst.mOrgRst = Format(Val(sCount) * (100 / Val(cbxMaxCount.Text)), "0")
                    End If
                    m_al_Rst.Add(oRst)

                    .Col = .GetColFromID("percent") : sPercent = .Text
                    Dim sPerTCd = LISAPP.COMM.RstFn.fnGet_ManualDiff_Percent_TclsCd(msTestCd, msSpcCd, sTestCd)
                    If sPerTCd <> "" Then
                        oRst = New ResultInfo_Test

                        oRst.mTestCd = sPerTCd
                        If bNRBC And sCount <> "" Then
                            oRst.mOrgRst = Format(Val(sWBCVal) * (Convert.ToInt16(sCount) / 100), "0.00").ToString
                        Else
                            ''-- WBC�� ����ϴ� ���
                            'oRst.mOrgRst = sPercent
                            ''-- WBC�� ����ϴ� ���

                            '-- WBC�� ������� �ʰ�, ī��Ʈ�� ������ ����ϴ� ���
                            oRst.mOrgRst = Format((Val(sCount) / Val(Me.lblCnt.Text)) * 100, "0.00").ToString()
                            '-- WBC�� ������� �ʰ�, ī��Ʈ�� ������ ����ϴ� ���

                            If Val(oRst.mOrgRst) = 0 Then oRst.mOrgRst = ""

                        End If

                        m_al_Rst.Add(oRst)
                    End If

                End If
            Next
        End With

        'msDiffCmt = "����� �ٽ� ���̰����� Ȯ���߽��ϴ�.!!"
        msDiffCmt = "���̰� �˰� �� ��������Դϴ�." + vbCrLf
        If bNRBC Then
            '   msDiffCmt += "corrected WBC due to nRBC"
        End If
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        msDiffCmt = ""
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub FGR06_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Left = miLeftPos - Me.Width
        Me.Top = miTopPos - Me.Height

        Me.lblTitle.Text = LISAPP.COMM.RstFn.fnGet_ManualDiff_Tnmd(msTestCd, msSpcCd)
        Me.Text = Me.lblTitle.Text + " ��� �Է�"

        fKeyPadSetting()

        Me.lblRegNo.Text = msRegNo
        Me.lblName.Text = msPatNm
        Me.lblSexAge.Text = msSexAge
        Me.lblWBC.Text = msWBCcnt

        btnOK.Focus()
        With spdDiffCount
            For intCol As Integer = 1 To .MaxCols
                If intCol <> .GetColFromID("count") Then
                    .Col = intCol
                    .Row = -1
                    .Lock = True
                Else
                    .Col = .GetColFromID("count")
                    .Row = -1
                    .Lock = False
                End If
            Next

            '-- �������ǥ��
            'Dim strBuf() As String = LISAPP.DA_R.fnGet_ManualDiff_History(msBcNo, mstestcd, msSpcCd).Split("|"c) ' msBFViewRsts.Split("|"c)
            Dim strBuf() As String = msBFViewRsts.Split("|"c)
            Dim strTmtDt As String = ""

            For intIdx As Integer = 0 To strBuf.Length - 1
                If strBuf(intIdx) = "" Then Exit For

                Dim sTestcd As String = strBuf(intIdx).Split("^"c)(0)
                Dim strBfRst As String = strBuf(intIdx).Split("^"c)(1)
                Dim strFnDt As String = strBuf(intIdx).Split("^"c)(2)

                If intIdx = 0 Then strTmtDt = strFnDt

                If strTmtDt <= strFnDt Then
                    For intRow As Integer = 1 To .MaxRows
                        .Row = intRow
                        .Col = .GetColFromID("testcd")
                        If sTestcd = .Text Then
                            .Col = .GetColFromID("bfviewrst") : .Text = strBfRst
                            Exit For
                        End If
                    Next
                End If
            Next

            .Focus()
            .SetActiveCell(1, 1)
        End With

        If Dir(".\SSF\" + Me.Name + "_" + msTestCd + ".ss7") <> "" Then
            Dim blnflag = spdKeyInfo.LoadFromFile(".\SSF\" + Me.Name + "_" + msTestCd + ".ss7")
        End If

        fDisplayKeySetting()
    End Sub

    Private Sub FGR06_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        e.Handled = True
    End Sub

    Private Sub btnKeySave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeySave.Click

        Dim iCnt As Integer
        Dim sTNMD As String = ""
        Dim iMod As Integer

        '< add freety 2005/05/02
        '# User�� ������ �� �ֵ��� ����
        If btnKeySave.Tag.ToString = "V" Then
            btnKeySave.Tag = "S"
            btnKeySave.Text = "Key ���� ����"

            With spdDiffCount
                For iCnt = 1 To .MaxRows
                    .Row = iCnt
                    .Col = .GetColFromID("tnmd") : sTNMD = .Text

                    If sTNMD <> "" Then
                        iMod = iCnt Mod 8

                        If iMod = 0 Then iMod = 8

                        If iCnt > 8 Then
                            Call spdKeyInfo.SetText(4, iMod, sTNMD)
                        Else
                            Call spdKeyInfo.SetText(2, iMod, sTNMD)
                        End If
                    End If
                Next
            End With

            For Each btnBuf As Control In Me.pnlKey.Controls
                For i As Integer = 0 To 17
                    If btnBuf.Name.StartsWith("btn") And btnBuf.Name.EndsWith(Format(i, "00")) Then
                        btnBuf.Enabled = True
                    ElseIf btnBuf.Name.StartsWith("lbl") And btnBuf.Name.EndsWith(Format(i, "00")) Then
                        btnBuf.Text = ""
                    End If
                Next
            Next
        Else
            btnKeySave.Text = "Key ����"
            btnKeySave.Tag = "V"

            For Each btnBuf As Control In Me.pnlKey.Controls
                For i As Integer = 0 To 17
                    If btnBuf.Name.StartsWith("btn") And btnBuf.Name.EndsWith(Format(i, "00")) Then
                        btnBuf.Enabled = False
                    End If
                Next
            Next

            With spdKeyInfo
                If Dir(Application.StartupPath & "\SSF", FileAttribute.Directory) = "" Then
                    MkDir(Application.StartupPath & "\SSF")
                End If

                .SaveToFile(Application.StartupPath & "\SSF\" + Me.Name + "_" + msTestCd + ".ss7", False)
            End With
        End If
        '> add freety 2005/05/02
        Me.GroupBox3.Focus()
    End Sub

    Private Sub btnKey_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKey00.Click, btnKey01.Click, btnKey02.Click, btnKey03.Click, btnKey04.Click, btnKey05.Click, btnKey06.Click, btnKey07.Click, btnKey08.Click, btnKey09.Click, btnKey10.Click, btnKey11.Click, btnKey12.Click, btnKey13.Click, btnKey14.Click, btnKey15.Click, btnKey16.Click, btnKey17.Click
        If Not (Me.spdKeyInfo.ActiveCol = 2 Or Me.spdKeyInfo.ActiveCol = 4) Then Return
        If Me.spdKeyInfo.ActiveRow < 1 Then Return

        Dim btnSel As Button = CType(sender, Button)
        Dim sKey As String = btnSel.Text.Substring(0, 1)
        Dim sTNm As String = ""

        'spdKeyInfo�� Key ǥ��
        With Me.spdKeyInfo
            .Col = .ActiveCol
            .Row = .ActiveRow
            sTNm = .Text

            .Col = .ActiveCol - 1
            .Row = .ActiveRow
            .Text = sKey
        End With

        '���� ������ �׸���� Label�� ǥ��
        For Each lblBuf As Control In Me.pnlKey.Controls
            If lblBuf.Name.StartsWith("lbl") And lblBuf.Name.EndsWith(btnSel.Name.Substring(3)) Then
                lblBuf.Text = sTNm

                Exit For
            End If
        Next
    End Sub

    Private Sub lblKey_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblKey01.MouseHover, lblKey02.MouseHover, lblKey03.MouseHover, lblKey04.MouseHover, lblKey05.MouseHover, lblKey06.MouseHover, lblKey07.MouseHover, lblKey08.MouseHover, lblKey09.MouseHover, lblKey10.MouseHover, lblKey11.MouseHover, lblKey12.MouseHover, lblKey13.MouseHover, lblKey14.MouseHover, lblKey15.MouseHover, lblKey16.MouseHover

        Dim tooltip As ToolTip = New ToolTip

        tooltip.SetToolTip(CType(sender, Label), CType(sender, Label).Text)
        tooltip.Active = True
    End Sub

    Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim iRow As Integer

        lblCnt.Text = ""

        With spdDiffCount
            For iRow = 1 To .MaxRows
                .SetText(.GetColFromID("count"), iRow, "")
                .SetText(.GetColFromID("percent"), iRow, "")
            Next

        End With

    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click

        If btnMove.Text = "��" Then
            pnlResult.Visible = False

            btnMove.Left = 5
            grpKeypad.Left = btnMove.Left + btnMove.Width + 2
            grpSetting.Left = grpKeypad.Left + grpKeypad.Width + 2

            btnMove.Text = "��"
        Else
            pnlResult.Visible = True

            btnMove.Left = 295
            grpKeypad.Left = btnMove.Left + btnMove.Width + 2
            grpSetting.Left = grpKeypad.Left + grpKeypad.Width + 2


            btnMove.Text = "��"
        End If

    End Sub
End Class
