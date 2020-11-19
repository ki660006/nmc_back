Public Class PATINFO
    Inherits System.Windows.Forms.Form

    Public RegNo As String = ""
    Public PatNm As String = ""
    Public SexAge As String = ""
    Public IdNo As String = ""

    Public OrdDt As String = ""
    Public DeptNm As String = ""
    Public DoctorNm As String = ""
    Public WardRoom As String = ""
    Public InWonDate As String = ""

    Public Tel As String = ""
    Public Addr1 As String = ""
    Public Addr2 As String = ""

    Private Const mc_iTerm As Integer = 30
    Private m_timer As Threading.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents txtInown As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Private miTimer As Integer = 0

    Public Sub Display_PatInfo()
        Me.txtRegNo.Text = RegNo
        Me.txtPatNm.Text = PatNm
        Me.txtSexAge.Text = SexAge
        Me.txtIdNo.Text = IdNo

        Me.txtOrdDt.Text = OrdDt
        Me.txtDeptNm.Text = DeptNm
        Me.txtDoctorNm.Text = DoctorNm
        Me.txtWardRoom.Text = WardRoom

        If InWonDate.Trim = "/" Then InWonDate = ""
        Me.txtInown.Text = InWonDate

        Me.txtTel.Text = Tel
        Me.txtAddr1.Text = Addr1
        Me.txtAddr2.Text = Addr2

        'm_timer = New Threading.Timer(AddressOf sbTimerProc, Nothing, 0, 1000)
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbInitialize()
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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtIdNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRegNo As System.Windows.Forms.TextBox
    Friend WithEvents txtPatNm As System.Windows.Forms.TextBox
    Friend WithEvents txtSexAge As System.Windows.Forms.TextBox
    Friend WithEvents txtOrdDt As System.Windows.Forms.TextBox
    Friend WithEvents txtDoctorNm As System.Windows.Forms.TextBox
    Friend WithEvents txtDeptNm As System.Windows.Forms.TextBox
    Friend WithEvents txtWardRoom As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTel As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr1 As System.Windows.Forms.TextBox
    Friend WithEvents txtAddr2 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtSexAge = New System.Windows.Forms.TextBox
        Me.txtPatNm = New System.Windows.Forms.TextBox
        Me.txtRegNo = New System.Windows.Forms.TextBox
        Me.txtIdNo = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtInown = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtDoctorNm = New System.Windows.Forms.TextBox
        Me.txtDeptNm = New System.Windows.Forms.TextBox
        Me.txtOrdDt = New System.Windows.Forms.TextBox
        Me.txtWardRoom = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtAddr2 = New System.Windows.Forms.TextBox
        Me.txtAddr1 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtTel = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSexAge)
        Me.GroupBox1.Controls.Add(Me.txtPatNm)
        Me.GroupBox1.Controls.Add(Me.txtRegNo)
        Me.GroupBox1.Controls.Add(Me.txtIdNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(210, 136)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "신상정보"
        '
        'txtSexAge
        '
        Me.txtSexAge.BackColor = System.Drawing.Color.White
        Me.txtSexAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSexAge.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtSexAge.Location = New System.Drawing.Point(91, 62)
        Me.txtSexAge.Name = "txtSexAge"
        Me.txtSexAge.ReadOnly = True
        Me.txtSexAge.Size = New System.Drawing.Size(107, 21)
        Me.txtSexAge.TabIndex = 2
        Me.txtSexAge.Text = "710101-1234567"
        Me.txtSexAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPatNm
        '
        Me.txtPatNm.BackColor = System.Drawing.Color.White
        Me.txtPatNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPatNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtPatNm.Location = New System.Drawing.Point(91, 40)
        Me.txtPatNm.Name = "txtPatNm"
        Me.txtPatNm.ReadOnly = True
        Me.txtPatNm.Size = New System.Drawing.Size(107, 21)
        Me.txtPatNm.TabIndex = 1
        Me.txtPatNm.Text = "710101-1234567"
        Me.txtPatNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtRegNo
        '
        Me.txtRegNo.BackColor = System.Drawing.Color.White
        Me.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRegNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtRegNo.Location = New System.Drawing.Point(91, 18)
        Me.txtRegNo.Name = "txtRegNo"
        Me.txtRegNo.ReadOnly = True
        Me.txtRegNo.Size = New System.Drawing.Size(107, 21)
        Me.txtRegNo.TabIndex = 0
        Me.txtRegNo.Text = "710101-1234567"
        Me.txtRegNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtIdNo
        '
        Me.txtIdNo.BackColor = System.Drawing.Color.White
        Me.txtIdNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNo.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtIdNo.Location = New System.Drawing.Point(91, 84)
        Me.txtIdNo.Name = "txtIdNo"
        Me.txtIdNo.ReadOnly = True
        Me.txtIdNo.Size = New System.Drawing.Size(107, 21)
        Me.txtIdNo.TabIndex = 3
        Me.txtIdNo.Text = "710101-1234567"
        Me.txtIdNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(10, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 21)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "주민등록번호"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(10, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 21)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Sex/Age"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(10, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 21)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "성명"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 21)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "등록번호"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtInown)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtDoctorNm)
        Me.GroupBox2.Controls.Add(Me.txtDeptNm)
        Me.GroupBox2.Controls.Add(Me.txtOrdDt)
        Me.GroupBox2.Controls.Add(Me.txtWardRoom)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.GroupBox2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(123, Byte), Integer))
        Me.GroupBox2.Location = New System.Drawing.Point(220, 6)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(246, 136)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "처방정보"
        '
        'txtInown
        '
        Me.txtInown.BackColor = System.Drawing.Color.White
        Me.txtInown.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInown.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtInown.Location = New System.Drawing.Point(91, 106)
        Me.txtInown.Name = "txtInown"
        Me.txtInown.ReadOnly = True
        Me.txtInown.Size = New System.Drawing.Size(146, 21)
        Me.txtInown.TabIndex = 9
        Me.txtInown.Text = "2010-07-08/2010-08-20"
        Me.txtInown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label11.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(10, 106)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(80, 21)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "입원/퇴원"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDoctorNm
        '
        Me.txtDoctorNm.BackColor = System.Drawing.Color.White
        Me.txtDoctorNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDoctorNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDoctorNm.Location = New System.Drawing.Point(91, 62)
        Me.txtDoctorNm.Name = "txtDoctorNm"
        Me.txtDoctorNm.ReadOnly = True
        Me.txtDoctorNm.Size = New System.Drawing.Size(146, 21)
        Me.txtDoctorNm.TabIndex = 2
        Me.txtDoctorNm.Text = "710101-1234567"
        Me.txtDoctorNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtDeptNm
        '
        Me.txtDeptNm.BackColor = System.Drawing.Color.White
        Me.txtDeptNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDeptNm.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtDeptNm.Location = New System.Drawing.Point(91, 40)
        Me.txtDeptNm.Name = "txtDeptNm"
        Me.txtDeptNm.ReadOnly = True
        Me.txtDeptNm.Size = New System.Drawing.Size(146, 21)
        Me.txtDeptNm.TabIndex = 1
        Me.txtDeptNm.Text = "710101-1234567"
        Me.txtDeptNm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtOrdDt
        '
        Me.txtOrdDt.BackColor = System.Drawing.Color.White
        Me.txtOrdDt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtOrdDt.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtOrdDt.Location = New System.Drawing.Point(91, 18)
        Me.txtOrdDt.Name = "txtOrdDt"
        Me.txtOrdDt.ReadOnly = True
        Me.txtOrdDt.Size = New System.Drawing.Size(146, 21)
        Me.txtOrdDt.TabIndex = 0
        Me.txtOrdDt.Text = "710101-1234567"
        Me.txtOrdDt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtWardRoom
        '
        Me.txtWardRoom.BackColor = System.Drawing.Color.White
        Me.txtWardRoom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtWardRoom.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtWardRoom.Location = New System.Drawing.Point(91, 84)
        Me.txtWardRoom.Name = "txtWardRoom"
        Me.txtWardRoom.ReadOnly = True
        Me.txtWardRoom.Size = New System.Drawing.Size(146, 21)
        Me.txtWardRoom.TabIndex = 3
        Me.txtWardRoom.Text = "710101-1234567"
        Me.txtWardRoom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label9.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(10, 84)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 21)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "병동/병실"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 21)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "의뢰의사"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(10, 40)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 21)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "진료과"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(10, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(80, 21)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "처방일시"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(8, 245)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(454, 30)
        Me.btnClose.TabIndex = 2
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtAddr2)
        Me.GroupBox3.Controls.Add(Me.txtAddr1)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtTel)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 137)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(457, 93)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'txtAddr2
        '
        Me.txtAddr2.BackColor = System.Drawing.Color.White
        Me.txtAddr2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr2.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAddr2.Location = New System.Drawing.Point(91, 62)
        Me.txtAddr2.Name = "txtAddr2"
        Me.txtAddr2.ReadOnly = True
        Me.txtAddr2.Size = New System.Drawing.Size(358, 21)
        Me.txtAddr2.TabIndex = 9
        Me.txtAddr2.Text = "710101-1234567"
        '
        'txtAddr1
        '
        Me.txtAddr1.BackColor = System.Drawing.Color.White
        Me.txtAddr1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtAddr1.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtAddr1.Location = New System.Drawing.Point(91, 40)
        Me.txtAddr1.Name = "txtAddr1"
        Me.txtAddr1.ReadOnly = True
        Me.txtAddr1.Size = New System.Drawing.Size(358, 21)
        Me.txtAddr1.TabIndex = 7
        Me.txtAddr1.Text = "710101-1234567"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label10.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(10, 40)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 21)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "주소"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTel
        '
        Me.txtTel.BackColor = System.Drawing.Color.White
        Me.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTel.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtTel.Location = New System.Drawing.Point(91, 18)
        Me.txtTel.Name = "txtTel"
        Me.txtTel.ReadOnly = True
        Me.txtTel.Size = New System.Drawing.Size(358, 21)
        Me.txtTel.TabIndex = 5
        Me.txtTel.Text = "710101-1234567"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label8.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(10, 18)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(80, 21)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "연락처"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1000
        '
        'PATINFO
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(474, 291)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PATINFO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "최신 환자정보"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbInitialize()
        Me.txtRegNo.Text = ""
        Me.txtPatNm.Text = ""
        Me.txtSexAge.Text = ""
        Me.txtIdNo.Text = ""

        Me.txtOrdDt.Text = ""
        Me.txtDeptNm.Text = ""
        Me.txtDoctorNm.Text = ""
        Me.txtWardRoom.Text = ""

        Me.txtTel.Text = ""
        Me.txtAddr1.Text = ""
        Me.txtAddr2.Text = ""
    End Sub

    Private Sub sbTimerProc(ByVal state As Object)
        If mc_iTerm - miTimer < 1 Then
            If Not m_timer Is Nothing Then
                m_timer.Change(Threading.Timeout.Infinite, Threading.Timeout.Infinite)
                m_timer.Dispose()
            End If

            Me.Close()

            Return
        End If

        Me.btnClose.Text = (mc_iTerm - miTimer).ToString & "초 후에 자동으로 사라집니다.  지금 종료하려면 여기를 누르세요."
        Me.btnClose.Refresh()

        miTimer += 1
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        miTimer = mc_iTerm

        sbTimerProc(Nothing)
    End Sub

    Private Sub PATINFO_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If mc_iTerm - miTimer < 1 Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        sbTimerProc(Nothing)

    End Sub
End Class
