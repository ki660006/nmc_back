<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDHELP_TEST_NEW_S01
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
        Me.picFileImg = New System.Windows.Forms.PictureBox()
        CType(Me.picFileImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picFileImg
        '
        Me.picFileImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picFileImg.Location = New System.Drawing.Point(0, 0)
        Me.picFileImg.Name = "picFileImg"
        Me.picFileImg.Size = New System.Drawing.Size(672, 605)
        Me.picFileImg.TabIndex = 0
        Me.picFileImg.TabStop = False
        '
        'FGCDHELP_TEST_NEW_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(672, 605)
        Me.Controls.Add(Me.picFileImg)
        Me.Name = "FGCDHELP_TEST_NEW_S01"
        Me.Text = "FGCDHELP_TEST_NEW_S01"
        CType(Me.picFileImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picFileImg As System.Windows.Forms.PictureBox
End Class
