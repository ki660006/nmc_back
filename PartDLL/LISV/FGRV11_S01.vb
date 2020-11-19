
Public Class FGRV11_S01
    Inherits System.Windows.Forms.Form

    Public msRegNo As String = ""
    Public msExamCd As String = ""
    Public msExamNm As String = ""
    Public msEndDate As String = ""
    Public msRefTxt As String = ""
    Friend WithEvents rstChart As AxAckResultViewer.RSTCHART03
    Public msDecimal As String = ""

    Public Property RegNo() As String
        Get
            Return msRegNo
        End Get
        Set(ByVal Value As String)
            msRegNo = Value
        End Set
    End Property

    Public Property ExamCd() As String
        Get
            Return msExamCd
        End Get
        Set(ByVal Value As String)
            msExamCd = Value
        End Set
    End Property

    Public Property ExamNm() As String
        Get
            Return msExamNm
        End Get
        Set(ByVal Value As String)
            msExamNm = Value
        End Set
    End Property

    Public Property EndDate() As String
        Get
            Return msEndDate
        End Get
        Set(ByVal Value As String)
            msEndDate = Value
        End Set
    End Property

    Public Property RefTxt() As String
        Get
            Return msRefTxt
        End Get
        Set(ByVal Value As String)
            msRefTxt = Value
        End Set
    End Property

    Public Sub Display_Chart(ByVal raList As ArrayList)


        rstChart.RegNo = msRegNo
        rstChart.ExamCd = msExamCd
        rstChart.ExamNm = msExamNm
        rstChart.EndDate = msEndDate
        rstChart.RefTxt = msRefTxt
        rstChart.msDecimal = msDecimal
        rstChart.DataGridVisible = True
        rstChart.PointLabelVisible = True
        rstChart.AxisVisible = True


        rstChart.Display_Chart(raList, msExamNm)

    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

    End Sub

    Public Sub New(ByVal rsRegNo As String, ByVal rsExamCd As String, _
                   ByVal rsExamNm As String, ByVal rsEndDate As String, _
                   ByVal rsRefTxt As String, ByVal rsDecimal As String, _
                   ByVal raList As ArrayList)
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.

        msRegNo = rsRegNo
        msExamCd = rsExamCd
        msExamNm = rsExamNm
        msEndDate = rsEndDate
        msRefTxt = rsRefTxt
        msDecimal = rsDecimal

        Display_Chart(raList)

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.rstChart = New AxAckResultViewer.RSTCHART03
        Me.SuspendLayout()
        '
        'rstChart
        '
        Me.rstChart.AxisVisible = False
        Me.rstChart.BackColor = System.Drawing.Color.White
        Me.rstChart.DataGridVisible = False
        Me.rstChart.EndDate = ""
        Me.rstChart.ExamCd = ""
        Me.rstChart.ExamNm = ""
        Me.rstChart.Location = New System.Drawing.Point(-1, 1)
        Me.rstChart.Name = "rstChart"
        Me.rstChart.PointLabelVisible = False
        Me.rstChart.RefTxt = ""
        Me.rstChart.RegNo = ""
        Me.rstChart.Size = New System.Drawing.Size(778, 471)
        Me.rstChart.TabIndex = 0
        Me.rstChart.Viewer = False
        '
        'FGRV11_S01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(776, 470)
        Me.Controls.Add(Me.rstChart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "FGRV11_S01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "그래프 보기"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub FGRV11_S01_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim sFn As String = "FGRV11_S01_KeyDown"

        Try
            If e.KeyCode = Windows.Forms.Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
