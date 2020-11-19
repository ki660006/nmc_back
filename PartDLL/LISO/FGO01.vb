'>>> 처방입력

Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.SVar
Imports common.commlogin.login

Imports LISAPP.APP_DB
Imports LISAPP.APP_O
Imports LISAPP.APP_O.O01

Public Class FGO01
    Inherits System.Windows.Forms.Form
    Private Const msFile As String = "File : FGO01.vb, Class : O01" & vbTab

    Private msXML As String = "\XML\SaveAs_FGO01"

    Friend WithEvents btnSaveAs As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents lblOrdList As System.Windows.Forms.Label
    Friend WithEvents lstSaveList As System.Windows.Forms.ListBox
    Friend WithEvents sfdSCd As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnCollTk As System.Windows.Forms.Button
    Friend WithEvents txtFkOcs As System.Windows.Forms.TextBox
    Friend WithEvents btnTKReg As CButtonLib.CButton
    Friend WithEvents btnReg As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnDeptHlp As System.Windows.Forms.Button
    Friend WithEvents btnDoctorHlp As System.Windows.Forms.Button
    Friend WithEvents btnDiagHlp3 As System.Windows.Forms.Button
    Friend WithEvents btnDiagHlp2 As System.Windows.Forms.Button
    Friend WithEvents btnDiagHlp1 As System.Windows.Forms.Button
    Friend WithEvents btnDiagHlp0 As System.Windows.Forms.Button
    Friend WithEvents btnDrugHlp As System.Windows.Forms.Button
    Friend WithEvents grpCom As System.Windows.Forms.GroupBox
    Friend WithEvents btnComCdHlp As System.Windows.Forms.Button
    Friend WithEvents chkEmr As System.Windows.Forms.CheckBox
    Friend WithEvents cboComGbn As System.Windows.Forms.ComboBox
    Friend WithEvents lblComCd As System.Windows.Forms.Label
    Friend WithEvents txtComCd As System.Windows.Forms.TextBox
    Friend WithEvents dtpHopeDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpTest As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestCdHlp As System.Windows.Forms.Button
    Friend WithEvents btnSpcCdHlp As System.Windows.Forms.Button
    Friend WithEvents cboTOrdSlip As System.Windows.Forms.ComboBox
    Friend WithEvents lblSpcNm As System.Windows.Forms.Label
    Friend WithEvents lblSpcCd As System.Windows.Forms.Label
    Friend WithEvents txtSpcCd As System.Windows.Forms.TextBox
    Friend WithEvents lblTestItem As System.Windows.Forms.Label
    Friend WithEvents dtpEntDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboRegno As System.Windows.Forms.ComboBox
    Friend WithEvents txtRegno As System.Windows.Forms.TextBox
    Friend WithEvents txtDonCnt As System.Windows.Forms.TextBox
    Friend WithEvents txtHeight As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtTestCd As System.Windows.Forms.TextBox

    'Private mJubSuGbn As New Item

    Private Sub sbDisplaySaveList()

        lstSaveList.Items.Clear()

        If Dir(Application.StartupPath + msXML, FileAttribute.Directory) = "" Then
            MkDir(Application.StartupPath + msXML & "\")
        End If

        Dim strFile As String = Dir(Application.StartupPath + msXML + "\*.xml")

        Do While strFile <> ""
            lstSaveList.Items.Add(strFile)
            strFile = Dir()
        Loop

    End Sub

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
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOrdDt As System.Windows.Forms.Label
    Friend WithEvents lblJubsuGbn As System.Windows.Forms.Label
    Friend WithEvents lblIdno As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblDoctorCd As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblPatNm As System.Windows.Forms.Label
    Friend WithEvents lblRegNo As System.Windows.Forms.Label
    Friend WithEvents lblDeptCd As System.Windows.Forms.Label
    Friend WithEvents lblDiagCd As System.Windows.Forms.Label
    Friend WithEvents cboJubsuGbn As System.Windows.Forms.ComboBox
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents lblTel2 As System.Windows.Forms.Label
    Friend WithEvents lblTel1 As System.Windows.Forms.Label
    Friend WithEvents lblSex As System.Windows.Forms.Label
    Friend WithEvents lblAge As System.Windows.Forms.Label
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblHeight As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDeptCd As System.Windows.Forms.TextBox
    Friend WithEvents txtTel2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTel1 As System.Windows.Forms.TextBox
    Friend WithEvents lblDeptNm As System.Windows.Forms.Label
    Friend WithEvents lblWardNm As System.Windows.Forms.Label
    Friend WithEvents btnWardHlp As System.Windows.Forms.Button
    Friend WithEvents txtWardCd As System.Windows.Forms.TextBox
    Friend WithEvents txtRoomCd As System.Windows.Forms.TextBox
    Friend WithEvents lblSRNm As System.Windows.Forms.Label
    Friend WithEvents btnRoomHlp As System.Windows.Forms.Button
    Friend WithEvents dtpOrdDt As System.Windows.Forms.DateTimePicker
    Friend WithEvents tbcPatInfo As System.Windows.Forms.TabControl
    Friend WithEvents tbpPatInfo0 As System.Windows.Forms.TabPage
    Friend WithEvents tbpPatInfo1 As System.Windows.Forms.TabPage
    Friend WithEvents txtIdnoR As System.Windows.Forms.TextBox
    Friend WithEvents txtIdnoL As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents spdOrderList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblDAge As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblBCNOcnt As System.Windows.Forms.Label
    Friend WithEvents txtPrtBCNo As System.Windows.Forms.TextBox
    Friend WithEvents lblUserNm As System.Windows.Forms.Label
    Friend WithEvents lblUserId As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents spdComList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents btnOPDtHlp As System.Windows.Forms.Button
    Friend WithEvents txtOPDt As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents pnlOcsOrder As System.Windows.Forms.Panel
    Friend WithEvents chkTestOrder As System.Windows.Forms.CheckBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents lblPrtBCNOcnt As System.Windows.Forms.Label
    Friend WithEvents lblResDt As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtResDt As System.Windows.Forms.TextBox
    Friend WithEvents lblPatientGbn As System.Windows.Forms.Label
    Friend WithEvents lblDoctorNm As System.Windows.Forms.Label
    Friend WithEvents txtDoctorCd As System.Windows.Forms.TextBox
    Friend WithEvents txtDiagCd1 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiagCd0 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiagCd2 As System.Windows.Forms.TextBox
    Friend WithEvents txtDiagCd3 As System.Windows.Forms.TextBox
    Friend WithEvents rdoPatientGbn1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPatientGbn0 As System.Windows.Forms.RadioButton
    Friend WithEvents pnlPatientGbn As System.Windows.Forms.Panel
    Friend WithEvents lblBirthDay As System.Windows.Forms.Label
    Friend WithEvents txtBedno As System.Windows.Forms.TextBox
    Friend WithEvents pnlWardInfo As System.Windows.Forms.Panel
    Friend WithEvents lblDiagNm1 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNm0 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNm2 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNm3 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNmE0 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNmE1 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNmE2 As System.Windows.Forms.Label
    Friend WithEvents lblDiagNmE3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grpDrug As System.Windows.Forms.GroupBox
    Friend WithEvents spdDrugList As AxFPSpreadADO.AxfpSpread
    Friend WithEvents txtDrugCd As System.Windows.Forms.TextBox
    Friend WithEvents lblDrugCd As System.Windows.Forms.Label
    Friend WithEvents lblDonCnt As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGO01))
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
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
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.txtDonCnt = New System.Windows.Forms.TextBox
        Me.txtRegno = New System.Windows.Forms.TextBox
        Me.cboRegno = New System.Windows.Forms.ComboBox
        Me.lblPatientGbn = New System.Windows.Forms.Label
        Me.pnlPatientGbn = New System.Windows.Forms.Panel
        Me.rdoPatientGbn1 = New System.Windows.Forms.RadioButton
        Me.rdoPatientGbn0 = New System.Windows.Forms.RadioButton
        Me.Label31 = New System.Windows.Forms.Label
        Me.lblOrdDt = New System.Windows.Forms.Label
        Me.lblDonCnt = New System.Windows.Forms.Label
        Me.lblRegNo = New System.Windows.Forms.Label
        Me.dtpOrdDt = New System.Windows.Forms.DateTimePicker
        Me.txtPatNm = New System.Windows.Forms.TextBox
        Me.lblBirthDay = New System.Windows.Forms.Label
        Me.lblPatNm = New System.Windows.Forms.Label
        Me.lblAge = New System.Windows.Forms.Label
        Me.lblIdno = New System.Windows.Forms.Label
        Me.cboJubsuGbn = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblJubsuGbn = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.pnlOcsOrder = New System.Windows.Forms.Panel
        Me.chkTestOrder = New System.Windows.Forms.CheckBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtIdnoR = New System.Windows.Forms.TextBox
        Me.lblDAge = New System.Windows.Forms.Label
        Me.Label29 = New System.Windows.Forms.Label
        Me.lblSex = New System.Windows.Forms.Label
        Me.txtIdnoL = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblUserNm = New System.Windows.Forms.Label
        Me.lblUserId = New System.Windows.Forms.Label
        Me.txtDeptCd = New System.Windows.Forms.TextBox
        Me.lblDeptCd = New System.Windows.Forms.Label
        Me.lblDoctorCd = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.spdOrderList = New AxFPSpreadADO.AxfpSpread
        Me.tbcPatInfo = New System.Windows.Forms.TabControl
        Me.tbpPatInfo0 = New System.Windows.Forms.TabPage
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.txtHeight = New System.Windows.Forms.TextBox
        Me.dtpHopeDt = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnDiagHlp3 = New System.Windows.Forms.Button
        Me.btnDiagHlp2 = New System.Windows.Forms.Button
        Me.btnDiagHlp1 = New System.Windows.Forms.Button
        Me.btnDiagHlp0 = New System.Windows.Forms.Button
        Me.btnDoctorHlp = New System.Windows.Forms.Button
        Me.btnDeptHlp = New System.Windows.Forms.Button
        Me.lblDiagNmE3 = New System.Windows.Forms.Label
        Me.lblDiagNmE2 = New System.Windows.Forms.Label
        Me.lblDiagNmE1 = New System.Windows.Forms.Label
        Me.lblDiagNmE0 = New System.Windows.Forms.Label
        Me.lblDiagNm1 = New System.Windows.Forms.Label
        Me.txtDiagCd1 = New System.Windows.Forms.TextBox
        Me.lblDoctorNm = New System.Windows.Forms.Label
        Me.txtDoctorCd = New System.Windows.Forms.TextBox
        Me.Label37 = New System.Windows.Forms.Label
        Me.txtResDt = New System.Windows.Forms.TextBox
        Me.lblResDt = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblWeight = New System.Windows.Forms.Label
        Me.lblHeight = New System.Windows.Forms.Label
        Me.lblDiagNm0 = New System.Windows.Forms.Label
        Me.txtDiagCd0 = New System.Windows.Forms.TextBox
        Me.lblTel2 = New System.Windows.Forms.Label
        Me.txtTel2 = New System.Windows.Forms.TextBox
        Me.lblTel1 = New System.Windows.Forms.Label
        Me.txtTel1 = New System.Windows.Forms.TextBox
        Me.lblDeptNm = New System.Windows.Forms.Label
        Me.lblDiagCd = New System.Windows.Forms.Label
        Me.lblDiagNm2 = New System.Windows.Forms.Label
        Me.txtDiagCd2 = New System.Windows.Forms.TextBox
        Me.txtDiagCd3 = New System.Windows.Forms.TextBox
        Me.lblDiagNm3 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.tbpPatInfo1 = New System.Windows.Forms.TabPage
        Me.Label34 = New System.Windows.Forms.Label
        Me.btnOPDtHlp = New System.Windows.Forms.Button
        Me.txtOPDt = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.pnlWardInfo = New System.Windows.Forms.Panel
        Me.dtpEntDt = New System.Windows.Forms.DateTimePicker
        Me.txtRoomCd = New System.Windows.Forms.TextBox
        Me.lblSRNm = New System.Windows.Forms.Label
        Me.btnRoomHlp = New System.Windows.Forms.Button
        Me.txtBedno = New System.Windows.Forms.TextBox
        Me.lblWardNm = New System.Windows.Forms.Label
        Me.btnWardHlp = New System.Windows.Forms.Button
        Me.txtWardCd = New System.Windows.Forms.TextBox
        Me.Label33 = New System.Windows.Forms.Label
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.btnReg = New CButtonLib.CButton
        Me.btnTKReg = New CButtonLib.CButton
        Me.txtFkOcs = New System.Windows.Forms.TextBox
        Me.btnCollTk = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnDelete = New System.Windows.Forms.Button
        Me.lstSaveList = New System.Windows.Forms.ListBox
        Me.lblPrtBCNOcnt = New System.Windows.Forms.Label
        Me.lblOrdList = New System.Windows.Forms.Label
        Me.btnSaveAs = New System.Windows.Forms.Button
        Me.txtPrtBCNo = New System.Windows.Forms.TextBox
        Me.lblBCNOcnt = New System.Windows.Forms.Label
        Me.Panel6 = New System.Windows.Forms.Panel
        Me.spdComList = New AxFPSpreadADO.AxfpSpread
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.spdDrugList = New AxFPSpreadADO.AxfpSpread
        Me.grpDrug = New System.Windows.Forms.GroupBox
        Me.btnDrugHlp = New System.Windows.Forms.Button
        Me.txtDrugCd = New System.Windows.Forms.TextBox
        Me.lblDrugCd = New System.Windows.Forms.Label
        Me.sfdSCd = New System.Windows.Forms.SaveFileDialog
        Me.grpCom = New System.Windows.Forms.GroupBox
        Me.cboComGbn = New System.Windows.Forms.ComboBox
        Me.btnComCdHlp = New System.Windows.Forms.Button
        Me.chkEmr = New System.Windows.Forms.CheckBox
        Me.lblComCd = New System.Windows.Forms.Label
        Me.txtComCd = New System.Windows.Forms.TextBox
        Me.grpTest = New System.Windows.Forms.GroupBox
        Me.btnTestCdHlp = New System.Windows.Forms.Button
        Me.btnSpcCdHlp = New System.Windows.Forms.Button
        Me.cboTOrdSlip = New System.Windows.Forms.ComboBox
        Me.lblSpcNm = New System.Windows.Forms.Label
        Me.lblSpcCd = New System.Windows.Forms.Label
        Me.txtSpcCd = New System.Windows.Forms.TextBox
        Me.lblTestItem = New System.Windows.Forms.Label
        Me.txtTestCd = New System.Windows.Forms.TextBox
        Me.GroupBox7.SuspendLayout()
        Me.pnlPatientGbn.SuspendLayout()
        Me.pnlOcsOrder.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.spdOrderList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbcPatInfo.SuspendLayout()
        Me.tbpPatInfo0.SuspendLayout()
        Me.tbpPatInfo1.SuspendLayout()
        Me.pnlWardInfo.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.spdDrugList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDrug.SuspendLayout()
        Me.grpCom.SuspendLayout()
        Me.grpTest.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GroupBox7.Controls.Add(Me.txtDonCnt)
        Me.GroupBox7.Controls.Add(Me.txtRegno)
        Me.GroupBox7.Controls.Add(Me.cboRegno)
        Me.GroupBox7.Controls.Add(Me.lblPatientGbn)
        Me.GroupBox7.Controls.Add(Me.pnlPatientGbn)
        Me.GroupBox7.Controls.Add(Me.lblOrdDt)
        Me.GroupBox7.Controls.Add(Me.lblDonCnt)
        Me.GroupBox7.Controls.Add(Me.lblRegNo)
        Me.GroupBox7.Controls.Add(Me.dtpOrdDt)
        Me.GroupBox7.Controls.Add(Me.txtPatNm)
        Me.GroupBox7.Controls.Add(Me.lblBirthDay)
        Me.GroupBox7.Controls.Add(Me.lblPatNm)
        Me.GroupBox7.Controls.Add(Me.lblAge)
        Me.GroupBox7.Controls.Add(Me.lblIdno)
        Me.GroupBox7.Controls.Add(Me.cboJubsuGbn)
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Controls.Add(Me.lblJubsuGbn)
        Me.GroupBox7.Controls.Add(Me.Label6)
        Me.GroupBox7.Controls.Add(Me.pnlOcsOrder)
        Me.GroupBox7.Controls.Add(Me.Label23)
        Me.GroupBox7.Controls.Add(Me.txtIdnoR)
        Me.GroupBox7.Controls.Add(Me.lblDAge)
        Me.GroupBox7.Controls.Add(Me.Label29)
        Me.GroupBox7.Controls.Add(Me.lblSex)
        Me.GroupBox7.Controls.Add(Me.txtIdnoL)
        Me.GroupBox7.Controls.Add(Me.Label5)
        Me.GroupBox7.Location = New System.Drawing.Point(4, 4)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(363, 223)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        '
        'txtDonCnt
        '
        Me.txtDonCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDonCnt.Location = New System.Drawing.Point(304, 36)
        Me.txtDonCnt.Name = "txtDonCnt"
        Me.txtDonCnt.Size = New System.Drawing.Size(55, 21)
        Me.txtDonCnt.TabIndex = 161
        '
        'txtRegno
        '
        Me.txtRegno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegno.Location = New System.Drawing.Point(131, 81)
        Me.txtRegno.Name = "txtRegno"
        Me.txtRegno.Size = New System.Drawing.Size(82, 21)
        Me.txtRegno.TabIndex = 7
        '
        'cboRegno
        '
        Me.cboRegno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRegno.FormattingEnabled = True
        Me.cboRegno.Items.AddRange(New Object() {"0", "L", "R", "T"})
        Me.cboRegno.Location = New System.Drawing.Point(89, 81)
        Me.cboRegno.Name = "cboRegno"
        Me.cboRegno.Size = New System.Drawing.Size(40, 20)
        Me.cboRegno.TabIndex = 6
        '
        'lblPatientGbn
        '
        Me.lblPatientGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPatientGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatientGbn.ForeColor = System.Drawing.Color.White
        Me.lblPatientGbn.Location = New System.Drawing.Point(4, 35)
        Me.lblPatientGbn.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPatientGbn.Name = "lblPatientGbn"
        Me.lblPatientGbn.Size = New System.Drawing.Size(84, 22)
        Me.lblPatientGbn.TabIndex = 0
        Me.lblPatientGbn.Text = "진료구분"
        Me.lblPatientGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlPatientGbn
        '
        Me.pnlPatientGbn.BackColor = System.Drawing.Color.Thistle
        Me.pnlPatientGbn.Controls.Add(Me.rdoPatientGbn1)
        Me.pnlPatientGbn.Controls.Add(Me.rdoPatientGbn0)
        Me.pnlPatientGbn.Controls.Add(Me.Label31)
        Me.pnlPatientGbn.ForeColor = System.Drawing.Color.Indigo
        Me.pnlPatientGbn.Location = New System.Drawing.Point(89, 36)
        Me.pnlPatientGbn.Name = "pnlPatientGbn"
        Me.pnlPatientGbn.Size = New System.Drawing.Size(125, 22)
        Me.pnlPatientGbn.TabIndex = 159
        Me.pnlPatientGbn.TabStop = True
        '
        'rdoPatientGbn1
        '
        Me.rdoPatientGbn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.rdoPatientGbn1.Location = New System.Drawing.Point(60, 1)
        Me.rdoPatientGbn1.Name = "rdoPatientGbn1"
        Me.rdoPatientGbn1.Size = New System.Drawing.Size(48, 20)
        Me.rdoPatientGbn1.TabIndex = 3
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
        Me.rdoPatientGbn0.TabIndex = 2
        Me.rdoPatientGbn0.TabStop = True
        Me.rdoPatientGbn0.Tag = "1"
        Me.rdoPatientGbn0.Text = "외래"
        '
        'Label31
        '
        Me.Label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label31.Location = New System.Drawing.Point(0, 0)
        Me.Label31.Margin = New System.Windows.Forms.Padding(1)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(125, 22)
        Me.Label31.TabIndex = 2
        '
        'lblOrdDt
        '
        Me.lblOrdDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblOrdDt.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblOrdDt.ForeColor = System.Drawing.Color.White
        Me.lblOrdDt.Location = New System.Drawing.Point(4, 12)
        Me.lblOrdDt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblOrdDt.Name = "lblOrdDt"
        Me.lblOrdDt.Size = New System.Drawing.Size(84, 22)
        Me.lblOrdDt.TabIndex = 11
        Me.lblOrdDt.Text = "처방일자"
        Me.lblOrdDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDonCnt
        '
        Me.lblDonCnt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDonCnt.ForeColor = System.Drawing.Color.Black
        Me.lblDonCnt.Location = New System.Drawing.Point(219, 36)
        Me.lblDonCnt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDonCnt.Name = "lblDonCnt"
        Me.lblDonCnt.Size = New System.Drawing.Size(84, 22)
        Me.lblDonCnt.TabIndex = 144
        Me.lblDonCnt.Text = "헌혈횟수"
        Me.lblDonCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRegNo
        '
        Me.lblRegNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblRegNo.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblRegNo.ForeColor = System.Drawing.Color.White
        Me.lblRegNo.Location = New System.Drawing.Point(4, 81)
        Me.lblRegNo.Margin = New System.Windows.Forms.Padding(0)
        Me.lblRegNo.Name = "lblRegNo"
        Me.lblRegNo.Size = New System.Drawing.Size(84, 22)
        Me.lblRegNo.TabIndex = 3
        Me.lblRegNo.Text = "등록번호"
        Me.lblRegNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpOrdDt
        '
        Me.dtpOrdDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpOrdDt.Location = New System.Drawing.Point(89, 13)
        Me.dtpOrdDt.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpOrdDt.Name = "dtpOrdDt"
        Me.dtpOrdDt.Size = New System.Drawing.Size(124, 21)
        Me.dtpOrdDt.TabIndex = 0
        Me.dtpOrdDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'txtPatNm
        '
        Me.txtPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatNm.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.txtPatNm.Location = New System.Drawing.Point(89, 104)
        Me.txtPatNm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPatNm.MaxLength = 20
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.Size = New System.Drawing.Size(125, 21)
        Me.txtPatNm.TabIndex = 8
        Me.txtPatNm.Text = "지성"
        '
        'lblBirthDay
        '
        Me.lblBirthDay.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblBirthDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBirthDay.ForeColor = System.Drawing.Color.White
        Me.lblBirthDay.Location = New System.Drawing.Point(218, 61)
        Me.lblBirthDay.Name = "lblBirthDay"
        Me.lblBirthDay.Size = New System.Drawing.Size(84, 20)
        Me.lblBirthDay.TabIndex = 155
        Me.lblBirthDay.Text = "BirthDay"
        Me.lblBirthDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPatNm
        '
        Me.lblPatNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblPatNm.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblPatNm.ForeColor = System.Drawing.Color.White
        Me.lblPatNm.Location = New System.Drawing.Point(4, 104)
        Me.lblPatNm.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPatNm.Name = "lblPatNm"
        Me.lblPatNm.Size = New System.Drawing.Size(84, 22)
        Me.lblPatNm.TabIndex = 5
        Me.lblPatNm.Text = "성명"
        Me.lblPatNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblAge
        '
        Me.lblAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAge.Location = New System.Drawing.Point(89, 174)
        Me.lblAge.Name = "lblAge"
        Me.lblAge.Size = New System.Drawing.Size(125, 22)
        Me.lblAge.TabIndex = 14
        Me.lblAge.Text = "26"
        Me.lblAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblIdno
        '
        Me.lblIdno.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblIdno.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblIdno.ForeColor = System.Drawing.Color.White
        Me.lblIdno.Location = New System.Drawing.Point(4, 127)
        Me.lblIdno.Margin = New System.Windows.Forms.Padding(0)
        Me.lblIdno.Name = "lblIdno"
        Me.lblIdno.Size = New System.Drawing.Size(84, 22)
        Me.lblIdno.TabIndex = 7
        Me.lblIdno.Text = "주민등록번호"
        Me.lblIdno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboJubsuGbn
        '
        Me.cboJubsuGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJubsuGbn.Items.AddRange(New Object() {"[00] 일반", "[09] 수혈", "[10] 헌혈", "[01] 신검", "[02] 종검", "[03] 위탁", "[04] 실습", "[05] TEST", "[06] VIP", "[07] QI", "[09] QC"})
        Me.cboJubsuGbn.Location = New System.Drawing.Point(89, 59)
        Me.cboJubsuGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboJubsuGbn.Name = "cboJubsuGbn"
        Me.cboJubsuGbn.Size = New System.Drawing.Size(124, 20)
        Me.cboJubsuGbn.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 22)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "성별"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblJubsuGbn
        '
        Me.lblJubsuGbn.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblJubsuGbn.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblJubsuGbn.ForeColor = System.Drawing.Color.White
        Me.lblJubsuGbn.Location = New System.Drawing.Point(4, 58)
        Me.lblJubsuGbn.Margin = New System.Windows.Forms.Padding(0)
        Me.lblJubsuGbn.Name = "lblJubsuGbn"
        Me.lblJubsuGbn.Size = New System.Drawing.Size(84, 22)
        Me.lblJubsuGbn.TabIndex = 1
        Me.lblJubsuGbn.Text = "접수구분"
        Me.lblJubsuGbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(4, 173)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 22)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "나이"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlOcsOrder
        '
        Me.pnlOcsOrder.BackColor = System.Drawing.Color.YellowGreen
        Me.pnlOcsOrder.Controls.Add(Me.chkTestOrder)
        Me.pnlOcsOrder.Controls.Add(Me.Label30)
        Me.pnlOcsOrder.ForeColor = System.Drawing.Color.DarkGreen
        Me.pnlOcsOrder.Location = New System.Drawing.Point(219, 13)
        Me.pnlOcsOrder.Name = "pnlOcsOrder"
        Me.pnlOcsOrder.Size = New System.Drawing.Size(100, 22)
        Me.pnlOcsOrder.TabIndex = 160
        '
        'chkTestOrder
        '
        Me.chkTestOrder.Checked = True
        Me.chkTestOrder.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTestOrder.Location = New System.Drawing.Point(11, 1)
        Me.chkTestOrder.Name = "chkTestOrder"
        Me.chkTestOrder.Size = New System.Drawing.Size(87, 20)
        Me.chkTestOrder.TabIndex = 1
        Me.chkTestOrder.TabStop = False
        Me.chkTestOrder.Text = "Only 처방"
        '
        'Label30
        '
        Me.Label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label30.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label30.Location = New System.Drawing.Point(0, 0)
        Me.Label30.Margin = New System.Windows.Forms.Padding(0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(100, 22)
        Me.Label30.TabIndex = 0
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label23.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.White
        Me.Label23.Location = New System.Drawing.Point(4, 196)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(84, 22)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "일령"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIdnoR
        '
        Me.txtIdnoR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdnoR.Location = New System.Drawing.Point(166, 128)
        Me.txtIdnoR.Margin = New System.Windows.Forms.Padding(1)
        Me.txtIdnoR.MaxLength = 7
        Me.txtIdnoR.Name = "txtIdnoR"
        Me.txtIdnoR.Size = New System.Drawing.Size(48, 21)
        Me.txtIdnoR.TabIndex = 10
        Me.txtIdnoR.Text = "1234567"
        '
        'lblDAge
        '
        Me.lblDAge.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDAge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDAge.Location = New System.Drawing.Point(89, 196)
        Me.lblDAge.Name = "lblDAge"
        Me.lblDAge.Size = New System.Drawing.Size(100, 22)
        Me.lblDAge.TabIndex = 16
        Me.lblDAge.Text = "26"
        Me.lblDAge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label29.Location = New System.Drawing.Point(194, 201)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(17, 12)
        Me.Label29.TabIndex = 17
        Me.Label29.Text = "일"
        '
        'lblSex
        '
        Me.lblSex.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSex.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSex.Location = New System.Drawing.Point(89, 151)
        Me.lblSex.Name = "lblSex"
        Me.lblSex.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblSex.Size = New System.Drawing.Size(125, 22)
        Me.lblSex.TabIndex = 12
        Me.lblSex.Text = "남"
        Me.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtIdnoL
        '
        Me.txtIdnoL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdnoL.Location = New System.Drawing.Point(89, 128)
        Me.txtIdnoL.Margin = New System.Windows.Forms.Padding(1)
        Me.txtIdnoL.MaxLength = 6
        Me.txtIdnoL.Name = "txtIdnoL"
        Me.txtIdnoL.Size = New System.Drawing.Size(48, 21)
        Me.txtIdnoL.TabIndex = 9
        Me.txtIdnoL.Text = "770405"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(144, 132)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(11, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "~"
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
        'txtDeptCd
        '
        Me.txtDeptCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDeptCd.Location = New System.Drawing.Point(87, 8)
        Me.txtDeptCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtDeptCd.MaxLength = 10
        Me.txtDeptCd.Name = "txtDeptCd"
        Me.txtDeptCd.Size = New System.Drawing.Size(76, 21)
        Me.txtDeptCd.TabIndex = 0
        Me.txtDeptCd.Text = "D0001"
        '
        'lblDeptCd
        '
        Me.lblDeptCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDeptCd.ForeColor = System.Drawing.Color.Black
        Me.lblDeptCd.Location = New System.Drawing.Point(2, 8)
        Me.lblDeptCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDeptCd.Name = "lblDeptCd"
        Me.lblDeptCd.Size = New System.Drawing.Size(84, 22)
        Me.lblDeptCd.TabIndex = 109
        Me.lblDeptCd.Text = "진료과"
        Me.lblDeptCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDoctorCd
        '
        Me.lblDoctorCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDoctorCd.ForeColor = System.Drawing.Color.Black
        Me.lblDoctorCd.Location = New System.Drawing.Point(2, 31)
        Me.lblDoctorCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDoctorCd.Name = "lblDoctorCd"
        Me.lblDoctorCd.Size = New System.Drawing.Size(84, 22)
        Me.lblDoctorCd.TabIndex = 102
        Me.lblDoctorCd.Text = "의뢰의사"
        Me.lblDoctorCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.spdOrderList)
        Me.Panel2.Location = New System.Drawing.Point(6, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(496, 205)
        Me.Panel2.TabIndex = 61
        '
        'spdOrderList
        '
        Me.spdOrderList.DataSource = Nothing
        Me.spdOrderList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdOrderList.Location = New System.Drawing.Point(0, 0)
        Me.spdOrderList.Margin = New System.Windows.Forms.Padding(1)
        Me.spdOrderList.Name = "spdOrderList"
        Me.spdOrderList.OcxState = CType(resources.GetObject("spdOrderList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdOrderList.Size = New System.Drawing.Size(492, 201)
        Me.spdOrderList.TabIndex = 1
        '
        'tbcPatInfo
        '
        Me.tbcPatInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tbcPatInfo.Controls.Add(Me.tbpPatInfo0)
        Me.tbcPatInfo.Controls.Add(Me.tbpPatInfo1)
        Me.tbcPatInfo.HotTrack = True
        Me.tbcPatInfo.ItemSize = New System.Drawing.Size(48, 20)
        Me.tbcPatInfo.Location = New System.Drawing.Point(4, 231)
        Me.tbcPatInfo.Name = "tbcPatInfo"
        Me.tbcPatInfo.SelectedIndex = 0
        Me.tbcPatInfo.Size = New System.Drawing.Size(364, 399)
        Me.tbcPatInfo.TabIndex = 0
        Me.tbcPatInfo.TabStop = False
        '
        'tbpPatInfo0
        '
        Me.tbpPatInfo0.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbpPatInfo0.Controls.Add(Me.Label2)
        Me.tbpPatInfo0.Controls.Add(Me.txtWeight)
        Me.tbpPatInfo0.Controls.Add(Me.txtHeight)
        Me.tbpPatInfo0.Controls.Add(Me.dtpHopeDt)
        Me.tbpPatInfo0.Controls.Add(Me.Label1)
        Me.tbpPatInfo0.Controls.Add(Me.btnDiagHlp3)
        Me.tbpPatInfo0.Controls.Add(Me.btnDiagHlp2)
        Me.tbpPatInfo0.Controls.Add(Me.btnDiagHlp1)
        Me.tbpPatInfo0.Controls.Add(Me.btnDiagHlp0)
        Me.tbpPatInfo0.Controls.Add(Me.btnDoctorHlp)
        Me.tbpPatInfo0.Controls.Add(Me.btnDeptHlp)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNmE3)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNmE2)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNmE1)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNmE0)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNm1)
        Me.tbpPatInfo0.Controls.Add(Me.txtDiagCd1)
        Me.tbpPatInfo0.Controls.Add(Me.lblDoctorNm)
        Me.tbpPatInfo0.Controls.Add(Me.txtDoctorCd)
        Me.tbpPatInfo0.Controls.Add(Me.Label37)
        Me.tbpPatInfo0.Controls.Add(Me.txtResDt)
        Me.tbpPatInfo0.Controls.Add(Me.lblResDt)
        Me.tbpPatInfo0.Controls.Add(Me.Label22)
        Me.tbpPatInfo0.Controls.Add(Me.lblWeight)
        Me.tbpPatInfo0.Controls.Add(Me.lblHeight)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNm0)
        Me.tbpPatInfo0.Controls.Add(Me.txtDiagCd0)
        Me.tbpPatInfo0.Controls.Add(Me.lblTel2)
        Me.tbpPatInfo0.Controls.Add(Me.txtTel2)
        Me.tbpPatInfo0.Controls.Add(Me.lblTel1)
        Me.tbpPatInfo0.Controls.Add(Me.txtTel1)
        Me.tbpPatInfo0.Controls.Add(Me.lblDeptNm)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagCd)
        Me.tbpPatInfo0.Controls.Add(Me.txtDeptCd)
        Me.tbpPatInfo0.Controls.Add(Me.lblDeptCd)
        Me.tbpPatInfo0.Controls.Add(Me.lblDoctorCd)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNm2)
        Me.tbpPatInfo0.Controls.Add(Me.txtDiagCd2)
        Me.tbpPatInfo0.Controls.Add(Me.txtDiagCd3)
        Me.tbpPatInfo0.Controls.Add(Me.lblDiagNm3)
        Me.tbpPatInfo0.Controls.Add(Me.Label21)
        Me.tbpPatInfo0.Location = New System.Drawing.Point(4, 24)
        Me.tbpPatInfo0.Margin = New System.Windows.Forms.Padding(1)
        Me.tbpPatInfo0.Name = "tbpPatInfo0"
        Me.tbpPatInfo0.Size = New System.Drawing.Size(356, 371)
        Me.tbpPatInfo0.TabIndex = 0
        Me.tbpPatInfo0.Text = "일반내용"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.Location = New System.Drawing.Point(327, 198)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 12)
        Me.Label2.TabIndex = 231
        Me.Label2.Text = "kg"
        '
        'txtWeight
        '
        Me.txtWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWeight.Location = New System.Drawing.Point(264, 191)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(61, 21)
        Me.txtWeight.TabIndex = 230
        '
        'txtHeight
        '
        Me.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtHeight.Location = New System.Drawing.Point(87, 190)
        Me.txtHeight.Name = "txtHeight"
        Me.txtHeight.Size = New System.Drawing.Size(65, 21)
        Me.txtHeight.TabIndex = 229
        '
        'dtpHopeDt
        '
        Me.dtpHopeDt.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        Me.dtpHopeDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHopeDt.Location = New System.Drawing.Point(87, 252)
        Me.dtpHopeDt.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpHopeDt.Name = "dtpHopeDt"
        Me.dtpHopeDt.Size = New System.Drawing.Size(157, 21)
        Me.dtpHopeDt.TabIndex = 228
        Me.dtpHopeDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(3, 251)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 22)
        Me.Label1.TabIndex = 227
        Me.Label1.Text = "검사희망일"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDiagHlp3
        '
        Me.btnDiagHlp3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDiagHlp3.Image = CType(resources.GetObject("btnDiagHlp3.Image"), System.Drawing.Image)
        Me.btnDiagHlp3.Location = New System.Drawing.Point(165, 124)
        Me.btnDiagHlp3.Name = "btnDiagHlp3"
        Me.btnDiagHlp3.Size = New System.Drawing.Size(21, 21)
        Me.btnDiagHlp3.TabIndex = 226
        Me.btnDiagHlp3.Tag = "3"
        Me.btnDiagHlp3.UseVisualStyleBackColor = True
        '
        'btnDiagHlp2
        '
        Me.btnDiagHlp2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDiagHlp2.Image = CType(resources.GetObject("btnDiagHlp2.Image"), System.Drawing.Image)
        Me.btnDiagHlp2.Location = New System.Drawing.Point(165, 101)
        Me.btnDiagHlp2.Name = "btnDiagHlp2"
        Me.btnDiagHlp2.Size = New System.Drawing.Size(21, 21)
        Me.btnDiagHlp2.TabIndex = 225
        Me.btnDiagHlp2.Tag = "2"
        Me.btnDiagHlp2.UseVisualStyleBackColor = True
        '
        'btnDiagHlp1
        '
        Me.btnDiagHlp1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDiagHlp1.Image = CType(resources.GetObject("btnDiagHlp1.Image"), System.Drawing.Image)
        Me.btnDiagHlp1.Location = New System.Drawing.Point(165, 78)
        Me.btnDiagHlp1.Name = "btnDiagHlp1"
        Me.btnDiagHlp1.Size = New System.Drawing.Size(21, 21)
        Me.btnDiagHlp1.TabIndex = 224
        Me.btnDiagHlp1.Tag = "1"
        Me.btnDiagHlp1.UseVisualStyleBackColor = True
        '
        'btnDiagHlp0
        '
        Me.btnDiagHlp0.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDiagHlp0.Image = CType(resources.GetObject("btnDiagHlp0.Image"), System.Drawing.Image)
        Me.btnDiagHlp0.Location = New System.Drawing.Point(165, 54)
        Me.btnDiagHlp0.Name = "btnDiagHlp0"
        Me.btnDiagHlp0.Size = New System.Drawing.Size(21, 21)
        Me.btnDiagHlp0.TabIndex = 223
        Me.btnDiagHlp0.Tag = "0"
        Me.btnDiagHlp0.UseVisualStyleBackColor = True
        '
        'btnDoctorHlp
        '
        Me.btnDoctorHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDoctorHlp.Image = CType(resources.GetObject("btnDoctorHlp.Image"), System.Drawing.Image)
        Me.btnDoctorHlp.Location = New System.Drawing.Point(165, 31)
        Me.btnDoctorHlp.Name = "btnDoctorHlp"
        Me.btnDoctorHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnDoctorHlp.TabIndex = 222
        Me.btnDoctorHlp.UseVisualStyleBackColor = True
        '
        'btnDeptHlp
        '
        Me.btnDeptHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDeptHlp.Image = CType(resources.GetObject("btnDeptHlp.Image"), System.Drawing.Image)
        Me.btnDeptHlp.Location = New System.Drawing.Point(165, 8)
        Me.btnDeptHlp.Name = "btnDeptHlp"
        Me.btnDeptHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnDeptHlp.TabIndex = 221
        Me.btnDeptHlp.UseVisualStyleBackColor = True
        '
        'lblDiagNmE3
        '
        Me.lblDiagNmE3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNmE3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNmE3.Location = New System.Drawing.Point(260, 124)
        Me.lblDiagNmE3.Name = "lblDiagNmE3"
        Me.lblDiagNmE3.Size = New System.Drawing.Size(89, 20)
        Me.lblDiagNmE3.TabIndex = 193
        Me.lblDiagNmE3.Text = "진단명"
        Me.lblDiagNmE3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiagNmE3.Visible = False
        '
        'lblDiagNmE2
        '
        Me.lblDiagNmE2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNmE2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNmE2.Location = New System.Drawing.Point(260, 101)
        Me.lblDiagNmE2.Name = "lblDiagNmE2"
        Me.lblDiagNmE2.Size = New System.Drawing.Size(89, 20)
        Me.lblDiagNmE2.TabIndex = 192
        Me.lblDiagNmE2.Text = "진단명"
        Me.lblDiagNmE2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiagNmE2.Visible = False
        '
        'lblDiagNmE1
        '
        Me.lblDiagNmE1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNmE1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNmE1.Location = New System.Drawing.Point(260, 78)
        Me.lblDiagNmE1.Name = "lblDiagNmE1"
        Me.lblDiagNmE1.Size = New System.Drawing.Size(89, 20)
        Me.lblDiagNmE1.TabIndex = 191
        Me.lblDiagNmE1.Text = "진단명"
        Me.lblDiagNmE1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiagNmE1.Visible = False
        '
        'lblDiagNmE0
        '
        Me.lblDiagNmE0.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNmE0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNmE0.Location = New System.Drawing.Point(260, 55)
        Me.lblDiagNmE0.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDiagNmE0.Name = "lblDiagNmE0"
        Me.lblDiagNmE0.Size = New System.Drawing.Size(89, 20)
        Me.lblDiagNmE0.TabIndex = 190
        Me.lblDiagNmE0.Text = "진단명"
        Me.lblDiagNmE0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDiagNmE0.Visible = False
        '
        'lblDiagNm1
        '
        Me.lblDiagNm1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNm1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm1.Location = New System.Drawing.Point(187, 78)
        Me.lblDiagNm1.Name = "lblDiagNm1"
        Me.lblDiagNm1.Size = New System.Drawing.Size(158, 20)
        Me.lblDiagNm1.TabIndex = 189
        Me.lblDiagNm1.Text = "진단명"
        Me.lblDiagNm1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDiagCd1
        '
        Me.txtDiagCd1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiagCd1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiagCd1.Location = New System.Drawing.Point(87, 78)
        Me.txtDiagCd1.MaxLength = 7
        Me.txtDiagCd1.Name = "txtDiagCd1"
        Me.txtDiagCd1.Size = New System.Drawing.Size(76, 21)
        Me.txtDiagCd1.TabIndex = 3
        Me.txtDiagCd1.Tag = "1"
        Me.txtDiagCd1.Text = "DI001"
        '
        'lblDoctorNm
        '
        Me.lblDoctorNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDoctorNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDoctorNm.Location = New System.Drawing.Point(187, 31)
        Me.lblDoctorNm.Margin = New System.Windows.Forms.Padding(11)
        Me.lblDoctorNm.Name = "lblDoctorNm"
        Me.lblDoctorNm.Size = New System.Drawing.Size(162, 22)
        Me.lblDoctorNm.TabIndex = 183
        Me.lblDoctorNm.Text = "내과"
        Me.lblDoctorNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDoctorCd
        '
        Me.txtDoctorCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctorCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDoctorCd.Location = New System.Drawing.Point(87, 31)
        Me.txtDoctorCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtDoctorCd.MaxLength = 5
        Me.txtDoctorCd.Name = "txtDoctorCd"
        Me.txtDoctorCd.Size = New System.Drawing.Size(76, 21)
        Me.txtDoctorCd.TabIndex = 1
        '
        'Label37
        '
        Me.Label37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label37.Location = New System.Drawing.Point(0, 217)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(420, 2)
        Me.Label37.TabIndex = 180
        '
        'txtResDt
        '
        Me.txtResDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtResDt.Location = New System.Drawing.Point(87, 227)
        Me.txtResDt.Margin = New System.Windows.Forms.Padding(1)
        Me.txtResDt.MaxLength = 10
        Me.txtResDt.Name = "txtResDt"
        Me.txtResDt.Size = New System.Drawing.Size(117, 21)
        Me.txtResDt.TabIndex = 10
        '
        'lblResDt
        '
        Me.lblResDt.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblResDt.ForeColor = System.Drawing.Color.Black
        Me.lblResDt.Location = New System.Drawing.Point(2, 227)
        Me.lblResDt.Name = "lblResDt"
        Me.lblResDt.Size = New System.Drawing.Size(84, 22)
        Me.lblResDt.TabIndex = 179
        Me.lblResDt.Text = "예약일"
        Me.lblResDt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label22.Location = New System.Drawing.Point(383, 194)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(18, 12)
        Me.Label22.TabIndex = 145
        Me.Label22.Text = "kg"
        '
        'lblWeight
        '
        Me.lblWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblWeight.ForeColor = System.Drawing.Color.Black
        Me.lblWeight.Location = New System.Drawing.Point(178, 190)
        Me.lblWeight.Margin = New System.Windows.Forms.Padding(0)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(85, 22)
        Me.lblWeight.TabIndex = 143
        Me.lblWeight.Text = "체중"
        Me.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHeight
        '
        Me.lblHeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblHeight.ForeColor = System.Drawing.Color.Black
        Me.lblHeight.Location = New System.Drawing.Point(2, 189)
        Me.lblHeight.Margin = New System.Windows.Forms.Padding(0)
        Me.lblHeight.Name = "lblHeight"
        Me.lblHeight.Size = New System.Drawing.Size(84, 22)
        Me.lblHeight.TabIndex = 142
        Me.lblHeight.Text = "키"
        Me.lblHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDiagNm0
        '
        Me.lblDiagNm0.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNm0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm0.Location = New System.Drawing.Point(187, 55)
        Me.lblDiagNm0.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDiagNm0.Name = "lblDiagNm0"
        Me.lblDiagNm0.Size = New System.Drawing.Size(158, 20)
        Me.lblDiagNm0.TabIndex = 134
        Me.lblDiagNm0.Text = "진단명"
        Me.lblDiagNm0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDiagCd0
        '
        Me.txtDiagCd0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiagCd0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiagCd0.Location = New System.Drawing.Point(87, 54)
        Me.txtDiagCd0.Margin = New System.Windows.Forms.Padding(1)
        Me.txtDiagCd0.MaxLength = 7
        Me.txtDiagCd0.Name = "txtDiagCd0"
        Me.txtDiagCd0.Size = New System.Drawing.Size(76, 21)
        Me.txtDiagCd0.TabIndex = 2
        Me.txtDiagCd0.Tag = "0"
        Me.txtDiagCd0.Text = "DI001"
        '
        'lblTel2
        '
        Me.lblTel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTel2.ForeColor = System.Drawing.Color.Black
        Me.lblTel2.Location = New System.Drawing.Point(179, 159)
        Me.lblTel2.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTel2.Name = "lblTel2"
        Me.lblTel2.Size = New System.Drawing.Size(84, 22)
        Me.lblTel2.TabIndex = 130
        Me.lblTel2.Text = "연락처2"
        Me.lblTel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTel2
        '
        Me.txtTel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel2.Location = New System.Drawing.Point(264, 159)
        Me.txtTel2.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTel2.MaxLength = 15
        Me.txtTel2.Name = "txtTel2"
        Me.txtTel2.Size = New System.Drawing.Size(84, 21)
        Me.txtTel2.TabIndex = 7
        '
        'lblTel1
        '
        Me.lblTel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblTel1.ForeColor = System.Drawing.Color.Black
        Me.lblTel1.Location = New System.Drawing.Point(2, 158)
        Me.lblTel1.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTel1.Name = "lblTel1"
        Me.lblTel1.Size = New System.Drawing.Size(84, 22)
        Me.lblTel1.TabIndex = 128
        Me.lblTel1.Text = "연락처1"
        Me.lblTel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTel1
        '
        Me.txtTel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel1.Location = New System.Drawing.Point(87, 159)
        Me.txtTel1.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTel1.MaxLength = 15
        Me.txtTel1.Name = "txtTel1"
        Me.txtTel1.Size = New System.Drawing.Size(88, 21)
        Me.txtTel1.TabIndex = 6
        Me.txtTel1.Text = "031-1234-1234"
        '
        'lblDeptNm
        '
        Me.lblDeptNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDeptNm.Location = New System.Drawing.Point(187, 8)
        Me.lblDeptNm.Margin = New System.Windows.Forms.Padding(1)
        Me.lblDeptNm.Name = "lblDeptNm"
        Me.lblDeptNm.Size = New System.Drawing.Size(162, 22)
        Me.lblDeptNm.TabIndex = 117
        Me.lblDeptNm.Text = "내과"
        Me.lblDeptNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDiagCd
        '
        Me.lblDiagCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDiagCd.ForeColor = System.Drawing.Color.Black
        Me.lblDiagCd.Location = New System.Drawing.Point(2, 54)
        Me.lblDiagCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDiagCd.Name = "lblDiagCd"
        Me.lblDiagCd.Size = New System.Drawing.Size(84, 22)
        Me.lblDiagCd.TabIndex = 114
        Me.lblDiagCd.Text = "진단명"
        Me.lblDiagCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDiagNm2
        '
        Me.lblDiagNm2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNm2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm2.Location = New System.Drawing.Point(187, 101)
        Me.lblDiagNm2.Name = "lblDiagNm2"
        Me.lblDiagNm2.Size = New System.Drawing.Size(158, 20)
        Me.lblDiagNm2.TabIndex = 189
        Me.lblDiagNm2.Text = "진단명"
        Me.lblDiagNm2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDiagCd2
        '
        Me.txtDiagCd2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiagCd2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiagCd2.Location = New System.Drawing.Point(87, 101)
        Me.txtDiagCd2.MaxLength = 7
        Me.txtDiagCd2.Name = "txtDiagCd2"
        Me.txtDiagCd2.Size = New System.Drawing.Size(76, 21)
        Me.txtDiagCd2.TabIndex = 4
        Me.txtDiagCd2.Tag = "2"
        Me.txtDiagCd2.Text = "DI001"
        '
        'txtDiagCd3
        '
        Me.txtDiagCd3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDiagCd3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDiagCd3.Location = New System.Drawing.Point(87, 124)
        Me.txtDiagCd3.MaxLength = 7
        Me.txtDiagCd3.Name = "txtDiagCd3"
        Me.txtDiagCd3.Size = New System.Drawing.Size(76, 21)
        Me.txtDiagCd3.TabIndex = 5
        Me.txtDiagCd3.Tag = "3"
        Me.txtDiagCd3.Text = "DI001"
        '
        'lblDiagNm3
        '
        Me.lblDiagNm3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblDiagNm3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblDiagNm3.Location = New System.Drawing.Point(187, 124)
        Me.lblDiagNm3.Name = "lblDiagNm3"
        Me.lblDiagNm3.Size = New System.Drawing.Size(158, 20)
        Me.lblDiagNm3.TabIndex = 189
        Me.lblDiagNm3.Text = "진단명"
        Me.lblDiagNm3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label21.Location = New System.Drawing.Point(154, 198)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(23, 12)
        Me.Label21.TabIndex = 144
        Me.Label21.Text = "cm"
        '
        'tbpPatInfo1
        '
        Me.tbpPatInfo1.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.tbpPatInfo1.Controls.Add(Me.Label34)
        Me.tbpPatInfo1.Controls.Add(Me.btnOPDtHlp)
        Me.tbpPatInfo1.Controls.Add(Me.txtOPDt)
        Me.tbpPatInfo1.Controls.Add(Me.Label25)
        Me.tbpPatInfo1.Controls.Add(Me.pnlWardInfo)
        Me.tbpPatInfo1.Location = New System.Drawing.Point(4, 24)
        Me.tbpPatInfo1.Name = "tbpPatInfo1"
        Me.tbpPatInfo1.Size = New System.Drawing.Size(356, 371)
        Me.tbpPatInfo1.TabIndex = 1
        Me.tbpPatInfo1.Text = "병동내용"
        '
        'Label34
        '
        Me.Label34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label34.Location = New System.Drawing.Point(0, 120)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(308, 2)
        Me.Label34.TabIndex = 177
        '
        'btnOPDtHlp
        '
        Me.btnOPDtHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnOPDtHlp.Font = New System.Drawing.Font("굴림", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOPDtHlp.Location = New System.Drawing.Point(136, 128)
        Me.btnOPDtHlp.Name = "btnOPDtHlp"
        Me.btnOPDtHlp.Size = New System.Drawing.Size(28, 22)
        Me.btnOPDtHlp.TabIndex = 175
        Me.btnOPDtHlp.TabStop = False
        Me.btnOPDtHlp.Text = "▼"
        Me.btnOPDtHlp.Visible = False
        '
        'txtOPDt
        '
        Me.txtOPDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOPDt.Location = New System.Drawing.Point(68, 128)
        Me.txtOPDt.MaxLength = 10
        Me.txtOPDt.Name = "txtOPDt"
        Me.txtOPDt.Size = New System.Drawing.Size(68, 21)
        Me.txtOPDt.TabIndex = 4
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label25.ForeColor = System.Drawing.Color.White
        Me.Label25.Location = New System.Drawing.Point(0, 128)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(68, 22)
        Me.Label25.TabIndex = 176
        Me.Label25.Text = "수술예정일"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlWardInfo
        '
        Me.pnlWardInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.pnlWardInfo.Controls.Add(Me.dtpEntDt)
        Me.pnlWardInfo.Controls.Add(Me.txtRoomCd)
        Me.pnlWardInfo.Controls.Add(Me.lblSRNm)
        Me.pnlWardInfo.Controls.Add(Me.btnRoomHlp)
        Me.pnlWardInfo.Controls.Add(Me.txtBedno)
        Me.pnlWardInfo.Controls.Add(Me.lblWardNm)
        Me.pnlWardInfo.Controls.Add(Me.btnWardHlp)
        Me.pnlWardInfo.Controls.Add(Me.txtWardCd)
        Me.pnlWardInfo.Controls.Add(Me.Label33)
        Me.pnlWardInfo.Controls.Add(Me.Label32)
        Me.pnlWardInfo.Controls.Add(Me.Label10)
        Me.pnlWardInfo.Controls.Add(Me.Label8)
        Me.pnlWardInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlWardInfo.Name = "pnlWardInfo"
        Me.pnlWardInfo.Size = New System.Drawing.Size(312, 116)
        Me.pnlWardInfo.TabIndex = 178
        '
        'dtpEntDt
        '
        Me.dtpEntDt.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpEntDt.Location = New System.Drawing.Point(68, 92)
        Me.dtpEntDt.Margin = New System.Windows.Forms.Padding(1)
        Me.dtpEntDt.Name = "dtpEntDt"
        Me.dtpEntDt.Size = New System.Drawing.Size(108, 21)
        Me.dtpEntDt.TabIndex = 165
        Me.dtpEntDt.Value = New Date(2003, 4, 28, 13, 20, 23, 312)
        '
        'txtRoomCd
        '
        Me.txtRoomCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoomCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtRoomCd.Location = New System.Drawing.Point(68, 36)
        Me.txtRoomCd.MaxLength = 10
        Me.txtRoomCd.Name = "txtRoomCd"
        Me.txtRoomCd.Size = New System.Drawing.Size(68, 21)
        Me.txtRoomCd.TabIndex = 1
        Me.txtRoomCd.Text = "SR101"
        '
        'lblSRNm
        '
        Me.lblSRNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSRNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSRNm.Location = New System.Drawing.Point(164, 36)
        Me.lblSRNm.Name = "lblSRNm"
        Me.lblSRNm.Size = New System.Drawing.Size(144, 22)
        Me.lblSRNm.TabIndex = 164
        Me.lblSRNm.Text = "101"
        Me.lblSRNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblSRNm.Visible = False
        '
        'btnRoomHlp
        '
        Me.btnRoomHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnRoomHlp.Location = New System.Drawing.Point(136, 36)
        Me.btnRoomHlp.Name = "btnRoomHlp"
        Me.btnRoomHlp.Size = New System.Drawing.Size(28, 22)
        Me.btnRoomHlp.TabIndex = 163
        Me.btnRoomHlp.TabStop = False
        Me.btnRoomHlp.Text = "..."
        '
        'txtBedno
        '
        Me.txtBedno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBedno.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtBedno.Location = New System.Drawing.Point(68, 64)
        Me.txtBedno.MaxLength = 5
        Me.txtBedno.Name = "txtBedno"
        Me.txtBedno.Size = New System.Drawing.Size(68, 21)
        Me.txtBedno.TabIndex = 2
        Me.txtBedno.Text = "SB001"
        '
        'lblWardNm
        '
        Me.lblWardNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblWardNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblWardNm.Location = New System.Drawing.Point(164, 8)
        Me.lblWardNm.Name = "lblWardNm"
        Me.lblWardNm.Size = New System.Drawing.Size(144, 22)
        Me.lblWardNm.TabIndex = 164
        Me.lblWardNm.Text = "1W"
        Me.lblWardNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblWardNm.Visible = False
        '
        'btnWardHlp
        '
        Me.btnWardHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnWardHlp.Location = New System.Drawing.Point(136, 8)
        Me.btnWardHlp.Name = "btnWardHlp"
        Me.btnWardHlp.Size = New System.Drawing.Size(28, 22)
        Me.btnWardHlp.TabIndex = 1
        Me.btnWardHlp.TabStop = False
        Me.btnWardHlp.Text = "..."
        '
        'txtWardCd
        '
        Me.txtWardCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWardCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtWardCd.Location = New System.Drawing.Point(68, 8)
        Me.txtWardCd.MaxLength = 10
        Me.txtWardCd.Name = "txtWardCd"
        Me.txtWardCd.Size = New System.Drawing.Size(68, 21)
        Me.txtWardCd.TabIndex = 0
        Me.txtWardCd.Text = "W0001"
        '
        'Label33
        '
        Me.Label33.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label33.ForeColor = System.Drawing.Color.White
        Me.Label33.Location = New System.Drawing.Point(0, 92)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(68, 22)
        Me.Label33.TabIndex = 159
        Me.Label33.Text = "입원일자"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label32
        '
        Me.Label32.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label32.ForeColor = System.Drawing.Color.White
        Me.Label32.Location = New System.Drawing.Point(0, 64)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(68, 22)
        Me.Label32.TabIndex = 157
        Me.Label32.Text = "병상"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.Location = New System.Drawing.Point(0, 36)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 22)
        Me.Label10.TabIndex = 154
        Me.Label10.Text = "병실"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.Location = New System.Drawing.Point(0, 8)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 22)
        Me.Label8.TabIndex = 153
        Me.Label8.Text = "병동"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel4.Controls.Add(Me.btnExit)
        Me.Panel4.Controls.Add(Me.btnClear)
        Me.Panel4.Controls.Add(Me.btnReg)
        Me.Panel4.Controls.Add(Me.btnTKReg)
        Me.Panel4.Controls.Add(Me.txtFkOcs)
        Me.Panel4.Controls.Add(Me.btnCollTk)
        Me.Panel4.Controls.Add(Me.lblUserId)
        Me.Panel4.Controls.Add(Me.lblUserNm)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 635)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1145, 34)
        Me.Panel4.TabIndex = 7
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4672897!
        Me.btnExit.FocalPoints.CenterPtY = 0.4!
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
        Me.btnExit.Location = New System.Drawing.Point(1029, 4)
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
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.btnClear.FocalPoints.CenterPtX = 0.4766355!
        Me.btnClear.FocalPoints.CenterPtY = 0.12!
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
        Me.btnClear.Location = New System.Drawing.Point(921, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 188
        Me.btnClear.Text = "화면정리 (F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnReg
        '
        Me.btnReg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker5.IsActive = False
        DesignerRectTracker5.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker5.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.CenterPtTracker = DesignerRectTracker5
        CBlendItems3.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems3.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnReg.ColorFillBlend = CBlendItems3
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
        DesignerRectTracker6.IsActive = False
        DesignerRectTracker6.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker6.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnReg.FocusPtTracker = DesignerRectTracker6
        Me.btnReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnReg.ForeColor = System.Drawing.Color.White
        Me.btnReg.Image = Nothing
        Me.btnReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.ImageIndex = 0
        Me.btnReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnReg.Location = New System.Drawing.Point(813, 4)
        Me.btnReg.Name = "btnReg"
        Me.btnReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnReg.SideImage = Nothing
        Me.btnReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnReg.Size = New System.Drawing.Size(107, 25)
        Me.btnReg.TabIndex = 187
        Me.btnReg.Text = "처  방"
        Me.btnReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnTKReg
        '
        DesignerRectTracker7.IsActive = False
        DesignerRectTracker7.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker7.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTKReg.CenterPtTracker = DesignerRectTracker7
        CBlendItems4.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems4.iPoint = New Single() {0.0!, 0.1510574!, 0.3444109!, 0.9305136!, 1.0!}
        Me.btnTKReg.ColorFillBlend = CBlendItems4
        Me.btnTKReg.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnTKReg.Corners.All = CType(6, Short)
        Me.btnTKReg.Corners.LowerLeft = CType(6, Short)
        Me.btnTKReg.Corners.LowerRight = CType(6, Short)
        Me.btnTKReg.Corners.UpperLeft = CType(6, Short)
        Me.btnTKReg.Corners.UpperRight = CType(6, Short)
        Me.btnTKReg.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnTKReg.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnTKReg.FocalPoints.CenterPtX = 0.5!
        Me.btnTKReg.FocalPoints.CenterPtY = 0.0!
        Me.btnTKReg.FocalPoints.FocusPtX = 0.0!
        Me.btnTKReg.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker8.IsActive = False
        DesignerRectTracker8.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker8.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnTKReg.FocusPtTracker = DesignerRectTracker8
        Me.btnTKReg.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnTKReg.ForeColor = System.Drawing.Color.White
        Me.btnTKReg.Image = Nothing
        Me.btnTKReg.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTKReg.ImageIndex = 0
        Me.btnTKReg.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnTKReg.Location = New System.Drawing.Point(415, 5)
        Me.btnTKReg.Name = "btnTKReg"
        Me.btnTKReg.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnTKReg.SideImage = Nothing
        Me.btnTKReg.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTKReg.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnTKReg.Size = New System.Drawing.Size(107, 25)
        Me.btnTKReg.TabIndex = 186
        Me.btnTKReg.Text = "접수까지 처리"
        Me.btnTKReg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnTKReg.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnTKReg.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'txtFkOcs
        '
        Me.txtFkOcs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFkOcs.Location = New System.Drawing.Point(302, 9)
        Me.txtFkOcs.MaxLength = 20
        Me.txtFkOcs.Name = "txtFkOcs"
        Me.txtFkOcs.Size = New System.Drawing.Size(110, 21)
        Me.txtFkOcs.TabIndex = 179
        Me.txtFkOcs.Visible = False
        '
        'btnCollTk
        '
        Me.btnCollTk.Location = New System.Drawing.Point(220, 7)
        Me.btnCollTk.Name = "btnCollTk"
        Me.btnCollTk.Size = New System.Drawing.Size(76, 24)
        Me.btnCollTk.TabIndex = 157
        Me.btnCollTk.Text = "btnTest"
        Me.btnCollTk.UseVisualStyleBackColor = True
        Me.btnCollTk.Visible = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.btnDelete)
        Me.GroupBox3.Controls.Add(Me.lstSaveList)
        Me.GroupBox3.Controls.Add(Me.lblPrtBCNOcnt)
        Me.GroupBox3.Controls.Add(Me.lblOrdList)
        Me.GroupBox3.Controls.Add(Me.btnSaveAs)
        Me.GroupBox3.Controls.Add(Me.txtPrtBCNo)
        Me.GroupBox3.Controls.Add(Me.lblBCNOcnt)
        Me.GroupBox3.Location = New System.Drawing.Point(884, 203)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(254, 425)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDelete.Location = New System.Drawing.Point(187, 138)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.TabIndex = 127
        Me.btnDelete.Text = "Delete"
        '
        'lstSaveList
        '
        Me.lstSaveList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSaveList.ItemHeight = 12
        Me.lstSaveList.Location = New System.Drawing.Point(3, 163)
        Me.lstSaveList.Name = "lstSaveList"
        Me.lstSaveList.Size = New System.Drawing.Size(248, 256)
        Me.lstSaveList.TabIndex = 129
        '
        'lblPrtBCNOcnt
        '
        Me.lblPrtBCNOcnt.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrtBCNOcnt.BackColor = System.Drawing.Color.Wheat
        Me.lblPrtBCNOcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPrtBCNOcnt.Location = New System.Drawing.Point(196, 15)
        Me.lblPrtBCNOcnt.Name = "lblPrtBCNOcnt"
        Me.lblPrtBCNOcnt.Size = New System.Drawing.Size(54, 25)
        Me.lblPrtBCNOcnt.TabIndex = 4
        Me.lblPrtBCNOcnt.Text = "3장"
        Me.lblPrtBCNOcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblOrdList
        '
        Me.lblOrdList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOrdList.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.lblOrdList.ForeColor = System.Drawing.Color.White
        Me.lblOrdList.Location = New System.Drawing.Point(3, 138)
        Me.lblOrdList.Name = "lblOrdList"
        Me.lblOrdList.Size = New System.Drawing.Size(122, 23)
        Me.lblOrdList.TabIndex = 128
        Me.lblOrdList.Text = "저장된 검사항목 리스트"
        Me.lblOrdList.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnSaveAs
        '
        Me.btnSaveAs.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSaveAs.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSaveAs.Location = New System.Drawing.Point(126, 138)
        Me.btnSaveAs.Name = "btnSaveAs"
        Me.btnSaveAs.Size = New System.Drawing.Size(60, 22)
        Me.btnSaveAs.TabIndex = 126
        Me.btnSaveAs.Text = "Save As"
        '
        'txtPrtBCNo
        '
        Me.txtPrtBCNo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtPrtBCNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrtBCNo.Location = New System.Drawing.Point(4, 43)
        Me.txtPrtBCNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtPrtBCNo.Multiline = True
        Me.txtPrtBCNo.Name = "txtPrtBCNo"
        Me.txtPrtBCNo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPrtBCNo.Size = New System.Drawing.Size(245, 90)
        Me.txtPrtBCNo.TabIndex = 1
        Me.txtPrtBCNo.TabStop = False
        '
        'lblBCNOcnt
        '
        Me.lblBCNOcnt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBCNOcnt.BackColor = System.Drawing.Color.Khaki
        Me.lblBCNOcnt.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblBCNOcnt.Location = New System.Drawing.Point(4, 15)
        Me.lblBCNOcnt.Margin = New System.Windows.Forms.Padding(0)
        Me.lblBCNOcnt.Name = "lblBCNOcnt"
        Me.lblBCNOcnt.Size = New System.Drawing.Size(191, 24)
        Me.lblBCNOcnt.TabIndex = 0
        Me.lblBCNOcnt.Text = "최근 바코드 출력내역"
        Me.lblBCNOcnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel6.Controls.Add(Me.spdComList)
        Me.Panel6.Location = New System.Drawing.Point(6, 40)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(497, 313)
        Me.Panel6.TabIndex = 124
        '
        'spdComList
        '
        Me.spdComList.DataSource = Nothing
        Me.spdComList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdComList.Location = New System.Drawing.Point(0, 0)
        Me.spdComList.Margin = New System.Windows.Forms.Padding(1)
        Me.spdComList.Name = "spdComList"
        Me.spdComList.OcxState = CType(resources.GetObject("spdComList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdComList.Size = New System.Drawing.Size(493, 309)
        Me.spdComList.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.spdDrugList)
        Me.Panel1.Location = New System.Drawing.Point(6, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(241, 160)
        Me.Panel1.TabIndex = 1
        '
        'spdDrugList
        '
        Me.spdDrugList.DataSource = Nothing
        Me.spdDrugList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdDrugList.Location = New System.Drawing.Point(0, 0)
        Me.spdDrugList.Margin = New System.Windows.Forms.Padding(1)
        Me.spdDrugList.Name = "spdDrugList"
        Me.spdDrugList.OcxState = CType(resources.GetObject("spdDrugList.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdDrugList.Size = New System.Drawing.Size(237, 156)
        Me.spdDrugList.TabIndex = 0
        '
        'grpDrug
        '
        Me.grpDrug.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpDrug.Controls.Add(Me.Panel1)
        Me.grpDrug.Controls.Add(Me.btnDrugHlp)
        Me.grpDrug.Controls.Add(Me.txtDrugCd)
        Me.grpDrug.Controls.Add(Me.lblDrugCd)
        Me.grpDrug.Location = New System.Drawing.Point(884, 4)
        Me.grpDrug.Margin = New System.Windows.Forms.Padding(0)
        Me.grpDrug.Name = "grpDrug"
        Me.grpDrug.Size = New System.Drawing.Size(254, 203)
        Me.grpDrug.TabIndex = 0
        Me.grpDrug.TabStop = False
        '
        'btnDrugHlp
        '
        Me.btnDrugHlp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDrugHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnDrugHlp.Image = CType(resources.GetObject("btnDrugHlp.Image"), System.Drawing.Image)
        Me.btnDrugHlp.Location = New System.Drawing.Point(224, 14)
        Me.btnDrugHlp.Name = "btnDrugHlp"
        Me.btnDrugHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnDrugHlp.TabIndex = 229
        Me.btnDrugHlp.UseVisualStyleBackColor = True
        '
        'txtDrugCd
        '
        Me.txtDrugCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDrugCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDrugCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtDrugCd.Location = New System.Drawing.Point(161, 14)
        Me.txtDrugCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtDrugCd.MaxLength = 10
        Me.txtDrugCd.Name = "txtDrugCd"
        Me.txtDrugCd.Size = New System.Drawing.Size(62, 21)
        Me.txtDrugCd.TabIndex = 0
        Me.txtDrugCd.Tag = "0"
        Me.txtDrugCd.Text = "DR001"
        '
        'lblDrugCd
        '
        Me.lblDrugCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDrugCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.lblDrugCd.ForeColor = System.Drawing.Color.Black
        Me.lblDrugCd.Location = New System.Drawing.Point(8, 13)
        Me.lblDrugCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblDrugCd.Name = "lblDrugCd"
        Me.lblDrugCd.Size = New System.Drawing.Size(152, 22)
        Me.lblDrugCd.TabIndex = 143
        Me.lblDrugCd.Text = "투여약물"
        Me.lblDrugCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grpCom
        '
        Me.grpCom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpCom.Controls.Add(Me.Panel6)
        Me.grpCom.Controls.Add(Me.cboComGbn)
        Me.grpCom.Controls.Add(Me.btnComCdHlp)
        Me.grpCom.Controls.Add(Me.chkEmr)
        Me.grpCom.Controls.Add(Me.lblComCd)
        Me.grpCom.Controls.Add(Me.txtComCd)
        Me.grpCom.Location = New System.Drawing.Point(372, 271)
        Me.grpCom.Name = "grpCom"
        Me.grpCom.Size = New System.Drawing.Size(508, 358)
        Me.grpCom.TabIndex = 124
        Me.grpCom.TabStop = False
        '
        'cboComGbn
        '
        Me.cboComGbn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboComGbn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboComGbn.Items.AddRange(New Object() {"[1] 혈액준비(Prep)", "[2] 혈액수혈(Tranf)", "[3] 응급수혈(Emer)", "[4] Irradiation"})
        Me.cboComGbn.Location = New System.Drawing.Point(8, 16)
        Me.cboComGbn.Margin = New System.Windows.Forms.Padding(1)
        Me.cboComGbn.MaxDropDownItems = 10
        Me.cboComGbn.Name = "cboComGbn"
        Me.cboComGbn.Size = New System.Drawing.Size(241, 20)
        Me.cboComGbn.TabIndex = 147
        Me.cboComGbn.TabStop = False
        Me.cboComGbn.Tag = "COMGBN_01"
        '
        'btnComCdHlp
        '
        Me.btnComCdHlp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnComCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnComCdHlp.Image = CType(resources.GetObject("btnComCdHlp.Image"), System.Drawing.Image)
        Me.btnComCdHlp.Location = New System.Drawing.Point(480, 15)
        Me.btnComCdHlp.Name = "btnComCdHlp"
        Me.btnComCdHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnComCdHlp.TabIndex = 229
        Me.btnComCdHlp.UseVisualStyleBackColor = True
        '
        'chkEmr
        '
        Me.chkEmr.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkEmr.Location = New System.Drawing.Point(255, 17)
        Me.chkEmr.Margin = New System.Windows.Forms.Padding(0)
        Me.chkEmr.Name = "chkEmr"
        Me.chkEmr.Size = New System.Drawing.Size(50, 20)
        Me.chkEmr.TabIndex = 161
        Me.chkEmr.TabStop = False
        Me.chkEmr.Text = "응급"
        '
        'lblComCd
        '
        Me.lblComCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblComCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblComCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblComCd.ForeColor = System.Drawing.Color.White
        Me.lblComCd.Location = New System.Drawing.Point(323, 15)
        Me.lblComCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblComCd.Name = "lblComCd"
        Me.lblComCd.Size = New System.Drawing.Size(93, 22)
        Me.lblComCd.TabIndex = 119
        Me.lblComCd.Text = "성분제제코드"
        Me.lblComCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtComCd
        '
        Me.txtComCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtComCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtComCd.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtComCd.Location = New System.Drawing.Point(417, 15)
        Me.txtComCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtComCd.MaxLength = 5
        Me.txtComCd.Name = "txtComCd"
        Me.txtComCd.Size = New System.Drawing.Size(62, 21)
        Me.txtComCd.TabIndex = 1
        Me.txtComCd.Text = "LA001"
        '
        'grpTest
        '
        Me.grpTest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpTest.Controls.Add(Me.Panel2)
        Me.grpTest.Controls.Add(Me.btnTestCdHlp)
        Me.grpTest.Controls.Add(Me.btnSpcCdHlp)
        Me.grpTest.Controls.Add(Me.cboTOrdSlip)
        Me.grpTest.Controls.Add(Me.lblSpcNm)
        Me.grpTest.Controls.Add(Me.lblSpcCd)
        Me.grpTest.Controls.Add(Me.txtSpcCd)
        Me.grpTest.Controls.Add(Me.lblTestItem)
        Me.grpTest.Controls.Add(Me.txtTestCd)
        Me.grpTest.Location = New System.Drawing.Point(372, 4)
        Me.grpTest.Margin = New System.Windows.Forms.Padding(1)
        Me.grpTest.Name = "grpTest"
        Me.grpTest.Size = New System.Drawing.Size(508, 270)
        Me.grpTest.TabIndex = 0
        Me.grpTest.TabStop = False
        '
        'btnTestCdHlp
        '
        Me.btnTestCdHlp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTestCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTestCdHlp.Image = CType(resources.GetObject("btnTestCdHlp.Image"), System.Drawing.Image)
        Me.btnTestCdHlp.Location = New System.Drawing.Point(479, 37)
        Me.btnTestCdHlp.Name = "btnTestCdHlp"
        Me.btnTestCdHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnTestCdHlp.TabIndex = 228
        Me.btnTestCdHlp.UseVisualStyleBackColor = True
        '
        'btnSpcCdHlp
        '
        Me.btnSpcCdHlp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSpcCdHlp.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnSpcCdHlp.Image = CType(resources.GetObject("btnSpcCdHlp.Image"), System.Drawing.Image)
        Me.btnSpcCdHlp.Location = New System.Drawing.Point(277, 37)
        Me.btnSpcCdHlp.Name = "btnSpcCdHlp"
        Me.btnSpcCdHlp.Size = New System.Drawing.Size(21, 21)
        Me.btnSpcCdHlp.TabIndex = 227
        Me.btnSpcCdHlp.UseVisualStyleBackColor = True
        '
        'cboTOrdSlip
        '
        Me.cboTOrdSlip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTOrdSlip.Location = New System.Drawing.Point(7, 15)
        Me.cboTOrdSlip.Margin = New System.Windows.Forms.Padding(1)
        Me.cboTOrdSlip.MaxDropDownItems = 10
        Me.cboTOrdSlip.Name = "cboTOrdSlip"
        Me.cboTOrdSlip.Size = New System.Drawing.Size(256, 20)
        Me.cboTOrdSlip.TabIndex = 149
        Me.cboTOrdSlip.TabStop = False
        Me.cboTOrdSlip.Tag = "COMGBN_01"
        '
        'lblSpcNm
        '
        Me.lblSpcNm.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSpcNm.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.lblSpcNm.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSpcNm.Location = New System.Drawing.Point(266, 15)
        Me.lblSpcNm.Margin = New System.Windows.Forms.Padding(1)
        Me.lblSpcNm.Name = "lblSpcNm"
        Me.lblSpcNm.Size = New System.Drawing.Size(236, 20)
        Me.lblSpcNm.TabIndex = 144
        Me.lblSpcNm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSpcCd
        '
        Me.lblSpcCd.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblSpcCd.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblSpcCd.ForeColor = System.Drawing.Color.White
        Me.lblSpcCd.Location = New System.Drawing.Point(7, 36)
        Me.lblSpcCd.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSpcCd.Name = "lblSpcCd"
        Me.lblSpcCd.Size = New System.Drawing.Size(84, 22)
        Me.lblSpcCd.TabIndex = 125
        Me.lblSpcCd.Text = "검체명"
        Me.lblSpcCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSpcCd
        '
        Me.txtSpcCd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSpcCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSpcCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSpcCd.Location = New System.Drawing.Point(92, 37)
        Me.txtSpcCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtSpcCd.MaxLength = 4
        Me.txtSpcCd.Name = "txtSpcCd"
        Me.txtSpcCd.Size = New System.Drawing.Size(184, 21)
        Me.txtSpcCd.TabIndex = 0
        '
        'lblTestItem
        '
        Me.lblTestItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTestItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.lblTestItem.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.lblTestItem.ForeColor = System.Drawing.Color.White
        Me.lblTestItem.Location = New System.Drawing.Point(299, 36)
        Me.lblTestItem.Margin = New System.Windows.Forms.Padding(0)
        Me.lblTestItem.Name = "lblTestItem"
        Me.lblTestItem.Size = New System.Drawing.Size(84, 22)
        Me.lblTestItem.TabIndex = 119
        Me.lblTestItem.Text = "검사항목"
        Me.lblTestItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTestCd
        '
        Me.txtTestCd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTestCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTestCd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTestCd.ImeMode = System.Windows.Forms.ImeMode.Alpha
        Me.txtTestCd.Location = New System.Drawing.Point(384, 37)
        Me.txtTestCd.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTestCd.MaxLength = 5
        Me.txtTestCd.Name = "txtTestCd"
        Me.txtTestCd.Size = New System.Drawing.Size(94, 21)
        Me.txtTestCd.TabIndex = 1
        Me.txtTestCd.Text = "LA001"
        '
        'FGO01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1145, 669)
        Me.Controls.Add(Me.grpTest)
        Me.Controls.Add(Me.tbcPatInfo)
        Me.Controls.Add(Me.grpDrug)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.grpCom)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGO01"
        Me.Text = "처방입력"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.pnlPatientGbn.ResumeLayout(False)
        Me.pnlOcsOrder.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.spdOrderList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbcPatInfo.ResumeLayout(False)
        Me.tbpPatInfo0.ResumeLayout(False)
        Me.tbpPatInfo0.PerformLayout()
        Me.tbpPatInfo1.ResumeLayout(False)
        Me.tbpPatInfo1.PerformLayout()
        Me.pnlWardInfo.ResumeLayout(False)
        Me.pnlWardInfo.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        CType(Me.spdComList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.spdDrugList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDrug.ResumeLayout(False)
        Me.grpDrug.PerformLayout()
        Me.grpCom.ResumeLayout(False)
        Me.grpCom.PerformLayout()
        Me.grpTest.ResumeLayout(False)
        Me.grpTest.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region " Spread 보기기/숨김 "
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

#Region " 메인버튼 처리 "

    'Function Key정의()
    Private Sub FGO01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

        Select Case e.KeyCode
            Case Keys.F4
                btnClear_Click(Nothing, Nothing)
            Case Keys.Escape
                btnExit_Click(Nothing, Nothing)

        End Select

    End Sub

    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click
        Dim sFn As String = "Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.ButtonClick"

        Try
            Me.txtPrtBCNo.Text = ""
            Me.lblPrtBCNOcnt.Text = ""

            sbReg_OnlyOrder()       ' 처방(Only)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Dim sFn As String = "Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.ButtonClick"

        Try
            sbFormClear()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

#Region " 폼내부 함수 "
    ' 폼 초기설정
    Private Sub sbFormInitialize()
        Dim sFn As String = "Private Sub sbFormInitialize()"
        Dim CommFN As New Fn
        Dim ServerDT As New ServerDateTime

        Try
            Me.Tag = "Load"

            ' 서버날짜로 설정
            Me.dtpOrdDt.Value = CDate(ServerDT.GetDate("-"))
            Me.dtpHopeDt.Value = Me.dtpOrdDt.Value

            Me.txtRegno.MaxLength = PRG_CONST.Len_RegNo - 1

            sbFormClear()

            Me.cboJubsuGbn.SelectedIndex = 0

            'Spread Header이름을 컬럼명으로 설정
            With CommFN
                .SpdSetColName(spdDrugList)
            End With

            ' 로그인정보 설정
            Me.lblUserId.Text = USER_INFO.USRID
            Me.lblUserNm.Text = USER_INFO.USRNM

            sbSpreadColHidden(True)

            sbDisplaySaveList()

            If Me.lblUserId.Text = "ACK" Then
                Me.btnCollTk.Visible = True
                Me.txtFkOcs.Visible = True
            End If

            sbDisplay_TOrdSlip()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    ' 화면정리
    Private Sub sbFormClear()
        Dim sFn As String = "Private Sub sbFormClear()"

        Try
            Me.cboRegno.SelectedIndex = 1
            Me.txtRegno.Text = ""
            Me.txtPatNm.Text = ""
            Me.txtIdnoL.Text = "" : Me.txtIdnoR.Text = "" : Me.lblBirthDay.Text = ""
            Me.lblSex.Text = ""
            Me.lblAge.Text = ""
            Me.lblDAge.Text = ""

            Me.txtDeptCd.Text = "" : Me.lblDeptNm.Text = ""
            Me.txtDoctorCd.Text = "" : Me.lblDoctorNm.Text = ""
            Me.txtDiagCd0.Text = "" : Me.lblDiagNm0.Text = ""
            Me.txtDiagCd1.Text = "" : Me.lblDiagNm1.Text = ""
            Me.txtDiagCd2.Text = "" : Me.lblDiagNm2.Text = ""
            Me.txtDiagCd3.Text = "" : Me.lblDiagNm3.Text = ""
            Me.txtTel1.Text = ""
            Me.txtTel2.Text = ""
            Me.txtHeight.Text = ""
            Me.txtWeight.Text = ""

            Me.txtWardCd.Text = "" : Me.lblWardNm.Text = ""
            Me.txtRoomCd.Text = "" : Me.lblSRNm.Text = ""
            Me.txtBedno.Text = ""
            Me.dtpEntDt.Value = CDate(Format(Now, "yyyy-MM") + "-01")
            Me.txtOPDt.Text = ""

            Me.txtTestCd.Text = ""
            Me.txtSpcCd.Text = ""
            Me.lblSpcNm.Text = ""

            Me.spdOrderList.MaxRows = 0

            Me.txtPrtBCNo.Text = ""
            Me.lblPrtBCNOcnt.Text = ""

            Me.txtComCd.Text = ""
            Me.spdComList.MaxRows = 0
            Me.chkEmr.Checked = False

            Me.txtResDt.Text = ""

            Me.txtDrugCd.Text = ""
            Me.spdDrugList.MaxRows = 0

            Me.txtDonCnt.Text = "1"

            Me.dtpHopeDt.Value = Now

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    Private Sub sbDisplay_TOrdSlip()
        Dim sFn As String = "Private Sub sbDisplay_TOrdSlip()"

        Try
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_TOrdSlip()

            If dt.Rows.Count < 1 Then Return

            cboTOrdSlip.Items.Clear()
            cboTOrdSlip.Items.Add("[  ] 전체")
            For ix As Integer = 0 To dt.Rows.Count - 1
                cboTOrdSlip.Items.Add(dt.Rows(ix).Item("tordslipnm").ToString.Trim())
            Next

            If cboTOrdSlip.Items.Count > 0 Then cboTOrdSlip.SelectedIndex = 0

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    ' 칼럼 Hidden 유무
    Private Sub sbSpreadColHidden(ByVal abFlag As Boolean)
        Dim sFn As String = "Private Sub sbSpreadColHidden(Boolean)"

        Try
            'Me.lblBirthDay.Visible = Not abFlag

            Me.lblDiagNmE0.Visible = Not abFlag
            Me.lblDiagNmE1.Visible = Not abFlag
            Me.lblDiagNmE2.Visible = Not abFlag
            Me.lblDiagNmE3.Visible = Not abFlag

            With Me.spdOrderList
                .Col = .GetColFromID("spccd") : .ColHidden = abFlag
                .Col = .GetColFromID("sugacd") : .ColHidden = abFlag
                .Col = .GetColFromID("insugbn") : .ColHidden = abFlag
                .Col = .GetColFromID("tsectcd") : .ColHidden = abFlag
                .Col = .GetColFromID("minspcvol") : .ColHidden = abFlag
                .Col = .GetColFromID("tordcd") : .ColHidden = abFlag
                .Col = .GetColFromID("tcdgbn") : .ColHidden = abFlag
                .Col = .GetColFromID("tcd") : .ColHidden = abFlag
            End With

            With Me.spdComList
                .Col = .GetColFromID("comcdo") : .ColHidden = abFlag
                .Col = .GetColFromID("spccd") : .ColHidden = abFlag
                .Col = .GetColFromID("ordkey") : .ColHidden = abFlag
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 데이타 유효성 체크
    Private Function fnValidation() As Boolean
        Dim sFn As String = "Private Function fnValidation() As Boolean"

        Dim intReqQnt As Integer

        Dim intDrugDay As Integer
        Dim intDrugQuantity As Integer

        fnValidation = False
        Try
            If Me.cboJubsuGbn.Text.Equals("") Then
                MsgBox("접수구분을 선택해 주십시오", MsgBoxStyle.Information, Me.Text)
                cboJubsuGbn.Focus()
                Exit Function
            End If

            If Me.txtPatNm.Text.Equals("") Then
                MsgBox("이름을 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtPatNm.Focus()
                Exit Function
            End If

            If Me.rdoPatientGbn1.Checked = True And (Me.chkTestOrder.Checked = True Or Me.cboJubsuGbn.Text = "수혈") Then
                If Me.txtRegno.Text.Equals("") Then
                    MsgBox("등록번호를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    txtRegno.Focus()
                    Exit Function
                End If
            End If

            ' Only처방 주민등록번호, 진료과, 의뢰의사 필수
            If Me.chkTestOrder.Checked = True Then
                If Me.txtIdnoL.Text.Equals("") Then
                    MsgBox("주민등록번호(좌측)를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    txtIdnoL.Focus()
                    Exit Function
                End If

                If Me.lblBirthDay.Text.Equals("") Then
                    MsgBox("주민등록번호(좌측)를 정확히 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.txtIdnoL.Focus()
                    Exit Function
                ElseIf Not IsDate(Me.lblBirthDay.Text) Then
                    MsgBox("주민등록번호(좌측)를 정확히 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.txtIdnoL.Focus()
                    Exit Function
                End If

                If Me.txtIdnoR.Text.Equals("") Then
                    MsgBox("주민등록번호(우측)를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.txtIdnoR.Focus()
                    Exit Function
                End If

                If Me.txtDeptCd.Text.Equals("") Then
                    MsgBox("진료과를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.tbcPatInfo.SelectedTab = tbpPatInfo0
                    Me.txtDeptCd.Focus()
                    Exit Function
                End If

                If Me.txtDoctorCd.Text.Equals("") Then
                    MsgBox("의뢰의사를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.tbcPatInfo.SelectedTab = tbpPatInfo0
                    Me.txtDoctorCd.Focus()
                    Exit Function
                End If
            End If

            ' 입원일경우 필수 항목
            If Me.rdoPatientGbn1.Checked = True Then
                If Me.lblWardNm.Text.Equals("") Then
                    MsgBox("병동을 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.tbcPatInfo.SelectedTab = tbpPatInfo1
                    Me.txtWardCd.Focus()
                    Exit Function
                End If

                If Me.lblSRNm.Text.Equals("") Then
                    MsgBox("병실을 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Me.tbcPatInfo.SelectedTab = tbpPatInfo1
                    Me.txtRoomCd.Focus()
                    Exit Function
                End If

            End If

            If Not Me.txtOPDt.Text.Equals("") Then
                If Me.txtOPDt.Text.IndexOf("-") < 0 Then
                    Me.txtOPDt.Text = Me.txtOPDt.Text.Substring(0, 4) + "-" + Me.txtOPDt.Text.Substring(4, 2) + "-" + Me.txtOPDt.Text.Substring(6, 2)
                End If

                If Not IsDate(Me.txtOPDt.Text) Then
                    MsgBox("수술예정일이 잘 못 되었습니다.", MsgBoxStyle.Information, Me.Text)
                    tbcPatInfo.SelectedTab = tbpPatInfo1
                    txtOPDt.Focus()
                    Exit Function
                End If
            End If

            If Not Me.txtResDt.Text.Equals("") Then
                If Me.txtResDt.Text.IndexOf("-") < 0 Then
                    Me.txtResDt.Text = Me.txtResDt.Text.Substring(0, 4) + "-" + Me.txtResDt.Text.Substring(4, 2) + "-" + Me.txtResDt.Text.Substring(6, 2)
                End If

                If Not IsDate(Me.txtResDt.Text) Then
                    MsgBox("예약일이 잘 못 되었습니다.", MsgBoxStyle.Information, Me.Text)
                    tbcPatInfo.SelectedTab = tbpPatInfo0
                    txtResDt.Focus()
                    Exit Function
                End If
            End If

            If Me.spdOrderList.MaxRows = 0 Then
                MsgBox("검사항목을 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                txtTestCd.Focus()
                Exit Function
            End If

            If Me.cboJubsuGbn.Text = "수혈" Then
                With Me.spdComList
                    If .MaxRows = 0 Then
                        MsgBox("성분제제를 선택해 주십시오.", MsgBoxStyle.Information, Me.Text)
                        txtComCd.Focus()
                        Exit Function
                    End If

                    For intRow As Integer = 1 To .MaxRows
                        ' 의뢰량 체크

                        .Row = intRow
                        .Col = .GetColFromID("qnt")
                        If .Text.ToString = "" Or Not IsNumeric(.Text.ToString) Then .Text = "0"

                        intReqQnt = CInt(.Text.ToString)
                        If intReqQnt < 1 Then
                            MsgBox("의뢰량을 입력해 주십시오", MsgBoxStyle.Information, Me.Text)
                            .Focus()
                            .Row = intRow
                            .Col = .GetColFromID("qnt")
                            .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                            Exit Function
                        End If
                    Next
                End With
            End If

            If cboJubsuGbn.Text = "일반" Then
                ' 수량, 투여일 체크 
                With spdDrugList
                    If .MaxRows > 0 Then
                        For intRow As Integer = 1 To .MaxRows
                            .Row = intRow

                            .Col = .GetColFromID("수량")
                            If .Text.ToString = "" Or Not IsNumeric(.Text.ToString) Then .Text = "0"
                            intDrugQuantity = CInt(.Text.ToString)

                            .Col = .GetColFromID("투여일")
                            If .Text.ToString = "" Or Not IsNumeric(.Text.ToString) Then .Text = "0"
                            intDrugDay = CInt(.Text.ToString)

                            If intDrugQuantity < 1 Then
                                MsgBox("수량을 입력해 주십시오", MsgBoxStyle.Information, Me.Text)
                                .Focus()
                                .Row = intRow
                                .Col = .GetColFromID("수량")
                                .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                                Exit Function
                            End If

                            If intDrugDay < 1 Then
                                MsgBox("투여일을 입력해 주십시오", MsgBoxStyle.Information, Me.Text)
                                .Focus()
                                .Row = intRow
                                .Col = .GetColFromID("투여일")
                                .Action = FPSpreadADO.ActionConstants.ActionActiveCell
                                Exit Function
                            End If
                        Next
                    End If
                End With
            End If

            If Me.cboJubsuGbn.Text = "헌혈" Then
                If Me.txtDonCnt.Text = "" Or Not IsNumeric(Me.txtDonCnt.Text.ToString) Then
                    MsgBox("헌혈횟수를 입력해 주십시오.", MsgBoxStyle.Information, Me.Text)
                    Exit Function
                Else
                    If CInt(txtDonCnt.Text) < 0 Then
                        MsgBox("헌혈횟수가 1보다 작을 수 없습니다.", MsgBoxStyle.Information, Me.Text)
                        Exit Function
                    End If
                End If
            End If

            fnValidation = True

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Function

    ' 주민번호 왼쪽 Validated시 실행함수
    Private Sub sbIdNoLeft(ByVal asIdNoLeft As String)
        Dim sFn As String = "Private Sub fnIdNoLeft(ByVal asIdNoLeft As String)"
        Dim strIDYear As String
        Dim strIDMonth As String
        Dim strIDDay As String
        Dim dtBirthday As Date
        Dim intAGE As Integer

        Try
            ' 기입력의 경우
            Me.lblBirthDay.Text = ""
            If Me.txtIdnoL.Text.Length.Equals(6) Then
                strIDYear = Me.txtIdnoL.Text.Substring(0, 2)
                strIDMonth = Me.txtIdnoL.Text.Substring(2, 2)
                strIDDay = Me.txtIdnoL.Text.Substring(4, 2)

                If IsDate(strIDYear + "-" + strIDMonth + "-" + strIDDay) = False Then
                    MsgBox("주민등록번호를 확인해주세요", MsgBoxStyle.Information, Me.Text)
                    Me.txtIdnoL.Focus()
                    Exit Sub

                Else
                    Me.lblBirthDay.Text = strIDYear + "-" + strIDMonth + "-" + strIDDay
                    dtBirthday = CDate(Format(CType(Me.lblBirthDay.Text, Date), "yyyy-MM-dd"))
                    Me.lblBirthDay.Text = Format(dtBirthday, "yyyy-MM-dd").ToString
                End If

                intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                If Format(dtBirthday, "MMdd").ToString > Format(dtpOrdDt.Value, "MMdd").ToString Then intAGE -= 1
                lblAge.Text = intAGE.ToString
                lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)

            ElseIf txtIdnoL.Text.Length < 4 Then
                lblAge.Text = txtIdnoL.Text
                lblDAge.Text = CStr(Val(txtIdnoL.Text) * 365)

            Else
                MsgBox("나이를 확인해주세요", MsgBoxStyle.Information, Me.Text)
                txtIdnoL.Focus()

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

    ' 주민번호 오른쪽 Validated시 실행함수
    Private Sub sbIdNoRight(ByVal asIdNoRight As String)
        Dim sFn As String = "Private Sub fnIdNoRight(ByVal asIdNoRight As String)"
        Dim strIDYear As String
        Dim strIDMonth As String
        Dim strIDDay As String
        Dim dtBirthday As Date
        Dim strSex As String
        Dim intAGE As Integer

        Try
            If txtIdnoL.Text.Length.Equals(6) AndAlso txtIdnoR.Text.Length.Equals(7) Then
                ' 주민번호로 나이계산

                Me.lblBirthDay.Text = ""

                If txtIdnoL.Text.Length.Equals(6) Then
                    strIDYear = txtIdnoL.Text.Substring(0, 2)
                    strIDMonth = txtIdnoL.Text.Substring(2, 2)
                    strIDDay = txtIdnoL.Text.Substring(4, 2)

                    If IsDate(strIDYear & "-" & strIDMonth & "-" & strIDDay) = False Then
                        MsgBox("주민등록번호를 확인해주세요", MsgBoxStyle.Information, Me.Text)
                        txtIdnoL.Focus()

                        Exit Sub
                    Else
                        '< rem freety 2006/12/15 : 주민등록번호 체크 루틴 수정
                        'If Val(txtIdnoR.Text.Substring(0, 1)) < 3 Then
                        '    lblBirthDay.Text = "19" & strIDYear & "-" & strIDMonth & "-" & strIDDay
                        'Else
                        '    lblBirthDay.Text = "20" & strIDYear & "-" & strIDMonth & "-" & strIDDay
                        'End If
                        '>

                        '< add freety 2006/12/15 : 주민등록번호 체크 루틴 수정
                        Select Case txtIdnoR.Text.Substring(0, 1)
                            Case "1", "2"
                                lblBirthDay.Text = "19" & strIDYear & "-" & strIDMonth & "-" & strIDDay

                            Case "3", "4"
                                lblBirthDay.Text = "20" & strIDYear & "-" & strIDMonth & "-" & strIDDay

                            Case "5", "6"   '외국인등록번호
                                lblBirthDay.Text = "19" & strIDYear & "-" & strIDMonth & "-" & strIDDay

                            Case "7", "8"   '외국인등록번호
                                lblBirthDay.Text = "20" & strIDYear & "-" & strIDMonth & "-" & strIDDay

                            Case "9", "0"
                                lblBirthDay.Text = "18" & strIDYear & "-" & strIDMonth & "-" & strIDDay

                        End Select
                        '>

                        dtBirthday = CType(lblBirthDay.Text, Date)
                    End If

                    intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                    If (dtBirthday.Month.ToString("MM") & dtBirthday.Day.ToString("dd")) > (dtpOrdDt.Value.Month.ToString("MM") & dtpOrdDt.Value.Day.ToString("dd")) Then
                        intAGE -= 1
                    End If
                    lblAge.Text = intAGE.ToString
                    lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)

                    ' 성별 판정
                    strSex = txtIdnoR.Text.Substring(0, 1)
                    If Val(strSex) Mod 2 = 1 Then
                        lblSex.Text = "남"
                    ElseIf Val(strSex) Mod 2 = 0 Then
                        lblSex.Text = "여"
                    End If

                    Exit Sub
                End If

            End If

            '입력된 숫자가 주민번호가 아닐때 성별 판정
            If txtIdnoR.Text.Trim.Length > 0 Then
                strSex = txtIdnoR.Text.Substring(0, 1)

                lblBirthDay.Text = ""
                If txtIdnoL.Text.Length.Equals(6) Then
                    strIDYear = txtIdnoL.Text.Substring(0, 2)
                    strIDMonth = txtIdnoL.Text.Substring(2, 2)
                    strIDDay = txtIdnoL.Text.Substring(4, 2)

                    If Val(txtIdnoR.Text.Substring(0, 1)) < 3 Then
                        lblBirthDay.Text = "19" & strIDYear & "-" & strIDMonth & "-" & strIDDay
                    Else
                        lblBirthDay.Text = "20" & strIDYear & "-" & strIDMonth & "-" & strIDDay
                    End If
                    dtBirthday = CType(lblBirthDay.Text, Date)

                    intAGE = CType(DateDiff(DateInterval.Year, dtBirthday, dtpOrdDt.Value), Integer)
                    If (dtBirthday.Month.ToString & dtBirthday.Day.ToString) > (dtpOrdDt.Value.Month.ToString & dtpOrdDt.Value.Day.ToString) Then
                        intAGE -= 1
                    End If
                    lblAge.Text = intAGE.ToString
                    lblDAge.Text = CType(DateDiff(DateInterval.Day, dtBirthday, dtpOrdDt.Value), String)
                End If

                If Val(strSex) Mod 2 = 1 Then
                    lblSex.Text = "남"
                ElseIf Val(strSex) Mod 2 = 0 Then
                    lblSex.Text = "여"
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 처방 ( Only )
    Private Sub sbReg_OnlyOrder()
        Dim sFn As String = "Private Sub sbReg_OnlyOrder()"

        Dim alOCSOrder As New ArrayList
        Dim PatInfo As New DB_MTS_Order.clsMTS0002
        Dim alDrugList As New ArrayList
        Dim alDiagList As New ArrayList

        Try
            ' 데이타 유효성 체크
            If fnValidation() = False Then Exit Sub

            ' 환자정보 수집 
            sbPatInfo_Collect(PatInfo)

            ' 진단명 수집
            fnDiag_Collect(alDiagList)

            ' 투여약물 수집
            If Me.cboJubsuGbn.Text.IndexOf("일반") >= 0 And Me.spdDrugList.MaxRows > 0 Then
                fnDrug_Collect(alDrugList)
            End If

            ' 일반검사항목 수집
            sbTestItem_Collect(alOCSOrder)

            If Me.cboJubsuGbn.Text.IndexOf("수혈") >= 0 Then
                ' 수혈의뢰 내역 수집
                fnTnsItem_Collect(alOCSOrder)
            End If
            alOCSOrder.TrimToSize()

            ' 처방(Only  처방)
            With (New DB_MTS_Order)
                Dim sRet As String = .ExecuteDo(alOCSOrder, PatInfo, alDrugList, alDiagList, lblUserId.Text)

                If sRet <> "" Then
                    Throw (New Exception(sRet))
                Else
                    sbFormClear()
                    MsgBox("정상적으로 처리되었습니다.", MsgBoxStyle.Information, Me.Text)
                End If
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 환자정보 수집
    Private Sub sbPatInfo_Collect(ByRef aoPatInfo As DB_MTS_Order.clsMTS0002)
        Dim sFn As String = "Private Sub fnPatient_Collect()"

        Try
            ' 환자정보 수집
            'PatientInfo = New DB_MTS_Order.clsMTS002
            With aoPatInfo
                .SEQ = ""                   ' 순번
                .BUNHO = Me.cboRegno.Text + Me.txtRegno.Text     ' 등록번호
                .SUNAME = Me.txtPatNm.Text     ' 성명
                .BIRTH = Me.lblBirthDay.Text.Replace("-", "")
                .SUJUMIN1 = Me.txtIdnoL.Text   ' 주민번호 왼쪽
                .SUJUMIN2 = Me.txtIdnoR.Text   ' 주민번호 오른쪽
                .ZIP_CODE1 = ""
                .ZIP_CODE2 = ""
                .ADDRESS1 = ""
                .ADDRESS2 = ""
                .TEL1 = Me.txtTel1.Text        ' 연락처1
                .TEL2 = Me.txtTel2.Text        ' 연락처1
                .SEND_DATE = ""
                .RECV_DATE = ""
                .IUD = "I"                  ' 입력구분
                .FLAG = ""

                .SEX = CType(IIf(Me.lblSex.Text = "남", "M", "F"), String) '성별
            End With

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 일반검사항목 수집( Only 처방 )
    Private Sub sbTestItem_Collect(ByRef alOCSOrder As ArrayList)
        Dim sFn As String = "Private Sub fnTestItem_Collect() "
        Dim OCSOrder As DB_MTS_Order.clsMTS0001

        Try
            ' 검사항목 수집
            For intRow As Integer = 1 To spdOrderList.MaxRows
                With spdOrderList
                    .Row = intRow
                    OCSOrder = New DB_MTS_Order.clsMTS0001

                    With OCSOrder
                        .SEQ = ""                       ' 순번
                        .IN_OUT_GUBUN = IIf(Me.rdoPatientGbn0.Checked, "O", "I").ToString      ' O:외래, I:입원구분
                        .FKOCS = ""
                        .BUNHO = Me.cboRegno.Text + Me.txtRegno.Text        ' 등록번호
                        .GWA = Me.txtDeptCd.Text                            ' 진료과
                        .IPWON_DATE = IIf(Me.rdoPatientGbn0.Checked, "", Me.dtpEntDt.Text.Replace("-", "")).ToString      ' 입원일자
                        .RESIDENT = ""                                      ' 주치의 
                        .DOCTOR = Me.txtDoctorCd.Text                       ' 의사코드
                        .HO_DONG = Me.txtWardCd.Text                        ' 병동
                        .HO_CODE = Me.txtRoomCd.Text                        ' 병실
                        .HO_BED = Me.txtBedno.Text                          ' 병상
                        .ORDER_DATE = dtpOrdDt.Text.Replace("-", "")        ' 처방일자
                        .ORDER_TIME = ""                                    ' 처방일시
                        .SLIP_GUBUN = "B"
                        If Me.cboJubsuGbn.Text = "헌혈" Then
                            .SURYANG = Me.txtDonCnt.Text
                        Else
                            .SURYANG = "1"                                  ' 수량(default = 1)
                        End If
                        .HOPE_DATE = Me.dtpHopeDt.Text.Substring(0, 10).Replace("-", "") ' 검사희망일
                        .HOPE_TIME = Me.dtpHopeDt.Text.Substring(10).Replace(":", "").Replace(" ", "").Substring(0, 4)  ' 검사희망시간

                        .DC_YN = "N"                    ' D/C 여부(default = "N")
                        .APPEND_YN = ""                 ' 추가여부
                        If PRG_CONST.DEPT_NOSUNAB.IndexOf(Me.txtDeptCd.Text + ",") >= 0 And .IN_OUT_GUBUN = "O" Then
                            .SUNAB_DATE = ""      ' 수납(default = "Y")
                        Else
                            .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                        End If

                        .SOURCE_FKOCS = ""              ' 추가검사인경우 Parent FKOCS
                        '.EMERGENCY 
                        '.REMARK
                        '.REQ_REMARK 
                        .HEIGHT = Me.txtHeight.Text       ' 키
                        .WEGHT = Me.txtWeight.Text        ' 체중
                        .SEND_DATE = ""
                        .RECV_DATE = ""
                        .IUD = "I"                      ' 입력구분
                        .FLAG = ""

                        .OPDT = Me.txtOPDt.Text.Replace("-", "")    ' 수술예정일                        
                    End With

                    .Col = .GetColFromID("tordcd") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 항목코드
                    .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 검체코드
                    .Col = .GetColFromID("errflg") : OCSOrder.EMERGENCY = IIf(.Text.Trim = "1", "Y", "").ToString ' 응급구분
                    .Col = .GetColFromID("remark") : OCSOrder.REMARK = .Text.Trim ' 리마크

                    alOCSOrder.Add(OCSOrder)
                End With
            Next
            alOCSOrder.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 수혈의뢰내역 수집
    Private Sub fnTnsItem_Collect(ByRef alOCSOrder As ArrayList)
        Dim sFn As String = "Private Sub fnTestItem_Collect() "
        Dim OCSOrder As DB_MTS_Order.clsMTS0001
        Dim intSURYANG As Integer

        Try
            ' 수혈의뢰내역 수집
            For intRow As Integer = 1 To spdComList.MaxRows
                With spdComList
                    .Row = intRow

                    .Col = .GetColFromID("qnt") : intSURYANG = CInt(.Text.Trim) ' 의뢰량
                    For intCnt As Integer = 1 To intSURYANG
                        OCSOrder = New DB_MTS_Order.clsMTS0001
                        With OCSOrder
                            .SEQ = ""                       ' 순번
                            .IN_OUT_GUBUN = IIf(Me.rdoPatientGbn0.Checked, "O", "I").ToString      ' O:외래, I:입원구분
                            .FKOCS = ""
                            .BUNHO = Me.cboRegno.Text + Me.txtRegno.Text         ' 등록번호
                            .GWA = Me.txtDeptCd.Text           ' 진료과
                            .IPWON_DATE = IIf(Me.rdoPatientGbn0.Checked, "", Me.dtpEntDt.Text.Replace("-", "")).ToString      ' 입원일자
                            .RESIDENT = ""                  ' 주치의 
                            .DOCTOR = Me.txtDoctorCd.Text      ' 의사코드
                            .HO_DONG = Me.txtWardCd.Text       ' 병동
                            .HO_CODE = Me.txtRoomCd.Text         ' 병실
                            .HO_BED = Me.txtBedno.Text            ' 병상
                            .ORDER_DATE = Me.dtpOrdDt.Text.Replace("-", "")  ' 처방일자
                            .ORDER_TIME = ""                ' 처방일시
                            '.HANGMOG_CODE
                            '.SPECIMEN_CODE
                            .SLIP_GUBUN = "B"
                            .SURYANG = "1"                  ' 수량(default = 1)
                            .HOPE_DATE = Me.txtResDt.Text.Replace("-", "")      ' 검사희망일
                            .HOPE_TIME = ""                 ' 검사희망시간
                            .DC_YN = "N"                    ' D/C 여부(default = "N")
                            .APPEND_YN = ""                 ' 추가여부
                            If PRG_CONST.DEPT_NOSUNAB.IndexOf(Me.txtDeptCd.Text + ",") >= 0 And .IN_OUT_GUBUN = "O" Then
                                .SUNAB_DATE = ""
                            Else

                                .SUNAB_DATE = .ORDER_DATE       ' 수납(default = "Y")
                            End If
                            .SOURCE_FKOCS = ""              ' 추가업사인경우 Parent FKOCS
                            .EMERGENCY = IIf(Me.chkEmr.Checked, "Y", "").ToString
                            .REMARK = ""
                            '.REQ_REMARK 
                            .HEIGHT = Me.txtHeight.Text       ' 키
                            .WEGHT = Me.txtWeight.Text        ' 체중
                            .SEND_DATE = ""
                            .RECV_DATE = ""
                            .IUD = "I"                      ' 입력구분
                            .FLAG = ""

                            .OPDT = Me.txtOPDt.Text.Replace("-", "")            ' 수술예정일                        
                        End With
                        .Col = .GetColFromID("comcdo") : OCSOrder.HANGMOG_CODE = .Text.Trim ' 성분제제 처방코드
                        .Col = .GetColFromID("spccd") : OCSOrder.SPECIMEN_CODE = .Text.Trim ' 성분제제구분(검체코드)

                        alOCSOrder.Add(OCSOrder)
                    Next

                End With
            Next
            alOCSOrder.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try

    End Sub

    ' 진단명 수집
    Private Sub fnDiag_Collect(ByRef alDiagList As ArrayList, _
                               Optional ByRef asDiagNm As String = "", Optional ByRef asDiagNmE As String = "")
        Dim sFn As String = "Private Sub fnDiag_Collect(ByRef alDiagList As ArrayList)"

        Dim DiagInfo As DB_MTS_Order.clsMTS0101
        Dim objTextBox As Windows.Forms.TextBox
        Dim objLabel As Windows.Forms.Label
        Dim objLabelE As Windows.Forms.Label

        Try
            For intCnt As Integer = 0 To 3
                If intCnt = 0 Then
                    objTextBox = txtDiagCd0
                    objLabel = lblDiagNm0
                    objLabelE = lblDiagNmE0
                ElseIf intCnt = 1 Then
                    objTextBox = txtDiagCd1
                    objLabel = lblDiagNm1
                    objLabelE = lblDiagNmE1
                ElseIf intCnt = 2 Then
                    objTextBox = txtDiagCd2
                    objLabel = lblDiagNm2
                    objLabelE = lblDiagNmE2
                ElseIf intCnt = 3 Then
                    objTextBox = txtDiagCd3
                    objLabel = lblDiagNm3
                    objLabelE = lblDiagNmE3
                End If

                If objTextBox.Text <> "" And objLabel.Text <> "" Then
                    DiagInfo = New DB_MTS_Order.clsMTS0101
                    With DiagInfo
                        .SANG_CODE = objTextBox.Text
                        .SANG_HNAME = objLabel.Text.Replace("'", "′")
                        .SANG_ENAME = objLabelE.Text.Replace("'", "′")
                        .BUNHO = Me.cboRegno.Text + Me.txtRegno.Text
                        .SEND_DATE = ""
                        .RECV_DATE = ""
                        .IUD = "I"                      ' 입력구분
                        .FLAG = ""
                        .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")   ' 처방일자
                    End With
                    alDiagList.Add(DiagInfo)

                    ' 처방+채혈일경우 진단명 입력을 위해 
                    If asDiagNm = "" Then asDiagNm = objLabel.Text.Replace("'", "′") _
                                     Else asDiagNm &= ", " & objLabel.Text.Replace("'", "′")

                    If asDiagNmE = "" Then asDiagNmE = objLabelE.Text.Replace("'", "′") _
                                      Else asDiagNmE &= ", " & objLabelE.Text.Replace("'", "′")
                End If
            Next
            alDiagList.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try
    End Sub

    ' 투여약물 수집
    Private Sub fnDrug_Collect(ByRef alDrugList As ArrayList, Optional ByRef asDrugNm As String = "")
        Dim sFn As String = "Private Sub fnDrug_Collect(ByRef alDrugInfo As ArrayList)"
        Dim DrugInfo As DB_MTS_Order.clsMTS0903

        Try
            With spdDrugList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow

                    DrugInfo = New DB_MTS_Order.clsMTS0903
                    With DrugInfo
                        .BUNHO = Me.cboRegno.Text + Me.txtRegno.Text
                        .SEND_DATE = ""
                        .RECV_DATE = ""
                        .IUD = "I"                      ' 입력구분
                        .FLAG = ""
                        .ORDER_DATE = Format(dtpOrdDt.Value, "yyyy-MM-dd")   ' 처방일자
                    End With
                    .Col = .GetColFromID("코드") : DrugInfo.DRUG_CODE = .Text
                    .Col = .GetColFromID("투여약물명") : DrugInfo.DRUG_NAME = .Text.Trim.Replace("'", "′")
                    .Col = .GetColFromID("수량") : DrugInfo.SURYANG = .Text
                    .Col = .GetColFromID("투여일") : DrugInfo.NALSU = .Text
                    alDrugList.Add(DrugInfo)

                    With DrugInfo
                        If asDrugNm = "" Then
                            asDrugNm = .DRUG_NAME.Trim & ": " & .SURYANG & "/" & .NALSU & ""
                        Else
                            asDrugNm &= ", " & .DRUG_NAME.Trim & ": " & .SURYANG & "/" & .NALSU & ""
                        End If
                    End With
                Next
            End With
            alDrugList.TrimToSize()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Throw (New Exception(ex.Message, ex))

        End Try


    End Sub

#End Region

#Region " Control Event 처리 "
    Private Sub FGO01_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If CType(Me.Tag, String) = "Load" Then
            txtRegno.Focus()

            Me.Tag = ""
        End If
    End Sub

    Private Sub txtRegNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtRegno.KeyDown
        Dim sFn As String = "Handles txtRegNo.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtRegno.Text = "" Then Return

        SendKeys.Send("{TAB}")

    End Sub

    Private Sub txtRegno_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRegno.KeyPress, txtDonCnt.KeyPress, txtHeight.KeyPress, txtWeight.KeyPress
        Fn.sbNumericTextBox(CType(sender, Windows.Forms.TextBox), e)
    End Sub

    Private Sub txtRegNo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRegno.Validating
        Dim sFn As String = "Handles txtRegNo.Validating"

        Try
            If Me.txtRegno.Text = "" Then Return

            Me.txtRegno.Text = Me.txtRegno.Text.PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            If Me.txtRegno.Text = "" Then Me.txtRegno.Text = "0"

            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Patinfo(Me.cboRegno.Text + Me.txtRegno.Text.Trim, "")

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    Me.txtPatNm.Text = .Item("suname").ToString.Trim()
                    Me.txtIdnoL.Text = .Item("idno_full").ToString.Trim().Split("-"c)(0) : sbIdNoLeft(Me.txtIdnoL.Text)
                    Me.txtIdnoR.Text = .Item("idno_full").ToString.Trim().Split("-"c)(1) : sbIdNoRight(Me.txtIdnoR.Text)

                    Me.lblBirthDay.Text = .Item("birth").ToString

                    Me.txtTel1.Text = .Item("tel1").ToString
                    Me.txtTel2.Text = .Item("tel2").ToString
                End With

                Me.txtDeptCd.Focus()

            Else
                Me.txtPatNm.Text = ""
                Me.txtIdnoL.Text = ""
                Me.txtIdnoR.Text = ""
                Me.lblSex.Text = "" : Me.lblAge.Text = "" : Me.lblDAge.Text = ""

                Me.txtTel1.Text = ""
                Me.txtTel2.Text = ""

            End If


        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub fnText1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles dtpOrdDt.KeyPress, txtComCd.KeyPress, txtTestCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            rdoPatientGbn0.Checked = True
            rdoPatientGbn0.Focus()
        End If
    End Sub

    Private Sub fnText2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtResDt.KeyPress, txtOPDt.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            txtTestCd.Focus()
        End If
    End Sub

    Private Sub fnText3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles rdoPatientGbn1.KeyPress, rdoPatientGbn0.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : cboJubsuGbn.Focus()
        End If
    End Sub

    Private Sub txtSpcCd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSpcCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : txtTestCd.Focus()
        End If
    End Sub

    Private Sub txtComCd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtComCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : txtComCd.Focus()
        End If
    End Sub

    Private Sub txtDrugCd_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDrugCd.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True : txtDrugCd.Focus()
        End If
    End Sub

    Private Sub tbcPatInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcPatInfo.Click
        If tbcPatInfo.SelectedIndex = 0 Then
            txtDeptCd.Focus()
        Else
            txtWardCd.Focus()
        End If
    End Sub

    Private Sub txtIdnoL_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIdnoL.Validated
        Dim sFn As String = "Private Sub txtIdnoL_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIdnoL.LostFocus"

        Try
            If Me.txtIdnoL.Modified = True Then
                ' 미입력의 경우
                If Me.txtIdnoL.Text.Length.Equals(0) Then
                    Me.lblAge.Text = ""
                    Me.lblDAge.Text = ""
                    Return
                End If

                ' 나이계산
                sbIdNoLeft(Me.txtIdnoL.Text)
                Me.txtIdnoL.Modified = False
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtIdnoR_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIdnoR.Validated
        Dim sFn As String = "Private Sub txtIdnoR_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIdnoR.LostFocus"

        Try
            If Me.txtIdnoR.Modified = True Then
                '미입력의 경우
                If Me.txtIdnoR.Text.Length.Equals(0) Then
                    Me.lblSex.Text = "" : Exit Sub
                End If

                ' 성별체크
                sbIdNoRight(Me.txtIdnoR.Text)
                Me.txtIdnoR.Modified = False
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtRIdno_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtIdnoR.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(13) Then
            e.Handled = True
            tbcPatInfo.SelectedTab = tbpPatInfo0
            Me.txtDeptCd.Focus()
        End If
    End Sub

    Private Sub dtpOrdDt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.GotFocus
        Me.dtpOrdDt.Tag = Format(dtpOrdDt.Value, "yyyy-MM-dd")
    End Sub

    Private Sub dtpOrdDt_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.DropDown
        dtpOrdDt.Tag = Format(dtpOrdDt.Value, "yyyy-MM-dd")
    End Sub

    Private Sub dtpOrdDt_CloseUp(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.CloseUp
        cboJubsuGbn.Focus()

        If CType(dtpOrdDt.Tag, String) = Format(dtpOrdDt.Value, "yyyy-MM-dd") Then Exit Sub
        sbFormClear()
    End Sub

    Private Sub dtpOrdDt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtpOrdDt.Validated
        If CType(dtpOrdDt.Tag, String) = Format(dtpOrdDt.Value, "yyyy-MM-dd") Then Exit Sub
        sbFormClear()
    End Sub

    Private Sub txtDeptCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeptCd.Validated
        Dim sFn As String = "Private Sub txtDeptCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDeptCd.Validated"

        If txtDeptCd.Text = "" Then Return

        Try

            btnDeptHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtDoctorCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDoctorCd.Validated
        Dim sFn As String = "Private Sub txtDoctorCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDoctorCd.Validated"

        If txtDoctorCd.Text = "" Then Return

        Try
            btnDoctorHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtDiagCd_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiagCd0.Validated, txtDiagCd1.Validated, txtDiagCd2.Validated, txtDiagCd3.Validated
        Dim sFn As String = "Private Sub txtDiagCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDiagCd.Validated"
        Dim objTextBox As Windows.Forms.TextBox = CType(sender, Windows.Forms.TextBox)

        If objTextBox.Text = "" Then Return

        Try
            Select Case objTextBox.Name
                Case "txtDiagCd0"
                    btnDiagHlp_Click(btnDiagHlp0, Nothing)
                Case "txtDiagCd1"
                    btnDiagHlp_Click(btnDiagHlp1, Nothing)
                Case "txtDiagCd2"
                    btnDiagHlp_Click(btnDiagHlp2, Nothing)
                Case "txtDiagCd3"
                    btnDiagHlp_Click(btnDiagHlp3, Nothing)
            End Select

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtWardCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWardCd.Validated
        Dim sFn As String = "Private Sub txtWardCd_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWardCd.Validated"

        If txtWardCd.Text = "" Then Return

        Try
            btnWardHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub txtSRCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRoomCd.Validated
        Dim sFn As String = "Private Sub txtSRCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSRCd.Validated"

        If txtRoomCd.Text = "" Then Return

        Try
            btnSRHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub txtSpcCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpcCd.Validated
        Dim sFn As String = "Private Sub txtSpcCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpcCd.Validated"

        If txtSpcCd.Text = "" Then Return

        Try
            btnSpcCdHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub txtTestCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestCd.Validated
        Dim sFn As String = "Private Sub txtTestCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTestCd.Validated"

        If txtTestCd.Text = "" Then Return

        Try
            btnTestCdHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try


    End Sub

    Private Sub txtComCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComCd.Validated
        Dim sFn As String = "Private Sub txtComCd_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtComCd.Validated"

        If txtComCd.Text = "" Then Return

        Try
            btnComCdHlp_Click(Nothing, Nothing)

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub spdList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdOrderList.DblClick, spdComList.DblClick, spdDrugList.DblClick
        Dim sFn As String = "Private Sub spdOrderList_DblClick(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_DblClickEvent) Handles spdOrderList.DblClick, spdComList.DblClick, spdDrugList.DblClick"
        Dim objSpd As AxFPSpreadADO.AxfpSpread = CType(sender, AxFPSpreadADO.AxfpSpread)

        Try
            If e.row < 1 Then Exit Sub

            If MsgBox("해당항목을 리스트에서 삭제 하시겠습니까?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.Yes Then
                With objSpd
                    .DeleteRows(e.row, 1) : .MaxRows -= 1
                End With
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub cboJubsuGbn_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboJubsuGbn.SelectionChangeCommitted
        If cboJubsuGbn.Text.IndexOf("수혈") >= 0 Then
            Me.grpDrug.Visible = False
            Me.grpCom.Visible = True
            Me.grpTest.Height = Me.grpCom.Top - 2

        Else
            If Me.cboJubsuGbn.Text.IndexOf("일반") >= 0 Then
                Me.grpDrug.Visible = True
            Else
                Me.grpDrug.Visible = False
            End If

            Me.grpCom.Visible = False
            Me.grpTest.Height = Me.grpCom.Top + Me.grpCom.Height - 3

        End If

        If Me.cboJubsuGbn.Text.IndexOf("수혈") >= 0 Or Me.cboJubsuGbn.Text.IndexOf("헌혈") >= 0 Then
            Me.chkTestOrder.Checked = True
            Me.pnlOcsOrder.Enabled = False

        Else
            Me.chkTestOrder.Checked = True
            Me.pnlOcsOrder.Enabled = True
        End If

        If Me.cboJubsuGbn.Text.IndexOf("헌혈") >= 0 Then
            Me.lblDonCnt.Visible = True
            Me.txtDonCnt.Visible = True
        Else
            Me.lblDonCnt.Visible = False
            Me.txtDonCnt.Visible = False
        End If

        Me.txtRegno.Focus()
    End Sub

#End Region

#Region " CodeHelp버튼 처리"
    Private Sub btnTestCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCdHlp.Click
        Dim sFn As String = "Private Sub btnTestCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTestCdHlp.Click"
        Dim CommFn As New Fn

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim sTclsCds As String = ""
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_testspc_list_ord(Ctrl.Get_Code(Me.cboTOrdSlip), Me.txtTestCd.Text, Me.txtSpcCd.Text)

            With spdOrderList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("tcd") : sTclsCds += .Text + "|"
                Next
            End With

            objHelp.FormText = "검사항목 코드"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = sTclsCds
            objHelp.OnRowReturnYN = True

            objHelp.AddField("''", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("tnmd", "검사명", 25, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("testcd", "검사코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spccd", "검체코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sugacd", "수가코드", , , , True)
            objHelp.AddField("insugbn", "보험구분", , , , True)
            objHelp.AddField("bcclscd", "검체분류", , , , True)
            objHelp.AddField("minspcvol", "최소채혈량", , , , True)
            objHelp.AddField("tordcd", "처방코드", , , , True)
            objHelp.AddField("tcdgbn", "구분", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("testspc", "검사검체", , , , True, "testspc", "Y")

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtTestCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtTestCd.Height + 80, dt)

            If alList.Count > 0 Then
                Dim arlTClsCd As New ArrayList

                For intidx As Integer = 0 To alList.Count - 1
                    Dim strTclsCd As String = alList.Item(intidx).ToString.Split("|"c)(2)
                    Dim strSpcCd As String = alList.Item(intidx).ToString.Split("|"c)(3)
                    Dim strTnmd As String = alList.Item(intidx).ToString.Split("|"c)(0)
                    Dim strSpcNmd As String = alList.Item(intidx).ToString.Split("|"c)(1)
                    Dim strSugaCd As String = alList.Item(intidx).ToString.Split("|"c)(4)
                    Dim strInsuGbn As String = alList.Item(intidx).ToString.Split("|"c)(5)
                    Dim strTSectCd As String = alList.Item(intidx).ToString.Split("|"c)(6)
                    Dim strMinSpcVol As String = alList.Item(intidx).ToString.Split("|"c)(7)
                    Dim strTordCd As String = alList.Item(intidx).ToString.Split("|"c)(8)
                    Dim strTcdGbn As String = alList.Item(intidx).ToString.Split("|"c)(9)
                    Dim strTCd As String = alList.Item(intidx).ToString.Split("|"c)(10)

                    ' 검사항목 선택 유/무 체크
                    If CommFn.SpdColSearch(spdOrderList, strTCd, spdOrderList.GetColFromID("tcd")) = 0 Then

                        With spdOrderList
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("tnmd") : .Text = strTnmd
                            .Col = .GetColFromID("spcnmd") : .Text = strSpcNmd
                            .Col = .GetColFromID("testcd") : .Text = strTclsCd
                            .Col = .GetColFromID("spccd") : .Text = strSpcCd
                            .Col = .GetColFromID("tordcd") : .Text = strTordCd
                            .Col = .GetColFromID("sugacd") : .Text = strSugaCd
                            .Col = .GetColFromID("insugbn") : .Text = strInsuGbn
                            .Col = .GetColFromID("tcdgbn") : .Text = strTcdGbn
                            .Col = .GetColFromID("minspcvol") : .Text = strMinSpcVol
                            .Col = .GetColFromID("tsectcd") : .Text = strTSectCd
                            .Col = .GetColFromID("tcd") : .Text = strTCd
                        End With
                    Else
                        MsgBox("이미 추가된 항목 입니다.", MsgBoxStyle.Information, Me.Text)
                    End If
                Next
            End If
            Me.txtTestCd.Text = ""
            Me.txtTestCd.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnComCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCdHlp.Click
        Dim sFn As String = "Private Sub btnComCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnComCdHlp.Click"

        Try

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Com_List(Ctrl.Get_Code(cboComGbn), Me.txtComCd.Text)

            Dim sComCds As String = ""

            With Me.spdComList
                For intRow As Integer = 1 To .MaxRows
                    .Row = intRow
                    .Col = .GetColFromID("ordkey") : sComCds += .Text + "|"
                Next
            End With

            objHelp.FormText = "성분제제"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.KeyCodes = sComCds
            objHelp.OnRowReturnYN = True

            objHelp.AddField("chk", "", 2, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, "CHECKBOX")
            objHelp.AddField("comcd", "성분제제코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("comnmd", "성분제제명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("tnsgbn", "구분", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("filter", "필터", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("comordcd", "처방코드", , , , True)
            objHelp.AddField("spccd", "검체코드", , , , True)
            objHelp.AddField("ordkey", "ordkey", , , , True, "ordkey", "Y")
            objHelp.AddField("donqnt", "donqnt", , , , True)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtComCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtComCd.Height + 80, dt)

            If alList.Count > 0 Then
                Dim arlTClsCd As New ArrayList

                For intidx As Integer = 0 To alList.Count - 1
                    Dim sComCd As String = alList.Item(intidx).ToString.Split("|"c)(0)
                    Dim sComNmd As String = alList.Item(intidx).ToString.Split("|"c)(1)
                    Dim sTrnGbn As String = alList.Item(intidx).ToString.Split("|"c)(2)
                    Dim sFilter As String = alList.Item(intidx).ToString.Split("|"c)(3)
                    Dim sComCdo As String = alList.Item(intidx).ToString.Split("|"c)(4)
                    Dim sSpcCd As String = alList.Item(intidx).ToString.Split("|"c)(5)
                    Dim sOrdKey As String = alList.Item(intidx).ToString.Split("|"c)(6)

                    ' 검사항목 선택 유/무 체크
                    If Fn.SpdColSearch(spdComList, sOrdKey, spdOrderList.GetColFromID("ordkey")) = 0 Then

                        With spdComList
                            .MaxRows += 1
                            .Row = .MaxRows
                            .Col = .GetColFromID("comcd") : .Text = sComCd
                            .Col = .GetColFromID("comnmd") : .Text = sComNmd
                            .Col = .GetColFromID("trngbn") : .Text = sTrnGbn
                            .Col = .GetColFromID("filter") : .Text = sFilter
                            .Col = .GetColFromID("comcdo") : .Text = sComCdo
                            .Col = .GetColFromID("spccd") : .Text = sSpcCd
                            .Col = .GetColFromID("ordkey") : .Text = sOrdKey
                        End With
                    Else
                        MsgBox("이미 추가된 항목 입니다.", MsgBoxStyle.Information, Me.Text)
                    End If
                Next
            End If
            Me.txtComCd.Text = ""
            Me.txtComCd.Focus()

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnDeptHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeptHlp.Click
        Dim sFn As String = "Private Sub btnDeptHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeptHlp.Click"

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptList(Me.txtDeptCd.Text)

            objHelp.FormText = "진료과코드"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("deptcd", "진료과", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("deptnm", "진료과명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtDeptCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtDeptCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtDeptCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.lblDeptNm.Text = alList.Item(0).ToString.Split("|"c)(1)

                Me.txtDoctorCd.Text = ""
                Me.lblDoctorNm.Text = ""

                Me.txtDoctorCd.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnDoctorHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoctorHlp.Click
        Dim sFn As String = "Private Sub btnDoctorHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDoctorHlp.Click"

        Try
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtDoctorCd)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_DeptDoctorList(Me.txtDeptCd.Text, Me.txtDoctorCd.Text)

            objHelp.FormText = "의사정보"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("doctorcd", "의사코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("doctornm", "의사명", 14, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("deptnm", "진료과명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("deptcd", "진료과코드", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtDoctorCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtDoctorCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.lblDoctorNm.Text = alList.Item(0).ToString.Split("|"c)(1)

                If Me.txtDeptCd.Text = "" Then
                    '-- 담당과 없이 의뢰의사만 선택시 해당과 입력
                    Me.txtDeptCd.Text = alList.Item(0).ToString.Split("|"c)(3)
                    Me.lblDeptNm.Text = alList.Item(0).ToString.Split("|"c)(2)
                End If

                Me.txtDiagCd0.Focus()

            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnDiagHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagHlp0.Click, btnDiagHlp2.Click, btnDiagHlp1.Click, btnDiagHlp3.Click
        Dim sFn As String = "Private Sub btnDiagHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiagHlp.Click"
        Dim objButton As Windows.Forms.Button = CType(sender, Windows.Forms.Button)
        Dim strTag As String = CType(objButton.Tag, String)
        Dim objTextBox As Windows.Forms.TextBox
        Dim objLabel As Windows.Forms.Label
        Dim objLabelE As Windows.Forms.Label

        Try
            If strTag = "0" Then
                objTextBox = txtDiagCd0
                objLabel = lblDiagNm0
                objLabelE = lblDiagNmE0
            ElseIf strTag = "1" Then
                objTextBox = txtDiagCd1
                objLabel = lblDiagNm1
                objLabelE = lblDiagNmE1
            ElseIf strTag = "2" Then
                objTextBox = txtDiagCd2
                objLabel = lblDiagNm2
                objLabelE = lblDiagNmE2
            ElseIf strTag = "3" Then
                objTextBox = txtDiagCd3
                objLabel = lblDiagNm3
                objLabelE = lblDiagNmE3
            End If

            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            objHelp.FormText = "진단코드"
            objHelp.TableNm = "ocs_db..vw_mts0101"

            If objTextBox.Text <> "" Then
                objHelp.Where = "SANG_CODE = '" + objTextBox.Text + "'"
            End If

            objHelp.GroupBy = ""
            objHelp.OrderBy = "SANG_NAME"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("SANG_CODE", "진단코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SANG_NAME", "진단명", 40, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("SANG_NAME_HAN", "진단명_한글", 0, , , True)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(objTextBox)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + objTextBox.Height + 80)

            If aryList.Count > 0 Then
                objTextBox.Text = aryList.Item(0).ToString.Split("|"c)(0)
                objLabel.Text = aryList.Item(0).ToString.Split("|"c)(1).Replace("'", "`")
                objLabelE.Text = aryList.Item(0).ToString.Split("|"c)(2).Replace("'", "`")

                If strTag = "0" Then
                    Me.txtDiagCd1.Focus()
                ElseIf strTag = "1" Then
                    Me.txtDiagCd2.Focus()
                ElseIf strTag = "2" Then
                    Me.txtDiagCd3.Focus()
                ElseIf strTag = "3" Then
                    Me.txtTel1.Focus()
                End If
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnWardHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWardHlp.Click
        Dim sFn As String = "Private Sub btnWardHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWardHlp.Click"
        Try

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_WardList(Me.txtWardCd.Text)

            objHelp.FormText = "병동코드"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtWardCd)

            objHelp.AddField("wardno", "코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("wardnm", "내용", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtWardCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtWardCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.lblWardNm.Text = alList.Item(0).ToString.Split("|"c)(0)

                Me.txtRoomCd.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub btnSRHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRoomHlp.Click
        Dim sFn As String = "Private Sub btnSRHlp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSRHlp.Click"

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.SData.fnGet_RoomList(Me.txtWardCd.Text, Me.txtRoomCd.Text)

            objHelp.FormText = "병실코드"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("wardno", "병동", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("roomno", "병실", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtRoomCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtRoomCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtRoomCd.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.lblSRNm.Text = alList.Item(0).ToString.Split("|"c)(1)

                If Me.txtWardCd.Text = "" Then
                    '-- 병동 없이 병실만 선택시 해당병동 입력
                    Me.txtWardCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                    Me.lblWardNm.Text = alList.Item(0).ToString.Split("|"c)(0)
                End If

                Me.txtBedno.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try
    End Sub

    Private Sub btnSpcCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpcCdHlp.Click
        Dim sFn As String = "Private Sub btnSpcCdHlp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSpcCdHlp.Click"

        Try
            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = LISAPP.COMM.CdFn.fnGet_Spc_List("", "", "", "", "", "", Me.txtSpcCd.Text)

            objHelp.FormText = "검체코드"

            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("spccd", "검체코드", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("spcnmd", "검체명", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(Me.txtSpcCd)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + Me.txtSpcCd.Height + 80, dt)

            If alList.Count > 0 Then
                Me.txtSpcCd.Text = alList.Item(0).ToString.Split("|"c)(0)
                Me.lblSpcNm.Text = alList.Item(0).ToString.Split("|"c)(1)

                Me.txtTestCd.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

#End Region

    Private Sub btnTKReg_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTKReg.Click
        Dim sFn As String = "btnTKReg_ButtonClick"

        Dim si As SYSIF01.SYSIF

        Try
            si = New SYSIF01.SYSIF(USER_INFO.USRID, USER_INFO.LOCALIP)

            Dim oi As SYSIF01.OrderInfo

            oi = New SYSIF01.OrderInfo

            Dim sRegNo As String = ""
            Dim sEntDay As String = ""
            Dim al_testcd As New ArrayList
            Dim al_spccd As New ArrayList
            Dim al_trmk As New ArrayList
            Dim al_emer As New ArrayList
            Dim al_fkocs As New ArrayList

            Dim iSuc As Integer = 0
            Dim sErrMsg As String = ""
            Dim al_return As New ArrayList

            If Me.cboRegno.Text + Me.txtRegno.Text.Trim = "" Then
                MsgBox("등록번호를 입력하세요.!!")
                Return
            End If

            sRegNo = Me.cboRegno.Text + Me.txtRegno.Text.Trim
            sEntDay = Me.dtpEntDt.Text      '-- 입원일

            'Order Info
            oi.OrderDay = Format(Now, "yyyy-MM-dd")
            oi.RegNo = Me.cboRegno.Text + Me.txtRegno.Text.Trim        ' 등록번호
            oi.PatNm = txtPatNm.Text.Trim         ' 성명
            oi.Sex = CType(IIf(lblSex.Text = "남", "M", "F"), String) '성별
            oi.Age = lblAge.Text.Trim             ' 나이
            oi.DAge = lblDAge.Text.Trim           ' 일 환산 나이
            oi.IdNoL = txtIdnoL.Text.Trim         ' 주민등록번호 왼쪽
            oi.IdNoR = txtIdnoR.Text.Trim         ' 주민등록번호 오른쪽
            oi.BirthDay = lblBirthDay.Text.Trim   ' 생일
            oi.TEL1 = txtTel1.Text.Trim           ' 연락처1
            oi.TEL2 = txtTel2.Text.Trim           ' 연락처2
            oi.DoctorCd = txtDoctorCd.Text.Trim   ' 의뢰의사코드
            oi.DoctorNm = lblDoctorNm.Text.Trim   ' 의뢰의사명
            oi.DeptCd = txtDeptCd.Text.Trim       ' 진료과코드
            oi.DeptNm = lblDeptNm.Text.Trim       ' 진료과명
            If IsDate(Me.txtOPDt.Text) Then
                oi.OpDt = Me.txtOPDt.Text.Trim            ' 수술예정일
            End If
            oi.JubsuGbn = Ctrl.Get_Code(Me.cboJubsuGbn)   ' 접수구분
            oi.IOGbn = IIf(rdoPatientGbn0.Checked, "O", "I").ToString   ' O:외래, I:입원구분

            For intRow As Integer = 1 To spdOrderList.MaxRows
                With spdOrderList
                    .Row = intRow

                    Dim strTestCd As String = ""
                    Dim strSpcCd As String = ""
                    Dim strEmer As String = ""
                    Dim strRemark As String = ""

                    .Col = .GetColFromID("testcd") : strTestCd = .Text.Trim ' 항목코드
                    .Col = .GetColFromID("spccd") : strSpcCd = .Text.Trim ' 검체코드
                    .Col = .GetColFromID("errflg") : strEmer = IIf(.Text.Trim = "1", "Y", "").ToString ' 응급구분
                    .Col = .GetColFromID("remark") : strRemark = .Text.Trim ' 리마크

                    al_testcd.Add(strTestCd)
                    al_spccd.Add(strSpcCd)
                    al_trmk.Add(strRemark)
                    al_emer.Add(strEmer)
                    al_fkocs.Add("")
                End With
            Next

            oi.TestCds = al_testcd
            oi.SpcCds = al_spccd
            oi.TRemarks = al_trmk
            oi.EmerYNs = al_emer
            oi.FKOCSs = al_fkocs

            'Enter Info
            If IsDate(sEntDay) Then
                oi.EntDt = sEntDay
                oi.WardNo = txtWardCd.Text.Trim
                oi.RoomNo = txtRoomCd.Text.Trim
                oi.BedNo = ""
            Else
                'MsgBox("입원일자 오류 발생!!")
                'Return
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

            oi = Nothing

            If iSuc = 0 Then
                '실패
                MsgBox(sErrMsg)
                Return
            Else
                sbFormClear()
                MsgBox("정상적으로 처리되었습니다.", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            Fn.log(msFile + sFn, Err)
            MsgBox(msFile + sFn + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub btnSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveAs.Click

        If Dir(Application.StartupPath + msXML, FileAttribute.Directory) = "" Then
            MkDir(Application.StartupPath + msXML & "\")
        End If

        sfdSCd.Filter = "xml files (*.xml)|*.xml"
        sfdSCd.InitialDirectory = Application.StartupPath & msXML & "\"

        If sfdSCd.ShowDialog() = DialogResult.OK Then

            Dim xmlWriter As Xml.XmlTextWriter = Nothing

            xmlWriter = New Xml.XmlTextWriter(sfdSCd.FileName, Nothing)
            xmlWriter.Formatting = Xml.Formatting.Indented
            xmlWriter.Indentation = spdOrderList.MaxCols '4
            xmlWriter.IndentChar = Chr(32)
            xmlWriter.WriteStartDocument(False)
            xmlWriter.WriteComment(" 선택된 검사분류 코드 ")
            xmlWriter.WriteStartElement("ROOT")

            With spdOrderList
                For intRow As Integer = 1 To .MaxRows
                    xmlWriter.WriteStartElement("TCLS")
                    For intCol As Integer = 1 To .MaxCols
                        .Row = intRow
                        .Col = intCol
                        xmlWriter.WriteElementString(.ColID, .Text)
                    Next
                    xmlWriter.WriteEndElement()
                Next
            End With

            xmlWriter.WriteEndElement()
            xmlWriter.Close()
        End If

        sbDisplaySaveList()
    End Sub

    Private Sub lstSaveList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSaveList.SelectedIndexChanged
        With lstSaveList

            'spdOrderList.MaxRows = 0

            Dim strDir As String
            strDir = Application.StartupPath + msXML + "\" + .Items(.SelectedIndex).ToString

            If Dir(strDir) > "" Then
                Dim xmlReader As Xml.XmlTextReader

                xmlReader = New Xml.XmlTextReader(strDir)
                While xmlReader.Read
                    xmlReader.ReadStartElement("ROOT")

                    Do While (True)
                        xmlReader.ReadStartElement("TCLS")

                        With spdOrderList
                            .MaxRows += 1
                            For intCol As Integer = 1 To .MaxCols
                                .Row = .MaxRows
                                .Col = intCol

                                Dim strColId As String = ""
                                Dim strTmp As String = ""
                                strColId = .ColID

                                Try
                                    strTmp = xmlReader.ReadElementString(strColId)
                                Catch ex As Exception

                                    Select Case .ColID
                                        Case "testcd" : strColId = "검사코드"
                                        Case "tnmd" : strColId = "검사명"
                                        Case "spcnmd" : strColId = "검체명"
                                        Case "spccd" : strColId = "검체코드"
                                        Case "errflg" : strColId = "응급"
                                        Case "sugacd" : strColId = "수가코드"
                                        Case "remark" : strColId = "Remark"
                                        Case "insugbn" : strColId = "보험구분"
                                        Case "tsectcd" : strColId = "계"
                                        Case "minspcvol" : strColId = "최소채혈량"
                                        Case "tordcd" : strColId = "처방코드"
                                        Case "tcdgbn" : strColId = "검사코드구분"
                                        Case "tcd" : strColId = "검사_검체코드"
                                        Case Else
                                            strColId = .ColID
                                    End Select

                                    strTmp = xmlReader.ReadElementString(strColId)

                                End Try

                                .Text = strTmp
                            Next
                        End With

                        xmlReader.ReadEndElement()
                        xmlReader.Read()
                        If xmlReader.Name <> "TCLS" Then
                            Exit Do
                        End If
                    Loop
                    xmlReader.Close()
                End While
            End If
        End With

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        With lstSaveList
            If .SelectedIndex > -1 Then
                Dim strDir As String
                strDir = Application.StartupPath + msXML + "\" + .Items(.SelectedIndex).ToString
                Kill(strDir)

                sbDisplaySaveList()
            Else
                MsgBox("삭제할 리스트를 선택하여 주세요.")
            End If
        End With

    End Sub

    Private Sub FGO01_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub txtPatNm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPatNm.KeyDown

        Dim sFn As String = "Handles txtPatNm.KeyDown"

        If e.KeyCode <> Keys.Enter Then Return
        If Me.txtRegno.Text <> "" Then Return

        If txtPatNm.Text.Length < 2 Then Return
        If txtPatNm.Text.Substring(0, 1) <> "?" Then Return

        Try
            Dim pntCtlXY As Point = Fn.CtrlLocationXY(Me)
            Dim pntFrmXY As Point = Fn.CtrlLocationXY(txtPatNm)

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim alList As New ArrayList
            Dim dt As DataTable = OCSAPP.OcsLink.Pat.fnGet_Patinfo("", Me.txtPatNm.Text)

            objHelp.FormText = "환자조회"

            objHelp.GroupBy = ""
            objHelp.OrderBy = ""
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = True

            objHelp.AddField("bunho", "등록번호", 10, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("suname", "성명", 12, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("sex", "성별", 4, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)
            objHelp.AddField("idno_full", "주민등록번호", 0, , , True)
            objHelp.AddField("birth", "생일", 0, , , True)
            objHelp.AddField("tel1", "연락처1", 0, , , True)
            objHelp.AddField("tel2", "연락처2", 0, , , True)

            alList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X, pntFrmXY.Y + pntCtlXY.Y + txtPatNm.Height + 80)

            If alList.Count > 0 Then
                Me.txtRegno.Text = alList.Item(0).ToString.Split("|"c)(0).Substring(0, 1)
                Me.txtRegno.Text = alList.Item(0).ToString.Split("|"c)(0).Substring(1)
                Me.txtPatNm.Text = alList.Item(0).ToString.Split("|"c)(1)
                Me.txtIdnoL.Text = alList.Item(0).ToString.Split("|"c)(3).Split("-"c)(0) : sbIdNoLeft(Me.txtIdnoL.Text)
                Me.txtIdnoR.Text = alList.Item(0).ToString.Split("|"c)(3).Split("-"c)(1) : sbIdNoRight(Me.txtIdnoR.Text)

                Me.txtTel1.Text = alList.Item(0).ToString.Split("|"c)(5)
                Me.txtTel2.Text = alList.Item(0).ToString.Split("|"c)(6)

                Me.txtDeptCd.Focus()
            Else
                Me.txtRegno.Text = ""
                Me.txtRegno.Text = ""
                Me.txtPatNm.Text = ""
                Me.txtIdnoL.Text = ""
                Me.txtIdnoR.Text = ""
                Me.lblSex.Text = "" : Me.lblAge.Text = "" : Me.lblDAge.Text = ""

                Me.txtTel1.Text = ""
                Me.txtTel2.Text = ""
                Me.txtRegno.Focus()
            End If

        Catch ex As Exception
            Fn.log(msFile & sFn, Err)
            Fn.ExclamationErrMsg(Err, Me.Text)

        End Try

    End Sub

    Private Sub cboRegno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRegno.SelectedIndexChanged
        If Me.txtRegno.Text = "" Then Return

        txtRegNo_Validating(Nothing, Nothing)

    End Sub

End Class