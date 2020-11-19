
Imports System
Imports System.Drawing

Imports COMMON.CommFN

Public Class FGPOPUPWIN01
    Inherits System.Windows.Forms.Form

    Public fdtRetVal As Date

    Public foBaseObj As System.Windows.Forms.Control

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.
        fnFormInitialize()
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
