<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RSTCHART04
    Inherits System.Windows.Forms.UserControl

    'UserControl은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.picChart = New System.Windows.Forms.PictureBox
        Me.btnView = New System.Windows.Forms.Button
        CType(Me.picChart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picChart
        '
        Me.picChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picChart.Location = New System.Drawing.Point(0, 0)
        Me.picChart.Name = "picChart"
        Me.picChart.Size = New System.Drawing.Size(762, 167)
        Me.picChart.TabIndex = 0
        Me.picChart.TabStop = False
        '
        'btnView
        '
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnView.Location = New System.Drawing.Point(0, 0)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(57, 25)
        Me.btnView.TabIndex = 3
        Me.btnView.Text = "확대(+)"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'RSTCHART04
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.picChart)
        Me.Name = "RSTCHART04"
        Me.Size = New System.Drawing.Size(762, 167)
        CType(Me.picChart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picChart As System.Windows.Forms.PictureBox
    Friend WithEvents btnView As System.Windows.Forms.Button

End Class
