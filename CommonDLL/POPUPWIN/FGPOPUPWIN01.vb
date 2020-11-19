
Imports System
Imports System.Drawing

Imports COMMON.CommFN

Public Class FGPOPUPWIN01
    Inherits System.Windows.Forms.Form

    Public fdtRetVal As Date

    Public foBaseObj As System.Windows.Forms.Control

#Region " Windows Form 디자이너에서 생성한 코드 "

    Public Sub New()
        MyBase.New()

        '이 호출은 Windows Form 디자이너에 필요합니다.
        InitializeComponent()

        'InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
        fnFormInitialize()
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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents moncDate As System.Windows.Forms.MonthCalendar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.moncDate = New System.Windows.Forms.MonthCalendar
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.moncDate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(168, 148)
        Me.Panel1.TabIndex = 0
        '
        'moncDate
        '
        Me.moncDate.Location = New System.Drawing.Point(0, 0)
        Me.moncDate.MaxSelectionCount = 1
        Me.moncDate.Name = "moncDate"
        Me.moncDate.TabIndex = 2
        '
        'FGPOPUPWIN01
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(168, 148)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FGPOPUPWIN01"
        Me.ShowInTaskbar = False
        Me.Text = "FGPOPUPWIN01"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub fnFormInitialize()
        Dim PointXY As System.Drawing.Point

        PointXY = Fn.CtrlLocationXY(CType(foBaseObj, System.Windows.Forms.Control))

        Me.Left = PointXY.X
        Me.Top = PointXY.Y
    End Sub

    Private Sub moncDate_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles moncDate.DateSelected
        'fdtRetVal = moncDate
        fdtRetVal = CType("2003-01-01", Date)
        Me.Close()
    End Sub

End Class
