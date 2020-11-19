<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FGLOGIN_S01
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtUsrPw2 = New System.Windows.Forms.TextBox
        Me.txtUsrPw1 = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(82, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(165, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(6, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 21)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "비밀번호"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("굴림체", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 21)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "비밀번호 확인"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtUsrPw2
        '
        Me.txtUsrPw2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrPw2.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.txtUsrPw2.Location = New System.Drawing.Point(107, 31)
        Me.txtUsrPw2.MaxLength = 20
        Me.txtUsrPw2.Name = "txtUsrPw2"
        Me.txtUsrPw2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUsrPw2.Size = New System.Drawing.Size(148, 21)
        Me.txtUsrPw2.TabIndex = 5
        Me.txtUsrPw2.Text = "13245678901234567890"
        '
        'txtUsrPw1
        '
        Me.txtUsrPw1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtUsrPw1.Enabled = False
        Me.txtUsrPw1.Location = New System.Drawing.Point(107, 6)
        Me.txtUsrPw1.MaxLength = 20
        Me.txtUsrPw1.Name = "txtUsrPw1"
        Me.txtUsrPw1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtUsrPw1.Size = New System.Drawing.Size(148, 21)
        Me.txtUsrPw1.TabIndex = 4
        Me.txtUsrPw1.Text = "13245678901234567890"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(160, 68)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(96, 26)
        Me.btnCancel.TabIndex = 42
        Me.btnCancel.Text = "취  소"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(65, 68)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(96, 26)
        Me.btnOk.TabIndex = 41
        Me.btnOk.Text = "확  인"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'FGLOGIN_S01
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(260, 101)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtUsrPw2)
        Me.Controls.Add(Me.txtUsrPw1)
        Me.KeyPreview = True
        Me.Name = "FGLOGIN_S01"
        Me.Text = "※ 신규아이디 비밀번호 확인"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtUsrPw2 As System.Windows.Forms.TextBox
    Friend WithEvents txtUsrPw1 As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
