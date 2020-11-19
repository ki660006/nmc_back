<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGCDMSG01
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FGCDMSG01))
        Me.picMsg = New System.Windows.Forms.PictureBox
        Me.imgMsg = New System.Windows.Forms.ImageList(Me.components)
        Me.lblBack = New System.Windows.Forms.Label
        Me.txtMsg = New System.Windows.Forms.TextBox
        Me.btnMok = New System.Windows.Forms.Button
        Me.btnCok = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        CType(Me.picMsg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picMsg
        '
        Me.picMsg.Image = CType(resources.GetObject("picMsg.Image"), System.Drawing.Image)
        Me.picMsg.InitialImage = Nothing
        Me.picMsg.Location = New System.Drawing.Point(3, 3)
        Me.picMsg.Margin = New System.Windows.Forms.Padding(1)
        Me.picMsg.Name = "picMsg"
        Me.picMsg.Size = New System.Drawing.Size(72, 84)
        Me.picMsg.TabIndex = 0
        Me.picMsg.TabStop = False
        '
        'imgMsg
        '
        Me.imgMsg.ImageStream = CType(resources.GetObject("imgMsg.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgMsg.TransparentColor = System.Drawing.Color.Transparent
        Me.imgMsg.Images.SetKeyName(0, "message_infor.jpg")
        Me.imgMsg.Images.SetKeyName(1, "message_caption.jpg")
        Me.imgMsg.Images.SetKeyName(2, "message_error.jpg")
        '
        'lblBack
        '
        Me.lblBack.BackColor = System.Drawing.Color.White
        Me.lblBack.Location = New System.Drawing.Point(4, 4)
        Me.lblBack.Name = "lblBack"
        Me.lblBack.Size = New System.Drawing.Size(496, 84)
        Me.lblBack.TabIndex = 1
        '
        'txtMsg
        '
        Me.txtMsg.BackColor = System.Drawing.SystemColors.Window
        Me.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMsg.Font = New System.Drawing.Font("굴림", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.txtMsg.Location = New System.Drawing.Point(84, 11)
        Me.txtMsg.Margin = New System.Windows.Forms.Padding(1)
        Me.txtMsg.Multiline = True
        Me.txtMsg.Name = "txtMsg"
        Me.txtMsg.ReadOnly = True
        Me.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsg.Size = New System.Drawing.Size(409, 70)
        Me.txtMsg.TabIndex = 2
        '
        'btnMok
        '
        Me.btnMok.Location = New System.Drawing.Point(210, 92)
        Me.btnMok.Margin = New System.Windows.Forms.Padding(1)
        Me.btnMok.Name = "btnMok"
        Me.btnMok.Size = New System.Drawing.Size(83, 26)
        Me.btnMok.TabIndex = 3
        Me.btnMok.Text = "확 인"
        Me.btnMok.UseVisualStyleBackColor = True
        '
        'btnCok
        '
        Me.btnCok.Location = New System.Drawing.Point(170, 92)
        Me.btnCok.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCok.Name = "btnCok"
        Me.btnCok.Size = New System.Drawing.Size(83, 26)
        Me.btnCok.TabIndex = 4
        Me.btnCok.Text = "확 인"
        Me.btnCok.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(252, 92)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(1)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 26)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "취 소"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'FGCDMSG01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(503, 123)
        Me.Controls.Add(Me.btnMok)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnCok)
        Me.Controls.Add(Me.txtMsg)
        Me.Controls.Add(Me.picMsg)
        Me.Controls.Add(Me.lblBack)
        Me.KeyPreview = True
        Me.Name = "FGCDMSG01"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        CType(Me.picMsg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picMsg As System.Windows.Forms.PictureBox
    Friend WithEvents imgMsg As System.Windows.Forms.ImageList
    Friend WithEvents lblBack As System.Windows.Forms.Label
    Friend WithEvents txtMsg As System.Windows.Forms.TextBox
    Friend WithEvents btnMok As System.Windows.Forms.Button
    Friend WithEvents btnCok As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
