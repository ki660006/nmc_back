<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class STRST01_S01
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
        Me.btnClose = New System.Windows.Forms.Button
        Me.picIMG = New System.Windows.Forms.PictureBox
        CType(Me.picIMG, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(336, 572)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(108, 36)
        Me.btnClose.TabIndex = 86
        Me.btnClose.Text = "닫기(Esc)"
        '
        'picIMG
        '
        Me.picIMG.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picIMG.BackColor = System.Drawing.Color.White
        Me.picIMG.Location = New System.Drawing.Point(57, 36)
        Me.picIMG.Name = "picIMG"
        Me.picIMG.Size = New System.Drawing.Size(374, 506)
        Me.picIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picIMG.TabIndex = 87
        Me.picIMG.TabStop = False
        '
        'STRST01_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 620)
        Me.Controls.Add(Me.picIMG)
        Me.Controls.Add(Me.btnClose)
        Me.Name = "STRST01_S01"
        Me.Text = "STRST01_S01"
        CType(Me.picIMG, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents picIMG As System.Windows.Forms.PictureBox
End Class
