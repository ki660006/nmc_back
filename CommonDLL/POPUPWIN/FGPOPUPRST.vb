Public Class FGPOPUPRST
    Inherits System.Windows.Forms.Form

    Public Event OnKeyDown_Space()

    Public Sub Clear()
        sbDisplayClear()
    End Sub

    Public Sub Display_Result(ByVal r_al_bcno As ArrayList, ByVal rsOrdSlip As String)
        Me.trst01.Display_Result(r_al_bcno, rsOrdSlip)

        Me.Show()
    End Sub

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        sbDisplayInit()
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
    Friend WithEvents trst01 As AxAckResultViewer.TOTRST03
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxSpcInfo

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    'Windows Form 디자이너를 사용하여 수정할 수 있습니다.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.axPatInfo = New AxAckPatientInfo.AxSpcInfo
        Me.trst01 = New AxAckResultViewer.TOTRST03
        Me.SuspendLayout()
        '
        'axPatInfo
        '
        Me.axPatInfo.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.axPatInfo.Location = New System.Drawing.Point(8, 4)
        Me.axPatInfo.Name = "axPatInfo"
        Me.axPatInfo.Size = New System.Drawing.Size(754, 115)
        Me.axPatInfo.TabIndex = 10
        '
        'trst01
        '
        Me.trst01.FastTestDateTime = False
        Me.trst01.Location = New System.Drawing.Point(8, 125)
        Me.trst01.Name = "trst01"
        Me.trst01.Size = New System.Drawing.Size(753, 442)
        Me.trst01.TabIndex = 11
        Me.trst01.UseDblCheck = False
        Me.trst01.UseDebug = False
        Me.trst01.UseLab = False
        Me.trst01.ViewMark = False
        Me.trst01.ViewReportOnly = False
        '
        'FGPOPUPRST
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(769, 576)
        Me.Controls.Add(Me.trst01)
        Me.Controls.Add(Me.axPatInfo)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FGPOPUPRST"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "결과 팝업 창"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub sbDisplay_SpcInfo(ByVal r_si As AxAckResultViewer.SpecimenInfo)
        Dim sFn As String = "sbDisplay_SpcInfo"

        Try
            axPatInfo.sbDisplay_SpcInfo(r_si)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub sbDisplayClear()
        Dim sFn As String = "Sub sbDisplayClear()"

        Try
            Me.axPatInfo.sbInit()
            Me.trst01.Clear()


        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message)

        End Try
    End Sub

    Private Sub sbDisplayInit()
        Dim sFn As String = "Sub sbDisplayInit()"

        Try
            Me.axPatInfo.sbInit()
            Me.trst01.Clear()

        Catch ex As Exception
            MsgBox(sFn + " - " + ex.Message)

        End Try
    End Sub


    '<----- Control Event ----->
    Private Sub FGPOPUPRST_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Windows.Forms.Keys.Space
                RaiseEvent OnKeyDown_Space()

            Case Windows.Forms.Keys.Escape
                Me.Close()

            Case Windows.Forms.Keys.F1
#If DEBUG Then
                MsgBox(Me.Location.ToString())
#End If

        End Select
    End Sub

    Private Sub FGPOPUPRST_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        COMMON.CommFN.DS_FormDesige.sbInti(Me)
    End Sub

    Private Sub trst01_ChangedBcNo1(ByVal spcinfo As AxAckResultViewer.SpecimenInfo) Handles trst01.ChangedBcNo
        sbDisplay_SpcInfo(spcinfo)
    End Sub
End Class
