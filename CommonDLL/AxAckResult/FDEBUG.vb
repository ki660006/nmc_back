Public Class FDEBUG
    Inherits System.Windows.Forms.Form

#Region " Windows Form �����̳ʿ��� ������ �ڵ� "

    Public Sub New()
        MyBase.New()

        '�� ȣ���� Windows Form �����̳ʿ� �ʿ��մϴ�.
        InitializeComponent()

        'InitializeComponent()�� ȣ���� ������ �ʱ�ȭ �۾��� �߰��Ͻʽÿ�.

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
    Friend WithEvents spd As AxFPSpreadADO.AxfpSpread
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(FDEBUG))
        Me.spd = New AxFPSpreadADO.AxfpSpread
        CType(Me.spd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'spd
        '
        Me.spd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spd.Location = New System.Drawing.Point(0, 0)
        Me.spd.Name = "spd"
        Me.spd.OcxState = CType(resources.GetObject("spd.OcxState"), System.Windows.Forms.AxHost.State)
        Me.spd.Size = New System.Drawing.Size(704, 445)
        Me.spd.TabIndex = 0
        '
        'FDEBUG
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(704, 445)
        Me.Controls.Add(Me.spd)
        Me.Name = "FDEBUG"
        Me.Text = "FDEBUG"
        CType(Me.spd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
