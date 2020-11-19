<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RSTCHART03
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RSTCHART03))
        Me.btnView = New System.Windows.Forms.Button
        Me.chxData = New AxTeeChart.AxTChart
        CType(Me.chxData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnView
        '
        Me.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnView.Location = New System.Drawing.Point(0, 0)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(57, 25)
        Me.btnView.TabIndex = 2
        Me.btnView.Text = "확대(+)"
        Me.btnView.UseVisualStyleBackColor = True
        '
        'chxData
        '
        Me.chxData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chxData.Enabled = True
        Me.chxData.Location = New System.Drawing.Point(0, 0)
        Me.chxData.Name = "chxData"
        Me.chxData.OcxState = CType(resources.GetObject("chxData.OcxState"), System.Windows.Forms.AxHost.State)
        Me.chxData.Size = New System.Drawing.Size(751, 235)
        Me.chxData.TabIndex = 3
        '
        'RSTCHART03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.btnView)
        Me.Controls.Add(Me.chxData)
        Me.Name = "RSTCHART03"
        Me.Size = New System.Drawing.Size(751, 235)
        CType(Me.chxData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents chxData As AxTeeChart.AxTChart

End Class
