'>>> 보관 검체 관리
Imports System.Windows.Forms
Imports System.Drawing

Imports COMMON.CommFN
Imports COMMON.CommFN.CGCOMMON13
Imports COMMON.SVar
Imports common.commlogin.login

Imports CDHELP.FGCDHELPFN

Imports LISAPP.APP_DB
Imports LISAPP.APP_KS.KsFn
Imports LISAPP.APP_KS.ExecFn

Public Class FGB12
    Inherits System.Windows.Forms.Form

    Dim objKeepBcno As New STU_KsRack
    Dim objToKeepBcno As New STU_KsRack
    Dim Max_Row As Integer
    Dim Max_Col As Integer
    Dim strToRackID As String = ""    ' 보관검체 이동시 옮길 rackid
    Dim strRealBcno As String = ""    ' 실제로 넘겨줄 완벽한 검체번호의 형태
    Dim strComment As String = ""     ' 클릭이벤트 발생했을때 해당 검체가 가지고 있는 보관 Comment (LK010M의 OTHER)
    Dim m_i_RackId_idx As Integer = 0
    Dim COM_01 As New COMMON.CommFN.Fn
    Dim objComm As New ServerDateTime
    Dim Click_Row As Integer = 0
    Dim Click_Col As Integer = 0

    Dim i_DownRow As Integer = 0
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtAlarm As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtMCol As System.Windows.Forms.TextBox
    Friend WithEvents txtMRow As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboRackID As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnDiscard As CButtonLib.CButton
    Friend WithEvents btnDiscard_All As CButtonLib.CButton
    Friend WithEvents btnExit As CButtonLib.CButton
    Friend WithEvents btnClear As CButtonLib.CButton
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents cboToRack_ID As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboToCol As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cboToRow As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents txtComment As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents spdManage As AxFPSpreadADO.AxfpSpread
    ' MouseDownEvent 가 발생했을때 해당 row 저장
    Dim i_DownCol As Integer = 0       ' MouseDownEvent 가 발생했을때 해당 col 저장

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        ClearData_All()

        ' RackID 가져오기
        Get_RackID()

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
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBCNO As System.Windows.Forms.TextBox
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DesignerRectTracker1 As CButtonLib.DesignerRectTracker = New CButtonLib.DesignerRectTracker
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGB12))
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.btnMove = New System.Windows.Forms.Button
        Me.cboToRack_ID = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cboToCol = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.cboToRow = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtBCNO = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtAlarm = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtMCol = New System.Windows.Forms.TextBox
        Me.txtMRow = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboRackID = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnDiscard = New CButtonLib.CButton
        Me.btnDiscard_All = New CButtonLib.CButton
        Me.btnExit = New CButtonLib.CButton
        Me.btnClear = New CButtonLib.CButton
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnModify = New System.Windows.Forms.Button
        Me.txtComment = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.spdManage = New AxFPSpreadADO.AxfpSpread
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.spdManage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnSearch)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.btnMove)
        Me.GroupBox1.Controls.Add(Me.cboToRack_ID)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cboToCol)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cboToRow)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtBCNO)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtRegNo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtTime)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 500)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1062, 59)
        Me.GroupBox1.TabIndex = 153
        Me.GroupBox1.TabStop = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(949, 12)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(68, 23)
        Me.btnSearch.TabIndex = 169
        Me.btnSearch.Text = "찾기"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(844, 13)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(1)
        Me.txtSearch.MaxLength = 8
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(104, 21)
        Me.txtSearch.TabIndex = 168
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.IndianRed
        Me.Label16.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label16.Location = New System.Drawing.Point(743, 13)
        Me.Label16.Margin = New System.Windows.Forms.Padding(1)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 21)
        Me.Label16.TabIndex = 167
        Me.Label16.Text = "등록번호 입력"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnMove
        '
        Me.btnMove.Location = New System.Drawing.Point(655, 34)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(68, 23)
        Me.btnMove.TabIndex = 166
        Me.btnMove.Text = "이동"
        '
        'cboToRack_ID
        '
        Me.cboToRack_ID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToRack_ID.Location = New System.Drawing.Point(205, 35)
        Me.cboToRack_ID.Margin = New System.Windows.Forms.Padding(1)
        Me.cboToRack_ID.Name = "cboToRack_ID"
        Me.cboToRack_ID.Size = New System.Drawing.Size(97, 20)
        Me.cboToRack_ID.TabIndex = 161
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label9.Location = New System.Drawing.Point(116, 35)
        Me.Label9.Margin = New System.Windows.Forms.Padding(1)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(88, 21)
        Me.Label9.TabIndex = 160
        Me.Label9.Text = "To Rack ID"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.IndianRed
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.White
        Me.Label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label8.Location = New System.Drawing.Point(8, 35)
        Me.Label8.Margin = New System.Windows.Forms.Padding(1)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(100, 21)
        Me.Label8.TabIndex = 159
        Me.Label8.Text = "보관검체이동"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboToCol
        '
        Me.cboToCol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToCol.Location = New System.Drawing.Point(401, 35)
        Me.cboToCol.Margin = New System.Windows.Forms.Padding(1)
        Me.cboToCol.Name = "cboToCol"
        Me.cboToCol.Size = New System.Drawing.Size(83, 20)
        Me.cboToCol.TabIndex = 165
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.White
        Me.Label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label11.Location = New System.Drawing.Point(322, 35)
        Me.Label11.Margin = New System.Windows.Forms.Padding(1)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 21)
        Me.Label11.TabIndex = 164
        Me.Label11.Text = "가     로"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboToRow
        '
        Me.cboToRow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboToRow.Location = New System.Drawing.Point(565, 35)
        Me.cboToRow.Margin = New System.Windows.Forms.Padding(1)
        Me.cboToRow.Name = "cboToRow"
        Me.cboToRow.Size = New System.Drawing.Size(82, 20)
        Me.cboToRow.TabIndex = 163
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.White
        Me.Label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label10.Location = New System.Drawing.Point(486, 35)
        Me.Label10.Margin = New System.Windows.Forms.Padding(1)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(78, 21)
        Me.Label10.TabIndex = 162
        Me.Label10.Text = "세로"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtBCNO
        '
        Me.txtBCNO.BackColor = System.Drawing.Color.White
        Me.txtBCNO.Location = New System.Drawing.Point(193, 13)
        Me.txtBCNO.Margin = New System.Windows.Forms.Padding(1)
        Me.txtBCNO.Name = "txtBCNO"
        Me.txtBCNO.ReadOnly = True
        Me.txtBCNO.Size = New System.Drawing.Size(109, 21)
        Me.txtBCNO.TabIndex = 150
        Me.txtBCNO.Text = "20031016-BB-0001"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label12.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.White
        Me.Label12.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label12.Location = New System.Drawing.Point(116, 13)
        Me.Label12.Margin = New System.Windows.Forms.Padding(1)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 21)
        Me.Label12.TabIndex = 149
        Me.Label12.Text = "검체번호"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.IndianRed
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.Location = New System.Drawing.Point(8, 13)
        Me.Label7.Margin = New System.Windows.Forms.Padding(1)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(100, 21)
        Me.Label7.TabIndex = 148
        Me.Label7.Text = "보관검체정보"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.Color.White
        Me.txtName.Location = New System.Drawing.Point(516, 13)
        Me.txtName.Margin = New System.Windows.Forms.Padding(1)
        Me.txtName.Name = "txtName"
        Me.txtName.ReadOnly = True
        Me.txtName.Size = New System.Drawing.Size(72, 21)
        Me.txtName.TabIndex = 147
        Me.txtName.Text = "아무개아기"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Location = New System.Drawing.Point(455, 13)
        Me.Label6.Margin = New System.Windows.Forms.Padding(1)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 21)
        Me.Label6.TabIndex = 146
        Me.Label6.Text = "성명"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtRegNo
        '
        Me.txtRegNo.BackColor = System.Drawing.Color.White
        Me.txtRegNo.Location = New System.Drawing.Point(394, 13)
        Me.txtRegNo.Margin = New System.Windows.Forms.Padding(1)
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(60, 21)
        Me.txtRegNo.TabIndex = 145
        Me.txtRegNo.Text = "12345678"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label5.Location = New System.Drawing.Point(322, 13)
        Me.Label5.Margin = New System.Windows.Forms.Padding(1)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 21)
        Me.Label5.TabIndex = 144
        Me.Label5.Text = "등록번호"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTime
        '
        Me.txtTime.BackColor = System.Drawing.Color.White
        Me.txtTime.Location = New System.Drawing.Point(650, 13)
        Me.txtTime.Margin = New System.Windows.Forms.Padding(1)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(72, 21)
        Me.txtTime.TabIndex = 143
        Me.txtTime.Text = "1234:59:59"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Location = New System.Drawing.Point(589, 13)
        Me.Label3.Margin = New System.Windows.Forms.Padding(1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 21)
        Me.Label3.TabIndex = 142
        Me.Label3.Text = "경과시간"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label17.ForeColor = System.Drawing.Color.Gray
        Me.Label17.Location = New System.Drawing.Point(3, 23)
        Me.Label17.Margin = New System.Windows.Forms.Padding(0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(1070, 10)
        Me.Label17.TabIndex = 222
        Me.Label17.Text = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" & _
            "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━"
        '
        'txtAlarm
        '
        Me.txtAlarm.BackColor = System.Drawing.Color.White
        Me.txtAlarm.Location = New System.Drawing.Point(503, 3)
        Me.txtAlarm.Margin = New System.Windows.Forms.Padding(1)
        Me.txtAlarm.MaxLength = 3
        Me.txtAlarm.Name = "txtAlarm"
        Me.txtAlarm.ReadOnly = True
        Me.txtAlarm.Size = New System.Drawing.Size(28, 21)
        Me.txtAlarm.TabIndex = 230
        Me.txtAlarm.Text = "999"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label13.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(407, 3)
        Me.Label13.Margin = New System.Windows.Forms.Padding(1)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(95, 21)
        Me.Label13.TabIndex = 229
        Me.Label13.Text = "Alarm 표시일"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtMCol
        '
        Me.txtMCol.BackColor = System.Drawing.Color.White
        Me.txtMCol.Location = New System.Drawing.Point(371, 3)
        Me.txtMCol.Margin = New System.Windows.Forms.Padding(1)
        Me.txtMCol.Name = "txtMCol"
        Me.txtMCol.ReadOnly = True
        Me.txtMCol.Size = New System.Drawing.Size(24, 21)
        Me.txtMCol.TabIndex = 228
        Me.txtMCol.Text = "99"
        '
        'txtMRow
        '
        Me.txtMRow.BackColor = System.Drawing.Color.White
        Me.txtMRow.Location = New System.Drawing.Point(268, 3)
        Me.txtMRow.Margin = New System.Windows.Forms.Padding(1)
        Me.txtMRow.Name = "txtMRow"
        Me.txtMRow.ReadOnly = True
        Me.txtMRow.Size = New System.Drawing.Size(24, 21)
        Me.txtMRow.TabIndex = 227
        Me.txtMRow.Text = "99"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label4.Location = New System.Drawing.Point(303, 3)
        Me.Label4.Margin = New System.Windows.Forms.Padding(1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 21)
        Me.Label4.TabIndex = 226
        Me.Label4.Text = "Max Col"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.Location = New System.Drawing.Point(192, 3)
        Me.Label1.Margin = New System.Windows.Forms.Padding(1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 21)
        Me.Label1.TabIndex = 225
        Me.Label1.Text = "Max Row"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboRackID
        '
        Me.cboRackID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRackID.Location = New System.Drawing.Point(86, 3)
        Me.cboRackID.Margin = New System.Windows.Forms.Padding(1)
        Me.cboRackID.Name = "cboRackID"
        Me.cboRackID.Size = New System.Drawing.Size(72, 20)
        Me.cboRackID.TabIndex = 224
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label14.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label14.Location = New System.Drawing.Point(5, 3)
        Me.Label14.Margin = New System.Windows.Forms.Padding(1)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 21)
        Me.Label14.TabIndex = 223
        Me.Label14.Text = "Rack ID"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDiscard
        '
        Me.btnDiscard.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker1.IsActive = False
        DesignerRectTracker1.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker1.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard.CenterPtTracker = DesignerRectTracker1
        CBlendItems1.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems1.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnDiscard.ColorFillBlend = CBlendItems1
        Me.btnDiscard.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDiscard.Corners.All = CType(6, Short)
        Me.btnDiscard.Corners.LowerLeft = CType(6, Short)
        Me.btnDiscard.Corners.LowerRight = CType(6, Short)
        Me.btnDiscard.Corners.UpperLeft = CType(6, Short)
        Me.btnDiscard.Corners.UpperRight = CType(6, Short)
        Me.btnDiscard.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDiscard.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDiscard.FocalPoints.CenterPtX = 0.4672897!
        Me.btnDiscard.FocalPoints.CenterPtY = 0.16!
        Me.btnDiscard.FocalPoints.FocusPtX = 0.0!
        Me.btnDiscard.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker2.IsActive = False
        DesignerRectTracker2.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker2.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard.FocusPtTracker = DesignerRectTracker2
        Me.btnDiscard.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDiscard.ForeColor = System.Drawing.Color.White
        Me.btnDiscard.Image = Nothing
        Me.btnDiscard.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard.ImageIndex = 0
        Me.btnDiscard.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDiscard.Location = New System.Drawing.Point(746, 3)
        Me.btnDiscard.Name = "btnDiscard"
        Me.btnDiscard.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDiscard.SideImage = Nothing
        Me.btnDiscard.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDiscard.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDiscard.Size = New System.Drawing.Size(103, 25)
        Me.btnDiscard.TabIndex = 195
        Me.btnDiscard.Tag = "availdt"
        Me.btnDiscard.Text = "폐  기"
        Me.btnDiscard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDiscard.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'btnDiscard_All
        '
        Me.btnDiscard_All.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DesignerRectTracker3.IsActive = False
        DesignerRectTracker3.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker3.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard_All.CenterPtTracker = DesignerRectTracker3
        CBlendItems2.iColor = New System.Drawing.Color() {System.Drawing.Color.AliceBlue, System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(40, Byte), Integer), CType(CType(180, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer), CType(CType(20, Byte), Integer)), System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))}
        CBlendItems2.iPoint = New Single() {0.0!, 0.2960725!, 0.8912387!, 1.0!}
        Me.btnDiscard_All.ColorFillBlend = CBlendItems2
        Me.btnDiscard_All.ColorFillSolid = System.Drawing.SystemColors.Control
        Me.btnDiscard_All.Corners.All = CType(6, Short)
        Me.btnDiscard_All.Corners.LowerLeft = CType(6, Short)
        Me.btnDiscard_All.Corners.LowerRight = CType(6, Short)
        Me.btnDiscard_All.Corners.UpperLeft = CType(6, Short)
        Me.btnDiscard_All.Corners.UpperRight = CType(6, Short)
        Me.btnDiscard_All.FillType = CButtonLib.CButton.eFillType.GradientLinear
        Me.btnDiscard_All.FillTypeLinear = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.btnDiscard_All.FocalPoints.CenterPtX = 0.4672897!
        Me.btnDiscard_All.FocalPoints.CenterPtY = 0.16!
        Me.btnDiscard_All.FocalPoints.FocusPtX = 0.0!
        Me.btnDiscard_All.FocalPoints.FocusPtY = 0.0!
        DesignerRectTracker4.IsActive = False
        DesignerRectTracker4.TrackerRectangle = CType(resources.GetObject("DesignerRectTracker4.TrackerRectangle"), System.Drawing.RectangleF)
        Me.btnDiscard_All.FocusPtTracker = DesignerRectTracker4
        Me.btnDiscard_All.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDiscard_All.ForeColor = System.Drawing.Color.White
        Me.btnDiscard_All.Image = Nothing
        Me.btnDiscard_All.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard_All.ImageIndex = 0
        Me.btnDiscard_All.ImageSize = New System.Drawing.Size(16, 16)
        Me.btnDiscard_All.Location = New System.Drawing.Point(643, 3)
        Me.btnDiscard_All.Name = "btnDiscard_All"
        Me.btnDiscard_All.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnDiscard_All.SideImage = Nothing
        Me.btnDiscard_All.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDiscard_All.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnDiscard_All.Size = New System.Drawing.Size(102, 25)
        Me.btnDiscard_All.TabIndex = 194
        Me.btnDiscard_All.Tag = "availdt"
        Me.btnDiscard_All.Text = "일괄폐기"
        Me.btnDiscard_All.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnDiscard_All.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnDiscard_All.TextMargin = New System.Windows.Forms.Padding(0)
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
        Me.btnExit.FocalPoints.CenterPtX = 0.4897959!
        Me.btnExit.FocalPoints.CenterPtY = 0.72!
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
        Me.btnExit.Location = New System.Drawing.Point(958, 3)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnExit.SideImage = Nothing
        Me.btnExit.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnExit.Size = New System.Drawing.Size(98, 25)
        Me.btnExit.TabIndex = 193
        Me.btnExit.Text = "종료(Esc)"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        Me.btnClear.Location = New System.Drawing.Point(850, 3)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Shape = CButtonLib.CButton.eShape.Rectangle
        Me.btnClear.SideImage = Nothing
        Me.btnClear.SideImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.SideImageSize = New System.Drawing.Size(48, 48)
        Me.btnClear.Size = New System.Drawing.Size(107, 25)
        Me.btnClear.TabIndex = 192
        Me.btnClear.Text = "화면정리(F4)"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        Me.btnClear.TextMargin = New System.Windows.Forms.Padding(0)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.btnModify)
        Me.Panel1.Controls.Add(Me.txtComment)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Location = New System.Drawing.Point(519, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(539, 25)
        Me.Panel1.TabIndex = 235
        '
        'btnModify
        '
        Me.btnModify.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnModify.Location = New System.Drawing.Point(471, 3)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(68, 23)
        Me.btnModify.TabIndex = 234
        Me.btnModify.Text = "저장"
        '
        'txtComment
        '
        Me.txtComment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtComment.BackColor = System.Drawing.Color.White
        Me.txtComment.Location = New System.Drawing.Point(110, 3)
        Me.txtComment.MaxLength = 200
        Me.txtComment.Multiline = True
        Me.txtComment.Name = "txtComment"
        Me.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComment.Size = New System.Drawing.Size(356, 21)
        Me.txtComment.TabIndex = 233
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label15.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Transparent
        Me.Label15.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label15.Location = New System.Drawing.Point(1, 3)
        Me.Label15.Margin = New System.Windows.Forms.Padding(1)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(108, 21)
        Me.Label15.TabIndex = 232
        Me.Label15.Text = "보관 Comment"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.btnDiscard)
        Me.Panel2.Controls.Add(Me.btnDiscard_All)
        Me.Panel2.Controls.Add(Me.btnClear)
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 561)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1066, 32)
        Me.Panel2.TabIndex = 236
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.spdManage)
        Me.Panel3.Location = New System.Drawing.Point(4, 36)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1053, 467)
        Me.Panel3.TabIndex = 237
        '
        'spdManage
        '
        'Me.spdManage.DataSource = Nothing
        Me.spdManage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spdManage.Location = New System.Drawing.Point(0, 0)
        Me.spdManage.Name = "spdManage"
        Me.spdManage.OcxState = CType(resources.GetObject("spdManage.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spdManage.Size = New System.Drawing.Size(1053, 467)
        Me.spdManage.TabIndex = 0
        '
        'FGB12
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1066, 593)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtAlarm)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtMCol)
        Me.Controls.Add(Me.txtMRow)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboRackID)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.KeyPreview = True
        Me.Name = "FGB12"
        Me.Text = "보관 검체 관리"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.spdManage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnExit_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Get_RackID()        ' RackID 가져오기
        Dim dt As DataTable = fnGet_KsRackInfo()

        If dt.Rows.Count > 0 Then
            For ix As Integer = 0 To dt.Rows.Count - 1
                Me.cboRackID.Items.Add(dt.Rows(ix).Item("rackid").ToString())
                Me.cboToRack_ID.Items.Add(dt.Rows(ix).Item("rackid").ToString())
            Next
        Else
            fn_PopMsg(Me, "I"c, "보관검체 Rack ID를 가져오지 못했습니다")
        End If

    End Sub

    Private Sub cboRackID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRackID.SelectedIndexChanged
        Dim sfn As String = "cboRackID_SelectedIndexChanged"
        Try
            Dim sRackID As String = cboRackID.SelectedItem.ToString()

            m_i_RackId_idx = Me.cboRackID.SelectedIndex  ' 다른 곳으로 이동시키는 경우 강제로 이벤트 발생하여 다시 화면에 뿌려주기 위해!!!

            ClearData_All()

            Dim dt As DataTable = fnGet_KsRackInfo(sRackID)

            If dt.Rows.Count > 0 Then
                With dt.Rows(0)
                    objKeepBcno.Bcclscd = .Item("bcclscd").ToString()
                    objKeepBcno.RackId = .Item("rackid").ToString()
                    objKeepBcno.SpcCd = .Item("spccd").ToString()
                    objKeepBcno.RegDt = .Item("regdt").ToString()
                    objKeepBcno.RegId = .Item("regid").ToString()
                    Me.txtMRow.Text = .Item("maxrow").ToString()
                    txtMCol.Text = .Item("maxcol").ToString()
                    Me.objKeepBcno.AlarmTerm = .Item("alarmterm").ToString() : txtAlarm.Text = objKeepBcno.AlarmTerm
                End With

                With spdManage
                    .MaxRows = CType(txtMRow.Text.Trim, Integer) : Max_Row = .MaxRows
                    .MaxCols = CType(txtMCol.Text.Trim, Integer) : Max_Col = .MaxCols
                    .Refresh()  ' 안해도 무방함 ㅡ.ㅡa

                    .ClearRange(1, 1, .MaxCols, .MaxRows, True)  ' 새 화면으로 clear
                    .BlockMode = True
                    .Col = 1 : .Col2 = .MaxCols : .Row = 1 : .Row2 = .MaxRows
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False

                    .ReDraw = False

                    cboToCol.Items.Clear()
                    cboToRow.Items.Clear()
                    For i As Integer = 1 To .MaxRows
                        For j As Integer = 1 To .MaxCols
                            .set_ColWidth(j, 13)
                            .set_RowHeight(i, 40)

                            cboToCol.Items.Add(j.ToString)
                        Next

                        cboToRow.Items.Add(i.ToString)
                    Next

                    .ReDraw = True

                End With

                ' spread에 보관 검체들을 보여준다. 
                dt = fnGet_KsBcnoInfo(objKeepBcno)

                If dt.Rows.Count > 0 Then
                    Shwo_BcnoList(dt)
                Else
                    Exit Sub
                End If
            Else
                'MsgBox("데이터가 존재하지 않습니다. 다시 확인하세요", MsgBoxStyle.Information, Me.Text)
                fn_PopMsg(Me, "I"c, "데이터가 존재하지 않습니다. 다시 확인하세요")
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try


    End Sub

    Private Sub Shwo_BcnoList(ByVal as_BcnoList As DataTable)   ' 보관 검체들 보여주기
        Dim strBcno As String = ""
        Dim strInsert_BCNO As String = ""

        Dim objBcnoTable As DataTable
        Dim PastBcno As String = ""     ' 경과시간
        Dim strPastDt As String = ""    ' 채혈시간
        Dim strAlarm As String = txtAlarm.Text.Trim      ' Alarm 표시일
        Dim NowTime As Date = objComm.GetDateTime

        Dim dbHour As Double
        If Not strAlarm.Equals("") Then  ' 알람 표시일이 있을경우
            dbHour = CType(txtAlarm.Text.Trim, Double) * 24
        Else
            dbHour = 0
        End If

        With spdManage
            For intRow As Integer = 0 To Max_Row - 1
                For iRow As Integer = 0 To as_BcnoList.Rows.Count - 1
                    If CType(as_BcnoList.Rows(iRow).Item("NUMROW"), Integer) = intRow + 1 Then
                        For intCol As Integer = 0 To Max_Col - 1
                            For iCol As Integer = 0 To as_BcnoList.Columns.Count - 1
                                If CType(as_BcnoList.Rows(iRow).Item("NUMCOL"), Integer) = intCol + 1 Then
                                    strBcno = as_BcnoList.Rows(iRow).Item("BCNO").ToString()
                                    strInsert_BCNO = strBcno.Substring(2, 6) & "-" & strBcno.Substring(8, 2) & "-" & _
                                                     strBcno.Substring(10, 4) & "-" & strBcno.Substring(14, 1)
                                    .Col = intCol + 1
                                    .Row = intRow + 1
                                    .Text = strInsert_BCNO

                                    '  Alarm 표시일보다 경과시간이 큰 경우 검체번호를 빨간색으로 보여주자.
                                    If dbHour <> 0 Then     ' Alarm 표시일이 있을경우
                                        objBcnoTable = fnGet_KsBcnoInfo(strBcno, objKeepBcno)

                                        If objBcnoTable.Rows.Count > 0 Then
                                            strPastDt = objBcnoTable.Rows(0).Item("COLLDT").ToString()
                                        Else
                                            Exit For
                                        End If

                                        If strPastDt = "" Then
                                            .BlockMode = True
                                            .Col = intCol + 1 : .Col2 = intCol + 1 : .Row = intRow + 1 : .Row2 = intRow + 1
                                            .ForeColor = System.Drawing.Color.Yellow
                                            .BackColor = System.Drawing.Color.Gray
                                            .BlockMode = False
                                        Else

                                            Dim PastTime As Double
                                            PastTime = DateDiff(DateInterval.Hour, CType(strPastDt, Date), NowTime)

                                            If dbHour <= PastTime Then
                                                .BlockMode = True
                                                .Col = intCol + 1 : .Col2 = intCol + 1 : .Row = intRow + 1 : .Row2 = intRow + 1
                                                .ForeColor = System.Drawing.Color.Red
                                                .BlockMode = False
                                            End If
                                        End If
                                    End If
                                End If
                            Next
                        Next
                    End If
                Next
            Next
        End With

    End Sub

    Private Sub spdManage_KeyDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_KeyDownEvent) Handles spdManage.KeyDownEvent
        If e.keyCode <> Windows.Forms.Keys.Enter Then Return

        Try
            Dim strBCNO As String = ""
            Dim strInsert_BCNO As String = ""
            Dim objCommDBFN As New LISAPP.APP_DB.DbFn
            Dim objData As DataTable
            Dim strSectId As String = ""

            With Me.spdManage
                .Col = .ActiveCol
                .Row = .ActiveRow
                strBCNO = .Text

                ' 동일한 자리에 이미 검체가 존재하는지 여부를 판별
                strSectId = CStr(cboRackID.SelectedItem)
                If strSectId Is Nothing Then
                    fn_PopMsg(Me, "I"c, "Rack ID를 선택하세요.!!")
                    Return
                End If
                objData = fnGet_Bcno_YesNo(CType(.ActiveRow, String), CType(.ActiveCol, String), strSectId)
                If objData IsNot Nothing Then
                    If objData.Rows.Count > 0 Then
                        fn_PopMsg(Me, "I"c, "보관 검체가 존재합니다. 다른곳을 선택하세요")
                        cboRackID_SelectedIndexChanged(m_i_RackId_idx, Nothing)
                        Exit Sub
                    End If
                End If
            End With

            With Me.spdManage
                If Not strBCNO.Equals("") Then
                    If strBCNO.Length.Equals(11) Then     ' 바코드로 찍은 경우 -> 적합한 검체번호 형식으로 바꿔줘야 함!
                        strBCNO = objCommDBFN.GetBCPrtToView(strBCNO)

                        strInsert_BCNO = strBCNO.Substring(2, 6) & "-" & strBCNO.Substring(8, 2) & "-" & strBCNO.Substring(10, 4) & _
                                         "-" & strBCNO.Substring(14, 1)
                    ElseIf strBCNO.Length.Equals(15) Then   ' 앞의 년도 2자리를 빼고 보여줘야 함!
                        strInsert_BCNO = strBCNO.Substring(2, 6) & "-" & strBCNO.Substring(8, 2) & "-" & strBCNO.Substring(10, 4) & _
                                         "-" & strBCNO.Substring(14, 1)
                    Else
                        fn_PopMsg(Me, "I"c, "잘못된 검체번호입니다. 다시 확인하세요")
                        'MsgBox("잘못된 검체번호입니다. 다시 확인하세요", MsgBoxStyle.Information, Me.Text)
                        .ClearRange(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False)
                        Exit Sub
                    End If

                    .Col = .ActiveCol : objKeepBcno.NumCol = CType(.ActiveCol, String)
                    .Row = .ActiveRow : objKeepBcno.NumRow = CType(.ActiveRow, String)
                    .Text = strInsert_BCNO.ToUpper()

                    Insert_KeepBcno(objKeepBcno, strBCNO.ToUpper, txtComment.Text.Trim)  ' LK010M에 보관검체정보 Insert

                    'fn_PopMsg(Me, "I"c, "저장 되었습니다.")


                    '.SetActiveCell(.ActiveCol + 1, .Row) '<20131111 보관검체 입력시에 셀 이동

                End If


            End With
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdManage_ClickEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_ClickEvent) Handles spdManage.ClickEvent
        'Debug.WriteLine("clickEvent")
        Try
            txtBCNO.Text = "" : txtRegNo.Text = "" : txtName.Text = "" : txtTime.Text = ""

            Dim strBCNO As String = ""
            Dim objTable As DataTable
            Dim RegDate As Date
            RegDate = objComm.GetDateTime
            Dim RegYear As String = ""
            RegYear = Year(RegDate).ToString.Substring(0, 2)    ' 년도만 가져온다

            With spdManage
                .Col = e.col : .Col2 = e.col : .Row = e.row : .Row2 = e.row
                If .BackColor.Equals(System.Drawing.Color.White) Then
                    txtComment.Text = ""
                End If
            End With

            With spdManage
                .Row = e.row : .Col = e.col
                strBCNO = .Text

                ' 화면을 새로 여는경우 초기값 0을 가지고 있음 (바로전의 클릭된것을 기억하고 있다가 다른cell이 클릭되면 white로 색상변경)
                If Click_Col <> 0 And Click_Row <> 0 Then
                    .BlockMode = True
                    .Col = Click_Col : .Col2 = Click_Col : .Row = Click_Row : .Row2 = Click_Row
                    .BackColor = System.Drawing.Color.White
                    .BlockMode = False
                End If

                .BlockMode = True
                .Col = .ActiveCol : .Col2 = .ActiveCol : .Row = .ActiveRow : .Row2 = .ActiveRow
                .BackColor = System.Drawing.Color.Pink
                .BlockMode = False

                Click_Col = e.col
                Click_Row = e.row

            End With

            If strBCNO <> "" Then   ' 검체번호가 존재할때 -> LJ010M 에서 보관검체정보를 갖고와서 보여준다.
                strRealBcno = RegYear & strBCNO.Replace("-", "")    ' 실제로 넘겨줄 완벽한 검체번호의 형태
                objTable = fnGet_KsBcnoInfo(strRealBcno, objKeepBcno)

                If objTable.Rows.Count > 0 Then
                    ShowBCNO_Info(objTable)

                    objTable = fnGet_KsRackInfo(Me.cboRackID.SelectedItem.ToString())

                    If objTable.Rows.Count > 0 Then
                        With objTable.Rows(0)
                            objToKeepBcno.Bcclscd = .Item("BCCLSCD").ToString()
                            objToKeepBcno.RackId = .Item("RACKID").ToString()
                            objToKeepBcno.SpcCd = .Item("SPCCD").ToString()
                            objToKeepBcno.RegDt = .Item("REGDT").ToString()
                            objToKeepBcno.RegId = .Item("REGID").ToString()
                        End With
                    End If

                Else
                    fn_PopMsg(Me, "I"c, "잘못된 검체번호 입니다. 다시 확인하세요")
                    'MsgBox("잘못된 검체번호 입니다. 다시 확인하세요", MsgBoxStyle.Information, Me.Text)
                End If
            Else    ' 비어있는 Cell인 경우 
                Exit Sub
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub ShowBCNO_Info(ByVal as_objTable As DataTable)
        Dim objDTable As DataTable = as_objTable
        Dim strBcno As String = ""
        Dim strRegNo As String = ""
        Dim strPastDt As String = ""        ' 채혈시간 (COLLDT) -> 현재시간과의 차를 구하여 경과시간을 알아냄

        Dim NowTime As Date
        NowTime = objComm.GetDateTime

        With objDTable.Rows(0)
            strBcno = .Item("BCNO").ToString()
            strPastDt = .Item("COLLDT").ToString()
            strRegNo = .Item("REGNO").ToString()

            txtBCNO.Text = strBcno.Substring(0, 8) & "-" & strBcno.Substring(8, 2) & "-" & strBcno.Substring(10, 4)

            If Not strRegNo.Equals("") Then
                txtRegNo.Text = .Item("REGNO").ToString()
            End If
            txtName.Text = .Item("PATNM").ToString()
            txtComment.Text = .Item("OTHER").ToString()
            txtTime.Text = Fn.TimeElapsed(CType(strPastDt, Date), NowTime)
        End With

    End Sub

    Private Sub btnClear_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
        ClearData_All()
    End Sub

    Private Sub ClearData_All()
        txtMRow.Text = "" : txtMCol.Text = "" : txtAlarm.Text = "" : txtComment.Text = ""
        With spdManage
            .MaxRows = 10 : .MaxCols = 10
            .ClearRange(1, 1, .MaxCols, .MaxRows, False)
        End With
        txtBCNO.Text = "" : txtRegNo.Text = "" : txtName.Text = "" : txtTime.Text = ""

        cboToRow.SelectedIndex = -1 : cboToCol.SelectedIndex = -1

    End Sub

    Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
        If fnExe_KeepBcnoComment(txtComment.Text.Trim, strRealBcno, objKeepBcno) = True Then
            fn_PopMsg(Me, "I"c, "정상적으로 저장되었습니다")
        Else
            Exit Sub
        End If
    End Sub

    Private Sub cboToRack_ID_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboToRack_ID.SelectedIndexChanged
        Dim objToTable As DataTable
        strToRackID = cboToRack_ID.SelectedItem.ToString()

        Dim strMaxRow As String = ""    ' 이동할 rack의 MaxRow
        Dim strMaxCol As String = ""    ' 이동할 rack의 MaxCol

        objToTable = fnGet_KsRackInfo(strToRackID)

        If objToTable.Rows.Count > 0 Then
            With objToTable.Rows(0)
                objToKeepBcno.Bcclscd = .Item("BCCLSCD").ToString()
                objToKeepBcno.RackId = .Item("RACKID").ToString()
                objToKeepBcno.SpcCd = .Item("SPCCD").ToString()
                objToKeepBcno.RegDt = .Item("REGDT").ToString()
                objToKeepBcno.RegId = .Item("REGID").ToString()

                strMaxRow = .Item("MAXROW").ToString()
                strMaxCol = .Item("MAXCOL").ToString()
            End With
        Else
            fn_PopMsg(Me, "I"c, "데이터가 없습니다. 다시 선택해주세요")
            'MsgBox("데이터가 없습니다. 다시 선택해주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        cboToRow.Items.Clear()      ' combobox 안의 내용을 clear시킴
        cboToCol.Items.Clear()

        For iRow As Integer = 1 To CType(strMaxRow, Integer)     ' ex) maxrow가 10 이면 1~10 까지 add시킨다
            cboToRow.Items.Add(iRow)
        Next
        For iCol As Integer = 1 To CType(strMaxCol, Integer)
            cboToCol.Items.Add(iCol)
        Next

    End Sub

    Private Sub btnMove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMove.Click
        Dim strToRow As String = ""     ' 이동할 rack의 Row
        Dim strToCol As String = ""

        If cboToRow.Text = "" Or cboToCol.Text = "" Then
            fn_PopMsg(Me, "I"c, "이동할 위치를 올바르게 선택해 주세요")
            'MsgBox("이동할 위치를 올바르게 선택해 주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        strToRow = cboToRow.SelectedItem.ToString() : objToKeepBcno.NumRow = strToRow
        strToCol = cboToCol.SelectedItem.ToString() : objToKeepBcno.NumCol = strToCol

        Bcno_Move(strToRow, strToCol)

    End Sub

    Private Sub Bcno_Move(ByVal as_Row As String, ByVal as_Col As String)
        Dim objTable As DataTable

        ' 새로운 위치에 이미 샘플이 있는지 체크한뒤 없으면 이동하기!!
        objTable = fnGet_Bcno_YesNo(as_Row, as_Col, , objToKeepBcno)

        If objTable.Rows.Count > 0 Then  ' 검체 있는 경우
            fn_PopMsg(Me, "I"c, "보관 검체가 존재합니다. 다른곳을 선택하세요")
            'MsgBox("보관 검체가 존재합니다. 다른곳을 선택하세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        Else    ' 선택한 곳으로 검체 insert -> 기존 위치의 정보 delete 
            If strRealBcno <> "" Then
                If InsertBcno_NewPlace(objKeepBcno, strRealBcno, objToKeepBcno, strComment) = True Then
                    cboRackID_SelectedIndexChanged(m_i_RackId_idx, Nothing)       ' Refresh 기능

                    fn_PopMsg(Me, "I"c, "정상적으로 이동되었습니다")
                    'MsgBox("정상적으로 이동되었습니다", MsgBoxStyle.Information, Me.Text)
                Else
                    fn_PopMsg(Me, "I"c, "이동에 실패했습니다. 다시 시도하세요")
                    'MsgBox("이동에 실패했습니다. 다시 시도하세요", MsgBoxStyle.Information, Me.Text)
                    Exit Sub
                End If
            Else
                fn_PopMsg(Me, "I"c, "이동할 검체를 선택해 주세요")
                'MsgBox("이동할 검체를 선택해 주세요", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        End If

    End Sub

    Private Sub btnDiscard_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiscard.Click
        Dim strBCNO As String = ""
        Dim lb_Continue As Boolean = False

        With spdManage
            .Row = .ActiveRow : .Col = .ActiveCol
            strBCNO = .Text
        End With

        If Not strBCNO.Equals("") Then
            lb_Continue = fn_PopConfirm(Me, "I"c, "선택한 검체를 폐기하시겠습니까?")

            If lb_Continue = True Then
                If Discard_Bcno(objKeepBcno, strRealBcno) = True Then
                    cboRackID_SelectedIndexChanged(m_i_RackId_idx, Nothing)
                    With spdManage
                        .ClearRange(.ActiveCol, .ActiveRow, .ActiveCol, .ActiveRow, False)

                        .BlockMode = True
                        .Col = .ActiveCol : .Col2 = .ActiveCol : .Row = .ActiveRow : .Row2 = .ActiveRow
                        .BackColor = System.Drawing.Color.White
                        .BlockMode = False
                    End With

                    fn_PopMsg(Me, "I"c, "정상적으로 폐기되었습니다")
                    'MsgBox("정상적으로 폐기되었습니다", MsgBoxStyle.Information, Me.Text)

                Else
                    fn_PopMsg(Me, "I"c, "폐기되지 못했습니다. 다시 시도하세요")
                    'MsgBox("폐기되지 못했습니다. 다시 시도하세요", MsgBoxStyle.Information, Me.Text)
                    Exit Sub

                End If
            Else
                Exit Sub
            End If
        Else
            fn_PopMsg(Me, "I"c, "폐기할 검체를 선택해 주세요")
            'MsgBox("폐기할 검체를 선택해 주세요", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

    End Sub

    Private Sub btnDiscard_All_ButtonClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDiscard_All.Click
        Dim arrBcnoList As New ArrayList
        Dim objBcnoTable As DataTable

        Dim lb_Continue As Boolean = False

        lb_Continue = fn_PopConfirm(Me, "I"c, "일괄폐기 하시겠습니까?")

        If lb_Continue = False Then Return


        Try
            objBcnoTable = fnGet_KsBcnoInfo(objKeepBcno)

            If objBcnoTable.Rows.Count > 0 Then
                For intCnt As Integer = 0 To objBcnoTable.Rows.Count - 1
                    arrBcnoList.Add(objBcnoTable.Rows(intCnt).Item("BCNO").ToString)
                Next
            End If

            If DiscardAll_Bcno(objKeepBcno, arrBcnoList) = True Then
                fn_PopMsg(Me, "I"c, "정상적으로 폐기되었습니다")
                'MsgBox("정상적으로 폐기되었습니다", MsgBoxStyle.Information, Me.Text)

                cboRackID_SelectedIndexChanged(m_i_RackId_idx, Nothing)
                With spdManage
                    .ClearRange(1, 1, .MaxCols, .MaxRows, False)
                End With
            Else
                fn_PopMsg(Me, "I"c, "폐기되지 못했습니다. 다시 시도하세요")
                'MsgBox("폐기되지 못했습니다. 다시 시도하세요", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Sub spdManage_MouseDownEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseDownEvent) Handles spdManage.MouseDownEvent
        'Debug.WriteLine("DN :" & e.x.ToString & ", " & e.y.ToString)

        Dim CR As New RowCol
        CR = fnGetColRow(e.x, e.y)
        'Debug.WriteLine("DN :" & CR.Col.ToString & ", " & CR.Row.ToString)

        i_DownRow = CR.Row
        i_DownCol = CR.Col

        ' **** add *********************************************************
        With spdManage
            .Col = i_DownRow
            .Row = i_DownCol
            strRealBcno = .Text.Trim
        End With

        If strRealBcno.Equals("") Then
            strRealBcno = ""
        End If
        ' ******************************************************************

    End Sub

    Private Sub spdManage_MouseUpEvent(ByVal sender As Object, ByVal e As AxFPSpreadADO._DSpreadEvents_MouseUpEvent) Handles spdManage.MouseUpEvent
        ' Debug.WriteLine("UP" & e.x.ToString & ", " & e.y.ToString)

        Try
            Dim CR As New RowCol
            CR = fnGetColRow(e.x, e.y)
            'Debug.WriteLine("UP :" & CR.Col.ToString & ", " & CR.Row.ToString)

            If i_DownRow = CR.Row And i_DownCol = CR.Col Then       ' 그 자리 클릭 -> MouseUpEvent 타지 않게 한다.
                Exit Sub
            End If

            Dim objToTable As DataTable
            Dim strMaxRow As String = ""    ' 이동할 rack의 MaxRow
            Dim strMaxCol As String = ""    ' 이동할 rack의 MaxCol

            objToTable = fnGet_KsRackInfo(cboRackID.SelectedItem.ToString())

            If objToTable.Rows.Count > 0 Then
                With objToTable.Rows(0)
                    objToKeepBcno.Bcclscd = .Item("bcclscd").ToString()
                    objToKeepBcno.RackId = .Item("rackid").ToString()
                    objToKeepBcno.SpcCd = .Item("spccd").ToString()
                    objToKeepBcno.RegDt = .Item("regdt").ToString()
                    objToKeepBcno.RegId = .Item("regid").ToString()

                    objToKeepBcno.NumRow = CR.Row.ToString
                    objToKeepBcno.NumCol = CR.Col.ToString
                End With
            End If

            Bcno_Move(CR.Row.ToString, CR.Col.ToString)

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)
        End Try

    End Sub

    Private Class RowCol
        Public Col As Integer
        Public Row As Integer

        Public Sub New()

        End Sub
    End Class

    Private Function fnGetColRow(ByVal aiX As Integer, ByVal aiY As Integer) As RowCol
        Dim sfn As String = "Private Function fnGetColRow(ByVal aiX As Integer, ByVal aiY As Integer) As RowCol"

        Try
            Dim CR As New RowCol
            Dim SpdWidth As Integer = spdManage.Width - 22 - 18
            Dim SpdHeight As Integer = spdManage.Height - 23 - 18
            Dim intCellWidth As Integer
            Dim intCellHeight As Integer

            With CR
                intCellWidth = CInt(Fix(SpdWidth / 10))
                intCellHeight = CInt(Fix(SpdHeight / 10))

                'intCellWidth = CInt(Fix(SpdWidth / spdManage.MaxCols))
                'intCellHeight = CInt(Fix(SpdHeight / spdManage.MaxRows))
                'Debug.WriteLine(" Cell W, H : " & intCellWidth.ToString & ", " & intCellHeight.ToString)

                .Col = CInt(Fix((aiX - 22) / intCellWidth)) + 1
                .Row = CInt(Fix((aiY - 23) / intCellHeight)) + 1
            End With

            fnGetColRow = CR

        Catch ex As Exception
            fn_PopMsg(Me, "E"c, ex.Message)

        End Try

    End Function

    Private Sub FGB12_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MdiTabControl.sbTabPageMove(Me)
    End Sub

    Private Sub FGB12_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.F4
                btnClear_ButtonClick(Nothing, Nothing)
            Case Keys.Escape
                btnExit_ButtonClick(Nothing, Nothing)
        End Select
    End Sub

    Private Sub txtSerach_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.Click
        txtSearch.SelectAll()
        txtSearch.SelectionStart = 0
    End Sub

    Private Sub txtSerach_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        txtSearch.SelectAll()
        txtSearch.SelectionStart = 0
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            Dim sRegNo As String = txtSearch.Text.Trim

            If IsNumeric(sRegNo.Substring(0, 1)) Then
                sRegNo = sRegNo.PadLeft(PRG_CONST.Len_RegNo, "0"c)
            Else
                sRegNo = sRegNo.Substring(0, 1).ToUpper + sRegNo.Substring(1).PadLeft(PRG_CONST.Len_RegNo - 1, "0"c)
            End If

            txtSearch.Text = sRegNo

            ' spread에 보관 검체들을 보여준다. 
            Dim dt As DataTable = fnGet_KsBcnoInfo_regno(txtSearch.Text)

            Dim pntCtlXY As New Point
            Dim pntFrmXY As New Point

            Dim objHelp As New CDHELP.FGCDHELP01
            Dim aryList As New ArrayList

            objHelp.FormText = "보관검체 정보"
            objHelp.MaxRows = 15
            objHelp.Distinct = True
            objHelp.OnRowReturnYN = False

            'bcno, rackid, numrow, numcol, other
            objHelp.AddField("rackid", "Rack ID", 8, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("numrow", "가로", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("numcol", "세로", 6, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter)
            objHelp.AddField("bcno", "검체번호", 15, FPSpreadADO.TypeHAlignConstants.TypeHAlignCenter, , , "bcno")
            objHelp.AddField("other", "Comment", 20, FPSpreadADO.TypeHAlignConstants.TypeHAlignLeft)

            pntFrmXY = Fn.CtrlLocationXY(Me)
            pntCtlXY = Fn.CtrlLocationXY(btnSearch)

            aryList = objHelp.Display_Result(Me, pntFrmXY.X + pntCtlXY.X - btnSearch.Left, pntFrmXY.Y + pntCtlXY.Y + btnSearch.Height + 80, dt)

            If aryList.Count > 0 Then

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FGB12_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.txtRegNo.MaxLength = PRG_CONST.Len_RegNo()
    End Sub

  
End Class
