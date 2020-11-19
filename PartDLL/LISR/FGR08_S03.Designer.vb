<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGR08_S03
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
        Me.picFileImg = New System.Windows.Forms.PictureBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.cboImgItem = New System.Windows.Forms.ComboBox
        CType(Me.picFileImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'picFileImg
        '
        Me.picFileImg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picFileImg.Location = New System.Drawing.Point(0, 0)
        Me.picFileImg.Name = "picFileImg"
        Me.picFileImg.Size = New System.Drawing.Size(768, 966)
        Me.picFileImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFileImg.TabIndex = 0
        Me.picFileImg.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.picFileImg)
        Me.Panel1.Location = New System.Drawing.Point(2, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(768, 966)
        Me.Panel1.TabIndex = 1
        '
        'cboImgItem
        '
        Me.cboImgItem.FormattingEnabled = True
        Me.cboImgItem.Location = New System.Drawing.Point(2, 2)
        Me.cboImgItem.Name = "cboImgItem"
        Me.cboImgItem.Size = New System.Drawing.Size(242, 20)
        Me.cboImgItem.TabIndex = 1
        '
        'FGR08_S03
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(772, 1006)
        Me.Controls.Add(Me.cboImgItem)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FGR08_S03"
        Me.Text = "FGR08_S03"
        CType(Me.picFileImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents picFileImg As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboImgItem As System.Windows.Forms.ComboBox
End Class
