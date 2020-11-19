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

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        sbDisplayInit()
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
    Friend WithEvents trst01 As AxAckResultViewer.TOTRST03
    Friend WithEvents axPatInfo As AxAckPatientInfo.AxSpcInfo

    '����: ���� ���ν����� Windows Form �����̳ʿ� �ʿ��մϴ�.
    'Windows Form �����̳ʸ� ����Ͽ� ������ �� �ֽ��ϴ�.  
    '�ڵ� �����⸦ ����Ͽ� �������� ���ʽÿ�.
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
        Me.Text = "��� �˾� â"
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
